using Domain.Contracts.Service;
using Domain.Model;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Domain.Service;

public class WebCrawlerService : IWebCrawlerService
{
    private readonly KabumUrlOptions _kabumUrlOptions;

    public WebCrawlerService(IOptionsSnapshot<KabumUrlOptions> options)
    {
        _kabumUrlOptions = options.Value;
    }

    public async Task<List<ProductModel>> GetProductsFromKabum(string productSearchTerm)
    {
        var loadUrl = $"{_kabumUrlOptions.SearchUrl}/mouse-razer";
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
        var productImageUrl = GetNodeAttribute(node, $"{node.XPath}/img", "src");
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
        if (innerText != null && innerText.Split(" ").Length > 1)
        {
            var price = innerText.Split(" ")[1];
            return double.TryParse(price, NumberStyles.Any, new CultureInfo("pt-BR"), out var parsedPrice)
                ? parsedPrice
                : 0.0;
        }

        return 0.0;
    }

    private string GetNodeAttribute(HtmlNode node, string xPath, string attr = "")
    {
        var selectedNode = string.IsNullOrEmpty(attr)
            ? node
            : node.SelectSingleNode(xPath);
        return selectedNode?.GetAttributeValue(attr, "");
    }
}