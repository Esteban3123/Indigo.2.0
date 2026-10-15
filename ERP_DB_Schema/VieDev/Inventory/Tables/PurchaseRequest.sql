CREATE TABLE [Inventory].[PurchaseRequest] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [OperatingUnitId]  INT           NOT NULL,
    [FunctionalUnitId] INT           NOT NULL,
    [RequestTypeId]    TINYINT       NOT NULL,
    [Observation]      VARCHAR (300) NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [ConfirmationUser] VARCHAR (20)  NULL,
    [ConfirmationDate] DATETIME      NULL,
    [AnnulmentUser]    VARCHAR (20)  NULL,
    [AnnulmentDate]    DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [Ordered]          BIT           CONSTRAINT [DF_PurchaseRequest_Ordered] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PurchaseRequest__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseRequest_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_PurchaseRequest_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PurchaseRequest_RequestType] FOREIGN KEY ([RequestTypeId]) REFERENCES [Inventory].[RequestType] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseRequest] NOCHECK CONSTRAINT [FK_PurchaseRequest_FunctionalUnit];


GO
ALTER TABLE [Inventory].[PurchaseRequest] NOCHECK CONSTRAINT [FK_PurchaseRequest_OperatingUnit];


GO
ALTER TABLE [Inventory].[PurchaseRequest] NOCHECK CONSTRAINT [FK_PurchaseRequest_RequestType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de la solicitud  1 - Producto  2 - Activo fijo  3 - Otro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseRequest', @level2type = N'COLUMN', @level2name = N'RequestTypeId';

