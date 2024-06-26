USE [master]
GO
/****** Object:  Database [ShoesKursovoi]    Script Date: 10.05.2024 19:35:04 ******/
CREATE DATABASE [ShoesKursovoi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoesKursovoi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ShoesKursovoi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShoesKursovoi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ShoesKursovoi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ShoesKursovoi] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoesKursovoi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoesKursovoi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoesKursovoi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoesKursovoi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShoesKursovoi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoesKursovoi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShoesKursovoi] SET  MULTI_USER 
GO
ALTER DATABASE [ShoesKursovoi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoesKursovoi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoesKursovoi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoesKursovoi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShoesKursovoi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShoesKursovoi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ShoesKursovoi] SET QUERY_STORE = OFF
GO
USE [ShoesKursovoi]
GO
/****** Object:  Table [dbo].[Departaments]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departaments](
	[IDDepartment] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departaments] PRIMARY KEY CLUSTERED 
(
	[IDDepartment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[IDEmpoyee] [int] IDENTITY(1,1) NOT NULL,
	[IDDepartment] [int] NOT NULL,
	[Post] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FIO] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[IDEmpoyee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[IdGender] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](1) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[IdGender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[IDMaterial] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountInStock] [int] NOT NULL,
	[CostWithoutNDS] [money] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[IDMaterial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialOfProduct]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialOfProduct](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Articul] [int] NOT NULL,
	[IDMaterial] [int] NOT NULL,
	[CountOfMaterial] [int] NOT NULL,
 CONSTRAINT [PK_MaterialOfProduct] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Articul] [int] IDENTITY(1000,1) NOT NULL,
	[IDType] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountInStock] [int] NOT NULL,
	[Image] [nvarchar](50) NULL,
	[IdGender] [int] NOT NULL,
	[Age] [nchar](1) NOT NULL,
	[CostWithoutNDS] [money] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Articul] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Production]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Production](
	[IDDepartment] [int] NOT NULL,
	[DateProduction] [datetime] NOT NULL,
	[CountOfWorkers] [int] NOT NULL,
	[IDProduction] [int] IDENTITY(1,1) NOT NULL,
	[SendStatus] [nvarchar](3) NOT NULL,
 CONSTRAINT [PK_Production] PRIMARY KEY CLUSTERED 
(
	[IDProduction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionContain]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionContain](
	[Articul] [int] NOT NULL,
	[CountOfProduct] [int] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDProduction] [int] NOT NULL,
 CONSTRAINT [PK_ProductionContain] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shops]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shops](
	[IDShop] [int] IDENTITY(1,1) NOT NULL,
	[Adress] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Shops] PRIMARY KEY CLUSTERED 
(
	[IDShop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesMaterialsInMaterialStock]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesMaterialsInMaterialStock](
	[IDSupply] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_SuppliesMaterialsInMaterialStock] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesMaterialsInMaterialStockContains]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesMaterialsInMaterialStockContains](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDSupply] [int] NOT NULL,
	[IDMaterial] [int] NOT NULL,
	[CountOfMaterial] [int] NOT NULL,
 CONSTRAINT [PK_SuppliesMaterialsInMaterialStockContains] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesProductsInProductStock]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesProductsInProductStock](
	[IDSupply] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_SuppliesProductsInProductStock] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesProductsInProductStockContains]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesProductsInProductStockContains](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDSupply] [int] NOT NULL,
	[ShoeArticul] [int] NOT NULL,
	[CountOfShoe] [int] NOT NULL,
 CONSTRAINT [PK_SuppliesProductsInProductStockContains] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesProductsInShops]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesProductsInShops](
	[IDSupply] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[IDShop] [int] NOT NULL,
 CONSTRAINT [PK_SuppliesProductsInShops] PRIMARY KEY CLUSTERED 
(
	[IDSupply] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesProductsInShopsContains]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesProductsInShopsContains](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDSupply] [int] NOT NULL,
	[ShoeArticul] [int] NOT NULL,
	[CountOfShoe] [int] NOT NULL,
 CONSTRAINT [PK_SuppliesProductsInShopsContains] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfProduct]    Script Date: 10.05.2024 19:35:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfProduct](
	[IDType] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TypeOfProduct] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departaments] ON 

INSERT [dbo].[Departaments] ([IDDepartment], [Address]) VALUES (1, N'ул. Пушкина, Колотушкина')
INSERT [dbo].[Departaments] ([IDDepartment], [Address]) VALUES (2, N'ул. Машиностроителей, 40')
SET IDENTITY_INSERT [dbo].[Departaments] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([IDEmpoyee], [IDDepartment], [Post], [Login], [Password], [FIO]) VALUES (1, 1, N'бухгалтер склада ГП', N'umnik122', N'123456', N'Мучкин Владимир Дмитриевич')
INSERT [dbo].[Employees] ([IDEmpoyee], [IDDepartment], [Post], [Login], [Password], [FIO]) VALUES (3, 1, N'бухгалтер склада материалов', N'mewmew5', N'123456', N'Зуев Андрей Андреевич')
INSERT [dbo].[Employees] ([IDEmpoyee], [IDDepartment], [Post], [Login], [Password], [FIO]) VALUES (4, 2, N'администратор', N'admin2', N'admin2', N'Масков Илья Дмитриевич')
INSERT [dbo].[Employees] ([IDEmpoyee], [IDDepartment], [Post], [Login], [Password], [FIO]) VALUES (7, 2, N'бухгалтер на производстве', N'proizv', N'123456', N'Ежиков Ежик Ежикович')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([IdGender], [Name]) VALUES (1, N'ж')
INSERT [dbo].[Gender] ([IdGender], [Name]) VALUES (2, N'м')
INSERT [dbo].[Gender] ([IdGender], [Name]) VALUES (3, N'в')
SET IDENTITY_INSERT [dbo].[Gender] OFF
GO
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([IDMaterial], [Name], [CountInStock], [CostWithoutNDS]) VALUES (1, N'Кожа', 1, 400.0000)
INSERT [dbo].[Material] ([IDMaterial], [Name], [CountInStock], [CostWithoutNDS]) VALUES (2, N'Замша', 50, 200.0000)
INSERT [dbo].[Material] ([IDMaterial], [Name], [CountInStock], [CostWithoutNDS]) VALUES (5, N'Резина', 130, 500.0000)
INSERT [dbo].[Material] ([IDMaterial], [Name], [CountInStock], [CostWithoutNDS]) VALUES (6, N'Пластик', 101, 300.0000)
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialOfProduct] ON 

INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (8, 1001, 1, 11)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (12, 1016, 1, 12)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (16, 1000, 1, 50)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (17, 1000, 5, 20)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (18, 1001, 2, 50)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (19, 1001, 5, 10)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (20, 1003, 1, 100)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (22, 1002, 1, 10)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (23, 1016, 5, 50)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (24, 1017, 1, 10)
INSERT [dbo].[MaterialOfProduct] ([ID], [Articul], [IDMaterial], [CountOfMaterial]) VALUES (25, 1017, 5, 5)
SET IDENTITY_INSERT [dbo].[MaterialOfProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1000, 1, N'Кожаные кроссовки-чулки', 70, N'imgs/photo_2024-05-07_16-10-40.jpg', 1, N'б', 3000.0000)
INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1001, 1, N'Кроссовки GEOBASKET', 70, N'imgs/Кроссовки GEOBASKET.jpg', 3, N'б', 5000.0000)
INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1002, 4, N'Туфли на прозрачном каблуке', 70, N'imgs/Туфли на прозрачном каблуке.jpg', 1, N'б', 6000.0000)
INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1003, 2, N'Кожанные кеды RAMONES', 110, N'imgs/Кожанные кеды RAMONES.jpg', 2, N'б', 3500.0000)
INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1016, 5, N'Ботинки Bunny Boots', 100, N'imgs/Bunny Boots.png', 2, N'б', 5000.0000)
INSERT [dbo].[Product] ([Articul], [IDType], [Name], [CountInStock], [Image], [IdGender], [Age], [CostWithoutNDS]) VALUES (1017, 2, N'Детские кожанные кеды', 100, N'imgs/Детские кожанные кеды.jpg', 3, N'м', 1500.0000)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Production] ON 

INSERT [dbo].[Production] ([IDDepartment], [DateProduction], [CountOfWorkers], [IDProduction], [SendStatus]) VALUES (2, CAST(N'2024-04-27T16:26:32.117' AS DateTime), 10, 1, N'да')
INSERT [dbo].[Production] ([IDDepartment], [DateProduction], [CountOfWorkers], [IDProduction], [SendStatus]) VALUES (1, CAST(N'2024-04-28T00:00:00.000' AS DateTime), 10, 2, N'да')
INSERT [dbo].[Production] ([IDDepartment], [DateProduction], [CountOfWorkers], [IDProduction], [SendStatus]) VALUES (1, CAST(N'2024-04-28T14:09:41.423' AS DateTime), 10, 3, N'да')
INSERT [dbo].[Production] ([IDDepartment], [DateProduction], [CountOfWorkers], [IDProduction], [SendStatus]) VALUES (1, CAST(N'2024-05-08T10:06:12.140' AS DateTime), 20, 7, N'да')
SET IDENTITY_INSERT [dbo].[Production] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductionContain] ON 

INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1001, 50, 1, 1)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1000, 10, 2, 1)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1002, 30, 4, 2)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1001, 30, 5, 2)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1001, 30, 6, 3)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1000, 20, 7, 7)
INSERT [dbo].[ProductionContain] ([Articul], [CountOfProduct], [ID], [IDProduction]) VALUES (1002, 10, 9, 7)
SET IDENTITY_INSERT [dbo].[ProductionContain] OFF
GO
SET IDENTITY_INSERT [dbo].[Shops] ON 

INSERT [dbo].[Shops] ([IDShop], [Adress]) VALUES (1, N'ул. Гагарина, 7')
INSERT [dbo].[Shops] ([IDShop], [Adress]) VALUES (2, N'ул. Масленникова, 13')
SET IDENTITY_INSERT [dbo].[Shops] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesMaterialsInMaterialStock] ON 

INSERT [dbo].[SuppliesMaterialsInMaterialStock] ([IDSupply], [Date]) VALUES (3, CAST(N'2024-04-16T14:44:12.463' AS DateTime))
SET IDENTITY_INSERT [dbo].[SuppliesMaterialsInMaterialStock] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesMaterialsInMaterialStockContains] ON 

INSERT [dbo].[SuppliesMaterialsInMaterialStockContains] ([ID], [IDSupply], [IDMaterial], [CountOfMaterial]) VALUES (5, 3, 1, 22)
INSERT [dbo].[SuppliesMaterialsInMaterialStockContains] ([ID], [IDSupply], [IDMaterial], [CountOfMaterial]) VALUES (12, 3, 6, 100)
SET IDENTITY_INSERT [dbo].[SuppliesMaterialsInMaterialStockContains] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesProductsInProductStock] ON 

INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (2, CAST(N'2024-04-17T15:58:15.830' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (3, CAST(N'2023-04-20T00:00:00.000' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (4, CAST(N'2024-04-27T16:26:32.117' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (5, CAST(N'2024-04-28T00:00:00.000' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (6, CAST(N'2024-04-28T00:00:00.000' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (7, CAST(N'2024-04-28T14:09:41.423' AS DateTime))
INSERT [dbo].[SuppliesProductsInProductStock] ([IDSupply], [Date]) VALUES (10, CAST(N'2024-05-08T10:06:12.140' AS DateTime))
SET IDENTITY_INSERT [dbo].[SuppliesProductsInProductStock] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesProductsInProductStockContains] ON 

INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (14, 2, 1001, 10)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (15, 2, 1003, 500)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (16, 3, 1000, 1000)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (17, 4, 1001, 50)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (18, 4, 1000, 10)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (19, 6, 1002, 30)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (20, 6, 1001, 30)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (21, 7, 1001, 30)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (24, 10, 1000, 20)
INSERT [dbo].[SuppliesProductsInProductStockContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (25, 10, 1002, 10)
SET IDENTITY_INSERT [dbo].[SuppliesProductsInProductStockContains] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesProductsInShops] ON 

INSERT [dbo].[SuppliesProductsInShops] ([IDSupply], [Date], [IDShop]) VALUES (4, CAST(N'2024-04-12T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[SuppliesProductsInShops] ([IDSupply], [Date], [IDShop]) VALUES (6, CAST(N'2024-04-20T13:53:07.450' AS DateTime), 1)
INSERT [dbo].[SuppliesProductsInShops] ([IDSupply], [Date], [IDShop]) VALUES (7, CAST(N'2024-03-01T00:00:00.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[SuppliesProductsInShops] OFF
GO
SET IDENTITY_INSERT [dbo].[SuppliesProductsInShopsContains] ON 

INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (15, 4, 1000, 50)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (26, 4, 1001, 10)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (27, 6, 1000, 10)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (28, 6, 1001, 10)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (29, 6, 1002, 10)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (30, 6, 1003, 10)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (31, 6, 1016, 50)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (32, 4, 1016, 500)
INSERT [dbo].[SuppliesProductsInShopsContains] ([ID], [IDSupply], [ShoeArticul], [CountOfShoe]) VALUES (33, 7, 1001, 1000)
SET IDENTITY_INSERT [dbo].[SuppliesProductsInShopsContains] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeOfProduct] ON 

INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (1, N'Кроссовки')
INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (2, N'Кеды')
INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (3, N'Лоферы')
INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (4, N'Туфли')
INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (5, N'Ботинки')
INSERT [dbo].[TypeOfProduct] ([IDType], [Name]) VALUES (6, N'Сандали')
SET IDENTITY_INSERT [dbo].[TypeOfProduct] OFF
GO
ALTER TABLE [dbo].[Production] ADD  CONSTRAINT [DF_Production_SendStatus]  DEFAULT (N'нет') FOR [SendStatus]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departaments] FOREIGN KEY([IDDepartment])
REFERENCES [dbo].[Departaments] ([IDDepartment])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departaments]
GO
ALTER TABLE [dbo].[MaterialOfProduct]  WITH CHECK ADD  CONSTRAINT [FK_MaterialOfProduct_Material] FOREIGN KEY([IDMaterial])
REFERENCES [dbo].[Material] ([IDMaterial])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MaterialOfProduct] CHECK CONSTRAINT [FK_MaterialOfProduct_Material]
GO
ALTER TABLE [dbo].[MaterialOfProduct]  WITH CHECK ADD  CONSTRAINT [FK_MaterialOfProduct_Product] FOREIGN KEY([Articul])
REFERENCES [dbo].[Product] ([Articul])
GO
ALTER TABLE [dbo].[MaterialOfProduct] CHECK CONSTRAINT [FK_MaterialOfProduct_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Gender] FOREIGN KEY([IdGender])
REFERENCES [dbo].[Gender] ([IdGender])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Gender]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_TypeOfProduct1] FOREIGN KEY([IDType])
REFERENCES [dbo].[TypeOfProduct] ([IDType])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_TypeOfProduct1]
GO
ALTER TABLE [dbo].[Production]  WITH CHECK ADD  CONSTRAINT [FK_Production_Departaments] FOREIGN KEY([IDDepartment])
REFERENCES [dbo].[Departaments] ([IDDepartment])
GO
ALTER TABLE [dbo].[Production] CHECK CONSTRAINT [FK_Production_Departaments]
GO
ALTER TABLE [dbo].[ProductionContain]  WITH CHECK ADD  CONSTRAINT [FK_ProductionContain_Product] FOREIGN KEY([Articul])
REFERENCES [dbo].[Product] ([Articul])
GO
ALTER TABLE [dbo].[ProductionContain] CHECK CONSTRAINT [FK_ProductionContain_Product]
GO
ALTER TABLE [dbo].[ProductionContain]  WITH CHECK ADD  CONSTRAINT [FK_ProductionContain_Production] FOREIGN KEY([IDProduction])
REFERENCES [dbo].[Production] ([IDProduction])
GO
ALTER TABLE [dbo].[ProductionContain] CHECK CONSTRAINT [FK_ProductionContain_Production]
GO
ALTER TABLE [dbo].[SuppliesMaterialsInMaterialStockContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesMaterialsInMaterialStockContains_Material] FOREIGN KEY([IDMaterial])
REFERENCES [dbo].[Material] ([IDMaterial])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesMaterialsInMaterialStockContains] CHECK CONSTRAINT [FK_SuppliesMaterialsInMaterialStockContains_Material]
GO
ALTER TABLE [dbo].[SuppliesMaterialsInMaterialStockContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesMaterialsInMaterialStockContains_SuppliesMaterialsInMaterialStock] FOREIGN KEY([IDSupply])
REFERENCES [dbo].[SuppliesMaterialsInMaterialStock] ([IDSupply])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesMaterialsInMaterialStockContains] CHECK CONSTRAINT [FK_SuppliesMaterialsInMaterialStockContains_SuppliesMaterialsInMaterialStock]
GO
ALTER TABLE [dbo].[SuppliesProductsInProductStockContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesProductsInProductStockContains_Product1] FOREIGN KEY([ShoeArticul])
REFERENCES [dbo].[Product] ([Articul])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesProductsInProductStockContains] CHECK CONSTRAINT [FK_SuppliesProductsInProductStockContains_Product1]
GO
ALTER TABLE [dbo].[SuppliesProductsInProductStockContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesProductsInProductStockContains_SuppliesProductsInProductStock] FOREIGN KEY([IDSupply])
REFERENCES [dbo].[SuppliesProductsInProductStock] ([IDSupply])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesProductsInProductStockContains] CHECK CONSTRAINT [FK_SuppliesProductsInProductStockContains_SuppliesProductsInProductStock]
GO
ALTER TABLE [dbo].[SuppliesProductsInShops]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesProductsInShops_Shops] FOREIGN KEY([IDShop])
REFERENCES [dbo].[Shops] ([IDShop])
GO
ALTER TABLE [dbo].[SuppliesProductsInShops] CHECK CONSTRAINT [FK_SuppliesProductsInShops_Shops]
GO
ALTER TABLE [dbo].[SuppliesProductsInShopsContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesProductsInShopsContains_Product] FOREIGN KEY([ShoeArticul])
REFERENCES [dbo].[Product] ([Articul])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesProductsInShopsContains] CHECK CONSTRAINT [FK_SuppliesProductsInShopsContains_Product]
GO
ALTER TABLE [dbo].[SuppliesProductsInShopsContains]  WITH CHECK ADD  CONSTRAINT [FK_SuppliesProductsInShopsContains_SuppliesProductsInShops] FOREIGN KEY([IDSupply])
REFERENCES [dbo].[SuppliesProductsInShops] ([IDSupply])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SuppliesProductsInShopsContains] CHECK CONSTRAINT [FK_SuppliesProductsInShopsContains_SuppliesProductsInShops]
GO
USE [master]
GO
ALTER DATABASE [ShoesKursovoi] SET  READ_WRITE 
GO
