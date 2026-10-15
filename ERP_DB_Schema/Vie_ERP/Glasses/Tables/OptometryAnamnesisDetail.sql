CREATE TABLE [Glasses].[OptometryAnamnesisDetail] (
    [Id]                             INT        IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT        NOT NULL,
    [Code]                           CHAR (10)  NOT NULL,
    [OtherDescription]               CHAR (200) NULL,
    CONSTRAINT [PK_AnamnesisClinicalEvaluationOptometry_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnamnesisClinicalEvaluationOptometry_ClinicalEvaluationOptometry_1] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción de otro síntoma. En caso que la opción Otros sea seleccionada, se debe diligenciar este campo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'OtherDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del síntoma seleccionado de la anamnesis oftalmológica:  1. Ardor  2. Astenopia  3. Cefalea  4. Neuralgia ocular  5. Fotofobia  6. Lagrimeo  7. Diplopia  8. Hiperemia  9. Secreciones  10. Prurito  11. Miodesopsias  12. Confusión al leer  13. Mareo  14. Otros', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla ClinicalEvaluationOptometry', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryAnamnesisDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';

