CREATE TABLE [Admissions].[ElectronicSignature] (
    [Id]                                       INT           IDENTITY (1, 1) NOT NULL,
    [CODCENATE]                                CHAR (10)     NOT NULL,
    [ElectronicInterface]                      BIT           NOT NULL,
    [URL]                                      VARCHAR (250) NOT NULL,
    [APIToken]                                 VARCHAR (100) NOT NULL,
    [Supplier]                                 VARCHAR (100) NOT NULL,
    [ValidityTime]                             INT           NOT NULL,
    [UserCreation]                             CHAR (20)     NOT NULL,
    [DateCreation]                             DATETIME      NOT NULL,
    [UserModification]                         CHAR (20)     NULL,
    [DateModification]                         DATETIME      NULL,
    [RequireDocumentPhoto]                     BIT           DEFAULT ((1)) NULL,
    [RequireSelfiePhoto]                       BIT           DEFAULT ((1)) NULL,
    [SendAutomaticEmail]                       BIT           DEFAULT ((1)) NULL,
    [SendAutomaticWhatsapp]                    BIT           DEFAULT ((1)) NULL,
    [InstitutionalEmailForElectronicSignature] VARCHAR (100) NULL,
    CONSTRAINT [PK_ElectronicSignature] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSignature_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación de la configuración (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación (CHAR 20), auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el usuario quien lo modifico', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación de la configuración de firmas electrónicas (DATETIME)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha de creación  de las firmas electrónicas', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó la configuración (CHAR 20), auditoría de creación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el usuario de creación', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo máximo de vigencia en minutos/segundos (INT) para que un firmante complete la firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ValidityTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'para llever el tiempo maximo de vigencia para firmantes', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ValidityTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ValidityTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Proveedor de firma electrónica (VARCHAR 100): 1=Zapsign, 2=By Truora u otro integrador', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Supplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'para usuario o proveedor firma electronica    1=Zapsign     2 =By Truora', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Supplier';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Supplier';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Token de autenticación API (VARCHAR 100) para comunicación segura con proveedor de firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'APIToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda las respectivas API Token', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'APIToken';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'APIToken';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección URL (VARCHAR 250) de la interfaz o endpoint de firma electrónica del proveedor', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'URL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Para introducir la URL de la interfaz', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'URL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario (BIT) que especifica si está habilitada la interfaz de firma electrónica (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ElectronicInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ElectronicInterface';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'ElectronicInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (FK a ADCENATEN), unidad funcional o sede donde se parametriza la firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (INT IDENTITY), clave primaria de la configuración de firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=1): envía automáticamente enlace/notificación de firma por WhatsApp (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticWhatsapp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar si se realiza envío automático a Whatsapp 1. Si - 0. No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticWhatsapp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticWhatsapp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=1): envía automáticamente enlace/notificación de firma por correo electrónico (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticEmail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar si se realiza envío automático a Email 1. Si - 0. No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticEmail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'SendAutomaticEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=1): requiere selfie/foto del usuario durante firma electrónica (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireSelfiePhoto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar si se requiere selfie al realizar firma electrónica 1. Si - 0. No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireSelfiePhoto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireSelfiePhoto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT, default=1): requiere foto/escaneo del documento de identidad para validar firma (1=Sí, 0=No)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireDocumentPhoto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar si se requiere foto al realizar firma electrónica 1. Si - 0. No', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireDocumentPhoto';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'RequireDocumentPhoto';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo institucional parametrizado (VARCHAR 100) desde el cual se originan notificaciones de firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'InstitutionalEmailForElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar el correo institucional parametrizado para usar en firma electrónica', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'InstitutionalEmailForElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature', @level2type = N'COLUMN', @level2name = N'InstitutionalEmailForElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de firma electrónica por centro de atención: define el proveedor externo, la URL e integración API para firmar documentos digitalmente durante el proceso de admisión, incluyendo requisitos de fotos y envío automático de notificaciones al paciente.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'ElectronicSignature';
