CREATE TABLE [dbo].[ADTIPOIDENTIFICA] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]          INT           NOT NULL,
    [SIGLA]           VARCHAR (2)   NOT NULL,
    [NOMBRE]          VARCHAR (100) NOT NULL,
    [ESTADO]          BIT           NOT NULL,
    [USUARIOCREA]     CHAR (20)     NOT NULL,
    [FECHACREA]       DATETIME      NOT NULL,
    [USUARIOMODIFICA] CHAR (20)     NULL,
    [FECHAMODIFICA]   DATETIME      NULL,
    [MinimunLength]   INT           NULL,
    [MaximumLength]   INT           NULL,
    [TreasuryCode]    VARCHAR (2)   NULL,
    CONSTRAINT [PK_ADTIPOIDENTIFICA] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ADTIPOIDENTIFICA_1]
    ON [dbo].[ADTIPOIDENTIFICA]([ESTADO] ASC, [SIGLA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud máxima permitida del documento de identificación (cédula, pasaporte, tarjeta de identidad). Tipo INT, aplica validación de caracteres para PII Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MaximumLength';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la longitud maxima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MaximumLength';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MaximumLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Longitud mínima permitida del documento de identificación (cédula, pasaporte, tarjeta de identidad). Tipo INT, aplica validación de caracteres para PII Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MinimunLength';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la longitud minima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MinimunLength';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'MinimunLength';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro del tipo de identificación. Tipo DATETIME, trazabilidad de auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha en que fue modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que realizó la última modificación del tipo de identificación. Tipo CHAR(20), auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el usuario quien modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del tipo de identificación. Tipo DATETIME, marca temporal de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHACREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'FECHACREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o cuenta que creó el registro del tipo de identificación. Tipo CHAR(20), auditoría de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Usuario quien creo el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'USUARIOCREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tipo de identificación: 1=Activo, 0=Inactivo. Tipo BIT, controla disponibilidad para pacientes, documentos y atenciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el estado  1 = Activo  0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del tipo de identificación: CC (Cédula Ciudadanía), CE (Cédula Extranjería), TI (Tarjeta Identidad), RC (Registro Civil), PA (Pasaporte), AS (Adulto Sin ID), MS (Menor Sin ID), NU (Número Único), CN (Certificado Nacido Vivo), CD (Carnet Diplomático), SC (Salvoconducto), PE (Permiso Permanencia). Tipo VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=CC -  Cédula de Ciudadanía  2=CE  -  Cédula de Extranjería  3=TI   -  Tarjeta de Identidad  4=RC  -  Registro Civil  5=PA  -  Pasaporte  6=AS  -  Adulto Sin Identificación  7=MS  -  Menor Sin Identificación  8=NU  -  Número único de identificación personal  9= CN  -  Certificado Nacido Vivo  10= CD  -  Carnet Diplomático (Aplica para extranjeros)  11= SC  -  Salvoconducto (Aplica para extranjeros)  12=PE  -  Permiso especial de Permanencia (Aplica para extranjeros)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'NOMBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de dos caracteres del tipo de identificación: CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE. Tipo VARCHAR(2), usado en documentos RIPS, facturas, contratos y datos de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'SIGLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1=CC -  Cédula de Ciudadanía  2=CE  -  Cédula de Extranjería  3=TI   -  Tarjeta de Identidad  4=RC  -  Registro Civil  5=PA  -  Pasaporte  6=AS  -  Adulto Sin Identificación  7=MS  -  Menor Sin Identificación  8=NU  -  Número único de identificación personal  9= CN  -  Certificado Nacido Vivo  10= CD  -  Carnet Diplomático (Aplica para extranjeros)  11= SC  -  Salvoconducto (Aplica para extranjeros)  12=PE  -  Permiso especial de Permanencia (Aplica para extranjeros)    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'SIGLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'SIGLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico único del tipo de identificación para referencia interna y FK en tablas de pacientes y documentos. Tipo INT, identificador lógico de dominio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'CODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) y consecutivo auto-incremental de la tabla ADTIPOIDENTIFICA. Tipo INT IDENTITY, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de documento de identificación aceptados en el sistema (cédula de ciudadanía, pasaporte, tarjeta de identidad, etc.). Define la sigla, nombre, longitudes válidas y estado de cada tipo de identificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de documento según la clasificación de la entidad fiscal o de tesorería (DIAN u organismo equivalente), usado para reportes tributarios y de facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'TreasuryCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADTIPOIDENTIFICA', @level2type = N'COLUMN', @level2name = N'TreasuryCode';
