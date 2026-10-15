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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo del contenedor', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'container_type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de la muestra', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_received_date';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio de origen del espécimen', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_source_site';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id del espécimen padre', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_parent_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id del espécimen', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'specimen_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la orden', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'order_id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'PathologyALULA', @level1type = N'TABLE', @level1name = N'REQUEST_SPECIMENS', @level2type = N'COLUMN', @level2name = N'id';

