CREATE TABLE [dbo].[INCUPSGRU] (
    [CODGRUIPS] CHAR (10)     NOT NULL,
    [DESGRUIPS] VARCHAR (100) NULL,
    [GRUPOIMAG] BIT           NOT NULL,
    CONSTRAINT [PK_INCUPSGRU] PRIMARY KEY CLUSTERED ([CODGRUIPS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me determina si el grupo es de tipo imagenologia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'GRUPOIMAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'DESGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSGRU', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';

