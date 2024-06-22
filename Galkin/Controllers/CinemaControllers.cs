using Galkin.Context;
using Galkin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Controllers
{
    [Route("api/CinemaControllers")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CinemaControllers : Controller
    {
        ///<summary>
        ///Получение списка кинотеатров
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="price"></param>
        /// <param name="vozm"></param>
        /// <returns>Список кинотеатров</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("get")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Get(string sort = "vozr")
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    IEnumerable<Cinema> cinemas = context.Cinema;
                    if (sort == "yb")
                    {
                        cinemas = context.Cinema.OrderByDescending(x => x.time).ToList();
                    }
                    else if (sort == "vozr")
                        cinemas = context.Cinema.OrderBy(x => x.time).ToList();
                    return Json(cinemas);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        ///<summary>
        ///Удаление списка кинотеатров
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="price"></param>
        /// <param name="vozm"></param>
        /// <returns>Список кинотеатров</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Delete(int id, [FromForm] string Token)
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    var existing = context.Cinema.FirstOrDefault(x => x.id == id);
                    if (existing == null) return StatusCode(400, "Ошибка в данных");
                    context.Cinema.Remove(existing);
                    context.SaveChanges();
                    return StatusCode(200, "Успешное удаление");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        ///<summary>
        ///Добавление списка кинотеатров
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="price"></param>
        /// <param name="vozm"></param>
        /// <returns>Список кинотеатров</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost("Add")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] Cinema cinema, [FromForm] string Token)
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    if (cinema == null) return StatusCode(400, "Ошибка в данных");
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    context.Cinema.Add(cinema);
                    context.SaveChanges();
                    return StatusCode(200, "Успешное добавление");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        ///<summary>
        ///Изменение списка кинотеатров
        /// </summary>
        /// <param name="name"></param>
        /// <param name="time"></param>
        /// <param name="price"></param>
        /// <param name="vozm"></param>
        /// <returns>Список кинотеатров</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPut("Change")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Change([FromForm] Cinema cinemas, [FromForm] string Token)
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    var existing = context.Cinema.FirstOrDefault(x => x.id == cinemas.id);
                    if (existing == null) return StatusCode(400, "Ошибка в данных");
                    if (cinemas == null) return StatusCode(400, "Ошибка в данных");
                    existing.name = cinemas.name;
                    existing.time = cinemas.time;
                    existing.price = cinemas.price;
                    existing.vozm = cinemas.vozm;
                    context.Update(existing);
                    context.SaveChanges();
                    return StatusCode(200, "Успешное изменение");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
