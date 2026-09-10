CREATE TABLE [dbo].[NTGRUPOS] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODGRUPO]  VARCHAR (4)   NOT NULL,
    [NOMBRE]    VARCHAR (100) NOT NULL,
    [CODUSUCRE] CHAR (20)     NOT NULL,
    [FECHACREA] DATETIME      CONSTRAINT [DF_NTGRUPO_FECHACREA] DEFAULT ([Common].[getdate]()) NOT NULL,
    [CODUSUMOD] CHAR (20)     NULL,
    [FECHAMOD]  DATETIME      NULL,
    CONSTRAINT [PK_NTGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del grupo. Tipo: DATETIME, nullable. Permite auditar cuándo se actualizó la configuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHAMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que realizó la última modificación del grupo. Tipo: CHAR(20), nullable. Rastreo de cambios y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que Modifica el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del grupo. Tipo: DATETIME, valor por defecto: getdate(). Campo obligatorio para auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador del usuario que creó el registro del grupo. Tipo: CHAR(20), obligatorio. Trazabilidad inicial del grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del grupo de usuarios/roles/entidades. Tipo: VARCHAR(100), obligatorio. Identificación legible del grupo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del grupo, clave de negocio. Tipo: VARCHAR(4), obligatorio. Identificador corto para búsquedas y referencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODGRUPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'CODGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incremental (IDENTITY) de la tabla NTGRUPOS. Tipo: INT, clave primaria. Referencia interna de la base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos o categorías de clasificación utilizados en el sistema. Permite organizar elementos bajo un nombre de grupo, registrando quién lo creó y quién lo modificó por última vez.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTGRUPOS';
