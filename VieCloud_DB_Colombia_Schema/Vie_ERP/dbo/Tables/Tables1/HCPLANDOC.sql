CREATE TABLE [dbo].[HCPLANDOC] (
    [CODCONSEC] VARCHAR (6)   NOT NULL,
    [DESCRIPLA] CHAR (80)     NOT NULL,
    [TIPDOCUME] TINYINT       NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [NOMARCHIV] CHAR (80)     NOT NULL,
    [EXTARCHIV] CHAR (10)     NOT NULL,
    [ASUNTO]    VARCHAR (100) NULL,
    [CODSERIPS] CHAR (20)     NULL,
    CONSTRAINT [PK_HCPLANDOC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_CODSERIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS (CUPS/SIPS). Identificador único del servicio de salud prestado. FK a tabla INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Asunto o tema del documento. Se registra obligatoriamente cuando el tipo de documento es 12 (Correo Electrónico). Campo para línea de asunto de comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se registra Si el tipo Documento es 12 que es coreo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'ASUNTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'ASUNTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo adjunto (ej: pdf, doc, jpg, png, txt). Define el formato/tipo de archivo digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'EXTARCHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'EXTARCHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'EXTARCHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo. Identificador del archivo digital asociado a la plantilla de documento en el EHR.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'NOMARCHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'NOMARCHIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'NOMARCHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención. Identificador de la unidad funcional, clínica o sede donde se originó el documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Documento (TINYINT 0-20). Clasificación del documento: consentimiento, notificación diagnóstica, laboratorios, imágenes, interconsultas, enfermería, correo electrónico, epicrisis, certificados, terapia, procedimientos quirúrgicos, entre otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento:  0. Consentimiento Informado  1. Notificacion Obligatoria de Diagnosticos  2. Laboratorios  3. Patologias  4. Imágenes Diagnosticas  5. Interconsultas  6. Resultados de Servicios Tomados en Sitio  7. Lectura de Imagenes  8. Insumos / Dispositivos  9. Recomendaciones  10. Enfermeria  11. OtrosProcedimientos  12. Correco Electrónico  13. Complicaciones Epicrisis  14. Pronóstico Epicrisis  15. Recomendaciones Epicrisis  16. Certificados asistenciales  17. RealizoLectura  18. Informe otros procedimientos (dashboard médicos)  19. Terapia  20. Instrumentador Quirurgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'TIPDOCUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la plantilla de documento. Texto explicativo que define el propósito y contenido de la plantilla utilizada en historias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'DESCRIPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'DESCRIPLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'DESCRIPLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo interno. Identificador único (PK) del registro de plantilla de documento en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantillas de documentos clínicos asociadas a servicios y centros de atención. Registra los archivos de plantilla (formularios, formatos) utilizados en la historia clínica, identificando el tipo de documento, el archivo físico y el servicio CUPS al que aplican.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLANDOC';
