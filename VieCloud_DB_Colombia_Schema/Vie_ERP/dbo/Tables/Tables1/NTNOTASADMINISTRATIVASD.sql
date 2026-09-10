CREATE TABLE [dbo].[NTNOTASADMINISTRATIVASD] (
    [ID]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTNOTASADMINISTRATIVASC] INT           NOT NULL,
    [IDGRUPO]                   INT           NOT NULL,
    [IDNTVARIABLE]              INT           NOT NULL,
    [VALOR]                     VARCHAR (MAX) NOT NULL,
    [IDITEMLISTA]               INT           NULL,
    CONSTRAINT [PK_NTVALORESREGISTRADOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTVALORESREGISTRADOS_NTGRUPOS] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[NTGRUPOS] ([ID]),
    CONSTRAINT [FK_NTVALORESREGISTRADOS_NTVALORESCABECERA] FOREIGN KEY ([IDNTNOTASADMINISTRATIVASC]) REFERENCES [dbo].[NTNOTASADMINISTRATIVASC] ([ID]),
    CONSTRAINT [FK_NTVALORESREGISTRADOS_NTVARIABLES] FOREIGN KEY ([IDNTVARIABLE]) REFERENCES [dbo].[NTVARIABLES] ([ID]),
    CONSTRAINT [FK_NTVALORESREGISTRADOS_NTVARIABLESL] FOREIGN KEY ([IDITEMLISTA]) REFERENCES [dbo].[NTVARIABLESL] ([ID])
);


GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASD] NOCHECK CONSTRAINT [FK_NTVALORESREGISTRADOS_NTVALORESCABECERA];




GO



GO
ALTER TABLE [dbo].[NTNOTASADMINISTRATIVASD] NOCHECK CONSTRAINT [FK_NTVALORESREGISTRADOS_NTVALORESCABECERA];


GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_NTNOTASADMINISTRATIVASD_IDNTNOTASADMINISTRATIVASC_IDNTVARIABLE]
    ON [dbo].[NTNOTASADMINISTRATIVASD]([IDNTNOTASADMINISTRATIVASC] ASC, [IDNTVARIABLE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del elemento seleccionado en variables de tipo lista/catálogo. FK a NTVARIABLESL. Nulo si la variable no es una lista desplegable. Almacena la opción elegida por el usuario en formularios administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si la variable es una lista guarda aqui el ID del Item seleccioado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDITEMLISTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido o dato diligenciado por el usuario en la variable administrativa. Tipo VARCHAR(MAX). Captura texto libre, números, fechas u otros valores según la definición de la variable. Campo principal de datos en notas administrativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor diligenciado por el usuario ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la variable administrativa asociada al valor registrado. FK a NTVARIABLES. Define la estructura, tipo y validaciones del campo completado. Vincula cada registro con su plantilla de variable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTVARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo/sección temático de la nota administrativa. FK a NTGRUPOS. Organiza variables en secciones lógicas (ej: datos paciente, datos administrativos, contrato). Facilita navegación y agrupamiento de campos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de Relación con el grupo de la nota administrativa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nota administrativa cabecera/padre. FK a NTNOTASADMINISTRATIVASC. Relaciona cada valor con su documento administrativo principal (contrato, autorización, glosa, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTNOTASADMINISTRATIVASC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo que relaciona con la tabla  (NTNOTASADMINISTRATIVASC) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTNOTASADMINISTRATIVASC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'IDNTNOTASADMINISTRATIVASC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY, INT). Clave primaria. Consecutivo incremental de registro de valor en notas administrativas. Permite rastrear cada dato ingresado en el formulario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las notas administrativas, donde cada fila representa el valor de una variable o campo dentro de una nota administrativa registrada. Almacena las respuestas o contenidos ingresados por el usuario para cada variable del formulario de notas administrativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTNOTASADMINISTRATIVASD';
