CREATE TABLE [MedicalDiet].[DietControlNursing] (
    [Id]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmissionNumber]      CHAR (10)                                                                        NOT NULL,
    [AttentionCenter]      CHAR (10)                                                                        NOT NULL,
    [FunctionalUnit]       CHAR (10)                                                                        NOT NULL,
    [CodeBed]              INT                                                                              NOT NULL,
    [CodeDiagnostic]       CHAR (4)                                                                         NULL,
    [FoodKind]             INT                                                                              NOT NULL,
    [DateDietOrder]        DATETIME                                                                         NOT NULL,
    [PhysicianObservation] VARCHAR (500)                                                                    NULL,
    [NurseObservation]     VARCHAR (500)                                                                    NULL,
    [UserCreation]         CHAR (20)                                                                        NOT NULL,
    [CreationDate]         DATETIME                                                                         NOT NULL,
    [idCHREGESTA]          INT                                                                              NOT NULL,
    CONSTRAINT [PK_DietControlNursing] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdmissionNumber_NUMINGRES] FOREIGN KEY ([AdmissionNumber]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [Fk_idCHREGESTA_CHREGESTA] FOREIGN KEY ([idCHREGESTA]) REFERENCES [dbo].[CHREGESTA] ([ID]),
    CONSTRAINT [FK_PatientCode_IPCODPACI] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MedicalDiet].[DietControlNursing].[PatientCode]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la estancia', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'idCHREGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación (DATETIME), timestamp del registro del control de dieta en el sistema', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación (CHAR 20), identificador del profesional/operario que registra el control de dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario creacion registro', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'UserCreation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de enfermería (VARCHAR 500, nullable), nota de la enfermera que registra/solicita la dieta al momento de la orden', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'NurseObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la enfermera que la registra cuando solicita la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'NurseObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'NurseObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación del médico (VARCHAR 500, nullable), nota clínica/comentario del profesional que ordena la dieta en historia clínica', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PhysicianObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion del medico que hace en la Historia clinica', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PhysicianObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PhysicianObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de orden de dieta (DATETIME), momento cuando el médico ordena o la enfermera registra/agrega nueva dieta al paciente', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'DateDietOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la orden de la dieta   1. Puede ser cuando la ordeno el medico.  2. Cuando la enfermera agrega una nueva dieta a ese paciente se toma esta. ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'DateDietOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'DateDietOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de comida (INT): 0=Desayuno, 1=Almuerzo, 2=Cena, 3=Complemento Mañana, 4=Complemento Tarde, 5=Otro', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FoodKind';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de comida:  0 - Desayuno  1 - Almuerzo  2 - Cena  3 - Complemento Mañana  4 - Complemento Tarde  5 - Otro  ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FoodKind';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FoodKind';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CHAR 4, nullable), clasificación CIE o diagnóstico clínico vigente cuando se ordena la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeDiagnostic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico que tenia cuando la enfermera solicito la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeDiagnostic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeDiagnostic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cama (INT), identificador de la cama asignada al paciente en el momento de solicitar la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeBed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama que tenia asignada el paciente cuando solicito la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeBed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeBed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10), área o sección (urgencia, hospitalización, ambulatoria) que controla la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (CHAR 10), unidad de salud donde se registra el control dietético', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AttentionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo centro atención', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AttentionCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AttentionCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso (CHAR 10, FK a ADINGRESO), identificador del ingreso activo cuando la enfermera solicita la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso que el paciente tenia cuando solicito la enfermera la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, PII ofuscado), identificación/cédula/documento del paciente en la orden de dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY), consecutivo de la tabla DietControlNursing', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de control de dietas por enfermería: guarda las órdenes de dieta asignadas a pacientes hospitalizados, incluyendo el tipo de alimentación indicada, el médico tratante, la unidad y cama, diagnóstico asociado, observaciones del médico y de enfermería, y la fecha en que se generó la orden.', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing';
