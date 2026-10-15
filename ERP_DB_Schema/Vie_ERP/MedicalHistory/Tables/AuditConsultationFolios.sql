CREATE TABLE [MedicalHistory].[AuditConsultationFolios] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [IPCODPACI]            VARCHAR (25)    NOT NULL,
    [NUMINGRES]            CHAR (10)       NULL,
    [CODCENATE]            CHAR (10)       NULL,
    [UFUCODIGO]            CHAR (10)       NULL,
    [RegisterDate]         DATETIME        NOT NULL,
    [ComputerName]         VARCHAR (50)    NOT NULL,
    [ComputerIP]           VARCHAR (50)    NOT NULL,
    [Latitudelocation]     NUMERIC (18, 3) NULL,
    [lengthlocation]       NUMERIC (18, 3) NULL,
    [ConsultationPlatform] INT             NOT NULL,
    [FormConsulted]        INT             NOT NULL,
    [CODUSUARI]            CHAR (20)       NOT NULL,
    [CODPROSAL]            CHAR (10)       NULL,
    CONSTRAINT [PK_AuditConsultationFolios] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo usuario', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Formulario de consulta historias   2 - Dashboard tablero   ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'FormConsulted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - vie   2 - indira - imagenologia   3 - indira - laboratorio   4 - Indira patologia    ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ConsultationPlatform';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Longitud  localización ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'lengthlocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Latitud localización ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Latitudelocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Ipcomputadora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre de la computadora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la fecha del registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Id';

