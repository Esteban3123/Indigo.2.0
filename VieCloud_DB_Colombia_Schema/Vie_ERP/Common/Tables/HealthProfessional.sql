CREATE TABLE [Common].[HealthProfessional] (
    [Id]                        INT          IDENTITY (1, 1) NOT NULL,
    [IdentificationTypeId]      INT          NOT NULL,
    [IdentificationNumber]      VARCHAR (25) NOT NULL,
    [FirstName]                 VARCHAR (50) NOT NULL,
    [SecondName]                VARCHAR (50) NULL,
    [FirstLastName]             VARCHAR (50) NOT NULL,
    [SecondLastName]            VARCHAR (50) NULL,
    [ProfessionalSpecialty]     VARCHAR (3)  NULL,
    [ExternalProfessional]      BIT          NOT NULL,
    [ProfessionalLicenseNumber] VARCHAR (15) NULL,
    [CreationUser]              VARCHAR (20) NOT NULL,
    [CreationDate]              DATETIME     NOT NULL,
    [ModificationUser]          VARCHAR (20) NULL,
    [ModificationDate]          DATETIME     NULL,
    [Timestamp]                 ROWVERSION   NOT NULL,
    [Status]                    BIT          NOT NULL,
    CONSTRAINT [PK_HealthProfessional__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HealthProfessional_ADTIPOIDENTIFICA] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP que registra el instante exacto de creación, modificación o cambio de estado del profesional de salud en el sistema. Auditoría automática de evento.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Timestamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de tarjeta profesional o licencia de ejercicio del profesional de salud. Identificador único de matrícula profesional (VARCHAR 15). PII sensible.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalLicenseNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero tarjeta profesional', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalLicenseNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalLicenseNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano: 1=Profesional externo/contratista, 0=Profesional interno/planta. Define si el profesional de salud es vinculado directo o externo al centro de atención.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ExternalProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional Externo 1- si, 0-No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ExternalProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ExternalProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica del profesional (VARCHAR 3). Referencia a tabla maestra INESPECIA. Ej: Cardiología, Pediatría, Urgencias, Cirugía.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la especialidad Tabla INESPECIA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del profesional de salud: cédula, pasaporte, documento único. Identificador único por tipo. PII ofuscado en búsqueda.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de identificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación: cédula de ciudadanía, pasaporte, licencia extranjera. FK a tabla [dbo].[ADTIPOIDENTIFICA]. Define el documento acreditativo.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion de maestro ADTIPOIDENTIFICA', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de cada profesional de salud en el sistema ERP/EHR Indigo Vie Cloud. Clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de profesionales de la salud (médicos, enfermeros, especialistas y otros). Contiene datos de identificación, nombre completo, especialidad, número de tarjeta profesional y si es externo a la institución.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del profesional de la salud (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del profesional de la salud (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que registró al profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario del sistema que realizó la última modificación al registro del profesional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación al registro del profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo del profesional de la salud en el sistema (1 = activo, 0 = inactivo).', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'HealthProfessional', @level2type = N'COLUMN', @level2name = N'Status';
