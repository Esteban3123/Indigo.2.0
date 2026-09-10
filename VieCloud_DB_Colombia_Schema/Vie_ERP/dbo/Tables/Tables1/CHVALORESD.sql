CREATE TABLE [dbo].[CHVALORESD] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [IDCHVALORESC] INT            NOT NULL,
    [IDGRUPO]      INT            NOT NULL,
    [IDCHVARIABLE] INT            NOT NULL,
    [VALOR]        VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]  INT            NULL,
    [LOCATIONY]    INT            NULL,
    CONSTRAINT [PK_CHVALORESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHVALORESD_CHGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[CHGRUPO] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVALORESC] FOREIGN KEY ([IDCHVALORESC]) REFERENCES [dbo].[CHVALORESC] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVARIABLES] FOREIGN KEY ([IDCHVARIABLE]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_CHVALORESD_CHVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[CHVARIABLESL] ([ID])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Posición vertical (INT, nullable). Coordenada de ubicación del elemento en la interfaz de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'LOCATIONY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda locación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'LOCATIONY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'LOCATIONY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de opción combo (FK → CHVARIABLESL, nullable). Referencia cuando la variable es tipo desplegable/lista de diagnósticos, códigos RIPS, procedimientos o valores predefinidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla CHVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de la variable (VARCHAR 5000). Almacena la respuesta, texto, valor clínico, diagnóstico, procedimiento, examen o hallazgo registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el valor de la variable dependencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de variable clínica (FK → CHVARIABLES). Especifica qué campo/pregunta se está respondiendo en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la variable que se esta almacenando   Tabla de relacion CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo (FK → CHGRUPO). Agrupa variables relacionadas de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del grupo que se esta almacenando   Tabla de relacion CHVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cabecera (FK → CHVALORESC). Vincula el valor detallado a su registro maestro de paciente/atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVALORESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera que guarda la informacion de la lista y paciente  Tabla de relacion CHVALORESCC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVALORESC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'IDCHVALORESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, autoincrementado). Clave primaria de la tabla de valores detallados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de valores registrados en encuestas o formularios de historia clínica estructurada. Cada fila corresponde a la respuesta concreta de una variable clínica dentro de un grupo de preguntas, asociada a un cuestionario o chequeo específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHVALORESD';
