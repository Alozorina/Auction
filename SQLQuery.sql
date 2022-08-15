USE [AuctionDb]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemCategories]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_ItemCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemPhoto]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemPhoto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[ItemId] [int] NOT NULL,
 CONSTRAINT [PK_ItemPhoto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedBy] [nvarchar](80) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[StartingPrice] [decimal](18, 2) NOT NULL,
	[CurrentBid] [decimal](18, 2) NOT NULL,
	[StatusId] [int] NOT NULL,
	[StartSaleDate] [datetime2](7) NOT NULL,
	[EndSaleDate] [datetime2](7) NOT NULL,
	[OwnerId] [int] NOT NULL,
	[BuyerId] [int] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12.08.2022 17:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[RoleId] [int] NOT NULL,
	[BirthDate] [datetime2](7) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Painting')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Steve Johnson')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Sculpture')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Ceramics')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Chinese Art')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (6, N'Porcelain')
GO
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (7, N'Jewelry')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemCategories] ON 
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (2, 2, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (3, 2, 2)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (4, 1, 2)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (5, 3, 2)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (6, 4, 2)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (7, 5, 2)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (8, 2, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (9, 1, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (10, 3, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (11, 4, 1)
GO
INSERT [dbo].[ItemCategories] ([Id], [ItemId], [CategoryId]) VALUES (12, 5, 1)
GO
SET IDENTITY_INSERT [dbo].[ItemCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemPhoto] ON 
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (1, N'steve-johnson-unsplash.jpg', 1)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (2, N'pexels-steve-johnson-1840624.jpg', 2)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (3, N'pexels-steve-johnson-1174000.jpg', 3)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (4, N'pexels-steve-johnson-1286632.jpg', 4)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (5, N'steve-johnson-RzykwoNjoLw-unsplash.jpg', 5)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (6, N'steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg', 5)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (7, N'pexels-jesse-zheng-732548.jpg', 6)
GO
INSERT [dbo].[ItemPhoto] ([Id], [Path], [ItemId]) VALUES (8, N'pawel-czerwinski-xubOAAKUwXc-unsplash.jpg', 7)
GO
SET IDENTITY_INSERT [dbo].[ItemPhoto] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (1, N'Blue Marble', N'Steve Johnson', N'Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson''s Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.', CAST(50.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, CAST(N'2022-07-22T12:00:00.0000000' AS DateTime2), CAST(N'2022-08-15T22:00:00.0000000' AS DateTime2), 1, NULL)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (2, N'Revolution', N'Steve Johnson', N'Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson''s Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.', CAST(60.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, CAST(N'2022-08-12T10:00:00.0000000' AS DateTime2), CAST(N'2022-08-15T12:00:00.0000000' AS DateTime2), 2, NULL)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (3, N'Sunset', N'Steve Johnson', N'Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson''s Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.', CAST(30.00 AS Decimal(18, 2)), CAST(40.00 AS Decimal(18, 2)), 4, CAST(N'2022-07-09T10:00:00.0000000' AS DateTime2), CAST(N'2022-08-15T12:00:00.0000000' AS DateTime2), 2, NULL)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (4, N'Spinning Around', N'Steve Johnson', N'Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson''s Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.', CAST(5.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 5, CAST(N'2022-06-14T10:00:00.0000000' AS DateTime2), CAST(N'2022-08-03T12:00:00.0000000' AS DateTime2), 6, 11)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (5, N'Antarctica Is Changing', N'Steve Johnson', N'Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson''s Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.', CAST(60.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)), 5, CAST(N'2022-07-09T10:00:00.0000000' AS DateTime2), CAST(N'2022-08-10T12:00:00.0000000' AS DateTime2), 6, NULL)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (6, N'Green Hills', N'Jesse Zheng', NULL, CAST(60.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)), 5, CAST(N'2022-07-09T10:00:00.0000000' AS DateTime2), CAST(N'2022-08-10T12:00:00.0000000' AS DateTime2), 6, NULL)
GO
INSERT [dbo].[Items] ([Id], [Name], [CreatedBy], [Description], [StartingPrice], [CurrentBid], [StatusId], [StartSaleDate], [EndSaleDate], [OwnerId], [BuyerId]) VALUES (7, N'Black Gold', N'Pawel Czerwinski', NULL, CAST(60.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 4, CAST(N'2022-08-09T11:00:00.0000000' AS DateTime2), CAST(N'2022-08-19T12:00:00.0000000' AS DateTime2), 11, NULL)
GO
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'User')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (1, N'On Approval')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (2, N'Upcoming')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (3, N'Rejected')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (4, N'Open')
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (5, N'Closed')
GO
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [RoleId], [BirthDate]) VALUES (1, N'Jane', N'Doe', N'janemail@mail.com', N'passwordJane', 2, CAST(N'2000-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [RoleId], [BirthDate]) VALUES (2, N'John', N'Doe', N'johnmail@mail.com', N'passwordJohn', 1, CAST(N'2000-02-02T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [RoleId], [BirthDate]) VALUES (6, N'Peter', N'Choi', N'peter@mail.com', N'password123', 1, CAST(N'1980-02-04T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [RoleId], [BirthDate]) VALUES (11, N'Dana', N'Meng', N'dana@mail.com', N'password123', 1, CAST(N'1997-08-02T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[ItemCategories]  WITH CHECK ADD  CONSTRAINT [FK_ItemCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ItemCategories] CHECK CONSTRAINT [FK_ItemCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ItemCategories]  WITH CHECK ADD  CONSTRAINT [FK_ItemCategories_Items_ItemId] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ItemCategories] CHECK CONSTRAINT [FK_ItemCategories_Items_ItemId]
GO
ALTER TABLE [dbo].[ItemPhoto]  WITH CHECK ADD  CONSTRAINT [FK_ItemPhoto_Items_ItemId] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ItemPhoto] CHECK CONSTRAINT [FK_ItemPhoto_Items_ItemId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Statuses_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Statuses_StatusId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_User_BuyerId] FOREIGN KEY([BuyerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_User_BuyerId]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_User_OwnerId] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_User_OwnerId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Roles_RoleId]
GO
