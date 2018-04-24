create database shopMeVaBe
use shopMeVaBe
create table Users(
	tenDangNhap varchar(20) primary key,
	matKhau varchar(20) NOT NULL,
	hoTen Nvarchar(30) NOT NULL,
	typeAccount bit
);

create table NhaCungCap(
	maNhaCungCap char(5) primary key,
	tenNhaCungCap nvarchar(30) NOT NULL
)

create table MatHang(
	maHang char(5) primary key,
	tenHang nvarchar(30) not NUll,
	giaVon int,
	giaBan int,
	soLuong int,
	maNhaCungCap char(5) references NhaCungCap(maNhaCungCap)
	on update cascade on delete cascade

);

create table HoaDon(
maHoaDon int IDENTITY(1,1) primary key,
tenDangNhap varchar(20) references Users(tenDangNhap)
on update cascade on delete cascade,
tenKhachHang nvarchar(30),
ngay date,
chiecKhau int,
khachdua int
);
create table chiTietHoaDon(
	stt int IDENTITY(1,1) primary key,
	maHoaDon int references HoaDon(maHoaDon)
	on update cascade on delete cascade,
	maHang char(5) references MatHang(maHang) 
	on update cascade on delete cascade,
	soLuong int
);
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap]) VALUES (N'n1   ', N'ÔMÔ')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap]) VALUES (N'n2   ', N'VINAMILK')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap]) VALUES (N'n3  ', N'DOwNY')
INSERT [dbo].[Users] ([tenDangNhap], [matKhau], [hoTen], [typeAccount]) VALUES (N'nhanvien1', N'12345', N'Nguyễn Văn A', 0)
INSERT [dbo].[Users] ([tenDangNhap], [matKhau], [hoTen], [typeAccount]) VALUES (N'nhanvien2', N'12345', N'Nguyễn văn B', 0)
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp001', N'Bột giặt', 20000, 23000, 50, N'n1   ')
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp002', N'Sữa tắm', 29000, 37000, 30, N'n1   ')
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp003', N'dầu gội', 30000, 35000, 40, N'n1   ')


