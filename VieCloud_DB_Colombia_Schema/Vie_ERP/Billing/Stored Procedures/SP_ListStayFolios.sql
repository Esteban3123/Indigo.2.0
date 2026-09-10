
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-01-13
-- Description:	SP que devuelve Lista los folios en los que se encuentra distribuida una estancias
-- =============================================

CREATE PROCEDURE [Billing].[SP_ListStayFolios]
	@StayId int,
	@admissionNumber varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	
	select rcd.Id as IdFolio,
		rcd.FolioOrder,
		sodd.GrandTotalSalesPrice as ValueFolio
	from Billing.ServiceOrderDetail sod with(nolock)
	inner join Billing.ServiceOrderDetailDistribution sodd with(nolock) on sod.Id = sodd.ServiceOrderDetailId
	inner join Billing.RevenueControlDetail rcd with(nolock) on sodd.RevenueControlDetailId = rcd.Id
	inner join Billing.RevenueControl rc with(nolock) on rcd.RevenueControlId = rc.Id
	where sod.HospitalStayId is not null And sod.HospitalStayId = @StayId
		And rc.AdmissionNumber = @admissionNumber

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los folios de facturación en los que está distribuida una estancia hospitalaria específica. Dado el identificador de la estancia y el número de admisión del paciente, recorre el detalle de órdenes de servicio, su distribución financiera y el control de ingresos para devolver cada folio (ID, orden del folio y valor total facturado) al que pertenece dicha estancia. Se usa para conocer en cuántos y cuáles folios quedó repartida la facturación de una hospitalización o estancia dentro de un ingreso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ListStayFolios';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ListStayFolios';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los folios de facturación entre los que se distribuye el valor de una estancia hospitalaria dentro de una admisión específica, junto con el valor asignado a cada folio.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una estancia hospitalaria identificada y un número de admisión asociados al control de ingresos.; La estancia debe tener detalles de orden de servicio con distribución vinculada a un detalle de folio (RevenueControlDetail) del control de ingresos de la admisión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran detalles de orden de servicio asociados a una estancia hospitalaria (HospitalStayId IS NOT NULL).; El cruce entre la estancia y los folios se realiza siempre vía la cadena ServiceOrderDetail → ServiceOrderDetailDistribution → RevenueControlDetail → RevenueControl, garantizando que los folios devueltos correspondan a la admisión indicada.; El valor reportado por folio corresponde al GrandTotalSalesPrice de la distribución del detalle de la orden de servicio, no al total bruto del ítem.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; estancia hospitalaria; admisión; orden de servicio; distribución de valores facturados; control de ingresos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.RevenueControlDetail: Devuelve una fila por cada distribución de detalle de orden de servicio cuya estancia (HospitalStayId) coincide con la solicitada y cuyo control de ingresos (RevenueControl.AdmissionNumber) corresponde a la admisión indicada, exponiendo el folio (IdFolio, FolioOrder) y el valor distribuido (ValueFolio = GrandTotalSalesPrice).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail; Billing.RevenueControl', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ListStayFolios';
-- GO
