USE [PVC]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 14/10/2020 18:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [varchar](32) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NULL,
	[PhoneNumber] [varchar](14) NULL,
	[Adress] [varchar](200) NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 14/10/2020 18:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [varchar](32) NOT NULL,
	[ClientId] [varchar](32) NOT NULL,
	[UserId] [varchar](32) NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 14/10/2020 18:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [varchar](32) NOT NULL,
	[OrderId] [varchar](32) NOT NULL,
	[ProductId] [varchar](32) NOT NULL,
	[SellValue] [numeric](8, 2) NULL,
	[Quantity] [numeric](18, 0) NULL,
	[TotalAmount] [numeric](8, 2) NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 14/10/2020 18:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [varchar](32) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[SellValue] [numeric](8, 2) NOT NULL,
	[Stock] [varchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14/10/2020 18:03:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [varchar](32) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Login] [varchar](20) NOT NULL,
	[PasswordHash] [varchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
