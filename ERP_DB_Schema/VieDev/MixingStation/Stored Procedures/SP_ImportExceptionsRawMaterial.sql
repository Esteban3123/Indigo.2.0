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