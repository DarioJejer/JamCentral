using System.Collections.Generic;

namespace JamCentral.Dtos
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<FollowingDto> Followees { get; set; }
        public ICollection<AttendenceDto> Attendences { get; set; }
    }
}