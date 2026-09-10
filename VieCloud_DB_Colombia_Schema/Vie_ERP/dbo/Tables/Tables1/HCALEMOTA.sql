CREATE TABLE [dbo].[HCALEMOTA] (
    [CODMOTANU] CHAR (2)  NOT NULL,
    [DESMOTANU] CHAR (80) NOT NULL,
    [TIPSERIPS] INT       NULL,
    CONSTRAINT [PK_HCALEMOTA] PRIMARY KEY CLUSTERED ([CODMOTANU] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio RIPS asociado al motivo de anulación (INT). Clasificación: 1=Laboratorios, 2=Patologías, 3=Imágenes Diagnósticas, 4=Procedimientos no quirúrgicos, 5=Procedimientos quirúrgicos, 6=Interconsultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio  1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4: Procedimeintos no Qx  5: Procedimientos Qx  6: Interconsultas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del motivo de anulación (hasta 80 caracteres). Texto explicativo de la razón por la cual se anula una cita, orden de servicio, procedimiento o interconsulta (ej: paciente no asistió, cancelación médica, cambio de fecha).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Motivo de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'DESMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de anulación (2 caracteres, PK). Identificador único para clasificar razones de cancelación de citas, órdenes o procedimientos en calendario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'CODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA', @level2type = N'COLUMN', @level2name = N'CODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos de anulación de alertas o citas en la historia clínica. Registra las razones por las cuales se cancela o anula un registro clínico, asociadas opcionalmente a un tipo de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCALEMOTA';
