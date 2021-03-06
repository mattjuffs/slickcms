/****** Object:  Database [slickcms]    Script Date: 25/03/2019 15:59:27 ******/
CREATE DATABASE [slickcms]  (EDITION = 'Basic', SERVICE_OBJECTIVE = 'Basic', MAXSIZE = 2 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [slickcms] SET COMPATIBILITY_LEVEL = 130
GO
ALTER DATABASE [slickcms] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [slickcms] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [slickcms] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [slickcms] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [slickcms] SET ARITHABORT OFF 
GO
ALTER DATABASE [slickcms] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [slickcms] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [slickcms] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [slickcms] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [slickcms] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [slickcms] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [slickcms] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [slickcms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [slickcms] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [slickcms] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [slickcms] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [slickcms] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [slickcms] SET  MULTI_USER 
GO
ALTER DATABASE [slickcms] SET ENCRYPTION ON
GO
ALTER DATABASE [slickcms] SET QUERY_STORE = ON
GO
ALTER DATABASE [slickcms] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
/****** Object:  DatabaseScopedCredential [https://slickhouse.blob.core.windows.net/sqldbauditlogs]    Script Date: 25/03/2019 15:59:28 ******/
CREATE DATABASE SCOPED CREDENTIAL [https://slickhouse.blob.core.windows.net/sqldbauditlogs] WITH IDENTITY = N'SHARED ACCESS SIGNATURE'
GO
/****** Object:  User [slickcms]    Script Date: 25/03/2019 15:59:28 ******/
CREATE USER [slickcms] WITH PASSWORD=N'zEo7O0pdVdSTgsGmkA7bONJlt35x1yBYfbASh5UWLVc=', DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'slickcms'
GO
/****** Object:  Table [dbo].[tbl_Category]    Script Date: 25/03/2019 15:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Type] [varchar](255) NOT NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Comment]    Script Date: 25/03/2019 15:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Comment](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[URL] [varchar](1024) NOT NULL,
	[IP] [varchar](45) NOT NULL,
	[HTTP_USER_AGENT] [varchar](1024) NOT NULL,
	[Content] [varchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Published] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Image]    Script Date: 25/03/2019 15:59:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Image](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Album] [varchar](255) NOT NULL,
	[Orientation] [varchar](10) NOT NULL,
	[Uploaded] [datetime2](7) NOT NULL,
	[Alt] [varchar](50) NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Caption] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Link]    Script Date: 25/03/2019 15:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Link](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[URL] [varchar](1024) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Published] [int] NOT NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Post]    Script Date: 25/03/2019 15:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Post](
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[URL] [varchar](1024) NOT NULL,
	[Summary] [varchar](max) NOT NULL,
	[Content] [varchar](max) NOT NULL,
	[Search] [varchar](255) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Published] [int] NOT NULL,
	[Pageable] [int] NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Relationship]    Script Date: 25/03/2019 15:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Relationship](
	[RelationshipID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[LinkID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_Relationships] PRIMARY KEY CLUSTERED 
(
	[RelationshipID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_Tag]    Script Date: 25/03/2019 15:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Tag](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 25/03/2019 15:59:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UUID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](1024) NOT NULL,
	[URL] [varchar](1024) NOT NULL,
	[IP] [varchar](45) NOT NULL,
	[Biography] [varchar](max) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Active] [int] NOT NULL,
	[LoginFails] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[tbl_Category] ADD  CONSTRAINT [DF_Categories_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[tbl_Category] ADD  CONSTRAINT [DF_Categories_Type]  DEFAULT ('') FOR [Type]
GO
ALTER TABLE [dbo].[tbl_Category] ADD  CONSTRAINT [DF_Categories_Description]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_HTTP_USER_AGENT]  DEFAULT ('') FOR [HTTP_USER_AGENT]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_Content]  DEFAULT ('') FOR [Content]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tbl_Comment] ADD  CONSTRAINT [DF_Comments_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF_Images_Album]  DEFAULT ('') FOR [Album]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF_Images_Size]  DEFAULT ('') FOR [Orientation]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF__Images__Uploaded__2F10007B]  DEFAULT (getdate()) FOR [Uploaded]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF_Images_Alt]  DEFAULT ('') FOR [Alt]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF_Images_Title]  DEFAULT ('') FOR [Title]
GO
ALTER TABLE [dbo].[tbl_Image] ADD  CONSTRAINT [DF_Images_Caption]  DEFAULT ('') FOR [Caption]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_URL]  DEFAULT ('') FOR [URL]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_Description]  DEFAULT ('') FOR [Description]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tbl_Link] ADD  CONSTRAINT [DF_Links_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Title]  DEFAULT ('') FOR [Title]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_URL]  DEFAULT ('') FOR [URL]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Summary]  DEFAULT ('') FOR [Summary]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Content]  DEFAULT ('') FOR [Content]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Search]  DEFAULT ('') FOR [Search]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[tbl_Post] ADD  CONSTRAINT [DF_Posts_Pageable]  DEFAULT ((0)) FOR [Pageable]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_CategoryID]  DEFAULT ((0)) FOR [CategoryID]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_LinkID]  DEFAULT ((0)) FOR [LinkID]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_PostID]  DEFAULT ((0)) FOR [PostID]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_UserID]  DEFAULT ((0)) FOR [UserID]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_TagID]  DEFAULT ((0)) FOR [TagID]
GO
ALTER TABLE [dbo].[tbl_Relationship] ADD  CONSTRAINT [DF_Relationships_Order]  DEFAULT ((0)) FOR [Order]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_Name]  DEFAULT ('') FOR [Name]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_Email]  DEFAULT ('') FOR [Email]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_Password]  DEFAULT ('') FOR [Password]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_URL]  DEFAULT ('') FOR [URL]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_IP]  DEFAULT ('') FOR [IP]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_Biography]  DEFAULT ('') FOR [Biography]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_DateModified]  DEFAULT (getdate()) FOR [DateModified]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((0)) FOR [Active]
GO
ALTER TABLE [dbo].[tbl_User] ADD  CONSTRAINT [DF_Users_LoginFails]  DEFAULT ((0)) FOR [LoginFails]
GO
ALTER DATABASE [slickcms] SET  READ_WRITE 
GO
