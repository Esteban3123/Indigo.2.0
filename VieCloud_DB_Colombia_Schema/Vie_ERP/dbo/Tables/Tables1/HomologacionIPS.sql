CREATE TABLE [dbo].[HomologacionIPS] (
    [CODIGO]                NVARCHAR (255) NULL,
    [NOMBRE]                NVARCHAR (255) NULL,
    [CUPS CORREGIDO]        NVARCHAR (255) NULL,
    [CUPS NOMBRE CORREGIDO] NVARCHAR (255) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de homologación que mapea códigos y nombres de procedimientos hacia sus equivalentes corregidos en el estándar CUPS (Clasificación Única de Procedimientos en Salud), utilizada para normalizar registros de IPS con codificación incorrecta o no estándar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HomologacionIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'HomologacionIPS';
GO
