CREATE TABLE [Inventory].[PharmaceuticalDispensingTransferDetail] (
    [Id]                                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingTransferId]          INT NOT NULL,
    [PharmaceuticalDispensingDetailBatchSerialId] INT NOT NULL,
    [Quantity]                                    INT NOT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingTransferDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingDetailBatchSerial] FOREIGN KEY ([PharmaceuticalDispensingDetailBatchSerialId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingTransfer] FOREIGN KEY ([PharmaceuticalDispensingTransferId]) REFERENCES [Inventory].[PharmaceuticalDispensingTransfer] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PharmaceuticalDispensingTransferDetail]
    ON [Inventory].[PharmaceuticalDispensingTransferDetail]([PharmaceuticalDispensingTransferId] ASC, [PharmaceuticalDispensingDetailBatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades farmacéuticas a devolver o transferir en el detalle del traslado; tipo INT, representa la cantidad del medicamento/producto del lote.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de serie del lote de distribución farmacéutica; clave foránea a PharmaceuticalDispensingDetailBatchSerial, vincula el detalle de transferencia al lote, número de serie y trazabilidad del medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de serie del lote de distribución farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del traslado o transferencia de dispensación farmacéutica; clave foránea a PharmaceuticalDispensingTransfer, agrupa todos los detalles de ítems del mismo traslado/devolución entre farmacias o almacenes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del traslado de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle del traslado de dispensación farmacéutica; clave primaria INT IDENTITY, cada registro representa una línea de medicamento transferido en una solicitud de traslado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del detalle de traslado de dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de transferencias de dispensación farmacéutica: registra cada ítem (lote o serial) incluido en una transferencia de medicamentos entre unidades o bodegas, indicando la cantidad trasladada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail';
