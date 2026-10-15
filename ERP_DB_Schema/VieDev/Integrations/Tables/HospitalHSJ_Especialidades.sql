CREATE TABLE [Integrations].[HospitalHSJ_Especialidades] (
    [LegacyCode] VARCHAR (200) NOT NULL,
    [LegacyName] VARCHAR (200) NOT NULL,
    [IndigoCode] VARCHAR (200) NOT NULL,
    [IndigoName] VARCHAR (200) NOT NULL,
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HospitalHSJ_Especialidades] PRIMARY KEY CLUSTERED ([Id] ASC)
);

