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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de envío, se asigna si el tipo de reporte es 2:  1 - Correo Certificado  2 - Mensajería', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'SendType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del evento:  1 - Pendiente por Autorizar  2 - Autorizado  3 - No Autorizado  4 - Autorizado con Aval  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de reporte:  1 - Llamada Telefónica  2 - Envío Físico  3 - Registro Página Web  4 - Correo Electrónico  5 - Tramita Paciente  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkEvents', @level2type = N'COLUMN', @level2name = N'ReportType';

