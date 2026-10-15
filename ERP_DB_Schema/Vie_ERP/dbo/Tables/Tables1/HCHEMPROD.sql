CREATE TABLE [dbo].[HCHEMPROD] (
    [CODPRODUC] TINYINT    NOT NULL,
    [DESPRODUC] NCHAR (60) NOT NULL,
    [ESTPRODUC] BIT        NOT NULL,
    CONSTRAINT [PK_HCHEMPROD] PRIMARY KEY CLUSTERED ([CODPRODUC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Producto - Hemoderivado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'ESTPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Tipo de Producto - Hemoderivados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'DESPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Producto - Hemoderivados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMPROD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';

