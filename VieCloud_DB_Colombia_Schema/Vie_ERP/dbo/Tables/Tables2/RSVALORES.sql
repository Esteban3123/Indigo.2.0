CREATE TABLE [dbo].[RSVALORES] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]  INT            NOT NULL,
    [IDGRUPO]      INT            NOT NULL,
    [IDRSVARIABLE] INT            NOT NULL,
    [VALOR]        VARCHAR (5000) NOT NULL,
    [IDITEMLISTA]  INT            NULL,
    CONSTRAINT [PK_RSVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RSVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[RSGRUPO] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[RSVALORES] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVARIABLES] FOREIGN KEY ([IDRSVARIABLE]) REFERENCES [dbo].[RSVARIABLES] ([ID]),
    CONSTRAINT [FK_RSVALORES_RSVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[RSVARIABLESL] ([ID])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) opcional que referencia la opción seleccionada en tabla RSVARIABLESL cuando la variable es tipo combo, dropdown o lista desplegable; permite trazabilidad de respuestas predefinidas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla RSVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido o respuesta guardada (VARCHAR 5000) de la variable; puede ser texto libre, número, fecha, descripción clínica, hallazgo, síntoma, resultado de examen o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda el valor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia la definición de la variable (tipo, nombre, formato) en tabla RSVARIABLES; indica qué característica clínica, parámetro o dato se está almacenando', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la variable que se esta almacenando   Tabla de relacion RSVARIABLES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDRSVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona el valor con su grupo o categoría de variables en la tabla RSGRUPO; organiza variables por secciones de historias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla RSGRUPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula el valor con el registro de historia clínica o episodio de atención en la tabla HCHISPACA; identifica la consulta, ingreso o atención asociada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la relacion con el tablero de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada registro de valor almacenado en la tabla RSVALORES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda los valores registrados en las variables de las escalas de valoración clínica (como escalas de dolor, riesgo, funcionalidad) aplicadas a un paciente durante su historia clínica. Cada fila representa la respuesta o puntaje anotado para una variable específica dentro de un grupo de una escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RSVALORES';
