
-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-06-22
-- Description:	Procedimiento que se encarga de validar para el CopyPaste e Importar las solicitudes de inventario del detalle de medicamentos,insumos o otros
-- =====================================================
CREATE PROCEDURE [Inventory].[SP_CopyPasteAndImportRequestsOtherDetail]
    @InventoryRequestDetailOther AS XML	
AS
BEGIN
	SET NOCOUNT ON

	--Tabla temporal de los detalles para el return
	DECLARE @Detail TABLE
	(
		RequestDetailOtherId INT,			
		ComponentType VARCHAR(1),
		ComponentTypeName VARCHAR(40),		
		ProductCode VARCHAR(30),
		SourceCodeName VARCHAR(300),
		consumptionUnit VARCHAR(400), 
		Quantity VARCHAR(20), 
		Description VARCHAR(300), 

		CodeResult INT, 
		MessageResult VARCHAR(300)
	)

	BEGIN TRY
	
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail		
				SELECT  0 AS RequestDetailOtherId,
						t.x.value('ComponentType[1]','VARCHAR(1)'),						
						'---' AS ComponentTypeName,					
						t.x.value('ProductCode[1]','VARCHAR(30)'),
						'---' AS SourceCodeName, 
						'---' AS consumptionUnit, 
						t.x.value('Quantity[1]','VARCHAR(20)'),
						t.x.value('Description[1]','VARCHAR(300)'),
						-------------------------------
						000 as CodeResult,
						'Producto agregado correctamente' as MessageResult
				FROM @InventoryRequestDetailOther.nodes('/InventoryRequest/InventoryRequestDetailOther') t(x)

					
			/********************************************************** VALIDACIONES DATOS VACIOS *************************************************************************/
	
			--valido que se ingreso un tipo			
			IF EXISTS (SELECT 1 FROM @Detail d WHERE d.ComponentType IS NULL OR d.ComponentType = '') 
			BEGIN
				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = 'El campo tipo esta vacio '
				FROM @Detail D						
			END

			--valido que se ingreso un producto			
			IF EXISTS (SELECT 1 FROM @Detail d WHERE d.ProductCode IS NULL OR d.ProductCode = '') 
			BEGIN
				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = 'El campo producto esta vacio '
				FROM @Detail D						
			END
		
			--valido que se ingreso una cantidad
			IF EXISTS (SELECT 1 FROM @Detail d WHERE d.Quantity IS NULL OR d.Quantity = '' ) 
			BEGIN
				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = 'La cantidad esta vacia '
				FROM @Detail D						
			END

			/********************************************************** VALIDACIONES FORMATOS *************************************************************************/

			-- Validar que el tipo ingresado sea numerico
			UPDATE d
				SET d.CodeResult = 999,
					d.MessageResult = CONCAT('El tipo del código : ',D.ProductCode,' No es numerico')

			FROM @Detail D
			WHERE d.CodeResult = 000 AND ISNUMERIC(D.ComponentType) = 0

			-- Validar que el valor ingresado sea una cantidad
			UPDATE d
				SET d.CodeResult = 999,
					d.MessageResult = CONCAT('La cantidad asociada al producto : ',D.ProductCode,' No es numerica')
			FROM @Detail D
			WHERE d.CodeResult = 000 AND ISNUMERIC(D.Quantity) = 0
			
			-- Validar que el valor ingresado sea una cantidad mayor o igual a 1'
			UPDATE d
				SET d.CodeResult = 999,
					d.MessageResult = 'La cantidad debe ser mayor o igual a 1'
			FROM @Detail D
			WHERE d.CodeResult = 000 AND D.Quantity <= 0 
					
			------------------------------------------------------------------------------------------------------------------------------------------------------------------

			--------------------------------------------------VERIFICAR SI EXISTE -----------------------------------------------------------------------------------
			-- Validar que el valor ingresado este entre 1 y 3
			UPDATE d
				SET d.CodeResult = 999,
					d.MessageResult = CONCAT('El tipo asociado al código : ',D.ProductCode,' No esta dentro del rango')
			FROM @Detail D
			WHERE  d.CodeResult = 000 AND D.ComponentType NOT BETWEEN 1 AND 3
			

			--ACTUALIZAR EL RequestDetailOtherId GENERAL 
			UPDATE D
				SET D.RequestDetailOtherId = 
			--SELECT 
					   CASE WHEN (D.ComponentType = '1') THEN (SELECT A.Id FROM Inventory.ATC a WHERE a.Code = D.ProductCode )
							WHEN (D.ComponentType = '2') THEN (SELECT s.Id FROM Inventory.InventorySupplie s WHERE s.Code = D.ProductCode) 
							WHEN (D.ComponentType = '3') THEN (SELECT ip.Id FROM Inventory.InventoryProduct ip WHERE ip.Code = D.ProductCode) 							
						END,
					D.ComponentTypeName =
						CASE WHEN (D.ComponentType = '1') THEN	'Medicamento'
							WHEN (D.ComponentType = '2') THEN	'Insumo'
							WHEN (D.ComponentType = '3') THEN   'Producto'						
						END
			FROM @Detail D	

		-------------------------------------------------

			--valido que sea y medicamento y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '1'  AND D.RequestDetailOtherId IS NULL )
			BEGIN		
				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = CONCAT('El código : ',D.ProductCode,' No es esta asociado a un medicamento')
				FROM @Detail D		
			END
		
			--valido que sea un insumo y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '2'  AND D.RequestDetailOtherId IS NULL)
			BEGIN		
				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = CONCAT('El código : ',D.ProductCode,' No es esta asociado a un insumo')
				FROM @Detail D	
			END

			--valido que sea un producto y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '3' AND D.RequestDetailOtherId IS NULL )
			BEGIN		

				UPDATE d
					SET d.CodeResult = 999,
						d.MessageResult = CONCAT('El código : ',D.ProductCode,' No es esta asociado a un Producto')
				FROM @Detail D		
			END

			/******************************************************** TIPO *****************************************************************************/

			--ACTUALIZAR EL NOMBRE DEL TIPO, CODIGO PRODUCTO Y UNIDAD DE EMPAQUE GENERAL 
		
			--valido que sea y medicamento y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '1'  AND D.RequestDetailOtherId IS NOT NULL )
			BEGIN		
				UPDATE d
					SET D.SourceCodeName =  a.Code +' - '+a.Name ,
						D.consumptionUnit =  a.Presentations
				FROM @Detail D		
				JOIN Inventory.ATC a ON a.Id = D.RequestDetailOtherId
			END
		
			--valido que sea un insumo y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '2'  AND D.RequestDetailOtherId IS NOT NULL)
			BEGIN		
				UPDATE D
					SET D.SourceCodeName =  s.Code +' - '+s.SupplieName ,
						D.consumptionUnit = ISNULL(pu.Code +' - '+pu.name,'001 - UNIDAD')
				FROM @Detail D		
				JOIN Inventory.InventorySupplie s  WITH(NOLOCK) ON s.Id = D.RequestDetailOtherId
				JOIN Inventory.InventoryProduct inp WITH(NOLOCK) ON inp.SupplieId = S.Id
				JOIN Inventory.PackagingUnit  pu  WITH(NOLOCK) ON pu.Id = inp.PackagingUnitId
			END

			--valido que sea un producto y que exista
			IF EXISTS(SELECT 1 FROM @Detail d WHERE d.ComponentType = '3' AND D.RequestDetailOtherId IS NOT NULL )
			BEGIN		
				UPDATE d
					SET D.SourceCodeName = ip.Code +' - '+ip.Name ,
						D.consumptionUnit = ISNULL(pu.Code +' - '+pu.name,'001 - UNIDAD')
				FROM @Detail D		
				JOIN Inventory.InventoryProduct ip ON ip.Id = d.RequestDetailOtherId			
				JOIN Inventory.PackagingUnit  pu  WITH(NOLOCK) ON pu.Id = ip.PackagingUnitId
			END

			------------------------------------------------------------------------------------------------------------------------------------------------------------------

			--Valido los datos duplicados
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail d
				WHERE d.CodeResult <> 999
				GROUP BY d.RequestDetailOtherId
				HAVING COUNT(1) > 1
			)
			BEGIN
			UPDATE	D
				SET d.CodeResult = 999,
					d.MessageResult =  CONCAT('Existen datos duplicados : ', D.ProductCode)
				FROM @Detail D				
			END	

			---------------------------------------------------------------------------------------------------------------------------------------------------------------- 			
			SELECT *
			FROM @Detail d

	END TRY
	BEGIN CATCH
		--Se retorna el error
		delete from @Detail
		insert INTo @Detail(CodeResult, MessageResult) values(888, ERROR_MESSAGE())		
		select * from @Detail
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida y procesa líneas de detalle para operaciones de copiar-pegar o importar solicitudes de inventario hospitalario, aceptando un XML con ítems que pueden ser medicamentos (tipo 1, catálogo ATC), insumos médicos (tipo 2, InventorySupplie) o productos generales (tipo 3, InventoryProduct). Verifica que cada ítem tenga tipo, código de producto y cantidad informados, que los valores sean numéricos y estén dentro de rangos permitidos, y que el código exista en el catálogo correspondiente. Retorna un resultado por cada línea indicando si el producto fue agregado correctamente o el motivo del error, facilitando la carga masiva y validación de solicitudes de farmacia e inventario antes de su registro definitivo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y enriquece, a partir de un XML, el detalle de productos (medicamentos, insumos u otros) para operaciones de copiar/pegar o importar solicitudes de inventario, retornando los registros con sus códigos de resultado y mensajes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /InventoryRequest/InventoryRequestDetailOther con nodos ComponentType, ProductCode, Quantity y Description.; Los catálogos Inventory.ATC, Inventory.InventorySupplie e Inventory.InventoryProduct deben contener los códigos referenciados para que la validación de existencia sea exitosa.; Para insumos y productos, debe existir relación con Inventory.PackagingUnit para resolver la unidad de consumo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Detail: Por cada nodo InventoryRequestDetailOther del XML se inserta una fila inicializada con CodeResult=000 y MessageResult=''Producto agregado correctamente''.; [UPDATE] @Detail: Si ComponentType es NULL o vacío en alguna fila, marca CodeResult=999 y MessageResult=''El campo tipo esta vacio''.; [UPDATE] @Detail: Si ProductCode es NULL o vacío, marca CodeResult=999 con mensaje ''El campo producto esta vacio''.; [UPDATE] @Detail: Si Quantity es NULL o vacío, marca CodeResult=999 con mensaje ''La cantidad esta vacia''.; [UPDATE] @Detail: Si ISNUMERIC(ComponentType)=0 y CodeResult sigue en 000, marca error 999 indicando que el tipo no es numérico para el ProductCode.; [UPDATE] @Detail: Si ISNUMERIC(Quantity)=0 y CodeResult=000, marca error 999 indicando que la cantidad no es numérica.; [UPDATE] @Detail: Si Quantity <= 0 y CodeResult=000, marca error 999 con mensaje ''La cantidad debe ser mayor o igual a 1''.; [UPDATE] @Detail: Si ComponentType no está entre 1 y 3 (con CodeResult=000), marca error 999 indicando que el tipo no está dentro del rango.; [UPDATE] @Detail: Resuelve RequestDetailOtherId: ComponentType=''1'' busca Id en Inventory.ATC por Code; ''2'' en Inventory.InventorySupplie por Code; ''3'' en Inventory.InventoryProduct por Code. Asigna ComponentTypeName = ''Medicamento'' / ''Insumo'' / ''Producto'' respectivamente.; [UPDATE] @Detail: Si ComponentType=''1'' y RequestDetailOtherId es NULL, marca 999 con mensaje que el código no está asociado a un medicamento.; [UPDATE] @Detail: Si ComponentType=''2'' y RequestDetailOtherId es NULL, marca 999 con mensaje que el código no está asociado a un insumo.; [UPDATE] @Detail: Si ComponentType=''3'' y RequestDetailOtherId es NULL, marca 999 con mensaje que el código no está asociado a un producto.; [UPDATE] @Detail: Para medicamentos existentes (ComponentType=''1''), SourceCodeName = ATC.Code+'' - ''+ATC.Name y consumptionUnit = ATC.Presentations.; [UPDATE] @Detail: Para insumos existentes (ComponentType=''2''), SourceCodeName = InventorySupplie.Code+'' - ''+SupplieName; consumptionUnit = PackagingUnit.Code+'' - ''+name (a través de InventoryProduct.SupplieId), o ''001 - UNIDAD'' si es NULL.; [UPDATE] @Detail: Para productos existentes (ComponentType=''3''), SourceCodeName = InventoryProduct.Code+'' - ''+Name y consumptionUnit = PackagingUnit.Code+'' - ''+name o ''001 - UNIDAD'' si es NULL.; [UPDATE] @Detail: Si existen filas con el mismo RequestDetailOtherId (COUNT>1) entre las no marcadas como error, se marcan todas las filas con CodeResult=999 y mensaje ''Existen datos duplicados : <ProductCode>''.; [RETURN_RESULT] @Detail: Devuelve el contenido completo de @Detail con códigos y mensajes de validación por fila.; [RETURN_RESULT] @Detail: Ante excepción, vacía @Detail e inserta una fila con CodeResult=888 y MessageResult=ERROR_MESSAGE() que se retorna al cliente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ComponentType = ''1'' → Resuelve Id contra Inventory.ATC y asigna ComponentTypeName=''Medicamento''; enriquece con Code-Name y Presentations.; si ComponentType = ''2'' → Resuelve Id contra Inventory.InventorySupplie y asigna ComponentTypeName=''Insumo''; enriquece con InventoryProduct y PackagingUnit.; si ComponentType = ''3'' → Resuelve Id contra Inventory.InventoryProduct y asigna ComponentTypeName=''Producto''; enriquece con PackagingUnit.; si CodeResult = 000 (fila aún sin error) → Aplica las validaciones de formato (numérico, rango, cantidad>=1) acumulando errores progresivamente. else Si ya tiene CodeResult=999, se omite la validación posterior.; si Existen filas con el mismo RequestDetailOtherId distinto de error → Marca todas las filas como duplicadas con CodeResult=999.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_CopyPasteAndImportRequestsOtherDetail';
-- GO
