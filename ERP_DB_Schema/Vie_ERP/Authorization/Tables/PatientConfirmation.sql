CREATE TABLE [Authorization].[PatientConfirmation] (
    [Id]               INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PatientCode]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [AdmisionNumber]   CHAR (10)                                                                        NOT NULL,
    [ConfirmationUser] VARCHAR (20)                                                                     NOT NULL,
    [ConfirmationDate] DATETIME                                                                         NOT NULL,
    CONSTRAINT [PK_PatientConfirmation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referente a la fecha que se completo la confirmación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referente al codigo del usuario que realizó la confirmación del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referente a el numero de admisión del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'AdmisionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referente al codigo del paciente, este codigo es el mismo del campo IPCODPACI de la tabla dbo.INPACIENT.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la confirmación del paciente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'PatientConfirmation', @level2type = N'COLUMN', @level2name = N'Id';

