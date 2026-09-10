CREATE TABLE [Glasses].[OptometryRefractionAutokeratometryDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [IdOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [AutokeratometryOne]             VARCHAR (10) NOT NULL,
    [AutokeratometryTwo]             VARCHAR (10) NOT NULL,
    [AutokeratometryThree]           VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_OptometryRefractionAutokeratometryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryRefractionAutokeratometryDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([IdOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer valor de autoqueratometría concatenado (VARCHAR 10); medición de curvatura corneal mediante autoqueratómetro en evaluación optométrica de refracción', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de los campos de Autoqueratometria concatenados', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryOne';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryOne';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ojo evaluado: 1=Ojo derecho (OD), 2=Ojo izquierdo (OS); INT para lateralidad en examen oftalmológico', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1. Derecho 2. Izquierdo', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Eye';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia OptometryClinicalEvaluationC; identificador único de la evaluación clínica optométrica de cabecera a la que pertenece este detalle de autoqueratometría', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de cabecera de valoracion (TABLA OptometryClinicalEvaluationC)', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'IdOptometryClinicalEvaluationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY); consecutivo/número secuencial generado automáticamente para cada registro de detalle de autoqueratometría', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las mediciones de autoqueratometría obtenidas durante la evaluación clínica optométrica, registrando los valores de curvatura corneal por ojo para determinar el astigmatismo y la forma de la córnea del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda lectura de autoqueratometría corneal del ojo evaluado, usada para verificar la curvatura de la córnea.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera lectura de autoqueratometría corneal del ojo evaluado, utilizada para confirmar y promediar las mediciones de curvatura corneal.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryThree';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryRefractionAutokeratometryDetail', @level2type = N'COLUMN', @level2name = N'AutokeratometryThree';
