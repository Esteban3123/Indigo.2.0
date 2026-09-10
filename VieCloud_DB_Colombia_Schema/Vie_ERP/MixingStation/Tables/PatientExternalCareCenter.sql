CREATE TABLE [MixingStation].[PatientExternalCareCenter] (
    [Id]                     INT                                                                         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdentificationNumber]   VARCHAR (25)                                                                NOT NULL,
    [IdentificationTypeId]   INT                                                                         NOT NULL,
    [Name]                   VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')     NOT NULL,
    [LastName]               VARCHAR (300) MASKED WITH (FUNCTION = 'partial(0, "LastName_Ofuscado", 0)') NULL,
    [GenderTypeId]           INT                                                                         NOT NULL,
    [Status]                 BIT                                                                         NOT NULL,
    [CreationUser]           VARCHAR (20)                                                                NOT NULL,
    [CreationDate]           DATETIME                                                                    NOT NULL,
    [ModificationUser]       VARCHAR (20)                                                                NULL,
    [ModificationDate]       DATETIME                                                                    NULL,
    [TimeStamp]              ROWVERSION                                                                  NOT NULL,
    [PatientMobileNumber]    VARCHAR (10)                                                                NULL,
    [PatientEmail]           VARCHAR (50)                                                                NULL,
    [ExternalFunctionalUnit] VARCHAR (100)                                                               NULL,
    [PatientBed]             VARCHAR (50)                                                                NULL,
    CONSTRAINT [PK_PatientExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PatientExternalCareCenter_GenderTypes] FOREIGN KEY ([GenderTypeId]) REFERENCES [Admissions].[GenderTypes] ([Id]),
    CONSTRAINT [FK_PatientExternalCareCenter_IdentificationType] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MixingStation].[PatientExternalCareCenter].[Name]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [MixingStation].[PatientExternalCareCenter].[LastName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cama del paciente en centro de atención externo. Identificador de ubicación física dentro de unidad funcional externa. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientBed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cama del paciente del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientBed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientBed';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad Funcional del centro de atención externo. Departamento o servicio clínico de origen externo. VARCHAR(100).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad Funcional del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del paciente del centro de atención externo. Contacto PII para comunicaciones. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientEmail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico del paciente del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientEmail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientEmail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número celular o teléfono del paciente del centro de atención externo. Contacto PII para notificaciones. VARCHAR(10).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientMobileNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de celular o teléfono del paciente del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientMobileNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'PatientMobileNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP de creación, registro o modificación del registro. Controla concurrencia y auditoría.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro. DATETIME, nulo si sin cambios posteriores a creación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que realizó última modificación. VARCHAR(20), auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del paciente. DATETIME, marca origen del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del usuario que creó el registro. VARCHAR(20), trazabilidad inicial.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 0=Inactivo. BIT, controla disponibilidad del paciente externo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de género/sexo: 1=Masculino, 2=Femenino. INT FK a [Admissions].[GenderTypes].', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'GenderTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo:  1. Masculino  2. Femenino', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'GenderTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'GenderTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apellido(s) del paciente externo. VARCHAR(300) MASKED (PII_Ofuscado). Nulo si no aplica.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'LastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apellidos del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'LastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'LastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre(s) del paciente externo. VARCHAR(300) MASKED (PII_Ofuscado). Dato identificatorio.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del paciente: CC, CE, TI, RC, PA, AS, MS, NI, NU, CN, CD, SC, PE. INT FK a [dbo].[ADTIPOIDENTIFICA].', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o código de identificación del paciente (cédula, pasaporte, documento). VARCHAR(25), PII.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'No. de identificación del paciente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de paciente del centro de atención externo. INT IDENTITY, clave primaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de pacientes provenientes de centros de atención externos (otras IPS o instituciones). Contiene datos de identificación, nombre, género, cama asignada y unidad funcional externa para pacientes referidos o remitidos desde afuera de la red propia.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'PatientExternalCareCenter';
