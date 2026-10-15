CREATE TABLE [MixingStation].[TransportationAssistant] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                 VARCHAR (20)  NOT NULL,
    [AssistantType]        TINYINT       NOT NULL,
    [PositionId]           INT           NULL,
    [EmployeeId]           INT           NULL,
    [IdentificationType]   TINYINT       NOT NULL,
    [IdentificationNumber] VARCHAR (20)  NOT NULL,
    [Name]                 VARCHAR (300) NOT NULL,
    [LastName]             VARCHAR (300) NULL,
    [Sex]                  TINYINT       NOT NULL,
    [BirthDate]            DATETIME      NOT NULL,
    [Status]               BIT           NOT NULL,
    [CreationUser]         VARCHAR (20)  NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [ModificationUser]     VARCHAR (20)  NULL,
    [ModificationDate]     DATETIME      NULL,
    [TimeStamp]            ROWVERSION    NOT NULL,
    CONSTRAINT [PK_TransportationAssistant] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransportationAssistant_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_TransportationAssistant_Position] FOREIGN KEY ([PositionId]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [MixingStation].[TransportationAssistant] NOCHECK CONSTRAINT [FK_TransportationAssistant_Employee];


GO
ALTER TABLE [MixingStation].[TransportationAssistant] NOCHECK CONSTRAINT [FK_TransportationAssistant_Position];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo:  1. Masculino  2. Femenino', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TransportationAssistant', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TransportationAssistant', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de auxiliar:  1. Interno  2. Contratado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'TransportationAssistant', @level2type = N'COLUMN', @level2name = N'AssistantType';

