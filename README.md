# HospitalManagement
 
# FHMS

Ứng dụng hỗ trợ quản lý nhân sự ở các bệnh viện dã chiến.

## 1. Mô tả 

  Năm 2021 là một năm khó khăn của đất nước ta khi lâm vào đại dịch lần thứ 4. Trong suốt đợt dịch này, việc các bệnh viện lớn quá tải dẫn đến nhu cầu cấp thiết
  xây dựng những bệnh viện dã chiến. Việc xây dựng nhanh chóng và nhân sự thay đổi liên tục khiến cho việc quản lý các bệnh viện này rất khó khăn. Một ứng dụng trên desktop nhằm hỗ trợ việc quản lý nhân viên và bệnh nhân sẽ đáp ứng được nhu cầu này và cũng hưởng ứng chủ trương của Nhà Nước là áp dụng công nghệ vào chống dịch.


### 2. Mục đích, yêu cầu, người dùng hướng tới của đề tài

#### Mục đích

* Sản phẩm được tạo ra nhằm mục đích hỗ trợ những người quản lý bệnh viện dã chiến dễ dàng hơn. 

* Sản phẩm được tạo ra nhằm mục đích giúp các bác sĩ có thể làm quen với môi trường làm việc nhanh hơn khi điều chuyển công tác. 

* Hỗ trợ các tổ công tác có thể thực hiện nhiệm vụ nhanh chóng hơn. 

#### Yêu cầu

* UI/UX hợp lý, rõ ràng, thuận tiện cho người sử dụng. 

* Ứng dụng có những tính năng cơ bản. 

* Phân chia quyền hạn rõ ràng. 

#### Người dùng

* Nhân viên quản lý nhân sự

* Nhân viên y tế (bác sĩ, y tá)

### 3. Tổng quan sản phẩm

#### 3.1 Chức năng
<details>
  <summary>Chức năng chung</summary>
 
- Đăng nhập
- Đăng xuất
- Quên mật khẩu
- Theo dõi số liệu tổng quan của bệnh viện
- Thiết lập các thông tin cá nhân của bản thân
- Báo cáo lỗi

</details>

  ###### Superadmin (Quản trị viên)

  <details>
    <summary>Quản lý toàn bộ danh sách bác sĩ trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách y tá trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách bệnh nhân trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Thêm
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý tòa</summary>

  - Thêm
  - Xóa
  - Xem chi tiết thông tin bệnh nhân
  - Chuyển bệnh nhân giữa các phòng

  </details>

  <details>
    <summary>Quản lý các tổ công tác</summary>

  - Thêm
  - Xóa
  - Lọc các tổ theo tầng và tòa

  </details>

  <details>
    <summary>Quản lý các tài khoản được cấp trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Thêm (tài khoản admin, tổ trưởng, bác sĩ, y tá)
  - Xóa

  </details>


  ###### Admin (Quản lý nhân sự)

  <details>
    <summary>Quản lý toàn bộ danh sách bác sĩ trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách y tá trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách bệnh nhân trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Thêm
  - Xóa
  - Xem chi tiết
  - Sửa
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý tòa</summary>

  - Xem chi tiết thông tin bệnh nhân
  - Chuyển bệnh nhân giữa các phòng

  </details>

  <details>
    <summary>Quản lý các tổ công tác</summary>

  - Thêm
  - Xóa
  - Lọc các tổ theo tầng và tòa

  </details>

  <details>
    <summary>Quản lý các tài khoản được cấp trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Thêm (tài khoản tổ trưởng, bác sĩ, y tá)
  - Xóa

  </details>


  ###### Leader (Tổ trưởng)

  <details>
    <summary>Quản lý toàn bộ danh sách bác sĩ trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xem chi tiết
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách y tá trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xem chi tiết
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý toàn bộ danh sách bệnh nhân trong bệnh viện</summary>

  - Tìm kiếm
  - Sắp xếp
  - Xem chi tiết
  - Xuất excel

  </details>

  <details>
    <summary>Quản lý phòng (do tổ công tác này quản lý)</summary>

  - Xem chi tiết thông tin bệnh nhân
  - Chuyển bệnh nhân giữa các phòng

  </details>

  <details>
    <summary>Quản lý tổ công tác</summary>

  - Hiển thị thông tin tổ
  - Hiển thị các thành viển trong tổ
  - Hiển thị công việc đã giao, hoàn thành của các thành viên trong tổ.
  - Xem thông tin của các thành viên trong tổ

  </details>

  <details>
    <summary>Quản lý phân chia công việc</summary>

  - Thêm công việc
  - Xóa công Việc
  - Cập nhập thông tin công việc
  - Hiển thị các công việc theo ngày
  - Xuất excel

  </details>


###### Member (Thành viên trong tổ)

  <details>
    <summary>Quản lý phòng (do tổ công tác này quản lý)</summary>

  - Xem chi tiết thông tin bệnh nhân
  - Chuyển bệnh nhân giữa các phòng

  </details>

  <details>
    <summary>Quản lý tổ công tác</summary>

  - Hiển thị thông tin tổ
  - Hiển thị các thành viển trong tổ
  - Hiển thị công việc đã giao, hoàn thành của các thành viên trong tổ.
  - Xem thông tin của các thành viên trong tổ

  </details>

  <details>
    <summary>Quản lý phân chia công việc</summary>

  - Hiển thị các công việc theo ngày
  - Xuất excel

  </details>


#### 3.2 Công nghệ sử dụng

- Công cụ: Visual Studio, SQL Server Management Studio, Github Desktop, Microsoft SQL Server, Microsoft Azure
- Ngôn ngữ lập trình: C#, TSQL
- Thư viện: .NET Framework, MaterialDesignXAML, Show Me The XAML, LiveCharts, GONG WPF Drag Drop, Entity Framework,ClosedXML

## 4. Hướng dẫn cài đặt
<details>
    <summary>Đối với người dùng</summary>

  * Liên hệ với nhà phát triển để được hỗ trợ khởi tạo cơ sở dữ liệu và kết nối đến cơ sở dữ liệu.
  * Giải nén và chạy file Setup.msi hoặc Setup.exe
    * Dowload phần mềm tại: https://drive.google.com/drive/u/2/folders/18iDTpAigeWhwV13JjmwNXfZm_JCUT8G3

</details>

<details>
    <summary>Đối với nhà phát triển</summary>

  * Dowload, giải nén phần mềm
    * Github: https://github.com/quangnhat22/HospitalManagement
    * Google Drive: https://drive.google.com/drive/u/2/folders/18iDTpAigeWhwV13JjmwNXfZm_JCUT8G3
  * Cài đặt database
    * Khuyến nghị sử dụng các dịch vụ đám mây như Azure, AWS,... để sử dụng tất cả tính năng hiện có của chương trình.
    * Ngoài ra có thể sử dụng SQL Server (Lưu ý: cách này sẽ mất đi tính năng tương tác giữa các user ở các máy tính khác nhau).
  * Khởi tạo Database bằng cách chạy script chứa trong file TaoCSL.sql
  * Kết nối với Database vừa tạo bằng cách thay đổi connectionStrings trong file App.config.
  * Có thể sử dụng project Seeds để tạo dữ liệu giả.
  * Đăng nhập với vai trò superadin
      * tên đăng nhập: superadmin
      * mật khẩu: 1

</details>

## 5. Hướng dẫn sử dụng

* Video demo: https://youtu.be/Kwr5I1C1UKs

## 6. Tác giả

| STT | MSSV     | Họ và tên                                                  | Lớp      | 
| --- | -------- | ---------------------------------------------------------- | -------- | 
| 1   | 20520719 | [Nguyễn Đình Nhật Quang](https://github.com/quangnhat22)          | KTPM2020 | 
| 2   | 20520342 | [Bùi Minh Tuấn](https://github.com/tuan20520342)             | KTPM2020 | 
| 3   | 20521800 | [Đỗ Phú Quang](https://github.com/phuquang14722) | KTPM2020 | 
| 4   | 20520236 | [Trần Đình Lộc](https://github.com/LocTranDinh)         | KTPM2020 | 
| 5   | 20521659 | [Võ Đình Nghĩa](https://github.com/nghia0111)             | KTPM2020 | 

* Sinh viên khoa Công nghệ Phần mềm, trường Đại học Công nghệ Thông tin, Đại học Quốc gia thành phố Hồ Chí Minh.

## 7. Giảng viên hướng dẫn

* Thầy Nguyễn Tấn Toàn, giảng viên Khoa Công Nghệ Phần Mềm, trường Đại học Công nghệ Thông tin, Đại học Quốc gia Thành phố Hồ Chí Minh.
