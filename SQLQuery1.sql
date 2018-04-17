USE [master]
GO
/****** Object:  Database [shopMeVaBe]    Script Date: 4/17/2018 7:43:51 AM ******/
CREATE DATABASE [shopMeVaBe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'shopMeVaBe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.HOADANG\MSSQL\DATA\shopMeVaBe.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'shopMeVaBe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.HOADANG\MSSQL\DATA\shopMeVaBe_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [shopMeVaBe] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [shopMeVaBe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [shopMeVaBe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [shopMeVaBe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [shopMeVaBe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [shopMeVaBe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [shopMeVaBe] SET ARITHABORT OFF 
GO
ALTER DATABASE [shopMeVaBe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [shopMeVaBe] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [shopMeVaBe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [shopMeVaBe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [shopMeVaBe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [shopMeVaBe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [shopMeVaBe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [shopMeVaBe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [shopMeVaBe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [shopMeVaBe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [shopMeVaBe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [shopMeVaBe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [shopMeVaBe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [shopMeVaBe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [shopMeVaBe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [shopMeVaBe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [shopMeVaBe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [shopMeVaBe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [shopMeVaBe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [shopMeVaBe] SET  MULTI_USER 
GO
ALTER DATABASE [shopMeVaBe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [shopMeVaBe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [shopMeVaBe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [shopMeVaBe] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [shopMeVaBe]
GO
/****** Object:  StoredProcedure [dbo].[addNhanVien]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[addNhanVien]
as
begin
declare @i INT = 0;
while @i < 20 
begin
insert into Users values(
CONCAT('nhanvien',LTRIM(STR(@i))),'12345',CONCAT(N'Nhân Viên ',STR(@i)),1) 
SET @i = @i + 1
end
end;
exec addNhanVien
GO
/****** Object:  Table [dbo].[chiTietHoaDon]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chiTietHoaDon](
	[stt] [int] IDENTITY(1,1) NOT NULL,
	[maHoaDon] [char](5) NULL,
	[maHang] [char](5) NULL,
	[soLuong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[stt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoaDon](
	[maHoaDon] [char](5) NOT NULL,
	[tenDangNhap] [varchar](20) NULL,
	[tenKhachHang] [nvarchar](30) NULL,
	[ngay] [date] NULL,
	[chiecKhau] [int] NULL,
	[khachdua] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[maHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MatHang]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MatHang](
	[maHang] [char](5) NOT NULL,
	[tenHang] [nvarchar](30) NOT NULL,
	[giaVon] [int] NULL,
	[giaBan] [int] NULL,
	[soLuong] [int] NULL,
	[maNhaCungCap] [char](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[maHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[maNhaCungCap] [char](5) NOT NULL,
	[tenNhaCungCap] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[maNhaCungCap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/17/2018 7:43:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[tenDangNhap] [varchar](20) NOT NULL,
	[matKhau] [varchar](20) NOT NULL,
	[hoTen] [nvarchar](30) NOT NULL,
	[typeAccount] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[tenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[chiTietHoaDon] ON 

INSERT [dbo].[chiTietHoaDon] ([stt], [maHoaDon], [maHang], [soLuong]) VALUES (1, N'2    ', N'sp001', 1)
SET IDENTITY_INSERT [dbo].[chiTietHoaDon] OFF
INSERT [dbo].[HoaDon] ([maHoaDon], [tenDangNhap], [tenKhachHang], [ngay], [chiecKhau], [khachdua]) VALUES (N'00001', N'nhanvien1', N'Nguyễn Văn A', CAST(0x163E0B00 AS Date), 5000, 45000)
INSERT [dbo].[HoaDon] ([maHoaDon], [tenDangNhap], [tenKhachHang], [ngay], [chiecKhau], [khachdua]) VALUES (N'2    ', N'nhanvien1', N'abc', CAST(0x1F3E0B00 AS Date), 0, 30000)
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp001', N'Bột giặt', 20000, 23000, 50, N'n1   ')
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp002', N'Sữa tắm', 29000, 37000, 30, N'n1   ')
INSERT [dbo].[MatHang] ([maHang], [tenHang], [giaVon], [giaBan], [soLuong], [maNhaCungCap]) VALUES (N'sp003', N'dầu gội', 30000, 35000, 40, N'n1   ')
INSERT [dbo].[NhaCungCap] ([maNhaCungCap], [tenNhaCungCap]) VALUES (N'n1   ', N'ÔMÔ')
INSERT [dbo].[Users] ([tenDangNhap], [matKhau], [hoTen], [typeAccount]) VALUES (N'nhanvien1', N'12345', N'Thằng nhân viên', 0)
ALTER TABLE [dbo].[chiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([maHang])
REFERENCES [dbo].[MatHang] ([maHang])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[chiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([maHoaDon])
REFERENCES [dbo].[HoaDon] ([maHoaDon])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([tenDangNhap])
REFERENCES [dbo].[Users] ([tenDangNhap])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MatHang]  WITH CHECK ADD FOREIGN KEY([maNhaCungCap])
REFERENCES [dbo].[NhaCungCap] ([maNhaCungCap])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [shopMeVaBe] SET  READ_WRITE 
GO
