﻿namespace ShoppingApplication24.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime AddedOn { get; set; }
        public virtual User Seller { get; set; }
        public int SellerId { get; set; }
        public virtual ProductStatus ProductStatus { get; set; }
        public int ProductStatusId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        
    }
}
