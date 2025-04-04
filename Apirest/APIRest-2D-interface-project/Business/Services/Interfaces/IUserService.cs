﻿using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Presentation.DTOs.AuthentificationDTOs.Request;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace APIRest_2D_interface_project.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> UserRegister(User user);
        Task<Boolean> UserLogin(User user);
    }
}
