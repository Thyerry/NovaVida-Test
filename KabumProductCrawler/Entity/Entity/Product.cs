using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity;

[Table("T_PRODUCT")]
public class Product
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("NAME")]
    public string Name { get; set; }

    [Column("PRICE")]
    public double Price { get; set; }

    [Column("URL")]
    public string Url { get; set; }

    [Column("IMAGE_URL")]
    public string ImageUrl { get; set; }
    
    public bool Equals(Product prd)
    {
        return Id == prd.Id
               && Name == prd.Name
               && Price == prd.Price
               && Url == prd.Url
               && ImageUrl == prd.ImageUrl;
    }
}