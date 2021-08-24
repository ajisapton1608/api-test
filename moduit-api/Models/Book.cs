using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moduit_api.Models
{
    public class Book
    {
        public Book() { }
        public Book(int id, int category, string title, string description, string footer, DateTime createdAt)
        {
            this.id = id;
            this.category = category;
            this.title = title;
            this.description = description;
            this.footer = footer;
            this.createdAt = createdAt;
        }

        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class BookTags: Book
    {
        public List<string> tags { get; set; }
    }

    public class BookQuery
    {
        public string title { get; set; }
        public string description { get; set; }
        public string tags { get; set; }
    }

    public class InputBook
    {
        public int id { get; set; }
        public int category { get; set; }
        public List<InputBookDetail> items { get; set; }
        public DateTime createdAt { get; set; }
    }

    public class InputBookDetail
    {
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
    }
}
