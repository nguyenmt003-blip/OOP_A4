/*
202419010
Nguyễn Minh Tuấn
*/ 
using System;

namespace PayrollSystem
{
    public class HourlyEmployee : Employee
    {
        public double HourlyRate { get; private set; }
        public double WorkedHours { get; private set; }

        // Constructor 1: Rút gọn (Số giờ làm mặc định là 0)
        public HourlyEmployee(string id, string name, double rate)
            : this(id, name, "Unassigned", rate, 0) { }

        // Constructor 2: Đầy đủ
        public HourlyEmployee(string id, string name, string dept, double rate, double hours)
            : base(id, name, dept)
        {
            if (rate < 0)
                throw new ArgumentException("Đơn giá giờ không được âm.", nameof(rate));
            if (hours < 0 || hours > 250)
                throw new ArgumentException("Số giờ làm hợp lệ từ 0 đến 250 giờ.", nameof(hours));

            HourlyRate = rate;
            WorkedHours = hours;
        }

        // Ghi đè công thức: Có tính tiền làm thêm giờ vượt ngưỡng 160h
        public override double CalculateGrossPay()
        {
            double basePay;
            if (WorkedHours <= 160)
            {
                basePay = WorkedHours * HourlyRate;
            }
            else
            {
                // 160h đầu tính bình thường, giờ dôi ra nhân hệ số 1.5
                basePay = (160 * HourlyRate) + ((WorkedHours - 160) * HourlyRate * 1.5);
            }
            return basePay + MonthlyBonus;
        }

        public override string GetEmployeeType() => "Nhân viên theo giờ";

        public override void DisplayPayrollInfo()
        {
            base.DisplayPayrollInfo();
            Console.WriteLine($"   + Đơn giá: {HourlyRate:N0}/h | Số giờ: {WorkedHours}h");
            Console.WriteLine($"   => THU NHẬP: {CalculateGrossPay():N0} VNĐ\n");
        }
    }
}