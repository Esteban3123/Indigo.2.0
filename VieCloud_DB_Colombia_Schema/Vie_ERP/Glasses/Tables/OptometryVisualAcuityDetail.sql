CREATE TABLE [Glasses].[OptometryVisualAcuityDetail] (
    [Id]                             INT IDENTITY (1, 1) NOT NULL,
    [idOptometryClinicalEvaluationC] INT NOT NULL,
    [Eye]                            INT NOT NULL,
    [FarAwayWithoutCorrection]       INT NOT NULL,
    [FarAwayWithCorrection]          INT NOT NULL,
    [Pinhole]                        INT NOT NULL,
    [ProximalWithoutCorrection]      INT NOT NULL,
    [ProximalWithCorrection]         INT NOT NULL,
    CONSTRAINT [PK_OptometryVisualAcuityDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OptometryVisualAcuityDetail_OptometryClinicalEvaluationC] FOREIGN KEY ([idOptometryClinicalEvaluationC]) REFERENCES [Glasses].[OptometryClinicalEvaluationC] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de agudeza visual registrado en la evaluación clínica optométrica. Almacena los resultados de visión lejana y próxima, con y sin corrección óptica, para cada ojo del paciente.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de agudeza visual.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la evaluación clínica optométrica a la que pertenece este detalle de agudeza visual.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'idOptometryClinicalEvaluationC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica el ojo evaluado (por ejemplo: ojo derecho, ojo izquierdo o ambos).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Eye';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de lejos sin corrección óptica (sin lentes ni gafas).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'FarAwayWithoutCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'FarAwayWithoutCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de lejos con corrección óptica (con lentes o gafas).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'FarAwayWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'FarAwayWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual medida con estenopeico (pinhole), usado para estimar el potencial visual sin defecto refractivo.', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'Pinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de cerca sin corrección óptica (sin lentes ni gafas).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'ProximalWithoutCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'ProximalWithoutCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agudeza visual de cerca con corrección óptica (con lentes o gafas).', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'ProximalWithCorrection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Glasses', @level1type = N'TABLE', @level1name = N'OptometryVisualAcuityDetail', @level2type = N'COLUMN', @level2name = N'ProximalWithCorrection';
