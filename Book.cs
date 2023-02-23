using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS_4
{
    class Book
    {
        public int bookID { get; set; }
        public string bookName { get; set; }
        public float bookPrice { get; set; }

        public Book(int bookID, string bookName, float bookPrice)
        {
            this.bookID = bookID;
            this.bookName = bookName;
            this.bookPrice = bookPrice;
        }
    }
}
