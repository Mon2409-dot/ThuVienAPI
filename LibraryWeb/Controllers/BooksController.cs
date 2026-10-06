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

        [HttpGet]
        public async Task<IActionResult> addBook()
        {
            var client = httpClientFactory.CreateClient("default");

            List<authorDTO> responseAu = new List<authorDTO>();
            var httpResponseAu = await client.GetAsync("https://localhost:7207/api/Authors/get-all-author");
            httpResponseAu.EnsureSuccessStatusCode();
            responseAu.AddRange(await httpResponseAu.Content.ReadFromJsonAsync<IEnumerable<authorDTO>>());
            ViewBag.listAuthor = responseAu;

            List<publisherDTO> responsePu = new List<publisherDTO>();
            var httpResponsePu = await client.GetAsync("https://localhost:7207/api/Publishers/get-all-publisher");
            httpResponsePu.EnsureSuccessStatusCode();
            responsePu.AddRange(await httpResponsePu.Content.ReadFromJsonAsync<IEnumerable<publisherDTO>>());
            ViewBag.listPublisher = responsePu;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addBook(addBookDTO addBookDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient("default");
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7207/api/Books/add-book"),
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(addBookDTO), System.Text.Encoding.UTF8,
                        System.Net.Mime.MediaTypeNames.Application.Json)
                };

                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Books");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> listBook(int id)
        {
            BookDTO response = new BookDTO();
            try
            {
                var client = httpClientFactory.CreateClient("default");
                var httpResponseMess = await client.GetAsync("https://localhost:7207/api/Books/get-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                response = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> editBook(int id)
        {
            var client = httpClientFactory.CreateClient("default");

            BookDTO responseBook = new BookDTO();
            var httpResponseMess = await client.GetAsync("https://localhost:7207/api/Books/get-book-by-id/" + id);
            httpResponseMess.EnsureSuccessStatusCode();
            responseBook = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            ViewBag.Book = responseBook;

            List<authorDTO> responseAu = new List<authorDTO>();
            var httpResponseAu = await client.GetAsync("https://localhost:7207/api/Authors/get-all-author");
            httpResponseAu.EnsureSuccessStatusCode();
            responseAu.AddRange(await httpResponseAu.Content.ReadFromJsonAsync<IEnumerable<authorDTO>>());
            ViewBag.listAuthor = responseAu;

            List<publisherDTO> responsePu = new List<publisherDTO>();
            var httpResponsePu = await client.GetAsync("https://localhost:7207/api/Publishers/get-all-publisher");
            httpResponsePu.EnsureSuccessStatusCode();
            responsePu.AddRange(await httpResponsePu.Content.ReadFromJsonAsync<IEnumerable<publisherDTO>>());
            ViewBag.listPublisher = responsePu;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> editBook([FromRoute] int id, editBookDTO bookDTO)
        {
            try
            {
                var client = httpClientFactory.CreateClient("default");
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:7207/api/Books/update-book-by-id/" + id),
                    Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(bookDTO), System.Text.Encoding.UTF8,
                        System.Net.Mime.MediaTypeNames.Application.Json)
                };

                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<addBookDTO>();
                if (response != null)
                {
                    return RedirectToAction("Index", "Books");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Books");
            }
            return RedirectToAction("Index", "Books");
        }

        public async Task<IActionResult> delBook([FromRoute] int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("default");
                var httpResponseMess = await client.DeleteAsync("https://localhost:7207/api/Books/delete-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "Books");
            }
        }
    }
}