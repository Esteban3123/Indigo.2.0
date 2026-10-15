
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