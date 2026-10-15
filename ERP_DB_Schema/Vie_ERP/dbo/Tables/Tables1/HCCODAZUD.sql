CREATE TABLE [dbo].[HCCODAZUD] (
    [CODCONSEC]   NUMERIC (18)  NOT NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CANPEDPRO]   INT           NOT NULL,
    [TIPPRODUC]   CHAR (1)      NOT NULL,
    [CANENTPRO]   INT           NULL,
    [Indications] VARCHAR (200) NULL,
    CONSTRAINT [PK_HCCODAZUD] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCCODAZUD_HCCODAZUC] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCCODAZUC] ([CODCONSEC]),
    CONSTRAINT [FK_HCCODAZUD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[HCCODAZUD] NOCHECK CONSTRAINT [FK_HCCODAZUD_HCCODAZUC];


GO
ALTER TABLE [dbo].[HCCODAZUD] NOCHECK CONSTRAINT [FK_HCCODAZUD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo de Indicaciones a la hora de prescribir un medicamento desde la opcion de emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'Indications';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Utilizada del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANENTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Producto  1: Medicamentos  2: Materiales e Insumos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'TIPPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCODAZUD', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

