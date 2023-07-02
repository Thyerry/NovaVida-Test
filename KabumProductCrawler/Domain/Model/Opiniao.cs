using Newtonsoft.Json;

namespace Domain.Model;

public class Opiniao
{
    [JsonProperty("codigo")]
    public int Codigo { get; set; }

    [JsonProperty("data")]
    public string Data { get; set; }

    [JsonProperty("data_prop")]
    public DateTime DataProp { get; set; }

    [JsonProperty("titulo")]
    public string Titulo { get; set; }

    [JsonProperty("opiniao")]
    public string OpiniaoText { get; set; }

    [JsonProperty("avaliacao")]
    public int Avaliacao { get; set; }

    [JsonProperty("info_positivo")]
    public string InfoPositivo { get; set; }

    [JsonProperty("info_negativo")]
    public string InfoNegativo { get; set; }

    [JsonProperty("cliente")]
    public Cliente Cliente { get; set; }
}