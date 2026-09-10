CREATE TABLE [Cost].[ClosedMonthCUPSEntity] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]      INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [CUPSEntityId]       INT             NOT NULL,
    [Quantity]           INT             NOT NULL,
    [UnitValue]          DECIMAL (18, 2) NOT NULL,
    [EstimatedValue]     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCUPSEntity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ClosedMonthCUPSEntity_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity];




GO



GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo estimado total (DECIMAL 18,2) para la actividad/servicio CUPS en el centro de producción durante el período cerrado; resultado de Quantity × UnitValue', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'EstimatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo estimado para la actividad en el centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'EstimatedValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'EstimatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario (DECIMAL 18,2) del servicio CUPS o procedimiento en el período; costo individual por unidad facturada en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo unitario en el Periodo de la actividad en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de servicios/procedimientos CUPS facturados en el período en el centro de producción; volumen de actividades realizadas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidades Facturadas en el Periodo de la actividad en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del servicio/procedimiento CUPS; referencia a catálogo de servicios de salud (diagnósticos, procedimientos, exámenes, recetas)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del centro de producción/unidad funcional; vincula la actividad CUPS al centro donde se realizó', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del mes cerrado para costeo; período contable en el que se registró la actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY) de la relación cierre mensual - CUPS - centro de producción; registro único de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de producción de servicios (CUPS) por entidad, correspondientes a meses cerrados en costos. Guarda cantidades, valores unitarios y totales estimados de cada servicio facturado o valorizado por centro de producción, asociados opcionalmente a un contrato específico.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity';
