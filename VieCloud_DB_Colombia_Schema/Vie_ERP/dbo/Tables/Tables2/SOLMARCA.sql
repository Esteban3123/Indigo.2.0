CREATE TABLE [dbo].[SOLMARCA] (
    [Autonumerico] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]       VARCHAR (4)   NOT NULL,
    [Descripcion]  VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_SOLMARCA] PRIMARY KEY CLUSTERED ([Autonumerico] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de la marca/fabricante de equipos médicos o insumos. Texto descriptivo de identificación comercial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Descripcion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Descripcion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de 4 caracteres que identifica la marca, fabricante o proveedor de equipos y materiales de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT) que sirve como clave primaria de la tabla SOLMARCA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA', @level2type = N'COLUMN', @level2name = N'Autonumerico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de marcas o categorías de solicitudes utilizadas en el sistema para clasificar y etiquetar diferentes tipos de solicitudes o requerimientos clínicos y administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLMARCA';
