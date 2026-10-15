CREATE TABLE [dbo].[IHPRODIAG] (
    [AUTO]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPRODUC]   CHAR (20)     NOT NULL,
    [CODDIAGNO]   CHAR (4)      NOT NULL,
    [TIPDIAGNOS]  CHAR (1)      NOT NULL,
    [OBSERVACIO]  VARCHAR (MAX) NULL,
    [CODPRODUCHC] CHAR (20)     NULL,
    CONSTRAINT [PK_IHPRODIAG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_IHPRODIAG_IHLISTPRO] FOREIGN KEY ([CODPRODUCHC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);


GO
ALTER TABLE [dbo].[IHPRODIAG] NOCHECK CONSTRAINT [FK_IHPRODIAG_IHLISTPRO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del producto relacionado con el campo CODPRODUC de la tabla IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUCHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del diagnostico agregado a la ficha tecnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de diagnostico agregado  1-Indicaciones 2- Contraindicaciones 3 -Precauciones 4-Reacciones Adversas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'TIPDIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de ficha tecnica diagnosticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHPRODIAG', @level2type = N'COLUMN', @level2name = N'AUTO';

