using Microsoft.AspNetCore.Mvc;
using moduit_api.Helpers;
using moduit_api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace moduit_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private string _BASEURL;

        public QuestionController()
        {
            _BASEURL = "https://screening.moduit.id";
        }

        [HttpGet]
        [Route("One")]
        public async Task<IActionResult> One()
        {
            string URL = _BASEURL + "/backend/question/one";
            Book obj = new Book();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
            }
            return Ok(obj);
        }

        [HttpGet]
        [Route("Two")]
        public async Task<IActionResult> Two([FromQuery] BookQuery param)
        {
            string URL = _BASEURL + "/backend/question/two";
            param.title = "Ergonomic";
            List<BookTags> obj = new List<BookTags>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(URL))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<List<BookTags>>(apiResponse);
                }
            }

            if(obj.Count() > 0)
            {
                if (!string.IsNullOrEmpty(param.title))
                    obj = obj.Where(i => i.title.ToLower().Contains(param.title.Trim().ToLower())).ToList();

                if (!string.IsNullOrEmpty(param.description))
                    obj = obj.Where(i => i.description.ToLower().Contains(param.description.Trim().ToLower())).ToList();

                if (!string.IsNullOrEmpty(param.tags))
                    obj = obj.Where(i => i.tags.Any(j => j.Equals(param.tags))).ToList();


                var orderByExpression = Helper.GetOrderByExpression<BookTags>("id");
                obj = Helper.OrderByDir<BookTags>(obj, "desc", orderByExpression).AsEnumerable().ToList();

                obj = Helper.Page<BookTags>(obj, 3, 1).ToList();
            }
            return Ok(obj);
        }

        [HttpPost]
        [Route("Three")]
        public async Task<IActionResult> Three([FromBody] InputBook item)
        {
            List<Book> lstBook = new List<Book>();
            if(item.items.Count() > 0)
            {
                foreach(InputBookDetail id in item.items)
                {
                    lstBook.Add(new Book(item.id, item.category, id.title, id.description, id.footer, item.createdAt));
                }
            }
            return Ok(lstBook);
        }
    }
}
