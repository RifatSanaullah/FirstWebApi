using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        FirstWebApiEntities _dbEntities;

        public UserController(FirstWebApiEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        [HttpGet]
        public ActionResult <IEnumerable<User>> GetUsers()
        {
            return _dbEntities.users.ToList();
        }

        [HttpGet("{id:int}")]
        public ActionResult <User> GetUser(int id)
        {
            return _dbEntities.users.Find(id);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUsers([FromForm]User model)
        {
                _dbEntities.users.Add(model);
                _dbEntities.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromForm]User model)
        {
            try
            {
                if (id == model.Id)
                {
                    _dbEntities.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _dbEntities.users.Add(model);
                    _dbEntities.users.Update(model);
                    _dbEntities.SaveChanges();
                    return StatusCode(StatusCodes.Status201Created);
                }

                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Not updated try to give correct information");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<HttpResponseMessage> DeleteUser(int id)
        {
            User user = _dbEntities.users.Find(id);

            if (user != null)
            {
                _dbEntities.users.Remove(user);
                _dbEntities.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }

            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }

    }
}
