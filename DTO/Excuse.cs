using System.Text.Json.Serialization;
using Models;

namespace DTO
{
    public class Excuse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ExcuseCategory Category { get; set; }
    }
}