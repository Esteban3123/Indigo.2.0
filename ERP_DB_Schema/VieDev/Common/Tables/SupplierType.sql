CREATE TABLE [Common].[SupplierType] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [ParentId]         INT           NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_SupplierType_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_SupplierType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_SupplierType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SupplierType_SupplierType] FOREIGN KEY ([ParentId]) REFERENCES [Common].[SupplierType] ([Id])
);


GO
ALTER TABLE [Common].[SupplierType] NOCHECK CONSTRAINT [FK_SupplierType_SupplierType];

