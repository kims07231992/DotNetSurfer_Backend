CREATE TABLE [dbo].[Articles]
(
	[ArticleId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[TopicId] INT FOREIGN KEY REFERENCES [dbo].[Topics](TopicId),
	[UserId] INT FOREIGN KEY REFERENCES [dbo].[Users](UserId),
	[Title] NVARCHAR(100) NOT NULL,
	[Content] NVARCHAR(MAX) NULL,
	[Category] NVARCHAR(10) DEFAULT('Free') NULL,	--Category, inside of topic
	[Picture] VARBINARY(MAX) NULL,
	[PictureMimeType] VARCHAR(50) NULL,
	[PictureUrl] NVARCHAR(2083) NULL,
	[PostDate] DATETIME2 DEFAULT GETDATE() NULL,
	[ModifyDate] DATETIME2 DEFAULT GETDATE() NULL,
	[ReadCount] INT DEFAULT 0 NULL,
	[ShowFlag] BIT NOT NULL DEFAULT 1
);
GO

CREATE INDEX ArticlesIndex
ON [dbo].[Articles] (TopicId)
GO

--Backup
CREATE TRIGGER trg_BackupArticles ON [dbo].[Articles]
AFTER DELETE
AS
BEGIN
	INSERT INTO [dbo].[BK_Articles] 
	SELECT * 
	FROM DELETED
END
GO