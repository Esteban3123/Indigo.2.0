CREATE TABLE [EHR].[OncologycalSchemeByWeeks] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT NOT NULL,
    [WeekNumber]                  INT NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByWeeks] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByWeeks_OncologycalSchemesByDrugs] FOREIGN KEY ([OncologycalSchemasbyDrugsId]) REFERENCES [EHR].[OncologycalSchemeByDrugs] ([Id])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByWeeks] NOCHECK CONSTRAINT [FK_OncologycalSchemeByWeeks_OncologycalSchemesByDrugs];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero semana', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'WeekNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Esquema Oncológico Por Semanas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByWeeks', @level2type = N'COLUMN', @level2name = N'Id';

