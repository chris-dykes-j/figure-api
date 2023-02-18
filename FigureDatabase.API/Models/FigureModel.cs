using System.ComponentModel.DataAnnotations;

namespace FigureDatabase.API.Models;

public class FigureModel
{
   [Key]
   public int Id { get; set; }
   
   [Required]
   public string FigureName { get; set; }
   
   [Required]
   public string CharacterName { get; set; }
   
   [Required]
   public string BrandName { get; set; }
   
   public DateTime? ReleaseDate { get; set; }
   
   public int? ReleasePrice { get; set; } 
   
   public long? JanCode { get; set; }
   
   public string? Series { get; set; }
   
   public string? ProductLine { get; set; }
   
   public string? Sculptor { get; set; }
}