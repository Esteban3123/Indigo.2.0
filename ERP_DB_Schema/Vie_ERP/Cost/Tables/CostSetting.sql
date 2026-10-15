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
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostInteropCostSetting_JournalVoucherTypes];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_AccountPayableConcepts];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_ProvisionJournalVoucherType];


GO
ALTER TABLE [Cost].[CostSetting] NOCHECK CONSTRAINT [FK_CostSetting_ProvisionReversalJournalVoucherType];


GO
CREATE NONCLUSTERED INDEX [IX_CostSetting_Year_Month]
    ON [Cost].[CostSetting]([Year], [Month]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica si se muestra o no el costo estandar promedio por actividades', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AverageStandardCostActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de tipo de comprobante contable de reversión de provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionReversalJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de tipo de comprobante contable de provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ProvisionJournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina de donde se obtendran los costos para el calculo unitario de las actividades:  1. Por organización  2. Por centro de Producción  ', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ActivityCalculationBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Validar actividades', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'ValidateActivities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de pago de tipo general', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountPayableConceptsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a realizar la estimacion de costos para la Mano de Obra  1 - Total Devengado  2 - Total Devengado + Seguridad Social Empleador  3 - Total Devengado + Seguridad Social Empleador + Parafiscales  4 - Total Devengado + Seguridad Social Empleador + Parafiscales + Provisiones', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo comprobante contable    Se llena siempre y cuando AccountingCosts esté en True, sino va NULL', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contabilizar costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de parametros de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostSetting', @level2type = N'COLUMN', @level2name = N'Id';

