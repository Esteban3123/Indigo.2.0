CREATE TABLE [Billing].[ServiceOrderDetailDistribution] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlDetailId]    INT             NOT NULL,
    [ServiceOrderDetailId]      INT             NOT NULL,
    [Quantity]                  INT             NOT NULL,
    [GrandTotalSalesPrice]      NUMERIC (20, 2) NOT NULL,
    [GrandTotalDiscount]        NUMERIC (20, 2) CONSTRAINT [DF_ServiceOrderDetailDistribution_GrandTotalDiscount] DEFAULT ((0)) NOT NULL,
    [DistributionType]          TINYINT         NOT NULL,
    [ThirdPartySalesPrice]      NUMERIC (20, 2) NOT NULL,
    [ThirdPartyPercentage]      NUMERIC (5, 2)  NOT NULL,
    [ApplyRecoveryFee]          TINYINT         CONSTRAINT [DF_ServiceOrderDetailDistribution_ApplyRecoveryFee] DEFAULT ((1)) NOT NULL,
    [RecoveryFeeType]           TINYINT         CONSTRAINT [DF_ServiceOrderDetailDistribution_RecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [SubTotalPatientSalesPrice] NUMERIC (20, 2) NOT NULL,
    [PatientPercentage]         NUMERIC (5, 2)  NOT NULL,
    [LastCaregroupId]           INT             CONSTRAINT [DF_ServiceOrderDetailDistribution_LastCaregroupId] DEFAULT ((0)) NOT NULL,
    [SubTotalSalesPrice]        NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__SubTo__520B5D0C] DEFAULT ((0)) NOT NULL,
    [GrandTotalTaxes]           NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__Grand__52FF8145] DEFAULT ((0)) NOT NULL,
    [DeductibleValue]           NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__Deduc__7648BD82] DEFAULT ((0)) NOT NULL,
    [InsurerCoveredValue]       NUMERIC (20, 2) CONSTRAINT [DF__ServiceOr__Insur__6C553EF4] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ServiceOrderDetailDistribution__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceOrderDetailDistribution_RevenueControlDetail] FOREIGN KEY ([RevenueControlDetailId]) REFERENCES [Billing].[RevenueControlDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailDistribution_ServiceOrderDetail] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceOrderDetailDistribution] NOCHECK CONSTRAINT [FK_ServiceOrderDetailDistribution_RevenueControlDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetailDistribution] NOCHECK CONSTRAINT [FK_ServiceOrderDetailDistribution_ServiceOrderDetail];


GO

-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 12-06-2024
-- Description:	Trigger para No permitir generar items en el folio con cantidades en 0
-- =============================================
CREATE TRIGGER [Billing].[TriggerInsertItemCountZero]
   ON [Billing].[ServiceOrderDetailDistribution]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM INSERTED WHERE Quantity <=0) BEGIN
		THROW 51000, 'No se pueden insertar items en el folio con cantidades en 0', 1
	END
END
GO

-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 20-01-2016
-- Description:	Trigger para eliminar las distribuciones de los productos que han sido devueltos totalmente y llegan a cantidad de 0
-- =============================================
CREATE TRIGGER [Billing].[TriggerDeleteProductCountZero]
   ON [Billing].[ServiceOrderDetailDistribution]
   AFTER UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	delete from [Billing].[ServiceOrderDetailDistribution] where Id in (select Id from inserted where Quantity <= 0)
END
GO
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 28/01/2019
-- Description:	Trigger para que genere error cuando se elimine un distribution y el folio este facturado
-- =============================================
CREATE TRIGGER [Billing].[TriggerEliminateDistribution]
   ON [Billing].[ServiceOrderDetailDistribution]
   AFTER delete
AS 
BEGIN
		
		if exists (select 1 
					from DELETED d
					join Billing.RevenueControlDetail rcd WITH (NOLOCK) on rcd.Id = d.RevenueControlDetailId
					where rcd.Status = 2)
		begin
			THROW 51000, 'Validacion TriggerEliminateDistribution: No se puede eliminar un registro de distribucion cuando el folio se encuentra facturado', 1
		end


		if exists (select 1 
					from DELETED d
					join Billing.ServiceOrderDetail sod WITH(NOLOCK) ON d.ServiceOrderDetailId =sod.Id
					JOIN Billing.ServiceOrder so WITH(NOLOCK) ON so.Id=sod.ServiceOrderId
					LEFT JOIN Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK) on sod.Id =sodd.ServiceOrderDetailId
					where	so.Status = 1
							AND sod.Packaging = 0
							AND sod.IsDelete = 0
							AND sod.InvoicedQuantity > 0 
							AND sodd.Id IS NULL)
		begin
			
			THROW 51000,'Validacion TriggerEliminateDistribution: No se puede eliminar un registro de distribucion cuando tenga Cantidades en el detalle de la orden de servicio', 1
		end

END