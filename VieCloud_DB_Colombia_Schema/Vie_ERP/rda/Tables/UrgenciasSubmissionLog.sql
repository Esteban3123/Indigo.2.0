CREATE TABLE [rda].[UrgenciasSubmissionLog] (
    [SubmissionId]   BIGINT        IDENTITY (1, 1) NOT NULL,
    [LocalPatientId] VARCHAR (20)  NOT NULL,
    [DocumentType]   VARCHAR (5)   NOT NULL,
    [DocumentNumber] VARCHAR (20)  NOT NULL,
    [SubmissionDate] DATETIME2 (7) NULL,
    [DataHash]       VARCHAR (64)  NULL,
    [IdHispaca]      VARCHAR (20)  NULL,
    [RdaBody]        VARCHAR (MAX) NULL,
    [Send]           BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([SubmissionId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de envíos de urgencias al sistema RDA (Registro de Atención de Urgencias). Guarda el historial de cada transmisión de datos de pacientes atendidos en urgencias, incluyendo el contenido enviado y su estado de envío.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de cada envío o transmisión registrada.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador local del paciente en el sistema, código interno del paciente atendido en urgencias.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'LocalPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'LocalPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del paciente (cédula, tarjeta de identidad, pasaporte, etc.).', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento de identidad del paciente, cédula o identificación.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el envío o transmisión del registro de urgencias al sistema RDA.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella digital (hash) del contenido enviado, usada para verificar integridad y detectar duplicados o cambios en los datos.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DataHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'DataHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro en el sistema HISPACA, código de referencia externo asignado por dicho sistema al registro de urgencias.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'IdHispaca';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'IdHispaca';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del mensaje o cuerpo del registro enviado al sistema RDA, en formato texto (generalmente XML o JSON).', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'RdaBody';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'RdaBody';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el registro fue enviado exitosamente al sistema RDA: 1 = enviado, 0 = pendiente o no enviado.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'Send';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'UrgenciasSubmissionLog', @level2type = N'COLUMN', @level2name = N'Send';
