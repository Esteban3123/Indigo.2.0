CREATE TABLE [dbo].[HCESCABRDES] (
    [ID]         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESCALAID] INT NOT NULL,
    [RESULTADOA] INT NULL,
    [RESULTADOB] INT NULL,
    [RESULTADOC] INT NULL,
    [RESULTADOD] INT NULL,
    CONSTRAINT [PK_HCESCADETALLE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCADETALLE_HCESCALAS] FOREIGN KEY ([HCESCALAID]) REFERENCES [dbo].[HCESCALAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado o puntuación D de la escala evaluada. Valor numérico (INT) que almacena la respuesta o puntaje parcial del componente D de la escala clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado D', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado o puntuación C de la escala evaluada. Valor numérico (INT) que almacena la respuesta o puntaje parcial del componente C de la escala clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resulatdo C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado o puntuación B de la escala evaluada. Valor numérico (INT) que almacena la respuesta o puntaje parcial del componente B de la escala clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resulatdo B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado o puntuación A de la escala evaluada. Valor numérico (INT) que almacena la respuesta o puntaje parcial del componente A de la escala clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el resultado A', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'RESULTADOA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la escala clínica asociada (clave foránea FK a HCESCALAS.ID). Referencia a la escala de evaluación, valoración o medición utilizada en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de Escalas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'HCESCALAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'HCESCALAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la fila de detalle de escala. Clave primaria de la tabla HCESCABRDES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de resultados de una escala clínica de historia clínica. Registra los valores o puntajes obtenidos en cada opción o sección (A, B, C, D) de una escala de valoración médica, vinculada a su escala cabecera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCABRDES';
