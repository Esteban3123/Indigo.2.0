CREATE TABLE [Authorization].[TraceabilityPaperworkEvents] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId]        INT           NOT NULL,
    [TraceabilityPaperworkAnnexesId] INT           NULL,
    [HealthAdministratorId]          INT           NOT NULL,
    [ReportType]                     TINYINT       NOT NULL,
    [Instructions]                   VARCHAR (MAX) NULL,
    [Status]                         TINYINT       NOT NULL,
    [AuthorizationNumber]            VARCHAR (20)  NULL,
    [AuthorizedQuantity]             INT           NULL,
    [Observations]                   VARCHAR (MAX) NULL,
    [PatientNotificated]             BIT           NOT NULL,
    [InformationPatient]             VARCHAR (MAX) NULL,
    [PhoneNumber]                    VARCHAR (20)  NULL,
    [Extension]                      VARCHAR (10)  NULL,
    [InitialTime]                    TIME (0)      NULL,
    [EndTime]                        TIME (0)      NULL,
    [ContactPerson]                  VARCHAR (50)  NULL,
    [Charge]                         VARCHAR (50)  NULL,
    [RadicateNumber]                 VARCHAR (20)  NULL,
    [SendType]                       TINYINT       NULL,
    [ReceivedDate]                   DATETIME      NULL,
    [ReceivePerson]                  VARCHAR (50)  NULL,
    [URL]                            VARCHAR (100) NULL,
    [RegistrationDate]               DATETIME      NULL,
    [Email]                          VARCHAR (100) NULL,
    [SendDate]                       DATETIME      NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [AuthorizedBy]                   VARCHAR (300) NULL,
    [AuthorizationDate]              DATETIME      NULL,
    [AuthorizationExpiredDate]       DATETIME      NULL,
    CONSTRAINT [PK_TraceabilityPaperworkEvents] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkEvents_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperworkEvents_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperworkEvents_TraceabilityPaperworkAnnexes] FOREIGN KEY ([TraceabilityPaperworkAnnexesId]) REFERENCES [Authorization].[TraceabilityPaperworkAnnexes] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_TraceabilityPaperworkEvents_TraceabilityPaperworkId]
    ON [Authorization].[TraceabilityPaperworkEvents]([TraceabilityPaperworkId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la autorización (DATETIME). Se asigna cuando el estado es Autorizado(2). Define hasta cuándo es válida la aprobación del procedimiento, servicio o medicamento ante la entidad administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento de autorización, este campo se asigna cuando el estado es autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de autorización (DATETIME). Se asigna cuando el estado es Autorizado(2). Registra cuándo la entidad administradora aprobó la solicitud de autorización del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de autorización, este campo se asigna cuando el estado es autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación del profesional/funcionario que da la autorización (VARCHAR 300). Se asigna cuando el estado es Autorizado con Aval(4). Trazabilidad de quién resolvió positivamente la solicitud ante la EPS/administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona que da la autorización, este campo se asigna cuando el estado es autorizado con aval(4)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el evento de trazabilidad (VARCHAR 20). Identificación del operario que registró el evento en el sistema. Auditoría de origen del registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuari quien creó el evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del evento de trazabilidad (DATETIME). Timestamp de cuándo se originó este evento de seguimiento del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío del reporte por correo electrónico (DATETIME). Se asigna si ReportType=4. Registra cuándo se notificó por email a la entidad administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha envío, se asigna si el tipo de reporte es 4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de correo electrónico destinatario (VARCHAR 100). Se asigna si ReportType=4. Contacto para envío de notificaciones y reportes vía correo.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Email, se asigna si el tipo de reporte es 4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro en página web (DATETIME). Se asigna si ReportType=3. Marca cuándo se radica la solicitud en el portal digital de la administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha registro, se asigna si el tipo de reporte es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección web del portal de radicación (VARCHAR 100). Se asigna si ReportType=3. Enlace al sitio donde se registró el trámite, autorización o glosa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'URL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'URL de la página web, se asigna si el tipo de reporte es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'URL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona que recibió físicamente el documento (VARCHAR 50). Se asigna si ReportType=2. Trazabilidad de quién firmó recibido en mensajería o correo certificado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivePerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona recibe, se asigna si el tipo de reporte es 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivePerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivePerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción del envío físico (DATETIME). Se asigna si ReportType=2. Registra cuándo la entidad administradora o paciente recibió el documento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha recibido, se asigna si el tipo de reporte es 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de envío físico (TINYINT). Se asigna si ReportType=2. Valores: 1=Correo Certificado, 2=Mensajería. Indica método de entrega con comprobante.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de envío, se asigna si el tipo de reporte es 2:  1 - Correo Certificado  2 - Mensajería', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de radicado ante entidad (VARCHAR 20). Se asigna si ReportType=2 o 3. Identificador oficial del trámite en la administradora o portal.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de radicado, se asigna si el tipo de reporte es 2 y 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo o puesto del contacto (VARCHAR 50). Se asigna si ReportType=1 o 2. Rol del funcionario que recibió la llamada o la documentación física.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Charge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargo, se asigna si el tipo de reporte es 1 o 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Charge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Charge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del contacto en la entidad administradora (VARCHAR 50). Se asigna si ReportType=1. Persona con quien se coordinó la solicitud de autorización por teléfono.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persona de contacto, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización de la llamada (TIME). Se asigna si ReportType=1. Momento en que terminó el diálogo telefónico de solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio de la llamada (TIME). Se asigna si ReportType=1. Momento en que se originó el contacto telefónico para autorización o glosa.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inicial, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión telefónica del contacto (VARCHAR 10). Se asigna si ReportType=1. Número adicional para comunicarse con el funcionario en la entidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extensión del teléfono, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Extension';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto en la entidad administradora (VARCHAR 20). Se asigna si ReportType=1. Número usado para solicitar autorización, glosa o información de procedimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de teléfono, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la información comunicada al paciente (VARCHAR MAX). Detalles sobre qué se informó respecto al estado de autorización, glosa o trámite del procedimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InformationPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información dada al paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InformationPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InformationPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de notificación al paciente (BIT). Booleano que confirma si el paciente fue informado sobre el evento de autorización, negación, glosa o vencimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PatientNotificated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permite saber si el paciente fue notificado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PatientNotificated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PatientNotificated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas adicionales sobre el evento (VARCHAR MAX). Comentarios, aclaraciones o detalles relevantes del proceso de autorización o seguimiento del trámite.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades/sesiones/dosis autorizadas (INT). Se asigna si Status=Autorizado(2). Número de procedimientos, medicamentos o prestaciones aprobadas por la administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad autorizada, se asigna si el estado es autorizado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número oficial de autorización (VARCHAR 20). Se asigna si Status=Autorizado(2). Referencia única que la administradora asigna al aprobar la solicitud. Usado en facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de autorización, se asigna si el estado es autorizado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del evento de autorización (TINYINT). Valores: 1=Pendiente por Autorizar, 2=Autorizado, 3=No Autorizado, 4=Autorizado con Aval. Indica el resultado de la solicitud ante la administradora.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del evento:  1 - Pendiente por Autorizar  2 - Autorizado  3 - No Autorizado  4 - Autorizado con Aval  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones o procedimientos a seguir (VARCHAR MAX). Indicaciones que debe cumplir el paciente, centro o profesional para ejecutar la prestación autorizada.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Instructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Instructions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Instructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de reporte o medio de comunicación (TINYINT). Valores: 1=Llamada Telefónica, 2=Envío Físico, 3=Registro Página Web, 4=Correo Electrónico, 5=Trámita Paciente. Define cómo se reportó la solicitud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de reporte:  1 - Llamada Telefónica  2 - Envío Físico  3 - Registro Página Web  4 - Correo Electrónico  5 - Tramita Paciente  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReportType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora (INT, FK). Referencia a la EPS, prepagada, municipio o administradora que resuelve la autorización. Clave de negocio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad administradora', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del anexo asociado (INT, FK, nullable). Se asigna cuando un anexo (documento adjunto, examen) está vinculado a este evento. Nulo si no hay anexo específico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkAnnexesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del anexo, este campo se asigna cuando a un anexo se le asocia un evento, de lo contrario va nulo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkAnnexesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkAnnexesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera del trámite (INT, FK). Referencia al registro maestro de trazabilidad. Vincula el evento a la solicitud principal de autorización, glosa o reclamación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la trazabilidad del tramite', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del evento (INT, PK IDENTITY). Clave primaria que identifica este evento de trazabilidad dentro del historial de autorizaciones y trámites.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eventos y gestiones de trazabilidad de trámites de autorización ante administradoras de salud (EPS/aseguradoras). Registra cada acción realizada sobre un trámite: envíos, respuestas, autorizaciones otorgadas, notificaciones al paciente y radicaciones, permitiendo hacer seguimiento completo al ciclo de autorización de servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents';
