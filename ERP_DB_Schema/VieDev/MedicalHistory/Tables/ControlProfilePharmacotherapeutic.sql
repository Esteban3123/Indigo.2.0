CREATE TABLE [MedicalHistory].[ControlProfilePharmacotherapeutic] (
    [id]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [PharmaProfileChange] BIT                                                                              NOT NULL,
    [CreateDate]          DATETIME                                                                         NOT NULL,
    [CreateProfessional]  CHAR (20)                                                                        NOT NULL,
    [ModifyDate]          DATETIME                                                                         NULL,
    [ModifyProfessional]  CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_ControlProfilePharmacotherapeutic] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPACIENT] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL] FOREIGN KEY ([CreateProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL1] FOREIGN KEY ([ModifyProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [MedicalHistory].[ControlProfilePharmacotherapeutic] NOCHECK CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPACIENT];


GO
ALTER TABLE [MedicalHistory].[ControlProfilePharmacotherapeutic] NOCHECK CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL];


GO
ALTER TABLE [MedicalHistory].[ControlProfilePharmacotherapeutic] NOCHECK CONSTRAINT [FK_ControlProfilePharmacotherapeutic_INPROFSAL1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda quien modifico  ( profesional)', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de modificación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'ModifyDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'CreateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cambio de perfil farmacéutico  1= si     0 = No', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PharmaProfileChange';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'ControlProfilePharmacotherapeutic', @level2type = N'COLUMN', @level2name = N'id';

