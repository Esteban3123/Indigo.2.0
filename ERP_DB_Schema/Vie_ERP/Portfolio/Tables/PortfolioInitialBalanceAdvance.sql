CREATE TABLE [Portfolio].[PortfolioInitialBalanceAdvance] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceId] INT           NOT NULL,
    [Type]                      TINYINT       CONSTRAINT [DF_PortfolioInitialBalanceAdvance_Type] DEFAULT ((1)) NOT NULL,
    [ThirdPartyId]              INT           NOT NULL,
    [MainAccountId]             INT           NOT NULL,
    [CostCenterId]              INT           NULL,
    [DocumentDate]              DATETIME      NOT NULL,
    [CustomerId]                INT           NULL,
    [Value]                     NUMERIC (18)  NOT NULL,
    [Observations]              VARCHAR (300) NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAdvance_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAdvance_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAdvance_PortfolioInitialBalance] FOREIGN KEY ([PortfolioInitialBalanceId]) REFERENCES [Portfolio].[PortfolioInitialBalance] ([Id]),
    CONSTRAINT [FK_PortfolioInitialBalanceAdvance_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAdvance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAdvance_CostCenter];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAdvance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAdvance_Customer];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAdvance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAdvance_MainAccounts];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAdvance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAdvance_PortfolioInitialBalance];


GO
ALTER TABLE [Portfolio].[PortfolioInitialBalanceAdvance] NOCHECK CONSTRAINT [FK_PortfolioInitialBalanceAdvance_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cliente asociado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo del Anticipo  1 - Anticipo Entidad  2 - Anticipo Tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'Id';

