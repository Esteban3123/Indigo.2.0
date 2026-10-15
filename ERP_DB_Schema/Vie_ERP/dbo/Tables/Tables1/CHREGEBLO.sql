CREATE TABLE [dbo].[CHREGEBLO] (
    [CODICAMAS] INT          NOT NULL,
    [CODTIPBLO] CHAR (2)     NOT NULL,
    [FECINIBLO] DATETIME     NOT NULL,
    [FECFINBLO] DATETIME     NULL,
    [OBSERBLOQ] CHAR (250)   NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_CHREGEBLO_1] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHREGEBLO_CHTIPBLOQ] FOREIGN KEY ([CODTIPBLO]) REFERENCES [dbo].[CHTIPBLOQ] ([CODTIPBLO]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[CHREGEBLO] NOCHECK CONSTRAINT [FK_CHREGEBLO_CHTIPBLOQ];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'OBSERBLOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'FECFINBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Incial de Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'FECINIBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'CODTIPBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGEBLO', @level2type = N'COLUMN', @level2name = N'CODICAMAS';

