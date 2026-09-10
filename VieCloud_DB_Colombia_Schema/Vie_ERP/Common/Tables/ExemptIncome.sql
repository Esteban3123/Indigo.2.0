CREATE TABLE [Common].[ExemptIncome] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ThirdPartyId]      INT             NOT NULL,
    [DateLiquidation]   DATETIME        NOT NULL,
    [VoucherType]       VARCHAR (30)    NULL,
    [VoucherCode]       VARCHAR (10)    NULL,
    [MonthlyIncome]     NUMERIC (18, 2) NULL,
    [ExemptIncomeValue] NUMERIC (18, 2) NOT NULL,
    [Comments]          VARCHAR (500)   NULL,
    CONSTRAINT [PK_ExemptIncome] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExemptIncome_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas y observaciones sobre la renta exenta del tercero, información complementaria de la liquidación, detalles fiscales o contables (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica información de la renta exenta.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en pesos de la renta exenta mensual del tercero, monto que no está sujeto a tributación (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ExemptIncomeValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la renta exenta mensual ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ExemptIncomeValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ExemptIncomeValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ingresos totales del mes del tercero, base para cálculo de renta exenta (NUMERIC 18,2)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'MonthlyIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingresos del mes ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'MonthlyIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'MonthlyIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del comprobante contable asociado a la liquidación de renta exenta (VARCHAR 10)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de comprobante contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o clasificación del comprobante contable: factura, recibo, resolución, etc. (VARCHAR 30)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comprobante contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de liquidación de la renta exenta, momento en que se calcula y registra el valor exento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'DateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la fecha de liquidación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'DateLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'DateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero (proveedor, profesional, contratista) que acumula renta exenta, FK a [Common].[ThirdParty] (INT)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tercero Renta Excenta Acumulada de Terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la renta exenta acumulada, saldo inicial de terceros para seguimiento fiscal (INT IDENTITY PK)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tabla Renta Excenta para saldos inciales', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de ingresos no gravables (exentos de retención o impuesto) asociados a terceros. Guarda el valor del ingreso exento, el ingreso mensual, el tipo y código de comprobante, y la fecha de liquidación, para el cálculo de nómina o retenciones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome';
