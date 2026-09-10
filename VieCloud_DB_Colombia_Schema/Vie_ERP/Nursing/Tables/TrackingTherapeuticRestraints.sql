CREATE TABLE [Nursing].[TrackingTherapeuticRestraints] (
    [idTherapeuticRestraints] INT           NOT NULL,
    [RegistrationDate]        DATETIME      NOT NULL,
    [Questions]               VARCHAR (MAX) NOT NULL,
    [AmongAlterations]        INT           NOT NULL,
    [HealthcareProfessional]  VARCHAR (25)  NOT NULL,
    [Observation]             VARCHAR (300) NULL,
    [HealthUnit]              VARCHAR (10)  NOT NULL,
    [Id]                      INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TrackingTherapeuticRestraints] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo único (IDENTITY), clave primaria de auditoría del seguimiento de restricciones terapéuticas', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de salud, centro de atención o unidad funcional donde se registra la restricción terapéutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación clínica, comentario o nota adicional del profesional sobre la restricción terapéutica aplicada', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional sanitario, médico, enfermero o personal de salud que registra y autoriza la restricción terapéutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional sanitario', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthcareProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador numérico de alteraciones comportamentales o clínicas presentes durante la aplicación de restricción', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AmongAlterations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Entre alteraciones', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AmongAlterations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AmongAlterations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas de evaluación, cuestionario o protocolo de valoración clínica para la restricción terapéutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Questions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preguntas', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Questions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Questions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro, momento en que se documenta el seguimiento de la restricción terapéutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a tabla TherapeuticRestraints (FK), identificador de la restricción terapéutica relacionada', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la tabla TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de seguimiento de restricciones terapéuticas en enfermería: documenta cada evaluación periódica realizada a pacientes que se encuentran con medidas de contención física o sujeción, incluyendo las respuestas a las preguntas de monitoreo, las alteraciones observadas, el profesional responsable y la unidad de salud donde se aplica la restricción.', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints';
