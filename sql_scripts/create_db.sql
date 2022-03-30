CREATE DATABASE ConstructionPlanningDb
GO

USE ConstructionPlanningDb
GO

CREATE TABLE [Customers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [MiddleName] nvarchar(255) NOT NULL,
  [Phone] nvarchar(255) NOT NULL,
  [Organization] nvarchar(255),
  [Address] nvarchar(255)
)
GO

CREATE TABLE [Projects] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [DateOfCreate] datetime NOT NULL,
  [Deadline] datetime NOT NULL,
  [AllocatedAmount] int NOT NULL,
  [CurrentAmount] int NOT NULL,
  [TotalCost] int NOT NULL DEFAULT (0),
  [CustomerId] int
)
GO

CREATE TABLE [ConstructionObjects] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [TotalCost] int NOT NULL DEFAULT (0),
  [ProjectId] int
)
GO

CREATE TABLE [ResourcesPerObject] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ConstructionObjectId] int,
  [ResourceId] int,
  [Count] int NOT NULL,
  [UsedCount] int NOT NULL,
  [TotalCost] int NOT NULL DEFAULT (0)
)
GO

CREATE TABLE [Resources] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) UNIQUE NOT NULL,
  [TypeId] int,
  [AvailableAmount] int NOT NULL DEFAULT (0),
  [UnitCost] int NOT NULL,
  [IsVsible] bit
)
GO

CREATE TABLE [ResourceTypes] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Deliveries] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ResourceId] int,
  [ProviderId] int,
  [Date] datetime NOT NULL,
  [UnitCost] int NOT NULL,
  [Count] int NOT NULL,
  [Cost] int NOT NULL
)
GO

CREATE TABLE [Providers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255) NOT NULL,
  [address] nvarchar(255) NOT NULL,
  [phone] nvarchar(255) NOT NULL,
  [IsVsible] bit
)
GO

CREATE TABLE [Sales] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Date] datetime NOT NULL,
  [ResourceId] int,
  [Customer] nvarchar(255) NOT NULL,
  [Count] int NOT NULL,
  [TotalCost] int NOT NULL
)
GO

ALTER TABLE [Projects] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id])
GO

ALTER TABLE [ConstructionObjects] ADD FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([Id])
GO

ALTER TABLE [ResourcesPerObject] ADD FOREIGN KEY ([ConstructionObjectId]) REFERENCES [ConstructionObjects] ([Id])
GO

ALTER TABLE [ResourcesPerObject] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id])
GO

ALTER TABLE [Resources] ADD FOREIGN KEY ([TypeId]) REFERENCES [ResourceTypes] ([Id])
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id])
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id])
GO

ALTER TABLE [Sales] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id])
GO
