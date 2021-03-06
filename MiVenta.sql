USE [master]
GO
/****** Object:  Database [MiVenta]    Script Date: 12/06/2022 03:53:49 p. m. ******/
CREATE DATABASE [MiVenta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiVenta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MiVenta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MiVenta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MiVenta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MiVenta] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiVenta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiVenta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiVenta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiVenta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiVenta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiVenta] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiVenta] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MiVenta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiVenta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiVenta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiVenta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiVenta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiVenta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiVenta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiVenta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiVenta] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MiVenta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiVenta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiVenta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiVenta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiVenta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiVenta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiVenta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiVenta] SET RECOVERY FULL 
GO
ALTER DATABASE [MiVenta] SET  MULTI_USER 
GO
ALTER DATABASE [MiVenta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiVenta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiVenta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiVenta] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MiVenta] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MiVenta] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MiVenta', N'ON'
GO
ALTER DATABASE [MiVenta] SET QUERY_STORE = OFF
GO
USE [MiVenta]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[concepto]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[concepto](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_venta] [bigint] NOT NULL,
	[cantidad] [int] NOT NULL,
	[importe] [decimal](16, 2) NOT NULL,
	[precioUnitario] [decimal](16, 2) NOT NULL,
	[id_producto] [int] NOT NULL,
 CONSTRAINT [PK_concepto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[direccion]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[direccion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](50) NOT NULL,
	[colonia] [varchar](80) NOT NULL,
	[calle] [varchar](50) NOT NULL,
	[numero] [int] NOT NULL,
 CONSTRAINT [PK_direccion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[producto]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[precioUnitario] [decimal](16, 2) NOT NULL,
	[existencia] [int] NULL,
	[url] [varchar](150) NULL,
 CONSTRAINT [PK_producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rol]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rol](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](10) NOT NULL,
 CONSTRAINT [PK_rol] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](120) NOT NULL,
	[password] [varchar](256) NOT NULL,
	[id_rol] [int] NULL,
	[nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[venta]    Script Date: 12/06/2022 03:53:50 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venta](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[total] [decimal](16, 2) NOT NULL,
	[id_usuario] [int] NOT NULL,
	[id_direccion] [int] NOT NULL,
	[entrega] [bit] NOT NULL,
	[id_cliente] [int] NOT NULL,
 CONSTRAINT [PK_dbo.venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([id], [nombre]) VALUES (1, N'tonini')
INSERT [dbo].[cliente] ([id], [nombre]) VALUES (2, N'tortu')
INSERT [dbo].[cliente] ([id], [nombre]) VALUES (4, N'mytortu')
INSERT [dbo].[cliente] ([id], [nombre]) VALUES (5, N'string')
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[concepto] ON 

INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (1, 5, 0, CAST(75.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (2, 6, 1, CAST(75.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (4, 12, 2, CAST(150.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (5, 13, 1, CAST(100.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (6, 13, 1, CAST(200.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (7, 13, 1, CAST(500.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (8, 16, 1, CAST(500.00 AS Decimal(16, 2)), CAST(56.60 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (9, 17, 1, CAST(500.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (10, 18, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (11, 18, 2, CAST(1000.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (12, 19, 1, CAST(100.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (13, 21, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (14, 21, 2, CAST(1000.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (15, 22, 5, CAST(1000.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (16, 23, 3, CAST(600.00 AS Decimal(16, 2)), CAST(150.00 AS Decimal(16, 2)), 5)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (17, 27, 1, CAST(600.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (18, 28, 2, CAST(1000.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (19, 29, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (20, 29, 2, CAST(1000.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (21, 30, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (22, 30, 2, CAST(1000.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (23, 31, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (24, 31, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (25, 32, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (26, 32, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (27, 33, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (28, 33, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(75.00 AS Decimal(16, 2)), 2)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (29, 34, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (30, 35, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (31, 36, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (32, 36, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(60.00 AS Decimal(16, 2)), 4)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (33, 37, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(56.50 AS Decimal(16, 2)), 1)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (34, 38, 1, CAST(1000.00 AS Decimal(16, 2)), CAST(15.00 AS Decimal(16, 2)), 9)
INSERT [dbo].[concepto] ([id], [id_venta], [cantidad], [importe], [precioUnitario], [id_producto]) VALUES (35, 43, 1, CAST(15.00 AS Decimal(16, 2)), CAST(15.00 AS Decimal(16, 2)), 9)
SET IDENTITY_INSERT [dbo].[concepto] OFF
GO
SET IDENTITY_INSERT [dbo].[direccion] ON 

INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (1, N'Nuevo León', N'Buena Vista', N'Castaño', 924)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (5, N'Nuevo León', N'San Miguel', N'Malandro', 287)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (6, N'Nuevo León', N'San Francisco', N'Codorniz', 190)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (7, N'Nuevo León', N'Buena Vista', N'Cerecino', 675)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (8, N'Nuevo León', N'San Miguel', N'Fracia', 432)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (9, N'buena vista', N'san miguel', N'saltillo', 679)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (10, N'buena vista', N'san miguel', N'saltillo', 679)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (11, N'buena vista', N'san miguel', N'saltillo', 679)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (12, N'nuevo leon', N'san miguel', N'frencia', 541)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (13, N'nuevo leon', N'buena vista', N'magnolia', 678)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (14, N'Nuevo León', N'AlianzaReal', N'AlianzaMini', 400)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (15, N'nuevo leon', N'alianza', N'alianzatres', 111)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (16, N'Nuevo León', N'Buena vista', N'Javier xhatl', 100)
INSERT [dbo].[direccion] ([id], [estado], [colonia], [calle], [numero]) VALUES (21, N'Nuevo León', N'Alianza Real', N'cerezo', 6901)
SET IDENTITY_INSERT [dbo].[direccion] OFF
GO
SET IDENTITY_INSERT [dbo].[producto] ON 

INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (1, N'botella de café', CAST(56.50 AS Decimal(16, 2)), 1, N'https://d50xhnwqnrbqk.cloudfront.net/images/products/large/7501058620101_1.jpg')
INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (2, N'paquete de chokis mix', CAST(75.00 AS Decimal(16, 2)), 1, N'https://cdn.shopify.com/s/files/1/0706/6309/products/7500478008957-00-CH1200Wx1200H_530x@2x.jpg?v=1611267443')
INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (4, N'Jumex 1.5L', CAST(60.00 AS Decimal(16, 2)), 1, N'https://cdn.shopify.com/s/files/1/0272/3269/8448/products/JugoJumexdurazno_720x.jpg?v=1605916713')
INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (5, N'Nesquik 1000g', CAST(150.00 AS Decimal(16, 2)), 0, N'https://m.media-amazon.com/images/I/81kXXqzvq1L._SL1500_.jpg')
INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (7, N'Cereal FrootLoops', CAST(40.50 AS Decimal(16, 2)), 1, N'https://www.laranitadelapaz.com.mx/images/thumbs/0007205_cereal-froot-loops-individual-kelloggs-50-cajitas-de-25-g-ieps-inc-fl_510.jpeg')
INSERT [dbo].[producto] ([id], [nombre], [precioUnitario], [existencia], [url]) VALUES (9, N'Barra Carlos V', CAST(15.00 AS Decimal(16, 2)), 1, N'http://papeporfirios.weebly.com/uploads/3/1/3/9/31392783/s957834755538123056_p2_i1_w460.jpeg')
SET IDENTITY_INSERT [dbo].[producto] OFF
GO
SET IDENTITY_INSERT [dbo].[rol] ON 

INSERT [dbo].[rol] ([id], [nombre]) VALUES (5, N'admin')
INSERT [dbo].[rol] ([id], [nombre]) VALUES (6, N'normal')
SET IDENTITY_INSERT [dbo].[rol] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (1, N'tony@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', NULL, N'tony')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (2, N'tortu@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', NULL, N'unaTortuga')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (3, N'gato@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 6, N'gatito')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (4, N'cafe@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 6, N'cafe')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (5, N'ismael@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 6, N'ismael')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (8, N'eloisa@gmail.com', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 6, N'eloisinaa')
INSERT [dbo].[usuario] ([id], [email], [password], [id_rol], [nombre]) VALUES (11, N'tito@gmail.com', N'5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5', 5, N'tito')
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
SET IDENTITY_INSERT [dbo].[venta] ON 

INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (5, CAST(N'2021-10-05T05:58:34.310' AS DateTime), CAST(75.00 AS Decimal(16, 2)), 11, 1, 0, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (6, CAST(N'2021-10-05T06:00:47.950' AS DateTime), CAST(75.00 AS Decimal(16, 2)), 11, 5, 0, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (12, CAST(N'2021-10-07T21:13:04.410' AS DateTime), CAST(113.00 AS Decimal(16, 2)), 4, 1, 0, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (13, CAST(N'2021-10-08T12:47:20.077' AS DateTime), CAST(191.50 AS Decimal(16, 2)), 3, 1, 0, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (16, CAST(N'2021-10-16T18:54:27.057' AS DateTime), CAST(56.60 AS Decimal(16, 2)), 1, 1, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (17, CAST(N'2021-10-16T19:03:03.403' AS DateTime), CAST(60.00 AS Decimal(16, 2)), 1, 5, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (18, CAST(N'2021-10-16T19:08:31.127' AS DateTime), CAST(206.50 AS Decimal(16, 2)), 5, 5, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (19, CAST(N'2021-10-17T14:02:33.713' AS DateTime), CAST(56.50 AS Decimal(16, 2)), 5, 5, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (21, CAST(N'2021-10-17T20:43:44.500' AS DateTime), CAST(176.50 AS Decimal(16, 2)), 5, 5, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (22, CAST(N'2021-10-17T20:45:20.010' AS DateTime), CAST(300.00 AS Decimal(16, 2)), 5, 1, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (23, CAST(N'2021-10-26T15:58:52.680' AS DateTime), CAST(450.00 AS Decimal(16, 2)), 3, 1, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (27, CAST(N'2021-10-26T16:12:11.993' AS DateTime), CAST(75.00 AS Decimal(16, 2)), 1, 5, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (28, CAST(N'2021-10-29T02:47:02.560' AS DateTime), CAST(120.00 AS Decimal(16, 2)), 5, 6, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (29, CAST(N'2021-10-29T15:00:08.303' AS DateTime), CAST(206.50 AS Decimal(16, 2)), 5, 7, 0, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (30, CAST(N'2021-10-29T15:02:39.337' AS DateTime), CAST(176.50 AS Decimal(16, 2)), 5, 8, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (31, CAST(N'2021-10-29T15:06:04.077' AS DateTime), CAST(131.50 AS Decimal(16, 2)), 5, 9, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (32, CAST(N'2021-10-29T15:06:37.893' AS DateTime), CAST(131.50 AS Decimal(16, 2)), 5, 10, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (33, CAST(N'2021-10-29T15:06:46.183' AS DateTime), CAST(131.50 AS Decimal(16, 2)), 5, 11, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (34, CAST(N'2021-10-29T15:08:26.983' AS DateTime), CAST(56.50 AS Decimal(16, 2)), 5, 12, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (35, CAST(N'2021-10-29T15:12:12.017' AS DateTime), CAST(56.50 AS Decimal(16, 2)), 5, 13, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (36, CAST(N'2021-10-29T15:24:14.740' AS DateTime), CAST(116.50 AS Decimal(16, 2)), 5, 14, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (37, CAST(N'2021-10-29T16:57:06.180' AS DateTime), CAST(56.50 AS Decimal(16, 2)), 8, 15, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (38, CAST(N'2021-11-04T08:18:29.430' AS DateTime), CAST(15.00 AS Decimal(16, 2)), 8, 16, 1, 1)
INSERT [dbo].[venta] ([id], [fecha], [total], [id_usuario], [id_direccion], [entrega], [id_cliente]) VALUES (43, CAST(N'2021-11-07T21:50:39.007' AS DateTime), CAST(15.00 AS Decimal(16, 2)), 8, 21, 0, 1)
SET IDENTITY_INSERT [dbo].[venta] OFF
GO
ALTER TABLE [dbo].[concepto]  WITH CHECK ADD  CONSTRAINT [FK_concepto_producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[producto] ([id])
GO
ALTER TABLE [dbo].[concepto] CHECK CONSTRAINT [FK_concepto_producto]
GO
ALTER TABLE [dbo].[concepto]  WITH CHECK ADD  CONSTRAINT [FK_concepto_venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[venta] ([id])
GO
ALTER TABLE [dbo].[concepto] CHECK CONSTRAINT [FK_concepto_venta]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_rol] FOREIGN KEY([id_rol])
REFERENCES [dbo].[rol] ([id])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_rol]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[cliente] ([id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_cliente]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_direccion1] FOREIGN KEY([id_direccion])
REFERENCES [dbo].[direccion] ([id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_direccion1]
GO
ALTER TABLE [dbo].[venta]  WITH CHECK ADD  CONSTRAINT [FK_venta_usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([id])
GO
ALTER TABLE [dbo].[venta] CHECK CONSTRAINT [FK_venta_usuario]
GO
USE [master]
GO
ALTER DATABASE [MiVenta] SET  READ_WRITE 
GO
