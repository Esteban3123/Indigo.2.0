CREATE TABLE [dbo].[CHCAMASTA] (
    [CODINDTAB] INT       NOT NULL,
    [DESTABCTR] CHAR (15) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [IMGTABCTR] IMAGE     NOT NULL,
    CONSTRAINT [PK_CHCAMASTA] PRIMARY KEY CLUSTERED ([CODINDTAB] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imagen/plano de fondo del TabPage o área de interfaz; tipo IMAGE (blob binario); usado para personalizar la presentación visual del control de pestaña en la UI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'IMGTABCTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plano de Fondo del TabPage o Area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'IMGTABCTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'IMGTABCTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la Unidad Funcional (UF); clave foránea que vincula la configuración de tabpage a la unidad operativa (consulta, urgencia, internación, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención, Institución o Sede; identificador del establecimiento de salud donde se configura la interfaz del tabpage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o etiqueta del TabPage; nombre legible del control de pestaña mostrado al usuario en la interfaz (ej: ''''Pacientes'''', ''''Diagnósticos'''', ''''Procedimientos'''')', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'DESTABCTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Tab Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'DESTABCTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'DESTABCTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único del TabPage o pestaña; llave primaria que distingue cada control de interfaz por centro y unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODINDTAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tab Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODINDTAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODINDTAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena las imágenes o íconos asociados a las tablas de control del sistema, organizadas por centro de atención y unidad funcional. Sirve para personalizar visualmente los maestros o catálogos del ERP/EHR según el contexto institucional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA';
