using Galkin.Context;
using Galkin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Controllers
{
    [Route("api/UserControllers")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserControllers : Controller
    {
        ///<summary>
        ///Получение списка авторизованных пользователей
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Получение списка пользователей</returns>
        /// <response code="200">Успешное получение данных</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Неавторизованный пользователь</response>
        [HttpPost("SignIn")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Get([FromForm] string login, [FromForm] string password)
        {
            try
            {
                using (var context = new UserContext())
                {
                    if (login == null || password == null) return StatusCode(400, "Ошибка в данных");
                    var users = context.User.FirstOrDefault(x => x.login == login && x.password == password);
                    if (users == null) return StatusCode(401, "Неавторизованный пользователь");
                    users.Token = GenerateToken();
                    context.SaveChanges();
                    return Json(users.Token);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public static string GenerateToken()
        {
            Random rnd = new Random();
            const string chars = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";
            return new string(Enumerable.Repeat(chars, 10).Select(s => s[rnd.Next(chars.Length)]).ToArray());
        }
    }
}
