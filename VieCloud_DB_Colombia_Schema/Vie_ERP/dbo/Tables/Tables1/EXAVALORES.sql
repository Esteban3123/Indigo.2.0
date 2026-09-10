CREATE TABLE [dbo].[EXAVALORES] (
    [ID]            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]   INT            NOT NULL,
    [IDEXAGRUPO]    INT            NOT NULL,
    [IDEXAVARIABLE] INT            NOT NULL,
    [VALOR]         VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]   INT            NULL,
    [VALOROPCION]   TINYINT        NULL,
    CONSTRAINT [PK_EXAVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVALORES_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[EXAVALORES] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVARIABLES] FOREIGN KEY ([IDEXAVARIABLE]) REFERENCES [dbo].[EXAVARIABLES] ([ID]),
    CONSTRAINT [FK_EXAVALORES_EXAVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[EXAVARIABLESL] ([ID]),
    CONSTRAINT [FK_EXAVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de interpretación clínica del valor: 1=Normal, 2=Anormal, 3=No valorado/No aplica (TINYINT, interpretación de hallazgo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1= Normal      2= Anormal     3=No valorado  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOROPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador opcional del elemento en lista de opciones predefinidas tabla EXAVARIABLESL (FK); para valores de lista desplegable o catálogo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del item ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor capturado o resultado del examen/control: texto, número, fecha o expresión según tipo variable (VARCHAR 5000); ej: resultado laboratorio, hallazgo imagen, respuesta cuestionario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del control o valor del control IDEXAVARIABLE  ejemplo:-- checkedit.editvalue', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable/parámetro de examen en tabla EXAVARIABLES (FK); define tipo de control UI (CheckEdit, MemoEdit, SpinEdit, GridLookUpEdit, DateEdit) y etiqueta de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el tag del control   CheckEdit    MemoEdit    SpinEdit    GridLookUpEdit    DateEdit', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de examen/prueba en tabla EXAGRUPO (FK); agrupa variables relacionadas de laboratorio, imagen o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de historia clínica del paciente en tabla HCHISPACA (FK); vincula valor del examen a atención/consulta específica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del registro que guarda en la tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incrementable de la fila en tabla EXAVALORES (clave primaria, INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados o valores registrados para cada variable de un examen o evaluación clínica del paciente. Almacena las respuestas, mediciones o hallazgos obtenidos durante la aplicación de instrumentos de valoración en la historia clínica, como escalas, pruebas diagnósticas o formularios de evaluación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORES';
