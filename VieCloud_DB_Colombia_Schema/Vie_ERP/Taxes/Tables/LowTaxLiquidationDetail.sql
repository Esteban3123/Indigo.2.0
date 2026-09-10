CREATE TABLE [Taxes].[LowTaxLiquidationDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LowTaxLiquidationId] INT             NOT NULL,
    [Concept]             TINYINT         NOT NULL,
    [NumberOfDays]        INT             NOT NULL,
    [Quantity]            INT             NOT NULL,
    [TotalValueTax]       NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_LowTaxLiquidationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LowTaxLiquidationDetail_LowTaxLiquidation] FOREIGN KEY ([LowTaxLiquidationId]) REFERENCES [Taxes].[LowTaxLiquidation] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del impuesto calculado, monto monetario (NUMERIC 18,2) a liquidar por concepto de publicidad o aviso', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor total Impuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del objeto publicado, número de avisos, carteles, afiches, volantes o pasacalles instalados', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del objeto publicado', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de días durante los cuales permanece instalada o vigente la publicidad, aviso o cartel en el sitio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'NumberOfDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de días que permanece instalada la publicidad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'NumberOfDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'NumberOfDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de instalación o fijación de avisos: 1=PASACALLES, 2=AVISOS NO ADOSADOS A PARED INFERIOR A 8M², 3=PENDONES Y FESTONES, 4=AFICHES Y VOLANTES', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'concepto de instalación o fijación de  avisos, carteles o afiches y la distribución de volantes:    1. PASACALLES  2. AVISOS NO ADOSADOS A LA PARED INFERIOR A 8 METROS  CUADRADOS  3. PENDONES Y FESTONES  4. AFICHES Y VOLANTES', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Concept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la liquidación de impuesto bajo, clave foránea a LowTaxLiquidation (cabecera del documento)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LowTaxLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LowTaxLiquidationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LowTaxLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del detalle de liquidación de impuesto bajo, clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la liquidación de impuesto de renta baja (bajo impuesto), donde se registran los conceptos, días, cantidades y valores tributarios que componen cada liquidación.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail';
