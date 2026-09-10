CREATE TABLE [dbo].[CONGRPUSD] (
    [CODUSUARI] CHAR (20) NOT NULL,
    [IDGRUPUSC] TINYINT   NOT NULL,
    CONSTRAINT [PK_CONGRPUSD] PRIMARY KEY CLUSTERED ([CODUSUARI] ASC),
    CONSTRAINT [FK_CONGRPUSD_CONGRPUSC] FOREIGN KEY ([IDGRUPUSC]) REFERENCES [dbo].[CONGRPUSC] ([IDGRUPUSC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del grupo de usuario (TINYINT). Clave foránea que referencia CONGRPUSC.IDGRUPUSC. Define la pertenencia del usuario a un grupo funcional, rol o categoría de permisos en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (CHAR 20). Clave primaria que identifica al profesional de salud, operador o personal del sistema. Equivalente a login, usuario o identificación del operario en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre usuarios del sistema y los grupos de usuarios a los que pertenecen. Permite controlar los permisos y accesos asignando cada usuario a uno o más grupos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSD';
