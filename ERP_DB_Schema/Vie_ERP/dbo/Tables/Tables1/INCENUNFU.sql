CREATE TABLE [dbo].[INCENUNFU] (
    [CODCENATE] CHAR (10)    NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_INCENUNFU] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_INCENUNFU_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]) ON UPDATE CASCADE,
    CONSTRAINT [FK_INCENUNFU_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[INCENUNFU] NOCHECK CONSTRAINT [FK_INCENUNFU_ADcenaten];


GO
ALTER TABLE [dbo].[INCENUNFU] NOCHECK CONSTRAINT [FK_INCENUNFU_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Reservado Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENUNFU', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENUNFU', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCENUNFU', @level2type = N'COLUMN', @level2name = N'CODCENATE';

