CREATE TABLE [dbo].[ADPA3047C] (
    [CODCENATE]          CHAR (10)      NOT NULL,
    [TIPENTREP]          CHAR (1)       NOT NULL,
    [INNUMTELL]          CHAR (5)       NULL,
    [NUTELCENL]          CHAR (7)       NULL,
    [NUEXTTELL]          CHAR (6)       NULL,
    [ENTEMAILL]          CHAR (50)      NULL,
    [INNUMTELD]          CHAR (5)       NOT NULL,
    [NUTELCEND]          CHAR (7)       NOT NULL,
    [NUEXTTELD]          CHAR (6)       NOT NULL,
    [ENTEMAILD]          CHAR (50)      NOT NULL,
    [CARDOCTRA]          CHAR (255)     NOT NULL,
    [INDAUDFOR]          NUMERIC (18)   NOT NULL,
    [EMAILREFECONT]      CHAR (100)     NULL,
    [ConectionFileShare] VARCHAR (450)  NULL,
    [LoadAccountSetup]   VARCHAR (1000) NULL,
    CONSTRAINT [PK_ADPA3047C] PRIMARY KEY CLUSTERED ([CODCENATE] ASC),
    CONSTRAINT [FK_ADPA3047C_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO





GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cadena de conexión a Azure File Share, credenciales y endpoint para acceso de servicios backend EHR a documentos adjuntos e imágenes (ej: integración ODO-Aquila radiología) (VARCHAR 450, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ConectionFileShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conection string del Azure File Share de los documentos adjuntos.
->esta se usa en los servicios Backend del EHR.
->surje de la necesidad de una app service azure conectar con un azure file share
->actualmente se usa para cargar las lecturas de imagenologia de la integracion ODO - Aquila', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ConectionFileShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ConectionFileShare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico configurado como remitente de referencias médicas y derivaciones entre centros (CHAR 100, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'EMAILREFECONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo para configurar  el E-Mail de donde se va enviar la referencia.C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'EMAILREFECONT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'EMAILREFECONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificador de auditoría, control de trazabilidad y conformidad normativa (NUMERIC 18).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recurso compartido/ruta UNC para almacenamiento de documentos complementarios, carpeta de red para archivos adicionales del centro (CHAR 255).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CARDOCTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recurso Compartido para el Almacenamiento de Documentos Adicionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CARDOCTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CARDOCTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico DTD, email corporativo de la Dirección Territorial Departamental para envíos de reportes (CHAR 50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'E-Mail DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de extensión telefónica DTD, extensión asociada al fax departamental (CHAR 6).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico fax DTD, línea de contacto de la Dirección Territorial Departamental (CHAR 7).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCEND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fax DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCEND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCEND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicativo/prefijo del número telefónico DTD (Dirección Territorial Departamental), código de área (CHAR 5).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicativo del Numero Telefonico Fax DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico DTL, email de contacto de la Dirección Territorial Local para notificaciones y referencias (CHAR 50, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'E-Mail DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'ENTEMAILL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de extensión telefónica DTL, complemento del fax de Dirección Territorial Local (CHAR 6, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Extension DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUEXTTELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número telefónico fax DTL, contacto de la Dirección Territorial Local para la unidad funcional (CHAR 7, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCENL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fax DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCENL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'NUTELCENL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicativo/prefijo del número telefónico DTL (Dirección Territorial Local), área geográfica o país (CHAR 5, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicativo del Numero Telefonico Fax DTL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'INNUMTELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de entidad territorial de reporte: 1=DTL y DTD (Dirección Territorial Local y Departamental), 2=DTD solo. Clasificación administrativa de salud (CHAR 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'TIPENTREP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Direccion Territorial de Salud a la que se reporta  1: DTL y DTD  2: DTD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'TIPENTREP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'TIPENTREP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, identificador único del punto de servicio de salud (CHAR 10). Referencia a ADCENATEN, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centron de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recurso compartido en Azure Blob Storage para almacenamiento de armado/configuración de cuentas y datos de setup inicial (VARCHAR 1000, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'LoadAccountSetup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recurso Compartido para el Almacenamiento de Armado de cuentas, azure blob storage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'LoadAccountSetup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C', @level2type = N'COLUMN', @level2name = N'LoadAccountSetup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de contacto y comunicación por centro de atención: guarda los datos de teléfono, extensión y correo electrónico (línea general y directa), así como parámetros de integración documental y conexión a recursos compartidos para cada centro de atención registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADPA3047C';
