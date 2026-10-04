using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem
{
    public class Payroll
    {
        public string Period { get; set; }
        
        // Danh sách đa hình: chứa bất kỳ đối tượng nào kế thừa từ Employee
        private List<Employee> _employees;

        public Payroll(string period)
        {
            if (string.IsNullOrWhiteSpace(period))
                throw new ArgumentException("Kỳ lương không được để trống.");

            Period = period;
            _employees = new List<Employee>();
        }

        // Thêm nhân sự: Có kiểm tra ràng buộc không được trùng mã
        public void AddEmployee(Employee employee)
        {
            if (employee == null) 
                throw new ArgumentNullException(nameof(employee));

            if (_employees.Any(e => e.EmployeeId.Equals(employee.EmployeeId, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Nhân viên có mã {employee.EmployeeId} đã tồn tại trong bảng lương.");
            }

            _employees.Add(employee);
        }

        // Tìm nhân viên theo mã
        public Employee? FindEmployee(string employeeId)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId.Equals(employeeId, StringComparison.OrdinalIgnoreCase));
        }

        // Tính tổng quỹ lương (Lời gọi Đa hình: không dùng if/else theo loại nhân sự)
        public double CalculateTotalPayroll()
        {
            return _employees.Sum(e => e.CalculateGrossPay());
        }

        // Tính tổng lương theo phòng ban
        public double CalculatePayrollByDepartment(string department)
        {
            return _employees
                .Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
                .Sum(e => e.CalculateGrossPay());
        }

        // Tìm người có thu nhập cao nhất (xử lý an toàn khi danh sách rỗng)
        public Employee? FindHighestPaidEmployee()
        {
            if (!_employees.Any()) return null;
            return _employees.OrderByDescending(e => e.CalculateGrossPay()).First();
        }

        // Hiển thị bảng lương
        public void DisplayPayroll()
        {
            Console.WriteLine($"================ BẢNG LƯƠNG KỲ: {Period} ================");
            if (!_employees.Any())
            {
                Console.WriteLine("Bảng lương hiện tại chưa có nhân sự nào.");
                return;
            }

            foreach (var emp in _employees)
            {
                emp.DisplayPayrollInfo(); // Lời gọi đa hình
            }

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine($"TỔNG QUỸ LƯƠNG PHẢI CHI: {CalculateTotalPayroll():N0} VNĐ");
            Console.WriteLine("=========================================================\n");
        }
    }
}