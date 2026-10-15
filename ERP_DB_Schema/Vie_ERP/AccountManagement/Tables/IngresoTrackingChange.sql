CREATE TABLE [AccountManagement].[IngresoTrackingChange] (
    [AdmissionNumber]  CHAR (10)    NOT NULL,
    [PatientCode]      VARCHAR (25) NOT NULL,
    [EntryType]        INT          NOT NULL,
    [EntryStatus]      CHAR (1)     NOT NULL,
    [IsDistributed]    BIT          NOT NULL,
    [CreationUser]     CHAR (20)    NULL,
    [CreationDate]     DATETIME     NULL,
    [ModificationUser] CHAR (20)    NULL,
    [ModificationDate] DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([AdmissionNumber] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece si el ingreso ya fue distribuido', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'IngresoTrackingChange', @level2type = N'COLUMN', @level2name = N'IsDistributed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'IngresoTrackingChange', @level2type = N'COLUMN', @level2name = N'EntryStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de ingreso', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'IngresoTrackingChange', @level2type = N'COLUMN', @level2name = N'EntryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'IngresoTrackingChange', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ingreso. Esta tabla hace referencia a ADINGRESO', @level0type = N'SCHEMA', @level0name = N'AccountManagement', @level1type = N'TABLE', @level1name = N'IngresoTrackingChange', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';

