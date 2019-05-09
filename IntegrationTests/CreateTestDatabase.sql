USE [GTL_TEST]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 07-05-2019 15:45:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StreetName] [varchar](50) NOT NULL,
	[HouseNumber] [varchar](10) NOT NULL,
	[ZipCode] [varchar](8) NOT NULL,
	[City] [varchar](30) NOT NULL,
	[Type] [varchar](20) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 07-05-2019 15:45:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](60) NOT NULL,
	[BirthDate] [datetime] NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Copy]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Copy](
	[Barcode] [int] IDENTITY(100000,1) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[MaterialISBN] [varchar](13) NOT NULL,
 CONSTRAINT [PK_Copy] PRIMARY KEY CLUSTERED 
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Library]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Library](
	[Name] [varchar](40) NOT NULL,
	[ProfessorLoanDuration] [int] NOT NULL,
	[ProfessorGracePeriod] [int] NOT NULL,
	[MemberLoanDuration] [int] NOT NULL,
	[MemberGracePeriod] [int] NOT NULL,
	[ProfessorMaxBooksOnLoan] [int] NOT NULL,
	[MemberMaxBooksOnLoan] [int] NOT NULL,
 CONSTRAINT [PK_Library] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loan]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[ReturnDate] [datetime] NULL,
	[MemberSsn] [varchar](15) NOT NULL,
	[CopyBarcode] [int] NOT NULL,
	[LibraryName] [varchar](40) NOT NULL,
 CONSTRAINT [PK_Loan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanerCard]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanerCard](
	[Barcode] [int] IDENTITY(10000,1) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MemberSsn] [varchar](15) NOT NULL,
 CONSTRAINT [PK_LoanerCard] PRIMARY KEY CLUSTERED 
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[ISBN] [varchar](13) NOT NULL,
	[Title] [varchar](60) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Edition] [int] NOT NULL,
	[Area] [varchar](50) NULL,
	[Size] [varchar](20) NULL,
	[DeletedAt] [datetime] NULL,
	[Type] [varchar](30) NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialAuthor]    Script Date: 07-05-2019 15:45:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialAuthor](
	[MaterialISBN] [varchar](13) NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_MaterialAuthor] PRIMARY KEY CLUSTERED 
(
	[MaterialISBN] ASC,
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialPublisher]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialPublisher](
	[MaterialISBN] [varchar](13) NOT NULL,
	[PublisherName] [varchar](60) NOT NULL,
 CONSTRAINT [PK_MaterialPublisher] PRIMARY KEY CLUSTERED 
(
	[MaterialISBN] ASC,
	[PublisherName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialSubject]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialSubject](
	[MaterialISBN] [varchar](13) NOT NULL,
	[SubjectName] [varchar](60) NOT NULL,
 CONSTRAINT [PK_MaterialSubject] PRIMARY KEY CLUSTERED 
(
	[MaterialISBN] ASC,
	[SubjectName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[Ssn] [varchar](15) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Type] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[Ssn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberAddress]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberAddress](
	[MemberSsn] [varchar](15) NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_MemberAddress] PRIMARY KEY CLUSTERED 
(
	[MemberSsn] ASC,
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](30) NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[LoanId] [int] NULL,
	[LoanerCardBarcode] [int] NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneNumber]    Script Date: 07-05-2019 15:45:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneNumber](
	[Number] [varchar](8) NOT NULL,
	[MemberSsn] [varchar](15) NULL,
 CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 07-05-2019 15:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[Name] [varchar](60) NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 07-05-2019 15:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaterialISBN] [varchar](13) NOT NULL,
	[MemberSsn] [varchar](15) NOT NULL,
	[AvailableTo] [datetime] NOT NULL,
	[AvailableFrom] [datetime] NOT NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 07-05-2019 15:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Role] [varchar](30) NOT NULL,
	[PasswordHash] [varchar](max) NOT NULL,
	[PasswordSalt] [varchar](max) NOT NULL,
	[PasswordLastChanged] [datetime] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 07-05-2019 15:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[Name] [varchar](60) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WishlistLine]    Script Date: 07-05-2019 15:46:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WishlistLine](
	[MaterialISBN] [varchar](13) NOT NULL,
	[Amount] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUpdated] [datetime] NULL,
 CONSTRAINT [PK_WishlistLine] PRIMARY KEY CLUSTERED 
(
	[MaterialISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (1, N'South Green First Drive', N'4,  th', N'6815', N'Lincoln', N'HOME')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (2, N'Oak Blvd.', N'4, 7 th', N'2933', N'Arlington', N'HOME')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (3, N'East Nobel St.', N'32', N'8600', N'Newark', N'HOME')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (4, N'Clarendon Freeway', N'7003,  ', N'1874', N'Sacramento', N'CAMPUS')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (5, N'Clarendon Drive', N'6', N'5409', N'Los Angeles', N'HOME')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (6, N'Green Fabien Avenue', N'6991, 8 ', N'5534', N'San Diego', N'CAMPUS')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (7, N'White New Avenue', N'60, 2 tv', N'2506', N'Anchorage', N'HOME')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (8, N'Cowley Drive', N'04, 6 ', N'5486', N'Honolulu', N'CAMPUS')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (9, N'Green Milton Boulevard', N'8540, 2 ', N'5864', N'Fresno', N'CAMPUS')
INSERT [dbo].[Address] ([Id], [StreetName], [HouseNumber], [ZipCode], [City], [Type]) VALUES (10, N'Green First Parkway', N'3', N'3546', N'Raleigh', N'HOME')
SET IDENTITY_INSERT [dbo].[Address] OFF
SET IDENTITY_INSERT [dbo].[Author] ON 

INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (1, N'Nicole Franco', CAST(N'1977-11-11T17:08:04.490' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (2, N'Pablo Terry', CAST(N'1992-08-24T23:17:27.830' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (3, N'Desiree Lambert', CAST(N'2000-01-06T11:40:10.280' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (4, N'Chadwick Stephenson', CAST(N'1956-12-20T08:44:46.500' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (5, N'Daphne Gilbert', CAST(N'1943-04-28T01:38:38.990' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (6, N'Sonya Mosley', CAST(N'1914-08-31T14:56:48.530' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (7, N'Katina Browning', CAST(N'1963-12-18T17:17:30.360' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (8, N'Kari Boyd', NULL)
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (9, N'Seth Mc Bride', CAST(N'1913-10-17T23:15:27.600' AS DateTime))
INSERT [dbo].[Author] ([Id], [Name], [BirthDate]) VALUES (10, N'Bernard Knight', CAST(N'1997-11-17T02:35:14.580' AS DateTime))
SET IDENTITY_INSERT [dbo].[Author] OFF
SET IDENTITY_INSERT [dbo].[Copy] ON 

INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100000, N'LEND', N'7646855215281')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100001, N'AVAILABLE', N'7833843933')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100002, N'FORBIDDEN', N'7646855215281')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100003, N'FORBIDDEN', N'6298387235')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100004, N'LEND', N'4752782914')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100005, N'LEND', N'6298387235')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100006, N'FORBIDDEN', N'9842228942363')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100007, N'DELETED', N'5544977597600')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100008, N'DELETED', N'9842228942363')
INSERT [dbo].[Copy] ([Barcode], [Status], [MaterialISBN]) VALUES (100009, N'FORBIDDEN', N'4752782914')
SET IDENTITY_INSERT [dbo].[Copy] OFF
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Alvin061', 49, 68, 17, 38, 19, 6)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Cheri582', 53, 60, 23, 45, 11, 10)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Elena445', 56, 62, 25, 32, 14, 5)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Frank383', 50, 68, 16, 35, 15, 10)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Jake058', 42, 66, 16, 39, 7, 8)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Jane906', 49, 67, 29, 31, 10, 8)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Jennifer297', 55, 65, 17, 43, 13, 6)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Johnnie555', 40, 70, 27, 40, 13, 7)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Stanley055', 42, 67, 17, 41, 8, 9)
INSERT [dbo].[Library] ([Name], [ProfessorLoanDuration], [ProfessorGracePeriod], [MemberLoanDuration], [MemberGracePeriod], [ProfessorMaxBooksOnLoan], [MemberMaxBooksOnLoan]) VALUES (N'Tammy625', 43, 63, 21, 38, 15, 9)
SET IDENTITY_INSERT [dbo].[Loan] ON 

INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (1, CAST(N'2015-02-06T17:10:59.140' AS DateTime), CAST(N'2015-03-11T05:16:12.590' AS DateTime), CAST(N'2015-02-24T10:17:20.510' AS DateTime), N'2907473799', 100007, N'Johnnie555')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (2, CAST(N'2020-09-10T02:48:55.090' AS DateTime), CAST(N'2020-10-05T01:17:21.470' AS DateTime), CAST(N'2020-11-13T22:42:12.600' AS DateTime), N'3103264497', 100008, N'Stanley055')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (3, CAST(N'2017-11-08T21:20:13.250' AS DateTime), CAST(N'2017-12-03T07:19:29.890' AS DateTime), CAST(N'2018-02-27T06:20:55.630' AS DateTime), N'2907473799', 100007, N'Johnnie555')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (4, CAST(N'2014-08-25T06:26:44.250' AS DateTime), CAST(N'2014-09-19T05:08:43.630' AS DateTime), CAST(N'2014-10-19T13:21:31.850' AS DateTime), N'2202435847', 100005, N'Jane906')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (5, CAST(N'2020-08-16T09:42:41.270' AS DateTime), CAST(N'2020-09-13T07:07:01.930' AS DateTime), CAST(N'2020-11-25T22:25:24.410' AS DateTime), N'1902812179', 100002, N'Elena445')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (6, CAST(N'2020-06-17T14:17:09.980' AS DateTime), CAST(N'2020-07-23T10:29:33.870' AS DateTime), CAST(N'2020-09-05T12:21:00.780' AS DateTime), N'2202435847', 100005, N'Jane906')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (7, CAST(N'2018-05-01T22:55:39.920' AS DateTime), CAST(N'2018-05-22T19:16:02.810' AS DateTime), CAST(N'2018-06-29T19:22:04.150' AS DateTime), N'3109469775', 100009, N'Tammy625')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (8, CAST(N'2019-12-20T11:11:08.510' AS DateTime), CAST(N'2020-01-16T08:43:08.640' AS DateTime), CAST(N'2020-03-31T01:14:08.190' AS DateTime), N'2106938938', 100004, N'Jake058')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (9, CAST(N'2019-12-24T12:51:59.930' AS DateTime), CAST(N'2020-02-01T21:47:21.090' AS DateTime), CAST(N'2020-01-04T14:42:16.580' AS DateTime), N'3109469775', 100009, N'Tammy625')
INSERT [dbo].[Loan] ([Id], [LoanDate], [DueDate], [ReturnDate], [MemberSsn], [CopyBarcode], [LibraryName]) VALUES (10, CAST(N'2014-09-25T17:31:59.210' AS DateTime), CAST(N'2014-10-24T01:04:14.290' AS DateTime), CAST(N'2014-12-28T20:45:36.100' AS DateTime), N'1902812179', 100002, N'Elena445')
SET IDENTITY_INSERT [dbo].[Loan] OFF
SET IDENTITY_INSERT [dbo].[LoanerCard] ON 

INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10000, CAST(N'2013-10-24T09:50:43.660' AS DateTime), 1, N'2907473799')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10001, CAST(N'2015-09-29T17:25:19.980' AS DateTime), 0, N'3103264497')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10002, CAST(N'2015-01-12T00:55:23.300' AS DateTime), 1, N'2907473799')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10003, CAST(N'2016-10-09T14:25:16.310' AS DateTime), 1, N'2202435847')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10004, CAST(N'2014-11-30T10:03:36.080' AS DateTime), 1, N'1902812179')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10005, CAST(N'2017-10-30T05:06:52.800' AS DateTime), 1, N'2202435847')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10006, CAST(N'2015-09-10T01:45:36.960' AS DateTime), 1, N'3109469775')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10007, CAST(N'2016-08-06T21:55:27.120' AS DateTime), 0, N'2106938938')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10008, CAST(N'2016-05-23T09:45:45.720' AS DateTime), 1, N'3109469775')
INSERT [dbo].[LoanerCard] ([Barcode], [IssueDate], [IsActive], [MemberSsn]) VALUES (10009, CAST(N'2017-01-16T07:07:47.770' AS DateTime), 1, N'1902812179')
SET IDENTITY_INSERT [dbo].[LoanerCard] OFF
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'1782226838439', N'Adpickover', N'habitatio in Longam, quad quantare Id', 7, N'Rwanda', NULL, NULL, N'BOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'2505082949381', N'Endrobopin', N'pladior Quad', 7, N'Cambodia', NULL, NULL, N'RAREBOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'4752782914', N'Winbanonistor', N'fecit. et travissimantor estis', 9, N'Singapore', NULL, NULL, N'BOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'4849245782', N'Trucadicator', N'egreddior fecit. estis', 7, N'Finland', N'77x07cm', NULL, N'BOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'5544977597600', N'Endtanedgor', N'habitatio Multum', 5, N'Sierra Leone', NULL, NULL, N'MAP')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'6298387235', N'Adcadedgin', N'rarendum imaginator si si', 8, N'United Kingdom', NULL, NULL, N'MAP')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'7194343474002', N'Lomeran', N'quoque quad et', 2, N'Mongolia', N'08x31mm', NULL, N'BOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'7646855215281', N'Tipfropover', N'nomen Sed si plurissimum non', 15, N'Guernsey', N'26x12cm', NULL, N'REFERENCEBOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'7833843933', N'Advenantor', N'vantis. Longam, fecundio, bono vobis et glavans eggredior. e esset quorum nomen novum vobis in', 7, N'Monaco', NULL, NULL, N'REFERENCEBOOK')
INSERT [dbo].[Material] ([ISBN], [Title], [Description], [Edition], [Area], [Size], [DeletedAt], [Type]) VALUES (N'9842228942363', N'Tupdimor', N'plorum quo estum.', 2, N'Mauritania', NULL, NULL, N'BOOK')
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'1782226838439', 1)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'2505082949381', 2)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'4752782914', 3)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'4849245782', 4)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'5544977597600', 5)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'6298387235', 6)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'7194343474002', 7)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'7646855215281', 8)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'7833843933', 9)
INSERT [dbo].[MaterialAuthor] ([MaterialISBN], [AuthorId]) VALUES (N'9842228942363', 10)
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'1782226838439', N'Adrobegor International ')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'2505082949381', N'Cippebicower International Corp.')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'4752782914', N'Doperackor International Inc')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'4849245782', N'Grotinegin Direct ')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'5544977597600', N'Inquestexover WorldWide Group')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'6298387235', N'Partuminin Direct Group')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'7194343474002', N'Renipamentor International Inc')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'7646855215281', N'Supmunopower  Inc')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'7833843933', N'Truerilin Direct Company')
INSERT [dbo].[MaterialPublisher] ([MaterialISBN], [PublisherName]) VALUES (N'9842228942363', N'Trusapicator  Corp.')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'1782226838439', N'Beverages')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'2505082949381', N'Cereals')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'4752782914', N'Confections')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'4849245782', N'Dairy')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'5544977597600', N'Grain')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'6298387235', N'Meat')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'7194343474002', N'Poultry')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'7646855215281', N'Produce')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'7833843933', N'Seafood')
INSERT [dbo].[MaterialSubject] ([MaterialISBN], [SubjectName]) VALUES (N'9842228942363', N'Shell fish')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'0605933742', N'Steven Adkins', N'bvnez8@pxpaos.com', N'STUDENT')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'1008461997', N'Shanda Garner', N'eumnzer057@kpcday.com', N'STUDENT')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'1902812179', N'Virginia Hodges', N'ejdzilbk3@vhphvkc.elbsoe.net', N'STUDENT')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'2102838431', N'Sheri Nielsen', N'wihe995@qgcgca.com', N'PROFESSOR')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'2106938938', N'Debbie Beasley', N'kzajhhi.jrppwntbgc@dfyoy.wry--j.com', N'STUDENT')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'2202435847', N'Laurie Mckee', N'gdpodotx@cmyhdz.com', N'PROFESSOR')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'2306836182', N'Angelina Crawford', N'ckmjfzw2@imqjkr.org', N'PROFESSOR')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'2907473799', N'Alan Rangel', N'owpjdc.tdtvfej@zotxy-.org', N'STUDENT')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'3103264497', N'Nora Grant', N'jaiolmz.gtgbdhexxh@oqpsog.net', N'PROFESSOR')
INSERT [dbo].[Member] ([Ssn], [Name], [Email], [Type]) VALUES (N'3109469775', N'Gena Wiley', N'rhxyu.bxxsayyjml@fnnnkkut.svwegb.com', N'PROFESSOR')
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'0605933742', 1)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'1008461997', 2)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'1902812179', 3)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'2102838431', 4)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'2106938938', 5)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'2202435847', 6)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'2306836182', 7)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'2907473799', 8)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'3103264497', 9)
INSERT [dbo].[MemberAddress] ([MemberSsn], [AddressId]) VALUES (N'3109469775', 10)
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (1, N'LOANERCARD', CAST(N'2019-01-19T21:09:18.570' AS DateTime), 9, 10008)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (2, N'LOANERCARD', CAST(N'2014-12-03T18:01:35.260' AS DateTime), 6, 10005)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (3, N'LOANERCARD', CAST(N'2013-08-21T09:17:04.950' AS DateTime), 6, 10005)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (4, N'LOANERCARD', CAST(N'2019-11-25T15:12:48.450' AS DateTime), 5, 10004)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (5, N'LOANERCARD', CAST(N'2016-03-06T12:06:41.110' AS DateTime), 3, 10002)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (6, N'LOANERCARD', CAST(N'2017-08-13T22:13:57.960' AS DateTime), 5, 10004)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (7, N'RETURNMATERIAL', CAST(N'2019-10-22T08:33:49.370' AS DateTime), 5, 10004)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (8, N'LOANERCARD', CAST(N'2017-09-02T21:34:06.080' AS DateTime), 1, 10000)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (9, N'RETURNMATERIAL', CAST(N'2017-03-29T18:40:45.120' AS DateTime), 10, 10009)
INSERT [dbo].[Notification] ([Id], [Type], [DateSent], [LoanId], [LoanerCardBarcode]) VALUES (10, N'LOANERCARD', CAST(N'2017-05-02T02:45:59.250' AS DateTime), 4, 10003)
SET IDENTITY_INSERT [dbo].[Notification] OFF
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'24023587', N'3103264497')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'28951313', N'1902812179')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'35722191', N'2102838431')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'40150162', N'2106938938')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'48119398', N'2202435847')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'56350188', N'2106938938')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'71609122', N'2202435847')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'76760857', N'0605933742')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'86029100', N'2106938938')
INSERT [dbo].[PhoneNumber] ([Number], [MemberSsn]) VALUES (N'95430315', N'3109469775')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Adrobegor International ')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Cippebicower International Corp.')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Doperackor International Inc')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Grotinegin Direct ')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Inquestexover WorldWide Group')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Partuminin Direct Group')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Renipamentor International Inc')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Supmunopower  Inc')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Truerilin Direct Company')
INSERT [dbo].[Publisher] ([Name]) VALUES (N'Trusapicator  Corp.')
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (1, N'7646855215281', N'2907473799', CAST(N'2016-08-18T21:01:36.360' AS DateTime), CAST(N'2016-08-11T21:01:35.360' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (2, N'7833843933', N'3103264497', CAST(N'2020-06-04T00:23:49.630' AS DateTime), CAST(N'2020-05-28T00:23:48.630' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (3, N'7646855215281', N'2907473799', CAST(N'2015-02-19T17:49:45.470' AS DateTime), CAST(N'2015-02-12T17:49:44.470' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (4, N'6298387235', N'2202435847', CAST(N'2020-05-22T16:54:41.320' AS DateTime), CAST(N'2020-05-15T16:54:40.320' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (5, N'4752782914', N'1902812179', CAST(N'2020-01-06T23:15:35.790' AS DateTime), CAST(N'2019-12-30T23:15:34.790' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (6, N'6298387235', N'2202435847', CAST(N'2015-10-08T09:46:56.310' AS DateTime), CAST(N'2015-10-01T09:46:55.310' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (7, N'9842228942363', N'3109469775', CAST(N'2020-02-11T10:35:23.560' AS DateTime), CAST(N'2020-02-04T10:35:22.560' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (8, N'5544977597600', N'2106938938', CAST(N'2016-12-12T01:29:09.440' AS DateTime), CAST(N'2016-12-05T01:29:08.440' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (9, N'9842228942363', N'3109469775', CAST(N'2019-08-02T05:00:06.620' AS DateTime), CAST(N'2019-07-26T05:00:05.620' AS DateTime))
INSERT [dbo].[Reservation] ([Id], [MaterialISBN], [MemberSsn], [AvailableTo], [AvailableFrom]) VALUES (10, N'4752782914', N'1902812179', CAST(N'2019-08-12T08:39:40.590' AS DateTime), CAST(N'2019-08-05T08:39:39.590' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reservation] OFF
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (1, N'Cassandra Sparks', N'ukaggtbf.gfjslwhg@yikqsbpos.qmugxi.com', N'ASSOCIATELIBRARIAN', N'DSG5ZN1GDT', N'NHOLV3JKQK', CAST(N'2016-03-30T18:59:35.560' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (2, N'Evan Hahn', N'zzdipf.eyydxgteg@knfe.mbstkb.net', N'LIBRARYASSISTANT', N'KE4OPJBTAF', N'9XTA45C7FO', CAST(N'2019-04-06T10:27:01.260' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (3, N'Diane Ramsey', N'lvhcxqsf.wqqpqjt@kj-ytu.net', N'LIBRARYASSISTANT', N'4T0RT4B95A', N'GWNMFE4NFP', CAST(N'2015-10-25T07:51:13.400' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (4, N'Judith Summers', N'mmfu860@gqstt-.org', N'CHECKOUTSTAFF', N'ZZ0ST4IOGS', N'B1UGZO9Y1X', CAST(N'2018-12-27T20:51:31.280' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (5, N'Jonathan Chen', N'vxljau64@gfuftl.net', N'ASSOCIATELIBRARIAN', N'PV657CSP5K', N'L878CH8GDR', CAST(N'2015-04-05T01:00:52.980' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (6, N'Danny Kramer', N'mtxo336@ylfvx-.org', N'LIBRARYASSISTANT', N'PON58UX43O', N'U0RY8N5G35', CAST(N'2018-05-05T19:11:17.190' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (7, N'Dale Mckay', N'aatfgemj@hhnbcaxzy.qradya.org', N'CHECKOUTSTAFF', N'QF4NXQ6BLY', N'ELSGN6NXGN', CAST(N'2017-06-30T05:57:23.360' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (8, N'Mary Pace', N'xfqgsu.rjwriof@qydswc.org', N'REFERENCELIBRARIAN', N'V34KOWYHF5', N'OBFE62NEZC', CAST(N'2015-03-23T07:05:01.490' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (9, N'Hilary Wu', N'qjbebgl9@bpsiojn.fabmke.org', N'CHECKOUTSTAFF', N'69C5OVORBP', N'86TP1FLTKT', CAST(N'2018-02-13T07:06:41.580' AS DateTime))
INSERT [dbo].[Staff] ([Id], [Name], [Email], [Role], [PasswordHash], [PasswordSalt], [PasswordLastChanged]) VALUES (10, N'Shane Andrade', N'wracmacl.pdljsqyg@fwdmpa.org', N'CHIEFLIBRARIAN', N'75R0JF2JUY', N'PTLGWT2SFA', CAST(N'2015-06-20T23:36:30.780' AS DateTime))
SET IDENTITY_INSERT [dbo].[Staff] OFF
INSERT [dbo].[Subject] ([Name]) VALUES (N'Beverages')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Cereals')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Confections')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Dairy')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Grain')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Meat')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Poultry')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Produce')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Seafood')
INSERT [dbo].[Subject] ([Name]) VALUES (N'Shell fish')
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'1782226838439', 14, CAST(N'2018-08-23T00:05:31.620' AS DateTime), CAST(N'2018-10-30T01:24:40.540' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'2505082949381', 14, CAST(N'2018-12-16T17:24:15.740' AS DateTime), CAST(N'2019-04-05T23:05:56.150' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'4752782914', 30, CAST(N'2016-12-05T14:06:02.220' AS DateTime), CAST(N'2017-09-29T21:20:29.430' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'4849245782', 29, CAST(N'2015-04-11T16:33:31.550' AS DateTime), CAST(N'2015-08-09T11:31:16.750' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'5544977597600', 4, CAST(N'2019-06-13T11:50:05.520' AS DateTime), CAST(N'2020-02-29T07:53:38.480' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'6298387235', 14, CAST(N'2017-04-29T20:57:56.290' AS DateTime), CAST(N'2017-11-27T09:37:20.080' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'7194343474002', 7, CAST(N'2019-01-19T19:39:42.130' AS DateTime), CAST(N'2019-07-26T04:35:12.840' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'7646855215281', 18, CAST(N'2015-01-08T05:23:50.050' AS DateTime), CAST(N'2015-07-21T22:17:36.700' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'7833843933', 13, CAST(N'2017-12-16T08:16:58.130' AS DateTime), CAST(N'2018-10-09T07:29:09.220' AS DateTime))
INSERT [dbo].[WishlistLine] ([MaterialISBN], [Amount], [DateCreated], [DateUpdated]) VALUES (N'9842228942363', 29, CAST(N'2016-05-17T23:40:17.090' AS DateTime), CAST(N'2017-01-24T01:09:05.650' AS DateTime))
ALTER TABLE [dbo].[Copy]  WITH CHECK ADD  CONSTRAINT [FK_Copy_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[Copy] CHECK CONSTRAINT [FK_Copy_Material]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Copy] FOREIGN KEY([CopyBarcode])
REFERENCES [dbo].[Copy] ([Barcode])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Copy]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Library] FOREIGN KEY([LibraryName])
REFERENCES [dbo].[Library] ([Name])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Library]
GO
ALTER TABLE [dbo].[Loan]  WITH CHECK ADD  CONSTRAINT [FK_Loan_Member] FOREIGN KEY([MemberSsn])
REFERENCES [dbo].[Member] ([Ssn])
GO
ALTER TABLE [dbo].[Loan] CHECK CONSTRAINT [FK_Loan_Member]
GO
ALTER TABLE [dbo].[LoanerCard]  WITH CHECK ADD  CONSTRAINT [FK_LoanerCard_Member] FOREIGN KEY([MemberSsn])
REFERENCES [dbo].[Member] ([Ssn])
GO
ALTER TABLE [dbo].[LoanerCard] CHECK CONSTRAINT [FK_LoanerCard_Member]
GO
ALTER TABLE [dbo].[MaterialAuthor]  WITH CHECK ADD  CONSTRAINT [FK_MaterialAuthor_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([Id])
GO
ALTER TABLE [dbo].[MaterialAuthor] CHECK CONSTRAINT [FK_MaterialAuthor_Author]
GO
ALTER TABLE [dbo].[MaterialAuthor]  WITH CHECK ADD  CONSTRAINT [FK_MaterialAuthor_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[MaterialAuthor] CHECK CONSTRAINT [FK_MaterialAuthor_Material]
GO
ALTER TABLE [dbo].[MaterialPublisher]  WITH CHECK ADD  CONSTRAINT [FK_MaterialPublisher_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[MaterialPublisher] CHECK CONSTRAINT [FK_MaterialPublisher_Material]
GO
ALTER TABLE [dbo].[MaterialPublisher]  WITH CHECK ADD  CONSTRAINT [FK_MaterialPublisher_Publisher] FOREIGN KEY([PublisherName])
REFERENCES [dbo].[Publisher] ([Name])
GO
ALTER TABLE [dbo].[MaterialPublisher] CHECK CONSTRAINT [FK_MaterialPublisher_Publisher]
GO
ALTER TABLE [dbo].[MaterialSubject]  WITH CHECK ADD  CONSTRAINT [FK_MaterialSubject_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[MaterialSubject] CHECK CONSTRAINT [FK_MaterialSubject_Material]
GO
ALTER TABLE [dbo].[MaterialSubject]  WITH CHECK ADD  CONSTRAINT [FK_MaterialSubject_Subject] FOREIGN KEY([SubjectName])
REFERENCES [dbo].[Subject] ([Name])
GO
ALTER TABLE [dbo].[MaterialSubject] CHECK CONSTRAINT [FK_MaterialSubject_Subject]
GO
ALTER TABLE [dbo].[MemberAddress]  WITH CHECK ADD  CONSTRAINT [FK_MemberAddress_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[MemberAddress] CHECK CONSTRAINT [FK_MemberAddress_Address]
GO
ALTER TABLE [dbo].[MemberAddress]  WITH CHECK ADD  CONSTRAINT [FK_MemberAddress_Member] FOREIGN KEY([MemberSsn])
REFERENCES [dbo].[Member] ([Ssn])
GO
ALTER TABLE [dbo].[MemberAddress] CHECK CONSTRAINT [FK_MemberAddress_Member]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Loan] FOREIGN KEY([LoanId])
REFERENCES [dbo].[Loan] ([Id])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Loan]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_LoanerCard] FOREIGN KEY([LoanerCardBarcode])
REFERENCES [dbo].[LoanerCard] ([Barcode])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_LoanerCard]
GO
ALTER TABLE [dbo].[PhoneNumber]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Member] FOREIGN KEY([MemberSsn])
REFERENCES [dbo].[Member] ([Ssn])
GO
ALTER TABLE [dbo].[PhoneNumber] CHECK CONSTRAINT [FK_Phone_Member]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Material]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Member] FOREIGN KEY([MemberSsn])
REFERENCES [dbo].[Member] ([Ssn])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Member]
GO
ALTER TABLE [dbo].[WishlistLine]  WITH CHECK ADD  CONSTRAINT [FK_WishlistLine_Material] FOREIGN KEY([MaterialISBN])
REFERENCES [dbo].[Material] ([ISBN])
GO
ALTER TABLE [dbo].[WishlistLine] CHECK CONSTRAINT [FK_WishlistLine_Material]
GO
