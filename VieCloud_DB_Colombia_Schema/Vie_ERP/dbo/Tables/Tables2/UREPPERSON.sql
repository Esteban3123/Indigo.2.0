CREATE TABLE [dbo].[UREPPERSON] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TITULO]      VARCHAR (200)   NULL,
    [DESCRIPCION] VARCHAR (2000)  NULL,
    [DEFINICION]  VARBINARY (MAX) NULL,
    [MODULO]      CHAR (2)        NULL,
    CONSTRAINT [PK_UREPPERSON] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERSON_SEGmodulu] FOREIGN KEY ([MODULO]) REFERENCES [dbo].[SEGmodulu] ([indmodulo]),
    CONSTRAINT [FK_UREPPERSON_UREPPERSON] FOREIGN KEY ([ID]) REFERENCES [dbo].[UREPPERSON] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del módulo del sistema (CHAR 2, FK a SEGmodulu) al que pertenece esta persona reportante; referencia a unidad funcional o área (ej: facturación, RIPS, recaudos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'MODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Módulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'MODULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'MODULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Definición técnica o estructura binaria (VARBINARY MAX) de reglas o configuración de la persona reportante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DEFINICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Definición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DEFINICION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DEFINICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de la persona reportante, datos adicionales o contexto, hasta 2000 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Título o nombre principal de la persona reportante, hasta 200 caracteres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'TITULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Titulo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'TITULO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'TITULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la persona reportante en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de reportes y documentos del sistema, donde cada registro representa un reporte o formulario disponible por módulo, incluyendo su título, descripción y definición binaria (plantilla o layout del reporte).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERSON';
