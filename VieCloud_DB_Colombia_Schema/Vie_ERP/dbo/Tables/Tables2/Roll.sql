CREATE TABLE [dbo].[Roll] (
    [Id]          INT          NOT NULL,
    [RollCode]    CHAR (3)     NOT NULL,
    [Description] VARCHAR (60) NOT NULL,
    [TimeStamp]   ROWVERSION   NOT NULL,
    [RollType]    TINYINT      NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de catálogo que almacena los roles o perfiles del sistema, identificados por un código de 3 caracteres y una descripción. El campo `RollType` permite clasificar los roles en categorías mediante un valor numérico pequeño. La columna `TimeStamp` de tipo `ROWVERSION` facilita el control de concurrencia optimista en operaciones de actualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Roll';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Roll';
GO
