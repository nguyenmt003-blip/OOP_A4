using System;

namespace PayrollSystem
{
    public class SalesEmployee : Employee
    {
        public double BaseSalary { get; private set; }
        public double SalesRevenue { get; private set; }
        public double CommissionRate { get; private set; }

        // Constructor 1: Rút gọn (Doanh số = 0, Tỷ lệ hoa hồng = 0)
        public SalesEmployee(string id, string name, double baseSalary)
            : this(id, name, "Unassigned", baseSalary, 0, 0) { }

        // Constructor 2: Đầy đủ
        public SalesEmployee(string id, string name, string dept, double baseSalary, double revenue, double rate)
            : base(id, name, dept)
        {
            if (baseSalary < 0)
                throw new ArgumentException("Lương cơ bản không được âm.", nameof(baseSalary));
            if (revenue < 0)
                throw new ArgumentException("Doanh số không được âm.", nameof(revenue));
            if (rate < 0 || rate > 0.3)
                throw new ArgumentException("Tỷ lệ hoa hồng phải từ 0 đến 0.3 (30%).", nameof(rate));

            BaseSalary = baseSalary;
            SalesRevenue = revenue;
            CommissionRate = rate;
        }

        // Phương thức cập nhật doanh số có kiểm soát (Yêu cầu mục B.4)
        public void UpdateSalesRevenue(double additionalRevenue)
        {
            if (additionalRevenue < 0)
                throw new ArgumentException("Doanh số cộng thêm không được âm.", nameof(additionalRevenue));
            SalesRevenue += additionalRevenue;
        }

        public override double CalculateGrossPay()
        {
            return BaseSalary + (SalesRevenue * CommissionRate) + MonthlyBonus;
        }

        public override string GetEmployeeType() => "Nhân viên kinh doanh";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"   + Lương CB: {BaseSalary:N0} | Doanh số: {SalesRevenue:N0} | Hoa hồng: {CommissionRate * 100}%");
            Console.WriteLine($"   => THU NHẬP: {CalculateGrossPay():N0} VNĐ\n");
        }
    }
}