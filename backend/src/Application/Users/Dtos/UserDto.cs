﻿using Application.Common.Models;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace Application.Users.Dtos
{
    public class UserDto: Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }

        public ICollection<RoleDto> Roles { get; set; }
    }
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(_ => _.FirstName).NotNull().NotEmpty();
            RuleFor(_ => _.LastName).NotNull().NotEmpty();
        }
    }
}
