CREATE DATABASE [band_tracker]
GO
USE [band_tracker]
GO
/****** Object:  Table [dbo].[bands]    Script Date: 12/16/2016 4:31:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bands_venues]    Script Date: 12/16/2016 4:31:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bands_venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[band_id] [int] NULL,
	[venue_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[venues]    Script Date: 12/16/2016 4:31:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[venues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[location] [varchar](255) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[bands] ON 

INSERT [dbo].[bands] ([id], [name], [description]) VALUES (1, N'nano.RIPE', N'A Japanese pop rock band with two core members.')
INSERT [dbo].[bands] ([id], [name], [description]) VALUES (2, N'Ling Tosite Sigure', N'A Japanese rock trio whose style resembles post-hardcore and progressive rock.')
INSERT [dbo].[bands] ([id], [name], [description]) VALUES (3, N'Yasuda Rei', N'A half-Japanese J-pop singer, also known as Rachel Rhodes.')
SET IDENTITY_INSERT [dbo].[bands] OFF
SET IDENTITY_INSERT [dbo].[bands_venues] ON 

INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (3, 1, 1)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (4, 2, 2)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (5, 1, 3)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (6, 3, 6)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (7, 3, 4)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (8, 3, 2)
INSERT [dbo].[bands_venues] ([id], [band_id], [venue_id]) VALUES (10, 1, 2)
SET IDENTITY_INSERT [dbo].[bands_venues] OFF
SET IDENTITY_INSERT [dbo].[venues] ON 

INSERT [dbo].[venues] ([id], [name], [location]) VALUES (1, N'Staples Center', N'Los Angeles, CA')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (2, N'Moda Center', N'Portland, OR')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (3, N'Budokan', N'Tokyo, Japan')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (4, N'Tokyo Dome', N'Tokyo, Japan')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (5, N'Aladdin Theater', N'Portland, Or')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (6, N'Microsoft Theater', N'Los Angeles, CA')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (7, N'Holocene', N'Portland, OR')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (8, N'The Know', N'Portland, OR')
INSERT [dbo].[venues] ([id], [name], [location]) VALUES (9, N'Edgefield', N'Portland, OR')
SET IDENTITY_INSERT [dbo].[venues] OFF
