CREATE TABLE [dbo].[HCINFLIQDEXT] (
    [ID]                         INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCINFLIQD]                INT NOT NULL,
    [NUMEROAPLICACIONINICIAL]    INT NOT NULL,
    [NUMEROAPLICACIONREPOSICION] INT NOT NULL,
    [CANTIDADREPOSICION]         INT NOT NULL,
    CONSTRAINT [PK_HCINFLIQDEXT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de reposición de líquidos; volumen/dosis aplicada en cada reposición o suministro adicional durante atención clínica (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la cantidad de reposicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'CANTIDADREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de aplicación para reposición; identificador de cada evento de reposición de fluidos en la historia clínica (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de aplicacion a reponer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONREPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de aplicación inicial; identificador del primer evento o aplicación base de líquidos en el registro clínico (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de aplicacion inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'NUMEROAPLICACIONINICIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de registro de información de líquidos; clave foránea que vincula a tabla padre de control de líquidos en historia clínica (INT, FK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id informacion liquidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'IDHCINFLIQD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo de fila en tabla HCINFLIQDEXT; clave primaria auto-incremental para trazabilidad de reposiciones (INT, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de reposiciones de líquidos en infusión durante la atención clínica. Registra cada evento de reposición de líquidos administrados al paciente, vinculado al registro principal de infusión de líquidos, indicando el número de aplicación inicial, el número de aplicación de reposición y la cantidad repuesta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINFLIQDEXT';
