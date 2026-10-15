CREATE TABLE [Payments].[InitialBalanceAccountPayable] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialBalanceId]            INT             NOT NULL,
    [SupplierId]                  INT             NOT NULL,
    [ThirdPartyId]                INT             NOT NULL,
    [SupplierDistributionLinesID] INT             NOT NULL,
    [MainAccountId]               INT             NOT NULL,
    [CostCenterId]                INT             NULL,
    [AccountPayableId]            INT             NULL,
    [BillNumber]                  VARCHAR (20)    NOT NULL,
    [BillDate]                    DATETIME        NOT NULL,
    [Term]                        INT             NOT NULL,
    [ExpiredDate]                 DATETIME        NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    [Balance]                     DECIMAL (18, 2) NOT NULL,
    [ServicePeriodDate]           DATETIME        CONSTRAINT [DF_InitialBalanceAccountPayable_ServicePeriodDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [FilingUnitId]                INT             NOT NULL,
    [SupplierTypeId]              INT             NOT NULL,
    [RadicatedDate]               DATETIME        CONSTRAINT [DF__InitialBa__Radic__5D273EA6] DEFAULT ('2022-01-01') NOT NULL,
    [CurrencyId]                  INT             NULL,
    CONSTRAINT [PK_InitialBalanceAccountPayable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_FilingUnit] FOREIGN KEY ([FilingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_InitialBalance] FOREIGN KEY ([InitialBalanceId]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLinesID]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_InitialBalanceAccountPayable_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_AccountPayable];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_CostCenter];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_Currency];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_FilingUnit];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_InitialBalance];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_MainAccounts];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_Supplier];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_SuppliersDistributionLines];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_SupplierType];


GO
ALTER TABLE [Payments].[InitialBalanceAccountPayable] NOCHECK CONSTRAINT [FK_InitialBalanceAccountPayable_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAccountPayable', @level2type = N'COLUMN', @level2name = N'CurrencyId';

