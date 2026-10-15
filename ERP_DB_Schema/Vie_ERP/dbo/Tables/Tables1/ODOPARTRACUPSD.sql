CREATE TABLE [dbo].[ODOPARTRACUPSD] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODOPARTRA] INT       NOT NULL,
    [CODSERIPS]   CHAR (20) NOT NULL,
    CONSTRAINT [PK_ODOPARTRACUPSD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODOPARTRACUPSD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ODOPARTRACUPSD_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);


GO
ALTER TABLE [dbo].[ODOPARTRACUPSD] NOCHECK CONSTRAINT [FK_ODOPARTRACUPSD_INCUPSIPS];


GO
ALTER TABLE [dbo].[ODOPARTRACUPSD] NOCHECK CONSTRAINT [FK_ODOPARTRACUPSD_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODOPARTRACUPSD', @level2type = N'COLUMN', @level2name = N'ID';

