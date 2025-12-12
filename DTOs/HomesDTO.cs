

namespace BoilerMonitoringAPI.DTOs
{



    public class HomesDTO
    {
        public Guid UserID { get; set; }
        public string HomeName { get; set; }
        public string Address { get; set; }
    }
    public class HomesDTOID : HomesDTO
    {
        public Guid HomeID { get; set; }
    }

    public class HomeDTOMembers : HomesDTOID
    {
        public ICollection<UserDTOID> Memebers { get; set; }
    }


    public class HomeUser
    {
        public string Email { get; set; }
        public Guid HomeID { get; set; }

    }
}
