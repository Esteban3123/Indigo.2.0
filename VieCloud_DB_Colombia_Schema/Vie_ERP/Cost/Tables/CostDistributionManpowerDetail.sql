CREATE TABLE [Cost].[CostDistributionManpowerDetail] (
    [Id]                                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionManpowerId]            INT             NOT NULL,
    [ProductionCenterId]                INT             NOT NULL,
    [HoursQuantity]                     INT             NOT NULL,
    [TotalAccrued]                      DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]                    DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution]         DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]                   DECIMAL (18, 2) CONSTRAINT [DF_CostDistributionManpowerDetail_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    [TotalAccruedRevalued]              DECIMAL (18, 2) NULL,
    [TotalProvisionRevalued]            DECIMAL (18, 2) NULL,
    [TotalEmployerContributionRevalued] DECIMAL (18, 2) NULL,
    [TotalParafiscalRevalued]           DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_CostDistributionManpowerDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionManpowerDetail_CostDistributionManpower] FOREIGN KEY ([DistributionManpowerId]) REFERENCES [Cost].[CostDistributionManpower] ([Id]),
    CONSTRAINT [FK_CostDistributionManpowerDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor revalorizado de aportes parafiscales (SENA, ICBF, cajas de compensación) del empleado, ajustado a la moneda del módulo. DECIMAL(18,2), nullable, para comparación histórica de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscalRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo TotalParafiscal revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscalRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscalRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor revalorizado de aportes patronales (pensión, salud, riesgos laborales) del empleado, ajustado a moneda del módulo. DECIMAL(18,2), nullable, referencial para análisis de variación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContributionRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo TotalEmployerContribution revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContributionRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContributionRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor revalorizado de provisiones (cesantías, vacaciones, bonificación) del periodo actual, ajustado a moneda del módulo. DECIMAL(18,2), nullable, para auditoría de pasivos laborales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvisionRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo TotalProvision revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvisionRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvisionRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor revalorizado del total devengado (salario bruto sin deducciones) del empleado en el periodo, ajustado a moneda del módulo. DECIMAL(18,2), nullable, para análisis comparativo de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccruedRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del campo TotalAccrued revalorizado a la moneda del módulo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccruedRevalued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccruedRevalued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de aportes parafiscales (SENA, ICBF, cajas de compensación) acumulados del empleado en el periodo seleccionado. DECIMAL(18,2), default=0, usado en nómina y costos operacionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de los parafiscales del empleado en el periodo seleccionado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de aportes patronales (pensión, salud, riesgos laborales) acumulados en el periodo. DECIMAL(18,2), default=0, componente crítico para cálculo de costos de mano de obra.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de los aportes patronales', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de provisiones acumuladas del periodo actual (cesantías, prima, vacaciones, bonificación especial). DECIMAL(18,2), default=0, pasivo laboral contable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de las provisiones del periodo actual', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total devengado (salario bruto sin deducciones) del empleado en el periodo específico. DECIMAL(18,2), default=0, base para nómina y costos de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total devengado (Sin ningun tipo de deducciones) del empleado en el periodo especifico', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas trabajadas por el empleado en el periodo de distribución. INT, usado en cálculo de tarifa horaria y análisis de productividad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas trabajadas del empleado', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del centro de producción o unidad funcional donde se distribuye el costo de mano de obra. INT NOT NULL, referencia a [Cost].[CostProductionCenter].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del encabezado de distribución de mano de obra al que pertenece este detalle. INT NOT NULL, referencia a [Cost].[CostDistributionManpower].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de detalle de distribución de mano de obra. INT IDENTITY, clave primaria clustered para auditoría y trazabilidad de costos laborales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion de la mano de obra', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de costos de mano de obra por centro de producción. Registra las horas trabajadas y los valores causados, provisionados, aportes patronales y parafiscales (originales y revaluados) asociados a cada distribución de personal en los centros de costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionManpowerDetail';
