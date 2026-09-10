CREATE TABLE [dbo].[UREPPERMISOU] (
    [ID]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ID_REPORTE] INT       NOT NULL,
    [CODUSUARI]  CHAR (20) NOT NULL,
    CONSTRAINT [PK_UREPPERMISOU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERMISOU_UREPPERSON] FOREIGN KEY ([ID_REPORTE]) REFERENCES [dbo].[UREPPERSON] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario, identificador único del profesional o usuario del sistema que tiene permiso de acceso al reporte; tipo CHAR(20), PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del reporte, clave foránea que referencia la tabla UREPPERSON; identifica el reporte específico para el cual se asignan permisos de visualización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la asignación de permisos (clave primaria, IDENTITY); cada fila representa un permiso de usuario a un reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permisos de acceso a reportes por usuario. Registra qué usuarios tienen autorización para visualizar o ejecutar cada reporte del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU';
