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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo ', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de salud', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional sanitario', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'HealthcareProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entre alteraciones', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'AmongAlterations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preguntas', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'Questions';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la tabla TherapeuticRestraints', @level0type = N'SCHEMA', @level0name = N'Nursing', @level1type = N'TABLE', @level1name = N'TrackingTherapeuticRestraints', @level2type = N'COLUMN', @level2name = N'idTherapeuticRestraints';

