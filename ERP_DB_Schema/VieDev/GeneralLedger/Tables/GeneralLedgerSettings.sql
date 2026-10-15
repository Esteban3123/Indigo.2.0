CREATE TABLE [GeneralLedger].[GeneralLedgerSettings] (
    [Id]                           INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDeficitAccount]             INT             NOT NULL,
    [IdSuperavitAccount]           INT             NOT NULL,
    [IdUtilityAccount]             INT             NOT NULL,
    [IdCloseDocument]              INT             NOT NULL,
    [IdDian]                       INT             NOT NULL,
    [IdDistrictTreasury]           INT             NOT NULL,
    [IdApprovalDocument]           INT             NOT NULL,
    [IdMovementDocument]           INT             NOT NULL,
    [IdIvaRetentionConcept]        INT             NOT NULL,
    [IdIcaRetentionConcept]        INT             NOT NULL,
    [IdSourceRetentionConcept]     INT             NOT NULL,
    [PrintSignature]               BIT             NOT NULL,
    [Signature]                    VARCHAR (100)   NULL,
    [Potition]                     VARCHAR (100)   NULL,
    [TaxReviewer]                  VARCHAR (100)   NULL,
    [ProfessionalCard]             VARCHAR (50)    NULL,
    [IdOperatingUnit]              INT             NOT NULL,
    [CreationUser]                 VARCHAR (20)    NOT NULL,
    [CreationDate]                 DATETIME        NOT NULL,
    [ModificationUser]             VARCHAR (20)    NULL,
    [ModificationDate]             DATETIME        NULL,
    [TimeStamp]                    ROWVERSION      NOT NULL,
    [HandlesElectronicBilling]     BIT             CONSTRAINT [DF_GeneralLedgerSettings_HandlesElectronicBilling] DEFAULT ((0)) NOT NULL,
    [SoftwareIdentifier]           VARCHAR (256)   NULL,
    [SoftwarePin]                  VARCHAR (128)   NULL,
    [SoftwareKey]                  VARCHAR (128)   NULL,
    [TestSetId]                    VARCHAR (128)   NULL,
    [UrlInvoices]                  VARCHAR (500)   NULL,
    [DigitalCertificate]           VARBINARY (MAX) NULL,
    [DigitalCertificateKey]        VARCHAR (50)    NULL,
    [Environment]                  BIT             CONSTRAINT [DF_GeneralLedgerSettings_Environment] DEFAULT ((0)) NOT NULL,
    [DianVersion]                  DECIMAL (18, 2) CONSTRAINT [DF_GeneralLedgerSettings_DianVersion] DEFAULT ((1)) NOT NULL,
    [HandlesElectronicPayroll]     BIT             CONSTRAINT [DF__GeneralLe__Handl__708E4D7E] DEFAULT ((0)) NOT NULL,
    [ElectronicPayrollIdentifier]  VARCHAR (256)   NULL,
    [ElectronicPayrollPin]         VARCHAR (128)   NULL,
    [ElectronicPayrollTestSetId]   VARCHAR (128)   NULL,
    [ElectronicPayrollEnvironment] BIT             CONSTRAINT [DF__GeneralLe__Elect__718271B7] DEFAULT ((0)) NOT NULL,
    [SupportDocumentIdentifier]    VARCHAR (256)   NULL,
    [SupportDocumentPin]           VARCHAR (128)   NULL,
    [SupportDocumentTestSetId]     VARCHAR (128)   NULL,
    [SupportDocumentEnvironment]   BIT             NOT NULL,
    [HandlesSupportDocument]       BIT             CONSTRAINT [DF_GeneralLedgerSettings_HandlesSupportDocument] DEFAULT ((0)) NOT NULL,
    [ValidateClientData]           BIT             CONSTRAINT [DF__GeneralLe__Valid__51EC2CD2] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_GeneralLedgerSettings__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingAccounting_DocumentType] FOREIGN KEY ([IdApprovalDocument]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingAccounting_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SettingAccounting_PUC] FOREIGN KEY ([IdDeficitAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_PUC1] FOREIGN KEY ([IdSuperavitAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_PUC2] FOREIGN KEY ([IdUtilityAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_PUC3] FOREIGN KEY ([IdDistrictTreasury]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_SettingAccounting_RetentionConcept] FOREIGN KEY ([IdCloseDocument]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingAccounting_RetentionConcept2] FOREIGN KEY ([IdMovementDocument]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingAccounting_RetentionConcept3] FOREIGN KEY ([IdIvaRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_RetentionConcept4] FOREIGN KEY ([IdIcaRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_RetentionConcept5] FOREIGN KEY ([IdSourceRetentionConcept]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_SettingAccounting_ThirdParty] FOREIGN KEY ([IdDian]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_DocumentType];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_OperatingUnit];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_PUC];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_PUC1];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_PUC2];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_PUC3];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_RetentionConcept];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_RetentionConcept2];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_RetentionConcept3];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_RetentionConcept4];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_RetentionConcept5];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSettings] NOCHECK CONSTRAINT [FK_SettingAccounting_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Documento de soporte. De ser verdadero, se habilitan los campos:  SupportDocumentPayrollIdentifier  SupportDocumentPayrollPin  SupportDocumentPayrollTestSetId  SupportDocumentPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesSupportDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución del Documento Soporte:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución de la Nómina Electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Nómina Electrónica. De ser verdadero, se habilitan los campos:  ElectronicPayrollIdentifier  ElectronicPayrollPin  ElectronicPayrollTestSetId  ElectronicPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución de la facturación electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Facturación Electrónica. De ser verdadero, se habilitan los campos:  SoftwareIdentifier  SoftwarePin  SoftwareKey  UrlInvoces  DigitalSignature  DigitalSignatureKey', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicBilling';

