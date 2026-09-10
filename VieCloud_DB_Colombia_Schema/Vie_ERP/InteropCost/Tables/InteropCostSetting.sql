CREATE TABLE [InteropCost].[InteropCostSetting] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]                   INT          NOT NULL,
    [Month]                  INT          NOT NULL,
    [AccountingCosts]        BIT          CONSTRAINT [DF_InteropCostSetting_AccountingCosts] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeId]   INT          NULL,
    [JournalVoucherTypeCode] VARCHAR (20) NOT NULL,
    [CostEstimateLabor]      TINYINT      CONSTRAINT [DF_InteropCostSetting_CostEstimateLabor] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_InteropCostSetting] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de estimación de costos de mano de obra: 1=Total Devengados (salario base), 2=Total Devengados más Carga Patronal (salario + aportes empleador). Define qué conceptos se incluyen en el cálculo de costos laborales.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va a realizar la estimacion de costos para la Mano de Obra  1 - Total Devengados  2 - Total Devengados Mas Carga patronal', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'CostEstimateLabor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del tipo de comprobante de diario contable (máx 20 caracteres). Identifica la naturaleza del asiento contable para registrar costos operacionales.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de tipo de comprobante de diario', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de comprobante contable (FK a catálogo de comprobantes). Se requiere solo si AccountingCosts está activado (True); NULL en caso contrario.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo comprobante contable    Se llena siempre y cuando AccountingCosts esté en True, sino va NULL', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana (0=No, 1=Sí) que indica si los costos deben contabilizarse en el módulo de contabilidad. Cuando es True, obliga a JournalVoucherTypeId ser not null.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contabilizar costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'AccountingCosts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mes del período contable (1-12) asociado a la configuración de costos. Componente de la dimensión temporal junto con Year para identificar el período de aplicación.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mes', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Month';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Año fiscal o período contable (entero de 4 dígitos) asociado a la configuración de costos. Componente de la dimensión temporal junto con Month.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Año', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Year';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Year';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (identity) de la configuración de parámetros de costos interoperacionales. Clave primaria de la tabla InteropCostSetting.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de parametros de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración mensual de parámetros para el cálculo de costos de interoperabilidad: define el tipo de comprobante contable, si se estiman costos de mano de obra y si se incluyen costos contables, por año y mes.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'InteropCostSetting';
