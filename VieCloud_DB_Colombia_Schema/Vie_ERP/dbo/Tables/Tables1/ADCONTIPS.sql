CREATE TABLE [dbo].[ADCONTIPS] (
    [CODIGOIPS]   CHAR (100)                                                         NOT NULL,
    [DSCRIPIPS]   VARCHAR (50)                                                       NOT NULL,
    [ESTADO]      BIT                                                                NOT NULL,
    [CODIGONIT]   CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)') NULL,
    [DIRECCIPS]   VARCHAR (100)                                                      NULL,
    [TELEFOIPS]   VARCHAR (15)                                                       NULL,
    [CORREOIPS]   VARCHAR (100)                                                      NULL,
    [DEPMUNCOD]   CHAR (5)                                                           NULL,
    [NIVATEIPS]   CHAR (1)                                                           NULL,
    [CLASEIPSS]   CHAR (1)                                                           NULL,
    [NOMREPLEG]   VARCHAR (60)                                                       NULL,
    [IDEREPLEG]   VARCHAR (15)                                                       NULL,
    [CODCENATE]   CHAR (10)                                                          NULL,
    [CODHABILITA] VARCHAR (15)                                                       NOT NULL,
    [DISTANCIA]   NUMERIC (18)                                                       NULL,
    [TIEMESPERA]  NUMERIC (18)                                                       NULL,
    CONSTRAINT [PK_ADCONTIPS] PRIMARY KEY CLUSTERED ([CODIGOIPS] ASC),
    CONSTRAINT [FK_ADINGRESO_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [CODHABILITA] UNIQUE NONCLUSTERED ([CODHABILITA] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[ADCONTIPS].[CODIGONIT]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo de espera remitido en horas (h). Duración estimada de atención en la IPS, referenciada en consultas y remisiones. Tipo: NUMERIC(18), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TIEMESPERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo de espera remitido en h', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TIEMESPERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TIEMESPERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distancia en kilómetros (km) desde punto de origen a la IPS. Métrica de localización geográfica. Tipo: NUMERIC(18), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DISTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Distancia en km', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DISTANCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DISTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Habilitación de la Institución Prestadora de Servicios de Salud (IPS). Identificador único regulatorio emitido por autoridad sanitaria. Tipo: VARCHAR(15), clave única, requerido. PK junto a CODIGOIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODHABILITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de Habilitación de la Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODHABILITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODHABILITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención de la IPS. Referencia a tabla ADCENATEN para desagregación de sedes/sucursales. Tipo: CHAR(10), FK, nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del representante legal de la IPS. Cédula, NIT o documento de identidad (PII). Tipo: VARCHAR(15), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'IDEREPLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del respresentante legal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'IDEREPLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'IDEREPLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del representante legal de la IPS. Denominación completa del apoderado o director legal. Tipo: VARCHAR(60), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NOMREPLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Representante legal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NOMREPLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NOMREPLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de Institución Prestadora de Servicios de Salud (IPS). Categorización: pública, privada, mixta, cooperativa. Tipo: CHAR(1), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CLASEIPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CLASEIPSS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CLASEIPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de la Institución Prestadora de Servicios de Salud (IPS). Clasificación por complejidad (1=bajo, 2=medio, 3=alto). Tipo: CHAR(1), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NIVATEIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NIVATEIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NIVATEIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de municipio de la Institución Prestadora de Servicios de Salud (IPS). Ubicación territorial, departamento y municipio. Tipo: CHAR(5), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Municipio  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico de la Institución Prestadora de Servicios de Salud (IPS). Email de contacto institucional. Tipo: VARCHAR(100), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CORREOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CORREOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CORREOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de la Institución Prestadora de Servicios de Salud (IPS). Número de contacto telefónico. Tipo: VARCHAR(15), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TELEFOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TELEFOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TELEFOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de la Institución Prestadora de Servicios de Salud (IPS). Domicilio completo, calle, número, localidad. Tipo: VARCHAR(100), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DIRECCIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DIRECCIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DIRECCIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT (Número de Identificación Tributaria) de la Institución Prestadora de Servicios de Salud (IPS). Identificador tributario, ofuscado (PII masked). Tipo: CHAR(15), nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nit Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Institución Prestadora de Servicios de Salud (IPS). Bit: 1=Activa/Vigente, 0=Inactiva/Deshabilitada. Indica si la IPS está operativa. Tipo: BIT, requerido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Activo=1;Inactivo:0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la Institución Prestadora de Servicios de Salud (IPS). Nombre o razón social de la IPS. Tipo: VARCHAR(50), requerido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DSCRIPIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DSCRIPIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DSCRIPIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Institución Prestadora de Servicios de Salud (IPS). Identificador único de la entidad prestadora. Clave primaria, búsqueda de IPS, remisiones, atenciones. Tipo: CHAR(100), requerido, PK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Institucion Prestadora de Servicios de Salud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Directorio de IPS (Instituciones Prestadoras de Servicios de Salud) contratadas o relacionadas con la red de atención. Guarda datos de identificación, contacto, nivel de atención y representación legal de cada IPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS';
