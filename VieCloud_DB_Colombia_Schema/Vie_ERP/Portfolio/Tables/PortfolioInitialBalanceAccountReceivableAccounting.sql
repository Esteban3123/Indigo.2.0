CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableAccounting] (
    [Id]                                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceAccountReceivableId] INT             NOT NULL,
    [MainAccountId]                              INT             NOT NULL,
    [ThirdPartyId]                               INT             NULL,
    [CostCenterId]                               INT             NULL,
    [Value]                                      NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivableAccounting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_PortfolioInitialBalanceAccountReceivable] FOREIGN KEY ([PortfolioInitialBalanceAccountReceivableId]) REFERENCES [Portfolio].[PortfolioInitialBalanceAccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableAccounting_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del saldo inicial a cobrar (cuentas por cobrar). Tipo NUMERIC(18,2), expresado en unidad monetaria del registro contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo, depósito, unidad funcional o área operativa asociada. Campo opcional (NULL) cuando la cuenta contable no maneja asignación por centro de costos. Referencia a Payroll.CostCenter.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo, depende si la cuenta contable maneja o no centro de costos, por eso puede ser null', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero, cliente, deudor, entidad externa o proveedor vinculado al saldo inicial de cuentas por cobrar. Campo opcional (NULL). Referencia a Common.ThirdParty.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal del plan de cuentas (Mayor General, Contabilidad). Referencia obligatoria a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o registro padre que agrupa los saldos iniciales de cuentas por cobrar (Portfolio). Referencia obligatoria a Portfolio.PortfolioInitialBalanceAccountReceivable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera para la cuenta de cobro de saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) del registro de desglose contable y de costos para saldos iniciales de cuentas por cobrar. Clave primaria de la estructura de distribución contable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estructura contable para cuentas de cobro', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros contables del saldo inicial de cartera en cuentas por cobrar: asocia cada saldo inicial de cartera con su cuenta contable principal, tercero y centro de costo, junto con el valor registrado.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableAccounting';
