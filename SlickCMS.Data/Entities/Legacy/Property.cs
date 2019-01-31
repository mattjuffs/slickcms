using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SlickCMS
{
    public partial class Property : IData<Property>
    {
        public void Insert()
        {
            //set system generated items:
            this.DateCreated = DateTime.Now;
            this.DateModified = DateTime.Now;

            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Properties.InsertOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }

        public Property Select(int id)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            return dc.Properties.SingleOrDefault(p => p.PropertyID == id);
        }

        public List<Property> SelectHomepage()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from p in dc.Properties
                where p.Published > 0
                orderby p.PropertyID descending
                select p
            );

            return query.Take(5).ToList();

            //return SelectMultipleWithoutSearch(0, 5).Where(p => p.Published > 0).OrderByDescending(p => p.PropertyID).ToList();
            //return this.SelectMultiplePublished(0, 5).OrderByDescending(p => p.PropertyID).ToList();
        }

        public List<Property> SelectMultiplePublished(int skip, int take)
        {
            return SelectMultiple(skip, take).Where(p => p.Published > 0).ToList();
        }

        public List<Property> SelectMultipleWithoutSearch(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from p in dc.Properties
                select p
            );

            //apply sort by criteria
            PropertySearch ps = new PropertySearch().Get();

            if (Validation.IsNull(ps.SortBy, "").ToString() != "")
            {
                switch (ps.SortBy)
                {
                    case "PriceAsc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "BedroomsAsc":
                        query = query.OrderBy(p => p.Bedrooms);
                        break;
                    case "BedroomsDesc":
                        query = query.OrderByDescending(p => p.Bedrooms);
                        break;
                    default:
                        //not implemented, so stick with standard sorting:
                        query = query.OrderByDescending(p => p.DateModified);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.DateModified);
            }

            return query.Skip(skip).Take(take).ToList();
        }

        public List<Property> SelectMultiple(int skip, int take)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from p in dc.Properties
                select p
            );

            //apply search criteria
            PropertySearch ps = new PropertySearch().Get();

            if (Validation.IsNull(ps.SaleType, "").ToString() != "")
                query = query.Where(p => p.SaleType == ps.SaleType);

            if (Validation.IsNull(ps.PropertyType, "").ToString() != "")
                query = query.Where(p => p.PropertyType == ps.PropertyType);

            if (Validation.IsNull(ps.PriceFrom, "0").ToString() != "0")
                query = query.Where(p => p.Price >= ps.PriceFrom);

            if (Validation.IsNull(ps.PriceTo, "0").ToString() != "0")
                query = query.Where(p => p.Price <= ps.PriceTo);

            if (Validation.IsNull(ps.Bedrooms, "0").ToString() != "0")
                query = query.Where(p => p.Bedrooms >= ps.Bedrooms);

            if (Validation.IsNull(ps.Bathrooms, "0").ToString() != "0")
                query = query.Where(p => p.Bathrooms >= ps.Bathrooms);

            if (Validation.IsNull(ps.Furnishings, "").ToString() != "")
                query = query.Where(p => p.Furnishings == ps.Furnishings);

            if (Validation.IsNull(ps.SortBy, "").ToString() != "")
            {
                switch (ps.SortBy)
                {
                    case "PriceAsc":
                        query = query.OrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        query = query.OrderByDescending(p => p.Price);
                        break;
                    case "BedroomsAsc":
                        query = query.OrderBy(p => p.Bedrooms);
                        break;
                    case "BedroomsDesc":
                        query = query.OrderByDescending(p => p.Bedrooms);
                        break;
                    default:
                        //not implemented, so stick with standard sorting:
                        query = query.OrderByDescending(p => p.DateModified);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(p => p.DateModified);
            }

            return query.Skip(skip).Take(take).ToList();
        }

        /// <summary>
        /// Searches Properties using a specified search
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="search">Search query</param>
        /// <returns></returns>
        public List<Property> SelectMultiple(int skip, int take, string search)
        {
            search = search.ToLower(); //case insensitive searching

            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            var query = (
                from p in dc.Properties
                where
                    p.SaleType.Contains(search) ||
                    p.PropertyType.Contains(search) ||
                    p.Price.ToString().Contains(search) ||
                    //p.Bedrooms.ToString().Contains(search) ||
                    //p.Bathrooms.ToString().Contains(search) ||
                    p.Furnishings.Contains(search) ||
                    p.Address2.Contains(search) ||
                    p.Address3.Contains(search) ||
                    p.Address4.Contains(search) ||
                    p.FullDescription.Contains(search) ||
                    p.KeyFeatures.Contains(search) ||
                    p.Summary.Contains(search) ||
                    p.PostCode.Contains(search)
                select p
            );

            query = query.OrderByDescending(p => p.DateModified);

            return query.Skip(skip).Take(take).ToList();
        }

        public static List<string> SelectPropertyTypes()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = (from p in dc.Properties orderby p.PropertyType ascending select p.PropertyType.ToString()).Distinct();
            return query.ToList();            
        }

        public static List<string> SelectParking()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = (from p in dc.Properties orderby p.Parking ascending select p.Parking.ToString()).Distinct();
            return query.ToList();
        }

        public static List<string> SelectFurnishings()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = (from p in dc.Properties orderby p.Furnishings ascending select p.Furnishings.ToString()).Distinct();
            return query.ToList();
        }

        /*public static List<string> SelectGarages()
        {
            List<string> garages = new List<string>();
            garages.Add("No Garage");
            garages.Add("Single");
            garages.Add("Double");
            return garages;
        }*/

        public static Dictionary<string, int> SelectGarages()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            d.Add("No Garage", 0);
            d.Add("Single", 1);
            d.Add("Double", 2);
            return d;
        }

        public static Dictionary<string, int> SelectStatus(bool forSale)
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            
            if (forSale)
            {
                d.Add("Invisible", 0);
                d.Add("Visible", 1);
                d.Add("New For Sale", 2);
                d.Add("Sold (STC)", 3);
            }
            else
            {
                d.Add("Invisible", 0);
                d.Add("Visible", 1);
                d.Add("New to Let", 2);
                d.Add("Must View", 3);
                d.Add("Let Agreed", 4);
            }

            //more options may need adding for ForSale Properties

            return d;
        }

        public static string SelectPropertyAddress(int propertyID)
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = (
                from p in dc.Properties
                where p.PropertyID == propertyID
                select
                    (p.Address1 != "" ? p.Address1 + " " : "")
                    + (p.Address2 != "" ? p.Address2 + " " : "")
                    + (p.Address3 != "" ? p.Address3 + " " : "")
                    + (p.Address4 != "" ? p.Address4 + " " : "")
                    + p.PostCode
            );

            return query.FirstOrDefault();
        }

        public void Update()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();

            Property property = dc.Properties.Single(p => p.PropertyID == this.PropertyID);

            property.Reference = this.Reference;
            property.SaleType = this.SaleType;
            property.PropertyType = this.PropertyType;
            property.Address1 = this.Address1;
            property.Address2 = this.Address2;
            property.Address3 = this.Address3;
            property.Address4 = this.Address4;
            property.PostCode = this.PostCode;
            property.Price = this.Price;
            property.Parking = this.Parking;
            property.Garages = this.Garages;
            property.Receptions = this.Receptions;
            property.Bedrooms = this.Bedrooms;
            property.Bathrooms = this.Bathrooms;
            property.Garden = this.Garden;
            property.Pets = this.Pets;
            property.Conservatory = this.Conservatory;
            property.Furnishings = this.Furnishings;
            property.Summary = this.Summary;
            property.FullDescription = this.FullDescription;
            property.KeyFeatures = this.KeyFeatures;
            property.RightMoveURL = this.RightMoveURL;
            property.DateModified = DateTime.Now;
            property.Published = this.Published;
            property.Tagline = this.Tagline;

            dc.SubmitChanges();
            dc.Dispose();
        }

        public void Delete()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            dc.Properties.DeleteOnSubmit(this);
            dc.SubmitChanges();
            dc.Dispose();
        }
    }
}
