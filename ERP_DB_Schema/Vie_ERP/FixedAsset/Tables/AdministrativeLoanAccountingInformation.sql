CREATE TABLE [FixedAsset].[AdministrativeLoanAccountingInformation] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentCatalog]                  INT NOT NULL,
    [IdAccountingStructure]               INT NOT NULL,
    [IdLoanSpendAccountingAccount]        INT NOT NULL,
    [IdLoanLeasingSpendAccountingAccount] INT NOT NULL,
    CONSTRAINT [PK_AdministrativeLoanAccountingInformation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdministrativeLoanAccountingInformation_AccountingStructure] FOREIGN KEY ([IdAccountingStructure]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_AdministrativeLoanAccountingInformation_MainAccounts] FOREIGN KEY ([IdLoanLeasingSpendAccountingAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AdministrativeLoanAccountingInformation_MainAccounts1] FOREIGN KEY ([IdLoanSpendAccountingAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[AdministrativeLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AdministrativeLoanAccountingInformation_AccountingStructure];


GO
ALTER TABLE [FixedAsset].[AdministrativeLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AdministrativeLoanAccountingInformation_MainAccounts];


GO
ALTER TABLE [FixedAsset].[AdministrativeLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AdministrativeLoanAccountingInformation_MainAccounts1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla AdministrativeLoanAccountingInformation ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';

