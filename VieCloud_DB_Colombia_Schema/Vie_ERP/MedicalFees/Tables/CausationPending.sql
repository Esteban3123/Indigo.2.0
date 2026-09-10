CREATE TABLE [MedicalFees].[CausationPending] (
    [Id]                             INT                                                                              IDENTITY (1, 1) NOT NULL,
    [InvoiceNumber]                  VARCHAR (20)                                                                     NOT NULL,
    [AdmissionNumber]                CHAR (10)                                                                        NOT NULL,
    [PatientCode]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PatientName]                    VARCHAR (255) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')          NOT NULL,
    [PerformsHealthProfessionalCode] VARCHAR (20)                                                                     NOT NULL,
    [InvoiceDate]                    DATETIME                                                                         NOT NULL,
    [IsQx]                           BIT                                                                              NOT NULL,
    [Data]                           VARCHAR (MAX)                                                                    NOT NULL,
    [Error]                          VARCHAR (MAX)                                                                    NOT NULL,
    [CreationDate]                   DATETIME                                                                         NOT NULL,
    [Retry]                          BIT                                                                              CONSTRAINT [DF__Causation__Retry__6D7E6D57] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CausationPending] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MedicalFees].[CausationPending].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MedicalFees].[CausationPending].[PatientName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) para controlar si se realizará reintento de causación desde Stella; 1=reintento pendiente, 0=sin reintento programado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para controlar si se va a realizar un reintento de causación desde Stella.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Retry';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de causación pendiente en la base de datos (DATETIME)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de error capturado cuando el procesamiento de causación falla; almacena detalles técnicos para diagnóstico', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Error';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el error en caso de que no se haya procesado el registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Error';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Error';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON con información completa necesaria para procesar la causación (RIPS, detalles de facturación, procedimientos)', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Data';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información necesaria para procesar en formato json', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Data';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de procedimiento quirúrgico: 1=es quirúrgico, 0=no quirúrgico; determina tipo de causación a procesar', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'IsQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Es quirúrgico? | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'IsQx';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'IsQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de emisión de la factura o documento de cobro (DATETIME); referencia temporal para causación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de salud (médico, cirujano, especialista) que ejecutó el servicio o procedimiento', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del profesional de salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente (PII ofuscado); identificación nominal en expediente de causación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o cédula del paciente (PII ofuscado); identificador único para vinculación con admisión e ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión o ingreso hospitalario; referencia a episodio de atención clínica asociado', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de admisión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de factura o documento de cobro; clave para trazabilidad en glosa y facturación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable (IDENTITY) de la tabla; clave primaria para referencias internas', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de causaciones de honorarios médicos pendientes de procesar. Almacena las facturas con sus ingresos y profesionales asociados que están en cola de reintento por errores en el proceso de causación contable.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending';
