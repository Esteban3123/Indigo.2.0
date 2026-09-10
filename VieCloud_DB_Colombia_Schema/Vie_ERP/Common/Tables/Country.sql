CREATE TABLE [Common].[Country] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                VARCHAR (2)  NOT NULL,
    [Name]                VARCHAR (80) NOT NULL,
    [Nationality]         VARCHAR (50) NULL,
    [State]               BIT          NOT NULL,
    [CreationUser]        VARCHAR (20) CONSTRAINT [DF_Country_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]        DATETIME     CONSTRAINT [DF_Country_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]    VARCHAR (20) NULL,
    [ModificationDate]    DATETIME     NULL,
    [TimeStamp]           ROWVERSION   NOT NULL,
    [StandardCode]        VARCHAR (3)  CONSTRAINT [DF_Country_StandardCode] DEFAULT ((0)) NOT NULL,
    [StandardCodeNumeric] VARCHAR (3)  NULL,
    CONSTRAINT [PK_Country__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Country__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código estándar ISO 3166-1 numérico del país (3 dígitos), utilizado para identificación de pacientes extranjeros y nacionalidad en registros demográficos; VARCHAR(3), nullable.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCodeNumeric';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me identifica el codigo estandar ISO 3166-1 (numérico) de los paises de acuerdo a la nacionalidad para los pacientes extranjeros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCodeNumeric';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCodeNumeric';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código estándar ISO 3166-1 alfa-2 del país (2 caracteres), identificador internacional de nacionalidad para pacientes extranjeros y registros administrativos; VARCHAR(2), requerido, único.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código estandard, me identifica el codigo estandar ISO 3166-1 (alfa-2) de los paises de acuerdo a la nacionalidad para los pacientes extranjeros', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'StandardCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática de auditoría que registra el instante exacto de creación, modificación o transacción en la tabla; se actualiza en cada cambio de registro, tipo TIMESTAMP SQL Server.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del país; DATETIME, nullable, permite rastrear cambios en información demográfica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificador que realizó la última modificación del registro del país; VARCHAR(20), nullable, para auditoría y trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del país en el sistema; DATETIME, requerida, genera timestamp automático al insertar.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificador que creó el registro del país en el sistema; VARCHAR(20), requerido, por defecto toma valor 999, para auditoría inicial.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de vigencia del país (1=Activo, 0=Inactivo); BIT, controla disponibilidad para selección en registros de pacientes extranjeros y nacionalidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Pais 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Denominación oficial de la nacionalidad o gentilicio del país (ej: Colombiano, Venezolano); VARCHAR(50), nullable, para etiquetado en documentos y certificados.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Nationality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nacionalidad', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Nationality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Nationality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre oficial o denominación completa del país; VARCHAR(80), requerido, utilizado en registros demográficos, pacientes extranjeros y reportes administrativos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Pais', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único de dos caracteres que identifica el país (ISO 3166-1 alfa-2, ej: CO, VE, EC); VARCHAR(2), requerido, clave única.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Pais', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) del país en el catálogo maestro; clave primaria, no se replica, permite referencias desde tablas de pacientes y nacionalidad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de los  Paises', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de países del sistema. Guarda los países disponibles para clasificar pacientes, documentos y registros según su origen o nacionalidad, incluyendo códigos internacionales ISO.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Country';
