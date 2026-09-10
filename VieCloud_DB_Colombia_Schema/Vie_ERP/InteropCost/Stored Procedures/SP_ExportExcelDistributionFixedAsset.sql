-- =============================================
-- Author:		Cristhian Mauricio Salazar
-- Create date: 08/11/2016
-- Description:	SP para exportar los datos de la distribucion de activos fijos
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_ExportExcelDistributionFixedAsset]
	@Container varchar(20),
	@Year int,
	@Month int
AS
BEGIN
	
	declare @Sql nvarchar(max) = '
	select act.AACCODACT + '' - '' + pro.APRNOMBRE as Activo, cal.ACADEPMEN as Valor, cen.CCCODIGO + '' - '' + cen.CCNOMBRE as CentroCosto, pc.Code + '' - '' + pc.Name as CentroProduccion
	from '+ @Container +'.dbo.AFNCALDEP cal
	inner join '+ @Container +'.dbo.AFNACTIVO act on act.OID = cal.AFNACTIVO
	inner join '+ @Container +'.dbo.AFNPRODUC pro on pro.OID = act.AFNPRODUC
	inner join '+ @Container +'.dbo.CTNCENCOS cen on cen.OID = cal.CTNCENCOS
	inner join '+ @Container +'.dbo.AFNDEPRECI dep on dep.OID = cal.AFNDEPRECI
	left join InteropCost.ProductionCenterCostCenter pcc on pcc.[CostCenterId] = cal.CTNCENCOS
	left join InteropCost.ProductionCenter pc on pc.Id = pcc.[ProductionCenterId]
	where cal.ACADEPMEN > 0 and year(dep.ACAFECCHCI) = '+ cast(@Year as varchar(20)) +' and month(dep.ACAFECCHCI) = ' + cast(@Month as varchar(20))

	exec sp_sqlexec @Sql

	--select act.AACCODACT + ' - ' + pro.APRNOMBRE as Activo, cal.ACADEPMEN as Valor, cen.CCCODIGO + ' - ' + cen.CCNOMBRE as CentroCosto, pc.Code + ' - ' + pc.Name as CentroProduccion
	--from DGEMPRES03.dbo.AFNCALDEP cal
	--inner join DGEMPRES03.dbo.AFNACTIVO act on act.OID = cal.AFNACTIVO
	--inner join DGEMPRES03.dbo.AFNPRODUC pro on pro.OID = act.AFNPRODUC
	--inner join DGEMPRES03.dbo.CTNCENCOS cen on cen.OID = cal.CTNCENCOS
	--inner join DGEMPRES03.dbo.AFNDEPRECI dep on dep.OID = cal.AFNDEPRECI
	--left join InteropCost.ProductionCenterCostCenter pcc on pcc.[CostCenterId] = cal.CTNCENCOS
	--left join InteropCost.ProductionCenter pc on pc.Id = pcc.[ProductionCenterId]

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que exporta a Excel la distribución mensual de la depreciación de activos fijos, filtrando por contenedor (empresa/base de datos), año y mes. Para cada activo, retorna el código y nombre del activo, el valor de depreciación mensual calculada, el centro de costo contable al que pertenece y el centro de producción asociado (si existe). Combina tablas del módulo de activos fijos (AFNCALDEP, AFNACTIVO, AFNPRODUC, AFNDEPRECI, CTNCENCOS) con las tablas de interoperabilidad de costos (ProductionCenter, ProductionCenterCostCenter) para vincular centros de costo con centros de producción. Se usa en contabilidad de costos para distribuir y reportar el gasto por depreciación de activos fijos entre las unidades funcionales o centros de producción de la institución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Exporta la distribución de depreciación de activos fijos por centro de costo y centro de producción para un mes y año dados, consultando dinámicamente la base de datos del contenedor indicado.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de base de datos recibido en @Container debe existir y contener las tablas AFNCALDEP, AFNACTIVO, AFNPRODUC, CTNCENCOS y AFNDEPRECI en el esquema dbo.; Debe existir relación previa cargada en InteropCost.ProductionCenterCostCenter y InteropCost.ProductionCenter para resolver el centro de producción (de lo contrario aparece como NULL por el LEFT JOIN).; @Year y @Month deben corresponder a la fecha de inicio de depreciación (AFNDEPRECI.ACAFECCHCI) que se desea consultar.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros con valor de depreciación mensual positivo (ACADEPMEN > 0); las filas con valor cero o negativo se excluyen.; El filtro temporal se basa exclusivamente en la fecha ACAFECCHCI de AFNDEPRECI descomponiéndola en año y mes.; La relación con centro de producción es opcional (LEFT JOIN): un activo sin mapeo en ProductionCenterCostCenter aún se reporta, pero con CentroProduccion nulo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Activo fijo; Depreciación mensual; Centro de costo; Centro de producción; Distribución de activos fijos', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas con Activo (código + nombre), Valor (ACADEPMEN), CentroCosto (código + nombre) y CentroProduccion (Code + Name) solo cuando ACADEPMEN > 0 y year(ACAFECCHCI)=@Year y month(ACAFECCHCI)=@Month.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_sqlexec', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'AFNCALDEP; AFNACTIVO; AFNPRODUC; CTNCENCOS; AFNDEPRECI; InteropCost.ProductionCenterCostCenter; InteropCost.ProductionCenter', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_ExportExcelDistributionFixedAsset';
-- GO
