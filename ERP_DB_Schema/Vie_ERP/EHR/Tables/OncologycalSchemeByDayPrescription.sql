CREATE TABLE [EHR].[OncologycalSchemeByDayPrescription] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT       NOT NULL,
    [CODPRODUC]                   CHAR (20) NOT NULL,
    [DayNumber]                   INT       NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByDayPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByDayPrescription_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByDayPrescription] NOCHECK CONSTRAINT [FK_OncologycalSchemeByDayPrescription_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el numero de dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'DayNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo produción', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'OncologycalSchemasbyDrugsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'OncologycalSchemeByDayPrescription', @level2type = N'COLUMN', @level2name = N'Id';

