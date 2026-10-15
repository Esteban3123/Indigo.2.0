CREATE TABLE [FixedAsset].[AsistentialLoanAccountingInformation] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEquipmentCatalog]                  INT NOT NULL,
    [IdAccountingStructure]               INT NOT NULL,
    [IdLoanSpendAccountingAccount]        INT NOT NULL,
    [IdLoanLeasingSpendAccountingAccount] INT NOT NULL,
    CONSTRAINT [PK_AsistentialLoanAccountingInformation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AsistentialLoanAccountingInformation_AccountingStructure] FOREIGN KEY ([IdAccountingStructure]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_AsistentialLoanAccountingInformation_MainAccounts] FOREIGN KEY ([IdLoanLeasingSpendAccountingAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AsistentialLoanAccountingInformation_MainAccounts1] FOREIGN KEY ([IdLoanSpendAccountingAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[AsistentialLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AsistentialLoanAccountingInformation_AccountingStructure];


GO
ALTER TABLE [FixedAsset].[AsistentialLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AsistentialLoanAccountingInformation_MainAccounts];


GO
ALTER TABLE [FixedAsset].[AsistentialLoanAccountingInformation] NOCHECK CONSTRAINT [FK_AsistentialLoanAccountingInformation_MainAccounts1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Información contable de préstamos existenciales', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';

