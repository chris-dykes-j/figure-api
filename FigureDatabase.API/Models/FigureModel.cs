using System.ComponentModel.DataAnnotations;

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