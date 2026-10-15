CREATE TABLE [dbo].[HCPLAINFD] (
    [CODPLAINF] NUMERIC (18)    NOT NULL,
    [CODPROMED] CHAR (20)       NOT NULL,
    [CANELEMED] NUMERIC (18, 2) NOT NULL,
    [CODUNIMED] VARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_HCPLAINFD] PRIMARY KEY CLUSTERED ([CODPLAINF] ASC, [CODPROMED] ASC),
    CONSTRAINT [FK_HCPLAINFD_HCPLAINFC] FOREIGN KEY ([CODPLAINF]) REFERENCES [dbo].[HCPLAINFC] ([CODPLAINF]),
    CONSTRAINT [FK_HCPLAINFD_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
ALTER TABLE [dbo].[HCPLAINFD] NOCHECK CONSTRAINT [FK_HCPLAINFD_HCPLAINFC];


GO
ALTER TABLE [dbo].[HCPLAINFD] NOCHECK CONSTRAINT [FK_HCPLAINFD_INUNIMEDI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida del medicamento estipulada en volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de producto a mezclar se almacena la cantidad estipulada en volumen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CANELEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto que hace parte de la mezcla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPROMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la plantilla de infusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAINFD', @level2type = N'COLUMN', @level2name = N'CODPLAINF';

