CREATE TABLE [Payroll].[Relationship] (
    [Id]                   INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]           INT            NOT NULL,
    [Name]                 VARCHAR (100)  NOT NULL,
    [KinshipId]            INT            NOT NULL,
    [BirthDate]            DATE           NULL,
    [ProvidesUPC]          BIT            NOT NULL,
    [UPCValue]             NUMERIC (18)   NOT NULL,
    [Dependent]            BIT            NOT NULL,
    [DependsType]          TINYINT        NULL,
    [DependsValue]         NUMERIC (18)   NULL,
    [DependsPercentage]    DECIMAL (5, 2) NULL,
    [State]                NCHAR (10)     NULL,
    [IsEmergencyContact]   BIT            NULL,
    [PhoneNumber]          NVARCHAR (20)  NULL,
    [Gender]               TINYINT        NULL,
    [IdentificationType]   INT            NULL,
    [IdentificationNumber] VARCHAR (50)   NULL,
    CONSTRAINT [PK_Kinship] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Kinship_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Relationship_Kinship] FOREIGN KEY ([KinshipId]) REFERENCES [Payroll].[Kinship] ([Id])
);


GO
ALTER TABLE [Payroll].[Relationship] NOCHECK CONSTRAINT [FK_Kinship_Employee];


GO
ALTER TABLE [Payroll].[Relationship] NOCHECK CONSTRAINT [FK_Relationship_Kinship];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de de identificacion', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de identificacion   0=Cédula de Ciudadanía (CC)  1=Cédula de Extranjería (CE)  2=Tarjeta de Identidad (TI)  3=Registro Civil (RC)  4=Pasaporte (PA)  5=Adulto Sin Identificación (AS)  6=Menor Sin Identificación (MS)  7= Nit (NI)  8= Número único de identificación personal (NU)  9= Cetrigicado Nacido Vivo (CN)  10= Carnet Diplomático (CD)  11= Salvoconducto (SC)  12 = Permiso especial de Permanencia (PE)  13 = Permiso por Protección Temporal (PT)  14 = Documento extranjero (DE)  15 = Sin identificacion (SI)', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿Es contacto de emergencia? | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'Relationship', @level2type = N'COLUMN', @level2name = N'IsEmergencyContact';

