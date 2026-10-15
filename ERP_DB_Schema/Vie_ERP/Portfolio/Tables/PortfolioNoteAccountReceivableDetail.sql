CREATE TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioNoteAccountReceivableId] INT             NOT NULL,
    [EntityName]                       VARCHAR (250)   NOT NULL,
    [EntityId]                         INT             NOT NULL,
    [MainAccountId]                    INT             NOT NULL,
    [CostCenterId]                     INT             NULL,
    [Value]                            NUMERIC (18, 2) NOT NULL,
    [BaseValue]                        NUMERIC (20, 2) CONSTRAINT [DF__Portfolio__BaseV__46505CF4] DEFAULT ((0)) NOT NULL,
    [TaxValue]                         NUMERIC (20, 2) CONSTRAINT [DF__Portfolio__TaxVa__4744812D] DEFAULT ((0)) NOT NULL,
    [TaxPercentage]                    NUMERIC (5, 2)  NULL,
    [TaxId]                            INT             NULL,
    CONSTRAINT [PK_PortfolioNoteAccountReceivableDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_PortfolioNoteAccountReceivableAdvance] FOREIGN KEY ([PortfolioNoteAccountReceivableId]) REFERENCES [Portfolio].[PortfolioNoteAccountReceivableAdvance] ([Id]),
    CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_TaxId] FOREIGN KEY ([TaxId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioNoteAccountReceivableDetail] NOCHECK CONSTRAINT [FK_PortfolioNoteAccountReceivableDetail_PortfolioNoteAccountReceivableAdvance];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del IVA Asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el porcentaje de impuestos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor base, antes de IVA', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'BaseValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del ajuste', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Centro de costo del detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable del detalle', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la entidad origen asociada al registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la entidad origen relacionada al registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la cuenta por cobrar asociada a la nota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'PortfolioNoteAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioNoteAccountReceivableDetail', @level2type = N'COLUMN', @level2name = N'Id';

