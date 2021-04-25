using AutoMapper;
using JokesOnYou.Web.Api.DTOs;
using JokesOnYou.Web.Api.Extensions;
using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace JokesOnYou.Web.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class JokeController
    {
        
    }
}
