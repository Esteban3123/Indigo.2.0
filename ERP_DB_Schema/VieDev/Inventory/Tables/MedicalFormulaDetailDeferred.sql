CREATE TABLE [Inventory].[MedicalFormulaDetailDeferred] (
    [Id]                       INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MedicalFormulaDetailId]   INT      NOT NULL,
    [FirstDeliveryDate]        DATETIME NOT NULL,
    [DeliveryQuantityDeferred] INT      NOT NULL,
    [Periodicity]              INT      NOT NULL,
    [Number]                   INT      NOT NULL,
    [DeliveryDate]             DATETIME NOT NULL,
    [DeliveryQuantity]         INT      NOT NULL,
    [PendingQuantity]          INT      NOT NULL,
    CONSTRAINT [PK_MedicalFormulaDetailDeferred] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MedicalFormulaDetailDeferred_MedicalFormulaDetailDeferred] FOREIGN KEY ([MedicalFormulaDetailId]) REFERENCES [Inventory].[MedicalFormulaDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[MedicalFormulaDetailDeferred] NOCHECK CONSTRAINT [FK_MedicalFormulaDetailDeferred_MedicalFormulaDetailDeferred];

