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
ALTER TABLE [HumanTalent].[Candidate] NOCHECK CONSTRAINT [fk_CityBirth];


GO
ALTER TABLE [HumanTalent].[Candidate] NOCHECK CONSTRAINT [fk_CityBirthId];


GO
ALTER TABLE [HumanTalent].[Candidate] NOCHECK CONSTRAINT [fk_CityDocumentId];


GO
ALTER TABLE [HumanTalent].[Candidate] NOCHECK CONSTRAINT [fk_CityResidenceId];


GO
ALTER TABLE [HumanTalent].[Candidate] NOCHECK CONSTRAINT [fk_KinshipId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imagen', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Image';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de modificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que modifico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de creación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Disponible (cuando se crea y/o no está ligado a ningún proceso), 2-  En proceso (cuando está ligado a un proceso de selección), 3- Inhabilitado (cuando se ha seleccionado la opción de Inhabilidad).', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono de contacto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'PhoneContactPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Parentesco', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'KinshipId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la persona de contacto', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'ContacPersonName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'correo electrónico', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Telefono celular', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CellPhone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Residencia en la ciudad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityResidenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vecindario', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Neighborhood';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Addres';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número Militar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NoMilitary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo militar', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'MilitaryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado Socio econócimo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Socioeconomic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CivilStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Rh';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de sangre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'BloodType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Age';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la nacionalidad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'NationalityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Ciudad natal', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityBirthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DateBirth';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre Apellido', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo Nombre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'SecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Documento de la ciudad', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'CityDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento  0  = CC
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'Candidate', @level2type = N'COLUMN', @level2name = N'Id';

