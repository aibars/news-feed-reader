﻿using System.ComponentModel.DataAnnotations;

namespace NewsFeedReader.Domain.ApiModels.Request
{
    public class RegisterRequestDto
    {
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
