CREATE TABLE [dbo].[HCDOCOBVI] (
    [AUTO]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [OBSERDOCU] CHAR (100)   NULL,
    CONSTRAINT [PK_HCDOCOBVI] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCDOCOBVI_HCDOCUMAD] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCDOCUMAD] ([CONSECUTI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del archivo de documentos clínicos; notas, comentarios o anotaciones sobre el documento de historia clínica (tipo CHAR 100, NULL permitido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones generales del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico interno que referencia el documento maestro en HCDOCUMAD; identificador único de relación documento-observación (FK a HCDOCUMAD.CONSECUTI, NUMERIC 18)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno autonumérico; clave primaria de la tabla de observaciones de documentos clínicos (INT IDENTITY, PK_HCDOCOBVI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones relacionadas con documentos de la historia clínica del paciente. Permite registrar notas u observaciones adicionales asociadas a un documento clínico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVI';
