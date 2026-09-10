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
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailDistribution__RevenueControlDetailId__INC__ApplyRecoveryFee__DistributionType__GrandTotalDiscount__GrandTot]
    ON [Billing].[ServiceOrderDetailDistribution]([RevenueControlDetailId] ASC)
    INCLUDE([Id], [ServiceOrderDetailId], [Quantity], [GrandTotalSalesPrice], [GrandTotalDiscount], [DistributionType], [ThirdPartySalesPrice], [ThirdPartyPercentage], [ApplyRecoveryFee], [RecoveryFeeType], [SubTotalPatientSalesPrice], [PatientPercentage]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailDistribution__ServiceOrderDetailId__INC__SubTotalPatientSalesPrice__ThirdPartySalesPrice]
    ON [Billing].[ServiceOrderDetailDistribution]([ServiceOrderDetailId] ASC)
    INCLUDE([ThirdPartySalesPrice], [SubTotalPatientSalesPrice]);


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
GO
-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 01/02/2016
-- Description:	Trigger para que genere error cuando hayan datos inconsistentes
-- =============================================
CREATE TRIGGER [Billing].[TriggerValidateData]
   ON [Billing].[ServiceOrderDetailDistribution]
   AFTER INSERT, UPDATE
AS 
BEGIN
	DECLARE @xx AS VARCHAR(MAX)

	IF EXISTS (SELECT 1 FROM inserted WHERE DistributionType = 1)
	BEGIN
		IF EXISTS(SELECT 1 FROM GeneralLedger.CompanySettings WHERE SalePriceIncludeTax =1 )
		OR EXISTS(SELECT 1
					FROM inserted i 
					JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON i.ServiceOrderDetailId = sod.Id
					where sod.GrossValue=0)
		BEGIN
			IF EXISTS
				(
					SELECT 1
					FROM inserted i 
					JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON i.ServiceOrderDetailId = sod.Id 
					WHERE i.DistributionType = 1 AND
						(
							ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice, 2) <> i.GrandTotalSalesPrice 
							AND
							ROUND(sod.InvoicedQuantity * sod.TotalSalesPrice,-2) <> i.GrandTotalSalesPrice
						)
				)
				BEGIN			
					SET @xx  = 'Error de datos, no concuerda la operacion (Cantidad * ValorUnitario) = Total ' + CAST((SELECT CONCAT('ServiceOrdeDetailId-B : ', STRING_AGG(ServiceOrderDetailId,','))  FROM inserted) AS VARCHAR);
					THROW 51000, @xx, 1
				end
		END
		ELSE BEGIN
			IF EXISTS
				(
					SELECT 1
					FROM inserted i 
					JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON i.ServiceOrderDetailId = sod.Id
					left JOIN GeneralLedger.GeneralLedgerIVA iva WITH(NOLOCK) on sod.IvaId = iva.Id
					WHERE i.DistributionType = 1 AND
						(	( i.SubTotalSalesPrice=0 AND
								ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
										-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity) , 2) <> i.GrandTotalSalesPrice 
								AND
								ROUND(((sod.GrossValue * sod.InvoicedQuantity) + ((sod.GrossValue * sod.InvoicedQuantity)*(isnull(iva.Percentage,0)/100)))
									-(((sod.GrossValue + (sod.GrossValue*(isnull(iva.Percentage,0)/100))) * (sod.ThirdPartyDiscountPercentage/100)) * sod.InvoicedQuantity) ,-2) <> i.GrandTotalSalesPrice
							) 
							OR ( i.SubTotalSalesPrice >0 AND (i.SubTotalSalesPrice-i.GrandTotalDiscount+i.GrandTotalTaxes) <> i.GrandTotalSalesPrice)
							
						)
				)
				BEGIN					
					SET @xx = 'Error de datos, no concuerda la operacion (Cantidad * ValorUnitario) = Total ' + CAST((SELECT CONCAT('ServiceOrdeDetailId-C : ', STRING_AGG(ServiceOrderDetailId,','))  FROM inserted) AS VARCHAR);
					THROW 51000, @xx, 1
				end	
		END		
	END

	IF EXISTS
	(
		SELECT 1
		FROM inserted i 
		JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = i.ServiceOrderDetailId 
		WHERE sod.SettlementType = 3 AND i.GrandTotalSalesPrice > 0
	)
	BEGIN
		THROW 51000, 'Error de datos, El item se encuentra incluido al 100% pero el valor es mayor a 0', 1
	END

	IF EXISTS
	(
		SELECT 1
		FROM inserted sodd
		WHERE sodd.GrandTotalSalesPrice > 0
			AND sodd.ApplyRecoveryFee = 2 
			AND sodd.RecoveryFeeType = 3
			AND sodd.PatientPercentage > 0
			AND sodd.SubTotalPatientSalesPrice = 0
			AND (sodd.GrandTotalSalesPrice - ROUND(sodd.ThirdPartyPercentage * sodd.GrandTotalSalesPrice / 100, 0)) > 0			
	)
	BEGIN
		THROW 51000, 'Error de datos, El Copago no fue calculado', 1
	END
END
GO
DISABLE TRIGGER [Billing].[TriggerValidateData]
    ON [Billing].[ServiceOrderDetailDistribution];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor cubierto por aseguradora (equivalente a copago asegurador). Solo se completa en folios madre o derivados paciente-aseguradora; en Colombia siempre es 0. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor cubierto Aseguradora:                   es como el Copago de la aseguradora, este campo solo estará lleno para Folios Cuentas Madre o sus derivados(paciente - aseguradora) en Colombia siempre sera 0', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'InsurerCoveredValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del deducible aplicado al servicio/producto. Monto a cargo del paciente antes de cobertura aseguradora. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del deducible', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DeductibleValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DeductibleValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuestos totales calculados como (SubTotalSalesPrice - GrandTotalDiscount) × IVA%. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'(SubTotalSalesPrice - GrandTotalDiscount) * IVA%', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalTaxes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor bruto sin IVA; calculado como valor unitario × cantidad. Base para cálculo de impuestos y distribuciones. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'valor bruto (sin IVA) x # cantidades', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del grupo de atención (caregroup) utilizado en última retarificación. Bandera interna para retarificaciones en distribución; sin relación directa con tabla de grupos. Tipo: INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo de atención de la última retarificación. Éste campo es usado como una bandera para las retarificaciones realizadas en el proceso de distribución. Por lo tanto no tiene relación con la tabla de grupos de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje del valor total a cargo del paciente por concepto de cuota de recuperación (copago, moderadora, etc). Tipo: NUMERIC(5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje del valor total cobrado por el Producto / Servicio a cargo del paciente por concepto de cuota de recuperacion (RecoveryFeeType)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total a cargo del paciente por cuota de recuperación, calculado según normativa vigente. Puede variar si se aplica descuento sobre valor de recuperación. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total calculado por el Producto / Servicio al paciente por concepto de cuotas de recuperacion. (RecoveryFeeType). Este valor es el calculado segun se establece en la ley, pero puede variar si al paciente se le concede un descuento sobre el valor de recuperacion.  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cuota de recuperación: 1-Ninguna, 2-Cuota Moderadora, 3-Copago, 4-Bono, 5-Cuota Recuperación. TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Cuota de Recuperacion  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Cuota de Recuperación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de aplicación de cuota: 0-No aplica, 1-Disponible para cálculo, 2-En proceso de aplicación sobre item. TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica 3 estados para el calculo de la cuota de recuperacion  0 - No Aplica Cuota de Recuperacion  1 - Disponible para hacer el calculo de la cuota de recuperacion  2 - Se esta aplicando cuota de recuperacion sobre el item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje cobrado al tercero (asegurador/empleador). Difiere del 100% cuando se aplica cuota de recuperación. Tipo: NUMERIC(5,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al porcentaje cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del 100% cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado al tercero (asegurador/empleador). Difiere de GrandTotalSalesPrice cuando aplica cuota de recuperación. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del GrandTotalSalesPrice cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución: 1-Ninguno, 2-Normal, 3-Por Corte de Cuentas, 4-Por Unidad Funcional, 5-NoPOS. TINYINT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Distribucion  1 - Ninguno  2 - Distribucion Normal  3 - Distribucion por Corte de Cuentas  4 - Distribucion por Unidad  5 - Distribucion NoPOS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento total aplicado al item. Se distribuye junto con el item en retarificaciones y folios derivados. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el total del descuento que se le realizo al item, Este descuento tambien se distribuye cuando el item es distribuido', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado por producto/servicio (heredado de ServiceOrderDetail). Base para distribución entre paciente, asegurador y terceros. Tipo: NUMERIC(20,2)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio establecido en GrandTotalSalesPrice de ServiceOrderDetail  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto/servicio en el detalle de orden. Multiplicador para cálculos de valores unitarios. Tipo: INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicio asociado. Referencia FK a [Billing].[ServiceOrderDetail]. Tipo: INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del folio (revenue control). Referencia FK a [Billing].[RevenueControlDetail]. Vincula distribución a factura. Tipo: INT', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del folio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de registro de distribución del folio. Clave primaria, se auto-incrementa. Tipo: INT IDENTITY', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la distribucion del folio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Distribución financiera de los ítems de órdenes de servicio (detalle de facturación): registra cómo se reparte el valor de cada servicio facturado entre el asegurador/tercero pagador y el paciente, incluyendo descuentos, impuestos, cuota de recuperación y deducibles.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailDistribution';

GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailDistribution_RevenueControlDetailId]
    ON [Billing].[ServiceOrderDetailDistribution]([RevenueControlDetailId] ASC)
    INCLUDE([ServiceOrderDetailId], [SubTotalPatientSalesPrice], [DeductibleValue], [SubTotalSalesPrice], [GrandTotalDiscount], [InsurerCoveredValue]);

GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailDistribution_ServiceOrderDetailId]
    ON [Billing].[ServiceOrderDetailDistribution]([ServiceOrderDetailId] ASC)
    INCLUDE([RevenueControlDetailId]);
