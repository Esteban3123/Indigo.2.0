CREATE TABLE [rda].[HospitalizacionSubmissionLog] (
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de envíos de información de hospitalización al sistema RDA (Registro de Datos Administrativos). Guarda el historial de cada transmisión realizada, incluyendo el contenido enviado, su estado de envío y el identificador en el sistema receptor.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de cada envío o transmisión registrada.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador local del paciente en el sistema Indigo, usado para vincular el envío con el paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'LocalPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'LocalPatientId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad del paciente (ej: CC, TI, CE, PA).', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento de identidad del paciente, cédula, identificación o documento.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DocumentNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el envío de la información al sistema RDA.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'SubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella digital o hash del contenido enviado, usado para detectar cambios o duplicados en la información transmitida.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DataHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'DataHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador asignado por el sistema HISPACA o sistema receptor externo al registro enviado.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'IdHispaca';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'IdHispaca';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del mensaje o payload enviado al sistema RDA, con los datos de hospitalización en formato estructurado.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'RdaBody';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'RdaBody';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el registro fue efectivamente enviado al sistema externo: 1 = enviado, 0 = pendiente de envío.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'Send';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'HospitalizacionSubmissionLog', @level2type = N'COLUMN', @level2name = N'Send';
