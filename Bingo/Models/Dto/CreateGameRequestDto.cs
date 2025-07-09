namespace Bingo.Models.Dto
{
    public class CreateGameRequestDto
    {
        public String Name { get; set; }
        public List<String> BoardFields { get; set; }
    }
}
