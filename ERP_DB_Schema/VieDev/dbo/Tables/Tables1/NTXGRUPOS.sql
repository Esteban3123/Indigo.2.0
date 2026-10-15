CREATE TABLE [dbo].[NTXGRUPOS] (
    [ID]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDNTADMINISTRATIVAS] INT NOT NULL,
    [IDNTGRUPOS]          INT NOT NULL,
    CONSTRAINT [PK_NTXGRUPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_NTXGRUPOS_NTADMINISTRATIVAS] FOREIGN KEY ([IDNTADMINISTRATIVAS]) REFERENCES [dbo].[NTADMINISTRATIVAS] ([ID]),
    CONSTRAINT [FK_NTXGRUPOS_NTXGRUPOS] FOREIGN KEY ([IDNTGRUPOS]) REFERENCES [dbo].[NTGRUPOS] ([ID])
);


GO
ALTER TABLE [dbo].[NTXGRUPOS] NOCHECK CONSTRAINT [FK_NTXGRUPOS_NTADMINISTRATIVAS];


GO
ALTER TABLE [dbo].[NTXGRUPOS] NOCHECK CONSTRAINT [FK_NTXGRUPOS_NTXGRUPOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla NTGRUPOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTGRUPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla NTADMINISTRATIVAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'IDNTADMINISTRATIVAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'NTXGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';

