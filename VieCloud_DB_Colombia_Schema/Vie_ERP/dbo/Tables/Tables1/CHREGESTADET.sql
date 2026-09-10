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



GO
CREATE NONCLUSTERED INDEX [IX_CHREGESTADET_IDCHREGESTA]
    ON [dbo].[CHREGESTADET]([IDCHREGESTA] ASC)
    INCLUDE([CANTIDADLIQ], [GENCUPSLIQ], [GENLIQUIDA], [GENULTFEC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del código CUPS (Clasificación Única de Procedimientos en Salud) utilizado en la liquidación del servicio o procedimiento; referencia a catálogo de procedimientos sanitarios para facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENCUPSLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del codigo CUPS usado para liquidar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENCUPSLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENCUPSLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades de servicio liquidadas; volumen de procedimientos, estancias u servicios facturados en este registro de detalle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'CANTIDADLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de unidades liquidadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'CANTIDADLIQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'CANTIDADLIQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la liquidación anterior o previa; nula si es primer registro de liquidación de la estancia, de lo contrario contiene la fecha de la liquidación previa tomada del ingreso; utilizado para anulación de órdenes de servicio tipo estancias y auditoría de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENULTFEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que corresponde a la liquidacion anterior. Cuando no se ha liquidado ninguna estancia éste campo va Null, de lo contrario va la fecha de la anterior liquidación que se toma del ingreso. Éste campo se usa para la anulación de ordenes de servicio tipo estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENULTFEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENULTFEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de la última liquidación efectuada; marca temporal del cierre o generación más reciente de factura, glosa o documento de cobro para esta estancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENLIQUIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la ultima liquidación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENLIQUIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'GENLIQUIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la estancia hospitalaria o de atención; clave foránea que vincula este detalle a su registro maestro de estancia en urgencias, hospitalización o servicio ambulatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'IDCHREGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'IDCHREGESTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'IDCHREGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de detalle de liquidación; secuencial de la tabla CHREGESTADET para rastreo y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las liquidaciones de un estado de cuenta o proceso de cobro (CHREGESTA): registra cada generación de liquidación con su fecha, cantidad de registros liquidados y número de CUPS incluidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHREGESTADET';
