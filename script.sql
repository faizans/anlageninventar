USE [IP3AnlagenInventar]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[AppUser2Room]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[Article]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Article](
	[ArticleId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Barcode] [varchar](50) NULL,
	[ArticleGroupId] [int] NULL,
	[SupplierBranchId] [int] NULL,
	[RoomId] [int] NULL,
	[AcquisitionDate] [datetime] NULL,
	[DepreciationCategoryId] [int] NOT NULL,
	[ArticleCategoryId] [int] NULL,
	[InsuranceCategoryId] [int] NULL,
	[OldBarcode] [varchar](50) NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ArticleCategory]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ArticleCategory](
	[ArticleCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ArticleCategory] PRIMARY KEY CLUSTERED 
(
	[ArticleCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ArticleGroup]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[Building]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Building](
	[BuildingId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[CompanyData]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[Depreciation]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Depreciation](
	[DepreciationId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[Value] [float] NULL,
	[Year] [int] NULL,
	[AdditionalStartDate] [datetime] NULL,
	[AdditionalEndDate] [datetime] NULL,
	[DepreciationCategoryId] [int] NULL,
 CONSTRAINT [PK_Depreciation] PRIMARY KEY CLUSTERED 
(
	[DepreciationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepreciationCategory]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[Floor]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Floor](
	[FloorId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[InsuranceCategory]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[Room]    Script Date: 23.07.2013 16:32:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FloorId] [int] NOT NULL,
	[Code] [varchar](50) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 23.07.2013 16:32:36 ******/
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
/****** Object:  Table [dbo].[SupplierBranch]    Script Date: 23.07.2013 16:32:36 ******/
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
ALTER TABLE [dbo].[Article]  WITH CHECK ADD  CONSTRAINT [FK_Article_DepreciationCategory] FOREIGN KEY([DepreciationCategoryId])
REFERENCES [dbo].[DepreciationCategory] ([DepreciationCategoryId])
GO
ALTER TABLE [dbo].[Article] CHECK CONSTRAINT [FK_Article_DepreciationCategory]
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
ALTER TABLE [dbo].[Depreciation]  WITH CHECK ADD  CONSTRAINT [FK_Depreciation_Article1] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([ArticleId])
GO
ALTER TABLE [dbo].[Depreciation] CHECK CONSTRAINT [FK_Depreciation_Article1]
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
