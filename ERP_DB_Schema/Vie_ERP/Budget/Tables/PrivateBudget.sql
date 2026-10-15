CREATE TABLE [Budget].[PrivateBudget] (
    [Id]                            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PrivateBudgetItemsStructureId] INT          NOT NULL,
    [MainAccountId]                 INT          NULL,
    [ThirdPartyId]                  INT          NULL,
    [CostCenterId]                  INT          NULL,
    [Month]                         TINYINT      NOT NULL,
    [Value]                         NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_PrivateBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PrivateBudget_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PrivateBudget_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PrivateBudget_PrivateBudgetItemsStructure] FOREIGN KEY ([PrivateBudgetItemsStructureId]) REFERENCES [Budget].[PrivateBudgetItemsStructure] ([Id]),
    CONSTRAINT [FK_PrivateBudget_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Budget].[PrivateBudget] NOCHECK CONSTRAINT [FK_PrivateBudget_CostCenter];


GO
ALTER TABLE [Budget].[PrivateBudget] NOCHECK CONSTRAINT [FK_PrivateBudget_MainAccounts];


GO
ALTER TABLE [Budget].[PrivateBudget] NOCHECK CONSTRAINT [FK_PrivateBudget_PrivateBudgetItemsStructure];


GO
ALTER TABLE [Budget].[PrivateBudget] NOCHECK CONSTRAINT [FK_PrivateBudget_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mes', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'Month';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'PrivateBudgetItemsStructureId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'PrivateBudget', @level2type = N'COLUMN', @level2name = N'Id';

