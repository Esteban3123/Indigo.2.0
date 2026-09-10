CREATE TABLE [dbo].[SOLCATEGO] (
    [CATEAUTON] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CATCODIGO] VARCHAR (20)  NOT NULL,
    [CATDESCRI] VARCHAR (100) NOT NULL,
    [CATESTADO] BIT           NOT NULL,
    CONSTRAINT [PK_CATEGORIAS] PRIMARY KEY CLUSTERED ([CATEAUTON] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la categoría: 1=Activo, 0=Inactivo. Bit que indica si la categoría está disponible para uso en solicitudes, órdenes y procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la categoria 1= Activo;2=Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción textual de la categoría. Nombre o etiqueta que identifica el tipo de solicitud, servicio, procedimiento o clasificación asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la categoria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único de la categoría. Identificador corto y legible para referencia rápida en interfaces, reportes y búsquedas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la categoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de autoincrementación. Clave primaria (INT IDENTITY) que identifica de forma inequívoca cada registro de categoría en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATEAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO', @level2type = N'COLUMN', @level2name = N'CATEAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de categorías del sistema. Permite clasificar elementos o conceptos en grupos definidos, cada uno con un código, nombre descriptivo y estado activo/inactivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCATEGO';
