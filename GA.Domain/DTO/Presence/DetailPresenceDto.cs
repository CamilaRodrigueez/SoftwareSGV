namespace GA.Domain.DTO.Presence
{
    public class DetailPresenceDto : StudentPresenceDto
    {
        public int IdPresence { get; set; }
        public string StrClass { get; set; }
        public string StrDate { get; set; }
        public string Ficha { get; set; }
    }
}
