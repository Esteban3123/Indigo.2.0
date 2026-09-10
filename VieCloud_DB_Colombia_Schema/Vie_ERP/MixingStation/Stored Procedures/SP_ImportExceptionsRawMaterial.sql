CREATE PROCEDURE [MixingStation].[SP_ImportExceptionsRawMaterial] 
	@XmlObject as Xml
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		CountFields INT, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		TypeDetail VARCHAR(20), 
		Codigo VARCHAR(20), 
		SupplieBy VARCHAR(25)
	)
	
	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		Id INT IDENTITY PRIMARY KEY, 
		StatusField INT, 
		MessageField VARCHAR(MAX), 
		-- ContractExternalClientsDetail --
		TypeDetail VARCHAR(20), 
		Codigo VARCHAR(20),
		SupplieBy VARCHAR(25),
		ItemId int,
		ItemName VARCHAR(MAX)
	)
	
	BEGIN TRY

		INSERT INTO @TableXmlObject
			(CountFields, StatusField, MessageField, TypeDetail, Codigo, SupplieBy)
			SELECT 
				t.x.value('CountFields[1]','int') as CountFields,
				t.x.value('StatusField[1]','int') as StatusField,
				t.x.value('MessageField[1]','varchar(100)') as MessageField,
				t.x.value('TypeDetail[1]','VARCHAR(20)') as TypeDetail,
				t.x.value('Codigo[1]','varchar(20)') as Codigo,
				t.x.value('SupplieBy[1]','VARCHAR(25)') as SupplieBy

			FROM @XmlObject.nodes('/Data/Row') t(x)

		/************************************* VALIDACIONES MASIVAS *************************************/
				IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
			GROUP BY t.Codigo
			HAVING COUNT(*) > 1
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'Existe mas de un registro asociado al producto ' + t.Codigo
			FROM @TableXmlObject t
			JOIN
			(
				SELECT t.Codigo
				FROM @TableXmlObject t
				WHERE t.StatusField = 1
				GROUP BY t.Codigo
				HAVING COUNT(*) > 1
			) t2 ON t.Codigo = t2.Codigo
			WHERE t.StatusField = 1
		END
		----------------------------------------
		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND (t.TypeDetail > 4)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor del tipo del registro ' + convert(VARCHAR(3),t.TypeDetail) + ' no puede ser mayor a 4'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND (t.TypeDetail > 4)
		END
		------------------------------------------
		IF EXISTS
		(
			SELECT 1
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND (t.SupplieBy > 2)
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El valor del tipo del registro ' + convert(VARCHAR(3),t.SupplieBy) + ' no puede ser mayor a 2'
			FROM @TableXmlObject t
			WHERE t.StatusField = 1
				AND (t.SupplieBy > 2)
		END
		---------------------------------
		IF  EXISTS
		(
		SELECT 1
			FROM @TableXmlObject t
				INNER JOIN Inventory.ATC ap ON t.Codigo = ap.Code  
			WHERE t.StatusField = 1
				AND t.TypeDetail = 1 
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El código ' + t.Codigo +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionado con un Medicamento'
			FROM @TableXmlObject t
				LEFT JOIN Inventory.ATC ap ON t.Codigo = ap.Code  AND ap.Code = T.Codigo 
			WHERE t.StatusField = 1  AND t.TypeDetail = 1 AND  ap.Id IS NULL
		END
		---------------------------------
		IF  EXISTS
		(
		SELECT 1
			FROM @TableXmlObject t
				INNER JOIN Inventory.InventorySupplie ap ON t.Codigo = ap.Code  
			WHERE t.StatusField = 1
				AND t.TypeDetail = 2
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El código ' + t.Codigo +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionado con un Insumo'
			FROM @TableXmlObject t
				LEFT JOIN Inventory.InventorySupplie ap ON t.Codigo = ap.Code AND ap.Code = T.Codigo 
			WHERE t.StatusField = 1  AND t.TypeDetail = 2 AND  ap.Id IS NULL
		END
		---------------------------------
		IF  EXISTS
		(
		SELECT 1
			FROM @TableXmlObject t
				INNER JOIN Inventory.InventoryProduct ap ON t.Codigo = ap.Code  
			WHERE t.StatusField = 1
				AND t.TypeDetail = 3
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El código ' + t.Codigo +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionado con un Producto'
			FROM @TableXmlObject t
				LEFT JOIN Inventory.InventoryProduct ap ON t.Codigo = ap.Code AND ap.Code = T.Codigo 
			WHERE t.StatusField = 1  AND t.TypeDetail = 3 AND  ap.Id IS NULL
		END
		---------------------------------
		IF  EXISTS
		(
		SELECT 1
			FROM @TableXmlObject t
				INNER JOIN Contract.CUPSEntity ap ON t.Codigo = ap.Code  
			WHERE t.StatusField = 1
				AND t.TypeDetail = 4
		)
		BEGIN
			UPDATE t
				SET t.StatusField = 0,
					t.MessageField = 'El código ' + t.Codigo +  ' del registro ' + convert(VARCHAR(3),t.Id) + ' no existe o no se encuentra relacionado con un Servicio'
			FROM @TableXmlObject t
				LEFT JOIN Contract.CUPSEntity ap ON t.Codigo = ap.Code AND ap.Code = T.Codigo 
			WHERE t.StatusField = 1  AND t.TypeDetail = 4 AND  ap.Id IS NULL
		END

		/************************************* INSERTAR ERRORES *************************************/
				INSERT INTO @TableResult 
			(
				StatusField, MessageField, TypeDetail, Codigo, SupplieBy
			)
			SELECT StatusField, MessageField, TypeDetail, Codigo, SupplieBy
			FROM @TableXmlObject
			WHERE StatusField = 0
			
		/********************************************************************************************/

		INSERT INTO @TableResult 
			(
				StatusField, MessageField, TypeDetail, Codigo, SupplieBy, ItemId, ItemName 
			)
			SELECT 1, '', TypeDetail, Codigo, SupplieBy,
			case 
						when b.Id is not null then b.Id
						when c.Id is not null then c.Id 
						when d.Id is not null then d.id
						when e.Id is not null then e.Id 
			end ItemId,
			case 
						when b.Name is not null then b.Code +' - '+ b.Name
						when c.SupplieName is not null then c.Code +' - '+ c.SupplieName
						when d.Name is not null then d.Code +' - '+ d.Name
						when e.Description is not null then e.Code +' - '+ e.Description 
			end ItemName
			FROM @TableXmlObject a
				LEFT join Inventory.ATC b on a.Codigo = b.Code  
				LEFT join Inventory.InventorySupplie c on a.Codigo = c.Code
				LEFT join Inventory.InventoryProduct d on a.Codigo = d.Code  
				LEFT join Contract.CUPSEntity e on a.Codigo = e.Code
			WHERE StatusField = 1

	END TRY
	BEGIN CATCH
		INSERT INTO @TableResult (StatusField, MessageField)
		VALUES (0, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as VARCHAR(5)))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de la estación de mezclas que recibe un XML con un listado de materias primas (medicamentos, insumos, productos y servicios) y valida cada ítem antes de importarlo. Realiza validaciones masivas: detecta códigos duplicados en el XML, verifica que el tipo de detalle no supere el valor 4, que el campo de suministro no supere el valor 2, y que cada código exista en el catálogo correspondiente según su tipo (medicamento en ATC, insumo en InventorySupplie, producto en InventoryProduct, servicio CUPS en CUPSEntity). Devuelve un resultado por fila indicando si fue aceptada o rechazada, con el mensaje de error descriptivo en caso de fallo, y en caso de éxito resuelve el identificador y nombre del ítem encontrado en el catálogo maestro. Se usa en el proceso de importación de excepciones de materias primas para la estación de mezclas farmacéuticas, garantizando la integridad referencial de cada insumo o medicamento antes de registrarlo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ImportExceptionsRawMaterial';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida un lote XML de excepciones de materia prima (medicamentos, insumos, productos o servicios CUPS) verificando duplicados, rangos de tipo y existencia del código en los catálogos correspondientes, y devuelve resultados con errores o ítems resueltos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data/Row con los nodos CountFields, StatusField, MessageField, TypeDetail, Codigo y SupplieBy.; Cada fila debe llegar con StatusField=1 para ser sometida a las validaciones masivas; las que llegan con otro estado se conservan tal cual.; TypeDetail debe representar el tipo de ítem (1=Medicamento ATC, 2=Insumo, 3=Producto, 4=Servicio CUPS).; Los códigos referenciados deben existir en Inventory.ATC, Inventory.InventorySupplie, Inventory.InventoryProduct o Contract.CUPSEntity según el TypeDetail.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se realiza ninguna escritura permanente en tablas físicas; todo el procesamiento ocurre en variables tipo TABLE.; Las validaciones masivas solo se aplican a filas con StatusField=1; las que ya están en 0 conservan su estado y mensaje original.; El TypeDetail válido está restringido al rango [1..4] y SupplieBy a [1..2].; Un mismo Codigo no puede aparecer más de una vez con StatusField=1 en el mismo lote.; El ItemName devuelto siempre tiene el formato ''Code - Nombre'' usando la columna descriptiva propia de cada catálogo (Name, SupplieName o Description).; Cualquier excepción se captura y se devuelve como una única fila de error sin abortar el lote.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Por cada fila con StatusField=0 (rechazada en validaciones) se inserta el registro con su MessageField de error sin ItemId ni ItemName.; [INSERT] @TableResult: Por cada fila con StatusField=1 se inserta con StatusField=1, mensaje vacío y se resuelve ItemId/ItemName desde ATC, InventorySupplie, InventoryProduct o CUPSEntity vía LEFT JOIN sobre Codigo, concatenando ''Code - Name/SupplieName/Description''.; [UPDATE] @TableXmlObject: Si para un mismo Codigo existe más de un registro con StatusField=1, todos esos registros pasan a StatusField=0 con mensaje ''Existe mas de un registro asociado al producto <Codigo>''.; [UPDATE] @TableXmlObject: Si TypeDetail > 4, se marca StatusField=0 con mensaje ''El valor del tipo del registro <TypeDetail> no puede ser mayor a 4''.; [UPDATE] @TableXmlObject: Si SupplieBy > 2, se marca StatusField=0 con mensaje ''El valor del tipo del registro <SupplieBy> no puede ser mayor a 2''.; [UPDATE] @TableXmlObject: Si TypeDetail=1 y el Codigo no existe en Inventory.ATC, se marca StatusField=0 con mensaje indicando que no se encuentra relacionado con un Medicamento.; [UPDATE] @TableXmlObject: Si TypeDetail=2 y el Codigo no existe en Inventory.InventorySupplie, se marca StatusField=0 con mensaje indicando que no se encuentra relacionado con un Insumo.; [UPDATE] @TableXmlObject: Si TypeDetail=3 y el Codigo no existe en Inventory.InventoryProduct, se marca StatusField=0 con mensaje indicando que no se encuentra relacionado con un Producto.; [UPDATE] @TableXmlObject: Si TypeDetail=4 y el Codigo no existe en Contract.CUPSEntity, se marca StatusField=0 con mensaje indicando que no se encuentra relacionado con un Servicio.; [RETURN_RESULT] @TableResult: Al final se retorna SELECT * FROM @TableResult con la suma de errores e ítems válidos.; [INSERT] @TableResult: En caso de excepción no controlada (CATCH), se inserta una fila con StatusField=0 y MessageField=ERROR_MESSAGE()+'' Linea: ''+ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe más de un registro con StatusField=1 agrupados por Codigo → Marca todos esos registros como inválidos (StatusField=0) por duplicidad.; si TypeDetail > 4 → Rechaza el registro: tipo fuera de rango permitido (1-4).; si SupplieBy > 2 → Rechaza el registro: SupplieBy fuera del rango permitido (1-2).; si TypeDetail = 1 → Valida existencia del Codigo en Inventory.ATC (Medicamento).; si TypeDetail = 2 → Valida existencia del Codigo en Inventory.InventorySupplie (Insumo).; si TypeDetail = 3 → Valida existencia del Codigo en Inventory.InventoryProduct (Producto).; si TypeDetail = 4 → Valida existencia del Codigo en Contract.CUPSEntity (Servicio CUPS).; si StatusField=0 después de validaciones → Se carga al resultado solo con datos del error. else Si StatusField=1, se enriquece con ItemId e ItemName resueltos del catálogo correspondiente.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.InventorySupplie; Inventory.InventoryProduct; Contract.CUPSEntity', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ImportExceptionsRawMaterial';
-- GO
