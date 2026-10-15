
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