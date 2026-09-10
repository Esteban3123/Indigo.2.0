CREATE TABLE [Glasses].[OptometryRefractionKeratometryDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [KeratometryOne]                 VARCHAR (10) NOT NULL,
    [KeratometryTwo]                 VARCHAR (10) NOT NULL,
    [KeratometryThree]               VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_OptometryRefractionKeratometryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionKeratometryDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera medición de queratometría concatenada (VARCHAR 10), validación/promedio de curvatura corneal; dato clínico para prescripción lentes y cirugía refractiva.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryThree';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryThree';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryThree';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda medición de queratometría concatenada (VARCHAR 10), valor de curvatura corneal en eje perpendicular; complementa diagnóstico refractivo y astigmatismo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera medición de queratometría concatenada (VARCHAR 10), valor de curvatura corneal en eje principal; búsqueda: medición corneal, refracción ocular, graduación lente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de los campos de Queratometría concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'KeratometryOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ojo examinado: 1=Ojo Derecho (OD), 2=Ojo Izquierdo (OI); código binario para lateralidad en refracción y queratometría.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Derecho 2. Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el Id de cabecera/evaluación clínica optométrica (tabla OptometryClinicalEvaluationC), agrupa mediciones por paciente/atención.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo/secuencia autoincremental de detalle de queratometría en evaluación optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de queratometría obtenida en la evaluación de refracción optométrica. Registra las tres mediciones de curvatura corneal (K1, K2, K3) por ojo del paciente, como parte del examen visual clínico.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionKeratometryDetail';
