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
    [ValidateInvoiceTotal]         BIT             CONSTRAINT [DF_GeneralLedgerSettings_ValidateInvoiceTotal] DEFAULT ((0)) NOT NULL,
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



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_GeneralLedgerSettings__IdOperatingUnit]
    ON [GeneralLedger].[GeneralLedgerSettings]([IdOperatingUnit] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validar datos del cliente (BIT, 1=Sí/0=No). Habilita verificación de información del tercero, paciente o entidad antes de procesar transacciones contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ValidateClientData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Validar datos del cliente | 1 = Si | 0 = No ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ValidateClientData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ValidateClientData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manejo de Documento de Soporte electrónico (BIT). Si es verdadero, habilita campos: SupportDocumentIdentifier, SupportDocumentPin, SupportDocumentTestSetId, SupportDocumentEnvironment para integración DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesSupportDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si maneja Documento de soporte. De ser verdadero, se habilitan los campos:  SupportDocumentPayrollIdentifier  SupportDocumentPayrollPin  SupportDocumentPayrollTestSetId  SupportDocumentPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesSupportDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesSupportDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entorno de ejecución del Documento de Soporte (BIT): 0=Pruebas/Sandbox, 1=Producción. Define dónde se validan y transmiten los documentos de soporte ante DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentEnvironment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entorno de ejecución del Documento Soporte:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentEnvironment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de set de pruebas DIAN para Documento de Soporte (VARCHAR 128). Credencial para validación en ambiente sandbox antes de producción.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentTestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave del set de pruebas proporcionado por la DIAN para el Documento Soporte', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentTestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentTestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PIN del Software de Documento de Soporte (VARCHAR 128). Credencial asignada por DIAN al registrar software, para autenticación en transmisiones electrónicas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentPin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pin del Software del Documento Soporte. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentPin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentPin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Software de Documento de Soporte (VARCHAR 256). Código único obtenido en registro DIAN, identifica el software ante autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Software del Documento Soporte. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SupportDocumentIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entorno de ejecución de Nómina Electrónica (BIT): 0=Pruebas/Sandbox, 1=Producción. Define dónde se validan y transmiten nóminas ante DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollEnvironment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entorno de ejecución de la Nómina Electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollEnvironment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollEnvironment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de set de pruebas DIAN para Nómina Electrónica (VARCHAR 128). Credencial para validación en ambiente sandbox de nómina electrónica.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollTestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave del set de pruebas proporcionado por la DIAN para la Nómina Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollTestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollTestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PIN del Software de Nómina Electrónica (VARCHAR 128). Credencial asignada por DIAN para autenticación en transmisión de nóminas electrónicas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pin del Software de Nómina Electrónica. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollPin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Software de Nómina Electrónica (VARCHAR 256). Código único obtenido en registro DIAN para transmisión de nóminas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Software de Nómina Electrónica. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ElectronicPayrollIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manejo de Nómina Electrónica (BIT). Si es verdadero, habilita campos: ElectronicPayrollIdentifier, ElectronicPayrollPin, ElectronicPayrollTestSetId, ElectronicPayrollEnvironment.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si maneja Nómina Electrónica. De ser verdadero, se habilitan los campos:  ElectronicPayrollIdentifier  ElectronicPayrollPin  ElectronicPayrollTestSetId  ElectronicPayrollEnvironment', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicPayroll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de Facturación Electrónica DIAN (DECIMAL 18,2, default=1). Controla compatibilidad de formato UBL/XML con resoluciones DIAN vigentes.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DianVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión Dian de Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DianVersion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DianVersion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entorno de ejecución de Facturación Electrónica (BIT, default=0): 0=Pruebas/Sandbox, 1=Producción. Define dónde se validan y transmiten facturas electrónicas ante DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entorno de ejecución de la facturación electrónica:  0 - Pruebas  1 - Producción', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Environment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave/contraseña del Certificado Digital (VARCHAR 50, PII). Credencial para desbloquear archivo de firma digital expedido por autoridad certificadora acreditada.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificateKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave del archivo de Certificado digital obtenido del ente acreditado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificateKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificateKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Certificado Digital X.509 (VARBINARY MAX, PII, Sensitive). Archivo de firma digital necesario para autenticación y no repudio en facturas electrónicas.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Certificado Digital. Archivo necesario para la firma digital.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'DigitalCertificate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del endpoint de envío de facturas electrónicas (VARCHAR 500). Dirección proporcionada por DIAN para transmisión de facturas en ambiente de pruebas o producción.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'UrlInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Url para el envío de Facturas o Url ambiente. Valor enviado por la DIAN con los datos de acceso al ambiente de pruebas de Software.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'UrlInvoices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'UrlInvoices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave de set de pruebas DIAN para Facturación Electrónica (VARCHAR 128). Credencial para validación en sandbox antes de envíos a producción.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clave del set de pruebas proporcionado por la DIAN para la Facturación Electrónica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TestSetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TestSetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña del Software/Ambiente (VARCHAR 128, PII, Sensitive). Credencial registrada como Facturador Electrónico, requiere para acceso a plataforma DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contraseña del Software o Contraseña del Ambiente. Valor ingresado en la plataforma al momento de registrarse como Facturador Electrónico.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareKey';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareKey';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PIN del Software de Facturación Electrónica (VARCHAR 128, PII, Sensitive). Credencial asignada por DIAN al registrar software para autenticación en transmisiones.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwarePin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pin del Software. Valor que se asignó para el software registrado en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwarePin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwarePin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Software de Facturación Electrónica (VARCHAR 256). Código único obtenido en registro DIAN, identifica facturador ante autoridad tributaria.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Software. Valor obtenido en el registro del software en el sitio de la DIAN.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareIdentifier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'SoftwareIdentifier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de manejo de Facturación Electrónica (BIT, default=0). Si es verdadero, habilita: SoftwareIdentifier, SoftwarePin, SoftwareKey, UrlInvoices, DigitalCertificate, DigitalCertificateKey.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si maneja Facturación Electrónica. De ser verdadero, se habilitan los campos:  SoftwareIdentifier  SoftwarePin  SoftwareKey  UrlInvoces  DigitalSignature  DigitalSignatureKey', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicBilling';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'HandlesElectronicBilling';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo automatizada (TIMESTAMP). Sello temporal inmutable generado en creación, registro o modificación del registro de configuración contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME nullable). Rastrea cuándo fue actualizada la configuración contable de la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó última modificación (VARCHAR 20 nullable). Identifica responsable del cambio en configuración de contabilidad/facturación.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Marca inicio del registro de configuración en el módulo de contabilidad general.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (VARCHAR 20). Identifica responsable de generación inicial de la configuración contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de Unidad Operativa (INT, FK→Common.OperatingUnit). Referencia la sede/centro/sucursal a la cual aplica esta configuración contable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarjeta profesional del contador/revisor (VARCHAR 50). Número de cédula profesional del responsable de la contabilidad.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'ProfessionalCard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Revisor Fiscal (VARCHAR 100). Identificación del profesional con rol de revisión fiscal en la organización.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TaxReviewer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Revisor fiscal', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TaxReviewer';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'TaxReviewer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo/Posición profesional (VARCHAR 100). Título del puesto del contador o revisor fiscal responsable.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Potition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Potition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Potition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Firma digital/electrónica del contador (VARCHAR 100). Representación de la firma autógrafa para documentos contables impresos.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Signature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Firma del contador', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Signature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Signature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Habilitar impresión de firmas (BIT). Indica si las firmas se incluyen en reportes y documentos imprimibles.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'PrintSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imprimir firmas', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'PrintSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'PrintSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Concepto de Retención en la Fuente (INT, FK→GeneralLedger.RetentionConcepts). Referencia al concepto fiscal para retenciones de impuestos sobre ingresos/ganancias.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSourceRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de retención de fuente', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSourceRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSourceRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Concepto de Retención ICA (INT, FK→GeneralLedger.RetentionConcepts). Referencia al concepto fiscal para retenciones del Impuesto de Industria y Comercio.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIcaRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de retención Ica', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIcaRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIcaRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Concepto de Retención IVA (INT, FK→GeneralLedger.RetentionConcepts). Referencia al concepto fiscal para retenciones del Impuesto al Valor Agregado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIvaRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Concepto de retención Iva', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIvaRetentionConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdIvaRetentionConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Tipo de Documento de Movimiento (INT, FK→GeneralLedger.JournalVoucherTypes). Referencia al tipo de comprobante usado en asientos contables de movimiento.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdMovementDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id documento de movimeinto ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdMovementDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdMovementDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Tipo de Documento de Aprobación (INT, FK→GeneralLedger.JournalVoucherTypes). Referencia al comprobante para autorizaciones contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdApprovalDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Documento de aprobación', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdApprovalDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdApprovalDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Tesorería del Distrito (INT, FK→Common.ThirdParty). Referencia al tercero que representa la tesorería municipal/distrital.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDistrictTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id tesoreria del distrito ', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDistrictTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDistrictTreasury';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Entidad DIAN (INT, FK→Common.ThirdParty). Referencia al tercero configurado para DIAN en el catálogo.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDian';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Dian', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDian';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDian';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Tipo de Documento de Cierre (INT, FK→GeneralLedger.JournalVoucherTypes). Referencia al comprobante usado en asientos de cierre de períodos contables.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdCloseDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Cerrar documento', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdCloseDocument';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdCloseDocument';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cuenta de Utilidad/Ganancias (INT, FK→GeneralLedger.MainAccounts). Referencia a cuenta principal del PUC para registrar ganancias/utilidades.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdUtilityAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta de utilidad', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdUtilityAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdUtilityAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cuenta de Superávit (INT, FK→GeneralLedger.MainAccounts). Referencia a cuenta principal del PUC para registrar excedentes de capital.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSuperavitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Cuenta Superavit', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSuperavitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdSuperavitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Cuenta de Déficit (INT, FK→GeneralLedger.MainAccounts). Referencia a cuenta principal del PUC para registrar pérdidas/déficits.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDeficitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cuenta de déficit', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDeficitAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'IdDeficitAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY, PK). Clave primaria autoincrementable de la configuración contable y fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general de la contabilidad y facturación electrónica de la organización. Guarda los parámetros del libro mayor, las cuentas contables de cierre, las credenciales para facturación electrónica ante la DIAN, nómina electrónica y documentos soporte, así como los datos del revisor fiscal y certificado digital.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSettings';
