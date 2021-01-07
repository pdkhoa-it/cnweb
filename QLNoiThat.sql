use master
go 

create database QLNoiThat 
go

use QLNoiThat
go

create table PhanQuyen
(
	ID int identity primary key,
	Ten nvarchar(50) not null
);
go

insert into PhanQuyen values(N'Quản Trị Viên');
insert into PhanQuyen values(N'Khách Hàng');

select * from PhanQuyen
create table TaiKhoan
(
	ID int identity primary key,
	Email varchar(100) not null,
	MatKhau varchar(max) not null,
	XacNhanMatKhau varchar(max) not null,
	Salt int not null,
	HoTen nvarchar(50) not null,
	NgaySinh varchar(20),
	GioiTinh nvarchar(3),
	DiaChi nvarchar(100),
	Sdt varchar(20),
	IDQuyen int not null,
	
	foreign key(IDQuyen) references PhanQuyen(ID)
);
go

create table NhomSanPham
(
	ID int identity primary key,
	Ten nvarchar(50) not null,
	Ten_slug varchar(50) not null
);
go


create table DanhMucSanPham
(
	ID int identity primary key,
	IDNhomSP int not null,
	Ten nvarchar(50) not null,
	Ten_slug varchar(50) not null

	foreign key(IDNhomSP) references NhomSanPham(ID)
);
go

create table NhaCungCap
(
	ID int identity primary key,
	Ten nvarchar(100) not null,
	DiaChi nvarchar(100) not null,
	SoDienThoai nvarchar(20) not null,
	Email nvarchar(100) not null,
	MaSoThue nvarchar(100) not null,
	SoTaiKhoan nvarchar(100) not null,
	NguoiDaiDien nvarchar(100) not null
);
go


create table SanPham
(
	ID int identity primary key,
	Ten nvarchar(100) not null,
	Ten_slug varchar(100) not null,
	Ten_img varchar(100) not null,
	Gia decimal not null,
	MoTa nvarchar(500) not null,
	IDNCC int not null,
	IDDanhMucSP int not null,

	foreign key(IDNCC) references NhaCungCap(ID),
	foreign key(IDDanhMucSP) references DanhMucSanPham(ID)
);
go

create table DonHang
(
	ID int identity primary key,
	HoTen nvarchar(50) not null,
	Sdt varchar(20) not null,
	Email varchar(100) not null,
	ThoiGian varchar(20) not null,
	DiaChiGiaoHang nvarchar(100) not null,
	HinhThucThanhToan tinyint not null,
	TinhTrangThanhToan tinyint not null,
	TinhTrangGiaoHang tinyint not null,
	TongTien decimal default 0 not null,
	DaGiamGia decimal default 0 not null,
);
go

create table ChiTietDonHang
(
	IDCT int identity primary key,
	IDSanPham int not null,
	IDDonHang int not null,
	SoLuong decimal not null,
	DonGia decimal not null,
	ThanhTien decimal default 0 not null

	foreign key(IDSanPham) references SanPham(ID),
	foreign key(IDDonhang) references DonHang(ID)
);
go

select * from NhomSanPham