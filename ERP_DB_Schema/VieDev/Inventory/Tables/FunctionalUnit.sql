CREATE TABLE [Inventory].[FunctionalUnit] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]           VARCHAR (200) NOT NULL,
    [CareCenterCode] VARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_Inventory_FunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

