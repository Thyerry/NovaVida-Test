namespace Domain.Model;

public class ProductReview
{
    public int codigo { get; set; }
    public string data { get; set; }
    public string data_prop { get; set; }
    public string titulo { get; set; }
    public string opiniao { get; set; }
    public int avaliacao { get; set; }
    public string info_positivo { get; set; }
    public string info_negativo { get; set; }
    public Cliente cliente { get; set; }
}