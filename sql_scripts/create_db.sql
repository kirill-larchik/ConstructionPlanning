CREATE DATABASE ConstructionPlanningDb
GO

USE ConstructionPlanningDb
GO

CREATE TABLE [Customers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL,
  [Phone] nvarchar(255) NOT NULL,
)
GO

CREATE TABLE [Projects] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [DateOfCreate] datetime NOT NULL,
  [Deadline] datetime NOT NULL,
  [AllocatedAmount] int NOT NULL,
  [CustomerId] int
)
GO

CREATE TABLE [ConstructionObjects] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Description] nvarchar(255) NOT NULL,
  [ProjectId] int
)
GO

CREATE TABLE [ResourcesPerObject] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ConstructionObjectId] int,
  [ResourceId] int,
  [Count] int NOT NULL,
  [UsedCount] int NOT NULL
)
GO

CREATE TABLE [Resources] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) UNIQUE NOT NULL,
  [TypeId] int,
  [AvaliableAmount] int NOT NULL DEFAULT (0),
  [UnitCost] int NOT NULL
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
  [Count] int NOT NULL
)
GO

CREATE TABLE [Providers] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Address] nvarchar(255) NOT NULL,
  [Phone] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [Sales] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Date] datetime NOT NULL,
  [ResourceId] int,
  [Customer] nvarchar(255) NOT NULL,
  [Count] int NOT NULL
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

ALTER TABLE [Resources] ADD FOREIGN KEY ([TypeId]) REFERENCES [ResourceTypes] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE SET NULL
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE SET NULL
GO

ALTER TABLE [Sales] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE SET NULL
GO
