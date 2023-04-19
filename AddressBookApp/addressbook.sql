CREATE TABLE [dbo].[Groups] (
  [Id] INT IDENTITY (1, 1) NOT NULL,
  [Name] NVARCHAR (200) NOT NULL,
  CONSTRAINT [PK_dbo.Groups] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [AK_dbo.GroupName] UNIQUE([Name])
);

CREATE TABLE [dbo].[Addresses] (
  [Id] INT IDENTITY (1, 1) NOT NULL,
  [Name] NVARCHAR (100) NOT NULL,
  [Kana] NVARCHAR (200) NOT NULL,
  [ZipCode] NVARCHAR (7) NULL,
  [Prefecture] NVARCHAR (10) NULL,
  [StreetAddress] NVARCHAR (600) NULL,
  [Telephone] NVARCHAR (11) NULL,
  [Mail] NVARCHAR (128) NULL,
  [Group_Id] INT NULL,
  CONSTRAINT [PK_dbo.Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_dbo.Addresses.Group_Id] FOREIGN KEY ([Group_Id]) REFERENCES [dbo].[Groups] ([Id]) ON DELETE CASCADE
);