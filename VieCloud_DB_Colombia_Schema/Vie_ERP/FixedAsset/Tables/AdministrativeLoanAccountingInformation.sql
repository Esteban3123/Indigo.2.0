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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para registrar gastos de depreciación y leasing de activos fijos; vinculado a tabla GeneralLedger.MainAccounts para control de egresos por arrendamiento financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para registrar gastos de depreciación de activos fijos; vinculado a tabla GeneralLedger.MainAccounts para control de egresos contables', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de estructura contable (centro de costo, departamento, línea de negocio); vinculado a Payroll.AccountingStructure para asignar depreciación a unidades organizacionales', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo de equipos, activo fijo o bien mueble; referencia a configuración de tipos de equipos sujetos a préstamo administrativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de configuración contable para préstamos administrativos y leasing de activos fijos en el módulo FixedAsset', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla AdministrativeLoanAccountingInformation ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la información contable asociada a los préstamos administrativos de activos fijos, vinculando cada equipo del catálogo con las cuentas contables de gasto por préstamo y arrendamiento (leasing) dentro de la estructura contable definida.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AdministrativeLoanAccountingInformation';
