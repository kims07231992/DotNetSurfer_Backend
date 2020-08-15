CREATE TABLE [dbo].[Tags]
(
	[TagId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[ArticleId] INT FOREIGN KEY REFERENCES [dbo].[Articles](ArticleId),
	[Content] NVARCHAR(20)
)
GO

CREATE INDEX TagsIndex
ON [dbo].[Tags] (ArticleId)
GO

ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT FK_Article_Keyword_CAS
FOREIGN KEY (ArticleId) REFERENCES [dbo].[Articles](ArticleId) ON DELETE CASCADE
GO