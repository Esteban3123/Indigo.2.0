CREATE TABLE [dbo].[RCPACIENREF] (
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPTIPODOC] INT                                                                              NOT NULL,
    [IPPRINOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NULL,
    [IPPRIAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NULL,
    [IPNOMCOMP] CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [IPFECNACI] DATE MASKED WITH (FUNCTION = 'default()')                                        NOT NULL,
    [IPSEXOPAC] INT                                                                              NOT NULL,
    [CODENTIDA] CHAR (9)                                                                         NULL,
    [AUUBICACI] CHAR (20)                                                                        NOT NULL,
    [IPDIRECCI] VARCHAR (MAX) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [IPTELEFON] VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')         NULL,
    CONSTRAINT [PK_RCPacienRef] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_RCPACIENREF_INUBICACI] FOREIGN KEY ([AUUBICACI]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPPRINOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPSEGNOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPNOMCOMP]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPFECNACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPDIRECCI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[RCPACIENREF].[IPTELEFON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');




GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico fijo del paciente (contacto principal). Dato PII enmascarado. VARCHAR(MAX), permite búsqueda por teléfono de paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fijo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTELEFON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del domicilio del paciente (calle, número, apartamento). Dato PII enmascarado. VARCHAR(MAX), incluye dirección de residencia para correspondencia y atención domiciliaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ubicación geográfica del paciente: barrio urbano o zona rural. FK a INUBICACI. Identificador territorial para segmentación de pacientes por localidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Ubicacion del Paciente, en esta opcion se determina el codigo del Barrio o Ubicacion rural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad EAPB (Empresa Administradora de Planes de Beneficios) del paciente. FK a INENTIDAD. Referencia a aseguradora o plan de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad EAPB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo biológico del paciente: 1=Masculino, 2=Femenino. INT, campo demográfico usado en reportes y estadísticas de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo del Paciente:  1=Masculino   2=Femenino     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (DATE). Dato PII enmascarado. Base para cálculo de edad, grupos etarios y análisis epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo concatenado del paciente (primer nombre + segundo nombre + primer apellido + segundo apellido). CHAR(250), enmascarado PII. Campo de búsqueda integral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente. CHAR(20), enmascarado PII. Componente del nombre para identificación civil e integración de registros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente. CHAR(20), enmascarado PII. Componente principal del apellido para búsqueda y deduplicación de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente. CHAR(20), enmascarado PII. Componente adicional de identificación nominal del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente. CHAR(20), enmascarado PII. Componente principal de identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad: 1=Cédula de Ciudadanía, 2=Cédula de Extranjería, 3=Tarjeta de Identidad, 4=Registro Civil, 5=Pasporte, 6=Adulto Sin Identificación, 7=Menor Sin Identificación, 8=NUIP. INT, classifica la naturaleza del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación  8=Número único de identificación personal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (clave primaria, identificador de paciente). VARCHAR(25), enmascarado PII. Equivalente a cédula o documento de identificación. FK referenciada en múltiples tablas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos personales de referencia de pacientes: identificación, nombre completo, fecha de nacimiento, sexo, entidad aseguradora, dirección y teléfono. Sirve como maestro de pacientes para consultas, admisiones y reportes clínicos o administrativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCPACIENREF';
