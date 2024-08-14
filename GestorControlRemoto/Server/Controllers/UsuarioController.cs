using GestorControlRemoto.Shared.DTOs;
using GestorControlRemoto.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestorControlRemoto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _confi;
        public UsuarioController(ApplicationDbContext context, IConfiguration confi)
        {
            _context = context;
            _confi = confi;
        }

        [HttpGet("Datos"), Authorize(Roles = "Usuario,Admin")]
        public async Task<ActionResult<List<Usuario>>> GetEjemplo()
        {
            var lista = await _context.Usuarios.ToListAsync();
            return Ok(lista);
        }

        public static Usuario usuario = new Usuario();
        [HttpPost("Registrar")]
        public async Task<ActionResult<string>> CreateEjemplo(UsuarioDTO objeto)
        {
            CreatePasswordHash(objeto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.NombreUsuario = objeto.NombreUsuario;
            usuario.NombreCuenta = objeto.NombreCuenta;
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.Rol = "Usuario";
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            var respuesta = "Registrado Con Exito";
            return respuesta;
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> InicioSesion(UsuarioDTO usuario)
        {
            var cuenta = await _context.Usuarios.Where(x => x.NombreCuenta == usuario.NombreCuenta).FirstOrDefaultAsync();
            if (cuenta == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            if (!VerifyPasswordHash(usuario.Password, cuenta.PasswordHash, cuenta.PasswordSalt))
            {
                return BadRequest("Contraseña incorrecta");
            }
            string token = CreateToken(cuenta);
            return Ok(token);
        }

        // Funcion que encripta la clave que viene desde el formulario
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        

        // Verifica la contraseña con el algoritmo usando paswordHash y passwordSalt
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Usuario user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.NombreUsuario),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
               _confi["JwtConfig:Secret"].ToString()));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }



    }
}
