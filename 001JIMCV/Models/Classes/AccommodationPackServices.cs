﻿namespace _001JIMCV.Models.Classes
{
    public class AccommodationPackServices
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public Accommodation Accommodation { get; set; }

        public int PackServicesId { get; set; }
        public PackServices PackServices { get; set; }
    }
}
