CREATE TABLE [dbo].[HCTIPENCD] (
    [AUTO]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOHCTIPENCC] INT           NOT NULL,
    [DESPREENC]     VARCHAR (500) NOT NULL,
    [PREREQENC]     BIT           NOT NULL,
    [TIPPREENC]     CHAR (1)      NOT NULL,
    [VALMINNUM]     INT           NOT NULL,
    [VALMAXNUM]     INT           NOT NULL,
    [PRESELUNI]     XML           NULL,
    [OBSERVASI]     VARCHAR (MAX) NULL,
    [OBSERVANO]     VARCHAR (MAX) NULL,
    [OBSERVANI]     VARCHAR (MAX) NULL,
    [OBSERVALIS]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCTIPENCD] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de recomendación o nota contextual mostrada cuando se selecciona un valor de lista (TIPPREENC=3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVALIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recomendaciones para cuando el tipo de respuesta es lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVALIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVALIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de recomendación o nota contextual mostrada cuando la respuesta seleccionada es Ninguno (TIPPREENC=1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La respectivas recomendaciones cuando el tipo de respuesta es Ninguna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de recomendación o nota contextual mostrada cuando la respuesta seleccionada es No (TIPPREENC=1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La respectivas recomendaciones cuando el tipo de respuesta es No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de recomendación o nota contextual mostrada cuando la respuesta seleccionada es Sí (TIPPREENC=1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'La respectivas recomendaciones cuando el tipo de respuesta es si.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'OBSERVASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento XML que especifica los valores predefinidos para preguntas de selección única (TIPPREENC=3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PRESELUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la lista de valores para las preguntas de seleccion unica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PRESELUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PRESELUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo permitido para respuestas de tipo numérico (TIPPREENC=2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMAXNUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor maximo para las respuestas de tipo numerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMAXNUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMAXNUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo permitido para respuestas de tipo numérico (TIPPREENC=2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMINNUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor minimo para las respuestas de tipo numerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMINNUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'VALMINNUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de respuesta esperada: 1=Sí/No/Ninguno, 2=Numérica, 3=Lista selección única, 4=Texto multilinea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'TIPPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de respuesta de la pregunta    1. Tipo de respuesta Si - No -Ninguno  2. Tipo de respuesta Numerica  3. Tipo de respuesta Lista de seleccion  4. Tipo de respuesta Multilinea (Texto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'TIPPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'TIPPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=pregunta obligatoria, 0=pregunta opcional en la encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PREREQENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la pregunta es requerida en la encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PREREQENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'PREREQENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Texto de la pregunta o enunciado de la encuesta a presentar al usuario, máx 500 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'DESPREENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'DESPREENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) a la cabecera de encuesta (HCTIPENCC) que agrupa preguntas relacionadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTOHCTIPENCC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auto de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTOHCTIPENCC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTOHCTIPENCC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumerado (IDENTITY) de la fila de detalle de encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna autonumerica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas o ítems detallados que componen una encuesta o formulario de historia clínica. Cada registro representa una pregunta específica dentro de una categoría de encuesta clínica, con su tipo de respuesta, rango de valores válidos y textos de observación según el resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCTIPENCD';
