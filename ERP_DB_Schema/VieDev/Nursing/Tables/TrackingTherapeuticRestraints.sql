CREATE TABLE [Nursing].[TrackingTherapeuticRestraints] (
    [idTherapeuticRestraints] INT           NOT NULL,
    [RegistrationDate]        DATETIME      NOT NULL,
    [Questions]               VARCHAR (MAX) NOT NULL,
    [AmongAlterations]        INT           NOT NULL,
    [HealthcareProfessional]  VARCHAR (25)  NOT NULL,
    [Observation]             VARCHAR (300) NULL,
    [HealthUnit]              VARCHAR (10)  NOT NULL,
    [Id]                      INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TrackingTherapeuticRestraints] PRIMARY KEY CLUSTERED ([Id] ASC)
);

