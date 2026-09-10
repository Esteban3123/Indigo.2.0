CREATE TABLE [dbo].[CentrosCosto] (
    [Codigo] VARCHAR (50) NULL,
    [Nombre] VARCHAR (50) NULL,
    [Estado] BIT          NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catálogo de centros de costo con código identificador, nombre descriptivo y estado activo/inactivo. Sirve como tabla maestra para clasificar unidades o áreas de gasto dentro de la organización de salud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CentrosCosto';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CentrosCosto';
GO
