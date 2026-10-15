CREATE TABLE [dbo].[EXAVALORESGRUPOS] (
    [ID]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA] INT NOT NULL,
    [IDEXAGRUPO]  INT NOT NULL,
    [VALOROPCION] INT NOT NULL,
    CONSTRAINT [PK_EXAVALORESGRUPO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EXAVALORESGRUPO_EXAGRUPO] FOREIGN KEY ([IDEXAGRUPO]) REFERENCES [dbo].[EXAGRUPO] ([ID]),
    CONSTRAINT [FK_EXAVALORESGRUPO_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID])
);


GO
ALTER TABLE [dbo].[EXAVALORESGRUPOS] NOCHECK CONSTRAINT [FK_EXAVALORESGRUPO_EXAGRUPO];


GO
ALTER TABLE [dbo].[EXAVALORESGRUPOS] NOCHECK CONSTRAINT [FK_EXAVALORESGRUPO_HCHISPACA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Normal  2 - Anormal  3 - No Valorado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID grupo de examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDEXAGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID HC (HCHISPACA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'EXAVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';

