using System;

namespace LiveTogether.Models
{
    public class Warranty
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public string Store { get; set; }

        public string StoreAddress { get; set; }

        public DateTime? PhurcaseDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Notes { get; set; }

        public string DocumentPath { get; set; }
    }
}