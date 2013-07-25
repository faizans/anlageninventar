USE [master]
GO
/****** Object:  Database [IP3AnlagenInventar]    Script Date: 25.07.2013 15:55:47 ******/
CREATE DATABASE [IP3AnlagenInventar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IP3AnlagenInventar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\IP3AnlagenInventar.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'IP3AnlagenInventar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\IP3AnlagenInventar_log.ldf' , SIZE = 2048KB , MAXSIZE = 1024000KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [IP3AnlagenInventar] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IP3AnlagenInventar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IP3AnlagenInventar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET ARITHABORT OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [IP3AnlagenInventar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IP3AnlagenInventar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IP3AnlagenInventar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET  DISABLE_BROKER 
GO
ALTER DATABASE [IP3AnlagenInventar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IP3AnlagenInventar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET RECOVERY FULL 
GO
ALTER DATABASE [IP3AnlagenInventar] SET  MULTI_USER 
GO
ALTER DATABASE [IP3AnlagenInventar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IP3AnlagenInventar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IP3AnlagenInventar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IP3AnlagenInventar] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'IP3AnlagenInventar', N'ON'
GO
USE [IP3AnlagenInventar]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppUser](
	[AppUserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Domain] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[Password] [varchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[IsAdAccount] [bit] NOT NULL,
 CONSTRAINT [PK_AppUSer] PRIMARY KEY CLUSTERED 
(
	[AppUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AppUser2Room]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser2Room](
	[AppUserId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
 CONSTRAINT [PK_AppUser2Room] PRIMARY KEY CLUSTERED 
(
	[AppUserId] ASC,
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Article]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [float] NULL,
	[Amount] [int] NULL,
	[Barcode] [varchar](50) NULL,
	[OldBarcode] [varchar](50) NULL,
	[ArticleGroupId] [int] NULL,
	[SupplierBranchId] [int] NULL,
	[RoomId] [int] NULL,
	[AcquisitionDate] [datetime] NULL,
	[DepreciationId] [int] NULL,
	[ArticleCategoryId] [int] NULL,
	[InsuranceCategoryId] [int] NULL,
	[Comment] [varchar](2000) NULL,
	[IsAvailable] [bit] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ArticleCategory]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArticleCategory](
	[ArticleCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_ArticleCategory] PRIMARY KEY CLUSTERED 
(
	[ArticleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ArticleGroup]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArticleGroup](
	[ArticleGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[RoomId] [int] NOT NULL,
	[Barcode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ArticleGroup] PRIMARY KEY CLUSTERED 
(
	[ArticleGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Building]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Building](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_Building] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CompanyData]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CompanyData](
	[Title] [varchar](100) NULL,
	[Address] [varchar](50) NULL,
	[PostalCode] [varchar](50) NULL,
	[Place] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Depreciation]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Depreciation](
	[DepreciationId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [float] NULL,
	[Year] [int] NULL,
	[AdditionalStartDate] [datetime] NULL,
	[AdditionalEndDate] [datetime] NULL,
	[DepreciationCategoryId] [int] NULL,
	[ArticleId] [int] NULL,
 CONSTRAINT [PK_Depreciation] PRIMARY KEY CLUSTERED 
(
	[DepreciationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepreciationCategory]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepreciationCategory](
	[DepreciationCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NULL,
	[DepreciationSpan] [float] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[DepreciationCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Floor]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Floor](
	[FloorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[BuildingId] [int] NOT NULL,
 CONSTRAINT [PK_Floor] PRIMARY KEY CLUSTERED 
(
	[FloorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InsuranceCategory]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InsuranceCategory](
	[InsuranceCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
 CONSTRAINT [PK_InsuranceCategory] PRIMARY KEY CLUSTERED 
(
	[InsuranceCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Room]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FloorId] [int] NOT NULL,
	[Code] [varchar](50) NULL,
	[ResponsiblePerson] [varchar](100) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SupplierBranch]    Script Date: 25.07.2013 15:55:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SupplierBranch](
	[SupplierBranchId] [int] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](50) NULL,
	[Place] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Comment] [varchar](2000) NULL,
 CONSTRAINT [PK_SupplierBranch] PRIMARY KEY CLUSTERED 
(
	[SupplierBranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[AppUser] ADD  CONSTRAINT [DF_AppUSer_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[AppUser] ADD  CONSTRAINT [DF_AppUser_IsAdAccount]  DEFAULT ((0)) FOR [IsAdAccount]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_IsAvailable]  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AppUser2Room]  WITH CHECK ADD  CONSTRAINT [FK_AppUser2Room_AppUser] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUser] ([AppUserId])
GO
ALTER TABLE [dbo].[AppUser2Room] CHECK CONSTRAINT [FK_AppUser2Room_AppUser]
GO
ALTER TABLE [dbo].[AppUser2Room]  WITH CHECK ADD  CONSTRAINT [FK_AppUser2Room_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[AppUser2Room] CHECK CONSTRAINT [FK_AppUser2Room_Room]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ArticleCategory] FOREIGN KEY([ArticleCategoryId])
REFERENCES [dbo].[ArticleCategory] ([ArticleCategoryId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_ArticleCategory]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_ArticleGroup] FOREIGN KEY([ArticleGroupId])
REFERENCES [dbo].[ArticleGroup] ([ArticleGroupId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_ArticleGroup]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Depreciation] FOREIGN KEY([DepreciationId])
REFERENCES [dbo].[Depreciation] ([DepreciationId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Depreciation]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_InsuranceCategory] FOREIGN KEY([InsuranceCategoryId])
REFERENCES [dbo].[InsuranceCategory] ([InsuranceCategoryId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_InsuranceCategory]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_Room]
GO
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_SupplierBranch] FOREIGN KEY([SupplierBranchId])
REFERENCES [dbo].[SupplierBranch] ([SupplierBranchId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_SupplierBranch]
GO
ALTER TABLE [dbo].[ArticleGroup]  WITH CHECK ADD  CONSTRAINT [FK_ArticleGroup_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[ArticleGroup] CHECK CONSTRAINT [FK_ArticleGroup_Room]
GO
ALTER TABLE [dbo].[Depreciation]  WITH CHECK ADD  CONSTRAINT [FK_Depreciation_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([ArticleId])
GO
ALTER TABLE [dbo].[Depreciation] CHECK CONSTRAINT [FK_Depreciation_Article]
GO
ALTER TABLE [dbo].[Depreciation]  WITH CHECK ADD  CONSTRAINT [FK_Depreciation_DepreciationCategory] FOREIGN KEY([DepreciationCategoryId])
REFERENCES [dbo].[DepreciationCategory] ([DepreciationCategoryId])
GO
ALTER TABLE [dbo].[Depreciation] CHECK CONSTRAINT [FK_Depreciation_DepreciationCategory]
GO
ALTER TABLE [dbo].[Floor]  WITH CHECK ADD  CONSTRAINT [FK_Floor_Building] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Building] ([BuildingId])
GO
ALTER TABLE [dbo].[Floor] CHECK CONSTRAINT [FK_Floor_Building]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Floor] FOREIGN KEY([FloorId])
REFERENCES [dbo].[Floor] ([FloorId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Floor]
GO
ALTER TABLE [dbo].[SupplierBranch]  WITH CHECK ADD  CONSTRAINT [FK_SupplierBranch_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[SupplierBranch] CHECK CONSTRAINT [FK_SupplierBranch_Supplier]
GO
USE [master]
GO
ALTER DATABASE [IP3AnlagenInventar] SET  READ_WRITE 
GO
