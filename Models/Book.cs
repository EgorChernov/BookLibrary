using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        [Required] [Key] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Author { get; set; }

        public int NumberOfPage { get; set; }

        public override string ToString()
        {
            return $"Guid='{Id}',Name='{Name}',Author='{Author}',NumberOfPage={NumberOfPage}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(obj, null)) return false;
            if (this.GetType() != obj.GetType()) return false;
            var b = (Book) obj;
            return this.Name == b.Name && this.Author == b.Author && this.NumberOfPage == b.NumberOfPage;
        }
    }
}