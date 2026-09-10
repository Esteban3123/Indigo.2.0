CREATE TABLE [Taxes].[ResultLoadPlaneCollection] (
    [Id]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [State]   INT           NOT NULL,
    [Message] VARCHAR (MAX) NOT NULL,
    [Record]  VARCHAR (350) NOT NULL,
    CONSTRAINT [PK_ResultLoadPlaneCollection] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grabación, registro o contenido de datos cargados en la colección de planes; cadena de hasta 350 caracteres que contiene la información procesada del archivo o lote importado', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Record';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grabacion', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Record';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Record';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de resultado, notificación o descripción del estado del proceso de carga; texto variable que incluye confirmaciones, advertencias o errores ocurridos durante la importación de planes', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del proceso de carga; código numérico que indica el resultado: éxito, error, pendiente o validación de la colección de planes', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla; clave primaria que registra cada intento o resultado de carga de la colección de planes', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de resultados del proceso de carga del plan de cobro (planeación de cartera o recaudo). Guarda el estado de cada intento de carga, el mensaje de respuesta (éxito o error) y el detalle del registro procesado.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection';
