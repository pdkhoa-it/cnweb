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
	NgaySinh varchar(20) ,
	GioiTinh nvarchar(3),
	DiaChi nvarchar(100),
	Sdt varchar(20),
	MaQuyen int,
	
	foreign key(MaQuyen) references PhanQuyen(Ma)
);
go

create table NhomSanPham
(
	ID int identity primary key,
	Ten nvarchar(50)
);
go

create table DanhMucSanPham
(
	ID int identity primary key,
	IDNhomSP int,
	Ten nvarchar(50),

	foreign key(IDNhomSP) references NhomSanPham(ID)
);
go

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


create table SanPham
(
	ID int identity primary key,
	Ten nvarchar(100) ,
	ImageSP varchar(100),
	MoTa nvarchar(500),
	IDNCC int,
	IDDanhMucSP int,

	foreign key(IDNCC) references NhaCungCap(ID),
	foreign key(IDDanhMucSP) references DanhMucSanPham(ID)
);
go

create table DonHang
(
	ID int identity primary key,
	IDTaiKhoan int,
	HinhThucThanhToan nvarchar(50),
	NgayThang varchar(20),
	DiaChiGiaoHang nvarchar(100),
	TinhTrangThanhToan tinyint,
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
