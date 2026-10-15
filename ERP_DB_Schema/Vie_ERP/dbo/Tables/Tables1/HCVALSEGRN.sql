CREATE TABLE [dbo].[HCVALSEGRN] (
    [Id]        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMCONSEC] INT NOT NULL,
    [FOLIOMAMA] INT NOT NULL,
    [FOLIOHIJO] INT NOT NULL,
    CONSTRAINT [PK_HCVALSEGRN] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCVALSEGRN_HCRECINAC] FOREIGN KEY ([NUMCONSEC]) REFERENCES [dbo].[HCRECINAC] ([NUMCONSEC])
);


GO
ALTER TABLE [dbo].[HCVALSEGRN] NOCHECK CONSTRAINT [FK_HCVALSEGRN_HCRECINAC];


GO
CREATE NONCLUSTERED INDEX [IX_HCVALSEGRN_FOLIOMAMA_NUMCONSEC]
    ON [dbo].[HCVALSEGRN]([FOLIOMAMA] ASC, [NUMCONSEC] ASC)
    INCLUDE([FOLIOHIJO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio del recien nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Folio de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'FOLIOMAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'NUMCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCVALSEGRN', @level2type = N'COLUMN', @level2name = N'Id';

