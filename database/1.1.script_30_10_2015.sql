USE [master]
GO
/****** Object:  Database [TraoDoiDoCu]    Script Date: 10/30/2015 10:20:59 AM ******/
CREATE DATABASE [TraoDoiDoCu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TraoDoiDoCu', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TraoDoiDoCu.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TraoDoiDoCu_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\TraoDoiDoCu_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TraoDoiDoCu] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TraoDoiDoCu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TraoDoiDoCu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET ARITHABORT OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TraoDoiDoCu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TraoDoiDoCu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TraoDoiDoCu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TraoDoiDoCu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TraoDoiDoCu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TraoDoiDoCu] SET  MULTI_USER 
GO
ALTER DATABASE [TraoDoiDoCu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TraoDoiDoCu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TraoDoiDoCu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TraoDoiDoCu] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TraoDoiDoCu]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/30/2015 10:20:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[PictureCategory] [image] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 10/30/2015 10:20:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CategoryID] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Picture] [nvarchar](500) NULL,
	[PostingDate] [date] NOT NULL,
	[Available] [bit] NULL,
	[UserID] [int] NOT NULL,
	[Location] [nvarchar](500) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/30/2015 10:20:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](500) NOT NULL,
	[PassWord] [nvarchar](500) NOT NULL,
	[FirstName] [nvarchar](500) NOT NULL,
	[LastName] [nvarchar](500) NOT NULL,
	[Email] [varchar](500) NULL,
	[Admin] [bit] NOT NULL,
	[Ban] [bit] NULL,
	[Rating] [int] NULL,
	[Phone] [varchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (1, N'Di động', N'Điện thoại di động, tablet,..', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (2, N'Đồ điện tử', N'Đồ điện tử', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (3, N'Dụng cụ bếp', N'Đồ dùng trong nhà bếp', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (4, N'Đồ chơi', N'Các loại đồ chơi', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (5, N'Đồ dùng cá nhân', N'Các loại quần áo thời trang, đồ dùng cá nhân ', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (6, N'Laptop', N'Các dòng Laptop và các thiết bị laptop', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (7, N'Xe máy', N'Các loại xe máy', NULL)
INSERT [dbo].[Category] ([ID], [Name], [Description], [PictureCategory]) VALUES (8, N'Tivi', N'Các loại tivi', NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (1, N'Iphone 5', N'Iphone 5 mới 90%', 1, 5700000, NULL, CAST(0x883A0B00 AS Date), NULL, 2, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (2, N'Bếp từ', N'Bếp từ dùng trong nhà bếp', 3, 2000000, NULL, CAST(0xC0390B00 AS Date), NULL, 3, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (4, N'Bếp ga', N'Bếp ga cũ', 3, 500000, NULL, CAST(0xEF390B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (5, N'Tủ lạnh mini', N'Tủ lạnh cũ', 2, 300000, NULL, CAST(0x9F3A0B00 AS Date), NULL, 4, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (6, N'Dell vostro 3446', N'Laptop', 6, 5000000, NULL, CAST(0xC73A0B00 AS Date), NULL, 2, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (7, N'Iphone 4s', N'Điện thoại Iphone 4s', 1, 2900000, NULL, CAST(0xD6390B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (8, N'Bộ loa mini', N'Loa mini', 2, 100000, NULL, CAST(0x7F3A0B00 AS Date), NULL, 3, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (9, N'Chuột quang', N'Chuột dành cho game thủ mới 99%', 6, 200000, NULL, CAST(0x993A0B00 AS Date), NULL, 4, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (10, N'Máy quạt', N'Máy quạt để bàn', 2, 150000, NULL, CAST(0x923A0B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (11, N'Sony Xperia Z5 Dual', N'Xperia Z5 mới 99%', 1, 13000000, NULL, CAST(0x893A0B00 AS Date), NULL, 3, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (12, N'Sirius', N'Sirius Fi 2015 phun xăng điện tử chạy chưa tới 1km', 7, 18000000, NULL, CAST(0x9B3A0B00 AS Date), NULL, 4, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (13, N'M7 camera', N'Máy bay điều khiển từ xa', 4, 500000, NULL, CAST(0x6A3A0B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (14, N'Samsung Galaxy Note 4', N'Samsung Galaxy mới 90%', 1, 7000000, NULL, CAST(0x4B3A0B00 AS Date), NULL, 4, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (15, N'Asus zenfone 5', N'asus zenfone 5 mới 99%', 1, 2000000, NULL, CAST(0x2B3A0B00 AS Date), NULL, 4, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (18, N'Asus K501LB-DM077D', N'Laptop Asus mới nguyên không trầy sướt', 6, 12500000, NULL, CAST(0xB53A0B00 AS Date), NULL, 2, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (19, N'Toshiba H10S3KS', N'Máy lạnh Toshiba chính hãng', 2, 3000000, NULL, CAST(0x96390B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (20, N'Máy tính Casino 570 plus', N'Máy tính cầm tay ', 5, 50000, NULL, CAST(0x6E390B00 AS Date), NULL, 5, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (22, N'Iphone 6s', N'Iphone 6s sách tay', 1, 13000000, NULL, CAST(0xC53A0B00 AS Date), NULL, 3, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (23, N'Tivi LCD LG 32 inch', N'Tivi màn hình phẳng 32 inch ', 8, 2000000, NULL, CAST(0x8F3A0B00 AS Date), NULL, 2, NULL)
INSERT [dbo].[Product] ([ID], [Name], [Description], [CategoryID], [Price], [Picture], [PostingDate], [Available], [UserID], [Location]) VALUES (24, N'Lumia 920', N'Điện thoại Lumia 920', 1, 3000000, NULL, CAST(0xF9390B00 AS Date), NULL, 2, NULL)
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [UserName], [PassWord], [FirstName], [LastName], [Email], [Admin], [Ban], [Rating], [Phone]) VALUES (1, N'khoa', N'123456', N'Khoa', N'Hoàng', N'hkhoa@gmail.com', 1, NULL, NULL, N'0123456789')
INSERT [dbo].[Users] ([ID], [UserName], [PassWord], [FirstName], [LastName], [Email], [Admin], [Ban], [Rating], [Phone]) VALUES (2, N'tuankiet', N'111111', N'Kiệt', N'Tuấn', N'tuankiet@gmail.com', 0, NULL, NULL, N'0123459876')
INSERT [dbo].[Users] ([ID], [UserName], [PassWord], [FirstName], [LastName], [Email], [Admin], [Ban], [Rating], [Phone]) VALUES (3, N'luongloi', N'123321', N'Lợi', N'Lương', N'luongloi@gmail.com', 0, NULL, NULL, N'0987654321')
INSERT [dbo].[Users] ([ID], [UserName], [PassWord], [FirstName], [LastName], [Email], [Admin], [Ban], [Rating], [Phone]) VALUES (4, N'vankhoa', N'123456', N'Khoa', N'Văn', N'vankhoa@gmail.com', 0, NULL, NULL, NULL)
INSERT [dbo].[Users] ([ID], [UserName], [PassWord], [FirstName], [LastName], [Email], [Admin], [Ban], [Rating], [Phone]) VALUES (5, N'mkhoi', N'123654', N'Khôi', N'Minh', N'mkhoi@gmail.com', 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Users]
GO
USE [master]
GO
ALTER DATABASE [TraoDoiDoCu] SET  READ_WRITE 
GO
