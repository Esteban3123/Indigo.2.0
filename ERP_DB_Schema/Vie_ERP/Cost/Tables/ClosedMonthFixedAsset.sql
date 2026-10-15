CREATE TABLE [Cost].[ClosedMonthFixedAsset] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [CostCenterId]     INT             NOT NULL,
    [FixedAssetItemId] INT             NOT NULL,
    [TotalHoursWorked] INT             NOT NULL,
    [ValueTotal]       NUMERIC (18, 2) NOT NULL,
    [AverageCost]      NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthFixedAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthFixedAsset_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthFixedAsset_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthFixedAsset_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_FixedAssetItem];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del artículo, este se calcula usando el valor total del artículo / la sumatoria de horas laboradas del artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor total depreciado durante el periodo para el artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ValueTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sumatoria de horas laboradas del artículo en el periodo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';

