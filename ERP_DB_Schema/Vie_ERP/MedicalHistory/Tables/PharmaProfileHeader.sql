CREATE TABLE [MedicalHistory].[PharmaProfileHeader] (
    [Id]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmissionNumber] CHAR (10)                                                                        NOT NULL,
    [CareCenterCode]  CHAR (10)                                                                        NOT NULL,
    [CreationDate]    DATETIME                                                                         NOT NULL,
    [UserCreate]      CHAR (20)                                                                        NOT NULL,
    CONSTRAINT [PK_PharmaProfileHeader] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaProfileHeader_ADCENATEN] FOREIGN KEY ([CareCenterCode]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PharmaProfileHeader_ADINGRESO] FOREIGN KEY ([AdmissionNumber]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PharmaProfileHeader_INPACIENT] FOREIGN KEY ([PatientCode]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [MedicalHistory].[PharmaProfileHeader] NOCHECK CONSTRAINT [FK_PharmaProfileHeader_ADCENATEN];


GO
ALTER TABLE [MedicalHistory].[PharmaProfileHeader] NOCHECK CONSTRAINT [FK_PharmaProfileHeader_ADINGRESO];


GO
ALTER TABLE [MedicalHistory].[PharmaProfileHeader] NOCHECK CONSTRAINT [FK_PharmaProfileHeader_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien   creo', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'UserCreate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha de creación del dato', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código centro ateción', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'CareCenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de admisión', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'PharmaProfileHeader', @level2type = N'COLUMN', @level2name = N'Id';

