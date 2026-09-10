CREATE TABLE [dbo].[HCESCALASRESULTITEMS] (
    [ID]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCESCALAS]           INT           NOT NULL,
    [TAGPREGUNTA]           VARCHAR (4)   NOT NULL,
    [TEXTOPREGUNTA]         VARCHAR (MAX) NOT NULL,
    [TEXTOITEMSELECCIONADO] VARCHAR (MAX) NOT NULL,
    [VALORITEMSELECCIONADO] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_HCESCALASRESULTITEMS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALASRESULTITEMS_HCESCALAS] FOREIGN KEY ([IDHCESCALAS]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCALASRESULTITEMS] NOCHECK CONSTRAINT [FK_HCESCALASRESULTITEMS_HCESCALAS];


GO
CREATE NONCLUSTERED INDEX [IX_HCESCALASRESULTITEMS_IDHCESCALAS]
    ON [dbo].[HCESCALASRESULTITEMS]([IDHCESCALAS] ASC)
    INCLUDE([TEXTOITEMSELECCIONADO], [TEXTOPREGUNTA], [VALORITEMSELECCIONADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor asignado a la opción seleccionada (VARCHAR 200); puede ser numérico (1-10), código alfanumérico o identificador que representa la puntuación o categoría de la respuesta en la escala clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'VALORITEMSELECCIONADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el valor de los items seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'VALORITEMSELECCIONADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'VALORITEMSELECCIONADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción legible de la opción/respuesta que el usuario seleccionó (ej: ''''Leve'''', ''''Ausente'''', ''''Grave''''); alternativa textual del valor numérico o código de respuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOITEMSELECCIONADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda lo selecionado item', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOITEMSELECCIONADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOITEMSELECCIONADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enunciado completo de la pregunta clínica presentada al paciente o evaluador (VARCHAR MAX); texto que define qué se pregunta en la escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  texto de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código etiqueta corto (VARCHAR 4) que identifica la pregunta dentro de la escala; usado para referencia interna y mapeo de preguntas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TAGPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tag de la pregunta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TAGPREGUNTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TAGPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia tabla HCESCALAS (ID); vincula la respuesta del ítem a la escala clínica madre (ej: escala de dolor, depresión, riesgo de caída).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Relación con la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT identity), consecutivo de fila de respuesta de escala en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de los ítems respondidos en cada aplicación de una escala clínica (como escala de dolor, Glasgow, Braden, entre otras). Guarda el detalle de cada pregunta evaluada, la opción seleccionada por el profesional y su valor numérico o categórico correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS';
