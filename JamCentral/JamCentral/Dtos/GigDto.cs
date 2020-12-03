using System;

namespace JamCentral.Dtos
{
    public class GigDto
    {
        public int Id { get; set; }
        public ApplicationUserDto Artist { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public bool IsCanceled { get; set; } 
    }
}