CREATE TABLE [Inventory].[PharmaceuticalDispensingDetail] (
    [Id]                                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingId]                INT             NOT NULL,
    [CareGroupId]                               INT             NOT NULL,
    [HealthAdministratorId]                     INT             NULL,
    [ThirdPartyId]                              INT             NULL,
    [ProductId]                                 INT             NOT NULL,
    [WarehouseId]                               INT             NOT NULL,
    [Quantity]                                  INT             NOT NULL,
    [ReturnedQuantity]                          INT             NOT NULL,
    [ServiceDate]                               DATETIME        NOT NULL,
    [FunctionalUnitId]                          INT             NOT NULL,
    [OrderedHealthProfessionalCode]             CHAR (20)       NULL,
    [OrderedProfessionalSpecialty]              CHAR (3)        NULL,
    [OrderedHealthProfessionalThirdPartyId]     INT             NULL,
    [AuthorizationNumber]                       VARCHAR (20)    NULL,
    [LiquidationType]                           TINYINT         NOT NULL,
    [CupsEntityId]                              INT             NULL,
    [SurchargeApply]                            BIT             NOT NULL,
    [SalePrice]                                 DECIMAL (18, 2) NOT NULL,
    [AverageCost]                               DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]                        NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                             DECIMAL (18, 2) NOT NULL,
    [TotalSalesPrice]                           DECIMAL (18, 2) NOT NULL,
    [GrandTotalSalesPrice]                      DECIMAL (18, 2) NOT NULL,
    [QuotationPharmaceuticalDispensingDetailId] INT             NULL,
    [EntityId]                                  INT             NULL,
    [EntityName]                                VARCHAR (250)   NULL,
    [FinalProductCost]                          DECIMAL (18, 2) NOT NULL,
    [GrossValue]                                DECIMAL (18, 2) CONSTRAINT [DF__Pharmaceu__SubTo__5C48F45D] DEFAULT ((0)) NOT NULL,
    [TaxValue]                                  DECIMAL (18, 2) CONSTRAINT [DF__Pharmaceu__TAXVa__5D3D1896] DEFAULT ((0)) NOT NULL,
    [IvaId]                                     INT             NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PharmaceuticalDispensingDetail] CHECK ([ReturnedQuantity]<=[Quantity]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_CUPSEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_GeneralLedgerIVA] FOREIGN KEY ([IvaId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing] FOREIGN KEY ([PharmaceuticalDispensingId]) REFERENCES [Inventory].[PharmaceuticalDispensing] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetail] FOREIGN KEY ([QuotationPharmaceuticalDispensingDetailId]) REFERENCES [Billing].[QuotationPharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty] FOREIGN KEY ([OrderedHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_ThirdParty1] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing];




GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetail];


GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing];


GO



GO



GO



GO





GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetail];


GO



GO



GO



GO



GO



GO



GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetail_PharmaceuticalDispensing];


GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [XL_indice_CAC]
    ON [Inventory].[PharmaceuticalDispensingDetail]([PharmaceuticalDispensingId] ASC, [ProductId] ASC, [OrderedHealthProfessionalCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetailId]
    ON [Inventory].[PharmaceuticalDispensingDetail]([QuotationPharmaceuticalDispensingDetailId] ASC);


GO
ALTER INDEX [IDX_PharmaceuticalDispensingDetail_QuotationPharmaceuticalDispensingDetailId]
    ON [Inventory].[PharmaceuticalDispensingDetail] DISABLE;




GO
CREATE NONCLUSTERED INDEX [XL_Indice_202]
    ON [Inventory].[PharmaceuticalDispensingDetail]([PharmaceuticalDispensingId] ASC)
    INCLUDE([ProductId], [Quantity], [ReturnedQuantity]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del IVA (Impuesto al Valor Agregado). INT, FK a GeneralLedgerIVA. Referencia al catálogo de tasas impositivas aplicables en Colombia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'IvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del Iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'IvaId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'IvaId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del impuesto IVA en pesos colombianos. DECIMAL(18,2). Calculado sobre el subtotal bruto según la tasa vigente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del impuesto en colombia seria el IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TaxValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal antes de IVA. DECIMAL(18,2), default 0. Base imponible = (Precio unitario - Descuento) × Cantidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sub total antes de iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrossValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo final del producto después de ajustes. DECIMAL(18,2). Incluye costo promedio y variaciones de valuación de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo Final del  producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la tabla o entidad origen del registro en EHR. VARCHAR(250). Trazabilidad de fuente de datos (ej: Atención, Ingreso, Urgencia).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la tabla de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla/entidad EHR de procedencia. INT. Vincula a atención, ingreso o servicio original en el sistema de salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla del EHR de la cual viene el registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'EntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a detalle de cotización farmacéutica. INT. Asignado al importar cotización; se anula al cancelar dispensación para permitir re-solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la cotización, se asigna cuando se importa una cotización desde el formulario de dispensación farmaceutica, y al momento de anular la dispensación se nulea este campo para poder volverla a pedir', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'QuotationPharmaceuticalDispensingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total de ventas final: (Precio unitario - Descuento) × Cantidad. DECIMAL(18,2). Valor a cobrar por línea de medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el ((valor unitario  - Valor Descuento) * Cantidad)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario neto menos descuento aplicado. DECIMAL(18,2). Base para cálculo de valor total de la dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor unitario menos el descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe en pesos del descuento otorgado. DECIMAL(18,2). = Subtotal × (DiscountPercentage / 100).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalSales por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de rebaja sobre subtotal. NUMERIC(5,2). Aplicado a política comercial, contrato o glosa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento que se le va aplicar a al subtotal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo promedio unitario del producto en inventario. DECIMAL(18,2). Método de valuación FIFO/promedio ponderado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Costo promedio del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AverageCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta unitario del medicamento. DECIMAL(18,2). Tarifa según catálogo o contrato con entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto, Valor Unitario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de aplicación de recargo al precio. BIT (0/1). Si aplica, incrementa SalePrice por servicio de dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a código CUPS de servicio. INT. Requerido si LiquidationType=2 (medicamento incluido 100% en servicio IPS).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del cups que se debe encontrar en la orden de servicio, Este campo se solicita solo si el tipo de liquidacion es 2, es decir que esta incluido al 100% dentro de un servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de facturación: 1=Manual Tarifario, 2=Incluido en servicio IPS 100%. TINYINT. Define cómo se liquida ante asegurador.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de liquidaicon  1 - Manual Tarifario  2 - Incluido al 100% dentro de un servicio IPS', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'LiquidationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de autorización del medicamento/servicio. VARCHAR(20). Requerido por asegurador o RIPS para facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Autorizacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'AuthorizationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a tercero (médico ordenante). INT. Identifica profesional de salud que prescribió el medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero del medico que ordena', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especialidad del médico que ordenó. CHAR(3). Código de especialidad médica (ej: 01=Medicina General).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la especialidad el profesional que ordeno', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedProfessionalSpecialty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud ordenante. CHAR(20). Procedente de tabla INPROFSAL; vincula a credencial sanitaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que ordeno el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'OrderedHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a unidad funcional dispensadora. INT. Área/farmacia del IPS donde se surte el medicamento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion  de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de dispensación del medicamento. DATETIME. Marca el evento de entrega al paciente/dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se dispenso el medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ServiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de medicamentos devueltos por paciente. INT. No puede exceder Quantity (CHECK constraint).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se ha devuelto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ReturnedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad a dispensar al paciente. INT. Unidades de medicamento según prescripción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a almacén de origen. INT. Depósito del cual sale el medicamento en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del almacen desde el cual se va a dispensar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a producto farmacéutico. INT. Identifica medicamento, presentación y lote en InventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a tercero (pagador/entidad). INT. Asegurador, EPS o entidad que financia la atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a administrador de salud (contratante). INT. Entidad con la que se formalizó el contrato o acuerdo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero con quien se realiza el contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'HealthAdministratorId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a grupo de atención/contrato. INT. Agrupa servicios y medicamentos bajo un mismo acuerdo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a encabezado de dispensación farmacéutica. INT. Agrupa múltiples detalles en un acto de dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la dispensación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de dispensación. INT IDENTITY(1,1). Clave primaria de auditoría y trazabilidad farmacéutica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de cada ítem dispensado en una orden farmacéutica: medicamento entregado, cantidad, precios, costos, descuentos, impuestos y datos del profesional que ordenó la fórmula. Permite trazabilidad de la dispensación de medicamentos por paciente, bodega y entidad pagadora para facturación, RIPS y control de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingDetail';

GO
CREATE NONCLUSTERED INDEX [IX_PharmaceuticalDispensingDetail_PharmaceuticalDispensingId]
    ON [Inventory].[PharmaceuticalDispensingDetail]([PharmaceuticalDispensingId] ASC)
    INCLUDE([ProductId], [WarehouseId]);
