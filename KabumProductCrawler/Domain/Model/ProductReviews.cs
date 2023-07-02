using Newtonsoft.Json;

namespace Domain.Model;

public class ProductReviews
{
    [JsonProperty("opinioes")]
    public List<Opiniao> Opinioes { get; set; }

    [JsonProperty("sucesso")]
    public bool Sucesso { get; set; }
}