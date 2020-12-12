use master
go 

create database QLNoiThat 
go

use QLNoiThat
go

create table PhanQuyen
(
	Ma int identity primary key,
	Ten nvarchar(50)
);
go

insert into PhanQuyen values(N'Quản Trị Viên');
insert into PhanQuyen values(N'Nhân Viên');
insert into PhanQuyen values(N'Khách Hàng');

create table TaiKhoan
(
	ID int identity primary key,
	TenDangNhap varchar(50),
	MatKhau varchar(max),
	Salt int,
	Email varchar(100),
	HoTen nvarchar(50),
	NgaySinh datetime,
	GioiTinh bit,
	DiaChi nvarchar(100),
	Sdt varchar(20),
	MaQuyen int,
	
	foreign key(MaQuyen) references PhanQuyen(Ma)
);
go

insert into TaiKhoan(TenDangNhap,MatKhau,Email,HoTen,NgaySinh,GioiTinh,DiaChi,Sdt,MaQuyen) 
	values('pdk','123','pdk@gmail.com',N'Phan Đăng Khoa','8/12/1999',0,N'An Giang','0123456789','AD');
insert into TaiKhoan(TenDangNhap,MatKhau,Email,HoTen,NgaySinh,GioiTinh,DiaChi,Sdt,MaQuyen) 
	values('lbh','123','lbh@gmail.com',N'Lâm Bách Hợp','6/11/1999',0,N'An Giang','0213452667','NV');
insert into TaiKhoan(TenDangNhap,MatKhau,Email,HoTen,NgaySinh,GioiTinh,DiaChi,Sdt,MaQuyen) 
	values('dnmuyen','123','dnmuyen@gmail.com',N'Đinh Ngọc Minh Uyên','9/16/1999',1,N'An Giang','0123456789','KH');


create table NhomSanPham
(
	ID int identity primary key,
	Ten nvarchar(50)
);
go

insert into NhomSanPham(Ten) values(N'Bàn');
insert into NhomSanPham(Ten) values(N'Ghế');


create table DanhMucSanPham
(
	ID int identity primary key,
	IDNhomSP int,
	Ten nvarchar(50),

	foreign key(IDNhomSP) references NhomSanPham(ID)
);
go

insert into DanhMucSanPham(IDNhomSP,Ten) values(1,N'Bàn Cao');
insert into DanhMucSanPham(IDNhomSP,Ten) values(1,N'Bàn Thấp');
insert into DanhMucSanPham(IDNhomSP,Ten) values(2,N'Ghế SoFa');
insert into DanhMucSanPham(IDNhomSP,Ten) values(2,N'Ghế Gỗ');

create table NhaCungCap
(
	ID int identity primary key,
	Ten nvarchar(100) ,
	DiaChi nvarchar(100),
	SoDienThoai nvarchar(20),
	Email nvarchar(100),
	MaSoThue nvarchar(100),
	SoTaiKhoan nvarchar(100),
	NguoiDaiDien nvarchar(100)
);
go

insert into NhaCungCap(Ten,DiaChi,SoDienThoai,Email,MaSoThue,SoTaiKhoan,NguoiDaiDien) values(N'Nhà Cung Cấp 1',N'An Giang','123408456','nhtung@gmail.com','1602135476','0151000574815','Nguyễn Hoàng Tùng');

create table SanPham
(
	ID int identity primary key,
	Ten nvarchar(100) ,
	IDNCC int,
	IDDanhMucSP int,
	MoTa nvarchar(500)

	foreign key(IDNCC) references NhaCungCap(ID),
	foreign key(IDDanhMucSP) references DanhMucSanPham(ID)
);
go

create table Hinh
(
	ID int identity primary key,
	IDSanPham int,
	DuongDan nvarchar(300)

	foreign key(IDSanPham) references SanPham(ID)
);
go


create table DonHang
(
	ID int identity primary key,
	IDTaiKhoan int,
	HinhThucThanhToan nvarchar(50),
	NgayThang datetime,
	DiaChiGiaoHang nvarchar(100),
	TinhTrangThanhToan bit,
	TinhTrangGiaoHang tinyint

	foreign key(IDTaiKhoan) references TaiKhoan(ID)
);
go

create table ChiTietDonHang
(
	ID int identity primary key,
	IDSanPham int,
	IDDonHang int,
	SoLuong decimal,
	DonGia decimal,
	ThanhTien decimal default 0

	foreign key(IDSanPham) references SanPham(ID),
	foreign key(IDDonhang) references DonHang(ID)
);
go


