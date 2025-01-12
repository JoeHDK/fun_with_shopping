﻿namespace WebshopApi.Models;
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public string SessionId { get; set; } = string.Empty;
    }