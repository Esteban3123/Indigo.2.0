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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de identificacion, de la tabla ADTIPOIDENTIFICA Este es el nuevo campo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Practica Deporte:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SportPractice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consume Cigarrillo:  1. Si  0. No', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'CigaretteConsumption';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Socio Económico:  1. Bajo - bajo  2. Bajo  3. Medio-bajo  4. Medio  5. Medio-alto  6. Alto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'SocioEconomicStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Vivienda  1. Propia  2. Arriendo  3. Familiar', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'HousingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado civil   ****** 0-soltero 1-casado   2-divorciado 3-viudo   4-union libre,   5-Separado  ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'MaritalStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH  0- Negativo(-),  1 - Positivo (+)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'RH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo sanguineo   0 - O,   1 - A,   2 - B,   3 - AB', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'BloodGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)  13 = Permiso por Protección Temporal (PT)  14 = Documento extranjero (DE)  15 = Sin identificacion (SI)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'Person', @level2type = N'COLUMN', @level2name = N'IdentificationType';

