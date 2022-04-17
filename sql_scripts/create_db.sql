CREATE DATABASE ConstructionPlanningDb
GO

USE ConstructionPlanningDb
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
  [Count] int NOT NULL,
  [TotalCost] int NOT NULL
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
  [Count] int NOT NULL,
  [TotalCost] int NOT NULL
)
GO

ALTER TABLE [Resources] ADD FOREIGN KEY ([TypeId]) REFERENCES [ResourceTypes] ([Id]) ON DELETE SET NULL
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE SET NULL
GO

ALTER TABLE [Deliveries] ADD FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]) ON DELETE SET NULL
GO

ALTER TABLE [Sales] ADD FOREIGN KEY ([ResourceId]) REFERENCES [Resources] ([Id]) ON DELETE SET NULL
GO
