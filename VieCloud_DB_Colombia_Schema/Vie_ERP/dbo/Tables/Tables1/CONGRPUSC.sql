CREATE TABLE [dbo].[CONGRPUSC] (
    [IDGRUPUSC] TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONNOMGRU] VARCHAR (50) NOT NULL,
    [CONNUMUSU] INT          NOT NULL,
    CONSTRAINT [PK_CONGRPUSC] PRIMARY KEY CLUSTERED ([IDGRUPUSC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de usuarios activos asignados al grupo de acceso; INT; control de membresía grupal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNUMUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CANTIDAD DE USUARIOS POR GRUPO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNUMUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNUMUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o denominación del grupo de usuarios; VARCHAR(50); identificador legible del grupo de permisos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNOMGRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNOMGRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'CONNOMGRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial del grupo de usuarios; TINYINT IDENTITY; clave primaria; rango 1-255', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupos de usuarios del sistema con su cantidad de integrantes. Permite organizar y clasificar los usuarios en grupos para la gestión de permisos y accesos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONGRPUSC';
