using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        protected ApplicationDbContext Context { get; private set; }
        public HomeController(ApplicationDbContext dbcontext)
        {
            Context = dbcontext;
        }
        public IActionResult Index()
        {
            return View(new Url());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Url url, CancellationToken token = default)
        {
            var shorturl = await Context.ShortenedUrls.Where(
                u => u.Short == url.ShortUrl).SingleOrDefaultAsync(token);
            return View("Results", new Web.Models.SearchSet()
            { RawUrls = new[] { shorturl.Raw }, ShortUrl = shorturl.Short });
        }
        public IActionResult Results(SearchSet set)
        {
            return View(set);
        }
        public IActionResult AddUrlSet()
        {
            return View(new AddUrlSet());
        }
        [HttpPost]
        public async Task<IActionResult> AddUrlSet(AddUrlSet set, CancellationToken token = default)
        {
            Guid? id = (await Context.UrlSets.Where(r => r.Key == set.Key).SingleOrDefaultAsync(token))?.Id;
            if (id == null)
            {
                id = Guid.NewGuid();
                await Context.AddAsync(new UrlSet() { Id = id.Value, Key = set.Key }, token);

            }

            return View("Index");
        }
        public IActionResult AddUrl()
        {
            return View(new AddUrl());
        }
        //public async Task<IActionResult> AddUrl(AddUrl url)
        //{
        //    await Context.AddAsync(new ShortenedUrl() { Id = Guid.NewGuid(), Raw = url.RawUrl, Short = url.ShortUrl);
        //}
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
        
    }
}
