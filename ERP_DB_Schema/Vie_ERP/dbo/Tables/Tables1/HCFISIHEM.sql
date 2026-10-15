CREATE TABLE [dbo].[HCFISIHEM] (
    [CODCENATE]  CHAR (10) NOT NULL,
    [UFUCODIGO]  CHAR (10) NOT NULL,
    [CODPRODUC]  TINYINT   NOT NULL,
    [CANPRODISP] INT       NOT NULL,
    CONSTRAINT [PK_HCFISIHEM] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC, [CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIHEM', @level2type = N'COLUMN', @level2name = N'CANPRODISP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIHEM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIHEM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFISIHEM', @level2type = N'COLUMN', @level2name = N'CODCENATE';

