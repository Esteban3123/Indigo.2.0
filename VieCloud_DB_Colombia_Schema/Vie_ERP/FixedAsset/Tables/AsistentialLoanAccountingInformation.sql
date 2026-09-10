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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de cuenta principal (MainAccounts) para registrar gasto por depreciación asociado a leasing/arrendamiento financiero de equipos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación x Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanLeasingSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de cuenta principal (MainAccounts) para registrar gasto por depreciación de préstamos; vincula movimientos contables de depreciación directa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Contable Gasto Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdLoanSpendAccountingAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la estructura contable/centro de costo; referencia a Payroll.AccountingStructure para clasificación presupuestaria y departamental.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Estructura Contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdAccountingStructure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) del catálogo de equipos médicos/asistenciales asociado al préstamo; vincula con el inventario de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Catálogo del Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'IdEquipmentCatalog';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de registro de información contable de préstamos asistenciales; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Información contable de préstamos existenciales', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la configuración contable de los activos fijos asistenciales dados en préstamo, vinculando cada equipo del catálogo con las cuentas contables correspondientes al gasto por préstamo y por leasing.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'AsistentialLoanAccountingInformation';
