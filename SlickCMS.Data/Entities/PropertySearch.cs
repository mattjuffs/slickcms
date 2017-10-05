using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SlickCMS
{
    public class PropertySearch
    {
        public string SaleType { get; set; }
        public string PropertyType { get; set; }
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public string Furnishings { get; set; }
        public string SortBy { get; set; }

        public PropertySearch Get()
        {
            if (HttpContext.Current.Session["PropertySearch"] != null)
            {
                return (PropertySearch)HttpContext.Current.Session["PropertySearch"];
            }
            else
            {
                return new PropertySearch();
            }
        }

        public void Set()
        {
            HttpContext.Current.Session["PropertySearch"] = this;
        }

        public static List<string> GetPropertyTypes()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = from p in dc.Properties orderby p.PropertyType ascending select p.PropertyType;
            //return query.Distinct().ToList();

            List<string> propertyTypes = new List<string>();
            propertyTypes.Add(""); //add default
            propertyTypes.AddRange(query.Distinct());

            return propertyTypes;
        }

        public static List<string> GetFurnishings()
        {
            SlickCMSDataContext dc = SlickCMSDataContext.Create();
            var query = from p in dc.Properties orderby p.Furnishings ascending select p.Furnishings;
            //return query.Distinct().ToList();

            List<string> furnishings = new List<string>();
            furnishings.Add("");
            furnishings.AddRange(query.Distinct());

            return furnishings;
        }

        public static List<string> GetPrices(bool forSale)
        {
            List<string> prices = new List<string>();

            if (forSale)
            {
                prices.Add("0");
                prices.Add("50000");
                prices.Add("60000");
                prices.Add("70000");
                prices.Add("80000");
                prices.Add("90000");
                prices.Add("100000");
                prices.Add("110000");
                prices.Add("120000");
                prices.Add("130000");
                prices.Add("140000");
                prices.Add("150000");
                prices.Add("160000");
                prices.Add("170000");
                prices.Add("180000");
                prices.Add("190000");
                prices.Add("200000");
                prices.Add("250000");
                prices.Add("300000");
                prices.Add("350000");
                prices.Add("400000");
                prices.Add("450000");
                prices.Add("500000");
                prices.Add("600000");
                prices.Add("700000");
                prices.Add("800000");
                prices.Add("900000");
                prices.Add("1000000");

            }
            else
            {
                //to let prices
                prices.Add("0");
                prices.Add("100");
                prices.Add("200");
                prices.Add("300");
                prices.Add("400");
                prices.Add("500");
                prices.Add("600");
                prices.Add("700");
                prices.Add("800");
                prices.Add("900");
                prices.Add("1000");
                prices.Add("1250");
                prices.Add("1500");
                prices.Add("1750");
                prices.Add("2000");
                prices.Add("3000");
                prices.Add("4000");
                prices.Add("5000");
            }

            return prices;
        }
    }
}
