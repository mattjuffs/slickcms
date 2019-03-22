using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SlickCMS.Data;
using SlickCMS.Data.Interfaces;
using System.Web;

namespace SlickCMS.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;
        
        public PostsController(IConfiguration config, SlickCMSContext context, IPostService postService) : base(context)
        {
            _config = config;
            _context = context;
            _postService = postService;
        }

        // GET: Posts
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }*/

        public IActionResult Index(int page = 1, bool viewall = false)
        {
            int take = _config.GetValue<int>("SlickCMS:PostsPerPage", 10);
            if (viewall) { take = _config.GetValue<int>("SlickCMS:ViewAllCount", 1000); }
            int totalPosts = _postService.TotalPosts();
            int remainder = (totalPosts % take);
            int totalPages = (totalPosts - remainder) / take;

            var posts = _postService.GetPublished(page, take);

            var postsModel = new Models.PostsModel
            {
                Name = "Posts",
                Posts = posts,
                Pagination = new Models.PaginationModel
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalPosts = totalPosts,
                    Query = ""
                },
                Type = Models.PostsModel.PageType.Post
            };

            return View(postsModel);
        }

        public IActionResult Search(string query = "", int page = 1, bool viewall = false)
        {
            query = HttpUtility.HtmlEncode(query);            

            int take = _config.GetValue<int>("SlickCMS:PostsPerPage", 10);
            if (viewall) { take = _config.GetValue<int>("SlickCMS:ViewAllCount", 1000); }
            int totalSearchResults = _postService.TotalSearchResults(query);
            int remainder = (totalSearchResults % take);
            int totalPages = (totalSearchResults - remainder) / take;

            var searchResults = _postService.Search(query, page, take);

            var postsModel = new Models.PostsModel
            {
                Name = "Search",
                Posts = searchResults,
                Pagination = new Models.PaginationModel
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalPosts = totalSearchResults,
                    Query = query
                },
                Type = Models.PostsModel.PageType.Search
            };

            return View("Search", postsModel);
        }

        public IActionResult Category(string name, int page = 1, bool viewall = false)
        {
            int take = _config.GetValue<int>("SlickCMS:PostsPerPage", 10);
            if (viewall) { take = _config.GetValue<int>("SlickCMS:ViewAllCount", 1000); }
            int totalPosts = _postService.TotalCategoryPosts(name);
            int remainder = (totalPosts % take);
            int totalPages = (totalPosts - remainder) / take;

            var posts = _postService.Category(name, page, take);

            var postsModel = new Models.PostsModel
            {
                Name = name,
                Posts = posts,
                Pagination = new Models.PaginationModel
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalPosts = totalPosts,
                    Query = ""
                },
                Type = Models.PostsModel.PageType.Category
            };

            return View("Category", postsModel);
        }

        public IActionResult Tag(string name, int page = 1, bool viewall = false)
        {
            int take = _config.GetValue<int>("SlickCMS:PostsPerPage", 10);
            if (viewall) { take = _config.GetValue<int>("SlickCMS:ViewAllCount", 1000); }
            int totalPosts = _postService.TotalTagPosts(name);
            int remainder = (totalPosts % take);
            int totalPages = (totalPosts - remainder) / take;

            var posts = _postService.Tag(name, page, take);

            var postsModel = new Models.PostsModel
            {
                Name = name,
                Posts = posts,
                Pagination = new Models.PaginationModel
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalPosts = totalPosts,
                    Query = ""
                },
                Type = Models.PostsModel.PageType.Tag
            };

            return View("Tag", postsModel);
        }

        // TODO: all of the below need amending to use the PostService, rather than the Post entity itself

        // GET: Posts/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }*/

        // GET: Posts/Create
        /*public IActionResult Create()
        {
            return View();
        }*/

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,UserId,Title,Url,Summary,Content,Search,DateCreated,DateModified,Published,Pageable")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }*/

        // GET: Posts/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }*/

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,UserId,Title,Url,Summary,Content,Search,DateCreated,DateModified,Published,Pageable")] Post post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }*/

        // GET: Posts/Delete/5
        /*public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }*/

        // POST: Posts/Delete/5
        /*[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        /*private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.PostId == id);
        }*/
    }
}
