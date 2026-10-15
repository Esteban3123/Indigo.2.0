CREATE TABLE [dbo].[OTVALORESGRUPOS] (
    [ID]          INT IDENTITY (1, 1) NOT NULL,
    [IDHCHISPACA] INT NOT NULL,
    [IDGRUPO]     INT NOT NULL,
    [VALOROPCION] INT NOT NULL,
    CONSTRAINT [PK_OTVALORESGRUPOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_OTVALORESGRUPOS_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_OTVALORESGRUPOS_OTGRUPO] FOREIGN KEY ([IDGRUPO]) REFERENCES [dbo].[OTGRUPO] ([ID])
);


GO
ALTER TABLE [dbo].[OTVALORESGRUPOS] NOCHECK CONSTRAINT [FK_OTVALORESGRUPOS_HCHISPACA];


GO
ALTER TABLE [dbo].[OTVALORESGRUPOS] NOCHECK CONSTRAINT [FK_OTVALORESGRUPOS_OTGRUPO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion marcada grupo:  1 -  Refiere  2 - No Refiere', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'VALOROPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del grupo de revision por sistemas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDGRUPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de HC (HCHISPACA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'OTVALORESGRUPOS', @level2type = N'COLUMN', @level2name = N'ID';

