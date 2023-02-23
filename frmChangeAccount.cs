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
    public partial class frmChangeAccount : Form
    {
        public frmChangeAccount()
        {
            InitializeComponent();
        }

        public string empID { get; set; }
        public string empPassword { get; set; }
        public bool empRole { get; set; }

        EmployeeDao employeeDao = new EmployeeDao();

        private void frmChangeAccount_Load(object sender, EventArgs e)
        {
            lbEmpID.Text = empID;
            lbEmpRole.Text = empRole.ToString();
            txtEmpPassword.Text = empPassword;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = txtEmpPassword.Text;
            Employee employee = new Employee(empID, password, empRole);
            employeeDao.updateEmployee(employee);

            MessageBox.Show("Update successful!!", "Title");

            this.Hide();
            frmLogin login = new frmLogin();
            login.Closed += (s, args) => this.Close();
            login.Show();
        }
    }
}
