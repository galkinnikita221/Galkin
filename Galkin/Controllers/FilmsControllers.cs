using Galkin.Context;
using Galkin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galkin.Controllers
{
    [Route("api/FilmsControllers")]
    [ApiExplorerSettings(GroupName = "v3")]
    public class FilmsControllers : Controller
    {
        ///<summary>
        ///Получение списка фильмов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="description"></param>
        /// <returns>Список фильмов</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("get")]
        [ProducesResponseType(typeof(Films), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Get(string sort = "vozr")
        {
            try
            {
                using (var context = new FilmsContext())
                {
                    IEnumerable<Films> cinemas = context.Films;
                    if (sort == "vozr")
                    {
                        cinemas = context.Films.OrderByDescending(x => x.name).ToList();
                    }
                    else if (sort == "yb")
                        cinemas = context.Films.OrderBy(x => x.name).ToList();
                    return Json(cinemas);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        ///<summary>
        ///Удаление списка фильмов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="description"></param>
        /// <returns>Список фильмов</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(Films), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Delete(int id, [FromForm] string Token)
        {
            try
            {
                using (var context = new FilmsContext())
                {
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    var existing = context.Films.FirstOrDefault(x => x.id == id);
                    if (existing == null) return StatusCode(400, "Ошибка в данных");
                    context.Films.Remove(existing);
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
        ///Добавление списка фильмов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="description"></param>
        /// <returns>Список фильмов</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost("Add")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] Films film, [FromForm] string Token)
        {
            try
            {
                using (var context = new FilmsContext())
                {
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    if (film == null) return StatusCode(400, "Ошибка в данных");
                    context.Films.Add(film);
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
        ///Изменение списка фильмов
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="description"></param>
        /// <returns>Список фильмов</returns>
        /// <response code="200">Успешное получение</response>
        /// <response code="400">Ошибка в данных</response>
        /// <response code="401">Неавторизованный пользователь</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPut("Change")]
        [ProducesResponseType(typeof(Cinema), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public ActionResult Change([FromForm] Films film, [FromForm] string Token)
        {
            try
            {
                using (var context = new FilmsContext())
                {
                    var users = new UserContext();
                    if (users.User.FirstOrDefault(x => x.Token == Token) == null) return StatusCode(401, "Неавторизованный пользователь");
                    var existing = context.Films.FirstOrDefault(x => x.id == film.id);
                    if (existing == null) return StatusCode(400, "Ошибка в данных");
                    existing.name = film.name;
                    existing.author = film.author;
                    existing.description = film.description;
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
