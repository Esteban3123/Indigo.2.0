CREATE TABLE [Nursing].[TherapeuticRestraints] (
    [Id]                              INT                                                                              IDENTITY (1, 1) NOT NULL,
    [PatientCode]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmissionNumber]                 CHAR (10)                                                                        NOT NULL,
    [HealthCenter]                    CHAR (10)                                                                        NOT NULL,
    [HealthUnit]                      CHAR (10)                                                                        NOT NULL,
    [OrderHealthcareProfessional]     CHAR (20)                                                                        NOT NULL,
    [OrderDate]                       DATETIME                                                                         NOT NULL,
    [Status]                          BIT                                                                              NOT NULL,
    [SuspendedHealthcareProfessional] CHAR (20)                                                                        NULL,
    [SuspendedDate]                   DATETIME                                                                         NULL,
    [CodeSuspendedJustification]      CHAR (4)                                                                         NULL,
    [SuspendedObservation]            VARCHAR (300)                                                                    NULL,
    CONSTRAINT [PK_TherapeuticRestraints] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación suspendida de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la suspensión del código de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'CodeSuspendedJustification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de suspención de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional sanitario suspendido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'SuspendedHealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de pedido', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orden del profesional sanitario', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'OrderHealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centro de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de admisión', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo de la restricción terapeutica', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';

