
/*
202419010
Nguyễn Minh Tuấn
*/ 
using System;
namespace PayrollSystem {
public abstract class Employee
{
    public string EmployeeId { get; private set; }
    public string FullName { get; private set; }
    public string Department { get; private set; }
    public double MonthlyBonus { get; protected set; }

    public Employee(string employeeId, string fullName) 
        : this(employeeId, fullName, "Unassigned", 0) { }

    public Employee(string employeeId, string fullName, string department) 
        : this(employeeId, fullName, department, 0) { }

    public Employee(string employeeId, string fullName, string department, double monthlyBonus)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
            throw new ArgumentException("Mã nhân sự không được để trống.", nameof(employeeId));

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Họ tên không được để trống.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(department))
            throw new ArgumentException("Phòng ban không được để trống.", nameof(department));

        if (monthlyBonus < 0)
            throw new ArgumentException("Thưởng không được âm.", nameof(monthlyBonus));

        this.EmployeeId = employeeId;
        this.FullName = fullName;
        this.Department = department;
        this.MonthlyBonus = monthlyBonus;
    }

    // --- METHODS (Nạp chồng addBonus chuẩn nghiệp vụ) ---

    // thưởng ko lý do
    public double AddBonus(double amount)
    {
        return AddBonus(amount, null);
    }

    // thưởng có lý do
    public double AddBonus(double amount, string? reason)
    {
        if (amount <= 0)
            throw new ArgumentException("Số tiền thưởng phải lớn hơn 0.", nameof(amount));

        // Nếu đã truyền lý do (khác null) thì nội dung TUYỆT ĐỐI không được rỗng
        if (reason != null && string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do thưởng không được để rỗng.", nameof(reason));

        this.MonthlyBonus += amount;

        if (!string.IsNullOrEmpty(reason))
        {
            Console.WriteLine($"Bonus added for reason: {reason}");
        }

        return this.MonthlyBonus;
    }

    // Tính theo tỷ lệ 
    public double AddBonus(double rate, double referenceAmount, string reason)
    {
        if (rate <= 0 || rate > 0.5)
            throw new ArgumentException("Tỷ lệ thưởng phải > 0 và <= 0.5.", nameof(rate));

        if (referenceAmount <= 0)
            throw new ArgumentException("Giá trị tham chiếu phải lớn hơn 0.", nameof(referenceAmount));

        // Bản 3 bắt buộc phải có reason
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do thưởng không được để rỗng.", nameof(reason));

        double bonusAmount = rate * referenceAmount;
        return AddBonus(bonusAmount, reason);
    }

    // Đặt lại thưởng khi sang tháng 
    public void ResetBonus()
    {
        this.MonthlyBonus = 0;
    }

    //Methods để ghi đè trong các lớp con
    public abstract double CalculateGrossPay();
    public abstract string GetEmployeeType();

    public virtual void DisplayPayrollInfo()
    {
        Console.WriteLine($"[{GetEmployeeType()}] Mã: {EmployeeId} | Tên: {FullName} | Phòng: {Department} | Thưởng: {MonthlyBonus:N0}");
    }
}
}
