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
insert into PhanQuyen values(N'Nhân Viên');
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
	NgaySinh varchar(20) not null,
	GioiTinh nvarchar(3) not null,
	DiaChi nvarchar(100) not null,
	Sdt varchar(20) not null,
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
	ImgPath varchar(max) not null,
	MoTa nvarchar(500) not null,
	IDNCC int not null,
	IDDanhMucSP int not null,

	foreign key(IDNCC) references NhaCungCap(ID),
	foreign key(IDDanhMucSP) references DanhMucSanPham(ID)
);
go

select * from SanPham


create table DonHang
(
	ID int identity primary key,
	IDTaiKhoan int not null,
	HinhThucThanhToan nvarchar(50) not null,
	NgayThang varchar(20) not null,
	DiaChiGiaoHang nvarchar(100) not null,
	TinhTrangThanhToan tinyint not null,
	TinhTrangGiaoHang tinyint not null

	foreign key(IDTaiKhoan) references TaiKhoan(ID)
);
go

create table ChiTietDonHang
(
	ID int identity primary key,
	IDSanPham int not null,
	IDDonHang int not null,
	SoLuong decimal not null,
	DonGia decimal not null,
	ThanhTien decimal default 0 not null

	foreign key(IDSanPham) references SanPham(ID),
	foreign key(IDDonhang) references DonHang(ID)
);
go
