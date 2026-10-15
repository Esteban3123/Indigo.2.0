CREATE TABLE [Common].[PersonDisability] (
    [Id]           INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DisabilityId] INT NOT NULL,
    [PersonId]     INT NOT NULL,
    [Percentage]   INT NOT NULL,
    CONSTRAINT [PK_PersonDisability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonDisability_Disability] FOREIGN KEY ([DisabilityId]) REFERENCES [Common].[Disability] ([Id]),
    CONSTRAINT [FK_PersonDisability_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[PersonDisability] NOCHECK CONSTRAINT [FK_PersonDisability_Disability];


GO
ALTER TABLE [Common].[PersonDisability] NOCHECK CONSTRAINT [FK_PersonDisability_Person];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDisability', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la persona ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDisability', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la discapacidad ', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDisability', @level2type = N'COLUMN', @level2name = N'DisabilityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonDisability', @level2type = N'COLUMN', @level2name = N'Id';

