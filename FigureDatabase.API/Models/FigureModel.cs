using System.ComponentModel.DataAnnotations;

namespace FigureDatabase.API.Models;

public class FigureModel
{
   [Key]
   public int Id { get; set; }

   public string Name { get; set; } = null!;

   public string Character { get; set; } = null!;

   public string Brand { get; set; } = null!;
   
   public int? Year { get; set; }
   
   public string? Month { get; set; }
   
   public int? ReleasePrice { get; set; } 
   
   public long? JanCode { get; set; }
   
   public string? Series { get; set; }
   
   public string? ProductLine { get; set; }
   
   public string? Sculptor { get; set; }
}