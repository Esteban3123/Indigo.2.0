
-- ===============================================================================================================================
-- Author:		
-- Create date: 09/07/2021
-- Description:	Procedimiento que se encarga de Generar la materia prima para los diferentes productos que tiene la campaña.
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_CalculationQuantityMaterialRaw]
	--@CampaignDetailId int,
	--@IdStock Integer,
	--@IdAlmacen Integer
	@xml as xml 
AS
BEGIN

	--Tabla de para obtener los medicamentos
	Declare @TableMedicamentos_SinAgrupar As Table(id int identity(1,1),Codigo varchar(100), Nombre varchar(200), CantidadDosis decimal(18,2), UnidadOrden varchar(10), CantidadDosisVolumen decimal(18,2), UnidadVolumenOrden varchar(10), TipoFormula Integer,ATCEntityId  integer, PharmaceuticalFormId Integer )
	Declare @TableMedicamentos_Agrupados As Table(id int identity(1,1),Codigo varchar(100), Nombre varchar(200), CantidadDosis decimal(18,2), UnidadOrden varchar(10), CantidadDosisVolumen decimal(18,2), UnidadVolumenOrden varchar(10), TipoFormula Integer,ATCEntityId  integer, PharmaceuticalFormId Integer, DosisRequerida decimal(18,2) )

Begin try

SET NOCOUNT ON;

	--Insertamos tos los Medicamentos de las diferentes solicitudes es decir se busca por la camapaña y la campaña tiene diferentes solicitudes y cada solicitud sus medicamentos
		INSERT INTO @TableMedicamentos_SinAgrupar 
							SELECT a2.Code,a2.Name, pd.Quantity, UnitPeso.Code as UnidadOrden, pd.Volume, UnitVolumen.Code as UnidadVolumenOrden, a2.FormulationType, a2.ATCEntityId,  a2.PharmaceuticalFormId
							FROM	MixingStation.ViewListCampaignDetailWithRequests v LEFT JOIN
													 Inventory.ATC a ON a.Id = v.ItemId AND v.ItemType = 2 LEFT JOIN
														  (SELECT        1 ItemType, AtcId, SupplieId, ProductId, PackageId Id, ComponentType, MainMedicine, Thinner, Vehicle,  Quantity, MeasurementUnitId, Volume, VolumeMeasureUnit
														   FROM            MixingStation.PackageDetail
														   UNION ALL
														   SELECT        3 ItemType, AtcId, SupplieId, ProductId, PackagePersonalizedId Id, ComponentType, MainMedicine, Thinner, Vehicle, Quantity, MeasurementUnitId, Volume, VolumeMeasureUnit 
														   FROM            MixingStation.PackagePersonalizedDetail) pd ON pd.Id = v.ItemId AND pd.ItemType = v.ItemType LEFT JOIN
													 Inventory.ATC a2 ON a2.Id = pd.AtcId LEFT JOIN
													 Inventory.InventorySupplie s ON s.Id = pd.SupplieId LEFT JOIN
													 Inventory.InventoryProduct prod ON prod.Id = pd.ProductId LEFT JOIN 
													 Inventory.InventoryMeasurementUnit UnitPeso on UnitPeso.id = pd.MeasurementUnitId  LEFT JOIN
													 Inventory.InventoryMeasurementUnit UnitVolumen on UnitVolumen.id = pd.VolumeMeasureUnit
							where CampaignDetailId = 51



			---Agrupamos por si viene el mismo medicamento por varias solicitudes
			INSert into @TableMedicamentos_Agrupados
			select Codigo, Nombre, sum(CantidadDosis) as CantidadDosis , UnidadOrden, sum(CantidadDosisVolumen) as CantidadDosisVolumen, UnidadVolumenOrden, TipoFormula, ATCEntityId, PharmaceuticalFormId,
				   MixingStation.CalculationQuantityMaterialRaw (TipoFormula,Codigo, sum(CantidadDosis), UnidadOrden) AS 'DosisRequerida'
			from @TableMedicamentos_SinAgrupar A
			group by  Codigo,Nombre, CantidadDosis, UnidadOrden, CantidadDosisVolumen, UnidadVolumenOrden, TipoFormula,ATCEntityId, PharmaceuticalFormId

        --select * from @TableMedicamentos_SinAgrupar
		select * from @TableMedicamentos_Agrupados 		   		
			---Buscar en Inventarios
		declare @Cantidadregistros as int = (select COUNT(ID) from @TableMedicamentos_Agrupados )	
		declare @Contador as int = 1
		declare @tableAcumulativa as table(Id int identity(1,1), Code varchar(150), NombreProducto varchar(250), cantidad int, concentracion decimal(18,2))

		--ciclo medicamentos
		WHILE @Contador <= @Cantidadregistros BEGIN
			
			 declare @ATCEntityId as int
			 declare @PharmaceuticalFormId as int
			 declare @DosisRequerida as decimal(18,2)
			 declare @CodigoMedicamentoGenerico as varchar(150)
			 declare @NombreMedicamentoGenerico as varchar(150)
			 select @ATCEntityId = ATCEntityId, @PharmaceuticalFormId = PharmaceuticalFormId, @DosisRequerida = DosisRequerida, @CodigoMedicamentoGenerico = Codigo , @NombreMedicamentoGenerico = Nombre  from @TableMedicamentos_Agrupados where id = @Contador 


			declare @tableMedicamentosDetalle as table(id int identity(1,1),CodigoMedicamento varchar(150),NombreMedicamento varchar(150),Disponibles int,Weight decimal(18,2),Volume decimal(18,2),Concentracion decimal(18,2))
			delete from @tableMedicamentosDetalle
			
		    

			insert into @tableMedicamentosDetalle
			select * from (
						select atc.Code As 'Code Medicamento', atc.Name  as 'Name Medicamento',sum(isnull(phy.Quantity,0)) as Disponibles,atc.Weight,atc.Volume,MixingStation.CalculationQuantityMaterialRaw (FormulationType,atc.Code, atc.Weight , UnitPeso.Code) AS Dosis			
						from  Inventory.ATC atc
							LEFT OUTER JOIN Inventory.InventoryProduct B on B.Status = 1 AND B.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy on phy.ProductId = B.Id
							--AND phy.WarehouseId IN (SELECT Id from @Almacenes)
							INNER JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
							LEFT JOIN Inventory.InventoryMeasurementUnit UnitPeso on UnitPeso.Id = atc.WeightMeasureUnit   
							LEFT JOIN Inventory.InventoryMeasurementUnit UnitVolumen on UnitVolumen.id = atc.VolumeMeasureUnit
						where atc.DiluentProduct = 0 AND atc.ATCEntityId = @ATCEntityId and atc.PharmaceuticalFormId = @PharmaceuticalFormId  AND E.Code IN ('001') 
						GROUP BY E.Code,CodeCUM, atc.Code, atc.Name , atc.FormulationType, atc.Weight, UnitPeso.Code, atc.Volume, UnitVolumen.Code 
						--order by Weight desc, Volume  desc
						) as tmp 
			--where tmp.Dosis <= @DosisRequerida  
			order by tmp.Weight desc, tmp.Volume  desc

			select * from @tableMedicamentosDetalle




			declare @Restante as decimal(18,2) = 0
			declare @Concentracion as decimal(18,2) 
			declare @cantidad as int
			declare @diferencia as int
			declare @CodigoMedicamento as varchar(150)
			declare @NombreMedicamento as varchar(250)


			WHILE @DosisRequerida > 0 BEGIN
					

						DECLARE	@diftmp as decimal(18,2)	=@DosisRequerida
						declare @contador2 AS INT = 0
					WHILE 1=1 BEGIN
						SET @CONTADOR2 = @CONTADOR2 +1
					select top 1 @DIFERENCIA = Diferencias, @CodigoMedicamento = CodigoMedicamento, @NombreMedicamento = NombreMedicamento,@Concentracion = Concentracion from (
					select  @diftmp - Concentracion as Diferencias ,Concentracion ,CodigoMedicamento, NombreMedicamento  from @tableMedicamentosDetalle
					) as tmp  order by concentracion desc

				select top 1  CodigoMedicamento,  NombreMedicamento, Concentracion, Diferencias from (
						select @diftmp - Concentracion as Diferencias ,Concentracion ,CodigoMedicamento, NombreMedicamento  from @tableMedicamentosDetalle
						) as tmp  order by concentracion desc

						set @diftmp = @diftmp - @Concentracion
						
						IF @diftmp <=0 BEGIN
							BREAK;
							END ELSE BEGIN
								insert into @tableAcumulativa 
			                   select @CodigoMedicamento, @NombreMedicamento, @CONTADOR2 ,@Concentracion 
							
							END
					END

					
				

					--if @DosisRequerida <= 0
					if @DosisRequerida > 0
						break;
					
		  END

		 
			--declare @Cantidad2 as int = (select COUNT(ID) from @tableMedicamentosDetalle )	
			--declare @Contador2 as int = 1	
			----ciclo medicamentos detalle
			--WHILE @Contador2 <= @Cantidad2 BEGIN
					 

			--	set @Contador2 +=1
			--END



			set @Contador =@Contador + 1  
		END
			

		select * from @tableAcumulativa
	
	End try
	Begin Catch

		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		return

	End Catch

END