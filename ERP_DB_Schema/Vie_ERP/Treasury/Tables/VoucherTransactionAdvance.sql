CREATE TABLE [Treasury].[VoucherTransactionAdvance] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionD] INT             NOT NULL,
    [PortfolioAdvanceId]    INT             NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [Percentage]            NUMERIC (5, 2)  NOT NULL,
    CONSTRAINT [PK_VoucherTransactionAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VoucherTransactionAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_VoucherTransactionAdvance_VoucherTransactionDetails] FOREIGN KEY ([IdVoucherTransactionD]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[VoucherTransactionAdvance] NOCHECK CONSTRAINT [FK_VoucherTransactionAdvance_PortfolioAdvance];


GO
ALTER TABLE [Treasury].[VoucherTransactionAdvance] NOCHECK CONSTRAINT [FK_VoucherTransactionAdvance_VoucherTransactionDetails];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Anticipo del Paciente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del detalle del comprobante de egreso relacionado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'IdVoucherTransactionD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'VoucherTransactionAdvance', @level2type = N'COLUMN', @level2name = N'Id';

