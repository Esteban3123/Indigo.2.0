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
ALTER TABLE [Authorization].[TraceabilityPaperworkEvents] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkEvents_HealthAdministrator];


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkEvents] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkEvents_TraceabilityPaperwork];


GO
ALTER TABLE [Authorization].[TraceabilityPaperworkEvents] NOCHECK CONSTRAINT [FK_TraceabilityPaperworkEvents_TraceabilityPaperworkAnnexes];


GO
CREATE NONCLUSTERED INDEX [IX_TraceabilityPaperworkEvents_TraceabilityPaperworkId]
    ON [Authorization].[TraceabilityPaperworkEvents]([TraceabilityPaperworkId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de vencimiento de autorización, este campo se asigna cuando el estado es autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de autorización, este campo se asigna cuando el estado es autorizado(2)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona que da la autorización, este campo se asigna cuando el estado es autorizado con aval(4)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuari quien creó el evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del evento', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha envío, se asigna si el tipo de reporte es 4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email, se asigna si el tipo de reporte es 4', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha registro, se asigna si el tipo de reporte es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'URL de la página web, se asigna si el tipo de reporte es 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona recibe, se asigna si el tipo de reporte es 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivePerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha recibido, se asigna si el tipo de reporte es 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReceivedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de envío, se asigna si el tipo de reporte es 2:  1 - Correo Certificado  2 - Mensajería', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de radicado, se asigna si el tipo de reporte es 2 y 3', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'RadicateNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cargo, se asigna si el tipo de reporte es 1 o 2', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Charge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persona de contacto, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora Final, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora inicial, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InitialTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Extensión del teléfono, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Extension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de teléfono, se asigna si el tipo de reporte es 1', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PhoneNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información dada al paciente', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'InformationPatient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite saber si el paciente fue notificado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'PatientNotificated';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad autorizada, se asigna si el estado es autorizado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No. de autorización, se asigna si el estado es autorizado', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del evento:  1 - Pendiente por Autorizar  2 - Autorizado  3 - No Autorizado  4 - Autorizado con Aval  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Instrucciones', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Instructions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de reporte:  1 - Llamada Telefónica  2 - Envío Físico  3 - Registro Página Web  4 - Correo Electrónico  5 - Tramita Paciente  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReportType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la entidad administradora', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del anexo, este campo se asigna cuando a un anexo se le asocia un evento, de lo contrario va nulo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkAnnexesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la trazabilidad del tramite', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Id';

