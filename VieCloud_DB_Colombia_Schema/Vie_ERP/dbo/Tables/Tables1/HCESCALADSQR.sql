CREATE TABLE [dbo].[HCESCALADSQR] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCESCALAS] INT NOT NULL,
    [RESULTADO1]  INT NOT NULL,
    [RESULTADO2]  INT NOT NULL,
    [RESULTADO3]  INT NOT NULL,
    [RESULTADO4]  INT NOT NULL,
    CONSTRAINT [PK_HCESCALADSQR] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALADSQR_HCESCALAS] FOREIGN KEY ([IDHCESCALAS]) REFERENCES [dbo].[HCESCALAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación ítems 26-30 de escala psiquiátrica; respuesta positiva indica consumo problemático de alcohol; screening de dependencia/abuso alcohólico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ítem 26 al 30: Una respuesta positiva indica presencia de  consumo problemático de alcohol.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación ítem 25 de escala psiquiátrica; respuesta positiva indica antecedente o presencia de trastorno convulsivo/epilepsia; historia de convulsiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ítem 25: Respuesta positiva en este ítem indica caso de trastorno convulsivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación ítems 21-24 de escala psiquiátrica; respuesta positiva indica presencia de síntomas psicóticos; evaluación de psicosis/desorganización del pensamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ítem 21 al 24:  Una respuesta positiva indica presencia de trastorno psicótico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Puntuación ítems 1-20 de escala psiquiátrica; ≥11 puntos indica alta probabilidad de trastorno depresivo o ansioso; screening inicial de depresión/ansiedad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ítem 1 al 20:  = 11 puntos - Alta probabilidad de sufrir un trastorno depresivo o ansioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'RESULTADO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula a tabla HCESCALAS; referencia al registro padre de la escala de evaluación clínica de salud mental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de cada registro de detalle en la escala de salud mental; clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de resultados de escalas clínicas aplicadas en historia clínica. Guarda hasta cuatro valores o puntajes parciales obtenidos en la evaluación de una escala (por ejemplo, escala de dolor, Glasgow, Braden u otras escalas de valoración clínica).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALADSQR';
