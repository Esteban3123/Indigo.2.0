CREATE TABLE [Common].[PersonProfession] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdProfession]  TINYINT NOT NULL,
    [PersonId]      INT     NOT NULL,
    [IsSupported]   BIT     NOT NULL,
    [PersonStudyId] INT     NULL,
    [State]         BIT     NOT NULL,
    CONSTRAINT [PK_ProfessionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonProfession_PersonStudy] FOREIGN KEY ([PersonStudyId]) REFERENCES [Common].[PersonStudy] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Employee] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ProfessionDetail_Profession] FOREIGN KEY ([IdProfession]) REFERENCES [Payroll].[Profession] ([Id])
);


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_PersonProfession_PersonStudy];


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_ProfessionDetail_Employee];


GO
ALTER TABLE [Common].[PersonProfession] NOCHECK CONSTRAINT [FK_ProfessionDetail_Profession];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-activo 0-inactivo', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del estudio que soporta la profesion', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonStudyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'profesion soportado por un estudio (SI/NO)', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IsSupported';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Profession FK', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'IdProfession';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonProfession', @level2type = N'COLUMN', @level2name = N'Id';

