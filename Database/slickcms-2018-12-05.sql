/****** Object:  Table [dbo].[tbl_Category]    Script Date: 2018-12-05 13:24:20 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Comment]    Script Date: 2018-12-05 13:24:20 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Image]    Script Date: 2018-12-05 13:24:20 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Link]    Script Date: 2018-12-05 13:24:21 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Post]    Script Date: 2018-12-05 13:24:21 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Relationship]    Script Date: 2018-12-05 13:24:21 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_Tag]    Script Date: 2018-12-05 13:24:21 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 2018-12-05 13:24:21 ******/
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
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

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
/****** Object:  StoredProcedure [dbo].[Admin_DeleteCategory]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Delets a category from the database, including all relationships associated with it
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteCategory]
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Categories Where CategoryID = @CategoryID
	Delete From dbo.Relationships Where CategoryID = @CategoryID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeleteComment]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-10-01
-- Description:	Delets a comment from the database
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteComment]
(
	@CommentID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Comments Where CommentID = @CommentID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeleteLink]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-27
-- Description:	Delets a link from the database, including all relationships associated with it
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteLink]
(
	@LinkID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Links Where LinkID = @LinkID
	Delete From dbo.Relationships Where LinkID = @LinkID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeletePost]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-01-01
-- Description:	Delets a post from the database, including all comments and relationships associated with it
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeletePost]
(
	@PostID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Comments Where PostID = @PostID
	Delete From dbo.Posts Where PostID = @PostID
	Delete From dbo.Relationships Where PostID = @PostID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeleteRelationship]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Deletes a relationship from the database
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteRelationship]
(
	@RelationshipID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Relationships Where RelationshipID = @RelationshipID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeleteTag]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2010-01-07
-- Description:	Delets a tag from the database, including all relationships associated with it
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteTag]
(
	@TagID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Tags Where TagID = @TagID
	Delete From dbo.Relationships Where TagID = @TagID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_DeleteUser]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-31
-- Description:	Delets a user from the database, including all relationships associated with it
-- =============================================
CREATE PROCEDURE [dbo].[Admin_DeleteUser]
(
	@UserID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Delete From dbo.Users Where UserID = @UserID
	Delete From dbo.Relationships Where UserID = @UserID
	Update dbo.Posts Set UserID = 0 Where UserID = @UserID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_IncrementLoginCounter]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Increments the login counter
-- =============================================
CREATE PROCEDURE [dbo].[Admin_IncrementLoginCounter]
(
	@Email varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Users
	Set [LoginFails] = (LoginFails + 1)
	Where [Email] = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertCategory]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-28
-- Description:	Used for inserting new categories into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertCategory]
(
	@Name varchar(255),
	@Type varchar(255),
	@Description text
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into dbo.Categories(
		[Name],
		[Type],
		[Description]
	)Values(
		@Name,
		@Type,
		@Description
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertImage]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-07-27
-- Description:	Inserts an Image into dbo.Images
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertImage]
(
	@Name varchar(255),
	@Album varchar(255),
	@Orientation varchar(10),
	@Alt varchar(50),
	@Title varchar(255),
	@Caption varchar(1024)
)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [dbo].[Images](Name, Album, Orientation, Uploaded, Alt, Title, Caption)
    VALUES (
		@Name,
		@Album,
		@Orientation,
		GETDATE(),
		@Alt,
		@Title,
		@Caption
	)
END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertLink]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-26
-- Description:	Used for inserting new links into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertLink]
(
	@Name varchar(255),
	@URL varchar(1024),
	@Description text,
	@Published int
)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [dbo].[Links](
		[Name]
		,[URL]
		,[Description]
		,[DateCreated]
		,[DateModified]
		,[Published]
	)
	VALUES(
		@Name
		,@URL
		,@Description
		,GETDATE()
		,GETDATE()
		,@Published
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertPost]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2008-12-13
-- Description:	Used for inserting new posts into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertPost]
(
	@UserID int,
	@Title varchar(255),
	@Summary text,
	@Content text,
	@Search varchar(255),
	@Published int,
	@Pageable int,
	@URL varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into dbo.Posts (
		[UserID],
		[Title],
		[Summary],
		[Content],
		[Search],
		[DateCreated],
		[DateModified],
		[Published],
		[Pageable],
		[URL]
	)
	Values (
		@UserID,
		@Title,
		@Summary,
		@Content,
		@Search,
		GetDate(),
		GetDate(),
		@Published,
		@Pageable,
		@URL
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertRelationship]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-28
-- Description:	Used for inserting new relationships into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertRelationship]
(
	@CategoryID int,
	@LinkID int,
	@PostID int,
	@UserID int,
	@TagID int,
	@Order int
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into dbo.Relationships (
		[CategoryID],
		[LinkID],
		[PostID],
		[UserID],
		[TagID],
		[Order]
	)Values(
		@CategoryID,
		@LinkID,
		@PostID,
		@UserID,
		@TagID,
		@Order
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertTag]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-07
-- Description:	Used for inserting new tags into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertTag]
(
	@Name varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into dbo.Tags(
		[Name]
	)Values(
		@Name
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_InsertUser]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Inserts a new user
-- =============================================
CREATE PROCEDURE [dbo].[Admin_InsertUser]
(
	@Name varchar(50),
	@Email varchar(255),
	@Password varchar(32),
	@URL varchar(1024),
	@IP varchar(15),
	@Biography text,
	@Active int,
	@LoginFails int
)
AS
BEGIN

	SET NOCOUNT ON;

    Insert Into [dbo].[Users](
		[UUID]
		,[Name]
		,[Email]
		,[Password]
		,[URL]
		,[IP]
		,[Biography]
		,[DateCreated]
		,[DateModified]
		,[Active]
		,[LoginFails]
	)
	Values(
		NEWID()
		,@Name
		,@Email
		,@Password
		,@URL
		,@IP
		,@Biography
		,GETDATE()
		,GETDATE()
		,@Active
		,@LoginFails
	)
END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectCategories]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Gets categories for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectCategories]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[CategoryID],
		[Name],
		[Type]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [Type], [Name]
		) As
		[RowNumber],
		[CategoryID],
		[Name],
		[Type]
	From dbo.Categories
	) As CategoriesWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectCategory]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Gets a category for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectCategory]
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		CategoryID,
		[Name],
		[Type],
		[Description]
	From dbo.Categories
	Where CategoryID = @CategoryID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectComment]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-05
-- Description:	Gets a comment for editing within the Admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectComment]
(
	@CommentID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[CommentID],
		[PostID],
		[UserID],
		[Name],
		[Email],
		[URL],
		[IP],
		[HTTP_USER_AGENT],
		[Content],
		[DateCreated],
		[DateModified],
		[Published]
	From dbo.Comments
	Where [CommentID] = @CommentID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectComments]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-05
-- Description:	Retrieves the Comments for the Admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectComments]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Select
		[CommentID],
		[PostID],
		(Select [Title] From dbo.Posts P Where P.PostID = CommentsWithRowNumbers.PostID) As [PostTitle],
		[UserID],
		[Name],
		[Email],
		[URL],
		[IP],
		LEFT(CAST(Content As varchar(50)),50) As [Content],
		[DateModified],
		[Published]
	From (
		Select ROW_NUMBER()
		Over (
			Order By Published Asc, DateCreated Desc
		) As
		[RowNumber],
		[CommentID],
		[PostID],
		(Select [Title] From dbo.Posts P Where P.PostID = C.PostID) As [PostTitle],
		[UserID],
		[Name],
		[Email],
		[URL],
		[IP],
		LEFT(CAST(Content As varchar(50)),50) As [Content],
		[DateModified],
		[Published]
	From dbo.Comments C
	) As CommentsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectLink]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-27
-- Description:	Gets all information for a link
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectLink]
(
	@LinkID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[LinkID]
		,[Name]
		,[URL]
		,[Description]
		,[DateCreated]
		,[DateModified]
		,[Published]
	From [dbo].[Links]
	Where [LinkID] = @LinkID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectLinks]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-26
-- Description:	Gets links for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectLinks]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[LinkID]
		,[Name]
		,[URL]
		,[DateModified]
		,[Published]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [LinkID] Desc
		) As
		[RowNumber]
		,[LinkID]
		,[Name]
		,[URL]
		,[DateModified]
		,[Published]
	From [dbo].[Links]
	) As LinksWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectPost]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Juffs
-- Create date: 2008-12-13
-- Description:	Gets all information for a post
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectPost]
(
	@PostID int
)
AS
BEGIN

	SET NOCOUNT ON;

    Select
		[PostID],
		[UserID],
		IsNULL((Select [Name] From Users Where UserID = P.UserID),'N/A') As [Author],
		[Title],
		[Summary],
		[Content],
		[Search],
		[DateCreated],
		[DateModified],
		[Published],
		[Pageable],
		[URL]
	From dbo.Posts P
	Where [PostID] = @PostID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectPostID]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matt Juffs
-- Create date: 2010-09-21
-- Description:	Gets the last PostID
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectPostID]
AS
BEGIN

	SET NOCOUNT ON;

	Select Top 1 PostID
	From dbo.Posts
	Order By DateModified Desc

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectPosts]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matt Juffs
-- Create date: 2008-12-13
-- Description:	Gets posts for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectPosts]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[PostID],
		[UserID],
		IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As [Author],
		[Title],
		[Summary],
		[DateCreated],
		[DateModified],
		[Published],
		[Pageable]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [Pageable] Desc, [DateCreated] Desc
		) As
			[RowNumber],
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As [Author],
			[Title],
			[Summary],
			[DateCreated],
			[DateModified],
			[Published],
			[Pageable]
		From Posts P
	) As PostsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectRelationship]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Gets a relationship for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectRelationship]
(
	@RelationshipID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[RelationshipID],
		[CategoryID],
		[LinkID],
		[PostID],
		[UserID],
		[TagID],
		[Order]
	From dbo.[Relationships] R
	Where R.RelationshipID = @RelationshipID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectRelationships]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2009-08-28
-- Description:	Gets relationships for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectRelationships]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Select
		[RelationshipID],
		[CategoryID],
		ISNULL((Select Name From dbo.Categories C Where C.CategoryID = RelationshipsWithRowNumbers.CategoryID),'') As [CategoryName],
		[LinkID],
		ISNULL((Select Name From dbo.Links L Where L.LinkID = RelationshipsWithRowNumbers.LinkID),'') As [LinkName],
		[PostID],
		ISNULL((Select Title From dbo.Posts P Where P.PostID = RelationshipsWithRowNumbers.PostID),'') As [PostTitle],
		[UserID],
		ISNULL((Select Name From dbo.Users U Where U.UserID = RelationshipsWithRowNumbers.UserID),'') As [UserName],
		[TagID],
		ISNULL((Select Name From dbo.Tags T Where T.TagID = RelationshipsWithRowNumbers.TagID),'') As [TagName],
		[Order]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [RelationshipID] Desc
		) As
		[RowNumber],
		[RelationshipID],
		[CategoryID],
		ISNULL((Select Name From dbo.Categories C Where C.CategoryID = R.CategoryID),'') As [CategoryName],
		[LinkID],
		ISNULL((Select Name From dbo.Links L Where L.LinkID = R.LinkID),'') As [LinkName],
		[PostID],
		ISNULL((Select Title From dbo.Posts P Where P.PostID = R.PostID),'') As [PostTitle],
		[UserID],
		ISNULL((Select Name From dbo.Users U Where U.UserID = R.UserID),'') As [UserName],
		[TagID],
		ISNULL((Select Name From dbo.Tags T Where T.TagID = R.TagID),'') As [TagName],
		[Order]
	From dbo.[Relationships] R
	) As RelationshipsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectTag]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matt Juffs
-- Create date: 2010-01-07
-- Description:	Gets a tag for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectTag]
(
	@TagID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[TagID],
		[Name]
	From dbo.Tags
	Where TagID = @TagID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectTags]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matt Juffs
-- Create date: 2010-01-07
-- Description:	Gets tags for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectTags]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Select
		[TagID],
		[Name]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [Name]
		) As
		[RowNumber],
		[TagID],
		[Name]
	From dbo.Tags
	) As TagsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectUser]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Selects users for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectUser]
(
	@UserID int
)
AS
BEGIN

	SET NOCOUNT ON;

    Select
		[UserID]
		,[Name]
		,[Email]
		,[URL]
		,[IP]
		,[Biography]
		,[DateCreated]
		,[DateModified]
		,[Active]
		,[LoginFails]
	From [dbo].[Users]
	Where [UserID] = @UserID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_SelectUsers]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Selects users for the admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_SelectUsers]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Select
		[UserID]
		,[Name]
		,[Email]
		,[URL]
		,[DateModified]
		,[Active]
		,[LoginFails]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [Name]
		) As
		[RowNumber]
		,[UserID]
		,[Name]
		,[Email]
		,[URL]
		,[DateModified]
		,[Active]
		,[LoginFails]
	From [dbo].[Users]
	) As UsersWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateCategory]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-28
-- Description:	Used for updating a category
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateCategory]
(
	@CategoryID int,
	@Name varchar(255),
	@Type varchar(255),
	@Description text
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Categories
	Set
		[Name] = @Name,
		[Type] = @Type,
		[Description] = @Description
	Where [CategoryID] = @CategoryID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateComment]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-01
-- Description:	Used for updating a comment
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateComment]
(
	@CommentID int,
	@PostID int,
	@UserID int,
	@Name varchar(50),
	@Email varchar(255),
	@URL varchar(1024),
	@Content text,
	@Published int
)
AS
BEGIN

	SET NOCOUNT ON;

	Update [Comments]
	Set
		[PostID] = @PostID,
		[UserID] = @UserID,
		[Name] = @Name,
		[Email] = @Email,
		[URL] = @URL,
		[Content] = @Content,
		[DateModified] = GETDATE(),
		[Published] = @Published
	Where [CommentID] = @CommentID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateLink]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-27
-- Description:	Used for updating a link
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateLink]
(
	@LinkID int,
	@Name varchar(255),
	@URL varchar(1024),
	@Description text,
	@Published int
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Links
	Set
		[Name] = @Name,
		[URL] = @URL,
		[Description] = @Description,
		[DateModified] = GETDATE(),
		[Published] = @Published
	Where [LinkID] = @LinkID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdatePassword]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Updates a Password once reset
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdatePassword]
(
	@Email varchar(255),
	@Password varchar(32)
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Update [dbo].[Users]
	Set	[Password] = @Password
	Where [Email] = @Email

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdatePost]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2008-12-13
-- Description:	Used for updating a post
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdatePost]
(
	@PostID int,
	@UserID int,
	@Title varchar(255),
	@Summary text,
	@Content text,
	@Search varchar(255),
	@Published int,
	@Pageable int,
	@URL varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Posts
	Set
		[UserID] = @UserID,
		[Title] = @Title,
		[Summary] = @Summary,
		[Content] = @Content,
		[Search] = @Search,
		[DateModified] = GetDate(),
		[Published] = @Published,
		[Pageable] = @Pageable,
		[URL] = @URL
	Where [PostID] = @PostID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateRelationship]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-28
-- Description:	Used for inserting new relationships into DB
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateRelationship]
(
	@RelationshipID int,
	@CategoryID int,
	@LinkID int,
	@PostID int,
	@UserID int,
	@TagID int,
	@Order int
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Relationships
	Set
		[CategoryID] = @CategoryID,
		[LinkID] = @LinkID,
		[PostID] = @PostID,
		[UserID] = @UserID,
		[TagID] = @TagID,
		[Order] = @Order
	Where RelationshipID = @RelationshipID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateTag]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-07
-- Description:	Used for updating a tag
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateTag]
(
	@TagID int,
	@Name varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

	Update dbo.Tags
	Set
		[Name] = @Name
	Where [TagID] = @TagID

END
GO
/****** Object:  StoredProcedure [dbo].[Admin_UpdateUser]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Used for updating a user within the Admin
-- =============================================
CREATE PROCEDURE [dbo].[Admin_UpdateUser]
(
	@UserID int,
	@Name varchar(50),
	@Email varchar(255),
	@Password varchar(32),
	@URL varchar(1024),
	@IP varchar(15),
	@Biography text,
	@Active int,
	@LoginFails int
)
AS
BEGIN

	SET NOCOUNT ON;

	If LEN(@Password)!=32
		Begin
			Update [dbo].[Users]
			Set
				[Name] = @Name
				,[Email] = @Email
				,[URL] = @URL
				,[IP] = @IP
				,[Biography] = @Biography
				,[DateModified] = GETDATE()
				,[Active] = @Active
				,[LoginFails] = @LoginFails
			Where [UserID] = @UserID
		End
	Else
		Begin
			Update [dbo].[Users]
			Set
				[Name] = @Name
				,[Email] = @Email
				,[Password] = @Password
				,[URL] = @URL
				,[IP] = @IP
				,[Biography] = @Biography
				,[DateModified] = GETDATE()
				,[Active] = @Active
				,[LoginFails] = @LoginFails
			Where [UserID] = @UserID
		End

	--2009-10-01 for #63
	Update [dbo].Comments
	Set
		[Name] = @Name,
		[Email] = @Email,
		[URL] = @URL
	Where [UserID] = @UserID

END
GO
/****** Object:  StoredProcedure [dbo].[CalculateDate]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-02-25
-- Description:	Calculates how long until the wedding!
-- =============================================
CREATE PROCEDURE [dbo].[CalculateDate]

AS
BEGIN

	SET NOCOUNT ON;

	Declare @Wedding datetime
	Declare @Now datetime

	Set @Wedding = '2009-05-16 12:00:00'
	--Set @Wedding = '2000-01-01 00:00:00' --debug
	Set @Now = GETDATE()

	Select
		DATEDIFF(mm,@Now,@Wedding) As [Months],
		DATEDIFF(wk,@Now,@Wedding) As [Weeks],
		DATEDIFF(dd,@Now,@Wedding) As [Days],
		DATEDIFF(hh,@Now,@Wedding) As [Hours],
		DATEDIFF(mi,@Now,@Wedding) As [Minutes],
		DATEDIFF(ss,@Now,@Wedding) As [Seconds]

END
GO
/****** Object:  StoredProcedure [dbo].[Feed_RSS20]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-11-17
-- Description:	Used for RSS 2.0 feed
-- =============================================
CREATE PROCEDURE [dbo].[Feed_RSS20]
AS
BEGIN

	SET NOCOUNT ON;

	Select Top 50
		P.[Title],
		Replace(convert(varchar, P.[DateCreated], 111), '/', '-') + '/' + P.[URL] As [URL],
		[Content],
		Left(datename(dw,P.[DateCreated]),3)+', '+convert(varchar(20),P.[DateCreated],113)+' GMT' As [DateCreated]
		,P.Pageable
	From dbo.Posts P
	Where P.[Published] = 1
	Order By P.[DateCreated] Desc

END
GO
/****** Object:  StoredProcedure [dbo].[Feed_RSS20_Comments]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-14
-- Description:	Used for RSS 2.0 Comments feed
-- =============================================
CREATE PROCEDURE [dbo].[Feed_RSS20_Comments]
AS
BEGIN

	SET NOCOUNT ON;

	Select Top 50
		'Comment on ' + P.[Title] + ' by ' + C.[Name] As [Title],
		Replace(convert(varchar, P.[DateCreated], 111), '/', '-') + '/' + P.[URL] + '/#comment-' + CAST(C.[CommentID] As varchar) As [URL],
		C.[Content],
		Left(datename(dw,C.[DateCreated]),3)+', '+convert(varchar(20),C.[DateCreated],113)+' GMT' As [DateCreated]
	From dbo.Comments C
	Inner Join Posts P On P.PostID = C.PostID
	Where C.[Published] = 1
	And P.[Published] = 1
	Order By C.[DateCreated] Desc

END
GO
/****** Object:  StoredProcedure [dbo].[Feed_Sitemap]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-12-07
-- Description:	Used for XML Sitemap
-- =============================================
CREATE PROCEDURE [dbo].[Feed_Sitemap]
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[loc] = 
		Case [Pageable]
			When 1 Then Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL]
			Else [URL]
		End,
		REPLACE(CONVERT(varchar(10), [DateCreated], 111), '/', '-') AS [lastmod],
		'monthly' As [changefreq],
		[Pageable] As [priority]
	From dbo.Posts
	Where [Published] = 1
	Order By [Pageable] Desc, [DateCreated] Desc

END
GO
/****** Object:  StoredProcedure [dbo].[Import_Comments]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-24
-- Description:	Used for Importing WordPress comments
-- =============================================
CREATE PROCEDURE [dbo].[Import_Comments]
(
	@PostID int,
	@Name varchar(50),
	@Email varchar(255),
	@URL varchar(1024),
	@IP varchar(15),
	@HTTP_USER_AGENT varchar(1024),
	@Content text,
	@DateCreated datetime
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into dbo.Comments (
		PostID, 
		UserID, 
		Name, 
		Email, 
		URL, 
		IP, 
		HTTP_USER_AGENT, 
		Content, 
		DateCreated, 
		DateModified, 
		Published
	)
	Values (
		@PostID,
		0,
		@Name,
		@Email,
		@URL,
		@IP,
		@HTTP_USER_AGENT,
		@Content,
		@DateCreated,
		@DateCreated,
		1
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Import_Posts]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-24
-- Description:	Used for importing Posts from WordPress
-- =============================================
CREATE PROCEDURE [dbo].[Import_Posts]
(
	@PostID int,
	@Title varchar(255),
	@URL varchar(255),
	@Content text,
	@DateCreated datetime,
	@DateModified datetime,
	@Pageable int
)
AS
BEGIN

	SET NOCOUNT ON;

	SET IDENTITY_INSERT dbo.Posts ON

	Insert Into dbo.Posts (
		PostID, 
		UserID, 
		Title, 
		URL, 
		Summary, 
		Content, 
		Search, 
		DateCreated, 
		DateModified, 
		Published, 
		Pageable
	)
	Values (
		@PostID,--PostID
		1,--UserID
		@Title,--Title
		@URL,--URL
		'',--Summary
		@Content,--Content
		'',--search
		@DateCreated,--date created
		@DateModified,--date modified
		1,
		@Pageable
	)

	SET IDENTITY_INSERT dbo.Posts OFF

END
GO
/****** Object:  StoredProcedure [dbo].[Import_Tags]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-24
-- Description:	Imports Tags from WordPress
-- =============================================
CREATE PROCEDURE [dbo].[Import_Tags]
(
	@PostID int,
	@Tag varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;

    Declare @TagID int
    
    Set @TagID = (
		Select TagID
		From Tags
		Where Name = @Tag
    )
    
    If @TagID IS NULL
    Begin
		Insert Into dbo.Tags (Name)
		Values (@Tag)
		Set @TagID = @@IDENTITY
    End
    
    --Select @TagID
    
    Insert Into dbo.Relationships (PostID,TagID)
    Values (@PostID,@TagID)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertComment]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-01
-- Description:	Used for inserting new comments into DB
-- =============================================
CREATE PROCEDURE [dbo].[InsertComment]
(
	@IP varchar(15),
	@HTTP_USER_AGENT varchar(1024),
	@PostID int,
	@UserID int,
	@Name varchar(50),
	@Email varchar(255),
	@URL varchar(1024),
	@Content text,
	@Published int
)
AS
BEGIN

	SET NOCOUNT ON;

	Insert Into [Comments] (
		[PostID],
		[UserID],
		[Name],
		[Email],
		[URL],
		[IP],
		[HTTP_USER_AGENT],
		[Content],
		[DateCreated],
		[DateModified],
		[Published]
	)
	Values (
		@PostID,
		@UserID,
		@Name,
		@Email,
		@URL,
		@IP,
		@HTTP_USER_AGENT,
		@Content,
		GETDATE(),
		GETDATE(),
		@Published
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-31
-- Description:	Used for login functionality
-- =============================================
CREATE PROCEDURE [dbo].[Login]
(
	@Email varchar(255),
	@Password varchar(32)
)
AS
BEGIN

	SET NOCOUNT ON;

    Select 
		[UserID],
		[Name],
		[Email],
		[URL],
		[LoginFails]
	From dbo.Users
	Where [Email] = @Email
	And [Password] = @Password
	And [Active] = 1
END
GO
/****** Object:  StoredProcedure [dbo].[Navigation]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2008-11-30
-- Description:	Used for pages navigation
-- =============================================
CREATE PROCEDURE [dbo].[Navigation]
	@CategoryID int
AS
BEGIN

	SET NOCOUNT ON;

	Select
		P.[Title],
		[URL] =
		Case [Pageable]
			When 1 Then Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL]
			Else [URL]
		End,
		P.[DateCreated]
	From dbo.Posts P
	Inner Join dbo.Relationships R On R.PostID = P.PostID
	Where P.[Published] = 1
	And R.[CategoryID] = @CategoryID
	Order By R.[Order]

END
GO
/****** Object:  StoredProcedure [dbo].[RecentComments]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-14
-- Description:	Gets the most recent published comments
-- =============================================
CREATE PROCEDURE [dbo].[RecentComments]
AS
BEGIN

	SET NOCOUNT ON;

	Select Top 5
		[CommentID],
		(Select [Title] From dbo.Posts P Where P.PostID = C.PostID) As [PostTitle],
		(
			Select
				[URL] =
				Case P.[Pageable]
					When 1 Then Replace(convert(varchar, P.[DateCreated], 111), '/', '-') + '/' + P.[URL]
					Else P.[URL]
				End
			From dbo.Posts P
			Where P.PostID = C.PostID
		) As [PostURL],
		[Name],
		LEFT(CAST(Content As varchar(200)),200) As [Content],
		[DateCreated]
	From dbo.Comments C
	Where [Published] = 1
	Order By DateCreated Desc

END
GO
/****** Object:  StoredProcedure [dbo].[SearchPosts]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-08-30
-- Description:	Used for site search
-- =============================================
CREATE PROCEDURE [dbo].[SearchPosts]
(
	@Keywords varchar(100)
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[Title],
		[Summary],
		[URL] =
		Case [Pageable]
			When 1 Then Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL]
			Else [URL]
		End,
		[DateCreated]
	From dbo.Posts P
	Where Published = 1
	And (
		[Title] Like '%' + @Keywords + '%'
		Or [Summary] Like '%' + @Keywords + '%'
		Or [Search] Like '%' + @Keywords + '%'
		Or CAST(Content As varchar(8000)) Like '%' + @Keywords + '%'
		Or (Select [Name] From dbo.Users U Where U.UserID = P.UserID) Like '%' + @Keywords + '%'
	)
	Order By [DateCreated] Desc
	    
END
GO
/****** Object:  StoredProcedure [dbo].[SelectArchives]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-02
-- Description:	Selects Yearly, Monthly or Daily Archives
-- =============================================
CREATE PROCEDURE [dbo].[SelectArchives]
(
	@Type varchar(10)
)
AS
BEGIN

	SET NOCOUNT ON;
	
	If @Type = 'daily'
	Begin
		--daily archives
		Select Distinct Convert(varchar,DateCreated,111) as [Archive], Count([PostID]) as [PostCount]
		From dbo.Posts
		Where [Pageable] = 1
		And [Published] = 1
		Group By Convert(varchar,DateCreated,111)
		Order By Archive Desc
	End
	
	If @Type = 'monthly'
	Begin
		--monthly archives
		Select Distinct Left(Convert(varchar,DateCreated,111),7) as [Archive], Count([PostID]) as [PostCount]
		From dbo.Posts
		Where [Pageable] = 1
		And [Published] = 1
		Group By Left(Convert(varchar,DateCreated,111),7)
		Order By Archive Desc
	End
	
	If @Type = 'yearly'
	Begin
		--yearly archives
		Select Distinct Left(Convert(varchar,DateCreated,111),4) as [Archive], Count([PostID]) as [PostCount]
		From dbo.Posts
		Where [Pageable] = 1
		And [Published] = 1
		Group By Left(Convert(varchar,DateCreated,111),4)
		Order By Archive Desc
	End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectCategories]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-04
-- Description:	Selects a list of Categories
-- =============================================
CREATE PROCEDURE [dbo].[SelectCategories]
AS
BEGIN

	SET NOCOUNT ON;

	Select
		C.CategoryID,
		C.Name,
		C.Type,
		C.Description,
		(Select COUNT(PostID) From dbo.Relationships R Where R.CategoryID = C.CategoryID) As [PostCount]
	From dbo.Categories C
	Where Type = 'Posts'
	Order By C.Name Asc

END
GO
/****** Object:  StoredProcedure [dbo].[SelectComments]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-10-03
-- Description:	Gets all comments for a post
-- =============================================
CREATE PROCEDURE [dbo].[SelectComments]
(
	@PostID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select
		[CommentID],
		[Name],
		[URL],
		[Content],
		[DateCreated]
	From dbo.Comments
	Where PostID = @PostID
	And Published = 1
	Order By DateCreated Asc

END
GO
/****** Object:  StoredProcedure [dbo].[SelectLinks]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2008-12-12
-- Description:	Gets links for a specific category, or all links
-- =============================================
CREATE PROCEDURE [dbo].[SelectLinks] 
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON;

	If @CategoryID != 0
		Begin
			Select [Name],[URL],[Description]
			From dbo.Links
			Where [Published] = 1
			And [LinkID] In (
				Select [LinkID] From [Relationships] Where [CategoryID] = @CategoryID And [LinkID] != 0
			)
			Order By [Name] Asc
		End
	Else
		Begin
			Select [Name],[URL],[Description]
			From dbo.Links
			Where [Published] = 1
			Order By [Name] Asc
		End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPost]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2008-11-30
-- Description:	Selects a single post
-- =============================================
CREATE PROCEDURE [dbo].[SelectPost]
(
	@URL varchar(255)
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Declare @HavePost int
	
	Set @HavePost = (Select COUNT(*) From dbo.Posts Where Published = 1 And [URL] = @URL)
	
	If @HavePost > 0
		Begin
			Select
				[PostID],
				[UserID],
				(Select [Name] From Users Where UserID = P.UserID) As Author,
				[Title],
				[Summary],
				[Content],
				[DateCreated],
				[DateModified],
				[URL] =
				Case [Pageable]
					When 1 Then Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL]
					Else [URL]
				End
			From dbo.Posts P
			Where Published = 1
			And [URL] = @URL
		End
	Else
		Begin
			Select
				[PostID],
				[UserID],
				(Select [Name] From Users Where UserID = P.UserID) As Author,
				[Title],
				[Summary],
				[Content],
				[DateCreated],
				[DateModified],
				'' As [URL]
			From dbo.Posts P
			Where [URL] = '404'
		End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPostCategories]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-06
-- Description:	Selects Category Names for a given Post
-- =============================================
CREATE PROCEDURE [dbo].[SelectPostCategories]
(
	@PostID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select C.[Name], C.[Description]
	From [Categories] C
	Inner Join [Relationships] R On R.[CategoryID] = C.[CategoryID]
	Where R.[PostID] = @PostID
	And C.[Type] = 'Posts'
	Order By C.[Name] Asc

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPosts]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-09-09
-- Description:	Selects a page of posts
-- =============================================
CREATE PROCEDURE [dbo].[SelectPosts]
(
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	--Thanks to: http://oassaf.wordpress.com/2006/11/20/paging-records-using-sql-2005-row_number-function/
	
	Select
		[PostID],
		[UserID],
		IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
		[Title],
		--[URL],
		Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
		[Summary],
		[Content],
		[DateCreated],
		[DateModified]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [DateCreated] Desc
		) As
			[RowNumber],
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
			[Title],
			[URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
	) As PostsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPostsArchive]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2009-12-23
-- Description:	Selects Archived posts
-- =============================================
CREATE PROCEDURE [dbo].[SelectPostsArchive]
(
	@Year int,
	@Month int,
	@Day int,
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	If @Year > 0 And @Month > 0 And @Day > 0
	--YYYY/MM/DD Archive
	Begin	
		Select
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
			[Title],
			Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From (
			Select ROW_NUMBER()
			Over (
				Order By [DateCreated] Desc
			) As
				[RowNumber],
				[PostID],
				[UserID],
				IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
				[Title],
				[URL],
				[Summary],
				[Content],
				[DateCreated],
				[DateModified]
			From dbo.Posts P
			Where [Pageable] = 1
			And [Published] = 1
			And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
			And @Month = CAST(DATEPART(mm,DateCreated) As int)
			And @Day = CAST(DATEPART(dd,DateCreated) As int)
		) As PostsWithRowNumbers
		Where [RowNumber] >= @Start
		And [RowNumber] <= @End
	End
	
	If @Year > 0 And @Month > 0 And @Day = 0
	--YYYY/MM Archive
	Begin
		Select
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
			[Title],
			Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From (
			Select ROW_NUMBER()
			Over (
				Order By [DateCreated] Desc
			) As
				[RowNumber],
				[PostID],
				[UserID],
				IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
				[Title],
				[URL],
				[Summary],
				[Content],
				[DateCreated],
				[DateModified]
			From dbo.Posts P
			Where [Pageable] = 1
			And [Published] = 1
			And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
			And @Month = CAST(DATEPART(mm,DateCreated) As int)
		) As PostsWithRowNumbers
		Where [RowNumber] >= @Start
		And [RowNumber] <= @End
	End

	If @Year > 0 And @Month = 0 And @Day = 0
	--YYYY Archive
	Begin
		Select
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
			[Title],
			Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From (
			Select ROW_NUMBER()
			Over (
				Order By [DateCreated] Desc
			) As
				[RowNumber],
				[PostID],
				[UserID],
				IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
				[Title],
				[URL],
				[Summary],
				[Content],
				[DateCreated],
				[DateModified]
			From dbo.Posts P
			Where [Pageable] = 1
			And [Published] = 1
			And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
		) As PostsWithRowNumbers
		Where [RowNumber] >= @Start
		And [RowNumber] <= @End
	End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPostsCategory]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-06
-- Description:	Selects Category posts
-- =============================================
CREATE PROCEDURE [dbo].[SelectPostsCategory]
(
	@CategoryName varchar(255),
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Declare @CategoryID int
	
	Set @CategoryID = (
		Select [CategoryID]
		From dbo.Categories
		Where [Name] = @CategoryName
	)
	
	Select
		[PostID],
		[UserID],
		IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
		[Title],
		Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
		[Summary],
		[Content],
		[DateCreated],
		[DateModified]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [DateCreated] Desc
		) As
			[RowNumber],
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
			[Title],
			[URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And [PostID] In (
			Select [PostID]
			From dbo.Relationships
			Where [CategoryID] = @CategoryID
		)
	) As PostsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPostsTag]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-07
-- Description:	Selects Tag posts
-- =============================================
CREATE PROCEDURE [dbo].[SelectPostsTag]
(
	@TagName varchar(255),
	@Start int,
	@End int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	Declare @TagID int
	
	Set @TagID = (
		Select [TagID]
		From dbo.[Tags]
		Where [Name] = @TagName
	)
	
	Select
		[PostID],
		[UserID],
		IsNull((Select [Name] From Users Where UserID = PostsWithRowNumbers.UserID),'N/A') As Author,
		[Title],
		Replace(convert(varchar, [DateCreated], 111), '/', '-') + '/' + [URL] As [URL],
		[Summary],
		[Content],
		[DateCreated],
		[DateModified]
	From (
		Select ROW_NUMBER()
		Over (
			Order By [DateCreated] Desc
		) As
			[RowNumber],
			[PostID],
			[UserID],
			IsNull((Select [Name] From Users Where UserID = P.UserID),'N/A') As Author,
			[Title],
			[URL],
			[Summary],
			[Content],
			[DateCreated],
			[DateModified]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And [PostID] In (
			Select [PostID]
			From dbo.Relationships
			Where [TagID] = @TagID
		)
	) As PostsWithRowNumbers
	Where [RowNumber] >= @Start
	And [RowNumber] <= @End

END
GO
/****** Object:  StoredProcedure [dbo].[SelectPostTags]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-07
-- Description:	Selects Tags for a given Post
-- =============================================
CREATE PROCEDURE [dbo].[SelectPostTags]
(
	@PostID int
)
AS
BEGIN

	SET NOCOUNT ON;

	Select T.[Name]
	From dbo.Tags T
	Inner Join [Relationships] R On R.[TagID] = T.[TagID]
	Where R.[PostID] = @PostID
	Order By T.[Name] Asc

END
GO
/****** Object:  StoredProcedure [dbo].[SelectTags]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-07
-- Description:	Selects Tags for Tag Cloud
-- =============================================
CREATE PROCEDURE [dbo].[SelectTags]
AS
BEGIN

	SET NOCOUNT ON;

	Select Top 50
		T.[TagID],
		T.[Name],
		COUNT(T.[TagID]) As [PostCount]
	From dbo.Tags T
	Inner Join dbo.Relationships R On R.TagID = T.TagID
	Group By T.[TagID], T.[Name]
	Order By COUNT(T.[TagID]) Desc

END
GO
/****** Object:  StoredProcedure [dbo].[SelectTotalPosts]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-08
-- Description:	Select Total Posts for pagination calculations
-- =============================================
CREATE PROCEDURE [dbo].[SelectTotalPosts]
(
	@TagName varchar(255),
	@CategoryName varchar(255),
	@Year int,
	@Month int,
	@Day int
)
AS
BEGIN

	SET NOCOUNT ON;

	--Tags
	If @TagName != ''
	Begin
		Declare @TagID int
		
		Set @TagID = (
			Select [TagID]
			From dbo.[Tags]
			Where [Name] = @TagName
		)

		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And [PostID] In (
			Select [PostID]
			From dbo.Relationships
			Where [TagID] = @TagID
		)
	End
	
	--Categories
	If @CategoryName != ''
	Begin
		Declare @CategoryID int
		
		Set @CategoryID = (
			Select [CategoryID]
			From dbo.Categories
			Where [Name] = @CategoryName
		)
		
		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And [PostID] In (
			Select [PostID]
			From dbo.Relationships
			Where [CategoryID] = @CategoryID
		)
	End
	
	--YYYY archives
	If @Year != 0 And @Month = 0 And @Day = 0
	Begin
		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
	End
	
	--YYYY/MM archives
	If @Year != 0 And @Month != 0 And @Day = 0
	Begin
		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
		And @Month = CAST(DATEPART(mm,DateCreated) As int)
	End
			
	--YYYY/MM/DD archives
	If @Year != 0 And @Month != 0 And @Day != 0
	Begin
		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
		And @Year = CAST(DATEPART(yyyy,DateCreated) As int)
		And @Month = CAST(DATEPART(mm,DateCreated) As int)
		And @Day = CAST(DATEPART(dd,DateCreated) As int)
	End
	
	--Catch all
	If 	@TagName = ''
		And @CategoryName = ''
		And @Year = 0
		And @Month = 0
		And @Day = 0
	Begin
		Select COUNT(*) As [TotalPosts]
		From dbo.Posts P
		Where [Pageable] = 1
		And [Published] = 1
	End

END
GO
/****** Object:  StoredProcedure [dbo].[Statistics]    Script Date: 2018-12-05 13:24:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Matthew Juffs
-- Create date: 2010-01-08
-- Description:	Count of data
-- =============================================
CREATE PROCEDURE [dbo].[Statistics]
AS
BEGIN

	SET NOCOUNT ON;

	Declare @Posts int
	Declare @Pages int
	Declare @Comments int
	Declare @Categories int
	Declare @Tags int
	Declare @Links int
	Declare @Users int

	--1) Published Posts and Pages
	Set @Posts = (
		Select COUNT(*) As [Posts]
		From dbo.Posts
		Where [Published] = 1
		And [Pageable] = 1
	)

	Set @Pages = (
		Select COUNT(*) As [Pages]
		From dbo.Posts
		Where [Published] = 1
		And [Pageable] = 0
	)

	--2) Published Comments
	Set @Comments = (
		Select COUNT(*) As [Comments]
		From dbo.Comments
		Where [Published] = 1
	)

	--3) Post Categories
	Set @Categories = (
		Select COUNT(*) As [Categories]
		From dbo.Categories
		Where [Type] = 'Posts'
	)

	--4) Tags
	Set @Tags = (
		Select COUNT(*) As [Tags]
		From dbo.Tags
	)
	--5) Links
	Set @Links = (
		Select COUNT(*) As [Links]
		From dbo.Links
		Where [Published] = 1
	)

	--6) Users
	Set @Users = (
		Select COUNT(*) As [Users]
		From dbo.Users
		Where [Active] = 1
	)
	
	Select
		@Posts As [Posts],
		@Pages As [Pages],
		@Comments As [Comments],
		@Categories As [Categories],
		@Tags As [Tags],
		@Links As [Links],
		@Users As [Users]

END
GO
