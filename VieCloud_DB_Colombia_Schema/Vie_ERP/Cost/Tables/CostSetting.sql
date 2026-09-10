CREATE TABLE [Cost].[CostSetting] (
    [Id]                                    INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                                  INT     NOT NULL,
    [Month]                                 INT     NOT NULL,
    [AccountingCosts]                       BIT     CONSTRAINT [DF_CostSetting_AccountingCosts] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeId]                  INT     NULL,
    [CostEstimateLabor]                     TINYINT CONSTRAINT [DF_CostInteropCostSetting_CostEstimateLabor] DEFAULT ((1)) NOT NULL,
    [AccountPayableConceptsId]              INT     NOT NULL,
    [ValidateActivities]                    BIT     CONSTRAINT [DF_CostSetting_ValidateActivities] DEFAULT ((0)) NOT NULL,
    [ActivityCalculationBy]                 TINYINT CONSTRAINT [DF_CostSetting_ActivityCalculationBy] DEFAULT ((1)) NOT NULL,
    [ProvisionJournalVoucherTypeId]         INT     NULL,
    [ProvisionReversalJournalVoucherTypeId] INT     NULL,
    [AverageStandardCostActivity]           BIT     CONSTRAINT [DF_CostSetting_AverageStandardCostActivity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostInteropCostSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInteropCostSetting_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostSetting_AccountPayableConcepts] FOREIGN KEY ([AccountPayableConceptsId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_CostSetting_ProvisionJournalVoucherType] FOREIGN KEY ([ProvisionJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostSetting_ProvisionReversalJournalVoucherType] FOREIGN KEY ([ProvisionReversalJournalVoucherTypeId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);




GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CostSetting_Year_Month]
    ON [Cost].[CostSetting]([Year], [Month]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita mostrar y usar costo estándar promedio por actividad en reportes y cálculos de rentabilidad; impacta valorización de servicios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AverageStandardCostActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se muestra o no el costo estandar promedio por actividades', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AverageStandardCostActivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AverageStandardCostActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tipo de comprobante contable (FK a GeneralLedger.JournalVoucherTypes) para reversión/anulación de provisiones de costos; puede ser NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionReversalJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tipo de comprobante contable de reversión de provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionReversalJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionReversalJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tipo de comprobante contable (FK a GeneralLedger.JournalVoucherTypes) para registro de provisiones de costos; puede ser NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de tipo de comprobante contable de provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionJournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Criterio (TINYINT, 1-2) para obtener costos unitarios de actividades: 1=Por organización (costo centralizado), 2=Por centro de producción (costo descentralizado).', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ActivityCalculationBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina de donde se obtendran los costos para el calculo unitario de las actividades:  1. Por organización  2. Por centro de Producción  ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ActivityCalculationBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ActivityCalculationBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que habilita validación de actividades clínicas/operacionales contra catálogo maestro antes de asignación de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ValidateActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar actividades', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ValidateActivities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ValidateActivities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del concepto general de pago/cuentas por pagar (FK a Payments.AccountPayableConcepts) usado para provisiones y acumulados.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de pago de tipo general', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de cálculo (TINYINT, 1-4) para estimación de costos de mano de obra: 1=Total Devengado, 2=+Seguridad Social Empleador, 3=+Parafiscales, 4=+Provisiones; impacta nómina y costos operacionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a realizar la estimacion de costos para la Mano de Obra  1 - Total Devengado  2 - Total Devengado + Seguridad Social Empleador  3 - Total Devengado + Seguridad Social Empleador + Parafiscales  4 - Total Devengado + Seguridad Social Empleador + Parafiscales + Provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del tipo de comprobante contable (FK a GeneralLedger.JournalVoucherTypes) para registro de costos; requerido solo si AccountingCosts=true, NULL si contabilización desactivada.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo comprobante contable    Se llena siempre y cuando AccountingCosts esté en True, sino va NULL', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que activa/desactiva la contabilización de costos en el módulo contable; si false, JournalVoucherTypeId será NULL.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contabilizar costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes (INT, 1-12) del período de vigencia de los parámetros de costos; determina período mensual.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año (INT) del período de vigencia de los parámetros de costos; rango típico 2000-2099.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la configuración de parámetros de costos del período contable.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de parametros de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros y configuración del módulo de costos por período (año y mes): define cómo se calculan, contabilizan y validan los costos de mano de obra, actividades y provisiones dentro del sistema de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting';
