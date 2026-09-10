CREATE TABLE [dbo].[HCDOCHISTORIASCLINICAS] (
    [ID]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]        VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]        CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]    DATETIME                                                                         NOT NULL,
    [USUARIOSUBE]      CHAR (20)                                                                        NOT NULL,
    [NOMBREARCHIVO]    VARCHAR (500)                                                                    NOT NULL,
    [EXTENSIONARCHIVO] VARCHAR (5)                                                                      NOT NULL,
    [TIPODOCUMENTO]    CHAR (3)                                                                         NOT NULL,
    [OBSERVACION]      VARCHAR (MAX)                                                                    NOT NULL,
    CONSTRAINT [PK_HCDOCHISTORIASCLINICAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCDOCHISTORIASCLINICAS_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCDOCHISTORIASCLINICAS].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación, nota o comentario adicional registrado en la historia clínica del paciente. Campo texto libre (VARCHAR MAX) para anotaciones médicas o administrativas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento clínico (CHAR 3). Clasificación según tabla HCCATDOCU: historia, examen, receta, diagnóstico, procedimiento, imagen, laboratorio u otro documento sanitario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lista tipo de documetos desde la tabla HCCATDOCU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'TIPODOCUMENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo adjunto, típicamente .PDF, .JPG, .PNG u otros formatos soportados. Identifica el tipo de archivo digital almacenado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'EXTENSIONARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension del archivo normalmente son   .PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'EXTENSIONARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'EXTENSIONARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del archivo digital de la historia clínica. Ruta o identificador del documento electrónico adjunto (VARCHAR 500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'NOMBREARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'NOMBREARCHIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'NOMBREARCHIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario conectado que cargó o registró el documento. Identificación del profesional de salud o administrativo responsable de la carga.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'USUARIOSUBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que esta connectado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'USUARIOSUBE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'USUARIOSUBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del documento en la historia clínica. Timestamp (DATETIME) del ingreso del archivo al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o institución donde se registra la historia. FK a ADCENATEN, identifica la sede sanitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, cédula, identificación o documento de identidad (PII ofuscado). FK a INPACIENT, identifica unívocamente al paciente en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY). Clave primaria secuencial de cada registro de documento en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos digitales adjuntos a la historia clínica de cada paciente: almacena los archivos cargados (imágenes, PDFs, formularios u otros documentos clínicos) asociados a un paciente y a un centro de atención, registrando quién los subió, cuándo y con qué observaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCHISTORIASCLINICAS';
