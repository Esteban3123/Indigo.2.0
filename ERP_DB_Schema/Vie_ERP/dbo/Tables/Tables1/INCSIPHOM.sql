CREATE TABLE [dbo].[INCSIPHOM] (
    [AUTO]       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]  CHAR (20) NOT NULL,
    [CODSERIPS1] CHAR (20) NOT NULL,
    [TIPOTARIF]  CHAR (1)  NOT NULL,
    CONSTRAINT [PK_INCSIPHOM] PRIMARY KEY NONCLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_INCSIPHOM_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_INCSIPHOM_INCUPSIPS1] FOREIGN KEY ([CODSERIPS1]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[INCSIPHOM] NOCHECK CONSTRAINT [FK_INCSIPHOM_INCUPSIPS];


GO
ALTER TABLE [dbo].[INCSIPHOM] NOCHECK CONSTRAINT [FK_INCSIPHOM_INCUPSIPS1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'TIPOTARIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCSIPHOM', @level2type = N'COLUMN', @level2name = N'AUTO';

