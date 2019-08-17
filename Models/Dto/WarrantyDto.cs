using System;

namespace LiveTogether.Models.Dto
{
    public class WarrantyDto
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public string Store { get; set; }

        public DateTime? PhurcaseDate { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}