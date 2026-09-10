CREATE TABLE [dbo].[HCPARNUTDCA] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCPARNUTCId] INT       NULL,
    [CODCENATE]   CHAR (10) NULL,
    CONSTRAINT [PK__HCPARNUT__3214EC07290D27DE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_IDNUTDCADCENATEN] FOREIGN KEY ([HCPARNUTCId]) REFERENCES [dbo].[HCPARNUTC] ([ID]),
    CONSTRAINT [FK_NUTDCAADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);


GO
ALTER TABLE [dbo].[HCPARNUTDCA] NOCHECK CONSTRAINT [FK_IDNUTDCADCENATEN];




GO
ALTER TABLE [dbo].[HCPARNUTDCA] NOCHECK CONSTRAINT [FK_IDNUTDCADCENATEN];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (unidad funcional, clínica, hospital) donde se parametriza la nutrición parenteral; referencia a ADCENATEN; tipo CHAR(10); clave foránea para identificar la sede de prestación del servicio de nutrición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del centro de atencion donde se esta parametrizando la nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cabecera (registro maestro) de nutrición parenteral; referencia a HCPARNUTC; clave foránea que vincula el detalle con la orden principal de nutrición parenteral del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de nutricion parenteral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'HCPARNUTCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de detalle de nutrición parenteral por centro de atención; tipo INT IDENTITY; generado automáticamente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de la relación entre parámetros nutricionales de historia clínica y el centro de atención donde aplican. Asocia configuraciones o registros nutricionales del paciente con un centro de atención específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARNUTDCA';
