CREATE TABLE [dbo].[Meses] (
    [Codigo] CHAR (2)  NOT NULL,
    [Mes]    CHAR (15) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla de referencia de los doce meses del año, usada para filtros y reportes por período mensual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico del mes (01 a 12), usado como llave de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses', @level2type = N'COLUMN', @level2name = N'Codigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del mes en texto (por ejemplo: Enero, Febrero), para mostrar en pantallas y reportes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses', @level2type = N'COLUMN', @level2name = N'Mes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Meses', @level2type = N'COLUMN', @level2name = N'Mes';
