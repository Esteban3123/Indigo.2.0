CREATE TABLE [Inventory].[DocumentInvoiceProductSales] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                      VARCHAR (20)    NOT NULL,
    [BillingAuthorizationId]    INT             NOT NULL,
    [FunctionalUnitId]          INT             NOT NULL,
    [DocumentDate]              DATETIME        NOT NULL,
    [OperatingUnitId]           INT             CONSTRAINT [DF_DocumentInvoiceProductSales_OperatingUnitId] DEFAULT ((9)) NOT NULL,
    [ThirdPartyId]              INT             NOT NULL,
    [WarehouseId]               INT             NOT NULL,
    [Description]               VARCHAR (MAX)   NULL,
    [Value]                     DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]             DECIMAL (18, 2) NOT NULL,
    [ValueTax]                  DECIMAL (18, 2) NOT NULL,
    [TotalValue]                DECIMAL (18, 2) NOT NULL,
    [InvoiceId]                 INT             NULL,
    [RecognitionId]             INT             NULL,
    [Status]                    TINYINT         NOT NULL,
    [CreationUser]              VARCHAR (20)    NOT NULL,
    [CreationDate]              DATETIME        NOT NULL,
    [ModificationUser]          VARCHAR (20)    NULL,
    [ModificationDate]          DATETIME        NULL,
    [ConfirmationUser]          VARCHAR (20)    NULL,
    [ConfirmationDate]          DATETIME        NULL,
    [AnnulmentUser]             VARCHAR (20)    NULL,
    [AnnulmentDate]             DATETIME        NULL,
    [TimeStamp]                 ROWVERSION      NOT NULL,
    [BranchOfficeId]            INT             NULL,
    [RetentionSource]           DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Reten__5E13A343] DEFAULT ((0)) NOT NULL,
    [WithholdingICA]            DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5F07C77C] DEFAULT ((0)) NOT NULL,
    [WithholdingTax]            DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5FFBEBB5] DEFAULT ((0)) NOT NULL,
    [DistrictTax]               DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Distr__60F00FEE] DEFAULT ((0)) NOT NULL,
    [IcaPercentage]             DECIMAL (6, 3)  CONSTRAINT [DF__DocumentI__IcaPe__61E43427] DEFAULT ((0)) NOT NULL,
    [FreightIVAValue]           DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Freig__62D85860] DEFAULT ((0)) NOT NULL,
    [FreightValue]              DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Freig__63CC7C99] DEFAULT ((0)) NOT NULL,
    [SaleModality]              TINYINT         CONSTRAINT [DF__DocumentI__SaleM__1D8F09DA] DEFAULT ((1)) NOT NULL,
    [ContractExternalClientsId] INT             NULL,
    [ConditionSalesId]          INT             NULL,
    [EconomicActivityId]        INT             NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSales_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ConditionSales] FOREIGN KEY ([ConditionSalesId]) REFERENCES [Billing].[ConditionSales] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ContractExternalClientsId] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Recognition] FOREIGN KEY ([RecognitionId]) REFERENCES [Budget].[Recognition] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSales_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-12
-- Description:	Trigger para actualizar el valor de una factura, si es una factura de producto
-- =============================================
CREATE TRIGGER [Inventory].[tggUpdateInvoiceThirPartySalesValue]
   ON [Inventory].[DocumentInvoiceProductSales]
   AFTER UPDATE
AS 
BEGIN
	IF UPDATE (Status) AND UPDATE(InvoiceId)
	BEGIN
		UPDATE i
			SET i.ThirdPartySalesValue = (ROUND(dips.Value, 2) + ROUND(dipsd.IvaValue, 2) - ROUND(dips.ValueDiscount, 2) - ROUND(dips.WithholdingTax, 2) - ROUND(dips.WithholdingICA, 2) - ROUND(dips.RetentionSource, 2) - dips.DistrictTax + dips.FreightValue + dips.FreightIVAValue),
				i.ValueTax = ROUND(dipsd.IvaValue, 2),
				i.TotalValue = i.InvoiceValue + ROUND(dipsd.IvaValue, 2)
		FROM Billing.Invoice i
		JOIN INSERTED dips ON i.Id = dips.InvoiceId
		JOIN 
		(
			SELECT	DocumentInvoiceProductSalesId,
					SUM(ROUND(IvaValue, 2)) IvaValue
			FROM Inventory.DocumentInvoiceProductSalesDetail
			GROUP BY DocumentInvoiceProductSalesId
		) dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
		WHERE dips.Status = 2 AND i.DocumentType = 7
	END
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la condición de venta (FK → Billing.ConditionSales). Define términos comerciales: plazo, descuentos aplicables, modalidad de pago para el documento de factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la condicion de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del contrato con centro de atención externo (FK → MixingStation.ContractExternalClients). Cuando se asocia, recalcula tarificación de productos según acuerdos comerciales del contrato externo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relaciona el Id del Contrato de centro de atencion externo, cuando un documento lo tiene calcula el valor   de los productos decuardo a la nueva tarificacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad de venta: 1=Contado (pago inmediato), 2=Crédito (pago diferido). TINYINT, determina condiciones de pago del documento de facturación de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'SaleModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'modalidad de venta: 1 - Contado; 2 - Crédito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'SaleModality';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'SaleModality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario del flete/transporte (DECIMAL 18,2). Costo de envío o acarreo de productos en la venta, incluido en cálculo de total.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA calculado sobre el flete (DECIMAL 18,2). Impuesto a valor agregado del transporte, obtenido mediante IcaPercentage.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva que se va a obtener del valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de ICA/IVA aplicado al valor del flete (DECIMAL 6,3). Tasa obtenida de parámetros de inventarios, calcula FreightIVAValue.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'IcaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va aplicar al flete, El porcentaje del flete se obtiene de los parametros de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'IcaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'IcaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto municipal o de distrito (DECIMAL 18,2). Gravamen local aplicable al documento de venta de productos, según jurisdicción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Impuesto de distrito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención de IVA (DECIMAL 18,2). Porcentaje del impuesto a valor agregado retenido en la factura de productos, por normativa tributaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la retencion del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención de ICA (DECIMAL 18,2). Retención del impuesto de contribución a actividades económicas en documento de venta de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la retencion del ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria de retenciones en la fuente de todos los ítems (DECIMAL 18,2). Acumulado de retenciones tributarias aplicadas a cada detalle de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RetentionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la sumatoria de la retencion en la fuente de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RetentionSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RetentionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de sucursal de facturación (FK → Payroll.BranchOffice, nullable). Se completa solo si el tercero/cliente maneja múltiples sucursales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la sucursal de facturación, este campo solo se llena si el tercero que seleccionaron maneja sucursal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de evento (TIMESTAMP, row version). Registra instante exacto de creación, modificación o cambio de estado del documento de venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de anulación del documento (DATETIME, nullable). Cuándo se invalidó o canceló la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que anuló el documento (VARCHAR 20, nullable). Identificación del operador que ejecutó la anulación de la factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de confirmación del documento (DATETIME, nullable). Cuándo se validó y confirmó la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que confirmó el documento (VARCHAR 20, nullable). Identificación del operador que confirmó la factura de venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación (DATETIME, nullable). Cuándo se editó el documento de factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de última modificación (VARCHAR 20, nullable). Identificación del operador que realizó el último cambio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del documento (DATETIME, auditoria). Cuándo se originó la factura de productos en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario creador del documento (VARCHAR 20, auditoria). Identificación del operador que registró la factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del documento: 1=Registrado, 2=Confirmado, 3=Anulado (TINYINT). Flujo de ciclo de vida de la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 1-Registrado 2-Confirmado 3-Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del reconocimiento presupuestario (FK → Budget.Recognition, nullable). Se completa cuando interfaces de presupuesto están activas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reconocimiento, este registro se llena cuando las interfaces de presupuesto estan activas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura generada (FK → Billing.Invoice, nullable). Referencia a documento de cobro/facturación formal derivado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura Generada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del documento (DECIMAL 18,2). Cálculo: Value - ValueDiscount + ValueTax + FreightIVAValue + retenciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor total del documento de venta de productos el cual se obtiene (Value - ValueDiscount + ValueTax)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Suma del IVA total (DECIMAL 18,2). Acumulado de impuestos a valor agregado de todos los ítems de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del IVA, es la suma de todo el IVAValue de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Suma de descuentos aplicados (DECIMAL 18,2). Acumulado de descuentos comerciales en todos los detalles de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, es la suma del DiscountValue de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor subtotal de la factura (DECIMAL 18,2). Suma de valores base (SubTotalValue) de todos los ítems sin impuestos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de la factura, es decir que es la suma del SubTotalValue de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nota del documento (VARCHAR MAX, nullable). Observaciones, conceptos adicionales del documento de venta de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Description';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén (FK → Inventory.Warehouse). Centro de distribución/bodega que gestiona control de inventario para la venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Almacen para el control del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tercero/cliente (FK → Common.ThirdParty). Entidad que compra productos: persona, empresa, institución de salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad operativa (FK → Common.OperatingUnit, default=9). Organización interna que emite o gestiona la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del documento (DATETIME). Cuándo se emitió o se registra la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DocumentDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de unidad funcional (FK → Payroll.FunctionalUnit). Departamento, servicio o área que realiza la venta de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de autorización de facturación (FK → Billing.BillingAuthorization). Permiso o rango numérico autorizado para emitir esta factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la autorizacion de facturacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del documento (VARCHAR 20, unique). Número de identificación única de la factura de productos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro (INT IDENTITY, PK). Clave primaria autoincrementable de la tabla DocumentInvoiceProductSales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Facturas de venta de productos del inventario (farmacia, insumos, comercial). Registra cada documento de venta con su valor, descuentos, impuestos, retenciones, modalidad de venta y referencias a la unidad funcional, bodega, tercero comprador y autorización de facturación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada a la factura de venta, usada para clasificar el tipo de operación comercial a efectos tributarios y de ICA.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
