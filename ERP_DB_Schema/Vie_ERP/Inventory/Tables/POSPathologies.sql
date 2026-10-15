CREATE TABLE [Inventory].[POSPathologies] (
    [Id]           INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]    INT     NULL,
    [DiagnosticId] INT     NOT NULL,
    [MedicamentId] INT     NULL,
    [MinimumAge]   TINYINT NULL,
    [MaximumAge]   TINYINT NULL,
    [AgeMeasure]   TINYINT NULL,
    CONSTRAINT [PK_POSPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_POSPathologies_ATC] FOREIGN KEY ([MedicamentId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_POSPathologies_Diagnostic] FOREIGN KEY ([DiagnosticId]) REFERENCES [Inventory].[Diagnostic] ([Id]),
    CONSTRAINT [FK_POSPathologies_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_ATC];


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_Diagnostic];


GO
ALTER TABLE [Inventory].[POSPathologies] NOCHECK CONSTRAINT [FK_POSPathologies_Product];


GO
-- Índice filtrado para optimizar consultas por ProductId (usado en ViewListRevenueControl)
CREATE NONCLUSTERED INDEX [IX_POSPathologies_ProductId]
ON [Inventory].[POSPathologies] ([ProductId] ASC, [Id] ASC)
WHERE [ProductId] IS NOT NULL;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de edad: 1:Años, 2:Meses, 3:Dias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'AgeMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad máxima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MaximumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad mínima', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MinimumAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del medicamento(Antes ATC), este campo se llena siempre y cuando el proceso venga desde ATC, si el proceso viene de productos este campo no se llena y se llena el campo ProductId', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'MedicamentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del diagnostico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'DiagnosticId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto, este valor va nulo ya que se agrega funcionalidad para manejo de pos en atc, cuando el proceso viene desde atc no se llena este campo sino que se llena MedicamentId', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de patologias', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'POSPathologies', @level2type = N'COLUMN', @level2name = N'Id';

