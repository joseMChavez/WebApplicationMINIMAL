using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationMINIMAL.Model
{
    public class Todo
    {
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
