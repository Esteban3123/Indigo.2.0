CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId]    NVARCHAR (150)  NOT NULL,
    [ContextKey]     NVARCHAR (300)  NOT NULL,
    [Model]          VARBINARY (MAX) NOT NULL,
    [ProductVersion] NVARCHAR (32)   NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC, [ContextKey] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de control de migraciones generada automáticamente por Entity Framework, utilizada para registrar el historial de migraciones aplicadas a la base de datos. Almacena el identificador de cada migración, la clave de contexto EF, el modelo serializado y la versión del framework, permitiendo rastrear el estado del esquema y evitar aplicar migraciones duplicadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'__MigrationHistory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'__MigrationHistory';
GO
