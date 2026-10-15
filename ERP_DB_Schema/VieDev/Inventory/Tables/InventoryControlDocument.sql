CREATE TABLE [Inventory].[InventoryControlDocument] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber] VARCHAR (100) NOT NULL,
    [DocumentType]   INT           NOT NULL,
    [DocumentUser]   VARCHAR (50)  NOT NULL,
    [DocumentDate]   DATETIME      NOT NULL,
    [Timestamp]      ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InventoryControlDocument__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del documento   Orden de compra = 1  Remision de Entrada = 2  Ajuste de Inventario = 3  Solicitudes = 4  Dispensacion Farmaceutica = 5  Remision de Salida = 6  Devolucion de Remisiones = 7  Comprobante de entrada = 8  Devolucion de compra = 9  Prestamo de mercancia = 10  Devolucion de suministro = 11  Devolucion de prestamo = 12  Ordenes de Traslado = 13  Devoluciones Ordenes de Traslado = 14  Remision de inventario en consignación = 15  Cesión de Contrato = 16  Otro si de Contrato = 17  Traslado de dispensación = 18  Cargo de productos en Transito =19  Dispensacion Farmaceutica por paciente = 50', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDocument', @level2type = N'COLUMN', @level2name = N'DocumentType';

