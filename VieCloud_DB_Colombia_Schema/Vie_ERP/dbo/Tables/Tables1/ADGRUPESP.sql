CREATE TABLE [dbo].[ADGRUPESP] (
    [GRUPCODIGO] CHAR (3)   NOT NULL,
    [GRUPDESCRI] CHAR (100) NULL,
    CONSTRAINT [PK_ADGRUPESP] PRIMARY KEY CLUSTERED ([GRUPCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del grupo especial; nombre o clasificación que agrupa entidades, pacientes, profesionales o servicios especializados. Tipo: CHAR(100). Permite búsquedas por categoría, especialidad o clasificación administrativa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del grupo especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo especial; identificador único de 3 caracteres (CHAR(3)) que referencia la clasificación especial. Clave primaria. Usado para filtros, reportes administrativos y búsquedas rápidas por grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo especial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de grupos de especialidades médicas. Permite clasificar y agrupar las especialidades clínicas para organización interna, reportes y parametrización del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP';
