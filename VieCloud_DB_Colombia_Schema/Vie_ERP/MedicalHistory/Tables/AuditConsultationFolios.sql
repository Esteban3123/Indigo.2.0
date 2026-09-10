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
    [CODPROSAL]            CHAR (20)       NULL,
    CONSTRAINT [PK_AuditConsultationFolios] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realizó la consulta; identificador único del médico, especialista o profesional vinculado a la atención (VARCHAR 25, FK).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del profesional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario del sistema que accedió o consultó el registro; identificador de sesión del usuario autenticado en ERP/EHR (CHAR 20, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo usuario', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formulario consultado: 1=Formulario de consulta historias clínicas, 2=Dashboard tablero análítico; indicador de qué interfaz fue accedida (INT, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'FormConsulted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Formulario de consulta historias   2 - Dashboard tablero   ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'FormConsulted';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'FormConsulted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plataforma origen de la consulta: 1=Vie, 2=Indira Imagenología, 3=Indira Laboratorio, 4=Indira Patología; módulo transaccional accedido (INT, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ConsultationPlatform';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - vie   2 - indira - imagenologia   3 - indira - laboratorio   4 - Indira patologia    ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ConsultationPlatform';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ConsultationPlatform';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada de longitud geográfica de la ubicación del acceso; dato geoespacial para auditoría de localización (NUMERIC 18,3, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'lengthlocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Longitud  localización ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'lengthlocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'lengthlocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Coordenada de latitud geográfica de la ubicación del acceso; dato geoespacial para auditoría de localización (NUMERIC 18,3, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Latitudelocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Latitud localización ', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Latitudelocation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Latitudelocation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección IP de la computadora/dispositivo desde el cual se realizó el acceso; dato de auditoría de red (VARCHAR 50, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Ipcomputadora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerIP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificativo del equipo informático que generó el acceso; hostname o NetBIOS del cliente (VARCHAR 50, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre de la computadora', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'ComputerName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta del registro de la auditoría de acceso; timestamp de cuando se consultó el folio (DATETIME, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'RegisterDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha del registro', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'RegisterDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'RegisterDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se realizó el acceso; identificador del servicio/departamento de atención (CHAR 10, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (sede, IPS, clínica) donde se registró el acceso; identificador de la entidad prestadora (CHAR 10, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o radicado del paciente en la atención; referencia a la admisión/episodio de urgencia, hospitalización o consulta externa (CHAR 10, opcional).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula, documento de identidad o número de identificación única; identificador PII del usuario atendido (VARCHAR 25, requerido, Identification_Ofuscado).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del paciente', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de auditoría; consecutivo autoincrementable de la tabla AuditConsultationFolios (INT IDENTITY, requerido).', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de accesos a folios de la historia clínica: guarda quién consultó qué formulario clínico de qué paciente, cuándo, desde qué equipo o plataforma y desde qué ubicación geográfica.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'AuditConsultationFolios';
