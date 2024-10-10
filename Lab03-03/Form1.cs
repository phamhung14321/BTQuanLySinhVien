using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab03_03
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            cmbKhoa.SelectedIndex = cmbKhoa.Items.IndexOf("Công nghệ thông tin");
            rbtnNu.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (txtMSSV.Text.Length != 10 || !long.TryParse(txtMSSV.Text, out _))
            {
                MessageBox.Show("Mã số sinh viên không hợp lệ!");
                return;
            }

            if (!(txtHoTen.Text.Length >= 3 && txtHoTen.Text.Length <= 100 && IsValidName(txtHoTen.Text)))
            {
                MessageBox.Show("Họ tên không hợp lệ!");
                return;
            }

            if (!double.TryParse(txtDiem.Text, out double diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm trung bình không hợp lệ!");
                return;
            }


            form2.AddOrUpdateStudent(txtMSSV.Text, txtHoTen.Text, rbtnNam.Checked ? "Nam" : "Nữ", txtDiem.Text,cmbKhoa.SelectedItem.ToString());
            MessageBox.Show("Thêm mới dữ liệu thành công!");

            form2.Show();
        }

        private bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSSV.Text) ||
                string.IsNullOrWhiteSpace(txtHoTen.Text) ||
                string.IsNullOrWhiteSpace(txtDiem.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (txtMSSV.Text.Length != 10 || !long.TryParse(txtMSSV.Text, out _))
            {
                MessageBox.Show("Mã số sinh viên không hợp lệ!");
                return;
            }

            if (!(txtHoTen.Text.Length >= 3 && txtHoTen.Text.Length <= 100 && IsValidName(txtHoTen.Text)))
            {
                MessageBox.Show("Họ tên không hợp lệ!");
                return;
            }

            if (!double.TryParse(txtDiem.Text, out double diem) || diem < 0 || diem > 10)
            {
                MessageBox.Show("Điểm trung bình không hợp lệ!");
                return;
            }

            // Lưu ý đảm bảo thứ tự các tham số ở đây khớp với phương thức trong Form2
            form2.AddOrUpdateStudent(txtMSSV.Text, txtHoTen.Text, rbtnNam.Checked ? "Nam" : "Nữ", cmbKhoa.SelectedItem.ToString(), txtDiem.Text);
            MessageBox.Show("Cập nhật dữ liệu thành công!");
            form2.Show();
        }


        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

       
    }
}

