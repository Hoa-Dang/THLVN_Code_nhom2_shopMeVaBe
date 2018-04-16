create database shopMeVaBe;
use shopMeVaBe;


create table Users(
	tenDangNhap varchar(20) primary key,
	matKhau varchar(20) NOT NULL,
	hoTen nvarchar(30) NOT NULL,
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
maHoaDon char(5) primary key,
tenDangNhap varchar(20) references Users(tenDangNhap)
on update cascade on delete cascade,
tenKhachHang nvarchar(30),
ngay date,
chiecKhau int,
khachdua int
);

create table chiTietHoaDon(
	stt int IDENTITY(1,1) primary key,
	maHoaDon char(5) references HoaDon(maHoaDon)
	on update cascade on delete cascade,
	maHang char(5) references MatHang(maHang) 
	on update cascade on delete cascade,
	soLuong int
);
select * from HoaDon
insert into Users values('nhanvien1','12345',N'Thằng nhân viên',0);
insert into Users values('nhanvien2','12345',N'Kon nhân viên',0);
insert into NhaCungCap values('n1',N'ÔMÔ');
insert into NhaCungCap values('n2',ComPho');
insert into MatHang values('sp001',N'Xì Bong',10000,12000,50,'n1');
insert into MatHang values('sp002',N'Downny',12000,15000,50,'n2');
insert into HoaDon values('00001','nhanvien1',N'Nguyễn Văn A','2018-4-7',5000,45000);