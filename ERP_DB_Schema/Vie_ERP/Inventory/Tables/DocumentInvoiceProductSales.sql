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
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_BillingAuthorization];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_BranchOffice];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_ContractExternalClientsId];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_FunctionalUnit];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Invoice];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_OperatingUnit];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Recognition];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_ThirdParty];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSales] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSales_Warehouse];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la condicion de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConditionSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relaciona el Id del Contrato de centro de atencion externo, cuando un documento lo tiene calcula el valor   de los productos decuardo a la nueva tarificacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'modalidad de venta: 1 - Contado; 2 - Crédito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'SaleModality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva que se va a obtener del valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va aplicar al flete, El porcentaje del flete se obtiene de los parametros de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'IcaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto de distrito', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria de la retencion en la fuente de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RetentionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la sucursal de facturación, este campo solo se llena si el tercero que seleccionaron maneja sucursal', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BranchOfficeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-Registrado 2-Confirmado 3-Anulado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reconocimiento, este registro se llena cuando las interfaces de presupuesto estan activas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'RecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Factura Generada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor total del documento de venta de productos el cual se obtiene (Value - ValueDiscount + ValueTax)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del IVA, es la suma de todo el IVAValue de los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, es la suma del DiscountValue de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor de la factura, es decir que es la suma del SubTotalValue de todos los detalles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Almacen para el control del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero de la entidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la autorizacion de facturacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'BillingAuthorizationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSales', @level2type = N'COLUMN', @level2name = N'Id';

