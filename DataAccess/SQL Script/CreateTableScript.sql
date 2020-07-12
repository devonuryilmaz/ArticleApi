--Create Table
CREATE TABLE [dbo].[Article](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ShortArticleContent] [nvarchar](250) NULL,
	[FullArticleContent] [nvarchar](1000) NOT NULL,
	[ArticleAddedDate] [datetime] NOT NULL
)