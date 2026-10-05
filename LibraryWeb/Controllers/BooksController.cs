using Microsoft.AspNetCore.Mvc;
using LibraryWeb.Models.DTO;

namespace LibraryWeb.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                var client = httpClientFactory.CreateClient("default");
                var httpResponseMess = await client.GetAsync("https://localhost:7207/api/Books/get-all-books");
                httpResponseMess.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<BookDTO>>());
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }
            return View(response);
        }
    }
}