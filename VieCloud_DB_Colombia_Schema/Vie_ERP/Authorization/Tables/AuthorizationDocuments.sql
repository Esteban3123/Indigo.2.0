CREATE TABLE [Authorization].[AuthorizationDocuments] (
    [Id]              INT            NOT NULL IDENTITY(1,1),
    [AnnexId]         INT            NULL,
    [EventId]         INT            NULL,
    [PatientCode]     VARCHAR(15)    NOT NULL,
    [AdmissionNumber] VARCHAR(10)    NOT NULL,
    [CareCenterCode]  VARCHAR(10)    NOT NULL,
    [FileName]        NVARCHAR(255)  NOT NULL,
    [FileExtension]   VARCHAR(10)    NOT NULL,
    [Observations]    NVARCHAR(255)  NULL,
    [CreationUser]    VARCHAR(20)    NOT NULL,
    [CreationDate]    DATETIME       NOT NULL DEFAULT (Common.GETDATE()),
    CONSTRAINT [PK_AuthorizationDocuments]     PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuthDoc_Annex]              FOREIGN KEY ([AnnexId]) REFERENCES [Authorization].[AuthorizationAnnexes]([Id]),
    CONSTRAINT [FK_AuthDoc_Event]              FOREIGN KEY ([EventId])  REFERENCES [Authorization].[AuthorizationEvents]([Id]),
    CONSTRAINT [CK_AuthDoc_Owner] CHECK (
        ([AnnexId] IS NOT NULL AND [EventId] IS NULL) OR
        ([AnnexId] IS NULL     AND [EventId] IS NOT NULL)
    )
);
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento adjunto de autorización; clave primaria IDENTITY; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'Id';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anexo de autorización al que pertenece el documento; FK a Authorization.AuthorizationAnnexes; INT NULL; exclusivo con EventId por constraint CK_AuthDoc_Owner', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'AnnexId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anexo al que pertenece el documento; nulo si el documento pertenece a un evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'AnnexId';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del evento de autorización al que pertenece el documento; FK a Authorization.AuthorizationEvents; INT NULL; exclusivo con AnnexId por constraint CK_AuthDoc_Owner', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'EventId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del evento al que pertenece el documento; nulo si el documento pertenece a un anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'EventId';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente al que corresponde el documento; VARCHAR(15) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso hospitalario al que pertenece el documento adjunto; VARCHAR(10) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de ingreso hospitalario', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención donde se originó el documento adjunto; VARCHAR(10) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CareCenterCode';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo adjunto incluyendo extensión; tras guardar el registro el archivo en disco se renombra a {Id}{FileExtension}; NVARCHAR(255) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'FileName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del archivo adjunto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'FileName';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo adjunto (ej. .pdf, .jpg, .png, .gif, .bmp); VARCHAR(10) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'FileExtension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extensión del archivo adjunto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'FileExtension';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o descripción del contenido del documento adjunto; NVARCHAR(255) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del documento adjunto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'Observations';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que adjuntó el documento; VARCHAR(20) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que adjuntó el documento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro en hora local (Common.GETDATE()); DATETIME NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de creación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos adjuntos de autorizaciones intrahospitalarias. Almacena los archivos digitales asociados a un anexo (AuthorizationAnnexes) o a un evento (AuthorizationEvents), nunca a ambos (CK_AuthDoc_Owner). Los archivos se almacenan en ruta compartida bajo eTipoCarpeta.Decreto_3047 y se referencian por nombre; tras guardar el registro el archivo en disco se renombra a {Id}{FileExtension}.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationDocuments';
GO
