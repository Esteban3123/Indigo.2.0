CREATE TABLE [Glasses].[OptometryAnamnesisDetail] (
    [Id]                             INT        IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT        NOT NULL,
    [Code]                           CHAR (10)  NOT NULL,
    [OtherDescription]               CHAR (200) NULL,
    CONSTRAINT [PK_AnamnesisClinicalEvaluationOptometry_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnamnesisClinicalEvaluationOptometry_ClinicalEvaluationOptometry_1] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción ampliada del síntoma oftalmológico cuando se selecciona opción ''''Otros''''. Campo de texto libre (CHAR 200) para registrar síntomas adicionales no incluidos en lista estándar de anamnesis óptica; obligatorio si se marca categoría ''''Otros''''.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'OtherDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción de otro síntoma. En caso que la opción Otros sea seleccionada, se debe diligenciar este campo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'OtherDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'OtherDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de síntoma oftalmológico seleccionado en anamnesis óptica (CHAR 10): ardor ocular, astenopia (fatiga visual), cefalea, neuralgia ocular, fotofobia, lagrimeo, diplopia (visión doble), hiperemia (ojo rojo), secreciones oculares, prurito (picazón), miodesopsias (moscas volantes), confusión al leer, mareo, u otros síntomas no listados. Clave para evaluación clínica optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del síntoma seleccionado de la anamnesis oftalmológica:  1. Ardor  2. Astenopia  3. Cefalea  4. Neuralgia ocular  5. Fotofobia  6. Lagrimeo  7. Diplopia  8. Hiperemia  9. Secreciones  10. Prurito  11. Miodesopsias  12. Confusión al leer  13. Mareo  14. Otros', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT FK) de la evaluación clínica oftalmológica asociada. Referencia a OptometryClinicalEvaluationC; establece relación entre síntomas anamnésicos y evaluación clínica optométrica del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los antecedentes y preguntas de la anamnesis optométrica registradas en cada evaluación clínica de optometría. Cada fila corresponde a un ítem o hallazgo específico dentro del formulario de anamnesis del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de detalle de anamnesis optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Id';
