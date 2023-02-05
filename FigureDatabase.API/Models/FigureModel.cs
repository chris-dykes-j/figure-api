using System.ComponentModel.DataAnnotations;

namespace FigureDatabase.API.Models;

public class FigureModel
{
   public FigureModel(int id, string name)
   {
      Id = id;
      Name = name;
   }

   [Key]
   public int Id { get; set; }
   
   [Required]
   public string Name { get; set; }
}