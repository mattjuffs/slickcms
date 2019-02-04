using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlickCMS.Data;
using SlickCMS.Data.Entities;
using SlickCMS.Data.Services;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;
        
        public PostsController(SlickCMSContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        // GET: Posts
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }*/

        public IActionResult Index()
        {
            //var postService = new SlickCMS.Data.Services.PostService(_context);
            //return View(postService.GetPublished());
            return View(_postService.GetPublished());
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
