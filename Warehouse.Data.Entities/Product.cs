namespace Warehouse.Data.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Company { get; set; }

        public string Mail { get; set; }

        public string Color { get; set; }

        public string Seller { get; set; }

        public string Review { get; set; }

        public string Description { get; set; }

        public string Currency { get; set; }

        public string Category { get; set; }

        public string Ean13 { get; set; }
    }
}
