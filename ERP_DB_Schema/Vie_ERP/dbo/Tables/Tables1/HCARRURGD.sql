CREATE TABLE [dbo].[HCARRURGD] (
    [AUTO]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC] NUMERIC (18) NOT NULL,
    [CODARRURG] CHAR (3)     NOT NULL,
    CONSTRAINT [PK_HCARRURGD] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_HCARRURGD_HCARRURGC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCARRURGC] ([AUTOARRIB])
);


GO
ALTER TABLE [dbo].[HCARRURGD] NOCHECK CONSTRAINT [FK_HCARRURGD_HCARRURGC];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCARRURGD]
    ON [dbo].[HCARRURGD]([CODCONCEC] ASC, [CODARRURG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODARRURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo Tabla Cabecera Arribo Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCARRURGD', @level2type = N'COLUMN', @level2name = N'AUTO';

