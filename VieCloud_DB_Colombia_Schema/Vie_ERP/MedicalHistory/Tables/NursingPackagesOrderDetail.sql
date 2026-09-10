CREATE TABLE [MedicalHistory].[NursingPackagesOrderDetail] (
    [Id]                     INT       IDENTITY (1, 1) NOT NULL,
    [IdNursingPackagesOrder] INT       NOT NULL,
    [CODPRODUC]              CHAR (20) NOT NULL,
    [Quantity]               INT       NOT NULL,
    [CurrentQuantity]        INT       NULL,
    CONSTRAINT [PK_HcfarmepdNursingPackagesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NursingPackagesOrderDetail_NursingPackagesOrder] FOREIGN KEY ([IdNursingPackagesOrder]) REFERENCES [MedicalHistory].[NursingPackagesOrder] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_NursingPackagesOrderDetail_CODPRODUC_IdNursingPackagesOrder_CurrentQuantity]
    ON [MedicalHistory].[NursingPackagesOrderDetail]([CODPRODUC] ASC, [IdNursingPackagesOrder] ASC)
    INCLUDE([CurrentQuantity]);


GO
CREATE NONCLUSTERED INDEX [IX_NursingPackagesOrderDetail_Quantity_IdNursingPackagesOrder]
    ON [MedicalHistory].[NursingPackagesOrderDetail]([Quantity] ASC, [IdNursingPackagesOrder] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad actual/dispensada/consumida (INT, nullable). Unidades reales entregadas, administradas o consumidas del producto hasta el momento del registro.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CurrentQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Cantidad actual', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CurrentQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CurrentQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad solicitada/prescrita (INT). Número total de unidades del producto ordenadas en esta línea del paquete de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la cantidad', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/insumo/medicamento (CHAR 20). Identificador de artículo, medicamento, material o recurso solicitado en el paquete de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el codigo del producto', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT, FK). Identificador de la orden/paquete de enfermería padre. Referencia a MedicalHistory.NursingPackagesOrder.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id Enfermería Paquetes Orden', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'IdNursingPackagesOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK). Consecutivo/número de secuencia de cada detalle de línea en el paquete de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los paquetes de enfermería ordenados: registra cada insumo o producto incluido en una orden de paquete de enfermería, con la cantidad solicitada y la cantidad disponible actual.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrderDetail';
