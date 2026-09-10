CREATE TABLE [dbo].[CALUSUARIOSD] (
    [ID]                    INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRELACALCOMITESAREAS] INT       NOT NULL,
    [CODIGOUSUARIO]         CHAR (20) NOT NULL,
    [LIDER]                 BIT       NOT NULL,
    CONSTRAINT [PK_CALUSUARIOSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALUSUARIOSD_CALCOMITESAREAS] FOREIGN KEY ([IDRELACALCOMITESAREAS]) REFERENCES [dbo].[CALCOMITESAREAS] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que marca si el usuario es designado como líder o coordinador del comité de calidad. Valores: 1=Sí es líder, 0=No es líder.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'LIDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El registo es True Es seleccionado como LIder ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'LIDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'LIDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (CHAR 20) que participa en el comité de calidad y áreas. Identificador del profesional de salud o personal administrativo asignado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'CODIGOUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'CODIGOUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'CODIGOUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que vincula el registro a la tabla CALCOMITESAREAS. Identifica el comité de calidad y área funcional asociados al usuario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'IDRELACALCOMITESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cual relaciona con ID de la tabla CALCOMITESAREAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'IDRELACALCOMITESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'IDRELACALCOMITESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) que genera automáticamente un número secuencial para cada registro de asignación de usuario a comité.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de usuarios asignados a comités y áreas de calidad, indicando cuál de ellos es el líder o responsable del grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALUSUARIOSD';
