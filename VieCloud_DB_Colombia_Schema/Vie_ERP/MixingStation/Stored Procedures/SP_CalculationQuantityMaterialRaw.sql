
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
							LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = atc.Id
							LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de cálculo de materia prima necesaria para preparar los medicamentos de una campaña de mezclas en la estación de preparación (Mixing Station). Recibe un parámetro XML con los datos de la campaña, reúne todos los componentes (medicamentos, diluyentes y vehículos) de las solicitudes asociadas a la campaña consultando la vista de detalle de campaña con solicitudes, y los detalla de paquetes estándar (PackageDetail) y personalizados (PackagePersonalizedDetail). Agrupa los medicamentos por código para consolidar dosis cuando un mismo medicamento aparece en varias solicitudes, y calcula la dosis requerida total usando la función MixingStation.CalculationQuantityMaterialRaw según el tipo de fórmula. Finalmente cruza el resultado con el inventario físico disponible en bodega para determinar qué productos concretos (presentaciones comerciales) se necesitan y en qué cantidad, apoyando el proceso de dispensación y preparación de mezclas oncológicas o de nutrición parenteral.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_CalculationQuantityMaterialRaw';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula, para los medicamentos requeridos por las solicitudes de una campaña de mezclas, la cantidad de presentaciones de materia prima necesarias del inventario disponible para cubrir la dosis total requerida.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro @xml se recibe pero no se utiliza en el cuerpo (el filtro de campaña está hardcodeado a 51); Debe existir información en MixingStation.ViewListCampaignDetailWithRequests para CampaignDetailId = 51; Los medicamentos deben estar catalogados en Inventory.ATC con ATCEntityId, PharmaceuticalFormId, FormulationType y unidades de medida correctamente parametrizados; Debe existir la bodega con Code = ''001'' en Inventory.Warehouse para que aparezcan disponibilidades; La función escalar MixingStation.CalculationQuantityMaterialRaw debe estar disponible y devolver la dosis equivalente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de campaña usado para filtrar las solicitudes está fijado a 51 (CampaignDetailId = 51), por lo que el procedimiento siempre opera sobre esa campaña independientemente del XML recibido; Solo se consideran productos cuya bodega tenga Code = ''001''; Se excluyen los productos marcados como diluyente (atc.DiluentProduct = 0); Solo se consideran InventoryProduct con Status = 1 (activos); La selección de presentaciones para cubrir la dosis se hace priorizando mayor concentración (ORDER BY concentracion DESC) y, en la consulta de detalle, mayor Weight y Volume; El cálculo de DosisRequerida y de Dosis por presentación se delega a la función MixingStation.CalculationQuantityMaterialRaw en función del TipoFormula, código y unidad; El procedimiento no modifica datos persistentes: solo retorna resultsets', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Campaña de mezclas; Solicitudes de preparación; Medicamento (ATC); Forma farmacéutica; Materia prima / dosis requerida; Concentración y peso/volumen del medicamento; Inventario físico por bodega; Diluyente; Paquete personalizado de mezcla', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve el listado agrupado de medicamentos de la campaña con su DosisRequerida calculada (SELECT * FROM @TableMedicamentos_Agrupados); [RETURN_RESULT] RESULTSET: Por cada medicamento procesado, devuelve el detalle de presentaciones disponibles en bodega ''001'' (SELECT * FROM @tableMedicamentosDetalle) y la presentación seleccionada en cada iteración del ciclo de cobertura; [RETURN_RESULT] RESULTSET: Devuelve la tabla acumulativa final con código, nombre, cantidad de unidades a usar y concentración por presentación elegida (SELECT * FROM @tableAcumulativa); [RETURN_RESULT] RESULTSET: Si ocurre un error, retorna un único registro con CodeMessage = 999 y Message = ERROR_MESSAGE() desde el bloque CATCH', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Mismo medicamento aparece en varias solicitudes de la campaña → Se agrupa por Codigo/Nombre/UnidadOrden/etc. y se suman CantidadDosis y CantidadDosisVolumen antes de calcular la dosis requerida; si @diftmp (dosis pendiente) - Concentracion <= 0 tras consumir un ítem disponible → BREAK del ciclo interno: se considera cubierta la dosis y NO se inserta esa última iteración en la tabla acumulativa else Se inserta en @tableAcumulativa el medicamento elegido con contador de unidades y concentración, y se continúa restando; si ItemType = 2 en ViewListCampaignDetailWithRequests → Se resuelve el ítem como ATC (Inventory.ATC); en caso contrario se busca en PackageDetail (ItemType=1) o PackagePersonalizedDetail (ItemType=3); si Se produce una excepción en el TRY → Devuelve un resultset con CodeMessage=999 y el ERROR_MESSAGE() como Message, y termina con RETURN', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'MixingStation.CalculationQuantityMaterialRaw', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.ViewListCampaignDetailWithRequests; Inventory.ATC; MixingStation.PackageDetail; MixingStation.PackagePersonalizedDetail; Inventory.InventorySupplie; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_CalculationQuantityMaterialRaw';
-- GO
