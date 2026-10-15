CREATE TABLE [Inventory].[EntranceVoucher] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                       VARCHAR (20)    NOT NULL,
    [OperatingUnitId]            INT             NOT NULL,
    [DocumentDate]               DATE            NOT NULL,
    [SupplierId]                 INT             NOT NULL,
    [SupplierDistributionLineId] INT             NOT NULL,
    [SupplierTypeId]             INT             NOT NULL,
    [WarehouseId]                INT             NOT NULL,
    [AccountPayableId]           INT             NULL,
    [Description]                VARCHAR (500)   NOT NULL,
    [ResourceType]               TINYINT         NOT NULL,
    [RoundService]               INT             NOT NULL,
    [IcaPercentage]              NUMERIC (5, 3)  NOT NULL,
    [InvoiceNumber]              VARCHAR (100)   NOT NULL,
    [InvoiceDate]                DATETIME        NOT NULL,
    [DayPeriod]                  INT             NOT NULL,
    [FreightValue]               DECIMAL (18, 2) NOT NULL,
    [FreightValueOutstanding]    DECIMAL (18, 2) NOT NULL,
    [FreightIVAPercentage]       NUMERIC (5, 2)  NOT NULL,
    [FreightIVAValue]            DECIMAL (18, 2) NOT NULL,
    [FreightIVAValueOutstanding] DECIMAL (18, 2) NOT NULL,
    [Value]                      DECIMAL (18, 2) NOT NULL,
    [ValueDiscount]              DECIMAL (18, 2) NOT NULL,
    [ValueTax]                   DECIMAL (18, 2) NOT NULL,
    [WithholdingTax]             DECIMAL (18, 2) NOT NULL,
    [WithholdingICA]             DECIMAL (18, 2) NOT NULL,
    [RetentionSource]            DECIMAL (18, 2) NOT NULL,
    [RetentionOther]             DECIMAL (18, 2) NOT NULL,
    [DeductionOther]             DECIMAL (18, 2) NOT NULL,
    [DistrictTax]                DECIMAL (18, 2) NOT NULL,
    [TotalValue]                 DECIMAL (18, 2) NOT NULL,
    [Status]                     TINYINT         NOT NULL,
    [CreationUser]               VARCHAR (20)    NOT NULL,
    [CreationDate]               DATETIME        NOT NULL,
    [ModificationUser]           VARCHAR (20)    NULL,
    [ModificationDate]           DATETIME        NULL,
    [ConfirmationUser]           VARCHAR (20)    NULL,
    [ConfirmationDate]           DATETIME        NULL,
    [AnnulmentUser]              VARCHAR (20)    NULL,
    [AnnulmentDate]              DATETIME        NULL,
    [TimeStamp]                  ROWVERSION      NOT NULL,
    [CommitmentDetailId]         INT             NULL,
    [DocumentSupportId]          INT             NULL,
    [TaxRegistration]            TINYINT         CONSTRAINT [DF__EntranceV__TaxRe__1A9B2C91] DEFAULT ((1)) NOT NULL,
    [CurrencyId]                 INT             CONSTRAINT [DF__EntranceV__Curre__2696EF22] DEFAULT ((1)) NOT NULL,
    [EconomicActivityId]         INT             NULL,
    CONSTRAINT [PK_EntranceVoucher] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_EntranceVoucher_TotalValue] CHECK ([TotalValue]=(((((((((([Value]+[ValueTax])-[ValueDiscount])-[WithholdingTax])-[WithholdingICA])-[RetentionSource])-[RetentionOther])-[DeductionOther])-[DistrictTax])+[FreightValue])+[FreightIVAValue])),
    CONSTRAINT [FK_EntranceVoucher_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_CommitmentDetail] FOREIGN KEY ([CommitmentDetailId]) REFERENCES [Budget].[CommitmentDetail] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Common].[Supplier] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_SuppliersDistributionLines] FOREIGN KEY ([SupplierDistributionLineId]) REFERENCES [Common].[SuppliersDistributionLines] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_SupplierType] FOREIGN KEY ([SupplierTypeId]) REFERENCES [Common].[SupplierType] ([Id]),
    CONSTRAINT [FK_EntranceVoucher_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [CK_EntranceVoucher_TotalValue];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_AccountPayable];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_CommitmentDetail];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Currency];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_OperatingUnit];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Supplier];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_SuppliersDistributionLines];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_SupplierType];


GO
ALTER TABLE [Inventory].[EntranceVoucher] NOCHECK CONSTRAINT [FK_EntranceVoucher_Warehouse];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_EntranceVoucher__Code]
    ON [Inventory].[EntranceVoucher]([Code] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EntranceVoucher__Status__DocumentDate__INC__Id]
    ON [Inventory].[EntranceVoucher]([Status] ASC, [DocumentDate] ASC)
    INCLUDE([Id]);


GO

-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2018-07-11
-- Description:	Validar que el % de retención de ICA sea el correspondiente al proveedor seleccionado
-- =============================================
CREATE TRIGGER [Inventory].[tgg_ValidateIcaPercentage]
   ON  [Inventory].[EntranceVoucher]
   AFTER INSERT, UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here

	IF EXISTS 
	(
		SELECT *
		FROM INSERTED ev
		JOIN Common.Supplier s ON ev.SupplierId = s.Id
		JOIN Common.ThirdParty tp ON s.IdThirdParty = tp.Id
		JOIN Common.SuppliersDistributionLines sdl ON ev.SupplierDistributionLineId = sdl.Id		
		JOIN Common.DistributionLinesICARetention dlir ON ev.OperatingUnitId = dlir.OperatingUnitId AND sdl.IdDistributionLine = dlir.DistributionLineId
		JOIN Payments.AccountPayableConcepts apc ON dlir.AccountPayableConceptId = apc.Id
		LEFT JOIN GeneralLedger.RetentionConcepts rc ON apc.RetentionConceptId = rc.Id
		WHERE tp.RetentionType = 2
			AND tp.Ica = 1
			AND s.SelfWithholdingICA = 0
			AND ISNULL(rc.Rate, 0) <> ev.IcaPercentage
	)
	BEGIN
		THROW 51000, 'Error generado por control desde trigger. El porcentaje de Ica del registro no corresponde con el definido en la linea de distribución', 1
	END

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociado al comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la moneda del contrato', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va a llevar el Registro del IVA  1 - IVA al Costo  2 - IVA Descontable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'TaxRegistration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del documento soporte', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'DocumentSupportId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del compromiso', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'CommitmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'AnnulmentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Anulación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'AnnulmentUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ConfirmationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Confirmación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ConfirmationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (registrado = 1,confirmado = 2,anulado = 3)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Impuesto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deducción otros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'DeductionOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Retención Otros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'RetentionOther';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria de la retencion en la fuente de todos los items ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'RetentionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria del valor del IVA de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ValueTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria de todos los descuentos que se aplicaron a los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ValueDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria del subtotal de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del iva flete que esta pendiente por devolver, es decir que  cuando se cree este campo va hacer igual al valor del iva flete y a medida de que se hagan devoluciones este valor debe ir disminuyendo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'FreightIVAValueOutstanding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva que se va a obtener del valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'FreightIVAValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va aplicar al flete, El porcentaje del flete se obtiene de los parametros de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'FreightIVAPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del flete que esta pendiente por devolver, es decir que  cuando se cree este campo va hacer igual al valor del flete y a medida de que se hagan devoluciones este valor debe ir disminuyendo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'FreightValueOutstanding';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del flete', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'FreightValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de plazo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'DayPeriod';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'InvoiceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'InvoiceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del ICA que se va a aplicar, El porcentaje del ICA se obtiene del tercero pero se debe poder modificar ya que este varia dependiendo de la ciudad y otros factores', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'IcaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va redondear los valores del contrato  1 -- Peso  10 -- Decima  100 -- Centesima  1000 -- Milesima    La formula que se usa es Round(valor / redondeo) * redondeo  Round(2005/ 10) * 10', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Recurso (Ninguna = 0,Funcionamiento = 1,Inversion = 2)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'ResourceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta por pagar que se crea al confirmar el comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tipo de proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'SupplierTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la linea de distribucion del proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'SupplierDistributionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del proveedor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'SupplierId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'OperatingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucher', @level2type = N'COLUMN', @level2name = N'Id';

