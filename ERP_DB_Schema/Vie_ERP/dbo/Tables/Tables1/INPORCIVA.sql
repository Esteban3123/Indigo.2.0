CREATE TABLE [dbo].[INPORCIVA] (
    [CODGOIVA]  CHAR (2)     NOT NULL,
    [DESCRIIVA] VARCHAR (80) NOT NULL,
    [PORCEIVA]  NUMERIC (18) NULL,
    [TARIFIVA]  NUMERIC (3)  NULL,
    CONSTRAINT [PK_INPORCIVA] PRIMARY KEY CLUSTERED ([CODGOIVA] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tarifa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'TARIFIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procentaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'PORCEIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion IVA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'DESCRIIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Iva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPORCIVA', @level2type = N'COLUMN', @level2name = N'CODGOIVA';

