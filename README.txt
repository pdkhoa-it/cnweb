
#Chào mừng đến với Furniture Store - Nhóm 18 - Công nghệ Web

/*Đây là một trang web đơn giản ứng dụng công nghệ ASP .NET MVC 5 và SQL Server*/

/*Liên kết github https://github.com/pdkhoa-it/cnweb */

#Thông tin đăng nhập
	**Admin**
	- Tên đăng nhập: admin@gmail.com
	- Mật khẩu     : 1234
	
	**Khách hàng**
	- Tên đăng nhập: nhtung@gmail.com
	- Mật khẩu     : 1234

#Qui trình thêm, xóa dữ liệu
	**Thêm**
	1. Phân quyền -> Tài khoản
	2. Nhà cung cấp
	3. Nhóm sản phẩm -> Danh mục sản phẩm
	4. Sản phẩm -> Đơn hàng -> Chi tiết đơn hàng
	
	**Xóa**
	1. Chi tiết đơn hàng -> Đơn hàng -> Sản phẩm
	2. Danh mục sản phẩm -> Nhóm sản phẩm
	3. Nhà cung cấp
	4. Tài khoản -> Phân quyền

#Chức năng có thể sử dụng
	**Chung**
	- Session: đăng nhập, hiển thị theo nhóm, áp dụng mã giảm giá (furniture), giỏ hàng

	**Admin**
	- Đăng nhập, đăng xuất
	- Xem, thêm, sửa, xóa dữ liệu các bảng
	- Nhập, xuất file Excel cho bảng *Sản phẩm*, *Đơn hàng*, *Chi tiết đơn hàng*
	
	**Khách hàng**
	- Đăng nhập, đăng xuất
	- Cập nhật thông tin cá nhân, đổi mật khẩu
	- Xem sản phẩm
	- Thêm sản phẩm vào giỏ, dùng mã giảm giá (furniture), thanh toán
	
	**Người lạ**
	- Xem sản phẩm
	- Thêm sản phẩm vào giỏ, dùng mã giảm giá (furniture), thanh toán

#Nền tảng sửa dụng
	- ASP .NET MVC 5
	- SQL Server 2018
	- NET Framework: 4.7.2
	- Entity Framework: 6.2.0
	- jQuery: 3.5.1
	- jQuery-Datatable, jQuery-UI,...
	- Bootstrap:
		+ Layout Admin: 4.1.3
		+ Main Admin: 4.5.3
		+ User: 4.0.0
