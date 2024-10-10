using System;
using System.Linq;
using System.Windows.Forms;

namespace Lab03_03
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            LoadInitialState();
        }

        private void LoadInitialState()
        {
            txtNam.Text = "0";
            txtNu.Text = "0";
            UpdateStudentCount();
        }

        public void AddOrUpdateStudent(string mssv, string hoTen, string gioiTinh, string khoa, string diem)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(mssv) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(diem))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra mã số sinh viên
            if (!long.TryParse(mssv, out _) || mssv.Length != 10)
            {
                MessageBox.Show("Mã số sinh viên không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra họ tên
            if (hoTen.Length < 3 || hoTen.Length > 100 || hoTen.Any(char.IsDigit) || hoTen.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
            {
                MessageBox.Show("Họ tên không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xem mã số sinh viên đã tồn tại hay chưa
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["dgvMSSV"].Value.ToString() == mssv)
                {
                    // Cập nhật thông tin sinh viên
                    row.Cells["dgvHoTen"].Value = hoTen;
                    row.Cells["dgvGioiTinh"].Value = gioiTinh;
                    row.Cells["dgvKhoa"].Value = khoa;
                    row.Cells["dgvDiem"].Value = diem;
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateStudentCount();
                    return;
                }
            }

            // Kiểm tra xem tên sinh viên đã tồn tại hay chưa
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["dgvHoTen"].Value.ToString().Equals(hoTen, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Tên sinh viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Thêm sinh viên mới vào DataGridView
            dgvSinhVien.Rows.Add(dgvSinhVien.Rows.Count + 1, mssv, hoTen, gioiTinh, khoa, diem);
            UpdateStudentCount();
        }

        private void UpdateStudentCount()
        {
            // Cập nhật số lượng sinh viên Nam/Nữ
            int maleCount = 0;
            int femaleCount = 0;

            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                if (row.Cells["dgvGioiTinh"].Value.ToString() == "Nam")
                {
                    maleCount++;
                }
                else
                {
                    femaleCount++;
                }
            }

            txtNam.Text = maleCount.ToString();
            txtNu.Text = femaleCount.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            // Gọi hàm tìm kiếm khi người dùng nhập
            SearchStudentByName(txtTim.Text);
        }

        private void SearchStudentByName(string searchKeyword)
        {
            // Nếu từ khóa tìm kiếm rỗng thì không làm gì
            if (string.IsNullOrEmpty(searchKeyword))
            {
                return; // Không làm gì nếu không có từ khóa
            }

            bool found = false;

            // Duyệt qua tất cả các hàng trong DataGridView
            foreach (DataGridViewRow row in dgvSinhVien.Rows)
            {
                // Kiểm tra xem tên sinh viên có chứa từ khóa không (bỏ qua hoa/thường)
                if (row.Cells["dgvHoTen"].Value.ToString().IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Nếu tìm thấy, chọn dòng này
                    row.Selected = true;
                    dgvSinhVien.FirstDisplayedScrollingRowIndex = row.Index; // Cuộn tới dòng tìm được
                    found = true;
                }
                else
                {
                    // Bỏ chọn các dòng khác
                    row.Selected = false;
                }
            }

            // Nếu không tìm thấy, có thể hiển thị thông báo
            if (!found)
            {
                MessageBox.Show("Không tìm thấy sinh viên nào có tên phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
