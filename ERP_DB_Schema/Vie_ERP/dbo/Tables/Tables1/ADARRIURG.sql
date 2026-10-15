CREATE TABLE [dbo].[ADARRIURG] (
    [AUTO]      NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES] CHAR (10)    NULL,
    [AUTOARRIB] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADARRIURG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_ADARRIURG_HCARRURGC] FOREIGN KEY ([AUTOARRIB]) REFERENCES [dbo].[HCARRURGC] ([AUTOARRIB])
);


GO
ALTER TABLE [dbo].[ADARRIURG] NOCHECK CONSTRAINT [FK_ADARRIURG_HCARRURGC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTO';

