CREATE TABLE [dbo].[CHREGESTADET] (
    [ID]          INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCHREGESTA] INT      NOT NULL,
    [GENLIQUIDA]  DATETIME NOT NULL,
    [GENULTFEC]   DATETIME NULL,
    [CANTIDADLIQ] INT      NOT NULL,
    [GENCUPSLIQ]  INT      NOT NULL,
    CONSTRAINT [PK_CHREGESTADET] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CHREGESTADET_CHREGESTA] FOREIGN KEY ([IDCHREGESTA]) REFERENCES [dbo].[CHREGESTA] ([ID])
);


GO
ALTER TABLE [dbo].[CHREGESTADET] NOCHECK CONSTRAINT [FK_CHREGESTADET_CHREGESTA];


GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTADET_IDCHREGESTA]
    ON [dbo].[CHREGESTADET]([IDCHREGESTA] ASC)
    INCLUDE([CANTIDADLIQ], [GENCUPSLIQ], [GENLIQUIDA], [GENULTFEC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del codigo CUPS usado para liquidar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENCUPSLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de unidades liquidadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'CANTIDADLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que corresponde a la liquidacion anterior. Cuando no se ha liquidado ninguna estancia éste campo va Null, de lo contrario va la fecha de la anterior liquidación que se toma del ingreso. Éste campo se usa para la anulación de ordenes de servicio tipo estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENULTFEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la ultima liquidación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENLIQUIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'IDCHREGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'ID';

