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
ALTER TABLE [Common].[ExemptIncome] NOCHECK CONSTRAINT [FK_ExemptIncome_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica información de la renta exenta.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la renta exenta mensual ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ExemptIncomeValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ingresos del mes ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'MonthlyIncome';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de comprobante contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante contable', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'VoucherType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la fecha de liquidación.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'DateLiquidation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tercero Renta Excenta Acumulada de Terceros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tabla Renta Excenta para saldos inciales', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ExemptIncome', @level2type = N'COLUMN', @level2name = N'Id';

