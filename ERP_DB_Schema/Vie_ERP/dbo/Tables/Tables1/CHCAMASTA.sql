CREATE TABLE [dbo].[CHCAMASTA] (
    [CODINDTAB] INT       NOT NULL,
    [DESTABCTR] CHAR (15) NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [IMGTABCTR] IMAGE     NOT NULL,
    CONSTRAINT [PK_CHCAMASTA] PRIMARY KEY CLUSTERED ([CODINDTAB] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plano de Fondo del TabPage o Area', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'IMGTABCTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tab Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'DESTABCTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tab Page', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHCAMASTA', @level2type = N'COLUMN', @level2name = N'CODINDTAB';

