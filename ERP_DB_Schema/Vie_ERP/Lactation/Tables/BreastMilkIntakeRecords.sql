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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que descartó', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ProfessionalCodeCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del descarte', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'DateCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificacion de la cancelación  y/o anulacion', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'JustificationCancellation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de la anulación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'IdCODMOTANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el estado de la extraccion de leche materna 

1 - Activo 

2 -Descartado  3 - Procesado', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del centro de atencion al que pertenece la leche materna ', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de almacenamiento
1 -  Refrigeración 
2 - Congelación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen almacenado en mililitros (4 enteros + 1 decimal)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StoredVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de almacenamiento', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'StorageDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de la extracción (máx 100 caracteres)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de lote (máx 20 enteros, no repetido)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'BatchNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen extraído en mililitros (4 enteros + 1 decimal)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractedVolume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de extracción', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'ExtractionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (niño) que recibe la leche materna donada por la madre.  

Se utiliza para establecer la relación entre la leche donada y el bebé receptor.

', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeRecipient';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Paciente mujer donante:

Persona fuente de la leche materna (lista desplegable, solo mujeres registradas  del formulario de pacientes.





', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'PatientCodeDonor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de leche materna

1. Autóloga (madre): habilita el campo Paciente y exige la asociación entre persona fuente y paciente.

             

2. Heteróloga (donante): desactiva el campo Paciente y permite el registro sin asociar un bebé específico.          
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords', @level2type = N'COLUMN', @level2name = N'SourceMilk';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de recepción de leche materna

Esta es una tabla genérica utilizada para registrar de manera preliminar la extracción y el almacenamiento de leche materna, donde se diligencian datos como el origen de la leche (autóloga o heteróloga), la persona fuente, el paciente cuando aplica, la fecha y hora de extracción, el volumen extraído, el lote, las observaciones, así como la fecha, volumen y tipo de almacenamiento (refrigeración o congelación). Este registro constituye el paso previo al proceso de gestión dentro del módulo de lactario, ya que primero se recibe y almacena la leche en esta tabla y posteriormente se gestiona en el dashboard de lactario para su uso clínico y trazabilidad.
', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'BreastMilkIntakeRecords';

