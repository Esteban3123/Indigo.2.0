CREATE TABLE [dbo].[AGCAUCACE] (
    [CODCAUCAN] CHAR (3)       NOT NULL,
    [DESCAUCAN] NVARCHAR (150) NOT NULL,
    [ESTCAUCAN] BIT            NOT NULL,
    CONSTRAINT [PK_AGCAUCACE] PRIMARY KEY CLUSTERED ([CODCAUCAN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) del motivo de cancelación de citas en espera; indica si la causa es vigente para registros nuevos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado(Activo o inactivo) de la Causa de cancelación de la cita en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'ESTCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada (NVARCHAR 150) del motivo o causa de cancelación de citas en espera; texto legible para reportes y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de Causa de cancelacion de las citas en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'DESCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (CHAR 3) único de la causa de cancelación de cita en espera; equivalente a motivo, razón o justificación de no-asistencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion de la citas en espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de causas de cancelación de citas o agendamientos. Registra los motivos por los cuales una cita médica o actividad agendada puede ser cancelada, junto con su estado de vigencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCAUCACE';
