CREATE TABLE [HumanTalent].[Candidate] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdentificationNumber] VARCHAR (25)  NOT NULL,
    [DocumentType]         INT           NOT NULL,
    [DocumentDate]         DATE          NOT NULL,
    [CityDocumentId]       INT           NOT NULL,
    [FirstName]            VARCHAR (20)  NOT NULL,
    [SecondName]           VARCHAR (20)  NULL,
    [FirstLastName]        VARCHAR (20)  NOT NULL,
    [SecondLastName]       VARCHAR (20)  NULL,
    [DateBirth]            DATE          NOT NULL,
    [CityBirthId]          INT           NOT NULL,
    [NationalityId]        INT           NOT NULL,
    [Age]                  INT           NOT NULL,
    [BloodType]            VARCHAR (2)   NOT NULL,
    [Rh]                   CHAR (1)      NOT NULL,
    [Gender]               INT           NOT NULL,
    [CivilStatus]          INT           NOT NULL,
    [Socioeconomic]        INT           NOT NULL,
    [MilitaryType]         INT           NULL,
    [NoMilitary]           VARCHAR (25)  NULL,
    [Addres]               VARCHAR (50)  NOT NULL,
    [Neighborhood]         VARCHAR (80)  NOT NULL,
    [CityResidenceId]      INT           NOT NULL,
    [Phone]                VARCHAR (20)  NULL,
    [CellPhone]            VARCHAR (20)  NOT NULL,
    [Email]                VARCHAR (50)  NOT NULL,
    [ContacPersonName]     VARCHAR (40)  NOT NULL,
    [KinshipId]            INT           NOT NULL,
    [PhoneContactPerson]   VARCHAR (20)  NOT NULL,
    [State]                INT           NULL,
    [CreationUser]         VARCHAR (50)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [Image]                VARCHAR (100) NULL,
    CONSTRAINT [PK__Candidat__3214EC07C2BFD675] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CityBirth] FOREIGN KEY ([NationalityId]) REFERENCES [Common].[Country] ([Id]),
    CONSTRAINT [fk_CityBirthId] FOREIGN KEY ([CityBirthId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [fk_CityDocumentId] FOREIGN KEY ([CityDocumentId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [fk_CityResidenceId] FOREIGN KEY ([CityResidenceId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [fk_KinshipId] FOREIGN KEY ([KinshipId]) REFERENCES [Payroll].[Kinship] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografía o imagen del candidato, almacenada como ruta de archivo VARCHAR(100)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Image';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imagen', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Image';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Image';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro del candidato, DATETIME', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación del registro, VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del candidato, DATETIME', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del candidato, VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del candidato: 1=Disponible (sin proceso asignado), 2=En proceso (ligado a selección), 3=Inhabilitado (descalificado). INT', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Disponible (cuando se crea y/o no está ligado a ningún proceso), 2-  En proceso (cuando está ligado a un proceso de selección), 3- Inhabilitado (cuando se ha seleccionado la opción de Inhabilidad).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto de la persona de referencia o emergencia, VARCHAR(20)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'PhoneContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono de contacto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'PhoneContactPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'PhoneContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del parentesco/relación con la persona de contacto, FK a [Payroll].[Kinship]', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'KinshipId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Parentesco', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'KinshipId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'KinshipId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona de contacto o referencia del candidato, VARCHAR(40)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ContacPersonName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la persona de contacto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ContacPersonName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ContacPersonName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del candidato, dirección de contacto principal, VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'correo electrónico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono celular del candidato, VARCHAR(20) requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CellPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Telefono celular', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CellPhone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CellPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono fijo del candidato, VARCHAR(20) opcional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad de residencia actual, FK a [Common].[City]', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityResidenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Residencia en la ciudad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityResidenceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityResidenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Barrio, vereda o localidad de residencia del candidato, VARCHAR(80)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Neighborhood';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vecindario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Neighborhood';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Neighborhood';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección completa de residencia del candidato, VARCHAR(50)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Addres';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Addres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de carnet militar o licencia militar del candidato, VARCHAR(25) opcional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NoMilitary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número Militar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NoMilitary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NoMilitary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Categoría de servicio militar (prestado, exonerado, etc.), INT opcional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'MilitaryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo militar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'MilitaryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'MilitaryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato socioeconómico o nivel de ingresos del candidato, INT', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Socioeconomic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Socio econócimo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Socioeconomic';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Socioeconomic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil del candidato (soltero, casado, viudo, divorciado), INT', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado civil', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CivilStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género del candidato (masculino, femenino, otro), INT', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Género', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh del grupo sanguíneo (+ o -), CHAR(1)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Rh';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo sanguíneo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Rh';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Rh';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de sangre del candidato (O, A, B, AB), VARCHAR(2)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'BloodType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de sangre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'BloodType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'BloodType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad calculada del candidato en años, INT', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Age';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la nacionalidad/país de origen, FK a [Common].[Country]', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NationalityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la nacionalidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NationalityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NationalityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad de nacimiento, FK a [Common].[City]', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityBirthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Ciudad natal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityBirthId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityBirthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del candidato, DATE requerida', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DateBirth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DateBirth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DateBirth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del candidato, VARCHAR(20) opcional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del candidato, VARCHAR(20) requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Apellido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del candidato, VARCHAR(20) opcional', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del candidato, VARCHAR(20) requerido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer nombre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ciudad donde se expidió el documento de identificación, FK a [Common].[City]', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Documento de la ciudad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición del documento de identificación, DATE requerida', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento (CC=Cédula, CE=Extranjería, TI=Tarjeta Identidad, RC=Registro Civil, PA=Pasaporte, etc.), INT de 0-12', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de documento  0  = CC
																					 1	= CE
																					 2	= TI
																					 3	= RC
																					 4	= PA	 
																					 5	= AS
																					 6	= MS
																					 7	= NIT
																					 8	= NU
																					 9	= CN
																					 10	= CD
																					 11	= SC
																					 12	= PE ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de identificación, cédula o documento del candidato. PII Identificación_Ofuscado, VARCHAR(25)', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable del candidato, INT IDENTITY(1,1) PRIMARY KEY', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de candidatos o aspirantes al proceso de selección de talento humano. Contiene la información personal, de identificación, de contacto y datos sociodemográficos de cada persona que aplica a una vacante en la organización.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate';
