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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        BookDao bookDao = new BookDao();
        EmployeeDao employeeDao = new EmployeeDao();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string empID = txtEmpID.Text;
            string empPassword = txtEmpPassword.Text;

            Employee employee = employeeDao.getEmployee(empID, empPassword);
            if (employee == null)
            {
                MessageBox.Show("ID or Password wrong!!","Title");
            } else
            {
                if (employee.empRole == true)
                {
                    this.Hide();
                    frmMaintainBooks maintainBooks = new frmMaintainBooks();
                    maintainBooks.Closed += (s, args) => this.Close();
                    maintainBooks.Show();
                } else
                {
                    this.Hide();
                    frmChangeAccount changeAccount = new frmChangeAccount() {
                        empID = employee.empID,
                        empPassword = employee.empPassword,
                        empRole = employee.empRole
                    };
                    changeAccount.Closed += (s, args) => this.Close();
                    changeAccount.Show();
                }
            }
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
