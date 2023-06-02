﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;


namespace _001JIMCV.Models.Classes
{
    public class Restauration
    {
       

            public int Id { get; set; }
            public int ProviderId { get; set; }
            public string Pays { get; set; }
            public string Ville { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public string Localisation { get; set; }
            public string Menu { get; set; }
            public decimal Tarif { get; set; }

            public string Email { get; set; }
            public string Status { get; set; }
            public byte[] Images { get; set; }
        }
    }
