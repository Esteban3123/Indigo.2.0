CREATE TABLE [dbo].[ReportRiskFactors] (
    [Id]                           INT           IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]                    VARCHAR (25)  NOT NULL,
    [IdRiskFactor]                 INT           NOT NULL,
    [Intervention]                 VARCHAR (600) NOT NULL,
    [Observation]                  VARCHAR (500) NOT NULL,
    [UserCreation]                 CHAR (20)     NOT NULL,
    [DateCreation]                 DATETIME      NOT NULL,
    [Status]                       INT           NULL,
    [LastActivationProfessional]   CHAR (20)     NULL,
    [LastActivationDate]           DATETIME      NULL,
    [LastInactivationProfessional] CHAR (20)     NULL,
    [LastInactivationDate]         DATETIME      NULL,
    [CancellationProfessionalCode] CHAR (20)     NULL,
    [CancellationReasonCode]       CHAR (4)      NULL,
    [CancellationObservation]      VARCHAR (500) NULL,
    [CancellationDate]             DATETIME      NULL,
    CONSTRAINT [PK_ReportRiskFactors] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ReportRiskFactors_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ReportRiskFactors_RiskFactor] FOREIGN KEY ([IdRiskFactor]) REFERENCES [dbo].[RiskFactor] ([Id]),
    CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_1] FOREIGN KEY ([UserCreation]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_2] FOREIGN KEY ([LastActivationProfessional]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_2];




GO



GO



GO



GO
ALTER TABLE [dbo].[ReportRiskFactors] NOCHECK CONSTRAINT [FK_ReportRiskFactors_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la anulación/cancelación del registro (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que indica cuando se anula un factor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación detallada o comentarios sobre la anulación del factor de riesgo (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la anulación del factor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación. FK a tabla HCMOANULB. Clasificación de causa (CHAR 4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationReasonCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del motivo de anulación del factor. Hace referencia a la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationReasonCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationReasonCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional que anuló/canceló el registro. FK a INPROFSAL. Auditoría (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que anula el registro referencia  a la tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio a estado Inactivo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la ultima vez que el profesional inactiva el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del último profesional que inactivó el registro. FK a SEGusuaru (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultimo profesional que inactiva el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastInactivationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio a estado Activo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la ultima vez que el profesional activa el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del último profesional que activó el registro. FK a SEGusuaru (CHAR 20)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ultimo profesional que activa el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'LastActivationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 2=Inactivo, 3=Anulado. INT, controla visibilidad y procesamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Activo 
2 - Inactivo 
3 - Anulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de factor de riesgo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'DateCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'DateCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario/profesional que creó el registro. FK a SEGusuaru. Trazabilidad de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que lo creó', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación clínica, nota o comentario adicional sobre el riesgo evaluado (VARCHAR 500)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la intervención clínica realizada ante el factor de riesgo identificado (VARCHAR 600)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Intervention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intervención del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Intervention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Intervention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del factor de riesgo asociado. FK a tabla RiskFactor. Vincula el tipo o categoría de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IdRiskFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla RiskFactor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IdRiskFactor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IdRiskFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del paciente (cédula, documento de identidad). FK a INPACIENT. PII - Identificación_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del paciente  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (IDENTITY) del registro de factor de riesgo reportado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de factores de riesgo reportados para cada paciente, incluyendo las intervenciones aplicadas, observaciones clínicas, estado de seguimiento y trazabilidad de activaciones, inactivaciones y cancelaciones del factor de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportRiskFactors';
