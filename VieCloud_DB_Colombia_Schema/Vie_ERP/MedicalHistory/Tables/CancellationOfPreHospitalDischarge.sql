CREATE TABLE [MedicalHistory].[CancellationOfPreHospitalDischarge] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [IdHCHISPACA] INT           NOT NULL,
    [CODMOTANU]   CHAR (4)      NOT NULL,
    [Observation] VARCHAR (200) NULL,
    CONSTRAINT [PK_CancellationOfPreHospitalDischarge] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationOfPreHospitalDischarge_HCHISPACA] FOREIGN KEY ([IdHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_CancellationOfPreHospitalDischarge_HCMOANULB] FOREIGN KEY ([CODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas explicativas sobre la anulación de la pre-alta hospitalaria; texto libre de hasta 200 caracteres que documenta el motivo específico o contexto clínico-administrativo de la cancelación del egreso anticipado (varchar 200, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la observación de la anulación de la pre-alta hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación de la pre-alta hospitalaria; referencia a tabla HCMOANULB que clasifica las razones por las cuales se cancela un egreso anticipado o salida prehospitalaria (char 4, FK obligatoria).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el código del motivo de anulación de la tabla HCMOANULB  ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la historia clínica (HC) o atención hospitalaria del paciente; referencia a tabla HCHISPACA que vincula esta cancelación de pre-alta a un ingreso, episodio clínico o proceso asistencial específico (int, FK obligatoria).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'IdHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el Id de la historia clínica', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'IdHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'IdHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de cancelación o anulación del alta prehospitalaria de un paciente. Guarda el motivo y la observación cuando se revierte o cancela una orden de egreso antes de la hospitalización formal.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de cancelación de alta prehospitalaria.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'CancellationOfPreHospitalDischarge', @level2type = N'COLUMN', @level2name = N'Id';
