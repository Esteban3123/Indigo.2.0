CREATE TABLE [dbo].[CHTIPBLOQ] (
    [CODTIPBLO] CHAR (2)     NOT NULL,
    [DESTIPBLO] CHAR (80)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTIPBLOQ] PRIMARY KEY CLUSTERED ([CODTIPBLO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de BLoqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'DESTIPBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTIPBLOQ', @level2type = N'COLUMN', @level2name = N'CODTIPBLO';

