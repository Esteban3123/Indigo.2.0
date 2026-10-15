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


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_MedicalFormulaDetailDeferred]
    ON [Inventory].[MedicalFormulaDetailDeferred]([MedicalFormulaDetailId] ASC, [Number] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad pendiente, cada vez que se realiza la dispensación este valor disminuye porque empieza con el mismo valor de DeliveryQuantity', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'PendingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a entregar, se calcula con DeliveryQuantityDeferred', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de entrega, se calcula con la primera fecha de entrega y la periodicidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero que identifica el diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece el tiempo en dias con la cual se va a entregar cada producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a entregar por cada diferido', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'DeliveryQuantityDeferred';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primera fecha de entrega. Con base a esta se calculan las otros diferidos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'FirstDeliveryDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la formula medica, en el detalle es donde van los productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'MedicalFormulaDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'MedicalFormulaDetailDeferred', @level2type = N'COLUMN', @level2name = N'Id';

