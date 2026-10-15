CREATE TABLE [EHR].[OncologycalSchemeByDay] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT NOT NULL,
    [DayNumber]                   INT NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDay] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDay_OncologycalSchemeByDrugs] FOREIGN KEY ([OncologycalSchemasbyDrugsId]) REFERENCES [EHR].[OncologycalSchemeByDrugs] ([Id])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByDay] NOCHECK CONSTRAINT [FK_OncologycalSchemeByDay_OncologycalSchemeByDrugs];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'DayNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Esquemas Oncológicos Por Drogas Id', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDay', @level2type = N'COLUMN', @level2name = N'Id';

