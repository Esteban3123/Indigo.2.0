CREATE TABLE [Cost].[CostDistributionDirectCostIva] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [CostDistributionDirectCostId] INT             NOT NULL,
    [BaseIva]                      DECIMAL (18, 2) NOT NULL,
    [GeneralLedgerIvaId]           INT             NOT NULL,
    [IvaValue]                     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDistributionDirectCostIva] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionDirectCostIva_CostDistributionDirectCost] FOREIGN KEY ([CostDistributionDirectCostId]) REFERENCES [Cost].[CostDistributionDirectCost] ([Id]),
    CONSTRAINT [FK_CostDistributionDirectCostIva_GeneralLedgerIVA] FOREIGN KEY ([GeneralLedgerIvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA calculado como resultado de la operación: Base IVA × Tarifa IVA. Monto impositivo (DECIMAL 18,2) generado en la distribución de costos directos, requerido para contabilización y RIPS.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor del iva - se da con la operación => Base iva * tarifa iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la tarifa o alícuota del IVA relacionada con la tabla GeneralLedgerIVA. Referencia a la configuración contable del porcentaje impositivo aplicado.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tarifa del iva relacionada con la tabla GeneralLedgerIva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'GeneralLedgerIvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base del IVA (DECIMAL 18,2) sobre el cual se aplica la tarifa impositiva. Monto antes del cálculo del impuesto, utilizado para determinar el IVA a distribuir en costos directos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'BaseIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor base del iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'BaseIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'BaseIva';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la distribución de costos de costo directo asociada (tabla CostDistributionDirectCost). Vincula el desglose del IVA al registro padre de asignación de costos directos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Distribución de costos costo directo - tabla CostDistributionDirectCost', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'CostDistributionDirectCostId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, IDENTITY 1,1) del registro de distribución de costos para costo directo con IVA. Clave primaria que identifica cada línea de desglose impositivo en la distribución de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de Distribución de costos, Costo directo Iva', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del IVA asociado a cada costo directo en la distribución de costos. Guarda la base gravable, la cuenta contable del IVA y el valor del impuesto calculado para cada ítem de costo directo distribuido.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionDirectCostIva';
