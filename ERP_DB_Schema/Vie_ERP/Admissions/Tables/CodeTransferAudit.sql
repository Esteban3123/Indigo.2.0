CREATE TABLE [Admissions].[CodeTransferAudit] (
    [Id]                         INT           IDENTITY (1, 1) NOT NULL,
    [PreviousPatientCode]        VARCHAR (25)  NOT NULL,
    [PreviousPatientName]        VARCHAR (500) NOT NULL,
    [PreviousIdentificationType] INT           NOT NULL,
    [NewpatientCode]             VARCHAR (25)  NOT NULL,
    [NewpatientName]             VARCHAR (500) NOT NULL,
    [IdentificationTypeNew]      INT           NOT NULL,
    [UserCode]                   CHAR (20)     NOT NULL,
    [Observations]               VARCHAR (250) NOT NULL,
    [RegistrationDate]           DATETIME      NOT NULL,
    CONSTRAINT [PK__CodeTran__3214EC077152B97B] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_CodeTransferAudit_SEGusuaru] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [Admissions].[CodeTransferAudit] NOCHECK CONSTRAINT [Fk_CodeTransferAudit_SEGusuaru];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la fecha del registro ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda observaciones', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda Codigo usuario', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda Tipo de identificacion del paciente nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'IdentificationTypeNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el Nombre paciente Nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda codigo de Paciente Nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el Tipo de identidad paciente Anterior', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousIdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda el Nombre del Paciente Anterior', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'guarda el codigo del paciente Anterior ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientCode';

