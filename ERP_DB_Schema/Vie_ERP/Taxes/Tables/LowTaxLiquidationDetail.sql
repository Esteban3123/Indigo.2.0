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
ALTER TABLE [Taxes].[LowTaxLiquidationDetail] NOCHECK CONSTRAINT [FK_LowTaxLiquidationDetail_LowTaxLiquidation];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor total Impuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'TotalValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del objeto publicado', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de días que permanece instalada la publicidad', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'NumberOfDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'concepto de instalación o fijación de  avisos, carteles o afiches y la distribución de volantes:    1. PASACALLES  2. AVISOS NO ADOSADOS A LA PARED INFERIOR A 8 METROS  CUADRADOS  3. PENDONES Y FESTONES  4. AFICHES Y VOLANTES', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Concept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'LowTaxLiquidationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'LowTaxLiquidationDetail', @level2type = N'COLUMN', @level2name = N'Id';

