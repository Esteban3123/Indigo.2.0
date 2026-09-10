-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-01-25
-- Description:	Procedimiento que se encarga de importar los detalles y movimientos a glosar
-- =============================================
CREATE PROCEDURE [Glosas].[SP_ImportGlosaMovementGlosas] 
	@XmlObject AS XML
AS
BEGIN
	SET NOCOUNT ON

	--/************************************* VARIABLES ************************************/
	
	--Parametros
	DECLARE @InvoiceDetailIdRows INT,
			@InvoiceDetailId INT,
			@InvoiceDetailIdQXRows INT,
			@InvoiceDetailIdQX INT,			
			@Rows INT,
			@Id INT,
			@PendingValueGlosado DECIMAL(18,2),
			@ValueGlosado DECIMAL(18,2)

	--Tabla para almacenar los items del listado que viene en el xml y los resultados a devolver
	DECLARE @TableXmlObject TABLE
	(
		Id INT IDENTITY(1,1),
		--------------------------------
		InvoiceNumber VARCHAR(50),
		InvoiceDetailId INT,
		HandleQX BIT DEFAULT(0),
		InvoiceDetailIdQX INT default(0),
		InvoiceDetailCodeQX INT,
		ServiceCode VARCHAR(20),
		ServiceName VARCHAR(250),
		MainGlosa BIT DEFAULT(0),
		CodeGlosa VARCHAR(50),
		CodeGlosaId INT,
		ResponsibleCode VARCHAR(50),
		ResponsibleId INT,
		ValueGlosadoString VARCHAR(50),
		ValueGlosado DECIMAL(18,2),
		InvoicedValue DECIMAL(18,2) DEFAULT(0),
		RationaleGlosa VARCHAR(MAX),
		DeterminedMainGlosa BIT DEFAULT(0),
		Reiterated BIT DEFAULT(0),
		--------------------------------
		StatusField INT DEFAULT(0), 
		MessageField VARCHAR(MAX)
	)

	BEGIN TRY
		INSERT INTO @TableXmlObject
			(
				InvoiceNumber, InvoiceDetailId, InvoiceDetailCodeQX, ServiceCode, ServiceName,
				CodeGlosa, ResponsibleCode, ValueGlosadoString, RationaleGlosa
			)
			SELECT	t.x.value('Factura[1]','varchar(50)') as InvoiceNumber,
					t.x.value('CodigoDetalle[1]','int') as InvoiceDetailId,
					t.x.value('CodigoQX[1]','int') as InvoiceDetailCodeQX,
					t.x.value('CodigoServicio[1]','varchar(50)') as ServiceCode,
					t.x.value('NombreServicio[1]','varchar(250)') as ServiceName,
					t.x.value('CodigoConcepto[1]','varchar(50)') as CodeGlosa,
					t.x.value('CodigoResponsable[1]','varchar(50)') as ResponsibleCode,
					t.x.value('ValorGlosa[1]','varchar(50)') as ValueGlosadoString,
					t.x.value('Comentario[1]','varchar(max)') as RationaleGlosa
			FROM @XmlObject.nodes('/NewDataSet/Datos') t(x)

		/***********************************************  VALIDACIONES ***********************************************/

		--Se valida que existan detalles a procesar (El nombre de la hoja del archivo debe ser Datos)
		IF NOT EXISTS (SELECT 1 FROM @TableXmlObject) 
		BEGIN
			INSERT INTO @TableXmlObject(StatusField, MessageField)
				SELECT 999, 'No se encontraron detalles para procesar'
		END

		--Se valida que la factura se encuentre pendiente de confirmar recepcion o reiteración
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT
								(
									'La factura ', t.InvoiceNumber, ' se encuentra en estado ', 
									CASE gpg.State
										WHEN 2 THEN 'Pendiente Evaluacion Glosa'
										WHEN 3 THEN 'Pendiente envio de oficio'
										WHEN 5 THEN 'Pendiente evaluacion reiteracion'
										WHEN 6 THEN 'Pendiente conciliacion'
										WHEN 7 THEN 'Pendiente de confirmar factura conciliacion'
										WHEN 8 THEN 'Conciliación'
										WHEN 9 THEN 'Conciliacion parcial'
										WHEN 11 THEN 'Glosa con Respuesta'
										WHEN 12 THEN 'Reiteracion con Respuesta'
										WHEN 13 THEN 'Pendiente confirmar Pago Parcial'
										WHEN 14 THEN 'Confirmado Pago Parcial'
										WHEN 15 THEN 'Traslado a Cobro Juridico'
									END
								)
		FROM @TableXmlObject t
		LEFT JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON t.InvoiceNumber = gpg.InvoiceNumber
		WHERE t.StatusField = 0 AND ISNULL(gpg.State, 1) NOT IN (1, 4)

		--Se identifican las facturas que se estan reiterando
		UPDATE t SET t.Reiterated = 1
		FROM @TableXmlObject t
		JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON t.InvoiceNumber = gpg.InvoiceNumber
		WHERE t.StatusField = 0 AND gpg.State = 4

		--Se valida que el detalle exista para la factura especificada
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('No existe el detalle ', t.InvoiceDetailId, ' relacionado a la factura ', t.InvoiceNumber, ' con el servicio ', t.ServiceCode, ' - ', t.ServiceName)
		FROM @TableXmlObject t
		LEFT JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceDetailId = gid.Id AND t.InvoiceNumber = gid.InvoiceNumber
		WHERE t.StatusField = 0 AND gid.Id IS NULL

		--Identificamos si el detalle maneja QX
		UPDATE t
			SET t.HandleQX = 1
		FROM @TableXmlObject t
		JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceDetailId = gid.Id AND t.InvoiceNumber = gid.InvoiceNumber
		JOIN Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gid.Id = gidqx.InvoiceDetailId
		WHERE t.StatusField = 0

		--se obitiene el id qx
		update t 
		set InvoiceDetailIdQX =gidqx.id 
		FROM @TableXmlObject t
		JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceDetailId = gid.Id AND t.InvoiceNumber = gid.InvoiceNumber
		JOIN Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gid.Id = gidqx.InvoiceDetailId  and t.ServiceCode = gidqx.ServiceCode
		where t.InvoiceDetailCodeQX <> 0 

		--Se valida que el detalle QX exista para el detalle de la factura especificada
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('No existe el detalleQX ', t.InvoiceDetailIdQX, ' relacionado a la factura ', t.InvoiceNumber, ' con el servicio ', t.ServiceCode, ' - ', t.ServiceName)
		FROM @TableXmlObject t
		JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceDetailId = gid.Id AND t.InvoiceNumber = gid.InvoiceNumber
		LEFT JOIN Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gid.Id = gidqx.InvoiceDetailId AND t.InvoiceDetailIdQX = gidqx.Id
		WHERE t.StatusField = 0 AND t.HandleQX = 1 AND gidqx.Id IS NULL

		--Se valida que el concepto de glosa exista y este activo
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' tiene un concepto de glosa que ', t.CodeGlosa, IIF(cg.Id IS NULL, ' no existe', ' esta inactivo'))
		FROM @TableXmlObject t
		LEFT JOIN Common.ConceptGlosas cg WITH (NOLOCK) ON t.CodeGlosa = cg.Code
		WHERE t.StatusField = 0 AND ISNULL(cg.State, 0) = 0

		--Agregamos el Id del concepto de glosa
		UPDATE t
			SET t.CodeGlosaId = cg.Id
		FROM @TableXmlObject t
		JOIN Common.ConceptGlosas cg WITH (NOLOCK) ON t.CodeGlosa = cg.Code
		WHERE t.StatusField = 0

		--Se valida que el responsable exista y este activo
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' tiene un responsable de glosa que ', t.ResponsibleCode, IIF(r.Id IS NULL, ' no existe', ' esta inactivo'))
		FROM @TableXmlObject t
		LEFT JOIN Glosas.Responsible r WITH (NOLOCK) ON t.ResponsibleCode = r.Code
		WHERE t.StatusField = 0 AND ISNULL(r.State, 0) = 0

		--Agregamos el Id del responsable de glosa
		UPDATE t
			SET t.ResponsibleId = r.Id
		FROM @TableXmlObject t
		JOIN Glosas.Responsible r WITH (NOLOCK) ON t.ResponsibleCode = r.Code
		WHERE t.StatusField = 0

		-- Se valida que no existe detalles con el mismo concepto de glosa
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' tiene el concepto de glosa ', t.CodeGlosa, ' repetido')
		FROM @TableXmlObject t
		JOIN
		(
			SELECT t.InvoiceDetailId, IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) InvoiceDetailIdQX, t.CodeGlosaId
			FROM @TableXmlObject t
			GROUP BY t.InvoiceDetailId, IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0), t.CodeGlosaId
			HAVING COUNT(1) > 1
		) td ON t.InvoiceDetailId = td.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = td.InvoiceDetailIdQX AND t.CodeGlosaId = td.CodeGlosaId
		WHERE t.StatusField = 0

		-- Si es una radicación: Se valida que no existe detalles con el mismo concepto de glosa ya agregado
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' ya existe en los movimientos de glosas con el concepto ', t.CodeGlosa)
		FROM @TableXmlObject t
		JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = ISNULL(gmg.InvoiceDetailIdQX, 0) AND t.CodeGlosaId = gmg.CodeGlosaId
		WHERE t.StatusField = 0 AND t.Reiterated = 0

		-- Si es una reiteracion: Se valida que existe el detalle con el mismo concepto de glosa
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' no existe en los movimientos de glosas con el concepto ', t.CodeGlosa)
		FROM @TableXmlObject t
		LEFT JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = ISNULL(gmg.InvoiceDetailIdQX, 0) AND t.CodeGlosaId = gmg.CodeGlosaId		
		WHERE t.StatusField = 0 AND t.Reiterated = 1 AND gmg.Id IS NULL

		-- Se Valida que el valor glosado sea un número válido
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' no tiene un valor glosado válido')
		FROM @TableXmlObject t
		WHERE t.StatusField = 0 AND 
			(
				ISNUMERIC(REPLACE(t.ValueGlosadoString,'.','')) = 0
				OR
				NOT (CAST(REPLACE(t.ValueGlosadoString,'.','') AS DECIMAL(18, 2)) > 0)
			)

		-- Actualizamos el valor glosado
		UPDATE t
			SET t.ValueGlosado = CAST(REPLACE(t.ValueGlosadoString,'.','') AS DECIMAL(18, 2))
		FROM @TableXmlObject t
		WHERE t.StatusField = 0

		-- Actualizamos el valor facturado del detalle
		UPDATE t
			SET t.InvoicedValue = ISNULL(id.ThirdPartySalesPrice, gid.InvoicedValue)
		FROM @TableXmlObject t
		JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceNumber = gid.InvoiceNumber AND t.InvoiceDetailId = gid.Id
		LEFT JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON gid.InvoiceDetailNativeId = id.Id
		WHERE t.StatusField = 0 AND t.HandleQX = 0

		-- Actualizamos el valor facturado del detalle quirurgico
		UPDATE t
			SET t.InvoicedValue = gidqx.InvoicedValue
		FROM @TableXmlObject t
		JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON t.InvoiceNumber = gid.InvoiceNumber AND t.InvoiceDetailId = gid.Id
		JOIN Glosas.GlosaInvoiceDetailQX gidqx WITH (NOLOCK) ON gid.Id = gidqx.InvoiceDetailId AND t.InvoiceDetailIdQX = gidqx.Id
		WHERE t.StatusField = 0 AND t.HandleQX = 1

		-- Se Valida que el valor glosado no supere el valor facturado del detalle
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' posee un valor glosado superior al valor facturado del detalle')
		FROM @TableXmlObject t
		WHERE t.StatusField = 0 AND t.ValueGlosado > t.InvoicedValue

		-- Si es una reiteracion se Valida que el valor reiterado no supere el valor glosado
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El detalle ', t.InvoiceDetailId, ' posee un valor reiterado (', FORMAT(t.ValueGlosado, 'c2', 'es-CO'), ') superior al valor pendiente por glosar del detalle (', FORMAT(gmg.ValuePendingConciliation, 'c2', 'es-CO'), ')')
		FROM @TableXmlObject t
		JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = ISNULL(gmg.InvoiceDetailIdQX, 0) AND t.CodeGlosaId = gmg.CodeGlosaId
		WHERE t.StatusField = 0 AND t.Reiterated = 1 AND t.ValueGlosado > gmg.ValuePendingConciliation

		/*******************************************  DETERMINAR PRINCIPAL *******************************************/

		IF NOT EXISTS (SELECT 1 FROM @TableXmlObject WHERE StatusField <> 0) AND EXISTS (SELECT 1 FROM @TableXmlObject WHERE Reiterated = 0)
		BEGIN
			-- Se valida que no existe detalles con el mismo concepto de glosa ya agregado
			UPDATE t
				SET t.MainGlosa = 1,
					DeterminedMainGlosa = 1
			FROM @TableXmlObject t
			JOIN
			(
				SELECT gmg.InvoiceDetailId, gmg.InvoiceDetailIdQX
				FROM
				(
						SELECT t.InvoiceDetailId, IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) InvoiceDetailIdQX
						FROM @TableXmlObject t
						WHERE t.Reiterated = 0
					UNION ALL
						SELECT gmg.InvoiceDetailId, ISNULL(gmg.InvoiceDetailIdQX, 0) InvoiceDetailIdQX
						FROM 
						(
							SELECT InvoiceNumber
							FROM @TableXmlObject t
							WHERE t.Reiterated = 0
							GROUP BY InvoiceNumber
						) t
						JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON t.InvoiceNumber = gmg.InvoiceNumber
						WHERE gmg.MainGlosa = 1
				) gmg
				GROUP BY gmg.InvoiceDetailId, gmg.InvoiceDetailIdQX
				HAVING COUNT(1) = 1
			) gmg ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = gmg.InvoiceDetailIdQX
			WHERE t.Reiterated = 0

			--Determina las glosas principales
			IF EXISTS (SELECT 1 FROM @TableXmlObject WHERE Reiterated = 0 AND DeterminedMainGlosa = 0)
			BEGIN
				SET @InvoiceDetailIdRows = 1
				SET @InvoiceDetailId = 0

				-- Se recorre los detalles
				WHILE @InvoiceDetailIdRows > 0
				BEGIN
					SELECT TOP 1
						@InvoiceDetailId = InvoiceDetailId,
						-----------------------------------
						@InvoiceDetailIdQXRows = 1,
						@InvoiceDetailIdQX = -1
					FROM @TableXmlObject t
					WHERE Reiterated = 0
						AND DeterminedMainGlosa = 0
						AND InvoiceDetailId > @InvoiceDetailId
					ORDER BY InvoiceDetailId

					SET @InvoiceDetailIdRows = @@ROWCOUNT
					IF @InvoiceDetailIdRows = 0 
					BEGIN
						BREAK
					END

					---------------------------------------

					-- Se recorre los detalles QX
					WHILE @InvoiceDetailIdQXRows > 0
					BEGIN
						SELECT TOP 1
							@InvoiceDetailIdQX = IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0),
							@ValueGlosado = MAX(ValueGlosado) + 1,
							-----------------------------------
							@Rows = 1,
							@Id = 0
						FROM @TableXmlObject t
						WHERE DeterminedMainGlosa = 0
							AND InvoiceDetailId = @InvoiceDetailId
							AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) > @InvoiceDetailIdQX
						GROUP BY IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0)
						ORDER BY IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0)

						SET @InvoiceDetailIdQXRows = @@ROWCOUNT
						IF @InvoiceDetailIdQXRows = 0 
						BEGIN
							BREAK
						END

						---------------------------------------

						SELECT @PendingValueGlosado = t.InvoicedValue - ISNULL(gmg.ValueGlosado, 0)
						FROM @TableXmlObject t
						LEFT JOIN
						(
							SELECT gmg.InvoiceDetailId, ISNULL(gmg.InvoiceDetailIdQX, 0) InvoiceDetailIdQX, SUM(gmg.ValueGlosado) ValueGlosado
							FROM Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) 
							WHERE gmg.MainGlosa = 1
								AND gmg.InvoiceDetailId = @InvoiceDetailId AND ISNULL(gmg.InvoiceDetailIdQX, 0) = @InvoiceDetailId
							GROUP BY gmg.InvoiceDetailId, ISNULL(gmg.InvoiceDetailIdQX, 0)
						) gmg ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = gmg.InvoiceDetailIdQX
						WHERE t.InvoiceDetailId = @InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = @InvoiceDetailIdQX

						UPDATE t
							SET t.DeterminedMainGlosa = 1
						FROM @TableXmlObject t
						WHERE t.InvoiceDetailId = @InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = @InvoiceDetailIdQX
							AND t.ValueGlosado > @PendingValueGlosado

						---------------------------------------

						-- Se recorre los valores a glosar
						WHILE @Rows > 0
						BEGIN
							SELECT TOP 1
								@Id = Id,
								@ValueGlosado = ValueGlosado
							FROM @TableXmlObject t
							WHERE DeterminedMainGlosa = 0
								AND InvoiceDetailId = @InvoiceDetailId
								AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = @InvoiceDetailIdQX
							ORDER BY ValueGlosado DESC, Id

							SET @Rows = @@ROWCOUNT
							IF @Rows = 0 
							BEGIN
								BREAK
							END

							---------------------------------------

							UPDATE t
								SET t.MainGlosa = 1,
									t.DeterminedMainGlosa = 1
							FROM @TableXmlObject t
							WHERE t.Id = @Id

							SET @PendingValueGlosado = @PendingValueGlosado - @ValueGlosado

							---------------------------------------

							UPDATE t
								SET t.DeterminedMainGlosa = 1
							FROM @TableXmlObject t
							WHERE t.InvoiceDetailId = @InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = @InvoiceDetailIdQX
								AND t.ValueGlosado > @PendingValueGlosado
						END
					END
				END
			END
		END

		/******************************************* VALIDAR SALDO FACTURA *******************************************/

		-- Se Valida que el valor glosado no supere el valor facturado del detalle
		UPDATE t
			SET t.StatusField = 999,
				t.MessageField = CONCAT('El valor glosado (', FORMAT(gmg.ValueGlosado, 'C0', 'es-CO'), ') no puede ser mayor al saldo (', FORMAT(ISNULL(ar.Balance, 0), 'C0', 'es-CO'), ') de la factura ', t.InvoiceNumber)
		FROM @TableXmlObject t
		JOIN
		(
			SELECT gmg.InvoiceDetailId, gmg.InvoiceDetailIdQX, SUM(gmg.ValueGlosado) ValueGlosado
			FROM
			(
					SELECT t.InvoiceDetailId, IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) InvoiceDetailIdQX, t.ValueGlosado
					FROM @TableXmlObject t
					WHERE t.StatusField = 0 AND t.MainGlosa = 1
				UNION ALL
					SELECT gmg.InvoiceDetailId, ISNULL(gmg.InvoiceDetailIdQX, 0) InvoiceDetailIdQX, gmg.ValueGlosado
					FROM 
					(
						SELECT InvoiceNumber
						FROM @TableXmlObject t
						WHERE t.StatusField = 0
						GROUP BY InvoiceNumber
					) t
					JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON t.InvoiceNumber = gmg.InvoiceNumber
					WHERE gmg.MainGlosa = 1
			) gmg
			GROUP BY gmg.InvoiceDetailId, gmg.InvoiceDetailIdQX
		) gmg ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = gmg.InvoiceDetailIdQX
		LEFT JOIN Portfolio.AccountReceivable ar WITH (NOLOCK) ON t.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		WHERE t.StatusField = 0 AND gmg.ValueGlosado > ISNULL(ar.Balance, 0)

		/************************************************** PROCESO **************************************************/

		IF NOT EXISTS (SELECT 1 FROM @TableXmlObject WHERE StatusField <> 0)
		BEGIN
			INSERT INTO Glosas.GlosaMovementGlosa
			(
				InvoiceNumber, InvoiceDetailId, InvoiceDetailIdQX,
				MainGlosa, TypeConcept, CodeGlosa, CodeGlosaId, ResponsibleId, 
				ValueGlosado, ValuePendingConciliation,
				RationaleGlosa, RationaleDateGlosa, State, TempState
			)
			SELECT	InvoiceNumber, InvoiceDetailId, IIF(HandleQX = 1, InvoiceDetailIdQX, NULL),
					MainGlosa, 1, CodeGlosa, CodeGlosaId, ResponsibleId,
					ValueGlosado, ValueGlosado,
					ISNULL(RationaleGlosa, ''), GETDATE(), 1, 1
			FROM @TableXmlObject
			WHERE Reiterated = 0

			UPDATE gmg
				SET gmg.ResponsibleReiterationId = t.ResponsibleId,
					gmg.ValueReiterated = t.ValueGlosado,
					gmg.RationaleReiteration = ISNULL(gmg.RationaleGlosa, ''),
					gmg.RationaleDateReiteration = GETDATE(),
					gmg.TempState = gmg.State,
					gmg.State = 3
			FROM @TableXmlObject t
			JOIN Glosas.GlosaMovementGlosa gmg ON t.InvoiceDetailId = gmg.InvoiceDetailId AND IIF(t.HandleQX = 1, t.InvoiceDetailIdQX, 0) = ISNULL(gmg.InvoiceDetailIdQX, 0) AND t.CodeGlosaId = gmg.CodeGlosaId
			WHERE t.Reiterated = 1

			UPDATE gpg
				SET gpg.ValueGlosado = gmg.ValueGlosado,
					gpg.BalanceGlosa = gmg.ValueGlosado
			FROM @TableXmlObject t
			JOIN Glosas.GlosaPortfolioGlosada gpg ON t.InvoiceNumber = gpg.InvoiceNumber
			JOIN
			(
				SELECT t.InvoiceNumber, SUM(gmg.ValueGlosado) ValueGlosado
				FROM 
				(
					SELECT InvoiceNumber
					FROM @TableXmlObject t
					WHERE t.Reiterated = 0
					GROUP BY InvoiceNumber
				) t
				JOIN Glosas.GlosaMovementGlosa gmg ON t.InvoiceNumber = gmg.InvoiceNumber
				WHERE gmg.MainGlosa = 1
				GROUP BY t.InvoiceNumber
			) gmg ON t.InvoiceNumber = gmg.InvoiceNumber
			WHERE t.Reiterated = 0

			-- Actualizamos el valor glosado
			UPDATE t
				SET t.MessageField = 'Proceso Realizado Exitosamente'
			FROM @TableXmlObject t
		END
	END TRY
	BEGIN CATCH
		INSERT INTO @TableXmlObject(StatusField, MessageField)
			SELECT 999, CONCAT(ERROR_MESSAGE(), ' Linea: ', ERROR_LINE())
	END CATCH

	SELECT	StatusField, 
			MessageField
	FROM @TableXmlObject
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que importa y registra los movimientos de glosa sobre ítems de factura, recibiendo un archivo XML con los detalles a glosar (número de factura, código de detalle, servicio, concepto de glosa, responsable, valor y comentario). Valida que cada factura esté en un estado apto para recibir glosa o reiteración consultando la cartera glosada (GlosaPortfolioGlosada), verifica que el detalle del ítem exista en GlosaInvoiceDetail y, cuando aplica, que el detalle quirúrgico correspondiente exista en GlosaInvoiceDetailQX. Una vez superadas las validaciones, registra el movimiento de glosa con su valor, responsable y justificación, siendo el punto de entrada principal para el proceso de auditoría de cuentas médicas y gestión de glosas con aseguradoras o EPS.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_ImportGlosaMovementGlosas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Importa desde un XML los detalles y movimientos de glosas a registrar (radicación o reiteración), validándolos y persistiéndolos en los movimientos de glosa y en la cartera glosada.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe tener nodos /NewDataSet/Datos con al menos un registro a procesar.; La factura debe existir en la cartera glosada y estar en estado 1 (radicada/pendiente de confirmar recepción) o 4 (pendiente reiteración); cualquier otro estado bloquea el proceso.; El detalle de factura referenciado debe existir asociado a la factura.; Si el detalle maneja QX y se envía CodigoQX distinto de 0, debe existir el detalle quirúrgico relacionado.; El concepto de glosa debe existir en Common.ConceptGlosas y estar activo (State=1).; El responsable de glosa debe existir en Glosas.Responsible y estar activo (State=1).; El valor glosado debe ser numérico (tras quitar puntos) y mayor que cero.; En radicación, no debe existir previamente el mismo detalle+concepto en movimientos de glosa.; En reiteración, debe existir previamente el detalle+concepto en movimientos de glosa.; El valor glosado no debe superar el valor facturado del detalle (ThirdPartySalesPrice o InvoicedValue, o el de QX si aplica).; En reiteración, el valor reiterado no debe superar ValuePendingConciliation del movimiento existente.; La suma de valores glosados principales por factura no debe superar el saldo (Balance) de la cuenta por cobrar tipo 2.; No deben existir filas duplicadas con la misma combinación de detalle, detalle QX y concepto de glosa dentro del XML.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas en estado 1 (radicación) o 4 (reiteración) de la cartera glosada.; Todo o nada: si cualquier fila queda con StatusField<>0 no se ejecuta ningún INSERT/UPDATE de persistencia.; Los nuevos movimientos siempre se insertan con TypeConcept=1, State=1, TempState=1 y ValuePendingConciliation igual al ValueGlosado inicial.; Las reiteraciones siempre llevan State=3 y conservan el estado anterior en TempState.; InvoiceDetailIdQX se persiste como NULL cuando el detalle no maneja QX.; El concepto de glosa y el responsable utilizados deben estar activos (State=1).; El valor glosado por detalle nunca excede su valor facturado, y la suma por factura nunca excede el saldo de la cuenta por cobrar.; No se permite duplicar (detalle, detalleQX, concepto de glosa) en una misma radicación, ni en lo ya existente.; Una reiteración solo aplica sobre movimientos previamente existentes con el mismo concepto y detalle.; RationaleGlosa nulo se sustituye por cadena vacía al persistir.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Glosas.GlosaMovementGlosa: Cuando todas las filas tienen StatusField=0 y Reiterated=0, se insertan los movimientos de glosa con TypeConcept=1, State=1, TempState=1, ValuePendingConciliation=ValueGlosado, RationaleDateGlosa=GETDATE() e InvoiceDetailIdQX solo si HandleQX=1.; [UPDATE] Glosas.GlosaMovementGlosa: Cuando Reiterated=1 y todas las filas son válidas, se registra la reiteración: ResponsibleReiterationId, ValueReiterated, RationaleReiteration, RationaleDateReiteration=GETDATE(), TempState=State actual y State=3.; [UPDATE] Glosas.GlosaPortfolioGlosada: Cuando Reiterated=0 y todo es válido, ValueGlosado y BalanceGlosa de la cartera glosada se igualan a la suma de ValueGlosado de los movimientos con MainGlosa=1 de la factura.; [RETURN_RESULT] @TableXmlObject: Siempre retorna StatusField y MessageField por fila: 999 con mensaje de error si falla validación, o ''Proceso Realizado Exitosamente'' si se completó.; [RETURN_RESULT] @TableXmlObject: Si ocurre excepción en TRY, se inserta una fila con StatusField=999 y MessageField=ERROR_MESSAGE() + línea.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No hay filas en el XML procesado → Inserta fila con StatusField=999 y mensaje ''No se encontraron detalles para procesar''.; si La factura está en estado distinto a 1 o 4 en GlosaPortfolioGlosada → Marca error indicando el estado actual textual de la factura. else Si State=4, marca la fila como Reiterated=1 (es una reiteración).; si Existen filas con Reiterated=0 y todas las validaciones pasaron → Ejecuta el algoritmo de determinación de glosa principal (MainGlosa) por detalle/QX, eligiendo principales según valor pendiente por glosar y mayor ValueGlosado.; si La suma de ValueGlosado principales por factura supera el Balance de AccountReceivable tipo 2 → Marca error ''no puede ser mayor al saldo''.; si Todas las filas quedan con StatusField=0 → Persiste inserts/updates en GlosaMovementGlosa y GlosaPortfolioGlosada y marca mensaje exitoso. else No realiza ninguna escritura y devuelve los errores acumulados.; si HandleQX=1 para el detalle → Usa InvoiceDetailIdQX y el InvoicedValue del detalle quirúrgico (GlosaInvoiceDetailQX). else Usa InvoicedValue del detalle (ThirdPartySalesPrice de Billing.InvoiceDetail o InvoicedValue de GlosaInvoiceDetail).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_ImportGlosaMovementGlosas';
-- GO
