IF EXISTS(SELECT * FROM sys.databases WHERE name = 'QuanLyTinTuc')
begin
	 use master
	alter database QuanLyTinTuc set single_user with rollback immediate
	Drop Database QuanLyTinTuc
end

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'QuanLyTinTuc')
BEGIN
CREATE DATABASE QuanLyTinTuc
END
GO
 USE QuanLyTinTuc
GO

CREATE TABLE [Users] (
  [Id] int PRIMARY KEY IDENTITY(1,1),
  [Username] nvarchar(255),
  [Email] nvarchar(255),
  [Password] nvarchar(255),
  [Role] nvarchar(255),
  [Image] nvarchar(255),
  [CreatedAt] datetime,
  [UpdatedAt] datetime
)
GO

CREATE TABLE [Articles] (
  [ArticleId] int PRIMARY KEY IDENTITY(1,1),
  [UserId] int,
  [TopicId] int,
  [BannerImage] nvarchar(255),
  [Slug] nvarchar(255),
  [Title] nvarchar(255),
  [Description] nvarchar(255),
  [Body] ntext,
  [CreatedAt] datetime,
  [UpdatedAt] datetime
)
GO

CREATE TABLE [Comment] (
  [CommentId] int PRIMARY KEY IDENTITY(1,1),
  [ArticleId] int,
  [UserId] int,
  [CommentBody] nvarchar(4000),
  [CreatedAt] datetime,
  [UpdatedAt] datetime
)
GO

CREATE TABLE [Tag] (
  [TagId] int PRIMARY KEY IDENTITY(1,1),
  [TagName] nvarchar(255)
)
GO

CREATE TABLE [ArticleTag] (
  [ArticleTagId] int PRIMARY KEY IDENTITY(1,1),
  [ArticleId] int,
  [TagId] int
)
GO

CREATE TABLE [Topic] (
  [TopicId] int PRIMARY KEY IDENTITY(1,1),
  [TopicName] nvarchar(100)
)
GO

ALTER TABLE [Articles] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [ArticleTag] ADD FOREIGN KEY ([TagId]) REFERENCES [Tag] ([TagId])
GO

ALTER TABLE [ArticleTag] ADD FOREIGN KEY ([ArticleId]) REFERENCES [Articles] ([ArticleId])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
GO

ALTER TABLE [Comment] ADD FOREIGN KEY ([ArticleId]) REFERENCES [Articles] ([ArticleId])
GO

ALTER TABLE [Articles] ADD FOREIGN KEY ([TopicId]) REFERENCES [Topic] ([TopicId])
GO

--INSERT INTO QuanLyTinTuc.dbo.Users
--(Username, Email, [Role], [Password] [Image], CreatedAt, UpdatedAt)
--VALUES(N'admin', N'admin@gmail.com', N'User', NULL, NULL, NULL);

INSERT INTO QuanLyTinTuc.dbo.Topic (TopicName) VALUES(N'Văn hóa');
INSERT INTO QuanLyTinTuc.dbo.Topic (TopicName) VALUES(N'Thể thao');
INSERT INTO QuanLyTinTuc.dbo.Topic (TopicName) VALUES(N'Giải trí');

INSERT INTO QuanLyTinTuc.dbo.Tag (TagName) VALUES(N'Chủ tịch Hồ Chí Minh');
INSERT INTO QuanLyTinTuc.dbo.Tag (TagName) VALUES(N'Quốc tế cộng sản');

--INSERT INTO QuanLyTinTuc.dbo.Articles (UserId, TopicId, BannerImage, Slug, Title, Description, Body, CreatedAt, UpdatedAt) VALUES(1, 1, NULL, N'test-slug-23040', N'Tiêu đề bài báo: lorem as  Keys to writing copy that actually converts and sells users', N'Test mô tả', N'thực dân Pháp đầu độc nhân dân chúng tôi. Chúng bắt mọi người phải uống rượu. Chúng tôi có phong tục lấy gạo ngon làm ra rượu uống, khi có bạn tới chơi hoặc khi có ngày giỗ tổ tiên. Bọn thực dân Pháp đã lấy gạo xấu, rẻ tiền nấu rượu. Không ai thèm mua của chúng. Khốn thay rượu làm ra lại quá nhiều. Sau đó, người ta hạ lệnh cho các viên tỉnh trưởng cứ theo đầu người mà bắt buộc phải đi mua thứ rượu không ai uống', NULL, NULL);

--INSERT INTO QuanLyTinTuc.dbo.ArticleTag (ArticleId, TagId) VALUES(1, 1);

--INSERT INTO QuanLyTinTuc.dbo.Comment (ArticleId, UserId, CommentBody, CreatedAt, UpdatedAt) VALUES(1, 1, N'Bình luận comment test', NULL, NULL);
