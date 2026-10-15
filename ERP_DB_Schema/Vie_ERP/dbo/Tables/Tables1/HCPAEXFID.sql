CREATE TABLE [dbo].[HCPAEXFID] (
    [CODCONCEC] INT       NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    CONSTRAINT [PK_HCPAEXFID] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_HCPAEXFID_HCPAEXFIC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCPAEXFIC] ([AUTO]),
    CONSTRAINT [FK_HCPAEXFID_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPAEXFID] NOCHECK CONSTRAINT [FK_HCPAEXFID_HCPAEXFIC];


GO
ALTER TABLE [dbo].[HCPAEXFID] NOCHECK CONSTRAINT [FK_HCPAEXFID_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAEXFID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

