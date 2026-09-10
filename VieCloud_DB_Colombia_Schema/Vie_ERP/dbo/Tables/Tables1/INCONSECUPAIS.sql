CREATE TABLE [dbo].[INCONSECUPAIS] (
    [ID]          INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPAIS]      INT         NOT NULL,
    [CONSECUTIVO] VARCHAR (6) NOT NULL,
    CONSTRAINT [PK_INCONSECUPAIS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo secuencial por país (VARCHAR 6), número correlativo único dentro de cada nación para identificar registros, transacciones o documentos en contexto geográfico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'CONSECUTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del país (INT, FK), clave foránea que relaciona a la tabla [PAISES] para determinar la jurisdicción o ubicación geográfica del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me relaciona el ID pais con la tabla de Paises ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT, PK IDENTITY), clave primaria de la tabla que genera secuencia automática para cada fila', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de consecutivos por país, utilizado para controlar la secuencia numérica de documentos o registros según el país de origen. Permite gestionar numeraciones independientes para cada país configurado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONSECUPAIS';
