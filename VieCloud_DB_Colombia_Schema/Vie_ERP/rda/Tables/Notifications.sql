CREATE TABLE [rda].[Notifications] (
    [Id]                                    INT          IDENTITY (1, 1) NOT NULL,
    [PatientCode]                           VARCHAR (20) NOT NULL,
    [IngressNumber]                         CHAR (10)    NOT NULL,
    [RDAPatientSent]                        BIT          CONSTRAINT [DF_Notifications_RDAPatientSent] DEFAULT ((0)) NOT NULL,
    [RDAPatientSubmissionDate]              DATETIME     NULL,
    [RDAEmergencySent]                      BIT          CONSTRAINT [DF_Notifications_RDAEmergencySent] DEFAULT ((0)) NOT NULL,
    [RDAEmergencySubmissionDate]            DATETIME     NULL,
    [RDAHospitalizationSent]                BIT          CONSTRAINT [DF_Notifications_RDAHospitalizationSent] DEFAULT ((0)) NOT NULL,
    [RDAHospitalizationSubmissionDate]      DATETIME     NULL,
    [RDAExternalconsultationSent]           BIT          CONSTRAINT [DF_Notifications_RDAExternalconsultationSent] DEFAULT ((0)) NOT NULL,
    [RDAExternalconsultationSubmissionDate] DATETIME     NULL,
    [CreationDate]                          DATETIME     CONSTRAINT [DF_Notifications_CreationDate] DEFAULT ([common].[GETDATE]()) NOT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del registro de notificación en el sistema, independiente de los envíos individuales.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta (DATETIME) del envío/notificación del RDA de consulta externa. NULL si no se ha enviado aún.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio del RDA consulta externa', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSubmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el RDA de consulta externa ya fue enviado/notificado para esta combinación paciente-ingreso.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si el RDA consulta externa ya fue enviado para el paciente e ingreso', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAExternalconsultationSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta (DATETIME) del envío/notificación del RDA de hospitalización. NULL si no se ha enviado aún.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio del RDA hospitalizacion', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSubmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el RDA de hospitalización ya fue enviado/notificado para esta combinación paciente-ingreso.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si el RDA hospitalizacion ya fue enviado para el paciente e ingreso', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAHospitalizationSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta (DATETIME) del envío/notificación del RDA de urgencia. NULL si no se ha enviado aún.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio del RDA urgencias', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySubmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el RDA de urgencia/emergencia ya fue enviado/notificado para esta combinación paciente-ingreso.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si el RDA urgencia ya fue enviado para el paciente e ingreso', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAEmergencySent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta (DATETIME) del envío/notificación del RDA de consulta de paciente. NULL si no se ha enviado aún.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de envio del RDA paciente', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSubmissionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSubmissionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el RDA de consulta/atención de paciente ambulatorio ya fue enviado/notificado para esta combinación paciente-ingreso.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda si el RDA paciente ya fue enviado para el paciente e ingreso', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'RDAPatientSent';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, identificador de la atención/episodio clínico específico asociado al RDA enviado. CHAR(10).', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'IngressNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'IngressNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente, equivalente a cédula, documento de identidad o identificación (PII - Identification_Ofuscado). VARCHAR(20), clave para búsqueda de atenciones.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de notificaciones y envíos de RDA (Remisión de Datos de Atención) ya procesados para pacientes en diferentes tipos de atención: consulta paciente, urgencia, hospitalización y consulta externa.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda los RDA que ya fueron enviados', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de notificación RDA.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'Notifications', @level2type = N'COLUMN', @level2name = N'Id';
