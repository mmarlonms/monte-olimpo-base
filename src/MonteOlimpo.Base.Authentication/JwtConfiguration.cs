﻿using System.ComponentModel.DataAnnotations;

namespace MonteOlimpo.Base.Authentication
{
    public class JwtConfiguration
    {

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string Secret { get; set; }

        public string Audience { get; set; }

        public int ExpirationInMinutes { get; set; } = 180;
    }
}