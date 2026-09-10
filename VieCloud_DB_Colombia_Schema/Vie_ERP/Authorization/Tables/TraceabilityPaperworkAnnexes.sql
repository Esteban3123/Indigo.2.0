CREATE TABLE [Authorization].[TraceabilityPaperworkAnnexes] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TraceabilityPaperworkId] INT           NOT NULL,
    [HealthAdministratorId]   INT           NOT NULL,
    [TypeRequestServices]     TINYINT       NOT NULL,
    [PriorityAttention]       TINYINT       NOT NULL,
    [Justification]           VARCHAR (MAX) NULL,
    [Folio]                   VARCHAR (20)  NULL,
    [Consecutive]             DECIMAL (18)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [DiagnosticCode]          VARCHAR (20)  NULL,
    CONSTRAINT [PK_TraceabilityPaperworkAnnexes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TraceabilityPaperworkAnnexes_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_TraceabilityPaperworkAnnexes_TraceabilityPaperwork] FOREIGN KEY ([TraceabilityPaperworkId]) REFERENCES [Authorization].[TraceabilityPaperwork] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10), asignado cuando no existe folio de ingreso para generar los anexos de autorización; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de diagnostico, se asigna cuando no hay folio para la generación de los anexos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'DiagnosticCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del anexo; VARCHAR(20); referencia a usuario del sistema', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuari quien creó el anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del anexo en el sistema; DATETIME', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del anexo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo del reporte de anexo registrado con código 11 en Common.Consecutive; DECIMAL(18)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del reporte de anexo, este consecutivo se registra con el código 11 en la tabla Common.Consecutive', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del ingreso/atención del cual se extrajo la justificación clínica; puede estar vacío si el paciente no tiene ingreso y se selecciona diagnóstico; VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. del folio del ingreso al cual se saco la justificación clinica, puede ir vacío cuando el item no tiene un ingreso y debe seleccionar un diagnostico', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Folio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica del anexo, argumentación médica para autorización de servicios; VARCHAR(MAX)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de prioridad de atención: 1=Prioritaria, 2=No Prioritaria; TINYINT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad de la atención:  1 - Prioritaria  2 - No Prioritaria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'PriorityAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicios solicitados: 1=Posterior a atención inicial, 2=Servicios electivos; TINYINT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de servicios solicitados:  1 - Posterior a la atención inicial  2 - Servicios electivos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TypeRequestServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad administradora de salud (aseguradora/EPS); FK a Contract.HealthAdministrator; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del trámite de trazabilidad; FK a Authorization.TraceabilityPaperwork; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de anexo de trazabilidad; clave primaria IDENTITY; INT', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anexos del trámite de trazabilidad de autorizaciones. Registra los documentos y solicitudes adjuntas a un trámite de autorización de servicios de salud, incluyendo el tipo de servicio solicitado, la prioridad de atención, el diagnóstico asociado y la justificación clínica presentada ante la administradora de salud.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'TraceabilityPaperworkAnnexes';
