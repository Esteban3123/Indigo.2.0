CREATE TABLE [Common].[Person] (
    [Id]                           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdentificationNumber]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IdentificationType]           INT                                                                              NOT NULL,
    [IdentificacionCityId]         INT                                                                              NULL,
    [IdentificationExpeditionDate] DATE                                                                             NULL,
    [MilitaryCardId]               INT                                                                              NULL,
    [MilitaryCardNumber]           VARCHAR (15) MASKED WITH (FUNCTION = 'partial(0, "MilitaryCard_Ofuscado", 0)')   NULL,
    [FirstName]                    VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')      NULL,
    [SecondName]                   VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')     NULL,
    [FirstLastName]                VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')   NULL,
    [SecondLastName]               VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')  NULL,
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
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[IdentificationNumber]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[MilitaryCardNumber]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[FirstName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[SecondName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[FirstLastName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[SecondLastName]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [Common].[Person].[BirthDate]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de identificación desde tabla [dbo].[ADTIPOIDENTIFICA], INT NULL, FK, nuevo campo, reemplaza IdentificationType numérico.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de identificacion, de la tabla ADTIPOIDENTIFICA Este es el nuevo campo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla de calzado, INT NULL, medida antropométrica complementaria.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShoeSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla de Calzado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShoeSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShoeSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla de pantalón, INT NULL, medida antropométrica de vestuario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'PantSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla del Pantalón', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'PantSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'PantSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Talla de camisa: 0=XS, 1=S, 2=M, 3=L, 4=XL, 5=XXL, VARCHAR(6) NULL, medida de vestuario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShirtSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tallas de camisa "0-XS", “1-S”, “2-M”, “3-L”, "4-XL", "5-XXL"', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShirtSize';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ShirtSize';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso corporal del paciente en kilogramos, INT NULL, dato antropométrico clínico, cálculo IMC.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso de la Persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Weight';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Weight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de creencias religiosas del paciente, FK a [Payroll].[ReligiousBeliefs], INT NULL, información cultural sanitaria.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ReligiousBeliefsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las creencias religiosa de la persona.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ReligiousBeliefsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'ReligiousBeliefsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo étnico, FK a [Payroll].[EthnicGroups], INT NULL, clasificación étnica demográfica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'EthnicGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo etnico', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'EthnicGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'EthnicGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Práctica de deporte: 1=Sí, 0=No, BIT NULL, actividad física, estilo vida del paciente.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SportPractice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Practica Deporte:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SportPractice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SportPractice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consumo de cigarrillo: 1=Sí, 0=No, BIT NULL, factor riesgo tabaquismo para antecedentes clínicos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'CigaretteConsumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consume Cigarrillo:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'CigaretteConsumption';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'CigaretteConsumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato socioeconómico: 1=Bajo-bajo, 2=Bajo, 3=Medio-bajo, 4=Medio, 5=Medio-alto, 6=Alto, TINYINT NULL, clasificación para cobro/subsidios.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SocioEconomicStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Socio Económico:  1. Bajo - bajo  2. Bajo  3. Medio-bajo  4. Medio  5. Medio-alto  6. Alto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SocioEconomicStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SocioEconomicStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de vivienda: 1=Propia, 2=Arriendo, 3=Familiar, TINYINT NULL, indicador estabilidad sociohabitat.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'HousingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Vivienda  1. Propia  2. Arriendo  3. Familiar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'HousingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'HousingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro: 1=Activo, 0=Inactivo, BIT NOT NULL, control vigencia paciente en el sistema.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil: 0=Soltero, 1=Casado, 2=Divorciado, 3=Viudo, 4=Unión libre, 5=Separado, TINYINT NULL, información familiar.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MaritalStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado civil   ****** 0-soltero 1-casado   2-divorciado 3-viudo   4-union libre,   5-Separado  ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MaritalStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MaritalStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de personas a cargo o dependientes económicos, TINYINT NULL, indicador vulnerabilidad social.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Dependents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Personas a cargo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Dependents';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Dependents';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de hijos del paciente, TINYINT NULL, composición familiar para evaluación socioeconómica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SonNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de hijos', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SonNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SonNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar o datos biométricos del paciente, VARBINARY(MAX) NULL, identificación biométrica, seguridad.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Huella dactilar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Fingerprint';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh sanguíneo: 0=Negativo(-), 1=Positivo(+), CHAR(1) NULL, complemento grupo sanguíneo, hemoterapia.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'RH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH  0- Negativo(-),  1 - Positivo (+)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'RH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'RH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo: 0=O, 1=A, 2=B, 3=AB, VARCHAR(2) NULL, dato clínico crítico para transfusiones.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo sanguineo   0 - O,   1 - A,   2 - B,   3 - AB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BloodGroup';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género/sexo: 1=Masculino, 2=Femenino, 3=Otro, TINYINT NULL, clasificación demográfica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1- Masculino, 2 - Femenino, 3 - Otro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Gender';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fallecimiento, DATE NULL, indicador estado vital del paciente, registros históricos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'DeathDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de fallecimiento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'DeathDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'DeathDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad de nacimiento, FK a [Common].[City], INT NULL, referencia geográfica de origen.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthCityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad de nacimiento FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthCityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthCityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (PII enmascarado), DATE NULL, cálculo edad, validación demográfica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BirthDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (PII enmascarado), VARCHAR(50) NULL, completitud de identidad civil.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Apellido', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido o apellido principal (PII enmascarado), VARCHAR(50) NULL, obligatorio para personas naturales, búsqueda crítica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido, es obligatorio solo para  personas naturalez', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstLastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (PII enmascarado), VARCHAR(50) NULL, complemento identidad para búsqueda semántica.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo nombre', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SecondName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (PII enmascarado), VARCHAR(50) NULL, obligatorio para personas naturales, búsqueda por nombre.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Nombre, es obligatorio solo para  personas naturalez', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de libreta militar (PII enmascarado), VARCHAR(15) NULL, documento de identificación militar adicional.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de libreta militar ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de libreta militar expedida: 1=primera, 2=segunda, INT NULL, TINYINT, documento militar complementario.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de libreta militar. 1- primera 2- Segunda', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MilitaryCardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de expedición o emisión del documento de identificación, DATE NULL, para validar vigencia documental.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationExpeditionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de expedicion del documento', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationExpeditionDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationExpeditionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad donde se expidió el documento de identificación, FK a [Common].[City], INT NULL, referencia geográfica administrativa.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificacionCityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ciudad de expedicion del documento FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificacionCityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificacionCityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación: CC (cédula ciudadanía), CE (cédula extranjería), TI (tarjeta identidad), RC (registro civil), PA (pasaporte), AS/MS (sin identificación), NIT, pasaporte diplomático, salvoconducto, permisos migratorios, documento extranjero, INT.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)  13 = Permiso por Protección Temporal (PT)  14 = Documento extranjero (DE)  15 = Sin identificacion (SI)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación del paciente, cédula, documento o equivalente (PII enmascarado), VARCHAR(25), UNIQUE, obligatorio para búsqueda de pacientes.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de identificacion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la persona en el sistema, INT IDENTITY, clave primaria de la tabla Person.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la tabla persona', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro maestro de personas naturales del sistema. Centraliza la identidad, datos demográficos, características físicas y socioeconómicas de cualquier persona (paciente, profesional, contacto) que interactúa con el ERP/EHR.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person';
