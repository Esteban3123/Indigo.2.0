CREATE TABLE [dbo].[IndicatorValue] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IndicatorId]   INT             NOT NULL,
    [IndicatorDate] DATE            NOT NULL,
    [Numerator]     NUMERIC (18, 2) NOT NULL,
    [Denominator]   NUMERIC (18, 2) NOT NULL,
    [Result]        NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_IndicatorValue__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IndicatorValue_Indicator] FOREIGN KEY ([IndicatorId]) REFERENCES [dbo].[Indicator] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_IndicatorValue__IndicatorId__IndicatorDate]
    ON [dbo].[IndicatorValue]([IndicatorId] ASC, [IndicatorDate] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del indicador; cociente o valor final (Numerador/Denominador); tipo NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Result';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Result';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominador del indicador; base de cálculo o población total; tipo NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Denominator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Denominador ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Denominator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Denominator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numerador del indicador; valor parcial del cálculo; tipo NUMERIC(18,2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Numerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numerador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Numerator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Numerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro o cálculo del indicador; período de medición; tipo DATE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del indicador relacionado; referencia a tabla Indicator (FK); tipo INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'IndicatorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del valor del indicador; clave primaria (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del valor del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valores históricos de indicadores de gestión o calidad: para cada indicador y fecha registra el numerador, denominador y resultado calculado. Permite hacer seguimiento y reportería de indicadores asistenciales, administrativos o de calidad en el tiempo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IndicatorValue';
