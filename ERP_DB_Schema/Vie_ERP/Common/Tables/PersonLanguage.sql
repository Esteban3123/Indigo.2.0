CREATE TABLE [Common].[PersonLanguage] (
    [Id]            INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PersonId]      INT     NOT NULL,
    [LanguageId]    INT     NULL,
    [LanguageLevel] TINYINT NOT NULL,
    CONSTRAINT [PK_EmployeeLanguage] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__PersonLan__Language] FOREIGN KEY ([LanguageId]) REFERENCES [Payroll].[Language] ([Id]),
    CONSTRAINT [FK_EmployeeLanguage_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id])
);


GO
ALTER TABLE [Common].[PersonLanguage] NOCHECK CONSTRAINT [FK__PersonLan__Language];


GO
ALTER TABLE [Common].[PersonLanguage] NOCHECK CONSTRAINT [FK_EmployeeLanguage_Person];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de lenguaje 1 - Bajo 2 - Medio 3 - Alto', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Lenguaje', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'LanguageId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id Empleado', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'PersonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'PersonLanguage', @level2type = N'COLUMN', @level2name = N'Id';

