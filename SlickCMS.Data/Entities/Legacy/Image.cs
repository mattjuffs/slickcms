using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SlickCMS
{
    public partial class Image : IData<Image>
    {
        /// <summary>
        /// Used for determining the position of the Image
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Includes Name + Extension as one string
        /// </summary>
        public string FullName { get; set; }

        public void Insert()
        {
            this.Uploaded = DateTime.Now;

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Images.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        public Image Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Images.SingleOrDefault(i => i.ImageID == id);
        }

        public List<Image> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from i in dc.Images select i).ToList();
        }

        public List<Image> SelectMultiple(int propertyID)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return (from i in dc.Images where i.PropertyID == propertyID orderby i.Main descending, i.Rank descending select i).ToList();
        }

        public static string SelectMain(int propertyID, int width, int height)
        {
            string url = "";
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            Image image = (from i in dc.Images where i.PropertyID == propertyID && i.Main select i).FirstOrDefault();

            if (image == null)
            {
                url = "/Image.aspx?img=image_coming_soon.jpg&w=" + width.ToString() + "&h=" + height.ToString();
            }
            else
            {
                url = "/Image.aspx?img=" + image.Name + ".jpg&w=" + width.ToString() + "&h=" + height.ToString();
            }

            return url;
        }

        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            Image image = dc.Images.Single(i => i.ImageID == this.ImageID);

            image.Alt = this.Alt;
            image.Title = this.Title;
            image.Caption = this.Caption;
            image.Main = this.Main;
            image.Rank = this.Rank;
            image.IsBanner = this.IsBanner;
            image.PostID = this.PostID;

            dc.SubmitChanges();
            dc.Dispose();
        }

        public void Delete()
        {
            Delete("");
        }

        public void Delete(string path)
        {
            /*SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.ExecuteCommand("Delete From Images Where ImageID = {0}", this.ImageID);
            dc.Dispose();*/

            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var obj = (from i in db.Images where i.ImageID == this.ImageID select i).FirstOrDefault();
                db.Images.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }

            if (path.Length > 0)
            {
                string file = path + "[folder]\\" + this.Name + this.Extension;

                string thumbImage = file.Replace("[folder]", "thumb");
                string largeImage = file.Replace("[folder]", "large");
                string originalImage = file.Replace("[folder]", "original");

                if (File.Exists(thumbImage)) File.Delete(thumbImage);
                if (File.Exists(largeImage)) File.Delete(largeImage);
                if (File.Exists(originalImage)) File.Delete(originalImage);
            }
        }

        public void SetMain(int imageID, int propertyID)
        {
            List<Image> images = SelectMultiple(propertyID);

            foreach (Image i in images)
            {
                i.Main = false;
                i.Update();
            }

            Image image = Select(imageID);
            image.Main = true;
            image.Update();
        }

        public void SetAlbumMain(int imageID, int albumID)
        {
            List<Image> images = GetAll(albumID);

            foreach (Image i in images)
            {
                i.Main = false;
                i.Update();
            }

            Image image = Select(imageID);
            image.Main = true;
            image.Update();
        }

        public void RankUp()
        {
            this.Rank--;
            this.Update();
        }

        public void RankDown()
        {
            this.Rank++;
            this.Update();
        }

        /// <summary>
        /// Gets all images
        /// </summary>
        /// <param name="postID">Set to ID of the Post, to retrieve all of its images</param>
        /// <returns></returns>
        public static List<Image> GetAll(int albumID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from i in db.Images
                    where i.AlbumID == albumID
                    select i
                );

                /*if (albumID > 0)
                {
                    query = query.Where(p => p.AlbumID == albumID);
                }*/

                if (albumID == 0)
                    query = query.OrderBy(i => i.Title);
                else
                    query = query.OrderBy(i => i.Rank);

                return query.ToList();
            }
        }

        /// <summary>
        /// Retrieves all Banners
        /// </summary>
        /// <param name="postID">ID of the Post to restrict to (set to 0 for all Banners)</param>
        /// <returns></returns>
        public static List<Image> GetBanners(int postID)
        {
            using (SlickCMSDataContext db = SlickCMSDataContext.Create())
            {
                var query = (
                    from i in db.Images
                    where i.PostID == postID
                    select i
                );

                if (postID > 0)
                    query = query.Where(i => i.IsBanner);

                return query.ToList();
            }
        }

        /// <summary>
        /// Creates a random GUID based filename
        /// </summary>
        /// <param name="originalName"></param>
        /// <returns></returns>
        public static string GetFileName(string originalName)
        {
            string extension = Path.GetExtension(originalName);
            string guid = System.Guid.NewGuid().ToString();

            return guid + extension;
        }

        /// <summary>
        /// Simple logic to determine if an image is portrait
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static bool IsPortrait(int width, int height)
        {
            if (height > width)
                return true;
            else
                return false;
        }

        public static System.Drawing.Image ResizeImage(string path, string image, decimal width, decimal height)
        {
            // check the image exists before attempting to resize it
            if (!File.Exists(path + image)) return null;

            // refactor by passing in the path and required width/height to an Image method

            System.Drawing.Image fullSizeImage = System.Drawing.Image.FromFile(path + image);

            decimal ratio;

            // adjust the height/width according to the orientation
            if (IsPortrait(fullSizeImage.Width, fullSizeImage.Height))
            {
                ratio = Convert.ToDecimal(fullSizeImage.Width) / Convert.ToDecimal(fullSizeImage.Height);

                // portrait: image height to stay as stated height, width to be adjusted

                //height = 600;
                width = height * ratio;
            }
            else
            {
                ratio = Convert.ToDecimal(fullSizeImage.Height) / Convert.ToDecimal(fullSizeImage.Width);

                // landscape: image width to stay as stated width, height to be adjusted

                //width = 800;
                height = width * ratio;
            }

            // check we're not trying to enlarge the image
            if (fullSizeImage.Width <= width || fullSizeImage.Height <= height)
            {
                width = fullSizeImage.Width;
                height = fullSizeImage.Height;
            }

            System.Drawing.Image.GetThumbnailImageAbort callback = new System.Drawing.Image.GetThumbnailImageAbort(ImageCallback);

            // fix to prevent .NET using the embedded thumbnail
            fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            fullSizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            // create the image
            System.Drawing.Image largeImage = fullSizeImage.GetThumbnailImage((int)width, (int)height, callback, IntPtr.Zero);

            // return the image - so that it can either be rendered on the fly, or saved to disk
            return largeImage;
        }

        private static bool ImageCallback()
        {
            return false;
        }
    }
}
