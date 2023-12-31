﻿using ApiRest.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using entityesLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using bussineslayer;
using entityesLayer.interfaces;
using Microsoft.AspNetCore.Cors;

namespace ApiRest.Controllers
{
    [Route("api/registeredusers/")]
    [EnableCors("MyPolicy")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuarioController : ControllerBase
    {
        private usuarioBL UsuarioBL = new usuarioBL();

        // Codigo para agregar la seguridad JWT
        private readonly JwtAuthenticationService authService;
        public UsuarioController(JwtAuthenticationService pAuthService)
        {
            authService = pAuthService;
        }
        //************************************************

        [HttpPost("Buscar")]
        public async Task<List<registeredUsers>> search([FromBody] SearchUser pUser)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string struser = JsonSerializer.Serialize(pUser);
            registeredUsers users = JsonSerializer.Deserialize<registeredUsers>(struser, option);
            return await UsuarioBL.SearchAsync(users);

        }

        [HttpGet]
        public async Task<IEnumerable<registeredUsers>> Get()
        {
            return await UsuarioBL.ObtainAllAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] registeredUsers pUsuario)
        {
            var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            string strUsuario = JsonSerializer.Serialize(pUsuario);
            registeredUsers usuario = JsonSerializer.Deserialize<registeredUsers>(strUsuario, option);
            if (usuario.id == id)
            {

                await UsuarioBL.ModifyAsync(usuario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                registeredUsers usuario = new registeredUsers();
                usuario.id = id;
                await UsuarioBL.DeleteAsync(usuario);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }

}

