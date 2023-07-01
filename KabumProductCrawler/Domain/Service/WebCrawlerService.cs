using Domain.Contracts.Service;
using Domain.Model;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Domain.Service;

public class WebCrawlerService : IWebCrawlerService
{
    private readonly KabumUrlOptions _kabumUrlOptions;

    public WebCrawlerService(IConfiguration configuration)
    {
        _kabumUrlOptions = new KabumUrlOptions
        {
            ProductUrl = configuration["KabumUrls:ProductUrl"] ?? throw new ArgumentNullException(nameof(configuration)),
            ReviewEndpoint = configuration["KabumUrls:ReviewEndpoint"] ?? throw new ArgumentNullException(nameof(configuration)),
            SearchUrl = configuration["KabumUrls:SearchUrl"] ?? throw new ArgumentNullException(nameof(configuration))
        };
    }

    public async Task<List<ProductModel>> GetProductsFromKabum(string productSearchTerm)
    {
        productSearchTerm = string.Join("-", productSearchTerm.Split(" "));
        var loadUrl = $"{_kabumUrlOptions.SearchUrl}/{productSearchTerm}";
        var web = new HtmlWeb();
        var doc = await web.LoadFromWebAsync(loadUrl);
        var productNodes = doc.DocumentNode.SelectNodes("//a[@class='sc-d55b419d-10 izMLCN productLink']");
        var result = new List<ProductModel>();

        if (productNodes is null)
            return result;

        result.AddRange(productNodes.Where(node => node != null).Select(ParseNodeToProductModel));

        return result;
    }

    public Task<List<ProductReview>> GetProductReviews(int quantity)
    {
        throw new NotImplementedException();
    }

    private ProductModel ParseNodeToProductModel(HtmlNode node)
    {
        var productId = ParseNodeAttributeToInt(node, "data-smarthintproductid");
        var productName = GetNodeInnerText(node, $"{node.XPath}/div/button/div");
        var productPrice = GetPriceFromNodeInnerText(node, $"{node.XPath}/div/div/span[2]");
        var productImageUrl = GetNodeAttribute(node, "src", $"{node.XPath}/img");
        var productUrl = GetNodeAttribute(node, "href");

        return new ProductModel
        {
            Id = productId,
            Name = productName,
            Price = productPrice,
            Url = productUrl,
            ImageUrl = productImageUrl
        };
    }

    private int ParseNodeAttributeToInt(HtmlNode node, string attr)
    {
        var attrValue = node.GetAttributeValue(attr, "");
        return int.TryParse(attrValue, out var parsedValue) ? parsedValue : 0;
    }

    private string? GetNodeInnerText(HtmlNode node, string xPath)
    {
        var selectedNode = node.SelectSingleNode(xPath);
        return selectedNode?.InnerText;
    }

    private double GetPriceFromNodeInnerText(HtmlNode node, string xPath)
    {
        var innerText = GetNodeInnerText(node, xPath);
        if (innerText != null && innerText.Split().Length > 1)
        {
            var price = innerText.Split()[1];
            return double.TryParse(price, NumberStyles.Any, new CultureInfo("pt-BR"), out var parsedPrice)
                ? parsedPrice
                : 0.0;
        }

        return 0.0;
    }

    private string GetNodeAttribute(HtmlNode node, string attr, string xPath = "")
    {
        var selectedNode = string.IsNullOrEmpty(xPath)
            ? node
            : node.SelectSingleNode(xPath);
        return selectedNode?.GetAttributeValue(attr, "");
    }
}