CREATE TABLE [Cost].[ClosedMonthCostActivity] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]          INT             NOT NULL,
    [CostActivityId]         INT             NOT NULL,
    [CUPSEntityId]           INT             NOT NULL,
    [CostProductionCenterId] INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [FixedAssetValue]        DECIMAL (24, 6) NOT NULL,
    [PayrollValue]           DECIMAL (24, 6) NOT NULL,
    [InventoryValue]         DECIMAL (24, 6) NOT NULL,
    [AddictionalValue]       DECIMAL (24, 6) NOT NULL,
    [UnitValue]              DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthCostActivity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthCostActivity_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity];




GO



GO



GO



GO



GO
ALTER TABLE [Cost].[ClosedMonthCostActivity] NOCHECK CONSTRAINT [FK_ClosedMonthCostActivity_CUPSEntity];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario promedio de la actividad; suma ponderada de Activos Fijos + Nómina + Inventario + Adicionales (DECIMAL 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio de la actividad al sumar los costos unitarios de los detalles (Activos Fijos, Nomina, Inventario, Adicionales)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo calculado de Adicionales; gastos complementarios, honorarios, servicios contratados u otros costos asociados (DECIMAL 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'AddictionalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo calculado de costos Adicionales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'AddictionalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'AddictionalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo calculado de Inventario; insumos, medicamentos, suministros o materias primas consumidas (DECIMAL 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'InventoryValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo calculado de Inventario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'InventoryValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'InventoryValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo calculado de Nómina; salarios, prestaciones y beneficios del personal asignado a la actividad (DECIMAL 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'PayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo calculado de Inventario', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'PayrollValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'PayrollValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo calculado de Activos Fijos; depreciación, mantenimiento o inversión en bienes muebles/inmuebles (DECIMAL 24,6)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo calculado de Activos Fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'FixedAssetValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Costo (FK → Payroll.CostCenter); departamento, sección o responsable del gasto (nullable)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Producción (FK → Cost.CostProductionCenter); unidad funcional o área generadora de costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Centro de Producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio CUPS relacionado (FK → Contract.CUPSEntity); código de procedimiento, servicio o atención', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio relacionado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad de costo (FK → Cost.CostActivity); tipo de actividad o prestación costeable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del costo por actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del mes cerrado (FK → Cost.ClosedMonth); período contable finalizado para costeo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de actividad de costo en mes cerrado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costos detallados por actividad en un mes cerrado contable. Registra el valor unitario y los componentes de costo (activos fijos, nómina, inventario y adicionales) para cada combinación de actividad, servicio CUPS, centro de producción y centro de costo dentro de un período mensual cerrado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'ClosedMonthCostActivity';
