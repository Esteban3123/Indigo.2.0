CREATE TABLE [dbo].[AGACTIMDDIAG] (
    [ID]        INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED] CHAR (3)  NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    CONSTRAINT [PK_AGACTIMDDIAG] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGACTIMDDIAG_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGACTIMDDIAG_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[AGACTIMDDIAG] NOCHECK CONSTRAINT [FK_AGACTIMDDIAG_AGACTIMED];


GO
ALTER TABLE [dbo].[AGACTIMDDIAG] NOCHECK CONSTRAINT [FK_AGACTIMDDIAG_INCUPSIPS];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AGACTIMDDIAG__CODACTMED__CODSERIPS]
    ON [dbo].[AGACTIMDDIAG]([CODACTMED] ASC, [CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMDDIAG', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la actividad médica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMDDIAG', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTIMDDIAG', @level2type = N'COLUMN', @level2name = N'ID';

