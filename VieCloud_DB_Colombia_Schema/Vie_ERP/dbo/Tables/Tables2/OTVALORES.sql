CREATE TABLE [dbo].[OTVALORES] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA]  INT           NOT NULL,
    [IDGRUPO]      INT           NOT NULL,
    [IDOTVARIABLE] INT           NOT NULL,
    [VALOR]        VARCHAR (MAX) NOT NULL,
    [IDITEMLISTA]  INT           NULL,
    CONSTRAINT [PK_OTVALORES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVALORES_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVALORES] FOREIGN KEY ([ID]) REFERENCES [dbo].[OTVALORES] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVARIABLES] FOREIGN KEY ([IDOTVARIABLE]) REFERENCES [dbo].[OTVARIABLES] ([ID]),
    CONSTRAINT [FK_OTVALORES_OTVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[OTVARIABLESL] ([ID])
);




GO



GO



GO





GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_OTVALORES_IDHCHISPACA_IDOTVARIABLE_IDITEMLISTA_VALOR]
    ON [dbo].[OTVALORES]([IDHCHISPACA] ASC, [IDOTVARIABLE] ASC)
    INCLUDE([IDITEMLISTA], [VALOR]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del elemento de lista desplegable (FK a OTVARIABLESL) cuando la variable es de tipo combo/selección; permite registrar la opción elegida de un catálogo predefinido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la lista en caso de que la variable sea de tipo combo.  se relaciona con la tabla OTVARIABLESL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido o dato capturado de la variable (VARCHAR MAX); almacena valores alfanuméricos, numéricos, textos largos o estructurados según la variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el valor ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable clínica/administrativa almacenada (FK a OTVARIABLES); referencia la definición y tipo de dato de la variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la variable que se esta almacenando ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDOTVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo funcional (FK a OTGRUPO) que agrupa variables relacionadas en la orden o evaluación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id del grupo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con la tabla HCHISPACA (historia clínica del paciente/atención); vincula el valor a un registro específico de historia o tablero clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la relacion con el tablero de la Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) que registra cada valor almacenado en la tabla OTVALORES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena los valores registrados en variables clínicas u opciones de seguimiento (OT) dentro de una historia clínica de hospitalización o atención. Cada fila representa el valor capturado para una variable específica de un grupo de un formulario clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORES';
