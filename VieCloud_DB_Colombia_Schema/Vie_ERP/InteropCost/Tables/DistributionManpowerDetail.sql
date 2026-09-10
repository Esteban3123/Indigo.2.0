CREATE TABLE [InteropCost].[DistributionManpowerDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionManpowerId]    INT             NOT NULL,
    [ProductionCenterId]        INT             NOT NULL,
    [HoursQuantity]             INT             NOT NULL,
    [TotalAccrued]              NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DistributionManpowerDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionManpowerDetail_DistributionManpower] FOREIGN KEY ([DistributionManpowerId]) REFERENCES [InteropCost].[DistributionManpower] ([Id]),
    CONSTRAINT [FK_DistributionManpowerDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de contribuciones parafiscales (SENA, ICBF, caja de compensación) del empleado en el período seleccionado. Tipo: NUMERIC(18,2). Valor por defecto: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de los parafiscales del empleado en el periodo seleccionado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalParafiscal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de aportes patronales (contribución empresarial a seguridad social y pensión) del período. Tipo: NUMERIC(18,2). Valor por defecto: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de los aportes patronales', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalEmployerContribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total de provisiones (cesantías, prima de servicios, vacaciones) devengadas en el período actual. Tipo: NUMERIC(18,2). Valor por defecto: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total de las provisiones del periodo actual', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalProvision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto total devengado sin deducciones del empleado en el período específico (salario bruto + beneficios). Tipo: NUMERIC(18,2). Valor por defecto: 0.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total devengado (Sin ningun tipo de deducciones) del empleado en el periodo especifico', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'TotalAccrued';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas trabajadas por el empleado en el período de distribución de costos. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas trabajadas del empleado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción, unidad funcional o servicio al cual se asignan los costos de mano de obra. Referencia FK a ProductionCenter. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o maestro de la distribución de mano de obra a la que pertenece este detalle. Referencia FK a DistributionManpower. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'DistributionManpowerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de distribución de mano de obra (clave primaria). Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion de la mano de obra', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de mano de obra por centro de producción: registra las horas trabajadas y los valores causados (acumulado, provisión, aportes patronales y parafiscales) asociados a cada línea de distribución de costos de personal.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionManpowerDetail';
