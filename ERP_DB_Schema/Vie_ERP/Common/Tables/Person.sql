CREATE TABLE [Common].[Person] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdentificationNumber]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IdentificationType]           INT                                                                              NOT NULL,
    [IdentificacionCityId]         INT                                                                              NULL,
    [IdentificationExpeditionDate] DATE                                                                             NULL,
    [MilitaryCardId]               INT                                                                              NULL,
    [MilitaryCardNumber]           VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "MilitaryCard_Ofuscado", 0)')   NULL,
    [FirstName]                    VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')      NULL,
    [SecondName]                   VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')     NULL,
    [FirstLastName]                VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NULL,
    [SecondLastName]               VARCHAR (50) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NULL,
    [BirthDate]                    DATE MASKED WITH (FUNCTION = 'default()')                                        NULL,
    [BirthCityId]                  INT                                                                              NULL,
    [DeathDate]                    DATE                                                                             NULL,
    [Gender]                       TINYINT                                                                          NULL,
    [BloodGroup]                   VARCHAR (2)                                                                      NULL,
    [RH]                           CHAR (1)                                                                         NULL,
    [Fingerprint]                  VARBINARY (MAX)                                                                  NULL,
    [SonNumber]                    TINYINT                                                                          NULL,
    [Dependents]                   TINYINT                                                                          NULL,
    [MaritalStatus]                TINYINT                                                                          NULL,
    [State]                        BIT                                                                              NOT NULL,
    [HousingType]                  TINYINT                                                                          NULL,
    [SocioEconomicStatus]          TINYINT                                                                          NULL,
    [CigaretteConsumption]         BIT                                                                              NULL,
    [SportPractice]                BIT                                                                              NULL,
    [EthnicGroupId]                INT                                                                              NULL,
    [ReligiousBeliefsId]           INT                                                                              NULL,
    [Weight]                       INT                                                                              NULL,
    [ShirtSize]                    VARCHAR (6)                                                                      NULL,
    [PantSize]                     INT                                                                              NULL,
    [ShoeSize]                     INT                                                                              NULL,
    [IdentificationTypeId]         INT                                                                              NULL,
    CONSTRAINT [PK_Person__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Person_ADTIPOIDENTIFICA] FOREIGN KEY ([IdentificationTypeId]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID]),
    CONSTRAINT [FK_Person_City_BirthCity] FOREIGN KEY ([BirthCityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Person_City_IdentificationCity] FOREIGN KEY ([IdentificacionCityId]) REFERENCES [Common].[City] ([Id]),
    CONSTRAINT [FK_Person_EthnicGroups] FOREIGN KEY ([EthnicGroupId]) REFERENCES [Payroll].[EthnicGroups] ([Id]),
    CONSTRAINT [FK_Person_ReligiousBeliefs] FOREIGN KEY ([ReligiousBeliefsId]) REFERENCES [Payroll].[ReligiousBeliefs] ([Id]),
    CONSTRAINT [UQ_Person__IdentificationNumber] UNIQUE NONCLUSTERED ([IdentificationNumber] ASC)
);


GO
ALTER TABLE [Common].[Person] NOCHECK CONSTRAINT [FK_Person_ADTIPOIDENTIFICA];


GO
ALTER TABLE [Common].[Person] NOCHECK CONSTRAINT [FK_Person_City_BirthCity];


GO
ALTER TABLE [Common].[Person] NOCHECK CONSTRAINT [FK_Person_City_IdentificationCity];


GO
ALTER TABLE [Common].[Person] NOCHECK CONSTRAINT [FK_Person_EthnicGroups];


GO
ALTER TABLE [Common].[Person] NOCHECK CONSTRAINT [FK_Person_ReligiousBeliefs];


GO
CREATE TRIGGER [Common].[tgUpdatePerson]
   ON  [Common].[Person]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	DECLARE @TableSchema VARCHAR(250) = 'Common'
	DECLARE @TableName VARCHAR(250) = 'Person'

    IF UPDATE(IdentificationType)
	BEGIN
		INSERT INTO [Common].[Log]
           ([TableSchema], [TableName], [Key], [ColumnName], [OldValue], [NewValue], [UpdatedBy], [UpdatedDate])
		SELECT @TableSchema, @TableName, i.Id, 'IdentificationType', d.IdentificationType, i.IdentificationType, NULL, GETDATE()
		FROM INSERTED i
		JOIN DELETED d ON i.Id = d.Id
		WHERE ISNULL(d.IdentificationType, '') <> ISNULL(i.IdentificationType, '')
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de identificacion, de la tabla ADTIPOIDENTIFICA Este es el nuevo campo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla de Calzado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShoeSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla del Pantalón', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'PantSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tallas de camisa "0-XS", “1-S”, “2-M”, “3-L”, "4-XL", "5-XXL"', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShirtSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de las creencias religiosa de la persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ReligiousBeliefsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo etnico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'EthnicGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Practica Deporte:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SportPractice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consume Cigarrillo:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'CigaretteConsumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Socio Económico:  1. Bajo - bajo  2. Bajo  3. Medio-bajo  4. Medio  5. Medio-alto  6. Alto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SocioEconomicStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vivienda  1. Propia  2. Arriendo  3. Familiar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'HousingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado civil   ****** 0-soltero 1-casado   2-divorciado 3-viudo   4-union libre,   5-Separado  ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MaritalStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Personas a cargo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Dependents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de hijos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SonNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Huella dactilar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH  0- Negativo(-),  1 - Positivo (+)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'RH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo sanguineo   0 - O,   1 - A,   2 - B,   3 - AB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Masculino, 2 - Femenino, 3 - Otro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de fallecimiento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'DeathDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciudad de nacimiento FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthCityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido, es obligatorio solo para  personas naturalez', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo nombre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Nombre, es obligatorio solo para  personas naturalez', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de libreta militar ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de libreta militar. 1- primera 2- Segunda', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de expedicion del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationExpeditionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciudad de expedicion del documento FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificacionCityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)  13 = Permiso por Protección Temporal (PT)  14 = Documento extranjero (DE)  15 = Sin identificacion (SI)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de identificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la tabla persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';

