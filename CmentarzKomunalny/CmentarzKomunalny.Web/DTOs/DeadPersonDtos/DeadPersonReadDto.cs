namespace CmentarzKomunalny.Web.DTOs.DeadPersonDtos
{
    public class DeadPersonReadDto
    {
        public int IdDeadPerson { get; set; }
        public int LodgingId { get; set; }
        public string Name { get; set; }
        public string DateOfDeath { get; set; }
    }
}
