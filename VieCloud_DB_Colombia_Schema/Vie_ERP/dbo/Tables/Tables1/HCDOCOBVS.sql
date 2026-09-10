CREATE TABLE [dbo].[HCDOCOBVS] (
    [AUTO]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CONSECUTI] NUMERIC (18) NOT NULL,
    [OBSERDOCU] CHAR (100)   NULL,
    CONSTRAINT [PK_HCDOCOBVS] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCDOCOBVS_HCDOCUMAD] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCDOCUMAD] ([CONSECUTI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales del archivo o documento clínico; notas, comentarios o anotaciones adicionales sobre el contenido, estado o procesamiento del registro documentario en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones generales del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'OBSERDOCU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico interno que referencia la clave foránea a HCDOCUMAD; identificador único de cada documento clínico en el sistema de historias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Autonumerico Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno autonumérico de la tabla HCDOCOBVS; identificador único de cada registro de observación o anotación asociada a documentos clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones asociadas a documentos de historia clínica. Guarda las notas u observaciones textuales vinculadas a un documento clínico específico del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCOBVS';
