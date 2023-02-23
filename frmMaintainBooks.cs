using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASS_4
{
    public partial class frmMaintainBooks : Form
    {
        public frmMaintainBooks()
        {
            InitializeComponent();
        }

        BookDao bookDao = new BookDao();
        DataTable dtBook;

        private void frmMaintainBooks_Load(object sender, EventArgs e)
        {
            getAllBooks();
        }

        private void getAllBooks()
        {
            dtBook = bookDao.getBooks();
            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();

            txtBookID.DataBindings.Add("Text", dtBook, "bookID");
            txtBookName.DataBindings.Add("Text", dtBook, "bookName");
            txtBookPrice.DataBindings.Add("Text", dtBook, "bookPrice");

            dgvListBook.DataSource = dtBook;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int bookID = int.Parse(txtBookID.Text);
            string bookName = txtBookName.Text;
            float bookPrice = float.Parse(txtBookPrice.Text);
            Book book = new Book(bookID, bookName, bookPrice);

            bool r = bookDao.updateBook(book);
            string s = (r == true ? "successful" : "fail");
            MessageBox.Show("Update " + s);
            getAllBooks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int bookID = int.Parse(txtBookID.Text);
            string bookName = txtBookName.Text;
            float bookPrice = float.Parse(txtBookPrice.Text);
            Book book = new Book(bookID, bookName, bookPrice);

            bool r = bookDao.addNewBook(book);
            string s = (r == true ? "successful" : "fail");
            MessageBox.Show("Add " + s);
            getAllBooks();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int bookID = int.Parse(txtBookID.Text);

            bool r = bookDao.removeBook(bookID);
            string s = (r == true ? "successful" : "fail");
            MessageBox.Show("Delete " + s);
            getAllBooks();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dtBook.DefaultView;
            string filter = "bookName like '%" + txtSearch.Text + "%'";
            dv.RowFilter = filter;
            lbTotal.Text = "Total Price: " + dtBook.Compute("SUM(bookPrice)", filter);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvListBook;
            try
            {
                int totalRows = dgv.Rows.Count-1;

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                if (rowIndex == totalRows - 1) return;

                changeRecord(dtBook.Rows[rowIndex], dtBook.Rows[rowIndex + 1]);

                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvListBook;
            try
            {
                int totalRows = dgv.Rows.Count - 1;

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                if (rowIndex == 0) return;

                changeRecord(dtBook.Rows[rowIndex], dtBook.Rows[rowIndex - 1]);

                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvListBook;
            try
            {
                int totalRows = dgv.Rows.Count - 1;

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                if (rowIndex == 0) return;

                changeRecord(dtBook.Rows[rowIndex], dtBook.Rows[0]);

                dgv.ClearSelection();
                dgv.Rows[0].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvListBook;
            try
            {
                int totalRows = dgv.Rows.Count - 1;

                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;

                if (rowIndex == totalRows - 1) return;

                changeRecord(dtBook.Rows[rowIndex], dtBook.Rows[totalRows-1]);

                dgv.ClearSelection();
                dgv.Rows[totalRows - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        public void changeRecord(DataRow row1, DataRow row2)
        {
            string bookID = row1["bookID"].ToString();
            string bookName = row1["bookName"].ToString();
            string bookPrice = row1["bookPrice"].ToString();

            row1["bookID"] = row2["bookID"];
            row1["bookName"] = row2["bookName"];
            row1["bookPrice"] = row2["bookPrice"];

            row2["bookID"] = bookID;
            row2["bookName"] = bookName;
            row2["bookPrice"] = bookPrice;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmBookReport bookReport = new frmBookReport();
            bookReport.Closed += (s, args) => this.Close();
            bookReport.Show();
        }
    }
}
