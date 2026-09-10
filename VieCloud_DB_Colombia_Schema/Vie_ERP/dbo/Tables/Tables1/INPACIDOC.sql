CREATE TABLE [dbo].[INPACIDOC] (
    [CODDOCALM]   NUMERIC (10)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DESDOCALM]   CHAR (200)                                                                       NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [FECREGDOC]   DATETIME                                                                         NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]   CHAR (10)                                                                        NULL,
    [NOMANTARC]   CHAR (250)                                                                       NOT NULL,
    [EXTEARCHI]   CHAR (5)                                                                         NOT NULL,
    [CODUSUARI]   CHAR (20)                                                                        NOT NULL,
    [TIPARCPAC]   CHAR (1)                                                                         NOT NULL,
    [INDAUDFOR]   NUMERIC (18)                                                                     NOT NULL,
    [TIPODOCUM]   VARCHAR (3)                                                                      NULL,
    [Observation] VARCHAR (500)                                                                    NULL,
    [SupportDate] DATETIME                                                                         NULL,
    CONSTRAINT [PK_INPACIDOC] PRIMARY KEY CLUSTERED ([CODDOCALM] ASC),
    CONSTRAINT [FK_INPACIDOC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_INPACIDOC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_ADINGRESO];


GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_INPACIENT];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIDOC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_ADINGRESO];


GO
ALTER TABLE [dbo].[INPACIDOC] NOCHECK CONSTRAINT [FK_INPACIDOC_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_INPACIDOC]
    ON [dbo].[INPACIDOC]([TIPARCPAC] ASC, [IPCODPACI] ASC, [CODCENATE] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIDOC_3]
    ON [dbo].[INPACIDOC]([IPCODPACI] ASC, [NUMINGRES] ASC, [TIPARCPAC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIDOC_2]
    ON [dbo].[INPACIDOC]([IPCODPACI] ASC, [TIPARCPAC] ASC);


GO
ALTER INDEX [IX_INPACIDOC_2]
    ON [dbo].[INPACIDOC] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del soporte técnico o gestión del documento; timestamp de cuando se resolvió o procesó la carga del archivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'SupportDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha hora soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'SupportDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'SupportDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones registradas por el usuario al cargar el archivo; notas diligenciadas en el formulario emergente que acompañan el documento del paciente o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dato diligenciado en el campo Observaciones, en el formulario emergente al momento de cargar el archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento clínico o administrativo (ej: historia, receta, examen, imagen, diagnóstico, RIPS); VARCHAR(3) clasificación del contenido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPODOCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPODOCUM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPODOCUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría y formato; campo de registro de trazabilidad y control de cambios en la carga documental.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo de registro de auditoría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de archivo almacenado: 1=Documento a nivel Paciente, 2=Documento a nivel Ingreso/Atención; clasificación del alcance del archivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPARCPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el tipo de archivo guardado  1: Paciente  2: Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPARCPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'TIPARCPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que realizó la carga o gestión del documento; identificador del profesional de salud o administrativo responsable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Extensión del archivo (ej: pdf, jpg, png, doc, docx); formato o tipo de fichero almacenado en repositorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Extension del Archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'EXTEARCHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre anterior o nombre original del archivo antes de ser procesado; referencia histórica del fichero cargado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre anterior del archivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NOMANTARC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso/atención/consulta; FK a ADINGRESO; vincula el documento a un episodio de atención específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (cédula, identificación, documento PII ofuscado); identificador único del paciente propietario del archivo; MASKED.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro del documento en el sistema; timestamp de creación o carga del archivo en el repositorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha hora del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'FECREGDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/institución donde se registró el documento; identificador de la unidad funcional o sede.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del documento almacenado; título, nombre descriptivo o resumen del contenido del archivo guardado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'DESDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Documento Almacenado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'DESDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'DESDOCALM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del documento almacenado (PK Identity); identificador secuencial del registro en tabla INPACIDOC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del docuento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC', @level2type = N'COLUMN', @level2name = N'CODDOCALM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos y archivos adjuntos de pacientes: almacena los documentos escaneados o cargados digitalmente asociados a un paciente y/o ingreso, incluyendo el tipo de archivo, fecha de registro, usuario que lo cargó y el centro de atención correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIDOC';
