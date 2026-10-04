using System;

namespace PayrollSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Payroll payroll = new Payroll("2026-09");

            try
            {                // KHỞI TẠO ĐỐI TƯỢNG
                
                // 1. E001: Nguyễn Minh An (Lương cố định)
                var e1 = new SalariedEmployee("E001", "Nguyễn Minh An", "Đào tạo", 15000000, 2000000);
                e1.AddBonus(1000000); // Thưởng cố định: 1.000.000
                payroll.AddEmployee(e1);

                // 2. E002: Trần Thu Bình (Theo giờ, không vượt ngưỡng: 150h)
                var e2 = new HourlyEmployee("E002", "Trần Thu Bình", "Hỗ trợ", 100000, 150);
                e2.AddBonus(500000, "Thưởng chuyên cần");
                payroll.AddEmployee(e2);

                // 3. E003: Lê Hoàng Chi (Theo giờ, có vượt ngưỡng: 170h)
                var e3 = new HourlyEmployee("E003", "Lê Hoàng Chi", "Hỗ trợ", 100000, 170);
                payroll.AddEmployee(e3); // Không có thưởng

                // 4. E004: Phạm Quốc Dũng (Kinh doanh)
                var e4 = new SalesEmployee("E004", "Phạm Quốc Dũng", "Kinh doanh", 8000000, 200000000, 0.05);
                e4.AddBonus(0.02, 50000000, "Thưởng vượt doanh số quý"); // Thưởng 2% của 50.000.000 = 1.000.000
                payroll.AddEmployee(e4);
    
                Console.WriteLine("Đã tạo các đối tượng theo yêu cầu. Bắt đầu hiển thị bảng lương...\n");

                // In toàn bộ bảng lương
                payroll.DisplayPayroll();

                // Kiểm tra các kết quả theo mong đợi:
                Console.WriteLine($"-> Tổng lương phòng Hỗ trợ mong đợi (33,000,000): {payroll.CalculatePayrollByDepartment("Hỗ trợ"):N0} VNĐ");
                
                var topEmployee = payroll.FindHighestPaidEmployee();
                if (topEmployee != null)
                {
                    Console.WriteLine($"-> Người thu nhập cao nhất: {topEmployee.FullName} ({topEmployee.CalculateGrossPay():N0} VNĐ)");
                }

                
                // KIỂM THỬ 10 TÌNH HUỐNG LỖI VÀ BIÊN
               
                Console.WriteLine("\n--------------- CHẠY KIỂM THỬ 10 NGOẠI LỆ ----------------");
                RunEdgeCaseTests(payroll);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không mong muốn: {ex.Message}");
            }
        }

        static void RunEdgeCaseTests(Payroll payroll)
        {
            Action<string, Action> test = (testName, action) =>
            {
                try
                {
                    action();
                    Console.WriteLine($"[FAIL] {testName} -> Vì khôngném ngoại lệ.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[PASS] {testName} -> Lỗi: {ex.Message}");
                }
            };

            test("1. Mã nhân sự rỗng", () => new SalariedEmployee("", "Tên", 1000));
            test("2. Họ tên rỗng", () => new SalariedEmployee("ID01", "", 1000));
            test("3. Thưởng âm", () => { var e = new SalariedEmployee("ID02", "Tên", 1000); e.AddBonus(-500); });
            test("4. Thưởng = 0 (Biên)", () => { var e = new SalariedEmployee("ID03", "Tên", 1000); e.AddBonus(0); });
            test("5. Giờ làm > 250", () => new HourlyEmployee("ID04", "Tên", "Dept", 100000, 251));
            test("6. Giờ làm âm", () => new HourlyEmployee("ID05", "Tên", "Dept", 100000, -1));
            test("7. Hoa hồng > 0.3", () => new SalesEmployee("ID06", "Tên", "Dept", 5000, 10000, 0.35));
            test("8. Trùng mã nhân sự", () => payroll.AddEmployee(new SalariedEmployee("E001", "Trùng", 5000)));
            test("9. Lý do thưởng rỗng", () => { var e = new SalariedEmployee("ID07", "Tên", 1000); e.AddBonus(500, ""); });
            test("10. Tỷ lệ thưởng > 0.5", () => { var e = new SalariedEmployee("ID08", "Tên", 1000); e.AddBonus(0.6, 1000, "Lý do"); });
        }
    }
}
