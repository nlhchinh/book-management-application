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
    public partial class frmBookReport : Form
    {
        public frmBookReport()
        {
            InitializeComponent();
        }

        BookDao bookDao = new BookDao();
        DataTable dtBook;

        private void frmBookReport_Load(object sender, EventArgs e)
        {
            dtBook = bookDao.getBooks();
            dgvListReport.DataSource = dtBook;

            DataView dv = dtBook.DefaultView;
            dv.Sort = "bookPrice Desc";
        }
    }
}
