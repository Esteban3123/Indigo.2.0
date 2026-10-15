CREATE TABLE [EHR].[OncologycalSchemeByWeeksPrescription] (
    [Id]                          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OncologycalSchemasbyDrugsId] INT       NOT NULL,
    [CODPRODUC]                   CHAR (20) NOT NULL,
    [WeekNumber]                  INT       NOT NULL,
    CONSTRAINT [PK_OncologycalSchemeByWeeksPrescription] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OncologycalSchemeByWeeksPrescription_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [EHR].[OncologycalSchemeByWeeksPrescription] NOCHECK CONSTRAINT [FK_OncologycalSchemeByWeeksPrescription_IHLISTPRO];

