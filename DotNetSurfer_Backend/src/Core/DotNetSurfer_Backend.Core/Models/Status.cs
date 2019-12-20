
namespace DotNetSurfer_Backend.Core.Models
{
    public class Status
    {
        public int StatusId { get; set; }

        public string CurrentStatus { get; set; }
    }

    public enum StatusType
    {
        Requested = 0,
        Ongoing = 1,
        Pending = 2,
        Completed = 3
    }
}
