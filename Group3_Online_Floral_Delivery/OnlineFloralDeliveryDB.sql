USE [OnlineFloralDeliveryDB]
GO
/****** Object:  Table [dbo].[BlogCategories]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogCategories](
	[BlogCategoriesID] [int] IDENTITY(1,1) NOT NULL,
	[BlogCategoriesName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogCategoriesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogComments]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogComments](
	[BlogComentID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Content] [text] NOT NULL,
	[Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogComentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[BlogName] [nvarchar](500) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[BlogCategoriesID] [int] NOT NULL,
	[Content] [text] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Picture] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BouquetDetails]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BouquetDetails](
	[BouquetID] [int] NOT NULL,
	[FlowerID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BouquetID] ASC,
	[FlowerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BouquetImages]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BouquetImages](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[BouquetID] [int] NOT NULL,
	[ImageFileName] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bouquets]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bouquets](
	[BouquetID] [int] IDENTITY(1,1) NOT NULL,
	[BouquetName] [nvarchar](100) NOT NULL,
	[OccasionID] [int] NOT NULL,
	[SaleID] [int] NOT NULL,
	[MainImage] [nvarchar](500) NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[OldUnitPrice] [int] NOT NULL,
	[ShortDescription] [nvarchar](1000) NOT NULL,
	[Description] [text] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BouquetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[BouquetID] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Content] [text] NOT NULL,
	[CommentDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Username] [varchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [bit] NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Picture] [varchar](500) NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [bit] NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Picture] [varchar](500) NULL,
	[Role] [int] NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flowers]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flowers](
	[FlowerID] [int] IDENTITY(1,1) NOT NULL,
	[FlowerName] [nvarchar](255) NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ShortDescription] [nvarchar](1000) NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Picture] [nvarchar](500) NOT NULL,
	[Meaning] [text] NOT NULL,
	[Status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FlowerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[OccasionID] [int] NOT NULL,
	[MessageContent] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Occasions]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Occasions](
	[OccasionID] [int] IDENTITY(1,1) NOT NULL,
	[OccasionName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OccasionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PaymentID] [int] NOT NULL,
	[Message] [text] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Recipient] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[ShippingDate] [datetime] NOT NULL,
	[Note] [nvarchar](255) NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] NOT NULL,
	[BouquetID] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC,
	[BouquetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentName] [nvarchar](255) NOT NULL,
	[Picture] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[SaleID] [int] IDENTITY(1,1) NOT NULL,
	[SaleName] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](255) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Picture] [nvarchar](500) NOT NULL,
	[Discount] [decimal](12, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SaleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 11/2/2020 11:54:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierID] [int] IDENTITY(1,1) NOT NULL,
	[SupplierName] [nvarchar](255) NOT NULL,
	[ContactName] [nvarchar](50) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BlogCategories] ON 

INSERT [dbo].[BlogCategories] ([BlogCategoriesID], [BlogCategoriesName]) VALUES (1, N'The most loving of life')
INSERT [dbo].[BlogCategories] ([BlogCategoriesID], [BlogCategoriesName]) VALUES (2, N'Beautiful drawing passionately')
INSERT [dbo].[BlogCategories] ([BlogCategoriesID], [BlogCategoriesName]) VALUES (3, N'Beautiful drawing love blooms')
INSERT [dbo].[BlogCategories] ([BlogCategoriesID], [BlogCategoriesName]) VALUES (4, N'Lilies are so beautiful')
INSERT [dbo].[BlogCategories] ([BlogCategoriesID], [BlogCategoriesName]) VALUES (5, N'Flower shore')
SET IDENTITY_INSERT [dbo].[BlogCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[BlogComments] ON 

INSERT [dbo].[BlogComments] ([BlogComentID], [BlogID], [Username], [Content], [Date]) VALUES (11, 6, N'Nguyen', N'esghy5ru', CAST(N'2020-11-02T13:29:20.887' AS DateTime))
INSERT [dbo].[BlogComments] ([BlogComentID], [BlogID], [Username], [Content], [Date]) VALUES (12, 5, N'Nguyen Ka', N'I love you', CAST(N'2020-11-02T21:37:51.713' AS DateTime))
INSERT [dbo].[BlogComments] ([BlogComentID], [BlogID], [Username], [Content], [Date]) VALUES (13, 5, N'Json ka', N'Beautiful, good', CAST(N'2020-11-02T21:38:16.007' AS DateTime))
SET IDENTITY_INSERT [dbo].[BlogComments] OFF
GO
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (2, N'The most beautiful flower today', N'nguyenhandsome', 1, N'<div>that spread all over the world. Not only are flowers treasured for providing precious food sources of seeds and oil, but since ancient times, sunflowers have been revered by indigenous people for their splendid beauty. And to this day, sunflowers are also chosen as a symbol for a health campaign that means a lot to the community.</div><div><br></div><div>With optical direction characteristics - always towards the sun:</div><div><br></div><div>In friendships, or partnerships outside of society, sunflowers represent sincerity and consistency.</div><div><br></div><div>- In love, it is a symbol of fidelity, iron and devotion, always towards the other party.</div><div><br></div><div>And, the shape of bright yellow petals like the rays of the sun and sunflowers always makes the viewer feel full of positive energy. The watcher''s mind will always be on goodness, and their life will never contain the two words "pessimism".</div><div><br></div><div>Besides, also because of the symbolic image of the sun, sunflower also shows power, admiration, worship and long-term sustainability.</div><div><br></div><div>Therefore, sunflowers are suitable for expressing affection on occasions of celebrating love and friendship, as well as on occasions of congratulation, to send messages of joy and excitement.</div><div><br></div><div>Finally, an extremely meaningful message that sunflower brings is: "Stand tall and follow your dreams. Focus on what''s positive in your life and don''t let anyone get you down" - Stay strong and follow your dreams. Focus on the positive things in your life and don''t let anyone discourage you.</div>', CAST(N'2020-10-17T14:30:00.000' AS DateTime), N'bo-hoa-ly-lan203057691.jpg')
INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (3, N'Flower beautiful', N'nguyenhandsome', 3, N'<p>Sunflowers represent loveliness, loyalty and longevity. Much of the meaning of a sunflower comes from its name, the sun itself - a powerful symbol of life. ... Additionally, Sunflowers are known to be the happy flowers for Jews, making them the perfect gift to bring joy to someone''s special day or your own ..</p>', CAST(N'2020-10-15T14:39:00.000' AS DateTime), N'images (8)205036921.jpg')
INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (4, N'xfngvnjhjgjmnhgm', N'nguyenhandsome', 1, N'<p>xfb gvmnhm</p>', CAST(N'2020-10-31T14:39:00.000' AS DateTime), N'1418137648_hoa-ly204006920.jpg')
INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (5, N'Love love', N'nguyenhandsome', 1, N'<p>Sunflowers represent loveliness, loyalty and longevity. Much of the meaning of a sunflower comes from its name, the sun itself - a powerful symbol of life. ... Additionally, Sunflowers are known to be the happy flowers for Jews, making them the perfect gift to bring joy to someone''s special day or your own .</p>', CAST(N'2020-10-19T14:40:00.000' AS DateTime), N'HongHoa203655601.jpg')
INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (6, N'srgftjhyhj', N'nguyenhandsome', 5, N'<p>sgrgthjt</p>', CAST(N'2020-10-20T14:40:00.000' AS DateTime), N'Lotus204103221.jpg')
INSERT [dbo].[Blogs] ([BlogID], [BlogName], [Username], [BlogCategoriesID], [Content], [Date], [Picture]) VALUES (7, N'OKOKOKOK', N'nguyenhandsome', 5, N'<p>aegdrhdthtfjnj</p>', CAST(N'2020-10-30T14:14:00.000' AS DateTime), N'aaaaa141203720696.jpg')
SET IDENTITY_INSERT [dbo].[Blogs] OFF
GO
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2097, 1, 10)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2097, 1002, 10)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2098, 1, 3)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2098, 2003, 4)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2099, 1, 5)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2099, 2004, 10)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2100, 2, 11)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2100, 1003, 9)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2100, 2004, 8)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2101, 2, 3)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2101, 1003, 5)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2101, 2003, 2)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2102, 1, 4)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2102, 1002, 9)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2103, 2, 11)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2103, 1003, 2)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2103, 2003, 7)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2104, 2, 11)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2104, 1003, 1)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2104, 2003, 4)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2104, 2007, 9)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2105, 1002, 11)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2105, 2003, 12)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2106, 1002, 1)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2106, 1003, 11)
INSERT [dbo].[BouquetDetails] ([BouquetID], [FlowerID], [Quantity]) VALUES (2106, 2005, 6)
GO
SET IDENTITY_INSERT [dbo].[Bouquets] ON 

INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2097, N'One Dozen Assorted Roses', 2, 9, N'HongHoa204805128.jpg', 90, 90, N'No gift has stood the test of time more than a dozen beautifully scented, gorgeous, seductive Roses. ', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">No gift has stood the test of time more than a
dozen beautifully scented, gorgeous, seductive Roses. Show your love and
romantic feelings with this captivating bouquet of short stem Roses in an
attractive blend of assorted colours.To add to the element of surprise, may we
recommend including a Premium Vase to your order.Wrapping varies depending on
florists.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2098, N'Aplenty', 2, 9, N'aaaaa1201511374.jpg', 100, 90, N'For a lasting impression we recommend adding a Premium Vase to complement these gorgeous flowers', N'<p class="MsoNormal"><span style="font-size:12.0pt;line-height:107%;font-family:
&quot;Times New Roman&quot;,serif;mso-fareast-font-family:&quot;Times New Roman&quot;">Send a
lavish display of colour with Gerberas aplenty. This lovely bountiful bunch of
flowers is right for every occasion and is guaranteed to give happy thoughts
and be loved by all.For a lasting impression we recommend adding a Premium Vase
to complement these gorgeous flowers.Wrapping varies depending on florists.<o:p></o:p></span></p><p>

<b><span style="font-size:13.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">Medium&nbsp;</span><span style="font-size:9.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">$</span><span style="font-size:13.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">25</span><span style="font-size:9.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">.84</span><span style="font-size:13.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">Large&nbsp;</span><span style="font-size:9.5pt;line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;
mso-fareast-font-family:&quot;Times New Roman&quot;;color:white;mso-ansi-language:EN-US;
mso-fareast-language:EN-US;mso-bidi-language:AR-SA">$</span></b><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2099, N'One Dozen Ardent Blooms', 2, 9, N'aaaa2201715586.jpg', 110, 110, N'No gift has stood the test of time more than a dozen beautifully scented, gorgeous, seductive Roses! ', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">No gift has stood the test of time more than a
dozen beautifully scented, gorgeous, seductive Roses! Show your love and romantic
feelings with this captivating bouquet of short stem Roses in an attractive
blend of assorted colours.This product does not come with a vase however; if
you would like to leave a lasting impression we recommend a Premium Vase to
complement your flowers.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2100, N'Budding Love', 1005, 9, N'aaaaaa3201822238.jpg', 110, 110, N'Does somebody make you feel warm inside? ', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">Does somebody make you feel warm inside? Let
that person know with this beautiful 24 short stem Rose bouquet in luscious
pink tonesVase not included however; if you are wishing to leave a lasting
impression, we recommend a Superior Vase to complement these luscious short
stemmed Roses.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2101, N'15 Assorted Orchids', 1006, 9, N'aaaaa5201932194.jpg', 60, 60, N'This exquisite bouquet made up of fifteen exotic and delicate sprays of Dendrobium Orchids, each spray bursting with vivid and beautifully colour harmonised blooms. ', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">This exquisite bouquet made up of fifteen exotic
and delicate sprays of Dendrobium Orchids, each spray bursting with vivid and
beautifully colour harmonised blooms. This is nature on display at its very,
very best.The vase in the picture is not included, but to personalise your
gift, we recommend a Standard Vase as an ideal match for these beautiful
Orchids.Wrapping varies depending on florists.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2102, N'Glamorous', 1005, 17, N'aaaa11202322752.jpg', 90, 90, N'Send this glamorous bouquet of sensational pink Oriental Lilies and premium long stem pink Roses and leave your special someone feeling like a superstar', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">Send this glamorous bouquet of sensational pink
Oriental Lilies and premium long stem pink Roses and leave your special someone
feeling like a superstar. Glamorous is your passport to success.Lilies on
delivery may be closed or partially closed in order to prevent damage and to
provide a longer lasting product.Vase not included, however if you would like
to make your flower gift extra special, why not add a vase to your order? We
recommend a Superior Vase to leave a lasting impression.Wrapping varies depending
on florists.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2103, N'Celebration', 1007, 1, N'aaaa112202524693.jpg', 80, 80, N'An explosion of two dozen vibrant Tulips is a necessary companion to any occasion worth celebrating! ', N'<p class="MsoNormal" style="background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;"><span style="font-size:10.5pt;
line-height:107%;font-family:&quot;Helvetica&quot;,sans-serif;mso-fareast-font-family:
&quot;Times New Roman&quot;;color:#333333">An explosion of two dozen vibrant Tulips is a
necessary companion to any occasion worth celebrating! Vase not included,
however to add to the element of surprise, may we recommend including a
Superior Vase to your order. Wrapping varies depending on florists.<o:p></o:p></span></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2104, N'Sweet Sentiments', 1003, 16, N'aaaa1111202758727.jpg', 120, 110, N'Send sweet sentiments to your family and friends for the times in life that we all love to celebrate. ', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">Send sweet sentiments to your family and friends
for the times in life that we all love to celebrate. Congratulate a new born,
an engagement, a wedding or any of those special moments with this beautiful
bouquet!For a lasting impression we recommend adding a Premium Vase to
complement these gorgeous flowers.Wrapping varies depending on florists.</span><br></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2105, N'Tina', 1004, 1, N'aaaaa141202858179.jpg', 110, 110, N'This bouquet of Irises is stunning and elegant. They will be a classy addition to any room.', N'<p class="MsoNormal"><span style="font-size:12.0pt;line-height:107%;font-family:
&quot;Times New Roman&quot;,serif;mso-fareast-font-family:&quot;Times New Roman&quot;">This bouquet
of Irises is stunning and elegant. They will be a classy addition to any
room.Vase not included however to better personalise your gift, we recommend
adding a Standard Vase to accompany this lovely Irises.<o:p></o:p></span></p>', 20, 0)
INSERT [dbo].[Bouquets] ([BouquetID], [BouquetName], [OccasionID], [SaleID], [MainImage], [UnitPrice], [OldUnitPrice], [ShortDescription], [Description], [Quantity], [Status]) VALUES (2106, N'Creamy White Spray Roses', 1002, 16, N'aaaa11111202956283.jpg', 130, 130, N'The featured vase is not included however; for a long lasting impression we recommend a Superior Vase which is the ideal selection for these very special Roses.', N'<p><span style="font-size: 10.5pt; line-height: 107%; font-family: Helvetica, sans-serif; color: rgb(51, 51, 51); background-image: initial; background-position: initial; background-size: initial; background-repeat: initial; background-attachment: initial; background-origin: initial; background-clip: initial;">Perfect for any occasion! Don''t just wait for
Valentines Day or your anniversary, surprise your loved one with these creamy,
white Spray Roses and make an ordinary day something special! Spray Roses are
great value with many blooming buds per stem.The featured vase is not included
however; for a long lasting impression we recommend a Superior Vase which is
the ideal selection for these very special Roses.</span><br></p>', 20, 0)
SET IDENTITY_INSERT [dbo].[Bouquets] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentID], [BouquetID], [Username], [Content], [CommentDate]) VALUES (7, 2097, N'nguyenka', N'Wow', CAST(N'2020-11-02T21:41:06.807' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
INSERT [dbo].[Customers] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Status]) VALUES (N'aaaa1234', N'$s2$16384$8$1$525twuFzZiTVsVT7bznkLrB9I2mR0EiUUUT5PLGzn9I=$7A0K9uC0gxxBRJ9AMSHe/tVkGZELkMNjZfVInijZpwA=', N'Trầnáccc', N'Nguyênscc', CAST(N'1999-10-11' AS Date), 0, N'0898809787', N'nguyenkamxxdce123@gmail.com', N'Tháp Mười, Đồng Tháp', N'Sontung205127468.jpg', 1)
INSERT [dbo].[Customers] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Status]) VALUES (N'nguyenka', N'$s2$16384$8$1$VCIoVjmUPU+vR2L4eg3uXNQM+t1THpygxnXwPRSspqQ=$2XMijFxUhS21W2cFTy38NwqOBSpf9/E1BfXFXgW8TM0=', N'Trần', N'Nguyên', CAST(N'1999-10-14' AS Date), 1, N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp', N'kkkkkkkkkkk204305709.png', 1)
GO
INSERT [dbo].[Employees] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Role], [Status]) VALUES (N'nguyenhandsome', N'$s2$16384$8$1$jDZiGy5n3HICQ+RcES3F5XBtY3p5LyQ0AzAU0dWbJ6Y=$JODSlnGIfbZMIh1pqSpZXgL+XXz/TQgQPT/BWikTnT0=', N'Tran', N'Ngoc Nguyen', CAST(N'1999-02-11' AS Date), 1, N'0898809791', N'nguyenka123@gmail.com', N'Tháp Mười, Đồng Tháp', N'60169784_459149858160801_7579551176362819584_o205913707.jpg', 0, 1)
INSERT [dbo].[Employees] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Role], [Status]) VALUES (N'NguyenHandsome1', N'$s2$16384$8$1$eA5TqcPW9HG6DVCOvWCeixh+hen2ft+s2Dn4zaJsbOA=$Tn3wg8VPCJB1RtYRroI9RVFa/dGzROy1JzaZXFKlYf0=', N'Trần', N'Nguyên', CAST(N'1999-02-11' AS Date), 1, N'0898809791', N'nguyenkame1fctfcftft23@gmail.com', N'Tháp Mười, Đồng Tháp', N'Sontunggg201508139.jpg', 1, 1)
INSERT [dbo].[Employees] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Role], [Status]) VALUES (N'tranngocnguyen1102', N'$s2$16384$8$1$8DAoXmZi8OKjJRyf0gfQhu1pgnwAaE72zm0DDlUztdM=$ji+QH4VmpjLw69B19c1DHpRQZchJQioyi/d6obDBAIs=', N'Trần', N'Nguyên', CAST(N'1999-10-23' AS Date), 1, N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp', N'Mytamconguong201713209.jpg', 1, 1)
INSERT [dbo].[Employees] ([Username], [Password], [FirstName], [LastName], [DateOfBirth], [Gender], [Phone], [Email], [Address], [Picture], [Role], [Status]) VALUES (N'tranngocnguyen110233', N'$s2$16384$8$1$1yzUgSx3Pr2TJzbeR7DCU8zs51Ot/EQ76fWSNeoqFgA=$3OElAQxWlubThI7V3ufYboYPkB1gHOWFTxlBvl7yqB0=', N'tran', N'nguen', CAST(N'1999-10-30' AS Date), 1, N'0898809791', N'nguyenka35356123@gmail.com', N'Tháp Mười, Đồng Tháp', N'Logo205755366.png', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Flowers] ON 

INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (1, N'Rose', 1, N'Trong ngày lễ tình nhân hay mỗi dịp kỉ niệm tình yêu, hoa hồng là một món quà không thể thiếu. Bởi lẽ đó là loại hoa tượng trưng cho tình yêu được nhiều người biết đến nhất trên thế giới, là loại hoa ngọt ngào, nồng thắm mà tình yêu đã len lỏi vào từng cánh hoa, nhị hoa và cả hương thơm dịu nhẹ phảng phất...', 10, 3660, N'rose205717812.jpg', N'<p class="MsoNormal">Based on the legend of the red rose associated with the
goddess of love, that is why this flower is considered a symbol of passionate,
sweet and intense love with many different levels of emotions. .<o:p></o:p></p><p class="MsoNormal">Because of that, couples, whether they have just been
together or have experienced many storms and storms in their life, often choose
a bouquet of bright red roses to express their love and give to each other. The
promise is forever to be together, no matter what difficulties, difficulties or
challenges stand in the way.<o:p></o:p></p><p class="MsoNormal">And each red rose petal is seen as a message of sincere love
that couples give each other on special and meaningful occasions.<o:p></o:p></p><p class="MsoNormal">For couples who have just fallen in love, bright red roses
are used to express their messages affirming their feelings for each other, and
are also considered witnesses to the promise of love to develop. durable and
durable.<o:p></o:p></p><p class="MsoNormal">Meanwhile, for couples who have gone through many ups and
downs together in life or those who are married to the same roof, red rose is
considered a rope to help bond. feelings of both are more lasting and help
evoke the burning and passionate feelings of love like the first days of love.<o:p></o:p></p><p class="MsoNormal">Not only considered to be the most typical and prominent representative
of the rose as well as the symbolic image of a romantic, passionate, burning
and sweet love of the couple, the image of red roses is also used. to describe
the perfection of a girl''s beauty.<o:p></o:p></p><p>











</p><p class="MsoNormal">In Western culture, in these countries, men often use the
beauty of red roses to describe the beauty of a woman who is attractive and
seductive. That is why they often use red roses to give the girls they have a
good relationship with instead of praising and praising their beauty.<span style="font-family:&quot;Times New Roman&quot;,serif"><o:p></o:p></span></p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2, N'Lotus', 2, N'The lotus flower with rare and noble qualities is unmatched, not only especially because the pink lotus is the national flower of Vietnam, but in addition, the lotus has also become a favorite gifted flower. its expressive ...', 12, 1280, N'Lotus200925563.jpg', N'<p>In everyday life, people give each other a lotus flower to pay homage, to celebrate the pure and pure qualities of the other party.</p><p>And just like other flowers, each color of the lotus shows its own deep meaning, on its original rustic, elegant background.</p><p>With Sen Trang: people can easily feel the idyllic, high, pure, subtle features of dignity. The white color of the lotus flower brings peace and peace to the soul. "Love bright white lotus - Live a life of virtue and high soul".</p><p>With Green Lotus - a special flower, because in essence, the blue lotus is the white lotus, which is pink but very light, the lower part of the wing is white, the tip is light pink. And when the white lotus just blooms, the outer petals are greenish, so they are called the blue lotus. However, between the white lotus and the blue lotus, there are different meanings that people have sent. If above we have known that the white lotus represents the absolute purity, the purity of the soul. Green lotus then brings back the meaning of willpower, resilient energy and eternal belief. It is also a symbol of freedom, equality and charity.</p><p>And, with the pink lotus - the flower voted to become the national flower of our country, its meaning has embraced all of the above, moreover, it is a popular flower, close to Vietnamese people. .</p><p>Above, are the meanings of lotus application in flower giving culture that Hoayeuthuong.com mentions to its readers. And of course, that is only a small part of the spiritual values ??that the lotus brings, because in Buddhism and literature, the lotus is always a sacred and noble symbol.</p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (1002, N'Orchid', 3, N'Orchids are a symbol of fertility and bloom, orchids are flowers with many different meanings.', 7, 1480, N'Lan202200864.jpg', N'<p>Orchids are a plant used for decoration in many families because orchids carry a pure, gentle but equally deep, brilliant beauty.</p><p><img alt="Hoa lan phong th?y dem l?i c?m giác xanh" src="https://hoaonline247.com/theme/V08/lib/ckfinder/userfiles/files/xhoa-lan-phong-thuy-1.jpg.pagespeed.ic.y1Y7P1u7Vd.webp" data-pagespeed-url-hash="3193193901" style="border: 1px solid rgb(238, 238, 238); max-width: 100%; margin: 20px 0px; font-family: &quot;Open Sans&quot;, sans-serif; text-align: center; height: 853px; width: 640px;"></p><p>Indoor orchids will bring a green feeling to the family''s living space, creating a cool, fresh space.</p><p><br></p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (1003, N'Tuberose', 4, N'Unlike other flowers, the perennial lilies never cease to function. The strength and beauty of this international flower has been strengthened in cultures around the world. With elegant appearance, there is no doubt that it represents loyalty, rebirth, and purity. Discover this flower''s symbolic meaning to enrich your life', 6, 1440, N'Hue201542303.jpg', N'<p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Tuberose and all its other forms, often considered, have the following meanings:</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Royal and regal</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Motherhood and fertility</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Youth purity and beauty</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Passion</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Renew and regenerate</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">Origin of the flower name</font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">There are hundreds of different types of lilies, but all belong to the same genus Lilium. Other lilies that are not of this genus, such as ordinary lily or water lily are not considered real lilies. Lilium is a Latin word derived from the Greek leirion, which, when we traced back through many civilizations, can be said to be one of the first words to refer to flowers. This indicates the importance of lily pads over the centuries and millennia.</font></p><p style="margin-bottom: 1.3em;"><img class="lazy-load-active" src="https://ynghiahoa.net/wp-content/uploads/2016/12/y-nghia-hoa-hue-2.jpg" data-src="https://ynghiahoa.net/wp-content/uploads/2016/12/y-nghia-hoa-hue-2.jpg" alt="ý nghia và bi?u tu?ng hoa hu?" width="700" height="467" style="max-width: 100%; height: auto; display: inline-block; transition: opacity 1s ease 0s; opacity: 1; color: rgb(119, 119, 119); font-family: Lato, sans-serif; font-size: medium;"><font color="#777777" face="Lato, sans-serif" size="3"><br></font></p><p style="margin-bottom: 1.3em;"><font color="#777777" face="Lato, sans-serif" size="3">The Romans and Greeks kept the lily in an important place, including it in their dozens of religious myths and multiplying it extensively. Alchemists claim that it is the tree of the moon that carries feminine qualities, while the demand for lilies in China for weddings is great since its name sounds similar to the beginning of a Sentence wishes the couple live together happily for a hundred years Chinese friends and family also often give lilies to those who have experienced the loss because it is believed to help relieve the pain. Don''t forget about the enduring European royal mark known as iris, often found on silver and wallpaper, the stylized floral design symbolizes regal and is based on a member of the line. they lilies.<br></font></p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2003, N'Faleur-de-lis', 6, N'The phone has a trendy design, powerful performance and a set of 4 super impressive cameras, optimized for ultra-sharp photography and movie shooting, promising to be worth upgrading oppo this year.', 10, 1420, N'1418137648_hoa-ly201011057.jpg', N'<p>In Vietnam, this flower is also known as lily or lilies. This flower has many colors, however white is the most popular.</p><p>Not only the meaning of happiness, but the beauty of the flower also evokes in the hearts of people watching the feelings of nobility, nobility and purity.</p><p>In addition, cypress also has the meaning of "old age", so flowers are often given to newlyweds to wish wishes for a hundred years of harmony and happiness forever.</p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2004, N'Hydrangeas', 8, N'The phone has a trendy design, powerful performance and a set of 4 super impressive cameras, optimized for ultra-sharp photography and movie shooting, promising to be worth upgrading oppo this year.', 23, 1640, N'nong-nan-sac-hoa-cam-tu-cau-nhat-ban-vao-mau-mua-10__1_201317953.jpg', N'<p>What is special about hydrangeas is that the color of the flower depends on the pH of the soil. If the soil has a pH of 7, the plants produce milky white flowers, if the soil has a pH greater than 7, the plants will produce pink or purple flowers, and if the pH is less than 7 the plants will produce blue flowers.</p><p><br></p><p>Therefore, flower color can be adjusted through adjusting the pH of the soil. (If you want the flower to be blue in the summer and autumn, apply iron chloride solution or you can bury a few rusty nails in the tree, you can also bury a little aluminum chloride, magnesium chloride in the ground. soil a little lime powder.)</p><p><br></p><p>In terms of meaning, the main meaning of hydrangeas is to symbolize cold, emotionless.</p><p><br></p><p>Besides, there are other meanings, which are still a lot of controversy around:</p><p><br></p><p>Due to the change in color of the soil pH, which gives hydrangeas a rather sad meaning, is change in love.</p><p><br></p><p>Another story is about a Japanese Emperor using hydrangeas to apologize to his loved one. So from then on, hydrangeas are also the flowers that send apologies.</p><p><br></p><p>And, from the appearance with fragile petals, crowded together to form a flower ball, hydrangeas also represent gratitude and sincerity.</p><p><br></p><p>Although there is much controversy around the meaning of hydrangeas, but some parts of the world think that hydrangeas are flowers used to celebrate the 4th anniversary of their wedding.</p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2005, N'Lotus flowers', 2, N'sdgvbdfb', 11, 1880, N'hoa-lan-20-10202119886.jpg', N'<p>svzdgvdsgv</p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2006, N'Sun Flower', 10, N'beautifull', 11, 2000, N'434019_y-nghia-hoa-huong-duong201731088.png', N'<p>Sunflowers originated in North America, and in the 16th century the seeds were brought to Europe and then spread around the world. Not only are flowers treasured for providing precious food sources of seeds and oil, but since ancient times, sunflowers have been revered by indigenous people for their splendid beauty. And to this day, sunflowers are also chosen as a symbol for a health campaign that means a lot to the community.</p><p><br></p><p>With optical direction characteristics - always towards the sun:</p><p><br></p><p>In friendships, or partnerships outside of society, sunflowers represent sincerity and consistency.</p><p><br></p><p>- In love, it is a symbol of fidelity, iron and devotion, always towards the other party.</p><p><br></p><p>And, the shape of bright yellow petals like the rays of the sun and sunflowers always makes the viewer feel full of positive energy. The watcher''s mind will always be on goodness, and their life will never contain the two words "pessimism".</p><p><br></p><p>Besides, also because of the symbolic image of the sun, sunflower also shows power, admiration, worship and long-term sustainability.</p><p><br></p><p>Therefore, sunflowers are suitable for expressing affection on occasions of celebrating love and friendship, as well as on occasions of congratulation, to send messages of joy and excitement.</p><p><br></p><p>Finally, an extremely meaningful message that sunflower brings is: "Stand tall and follow your dreams. Focus on what''s positive in your life and don''t let anyone get you down" - Stay strong and follow your dreams. Focus on the positive things in your life and don''t let anyone discourage you.</p>', 1)
INSERT [dbo].[Flowers] ([FlowerID], [FlowerName], [SupplierID], [ShortDescription], [UnitPrice], [Quantity], [Picture], [Meaning], [Status]) VALUES (2007, N'ho ahuongb hdhd', 5, N'hđg  hdhdj m dsđ', 15, 1820, N'Payment (27)203028806.png', N'<p>z jdjdjd krke&nbsp;</p>', 1)
SET IDENTITY_INSERT [dbo].[Flowers] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (2, 2, N'Wish you a fortune new year, lots of luck')
INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (8, 1006, N'Wish you always happy and live a long life')
INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (9, 1, N'Healthy new age')
INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (10, 1003, N'Happy valentine')
INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (11, 1007, N'Wish mom a lot of health')
INSERT [dbo].[Messages] ([MessageID], [OccasionID], [MessageContent]) VALUES (12, 1002, N'Merry Christmas')
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Occasions] ON 

INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1, N'Birthday')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (2, N'New Year')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1002, N'Christmas')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1003, N'Valentine')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1004, N'Good health')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1005, N'Opening ceremony')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1006, N'Longevity ceremony')
INSERT [dbo].[Occasions] ([OccasionID], [OccasionName]) VALUES (1007, N' Women''s International ')
SET IDENTITY_INSERT [dbo].[Occasions] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderID], [Username], [PaymentID], [Message], [CreationDate], [Recipient], [Address], [Phone], [ShippingDate], [Note], [Status]) VALUES (24, N'nguyenka', 1002, N'Healthy new age', CAST(N'2020-11-02T21:41:40.153' AS DateTime), N'Ngoc Hoang', N'Thap Muoi, Dong Thap', N'0898809791', CAST(N'2020-11-03T21:41:40.153' AS DateTime), N'', 0)
INSERT [dbo].[Order] ([OrderID], [Username], [PaymentID], [Message], [CreationDate], [Recipient], [Address], [Phone], [ShippingDate], [Note], [Status]) VALUES (25, N'nguyenka', 1003, N'I miss you very much', CAST(N'2020-11-02T21:42:14.130' AS DateTime), N'Ngoc trinh', N'Tháp Mười, Đồng Tháp', N'0898809791', CAST(N'2020-11-03T21:42:14.130' AS DateTime), N'', 0)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetails] ([OrderID], [BouquetID], [UnitPrice], [Quantity]) VALUES (24, 2097, 90, 1)
INSERT [dbo].[OrderDetails] ([OrderID], [BouquetID], [UnitPrice], [Quantity]) VALUES (25, 2098, 100, 1)
INSERT [dbo].[OrderDetails] ([OrderID], [BouquetID], [UnitPrice], [Quantity]) VALUES (25, 2099, 110, 1)
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentID], [PaymentName], [Picture]) VALUES (1002, N' Payment via Momo', N'Thẻ momo200122853.png')
INSERT [dbo].[Payments] ([PaymentID], [PaymentName], [Picture]) VALUES (1003, N'Payment via Visa card', N'Thẻ Visa200301680.png')
INSERT [dbo].[Payments] ([PaymentID], [PaymentName], [Picture]) VALUES (1004, N'Payment via bank card', N'Thẻ ngân hàng200431115.jpg')
INSERT [dbo].[Payments] ([PaymentID], [PaymentName], [Picture]) VALUES (1005, N'Payment via ViettelPay card', N'Thẻ viettelpay200613514.jpg')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([SaleID], [SaleName], [Content], [StartDate], [EndDate], [Picture], [Discount]) VALUES (1, N'No promotion', N'No promotion', CAST(N'2020-10-25T04:00:00.000' AS DateTime), CAST(N'2020-10-28T04:00:00.000' AS DateTime), N'NoSale204011494.jpg', CAST(1.00 AS Decimal(12, 2)))
INSERT [dbo].[Sales] ([SaleID], [SaleName], [Content], [StartDate], [EndDate], [Picture], [Discount]) VALUES (9, N'Promotion banquet', N'30% discount', CAST(N'2020-10-25T04:00:00.000' AS DateTime), CAST(N'2020-11-04T04:00:00.000' AS DateTime), N'28669504-grass-with-flowers-and-white-text-sale-on-it204941242.jpg', CAST(1.00 AS Decimal(12, 2)))
INSERT [dbo].[Sales] ([SaleID], [SaleName], [Content], [StartDate], [EndDate], [Picture], [Discount]) VALUES (11, N'Promotional roof ball', N'Non-stop sale', CAST(N'2020-10-26T11:45:00.000' AS DateTime), CAST(N'2020-10-29T11:46:00.000' AS DateTime), N'White-bouquet-wallpaper-HD204614209.jpg', CAST(3.00 AS Decimal(12, 2)))
INSERT [dbo].[Sales] ([SaleID], [SaleName], [Content], [StartDate], [EndDate], [Picture], [Discount]) VALUES (16, N'Women''s Day', N'Happy International Women''s Day', CAST(N'2020-03-05T06:24:00.000' AS DateTime), CAST(N'2020-03-09T06:25:00.000' AS DateTime), N'Lan202719842.jpg', CAST(20.00 AS Decimal(12, 2)))
INSERT [dbo].[Sales] ([SaleID], [SaleName], [Content], [StartDate], [EndDate], [Picture], [Discount]) VALUES (17, N'New Year', N'Happy New Year', CAST(N'2020-12-25T06:28:00.000' AS DateTime), CAST(N'2020-12-31T06:29:00.000' AS DateTime), N'GettyImages-686793555-e1576093850689203952807.jpg', CAST(25.00 AS Decimal(12, 2)))
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (1, N'Rose Shop', N'Elizabeth Tran', N'0898809791', N'nguyenkame@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (2, N'Lotus Shop', N'Laura Dang', N'0336660300', N'tnnguyena18098@cusc.ctu.edu.vn', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (3, N'Orchid  Shop', N'Christopher Vo', N'0122134000', N'nguyenhandsome@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (4, N'Tuberose Shop', N'Daniel Ly', N'0111122234', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (5, N'Carnation Shop', N'Adelia Huynh', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (6, N'Fleur-de-lis', N'Fleur-de-lis Shop', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (7, N'Auspicious ', N'Auspicious Shop', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (8, N'Hydrangeas Shop', N'Zulema Ong', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (9, N'Lilies Shop', N'Veronica Ly', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
INSERT [dbo].[Suppliers] ([SupplierID], [SupplierName], [ContactName], [Phone], [Email], [Address]) VALUES (10, N'Sun Flower Shop', N'Vivian Ngo', N'0898809791', N'nguyenkame123@gmail.com', N'Tháp Mười, Đồng Tháp')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([BlogCategoriesID])
REFERENCES [dbo].[BlogCategories] ([BlogCategoriesID])
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([Username])
REFERENCES [dbo].[Employees] ([Username])
GO
ALTER TABLE [dbo].[BouquetDetails]  WITH CHECK ADD FOREIGN KEY([BouquetID])
REFERENCES [dbo].[Bouquets] ([BouquetID])
GO
ALTER TABLE [dbo].[BouquetDetails]  WITH CHECK ADD FOREIGN KEY([FlowerID])
REFERENCES [dbo].[Flowers] ([FlowerID])
GO
ALTER TABLE [dbo].[BouquetImages]  WITH CHECK ADD FOREIGN KEY([BouquetID])
REFERENCES [dbo].[Bouquets] ([BouquetID])
GO
ALTER TABLE [dbo].[Bouquets]  WITH CHECK ADD FOREIGN KEY([SaleID])
REFERENCES [dbo].[Sales] ([SaleID])
GO
ALTER TABLE [dbo].[Bouquets]  WITH CHECK ADD FOREIGN KEY([OccasionID])
REFERENCES [dbo].[Occasions] ([OccasionID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([BouquetID])
REFERENCES [dbo].[Bouquets] ([BouquetID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([Username])
REFERENCES [dbo].[Customers] ([Username])
GO
ALTER TABLE [dbo].[Flowers]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Suppliers] ([SupplierID])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([OccasionID])
REFERENCES [dbo].[Occasions] ([OccasionID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [dbo].[Payments] ([PaymentID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([Username])
REFERENCES [dbo].[Customers] ([Username])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([BouquetID])
REFERENCES [dbo].[Bouquets] ([BouquetID])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([OrderID])
GO
