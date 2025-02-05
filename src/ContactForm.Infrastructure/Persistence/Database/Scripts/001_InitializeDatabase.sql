CREATE TABLE [dbo].[Users]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL,
	[Email] NVARCHAR(100) NOT NULL,
	[Phone] NVARCHAR(100) NULL,
	[Website] NVARCHAR(100) NULL,
	[CreatedDate] DATETIME NOT NULL,

	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE INDEX IX_Users_Email ON Users (Email);

CREATE TABLE [dbo].[Addresses]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[Street] NVARCHAR(100) NOT NULL,
	[City] NVARCHAR(100) NOT NULL,
	[Suite] NVARCHAR(100) NOT NULL,
	[ZipCode] NVARCHAR(100) NOT NULL,

	CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Address_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);

CREATE TABLE [dbo].[Companies]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[UserId] INT NOT NULL,
	[Name] NVARCHAR(100) NOT NULL,
	[CatchPhrase] NVARCHAR(100) NOT NULL,
	[Bs] NVARCHAR(100) NOT NULL,

	CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Company_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);

CREATE TABLE [dbo].[Geos]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[AddressId] INT NOT NULL,
	[Latitude] DECIMAL(9,6) NOT NULL,
	[Longitude] DECIMAL(9,6) NOT NULL,

	CONSTRAINT [PK_Geo] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Geo_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses]([Id])
);