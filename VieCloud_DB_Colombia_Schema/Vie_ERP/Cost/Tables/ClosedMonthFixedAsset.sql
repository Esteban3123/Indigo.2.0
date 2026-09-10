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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio unitario del activo fijo calculado como ValueTotal dividido entre TotalHoursWorked; representa el costo depreciado por hora laborada del artículo en el periodo cerrado (NUMERIC 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio del artículo, este se calcula usando el valor total del artículo / la sumatoria de horas laboradas del artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total depreciado o consumido del activo fijo durante el periodo contable cerrado; importe en moneda base para el artículo en el centro de costo (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ValueTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor total depreciado durante el periodo para el artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ValueTotal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ValueTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria o acumulado de horas laboradas, utilizadas o consumidas del activo fijo durante el periodo contable; base de cálculo para el costo promedio unitario (INT)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sumatoria de horas laboradas del artículo en el periodo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'TotalHoursWorked';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del artículo o bien de activo fijo; llave foránea que referencia la tabla FixedAsset.FixedAssetItem (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del artículo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo o unidad funcional responsable del activo fijo en el periodo; llave foránea a Payroll.CostCenter (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del periodo contable cerrado; llave foránea que referencia Cost.ClosedMonth para vincular datos de depreciación del mes (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y clave primaria de la tabla ClosedMonthFixedAsset; identifica el registro de costo de activo fijo por mes y centro (INT, Identity 1,1)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de activos fijos asociados a un cierre de mes contable, con el total de horas trabajadas, el valor total y el costo promedio por activo y centro de costo en ese período cerrado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthFixedAsset';
