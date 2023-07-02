using Newtonsoft.Json;

namespace Domain.Model;

public class Cliente
{
    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("cidade")]
    public string Cidade { get; set; }

    [JsonProperty("uf")]
    public string Uf { get; set; }
}