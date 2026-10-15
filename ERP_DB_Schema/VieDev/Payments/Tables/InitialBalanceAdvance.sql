CREATE TABLE [Payments].[InitialBalanceAdvance] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InitialBalanceId]            INT           NOT NULL,
    [SupplierId]                  INT           NOT NULL,
    [ThirdPartyId]                INT           NOT NULL,
    [SupplierDistributionLinesId] INT           NOT NULL,
    [MainAccountId]               INT           NOT NULL,
    [CostCenterId]                INT           NULL,
    [AdvancePaymentsId]           INT           NULL,
    [AdvancePaymentsDate]         DATETIME      NOT NULL,
    [AdvancePaymentsDescription]  VARCHAR (200) NOT NULL,
    [Value]                       DECIMAL (18)  NOT NULL,
    CONSTRAINT [PK_InitialBalanceAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InitialBalanceAdvance_AdvancePayments] FOREIGN KEY ([AdvancePaymentsId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_InitialBalance] FOREIGN KEY ([InitialBalanceId]) REFERENCES [Payments].[InitialBalance] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLinesId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_InitialBalanceAdvance_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_AdvancePayments];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_CostCenter];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_InitialBalance];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_MainAccounts];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_Supplier];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_SuppliersDistributionLines];


GO
ALTER TABLE [Payments].[InitialBalanceAdvance] NOCHECK CONSTRAINT [FK_InitialBalanceAdvance_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'InitialBalanceAdvance', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';

