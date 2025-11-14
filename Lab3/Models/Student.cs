using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab3.Models;

public partial class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Tên phải từ 4 đến 100 ký tự")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email bắt buộc nhập")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$",
        ErrorMessage = "Email phải có đuôi @gmail.com")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu bắt buộc nhập")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 ký tự trở lên")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
        ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn ngành học")]
    public string? Branch { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn giới tính")]
    public string? Gender { get; set; }

    public bool IsRegular { get; set; }

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Ngày sinh bắt buộc nhập")]
    public DateTime DateOfBirth { get; set; }

    // Score mới
    [Required(ErrorMessage = "Điểm bắt buộc nhập")]
    [Range(0.0, 10.0, ErrorMessage = "Điểm phải từ 0.0 đến 10.0")]
    public double? Score { get; set; }
}
