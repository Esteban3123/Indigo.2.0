CREATE TABLE [Glasses].[OptometryVisualAcuityCorrectionDetail] (
    [Id]                             INT          IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC] INT          NOT NULL,
    [Eye]                            INT          NOT NULL,
    [Sphere]                         VARCHAR (10) NOT NULL,
    [Cylinder]                       VARCHAR (10) NOT NULL,
    [Axis]                           VARCHAR (10) NOT NULL,
    [VisualAcuity]                   INT          NOT NULL,
    [Adition]                        VARCHAR (20) NULL,
    [Prism]                          VARCHAR (10) NOT NULL,
    [ProximalVision]                 INT          NOT NULL,
    CONSTRAINT [PK_OptometryVisualAcuityCorrectionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryVisualAcuityCorrectionDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la corrección visual (fórmula óptica) registrada durante una evaluación clínica optométrica. Guarda los parámetros de la lente correctora prescrita para cada ojo del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de corrección visual.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la evaluación clínica optométrica a la que pertenece este detalle de corrección; vincula con el encabezado de la consulta optométrica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo evaluado: identifica si los valores corresponden al ojo derecho (OD) o ojo izquierdo (OI).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor esférico de la lente correctora prescrita; corrige miopía o hipermetropía. Se expresa en dioptrías (ej: -1.50, +2.00).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Sphere';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cilíndrico de la lente prescrita; corrige el astigmatismo. Se expresa en dioptrías (ej: -0.75).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Cylinder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eje del cilindro en grados (0°–180°); indica la orientación del astigmatismo en la fórmula óptica.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Axis';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual obtenida con la corrección aplicada; representa la capacidad de visión del paciente con la lente prescrita (ej: 20/20).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'VisualAcuity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Adición o ADD: potencia adicional para visión de cerca prescrita en lentes bifocales o progresivos; aplica principalmente a pacientes con presbicia.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Adition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor prismático de la lente correctora; corrige problemas de alineación ocular o binocularidad (estrabismo, forias). Se expresa en dioptrías prismáticas.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Prism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'Prism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual proximal o de cerca obtenida con la corrección; evalúa la capacidad de lectura y visión de objetos próximos con la lente prescrita.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'ProximalVision';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityCorrectionDetail', @level2type = N'COLUMN', @level2name = N'ProximalVision';
