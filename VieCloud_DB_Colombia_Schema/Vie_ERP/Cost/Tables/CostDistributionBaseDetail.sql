CREATE TABLE [Cost].[CostDistributionBaseDetail] (
    [Id]                 INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT             NOT NULL,
    [ProductionCenterId] INT             NOT NULL,
    [MainAccountId]      INT             NOT NULL,
    [CostCenterId]       INT             NULL,
    [Quantity]           NUMERIC (18, 4) NULL,
    CONSTRAINT [PK_CostDistributionBaseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostDistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [Cost].[CostDistributionBase] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostDistributionBaseDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (NUMERIC 18,4) a distribuir en la base de distribución; se completa solo si el tipo de distribución es calculada, no para distribuciones automáticas', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad que se va a distribuir, Solo se llena este campo si el tipo de distribucion es calculada', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (FK a Payroll.CostCenter); requerido solo si la cuenta contable utiliza centros de costo para seguimiento de gastos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo, solo se solicita si la cuenta maneja centro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (FK a GeneralLedger.MainAccounts); representa la cuenta general que recibe la distribución de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de producción destino (FK a Cost.CostProductionCenter); especifica hacia qué unidad funcional o centro se asignarán los costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion al cual se va a distribuir', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la base de distribución padre (FK a Cost.CostDistributionBase); vincula este detalle a la configuración general de distribución de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY); identificador único del registro de detalle de distribución de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la base de distribución de costos por centro de producción. Registra las cantidades y cuentas contables asociadas a cada centro de costo para el prorrateo o distribución de costos indirectos entre las unidades productivas.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseDetail';
