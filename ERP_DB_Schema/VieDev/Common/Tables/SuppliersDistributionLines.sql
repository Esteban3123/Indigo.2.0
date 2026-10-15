CREATE TABLE [Common].[SuppliersDistributionLines] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSupplier]         INT NOT NULL,
    [IdDistributionLine] INT NOT NULL,
    [Status]             BIT CONSTRAINT [DF_SuppliersDistributionLines_Status] DEFAULT ((1)) NOT NULL,
    [PositionId]         INT NULL,
    [Factoring]          BIT CONSTRAINT [DF__Suppliers__Facto__1788EA65] DEFAULT ((0)) NOT NULL,
    [NotFactoring]       BIT CONSTRAINT [DF__Suppliers__NotFa__5921A398] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SuppliersDistributionLines] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuppliersDistributionLines_DistributionLines] FOREIGN KEY ([IdDistributionLine]) REFERENCES [Common].[DistributionLines] ([Id]),
    CONSTRAINT [FK_SuppliersDistributionLines_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id]),
    CONSTRAINT [FK_SuppliersDistributionLines_Supplier] FOREIGN KEY ([IdSupplier]) REFERENCES [Common].[Supplier] ([Id])
);


GO
ALTER TABLE [Common].[SuppliersDistributionLines] NOCHECK CONSTRAINT [FK_SuppliersDistributionLines_DistributionLines];


GO
ALTER TABLE [Common].[SuppliersDistributionLines] NOCHECK CONSTRAINT [FK_SuppliersDistributionLines_Position];


GO
ALTER TABLE [Common].[SuppliersDistributionLines] NOCHECK CONSTRAINT [FK_SuppliersDistributionLines_Supplier];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el proveedor es un independiente se permite agregar a la linea de distribución el cargo que ocupa el proveedor.   De esta manera poder realizar los calculos para el costeo por actividades', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'SuppliersDistributionLines', @level2type = N'COLUMN', @level2name = N'PositionId';

