
CREATE PROCEDURE [MedicalHistory].[ListFinishedProductsDevolution]
@Paciente varchar(25),
@Ingreso char(10),
@Centro  char(10),
@UnidadFuncional char(10)
AS
BEGIN
	SET NOCOUNT ON;

	Select distinct CAST(0 as bit) as Seleccion,  cum.Id as IdDetailPhysicalCUM, p.MainDrugCode, p.FullProductName, cum.BatchCode, 1 as CantidadDevolver,CONVERT(VARCHAR(50),cum.GroupingCodeDose)
	, CAST('' as varchar) as IdHCMOANULB , CAST('' as varchar(100)) as DevolutionObservations, p.Origin
	From MedicalHistory.ProductSusceptibleMixingStation p 
		inner join MedicalHistory.PharmaDose d on p.CodeSusceptibleMixingStation = d.CodeSusceptibleMixingStation and AppliedDose = 0 --and p.MainDrugCode = d.ProductCode 
		inner join MedicalHistory.DetailPhysicalCUM cum ON cum.GroupingCodeDose = D.GroupingCodeDose  and UsedQuantity  < cum.DispensedQuantity  
		inner join Inventory.InventoryProduct pro on  pro.Id = cum.ProductId 
		inner join Inventory.ATC atc on  atc.Id = pro.ATCId  
		inner join  HCFISIPRO fis on fis.ID = cum.IDHCFISIPRO  AND IPCODPACI = @Paciente 
	Where 
		fis.IPCODPACI = @Paciente  and fis.NUMINGRES = @Ingreso and p.CenterAttentionCode = @Centro 
		and not exists (select IdDetailPhysicalCUM  from HCDEVMEDD a where a.IdDetailPhysicalCUM = cum.Id and a.PROESTADO IN ('1','2'))  
		---and FunctionalUnitCode = '11011      ' 
END