using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlickCMS
{
    public partial class Album// : IData<Album>
    {
        public Image MainImage { get; set; }

        public class Summary : Album
        {
            public string PostTitle { get; set; }
        }

        public static List<Album.Summary> GetAll()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from a in db.Albums
                    join p in db.Posts on a.PostID equals p.PostID
                    orderby a.Name ascending
                    select new Album.Summary
                    {
                        AlbumID = a.AlbumID,
                        PostID = a.PostID,
                        DateCreated = a.DateCreated,
                        Name = a.Name,
                        Description = a.Description,
                        Rank = a.Rank,
                        PostTitle = p.Title,
                    }
                );

                return query.ToList();
            }
        }

        public void Add()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                db.Albums.InsertOnSubmit(this);
                db.SubmitChanges();
            }
        }

        public static void Delete(int albumID, string path)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = (from a in db.Albums where a.AlbumID == albumID select a).FirstOrDefault();
                db.Albums.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }

            List<Image> images = Image.GetAll(albumID);
            foreach (Image image in images)
            {
                image.Delete(path);
            }
        }

        public static Album Get(int albumID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                return db.Albums.Where(p => p.AlbumID == albumID).FirstOrDefault();
            }
        }

        public static void RankUp(int albumID)
        {
            Album album = Get(albumID);
            album.Rank++;
            album.Update();
        }

        public static void RankDown(int albumID)
        {
            Album album = Get(albumID);
            album.Rank--;
            album.Update();
        }

        public void Update()
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = (from a in db.Albums where a.AlbumID == this.AlbumID select a).FirstOrDefault();

                obj.PostID = this.PostID;
                obj.Name = this.Name;
                obj.Description = this.Description;
                obj.Rank = this.Rank;

                db.SubmitChanges();
            }
        }

        public static List<Album> GetAll(int postID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from a in db.Albums
                    where a.PostID == postID
                    orderby a.Rank ascending
                    select a
                );

                List<Album> albums = query.ToList();

                if (albums.Count == 0)
                    return albums;

                foreach (Album album in albums)
                {
                    var imageQuery = (
                        from i in db.Images
                        where i.AlbumID == album.AlbumID
                        orderby i.Main descending
                        select i
                    );

                    album.MainImage = imageQuery.FirstOrDefault();
                }

                return albums;
            }
        }
    }
}
