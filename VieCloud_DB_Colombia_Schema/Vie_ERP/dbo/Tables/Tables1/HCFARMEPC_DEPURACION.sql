CREATE TABLE [dbo].[HCFARMEPC_DEPURACION] (
    [CODCONCEC] NUMERIC (18) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de depuración de la historia clínica farmacéutica (HCFARMEPC), utilizado para auditar, corregir o limpiar datos relacionados con la dispensación y seguimiento farmacéutico de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC_DEPURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC_DEPURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del concepto de cargue o concepto económico asociado al registro farmacéutico en el proceso de depuración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC_DEPURACION', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC_DEPURACION', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
