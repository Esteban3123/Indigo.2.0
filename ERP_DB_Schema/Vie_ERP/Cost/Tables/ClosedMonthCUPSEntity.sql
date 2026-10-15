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
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_CUPSEntity];


GO
ALTER TABLE [Cost].[ClosedMonthCUPSEntity] NOCHECK CONSTRAINT [FK_ClosedMonthCUPSEntity_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo estimado para la actividad en el centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'EstimatedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo unitario en el Periodo de la actividad en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidades Facturadas en el Periodo de la actividad en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCUPSEntity', @level2type = N'COLUMN', @level2name = N'Id';

