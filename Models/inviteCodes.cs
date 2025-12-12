namespace BoilerMonitoringAPI.Models
{
    public class inviteCodes
    {
        public string inviteCode { get; set; }
        public Guid HomeID { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid CreatedByUserID { get; set; }

        public bool IsSingleuse { get; set; }
    }
}
