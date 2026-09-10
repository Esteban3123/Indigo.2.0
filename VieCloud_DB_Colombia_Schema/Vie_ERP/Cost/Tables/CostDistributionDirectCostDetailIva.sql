CREATE TABLE [Cost].[CostDistributionDirectCostDetailIva] (
    [Id]                                 INT             IDENTITY (1, 1) NOT NULL,
    [CostDistributionDirectCostDetailId] INT             NOT NULL,
    [BaseIva]                            DECIMAL (18, 2) NOT NULL,
    [GeneralLedgerIvaId]                 INT             NOT NULL,
    [IvaValue]                           DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostDetailIva] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostDetailIva_CostDistributionDirectCostDetail] FOREIGN KEY ([CostDistributionDirectCostDetailId]) REFERENCES [Cost].[CostDistributionDirectCostDetail] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostDetailIva_GeneralLedgerIVA] FOREIGN KEY ([GeneralLedgerIvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto de IVA calculado resultante (BaseIva × tarifa); valor del impuesto al valor agregado por detalle de distribución de costo (Decimal 18,2)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Iva calculado por detalle', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tarifa de IVA configurada en contabilidad general; llave foránea a GeneralLedgerIVA que define el porcentaje de impuesto a aplicar (3%, 5%, 19%, etc.)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarifa de Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base sobre el cual se calcula el IVA; monto gravable en moneda local antes de aplicar la tarifa de impuesto (Decimal 18,2)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'BaseIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Base para el calculo del Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'BaseIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'BaseIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de distribución de costo directo; llave foránea a tabla CostDistributionDirectCostDetail que agrupa los costos por centro de atención, procedimiento o servicio', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribución del costo', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de IVA calculado; referencia la tarifa de IVA elegida aplicada a cada detalle de distribución de costo directo (Decimal 18,2 en contabilidad)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro (Iva calculado por Tarifa elegida para cada detalle de la distribución del costo)', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del IVA asociado a cada ítem de costo directo en la distribución de costos. Registra la base gravable y el valor del IVA calculado, vinculando cada cargo con la cuenta contable correspondiente.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostDetailIva';
