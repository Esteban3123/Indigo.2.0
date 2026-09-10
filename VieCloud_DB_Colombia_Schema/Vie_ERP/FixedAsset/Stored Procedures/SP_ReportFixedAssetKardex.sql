
-- =============================================
-- Author:		Nicolas Pulido
-- Create date: 21/Marzo/2017
-- Description:	Genera el informe de kardex de activos fijos
-- =============================================
CREATE PROCEDURE [FixedAsset].[SP_ReportFixedAssetKardex]
	@Year int,
	@Month int,
	@parameterInitialPlate varchar(max),
	@parameterFinalPlate varchar(max),
	@parameterInitialCatalog varchar(max),
	@parameterFinalCatalog varchar(max),
	@parameterInitialGroup varchar(max),
	@parameterFinalGroup varchar(max),
	@parameterInitialLocation varchar(max),
	@parameterFinalLocation varchar(max),
	@parameterInitialResponsible varchar(max),
	@parameterFinalResponsible varchar(max)

AS
BEGIN
	
	--Tabla en la que se guardan los datos del reporte
	declare @FixedAssetKardexHeader table(PlateCode varchar(50), ProductName varchar(300), Serie varchar(50), Class int, UnitValue numeric(20,4), ResponsibleNit varchar(max), ResponsibleName varchar(max))
	
	insert into @FixedAssetKardexHeader
	select fapa.Plate, fai.[Description], fapa.Serie, fal.Class, fapa.HistoricalValue, tp.Nit, tp.[Name] from FixedAsset.FixedAssetKardexItem faki
	inner join FixedAsset.FixedAssetPhysicalAsset fapa on fapa.Id = faki.PhysicalAssetId
	inner join FixedAsset.FixedAssetItem fai on fai.Id = fapa.ItemId
	inner join FixedAsset.FixedAssetLocation fal on fal.Id = fapa.LocationId
	inner join FixedAsset.FixedAssetResponsible far on far.Id = fapa.ResponsibleId
	inner join Common.ThirdParty tp on tp.Id = far.ThirdPartyId
	inner join FixedAsset.FixedAssetItemCatalog faic on faic.Id = fai.ItemCatalogId
	inner join GeneralLedger.MainAccounts ma on ma.Id = fapa.MainAccountId
	where YEAR(fapa.AdquisitionDate) = @Year 
	AND MONTH(fapa.AdquisitionDate) = @Month 
	AND fapa.Plate between @parameterInitialPlate and @parameterFinalPlate 
	AND faic.Code between @parameterInitialCatalog and @parameterFinalCatalog 
	AND ma.Number between @parameterInitialGroup and @parameterFinalGroup 
	AND fal.Code between @parameterInitialLocation and @parameterFinalLocation
	AND tp.Nit between @parameterInitialResponsible and @parameterFinalResponsible

	select * from @FixedAssetKardexHeader order by Responsiblenit	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el informe de kardex de activos fijos de la organización para un año y mes específicos. Consolida información del activo físico (placa, serie, valor histórico de adquisición) con su descripción de catálogo, ubicación física, cuenta contable principal y el responsable (tercero) a cargo de cada bien. Permite filtrar por rangos de placa, catálogo, grupo contable, ubicación y responsable (NIT), devolviendo el listado ordenado por responsable para facilitar la trazabilidad y custodia de los activos fijos en reportes contables y de control patrimonial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetKardex';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'PROCEDURE', @level1name = N'SP_ReportFixedAssetKardex';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el informe de kardex de activos fijos adquiridos en un mes/año, filtrando por rangos de placa, catálogo, cuenta contable, ubicación y responsable.', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los activos físicos deben tener movimientos registrados en el kardex (FixedAssetKardexItem); Cada activo físico debe estar asociado a un ítem, ubicación, responsable y cuenta contable principal; El responsable debe estar vinculado a un tercero en Common.ThirdParty; Los parámetros de rango (placa, catálogo, grupo, ubicación, NIT responsable) deben ser comparables como cadenas con BETWEEN', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen activos físicos cuya fecha de adquisición pertenece exactamente al año y mes solicitados; Solo se reportan activos que tengan al menos un registro en el kardex (INNER JOIN con FixedAssetKardexItem); Solo se reportan activos con responsable y tercero asociado (no aparecen activos sin responsable); Los resultados se entregan ordenados por NIT del responsable; Todos los filtros de rango se aplican simultáneamente (AND)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex de activos fijos; Activo físico; Placa; Serie; Valor histórico; Clase de ubicación; Responsable; Tercero (NIT); Catálogo de activos fijos; Cuenta contable principal; Fecha de adquisición', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas con placa, descripción, serie, clase, valor histórico, NIT y nombre del responsable, ordenadas por NIT del responsable, solo para activos cuya AdquisitionDate coincide con el año y mes indicados y cumplen todos los rangos (placa, catálogo, número de cuenta, ubicación, NIT)', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'FixedAsset.FixedAssetKardexItem; FixedAsset.FixedAssetPhysicalAsset; FixedAsset.FixedAssetItem; FixedAsset.FixedAssetLocation; FixedAsset.FixedAssetResponsible; Common.ThirdParty; FixedAsset.FixedAssetItemCatalog; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'FixedAsset', @level1type=N'PROCEDURE', @level1name=N'SP_ReportFixedAssetKardex';
-- GO
