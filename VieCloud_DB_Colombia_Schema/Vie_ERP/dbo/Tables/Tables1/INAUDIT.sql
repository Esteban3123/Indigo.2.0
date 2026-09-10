CREATE TABLE [dbo].[INAUDIT] (
    [CODAUDIT]     INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIO]      NCHAR (100) NULL,
    [FECHA]        DATETIME    NULL,
    [NOMBREEQUIPO] NCHAR (100) NULL,
    [DIRECCIONIP]  NCHAR (100) NULL,
    [ACCION]       NCHAR (100) NULL,
    CONSTRAINT [PK_INAUDIT] PRIMARY KEY CLUSTERED ([CODAUDIT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Acción realizada: inserción, actualización, eliminación, lectura o consulta en el sistema; tipo de operación auditada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'ACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Acción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'ACCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'ACCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP, IPv4 o IPv6 del equipo o dispositivo desde el cual se ejecutó la acción; identificador de red', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'DIRECCIONIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'DIRECCIONIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'DIRECCIONIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de equipo, hostname o computadora desde la cual se realizó la operación; identificador del dispositivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'NOMBREEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'NOMBREEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'NOMBREEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se ejecutó la acción auditada; timestamp de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'FECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'FECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o login que ejecutó la acción; identificación del profesional, operador o sistema que realizó la operación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'USUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de auditoría, identificador autoincremental (INT) de cada registro de acción auditada en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'CODAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'CODAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT', @level2type = N'COLUMN', @level2name = N'CODAUDIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría del sistema: almacena el historial de acciones realizadas por los usuarios, indicando quién hizo qué, cuándo, desde qué equipo y dirección IP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAUDIT';
