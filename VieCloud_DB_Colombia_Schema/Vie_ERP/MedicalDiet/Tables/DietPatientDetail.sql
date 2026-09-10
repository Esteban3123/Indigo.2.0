CREATE TABLE [MedicalDiet].[DietPatientDetail] (
    [Id]                   INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DietControlNursingId] INT      NOT NULL,
    [DietTypeCode]         CHAR (3) NOT NULL,
    CONSTRAINT [PK_DietPatientDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DietControlNursingId] FOREIGN KEY ([DietControlNursingId]) REFERENCES [MedicalDiet].[DietControlNursing] ([Id]),
    CONSTRAINT [FK_DietTypeCode] FOREIGN KEY ([DietTypeCode]) REFERENCES [dbo].[CHTIPDIET] ([CODTIPDIE])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de tipo de dieta (CHAR 3), clasificación de régimen alimenticio (liquida, blanda, normal, diabética, etc.). FK a CHTIPDIET. Búsqueda: tipo dieta, régimen, clasificación alimenticia.', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de tipo de dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de control de dieta por enfermería (INT). FK a DietControlNursing. Vincula el detalle de dieta al control y monitoreo realizado por el personal de enfermería. Búsqueda: control enfermería, seguimiento dieta, monitoreo nutricional.', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietControlNursingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Dieta Control Enfermería Id', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietControlNursingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'DietControlNursingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY). Clave primaria secuencial de la tabla DietPatientDetail. Búsqueda: consecutivo, registro dieta, detalle dieta.', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle del tipo de dieta asignada a un paciente dentro de un control de enfermería. Registra qué dieta específica (por código) está vinculada a cada registro de control dietario del paciente durante su hospitalización o atención.', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietPatientDetail';
