using System.ComponentModel.DataAnnotations;

namespace ECommerce.Cache.Tests
{
    public class DocumentTest
    {
        [Key]
        public string Key { get; set; }
        public string Title { get; set; }
    }
}