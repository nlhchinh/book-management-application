using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASS_4
{
    class BookDao
    {
        string strConnection;

        public string getConnectionString()
        {
            string strConnection = @"server=Admin;database=BookStores;uid=sa;pwd=12345";
            return strConnection;
        }
        public BookDao()
        {
            strConnection = getConnectionString();
        }

        public DataTable getBooks()
        {
            string SQL = "select * from Books";
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtBook = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                da.Fill(dtBook);
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtBook;
        }

        public bool addNewBook(Book book)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Insert Books values(@bookID,@bookName,@bookPrice)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@bookID", book.bookID);
            cmd.Parameters.AddWithValue("@bookName", book.bookName);
            cmd.Parameters.AddWithValue("@bookPrice", book.bookPrice);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count;
            try
            {
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                count = 0;
            }
            return (count > 0);
        }

        public bool updateBook(Book book)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update Books set bookName=@bookName,bookPrice=@bookPrice " +
                "where bookID=@bookID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@bookID", book.bookID);
            cmd.Parameters.AddWithValue("@bookName", book.bookName);
            cmd.Parameters.AddWithValue("@bookPrice", book.bookPrice);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }

        public bool removeBook(int bookID)
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Books where bookID=@bookID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@bookID", bookID);
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            int count = cmd.ExecuteNonQuery();
            return (count > 0);
        }
    }
}
