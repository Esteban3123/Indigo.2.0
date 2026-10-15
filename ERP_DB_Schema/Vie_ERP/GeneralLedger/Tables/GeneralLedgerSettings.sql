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
    [ValidateInvoiceTotal] BIT      CONSTRAINT [DF_GeneralLedgerSettings_ValidateInvoiceTotal] DEFAULT ((0)) NOT NULL,
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
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GeneralLedgerSettings__IdOperatingUnit]
    ON [GeneralLedger].[GeneralLedgerSettings]([IdOperatingUnit] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validar datos del cliente | 1 = Si | 0 = No ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ValidateClientData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Documento de soporte. De ser verdadero, se habilitan los campos:  SupportDocumentPayrollIdentifier  SupportDocumentPayrollPin  SupportDocumentPayrollTestSetId  SupportDocumentPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesSupportDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución del Documento Soporte:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave del set de pruebas proporcionado por la DIAN para el Documento Soporte', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentTestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pin del Software del Documento Soporte. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentPin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Software del Documento Soporte. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución de la Nómina Electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave del set de pruebas proporcionado por la DIAN para la Nómina Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollTestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pin del Software de Nómina Electrónica. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Software de Nómina Electrónica. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Nómina Electrónica. De ser verdadero, se habilitan los campos:  ElectronicPayrollIdentifier  ElectronicPayrollPin  ElectronicPayrollTestSetId  ElectronicPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión Dian de Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DianVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entorno de ejecución de la facturación electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave del archivo de Certificado digital obtenido del ente acreditado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificateKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Certificado Digital. Archivo necesario para la firma digital.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Url para el envío de Facturas o Url ambiente. Valor enviado por la DIAN con los datos de acceso al ambiente de pruebas de Software.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'UrlInvoices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clave del set de pruebas proporcionado por la DIAN para la Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contraseña del Software o Contraseña del Ambiente. Valor ingresado en la plataforma al momento de registrarse como Facturador Electrónico.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Pin del Software. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwarePin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Software. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si maneja Facturación Electrónica. De ser verdadero, se habilitan los campos:  SoftwareIdentifier  SoftwarePin  SoftwareKey  UrlInvoces  DigitalSignature  DigitalSignatureKey', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Revisor fiscal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TaxReviewer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Potition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Firma del contador', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Signature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imprimir firmas', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'PrintSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de retención de fuente', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSourceRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de retención Ica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIcaRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Concepto de retención Iva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIvaRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id documento de movimeinto ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdMovementDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Documento de aprobación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdApprovalDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tesoreria del distrito ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDistrictTreasury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Dian', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDian';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Cerrar documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdCloseDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta de utilidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdUtilityAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Superavit', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSuperavitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cuenta de déficit', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDeficitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Id';

