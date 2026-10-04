using System;

namespace PayrollSystem
{
    public class SalariedEmployee : Employee
    {
        // Thuộc tính riêng được đóng gói
        public double MonthlySalary { get; private set; }
        public double ResponsibilityAllowance { get; private set; }

        // Constructor 1: Rút gọn (Phòng ban mặc định "Unassigned", phụ cấp mặc định 0)
        public SalariedEmployee(string id, string name, double salary)
            : this(id, name, "Unassigned", salary, 0) { }

        // Constructor 2: Đầy đủ (Ủy quyền constructor lớp cha base)
        public SalariedEmployee(string id, string name, string dept, double salary, double allowance)
            : base(id, name, dept)
        {
            if (salary < 0)
                throw new ArgumentException("Lương cố định không được âm.", nameof(salary));
            if (allowance < 0)
                throw new ArgumentException("Phụ cấp trách nhiệm không được âm.", nameof(allowance));

            MonthlySalary = salary;
            ResponsibilityAllowance = allowance;
        }

        // Ghi đè công thức tính thu nhập
        public override double CalculateGrossPay()
        {
            return MonthlySalary + ResponsibilityAllowance + MonthlyBonus;
        }

        public override string GetEmployeeType() => "Nhân viên lương cố định";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"   + Lương CB: {MonthlySalary:N0} | Phụ cấp: {ResponsibilityAllowance:N0}");
            Console.WriteLine($"   => THU NHẬP: {CalculateGrossPay():N0} VNĐ\n");
        }
    }
}