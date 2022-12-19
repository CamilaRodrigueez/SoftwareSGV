namespace GA.Domain.DTO.Presence
{
    public class ResultPresenceDto
    {
        public int IdUser { get; set; }
        public int IdFicha { get; set; }
        public int IdClass { get; set; }
        public string Identification { get; set; }
        public string FullName { get; set; }
        public int Count { get; set; }
    }
}
