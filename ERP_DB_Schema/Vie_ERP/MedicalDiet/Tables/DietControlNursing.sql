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
ALTER TABLE [MedicalDiet].[DietControlNursing] NOCHECK CONSTRAINT [FK_AdmissionNumber_NUMINGRES];


GO
ALTER TABLE [MedicalDiet].[DietControlNursing] NOCHECK CONSTRAINT [Fk_idCHREGESTA_CHREGESTA];


GO
ALTER TABLE [MedicalDiet].[DietControlNursing] NOCHECK CONSTRAINT [FK_PatientCode_IPCODPACI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la estancia', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'idCHREGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion registro', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario creacion registro', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'UserCreation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la enfermera que la registra cuando solicita la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'NurseObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion del medico que hace en la Historia clinica', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PhysicianObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la orden de la dieta   1. Puede ser cuando la ordeno el medico.  2. Cuando la enfermera agrega una nueva dieta a ese paciente se toma esta. ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'DateDietOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comida:  0 - Desayuno  1 - Almuerzo  2 - Cena  3 - Complemento Mañana  4 - Complemento Tarde  5 - Otro  ', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FoodKind';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico que tenia cuando la enfermera solicito la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeDiagnostic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama que tenia asignada el paciente cuando solicito la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'CodeBed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'FunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo centro atención', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AttentionCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso que el paciente tenia cuando solicito la enfermera la dieta', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalDiet', @level1type = N'TABLE', @level1name = N'DietControlNursing', @level2type = N'COLUMN', @level2name = N'Id';

