CREATE TABLE [PathologyALULA].[REQUEST_SPECIMENS] (
    [id]                     VARCHAR (16) NOT NULL,
    [order_id]               VARCHAR (16) NOT NULL,
    [specimen_id]            INT          NOT NULL,
    [specimen_parent_id]     INT          NOT NULL,
    [specimen_source_site]   INT          NULL,
    [specimen_received_date] DATETIME     NULL,
    [container_type]         INT          NULL,
    CONSTRAINT [PK_REQUEST_SPECIMENS] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contenedor, frasco o recipiente para transporte y almacenamiento de la muestra de laboratorio/patología (tubo, vial, frasco estéril)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'container_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del contenedor', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'container_type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'container_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de recepción de la muestra en laboratorio de patología; timestamp de ingreso del espécimen', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_received_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción de la muestra', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_received_date';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_received_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de origen del espécimen; lugar donde se tomó la muestra (tejido, sangre, fluido, órgano)', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_source_site';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio de origen del espécimen', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_source_site';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_source_site';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del espécimen padre; referencia jerárquica para muestras fraccionadas o derivadas de una muestra principal', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_parent_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id del espécimen padre', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_parent_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_parent_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del espécimen/muestra de laboratorio o patología; código de rastreo de la muestra', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id del espécimen', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de solicitud/petición de análisis de laboratorio o estudio patológico', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'order_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el id de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'order_id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'order_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo del registro de especímenes en la solicitud; clave primaria de la relación', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los especímenes (muestras biológicas) asociados a solicitudes de patología: qué muestra se recibió, de qué sitio del cuerpo proviene, cuándo llegó al laboratorio y en qué tipo de contenedor fue enviada.', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS';
