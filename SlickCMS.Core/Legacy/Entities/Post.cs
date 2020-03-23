using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Post : IData<Post>
    {
        public string KeywordSearch { get; set; }

        #region InsertMethods
        /// <summary>
        /// Creates a single Post
        /// </summary>
        public void Insert()
        {
            //set system generated items:
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Posts.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        /*public static void InsertMultiple(List<Post> posts)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Posts.InsertAllOnSubmit(posts);
            dc.SubmitChanges();
        }*/
        #endregion

        #region SelectMethods
        /// <summary>
        /// Retrieves one Post by ID
        /// </summary>
        /// <param name="PostID">ID of the Post to Get</param>
        /// <returns>A single Post, if it exists</returns>
        public Post Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Posts.SingleOrDefault(p => p.PostID == id);

            //use for debugging raw SQL query:
            //var query = dc.Posts.SingleOrDefault(p => p.PostID == PostID);
            //System.Diagnostics.Debug.WriteLine(query);
            //return query;
        }

        public Post Select(string url)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = (
                from p in dc.Posts
                where
                    p.URL == url
                    && !p.Deleted
                    && p.Archive == null// excluding archived posts, as they should now be retrieved using Select() passing in archive
                select p
            );
            Post post = query.FirstOrDefault();
            //Post post = dc.Posts.SingleOrDefault(p => p.URL == url);

            if (post == null)
            {
                //show 404 page
                // TODO: 404 is no longer stored within database - specify it as a static post object defined within this class?
                query = (from p in dc.Posts where p.URL == "404" select p);
                post = query.FirstOrDefault();
                //post = dc.Posts.SingleOrDefault(p => p.URL == "404");
            }

            return post;
        }

        /*public Post Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            //single parameter
            //return dc.ExecuteQuery<Post>("Select * From dbo.Posts Where PostID = {0}", id).FirstOrDefault();

            //multiple paramters
            return dc.ExecuteQuery<Post>("Select * From dbo.Posts Where PostID = {0} And PostTitle = {1}", id, "Hello World").FirstOrDefault();
        }*/

        /*public List<Post> SelectMultiple()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.ExecuteQuery<Post>("Select * From dbo.Posts").ToList();
        }*/

        /*public List<Post> SelectMultiple()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            //var query = from p in dc.Posts select p;
            //return query.ToList();
            //return dc.Posts.Take(10).ToList();
            return dc.Posts.ToList();
        }*/

        /// <summary>
        /// Retrieves one Post by Title
        /// </summary>
        /// <param name="Title">Title of the Post</param>
        /// <returns>A single Post, if it exists</returns>
        /*public Post Select(string Title)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Posts.SingleOrDefault(p => p.Title == Title);
        }*/

        /// <summary>
        /// Gets a subset of the Pageable Posts
        /// </summary>
        /// <returns>A selection of Posts</returns>
        public List<Post> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = from p in dc.Posts where !p.Deleted select p;

            //if (Validation.IsNull(this.KeywordSearch, "").ToString() != "")
            if (!string.IsNullOrEmpty(this.KeywordSearch))
            {
                //split keywordsearch
                string[] keywords = this.KeywordSearch.ToArray(' ');

                //implementing custom predicate builders for keyword(s) searches
                //http://stackoverflow.com/questions/597873/linq-multiple-where-clause

                //the following can be expanded to include more fields to search on
                Func<string, Func<Post, bool>> buildKeywordPredicate =
                keyword =>
                    x => x.Title.ToLower().Contains(keyword)
                        || x.Content.ToLower().Contains(keyword)
                        || x.Summary.ToLower().Contains(keyword);//note: ToLower is used as Contains is case sensitive

                Func<Func<Post, bool>, Func<Post, bool>, Func<Post, bool>> buildOrPredicate =
                    (pred1, pred2) =>
                        x => pred1(x) || pred2(x);

                // build the where clause
                Func<Post, bool> filter = null;

                foreach (string keyword in keywords)
                {
                    //TODO: add app setting to determine whether or not keyword searches are case sensitive
                    string word = keyword.ToLower();

                    filter = filter == null
                    ? buildKeywordPredicate(word)
                    : buildOrPredicate(filter, buildKeywordPredicate(word));
                }

                //return query.Where(filter).OrderByDescending(p => p.DateModified).ToList();
                return query.Where(filter).OrderBy(p => p.Rank).ToList();
            }
            else
            {
                //implement OrderBy after Where
                //query = query.OrderByDescending(p => p.DateModified);
                query = query.OrderBy(p => p.Rank);

                return query.Skip(skip).Take(take).ToList();
            }
        }

        /// <summary>
        /// Selects Posts that are pageable (i.e. blog posts, not pages)
        /// </summary>
        /// <param name="skip">Number of Posts to skip</param>
        /// <param name="take">Number of Posts to return</param>
        /// <returns>List of Posts</returns>
        public List<Post> SelectPageable(int skip, int take)
        {
            List<Post> posts = SelectMultiple(skip, take);
            return posts.Where(p => p.Pageable == 1 && !p.Deleted).ToList();
        }

        /// <summary>
        /// Gets the ID of the latest Post, useful for carrying out further operations
        /// </summary>
        /// <returns>ID of the latest Post</returns>
        /*public int SelectPostID()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from p in dc.Posts orderby p.PostID descending select p.PostID).FirstOrDefault();
        }*/

        /// <summary>
        /// Retrieves a list of Post titles
        /// </summary>
        /// <returns>List of PostTitle</returns>
        public List<PostTitle> SelectPostTitles()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from p in dc.Posts where !p.Deleted select new PostTitle { Title = p.Title }).Distinct().OrderBy(p => p.Title).ToList();
        }
        #endregion

        #region UpdateMethods
        /// <summary>
        /// Updates an existing Post
        /// </summary>
        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            //Post postToUpdate = GetPost(this.PostID);
            Post post = dc.Posts.Single(p => p.PostID == this.PostID);

            post.UserID = this.UserID;
            post.Title = this.Title;
            post.URL = this.URL;
            post.Summary = this.Summary;
            post.Content = this.Content;
            post.Search = this.Search;
            post.DateModified = DateTime.Now;
            post.Published = this.Published;
            post.Pageable = this.Pageable;
            post.ParentPage = this.ParentPage;
            post.Rank = this.Rank;
            post.Deleted = this.Deleted;
            post.Archive = this.Archive;

            dc.SubmitChanges();
            dc.Dispose();
        }
        #endregion

        #region DeleteMethods
        /// <summary>
        /// Deletes a Post and all associated data
        /// </summary>
        public void Delete()
        {
            /*SlickCMSDataContext dc = SlickCMSDataContext.Create();

            //dc.ExecuteCommand("Delete From dbo.Posts Where PostID = {0}", this.PostID);
            //errors with: Cannot remove an entity that has not been attached.
            //dc.Posts.DeleteOnSubmit(this);

            //var obj = dc.Posts.Where(p => p.PostID == this.PostID).FirstOrDefault();

            dc.SubmitChanges();
            dc.Dispose();*/

            // no longer deleting record from database, to avoid accidental deletions
            this.Deleted = true;
            this.Update();
        }

        public void Restore()
        {
            this.Deleted = false;
            this.Update();
        }

        /// <summary>
        /// Deletes a Post using its ID
        /// </summary>
        /// <param name="PostID"></param>
        public static void Delete(int id)
        {
            /*SlickCMSDataContext dc = SlickCMSDataContext.Create();
            Post post = dc.Posts.Where(p => p.PostID == id).FirstOrDefault();
            dc.Posts.DeleteOnSubmit(post);
            dc.SubmitChanges();
            dc.Dispose();*/

            Post p = new Post().Select(id);
            p.Deleted = true;
            p.Update();
        }

        /*public static int DeleteUnPublishedPosts()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            int removed = 0;

            var query = (
                from p in dc.Posts
                where p.Published == 0
                select p
            );

            removed = query.Count();

            foreach (var post in query)
            {
                dc.Posts.DeleteOnSubmit(post);
            }

            //or could do:
            //dc.Posts.DeleteAllOnSubmit(query);

            dc.SubmitChanges();
            dc.Dispose();

            return removed;
        }*/
        #endregion

        /// <summary>
        /// Determines if the Post is valid, depending on Validation errors
        /// </summary>
        /// <returns>True if Post passes validation</returns>
        public bool IsValid
        {
            get
            {
                return (Errors().Count() == 0);
            }
        }

        /// <summary>
        /// Carries out validation of the Post object and for any failure, creates a new Error
        /// </summary>
        /// <returns>Error(s) if validation fails</returns>
        public IEnumerable<Error> Errors()
        {
            //see: http://nerddinnerbook.s3.amazonaws.com/Part3.htm

            //define all business rules

            //IsNull
            Title = Convert.ToString(Validation.IsNull(Title, ""));

            //nulls

            //if Title is null, then IsNull above will set it to "", which still yields a new Error below:
            if (String.IsNullOrEmpty(Title))
                yield return new Error("Title required", "Title");

            if (PostID < 0)
                yield return new Error("PostID is invalid", "PostID");

            //maxlength validation
            Title = Validation.MaxLength(Title, 255);

            //datatype validation/manipulation
            //port from Clean()

            yield break;
        }

        /// <summary>
        /// Formats a Url to the full path
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetUrl(string url)
        {
            if (url == "home")
                return "/";
            else
                return String.Format("/pages/{0}.aspx", url);
        }

        public static string GetUrl(string rootUrl, string subUrl)
        {
            return String.Format("/pages/{0}/{1}.aspx", rootUrl, subUrl);
        }

        public static string GetArchiveUrl(string archive, string url)
        {
            return String.Format("/archive/{0}/{1}.aspx", archive, url);
        }

        /// <summary>
        /// Sub class for subset of Post Title data
        /// </summary>
        public class PostTitle
        {
            public string Title { get; set; }
        }

        /// <summary>
        /// Sub class for subset of Post Title/URL data - for navigation usage
        /// </summary>
        public class PostSummary
        {
            public int PostID { get; set; }
            public string Title { get; set; }
            public string URL { get; set; }
            public int? ParentPage { get; set; }
            public string Archive { get; set; }
            public bool Published { get; set; }

            public static List<PostSummary> SelectNavigation()
            {
                string[] excludedPages = { "terms-and-conditions", "child-protection-policy" }; // titles to exclude from navigation

                SlickCMSDataContext dc = SlickCMSDataContext.Create();

                var query = (
                    from p in dc.Posts
                    where
                        1==1
                        //&& p.Published == 1
                        && p.Pageable == 0
                        && !excludedPages.Contains(p.URL)
                        && p.ParentPage == 0// exclude sub-pages and nulls
                        && !p.Deleted
                        //&& p.Archive == null
                    //orderby p.Title ascending// TODO: implement ordering
                    orderby p.Rank ?? 0 ascending
                    select new PostSummary
                    {
                        PostID = p.PostID,
                        Title = p.Title,
                        URL = p.URL,
                        ParentPage = p.ParentPage,
                        Archive = p.Archive,
                        Published = (p.Published == 1),
                    }
                );

                return query.ToList();
            }

            /// <summary>
            /// Retrieves all sub pages for a root page
            /// TODO: cache the results in memory
            /// </summary>
            /// <param name="parentPage"></param>
            /// <returns></returns>
            public List<PostSummary> SelectSubPages()
            {
                return SelectSubPages(this.PostID);
            }

            public static List<PostSummary> SelectSubPages(int parentPage)
            {
                SlickCMSDataContext dc = SlickCMSDataContext.Create();

                var query = (
                    from p in dc.Posts
                    where
                        1==1
                        //&& p.Published == 1
                        && p.ParentPage == parentPage
                        && p.Pageable == 0
                        && !p.Deleted
                        //&& p.Archive == null
                    orderby p.Rank ?? 0 ascending
                    select new PostSummary
                    {
                        Title = p.Title,
                        URL = p.URL,
                        PostID = p.PostID,
                        Published = (p.Published == 1),
                        Archive = p.Archive,
                    }
                );

                return query.ToList();
            }

            public static List<PostSummary> GetArchived(int postID)
            {
                using (SlickCMSDataContext db = SlickCMSDataContext.Create())
                {
                    var query = (
                        from p in db.Posts
                        where
                            p.Archive != null
                            && p.Published == 1
                            && p.Pageable == 0
                            && !p.Deleted
                            && p.ParentPage == postID
                        orderby p.Rank
                        select new PostSummary
                        {
                            PostID = p.PostID,
                            Title = p.Title,
                            URL = p.URL,
                            ParentPage = p.ParentPage,
                            Archive = p.Archive,
                        }
                    );

                    return query.ToList();
                }
            }
        }

        public static string SelectPostTitle(int postID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (from p in db.Posts where p.PostID == postID select p.Title);
                return query.FirstOrDefault();
            }
        }

        /*public static List<Post> Search(string SearchQuery)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (

                    );

                return query.ToList();
            }
        }*/

        public static List<Post> GetDeleted()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from p in db.Posts
                    where p.Deleted == true
                    select p
                );

                return query.ToList();
            }
        }

        public void DeletePermanently()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = (
                    from p in db.Posts
                    where p.PostID == this.PostID
                    select p
                ).FirstOrDefault();

                if (obj == null) return;

                db.Posts.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }
        }

        public static int CountBlogPosts()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (from p in db.Posts where p.Pageable == 1 && p.Published == 1 select p);
                return query.Count();
            }
        }

        public static string GetBlogUrl(string url)
        {
            return String.Format("/blog/{0}.aspx", url);
        }

        public static List<Post> GetBlogPosts(int skip, int take)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from p in db.Posts
                    orderby p.DateCreated descending
                    where
                        p.Pageable == 1
                        && p.Published == 1
                    select p
                );

                return query.Skip(skip).Take(take).ToList();
            }
        }

        public Post Select(string archive, string url)
        {
            using (SlickCMSDataContext dc = SlickCMSDataContext.Create())
            {
                var query = (
                    from p in dc.Posts
                    where
                        p.URL == url
                        && !p.Deleted
                        && p.Archive == archive
                    select p
                );

                Post post = query.FirstOrDefault();

                if (post == null)
                {
                    // TODO: show 404 page
                    query = (from p in dc.Posts where p.URL == "404" select p);
                    post = query.FirstOrDefault();
                }

                return post;
            }
        }
    }
}
