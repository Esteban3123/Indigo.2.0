CREATE TABLE [Lactation].[BreastMilkIntakeRecords] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [SourceMilk]                   INT             NOT NULL,
    [PatientCodeDonor]             VARCHAR (25)    NOT NULL,
    [PatientCodeRecipient]         VARCHAR (25)    NULL,
    [ExtractionDate]               DATETIME        NOT NULL,
    [ExtractedVolume]              NUMERIC (18, 1) NOT NULL,
    [BatchNumber]                  VARCHAR (20)    NOT NULL,
    [Observations]                 VARCHAR (100)   NOT NULL,
    [StorageDate]                  DATETIME        NOT NULL,
    [StoredVolume]                 NUMERIC (18, 1) NOT NULL,
    [StorageType]                  NCHAR (10)      NOT NULL,
    [CODCENATE]                    CHAR (10)       NOT NULL,
    [Status]                       INT             CONSTRAINT [DF_BreastMilkIntakeRecords_Status] DEFAULT ((1)) NOT NULL,
    [IdCODMOTANU]                  CHAR (4)        NULL,
    [JustificationCancellation]    VARCHAR (500)   NULL,
    [DateCancellation]             DATETIME        NULL,
    [ProfessionalCodeCancellation] CHAR (20)       NULL,
    CONSTRAINT [PK_BreastMilkIntakeRecords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BreastMilkIntakeRecords_CODCENATE_INPACIENT] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_BreastMilkIntakeRecords_IdCODMOTANU_HCMOANULB] FOREIGN KEY ([IdCODMOTANU]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_BreastMilkIntakeRecords_PatientCodeDonor_INPACIENT] FOREIGN KEY ([PatientCodeDonor]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_BreastMilkIntakeRecords_PatientCodeRecipient_INPACIENT] FOREIGN KEY ([PatientCodeRecipient]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_BreastMilkIntakeRecords_ProfessionalCodeCancellation_INPROFSAL] FOREIGN KEY ([ProfessionalCodeCancellation]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, enfermero, técnico lactario) que descartó o anuló el registro de leche materna. FK a INPROFSAL. PII: Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que descartó', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta en que se registró el descarte, anulación o cancelación del lote de leche materna (DATETIME).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'DateCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del descarte', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'DateCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'DateCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón documentada del descarte o anulación: contaminación, vencimiento, error administrativo, baja calidad, rechazo clínico u otro motivo (varchar 500).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la cancelación  y/o anulacion', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo normalizado de anulación/cancelación de leche materna. FK a HCMOANULB (catálogo de motivos de anulación). Permite trazabilidad y auditoría.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de la anulación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del registro de extracción: 1=Activo (disponible para uso), 2=Descartado (anulado/rechazado), 3=Procesado (entregado/consumido en lactario). INT.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el estado de la extraccion de leche materna 

1 - Activo 

2 -Descartado  3 - Procesado', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del centro de atención, unidad funcional o servicio de lactario propietario del lote. FK a ADCENATEN (centros de atención).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del centro de atencion al que pertenece la leche materna ', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de conservación: 1=Refrigeración (4°C, corto plazo), 2=Congelación (-20°C/-80°C, largo plazo). NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de almacenamiento
1 -  Refrigeración 
2 - Congelación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen definitivo almacenado en mililitros tras recepción y validación. NUMERIC(18,1), permite 4 enteros + 1 decimal.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StoredVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen almacenado en mililitros (4 enteros + 1 decimal)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StoredVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StoredVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta de ingreso a almacenamiento (refrigeración o congelación) en lactario. DATETIME.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de almacenamiento', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Anotaciones clínicas/administrativas sobre la extracción: consistencia, color, olor, incidencias, observaciones maternas (máx 100 caracteres).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones de la extracción (máx 100 caracteres)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del lote de leche materna para trazabilidad, no repetible. Vincula donante→extracción→almacenamiento→uso. VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'BatchNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de lote (máx 20 enteros, no repetido)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'BatchNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'BatchNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen bruto extraído en mililitros en acto de ordeña/recolección. NUMERIC(18,1), 4 enteros + 1 decimal.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractedVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumen extraído en mililitros (4 enteros + 1 decimal)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractedVolume';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractedVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta de obtención/recolección de leche materna (ordeña manual/bomba). DATETIME. Crítico para trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de extracción', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/cédula del paciente pediátrico receptor (bebé/neonato) de leche materna donada. FK a INPACIENT. Vincula donación con receptor clínico. PII: Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeRecipient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de identificación del paciente (niño) que recibe la leche materna donada por la madre.  

Se utiliza para establecer la relación entre la leche donada y el bebé receptor.

', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeRecipient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeRecipient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/cédula de la madre o donante de leche materna (solo mujeres). FK a INPACIENT. En autóloga vincula madre→hijo. En heteróloga permite donación anónima. PII: Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeDonor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente mujer donante:

Persona fuente de la leche materna (lista desplegable, solo mujeres registradas  del formulario de pacientes.





', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeDonor';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeDonor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen/tipo de leche: 1=Autóloga (madre dona a su propio hijo, requiere asociación paciente-donante), 2=Heteróloga (donante anónimo, sin receptor específico). INT.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'SourceMilk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Origen de leche materna

1. Autóloga (madre): habilita el campo Paciente y exige la asociación entre persona fuente y paciente.

             

2. Heteróloga (donante): desactiva el campo Paciente y permite el registro sin asociar un bebé específico.          
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'SourceMilk';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'SourceMilk';






GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros preliminares de extracción, almacenamiento y trazabilidad de leche materna (autóloga/heteróloga). Captura origen, donante, receptor, volúmenes, lote, fechas y condiciones de conservación (refrigeración/congelación). Paso previo al dashboard de lactario para gestión clínica, auditoría y uso pediátrico.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Registros de recepción de leche materna

Esta es una tabla genérica utilizada para registrar de manera preliminar la extracción y el almacenamiento de leche materna, donde se diligencian datos como el origen de la leche (autóloga o heteróloga), la persona fuente, el paciente cuando aplica, la fecha y hora de extracción, el volumen extraído, el lote, las observaciones, así como la fecha, volumen y tipo de almacenamiento (refrigeración o congelación). Este registro constituye el paso previo al proceso de gestión dentro del módulo de lactario, ya que primero se recibe y almacena la leche en esta tabla y posteriormente se gestiona en el dashboard de lactario para su uso clínico y trazabilidad.
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de ingesta de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Id';
