CREATE TABLE [dbo].[IHPRODIAD] (
    [CODCONCEC]   INT           NOT NULL,
    [CODDIAGNO]   CHAR (4)      NOT NULL,
    [TIPDIAGNOS]  CHAR (1)      NOT NULL,
    [OBSERVACIO]  VARCHAR (MAX) NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CODPRODUCHC] CHAR (20)     NULL,
    CONSTRAINT [PK_IHPRODIAD] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODDIAGNO] ASC),
    CONSTRAINT [FK_IHPRODIAD_IHLISTPRO] FOREIGN KEY ([CODPRODUCHC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[IHPRODIAD] NOCHECK CONSTRAINT [FK_IHPRODIAD_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del producto relacionado con el campo CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del diagnostico agregado a la ficha tecnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Diagnostico  1= Indicaciones  2= Contraindicaciones  3= Precauciones  4= Reacciones Adversas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo el consecutivo de la cabecera de la tabla de diagnostico de productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

