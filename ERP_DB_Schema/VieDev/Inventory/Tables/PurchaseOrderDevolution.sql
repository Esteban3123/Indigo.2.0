CREATE TABLE [Inventory].[PurchaseOrderDevolution] (
    [Id]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)   NOT NULL,
    [DocumentDate]     DATE           NOT NULL,
    [SupplierId]       INT            NOT NULL,
    [Observation]      VARCHAR (1000) NULL,
    [Status]           TINYINT        NOT NULL,
    [CreationUser]     VARCHAR (20)   NOT NULL,
    [CreationDate]     DATETIME       NOT NULL,
    [ModificationUser] VARCHAR (20)   NULL,
    [ModificationDate] DATETIME       NULL,
    [ConfirmationUser] VARCHAR (20)   NULL,
    [ConfirmationDate] DATETIME       NULL,
    [AnnulmentUser]    VARCHAR (20)   NULL,
    [AnnulmentDate]    DATETIME       NULL,
    [TimeStamp]        ROWVERSION     NOT NULL,
    CONSTRAINT [PK_PurchaseOrderDevolution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderDevolution_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [UQ_PurchaseOrderDevolution__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
ALTER TABLE [Inventory].[PurchaseOrderDevolution] NOCHECK CONSTRAINT [FK_PurchaseOrderDevolution_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolution', @level2type = N'COLUMN', @level2name = N'Observation';

