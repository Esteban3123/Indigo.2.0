-- =============================================================================
-- Eventos de comunicación (teléfono, fax, web, email) ligados a un
-- AuthorizationControl y opcionalmente a un AuthorizationAnnexes.
-- Equivalente intrahospitalario de TraceabilityPaperworkEvents + legacy ADAUTEVEN.
-- =============================================================================

CREATE TABLE [Authorization].[AuthorizationEvents] (
    [Id]                        INT IDENTITY(1,1) NOT NULL,
    [AuthorizationControlId]    INT NOT NULL,
    [AuthorizationAnnexesId]    INT NULL,
    [HealthAdministratorId]     INT NULL,
    [ReportType]                TINYINT NOT NULL,
    [Status]                    TINYINT NULL,
    [FileNumber]                VARCHAR(20) NULL,
    [AuthorizationCode]         VARCHAR(20) NULL,
    [AuthorizedQuantity]        INT NULL,
    -- Llamada telefónica
    [PhoneNumber]               VARCHAR(20) NULL,
    [Extension]                 VARCHAR(10) NULL,
    [InitialTime]               TIME(0) NULL,
    [EndTime]                   TIME(0) NULL,
    [ContactPerson]             VARCHAR(80) NULL,
    [Charge]                    VARCHAR(50) NULL,
    -- Envío físico
    [RadicateNumber]            VARCHAR(20) NULL,
    [SendType]                  TINYINT NULL,
    [ReceivedDate]              DATETIME NULL,
    [RecipientPerson]           VARCHAR(80) NULL,
    [RecipientCharge]           VARCHAR(50) NULL,
    -- Web / Email
    [WebUrl]                    VARCHAR(150) NULL,
    [RegistrationDate]          DATETIME NULL,
    [RadicateNumberWebPage]     VARCHAR(20) NULL,
    [Email]                     VARCHAR(100) NULL,
    [SendDate]                  DATETIME NULL,
    -- General
    [Instructions]              VARCHAR(2000) NULL,
    [Comments]                  VARCHAR(2000) NULL,
    [PatientNotified]           BIT NOT NULL CONSTRAINT [DF_AuthorizationEvents_PatientNotified] DEFAULT (0),
    [PatientInfo]               VARCHAR(4000) NULL,
    [AuthorizedBy]              VARCHAR(300) NULL,
    [AuthorizationDate]         DATETIME NULL,
    [AuthorizationExpiredDate]  DATETIME NULL,
    [CreationUser]              VARCHAR(20) NOT NULL,
    [CreationDate]              DATETIME NOT NULL CONSTRAINT [DF_AuthorizationEvents_CreationDate] DEFAULT (Common.GETDATE()),
    CONSTRAINT [PK_AuthorizationEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationEvents_AuthorizationControl]
        FOREIGN KEY ([AuthorizationControlId])
        REFERENCES [Authorization].[AuthorizationControl] ([Id]),
    CONSTRAINT [FK_AuthorizationEvents_AuthorizationAnnexes]
        FOREIGN KEY ([AuthorizationAnnexesId])
        REFERENCES [Authorization].[AuthorizationAnnexes] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_AuthorizationEvents_ControlId]
    ON [Authorization].[AuthorizationEvents] ([AuthorizationControlId] ASC)
    INCLUDE ([CreationDate])
GO

-- =============================================================================
-- Extended properties
-- =============================================================================

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento de autorización intrahospitalaria; clave primaria IDENTITY; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del control de autorización al que pertenece este evento; FK a Authorization.AuthorizationControl; INT NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del control de autorización al que pertenece el evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anexo asociado al evento; FK a Authorization.AuthorizationAnnexes; INT NULL; nulo si el evento no está vinculado a un anexo específico', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationAnnexesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anexo asociado al evento; nulo si el evento no tiene anexo vinculado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationAnnexesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationAnnexesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora de salud (EPS/aseguradora) ante la cual se reporta el evento; FK lógica a Contract.HealthAdministrator; INT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora de salud', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reporte o canal de comunicación del evento: 1=Llamada Telefónica, 2=Envío Físico, 3=Registro Página Web, 4=Correo Electrónico, 5=Tramita Paciente; TINYINT NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de reporte: 1=Llamada Telefónica, 2=Envío Físico, 3=Registro Página Web, 4=Correo Electrónico, 5=Tramita Paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado que el evento produce sobre la autorización; comparte la máquina de estados del Dashboard Intrahospitalario (ERP-84): 2=En Trámite, 3=Radicado, 4=Autorizado, 8=Rechazado. Al guardar el evento se propaga a AuthorizationControl.Status. TINYINT NULL.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del evento: 2=En Trámite, 3=Radicado, 4=Autorizado, 8=Rechazado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado del expediente ante la entidad; se asigna cuando Status=3 (Radicado); se propaga a AuthorizationControl.FileNumber; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'FileNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de radicado del expediente, se asigna si Status=3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'FileNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-06', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'FileNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización otorgado por la entidad administradora; se asigna cuando Status=4 (Autorizado); usado en facturación y RIPS; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de autorización otorgado por la EPS cuando se autoriza', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades, sesiones o dosis autorizadas por la entidad; se asigna cuando Status=2 (Autorizado); INT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad autorizada cuando el estado es Autorizado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';


GO
-- Llamada telefónica
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto en la entidad administradora; se asigna cuando ReportType=1 (Llamada Telefónica); VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de teléfono, se asigna si ReportType=1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión telefónica del contacto en la entidad; se asigna cuando ReportType=1 (Llamada Telefónica); VARCHAR(10) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extensión del teléfono, se asigna si ReportType=1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio de la llamada telefónica; se asigna cuando ReportType=1; TIME(0) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inicial de la llamada, se asigna si ReportType=1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización de la llamada telefónica; se asigna cuando ReportType=1; TIME(0) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final de la llamada, se asigna si ReportType=1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contacto en la entidad administradora que atendió la llamada; se asigna cuando ReportType=1; VARCHAR(80) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona de contacto en la llamada, se asigna si ReportType=1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo del contacto en la entidad administradora; se asigna cuando ReportType=1 o 2; VARCHAR(50) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Charge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo del contacto, se asigna si ReportType=1 o 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Charge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Charge';


GO
-- Envío físico
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado o guía del envío físico ante la entidad; se asigna cuando ReportType=2 (Envío Físico); homólogo de TraceabilityPaperworkEvents.RadicateNumber; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de radicado del envío físico, se asigna si ReportType=2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-06', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de envío físico: 1=Correo Certificado, 2=Mensajería; se asigna cuando ReportType=2; TINYINT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de envío: 1=Correo Certificado, 2=Mensajería; se asigna si ReportType=2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que la entidad recibió el envío físico; se asigna cuando ReportType=2; DATETIME NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción del envío, se asigna si ReportType=2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona que recibió el documento físico en la entidad; se asigna cuando ReportType=2; VARCHAR(80) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que recibió el documento físico, se asigna si ReportType=2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo de la persona que recibió el documento físico; se asigna cuando ReportType=2; VARCHAR(50) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientCharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo del receptor del documento, se asigna si ReportType=2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientCharge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RecipientCharge';


GO
-- Web / Email
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL del portal web de la entidad donde se radicó la solicitud; se asigna cuando ReportType=3 (Página Web); VARCHAR(150) NULL. Para correo electrónico usar columna Email.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'WebUrl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL del portal de radicación, se asigna si ReportType=3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'WebUrl';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'WebUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro en el portal web de la entidad; se asigna cuando ReportType=3; DATETIME NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro web, se asigna si ReportType=3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado asignado por el portal web de la entidad; se asigna cuando ReportType=3; VARCHAR(20) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumberWebPage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de radicado en página web, se asigna si ReportType=3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumberWebPage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumberWebPage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico de contacto en la entidad; se asigna cuando ReportType=4 (Correo Electrónico); homólogo de TraceabilityPaperworkEvents.Email; VARCHAR(100) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email de contacto, se asigna si ReportType=4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-06', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío del reporte por correo electrónico; se asigna cuando ReportType=4; DATETIME NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envío por email, se asigna si ReportType=4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'SendDate';


GO
-- General
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones para el paciente o el autorizador asociadas al evento; VARCHAR(2000) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Instructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones del evento de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Instructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-07', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Instructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales sobre el evento; comentarios, aclaraciones o detalles relevantes del proceso de autorización; VARCHAR(2000) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios u observaciones del evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de notificación al paciente; BIT NOT NULL DEFAULT 0; confirma si el paciente fue informado sobre el resultado del evento de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientNotified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el paciente fue notificado del evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientNotified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientNotified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la información comunicada al paciente sobre el estado de la autorización; VARCHAR(4000) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientInfo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información comunicada al paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientInfo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'PatientInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación del funcionario de la EPS que otorgó la autorización; se asigna cuando Status=4 (Autorizado con Aval); VARCHAR(300) NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que da la autorización, se asigna cuando Status=Autorizado con Aval(4)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que la entidad administradora otorgó la autorización; se asigna cuando Status=2 (Autorizado); DATETIME NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de autorización, se asigna cuando Status=Autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la autorización otorgada; se asigna cuando Status=2 (Autorizado); define hasta cuándo es válida la aprobación; DATETIME NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de la autorización, se asigna cuando Status=Autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que creó el registro del evento; VARCHAR(20) NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creó el evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del evento vía Common.GETDATE(); DATETIME NOT NULL', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de creación del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-sonnet-4-6_2026-07-03', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eventos de comunicación y seguimiento de autorizaciones intrahospitalarias ante administradoras de salud (EPS/aseguradoras). Equivalente intrahospitalario de TraceabilityPaperworkEvents (ambulatorio). Registra cada gestión realizada sobre un control de autorización: llamadas telefónicas, envíos físicos, radicaciones web, correos electrónicos y notificaciones al paciente, permitiendo trazabilidad completa del ciclo de autorización de servicios durante el ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationEvents';
GO
