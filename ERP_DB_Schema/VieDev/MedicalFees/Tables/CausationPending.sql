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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para controlar si se va a realizar un reintento de causación desde Stella.', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Retry';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el error en caso de que no se haya procesado el registro', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Error';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información necesaria para procesar en formato json', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Data';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Es quirúrgico? | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'IsQx';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de la factura', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalFees', @level1type = N'TABLE', @level1name = N'CausationPending', @level2type = N'COLUMN', @level2name = N'Id';

