CREATE TABLE [dbo].[AGCACANQX] (
    [CODCACANQ]   CHAR (3)       NOT NULL,
    [DESCAUCAN]   NVARCHAR (150) NOT NULL,
    [ESTCAUCAN]   BIT            NOT NULL,
    [CANCELARWEB] BIT            CONSTRAINT [DF__AGCACANQX__CANCE__0D36447B] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AGCACANQX] PRIMARY KEY CLUSTERED ([CODCACANQ] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, 0=No / 1=Sí) que habilita esta causa de cancelación para ser utilizada en cancelaciones de citas a través del portal web o aplicación móvil.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CANCELARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usar en cancelacion de citas WEB --> 0: No , 1: Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CANCELARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CANCELARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro (activo/inactivo, BIT) que determina si la causa de cancelación está disponible para usar en cancelaciones de citas y atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado(Activo o inactivo) de la Causa de cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción narrativa (NVARCHAR 150) de la causa o motivo de cancelación de cita, consulta o atención médica; ej: Paciente ausente, Cambio de horario solicitado, Emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Causa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador único (CHAR 3) de la causa de cancelación; clave primaria para clasificar y referenciar motivos de cancelación en el sistema de citas y agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CODCACANQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CODCACANQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX', @level2type = N'COLUMN', @level2name = N'CODCACANQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de cancelación de cirugías o procedimientos quirúrgicos agendados. Permite registrar y gestionar los motivos por los cuales se cancela una programación quirúrgica, incluyendo si la cancelación puede realizarse desde el portal web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCACANQX';
