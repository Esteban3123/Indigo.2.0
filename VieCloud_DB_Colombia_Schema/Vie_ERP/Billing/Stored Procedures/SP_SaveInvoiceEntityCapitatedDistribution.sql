-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-03-16
-- Description:	Procedimiento que se encarga de guardar, actualizar la factura basica
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveInvoiceEntityCapitatedDistribution] 
    @InvoiceEntityCapitatedDistributionXml AS XML,
	@InvoiceEntityCapitatedDistributionDetailXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT, 
			@Code VARCHAR(20),
			@OperatingUnitId INT,
			@DocumentDate DATE,
			@InvoiceEntityCapitatedId INT,
			@Observation VARCHAR(MAX),
			@Status TINYINT,
			------------------------------
			@errors VARCHAR(MAX)

	--Tabla temporal de BasicBillingDetail
	DECLARE @Invoices TABLE
	(
		[InvoiceId] [int]
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),			
			@Code = t.x.value('Code[1]','varchar(20)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@InvoiceEntityCapitatedId = t.x.value('InvoiceEntityCapitatedId[1]','int'),
			@Observation = t.x.value('Observation[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint')
		FROM @InvoiceEntityCapitatedDistributionXml.nodes('/InvoiceEntityCapitatedDistribution') t(x)

		IF EXISTS (SELECT 1 FROM Billing.InvoiceEntityCapitatedDistribution iec WHERE iec.Id = @Id AND iec.Status <> 1)
		BEGIN
			SELECT 999 as CodeMessage, 'El registro se encuentra en estado: ' + IIF(iec.Status = 2, 'Confirmado', 'Anulado') as Message, '' as Code, 0 as Id
			FROM Billing.InvoiceEntityCapitatedDistribution iec
			WHERE iec.Id = @Id
			RETURN
		END

		--Si no se esta anulando
		IF @Status <> 3
		BEGIN
			--Se obtiene BasicBillingDetail
			INSERT INTO @Invoices
				SELECT DISTINCT
					t.x.value('Id[1]','int') as Id
				FROM @InvoiceEntityCapitatedDistributionDetailXml.nodes('/Invoice') t(x)

			/*************************************VALIDACIONES************************************/

			--- Valido que la factura monto fijo se encuentre confirmada
			IF NOT EXISTS ( SELECT 1 FROM [Billing].[InvoiceEntityCapitated] iec WHERE iec.Id = @InvoiceEntityCapitatedId AND iec.Status = 2 )
			BEGIN
				SELECT @errors = STUFF((
						SELECT iec.Code
						FROM [Billing].[InvoiceEntityCapitated] iec 
						WHERE iec.Id = @InvoiceEntityCapitatedId AND iec.Status <> 2
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los Factura Monto Fijo ' + ISNULL(@errors, '') + ' no existe o no se encuentra confirmada' AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que la factura monto fijo no haya sido distribuida
			IF EXISTS ( SELECT 1 FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd WHERE iecd.Id <> @Id AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId AND iecd.Status NOT IN (3, 4) )
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + iecd.Code
						FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd 
						WHERE iecd.Id <> @Id 
							AND iecd.InvoiceEntityCapitatedId = @InvoiceEntityCapitatedId 
							AND iecd.Status NOT IN (3, 4)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'La factura Monto Fijo ya se encuentra en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que todas las facturas sean un control de servicio
			IF EXISTS (SELECT 11 FROM @Invoices t LEFT JOIN [Billing].[Invoice] i ON t.InvoiceId = i.Id WHERE ISNULL(i.DocumentType, 0) <> 5)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ISNULL(i.InvoiceNumber, 'N/A')
						FROM @Invoices t 
						LEFT JOIN [Billing].[Invoice] i ON t.InvoiceId = i.Id 
						WHERE ISNULL(i.DocumentType, 0) <> 5
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes documentos no son controles de servicios: ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que los controles de servicios no se encuentren anulados
			IF EXISTS 
			(
				SELECT 1 
				FROM @Invoices t 
				JOIN Billing.Invoice i ON t.InvoiceId = i.Id
				WHERE i.Status <> 1
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Estado: ' + IIF(i.Status = 2, 'ANULADO', 'N/A') + ')'
						FROM @Invoices t 
						JOIN Billing.Invoice i ON t.InvoiceId = i.Id
						WHERE i.Status <> 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios no se encuentran en estado facturado ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			--- Valido que los controles de servicios no se encuentren ya distribuidas
			IF EXISTS 
			(
				SELECT 1 
				FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd
				JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
				JOIN @Invoices i ON iecdd.InvoiceId = i.InvoiceId
				WHERE iecd.Id <> @Id AND iecd.Status NOT IN (3, 4)
			)
			BEGIN
				SELECT @errors = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + i.InvoiceNumber + ' (Distribución: ' + iecd.Code + ')'
						FROM [Billing].[InvoiceEntityCapitatedDistribution] iecd
						JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
						JOIN @Invoices t ON iecdd.InvoiceId = t.InvoiceId
						JOIN Billing.Invoice i ON t.InvoiceId = i.Id
						WHERE iecd.Id <> @Id AND iecd.Status NOT IN (3, 4)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT 999 AS CodeMessage, 'Los siguientes controles de servicios ya se encuentran en otra distribución ' + CHAR(13) + CHAR(10) + @errors AS Message, '' AS Code, 0 AS Id
				RETURN
			END

			/*************************************************************************************/

			--Si se esta insertando por primera vez se consulta la secuencia numerica
			IF @Id = 0
			BEGIN
				IF @Code = '' 
				BEGIN
					--Consultamos si la secuencia es con O o OU
					DECLARE @IdForm VARCHAR(5) = '2057',							
							@pattern VARCHAR(300),
							@NextS INT,
							@idSequenceDetail INT
				
					-- Consultamos la secuencia numerica del formulario
					SELECT @pattern = cs.Pattern, 
						@NextS = bsd.[Next], 
						@idSequenceDetail = bsd.Id  
					FROM Billing.BillingSequenceDetail bsd 
					JOIN Billing.BillingSequence bs ON bs.Id = bsd.IdSequenseBillingC
					JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
					WHERE bs.IdForm = @IdForm 
						AND 
						(
							(bs.Scope = 'O')
							OR
							(bs.Scope <> 'O' AND bsd.IdOperatingUnit = @OperatingUnitId)
						)

					IF (@idSequenceDetail IS NULL)
					BEGIN
						SELECT 999 as CodeMessage, 'Secuencia de Facturación Básica no encontrada' as Message, '' as Code, 0 as Id
						RETURN
					END

					SELECT @Code = dbo.GetSequence('', @pattern, @NextS)
					UPDATE Billing.BillingSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail

					--Se inserta la cabecera
					INSERT INTO [Billing].[InvoiceEntityCapitatedDistribution]
					(
						[Code],[OperatingUnitId],[DocumentDate],[InvoiceEntityCapitatedId],[InvoiceValue],[TotalControlValue],[Observation],[Status],[CreationUser],[CreationDate]
					)
					SELECT @Code,@OperatingUnitId,@DocumentDate,@InvoiceEntityCapitatedId,0 AS InvoiceValue,0 AS TotalControlValue,@Observation,@Status,@CodeUser,Common.GETDATE()

					--Obtengo el id de la cabcera
					SET @Id = SCOPE_IDENTITY()
				END
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Billing].[InvoiceEntityCapitatedDistribution]
					SET [Code] = @Code,
						[OperatingUnitId] = @OperatingUnitId,
						[DocumentDate] = @DocumentDate,
						[InvoiceEntityCapitatedId] = @InvoiceEntityCapitatedId,
						[Observation] = @Observation,
						[Status] = @Status,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = Common.GETDATE()
				WHERE Id = @Id
			END

			-- Elimino los detalles que no se encuentren en las facturas enviadas
			DELETE iecdd
			FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
			LEFT JOIN @Invoices i ON iecdd.InvoiceId = i.InvoiceId
			WHERE iecdd.InvoiceEntityCapitatedDistributionId = @Id
				AND i.InvoiceId IS NULL

			-- Inserto los nuevo detalles
			INSERT INTO [Billing].[InvoiceEntityCapitatedDistributionDetail]
            (
				[InvoiceEntityCapitatedDistributionId]
			   ,[InvoiceId]
			   ,[HealthAdministratorId]
			   ,[InvoiceCategoryId]
			   ,[InvoiceNumber]
			   ,[InvoiceDate]
			   ,[InvoiceValue]
			)
			SELECT
				@Id,
				i.Id,
				i.HealthAdministratorId,
				i.InvoiceCategoryId,
				i.InvoiceNumber,
				i.InvoiceDate,
				i.ThirdPartySalesValue
			FROM @Invoices t
			JOIN Billing.Invoice i ON t.InvoiceId = i.Id
			JOIN Billing.RevenueControlDetail rcd ON i.RevenueControlDetailId = rcd.Id
			LEFT JOIN [Billing].[InvoiceEntityCapitatedDistributionDetail] iecdd ON i.Id = iecdd.InvoiceId AND iecdd.InvoiceEntityCapitatedDistributionId = @Id
			WHERE iecdd.Id IS NULL

			-- Actualizo de acuerdo al valor de la factura monto fijo
			UPDATE iecd
				SET iecd.InvoiceValue = iec.TotalValue
			FROM Billing.InvoiceEntityCapitatedDistribution iecd
			JOIN Billing.InvoiceEntityCapitated iec ON iecd.InvoiceEntityCapitatedId = iec.Id
			WHERE iecd.Id = @Id

			-- Actualizo de acuerdo al valor de los detalles de la distribucion
			UPDATE iecd
				SET iecd.TotalControlValue = iecdd.TotalValue
			FROM Billing.InvoiceEntityCapitatedDistribution iecd
			JOIN 
			(
				SELECT iecdd.InvoiceEntityCapitatedDistributionId, SUM(iecdd.InvoiceValue) TotalValue
				FROM Billing.InvoiceEntityCapitatedDistributionDetail iecdd
				WHERE iecdd.InvoiceEntityCapitatedDistributionId = @Id
				GROUP BY iecdd.InvoiceEntityCapitatedDistributionId
			) iecdd ON iecd.Id = iecdd.InvoiceEntityCapitatedDistributionId
			WHERE iecd.Id = @Id

		END
		ELSE
		BEGIN
			DECLARE @AnnulmentUser VARCHAR(20) = CASE WHEN @Status <> 3 THEN null ELSE @CodeUser END
			DECLARE @AnnulmentDate DATETIME = CASE WHEN @Status <> 3 THEN null ELSE Common.GETDATE() END

			UPDATE [Billing].[InvoiceEntityCapitatedDistribution]
				SET [Status] = @Status,
					[AnnulmentUser] = @AnnulmentUser,
					[AnnulmentDate] = @AnnulmentDate
			WHERE Id = @Id
		END

		SELECT 0 AS CodeMessage, 'Se guardó correctamente' AS Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS Message, '' AS Code, 0 AS Id
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una distribución de factura de capitación entre unidades operativas, procesando tanto el encabezado como el detalle de las facturas (controles de servicio) asociadas. Antes de persistir, ejecuta múltiples validaciones de negocio: que la factura de monto fijo exista y esté confirmada, que no haya sido distribuida previamente en otro registro activo, que los documentos del detalle sean efectivamente controles de servicio (tipo 5) y que estén en estado facturado (no anulados). También gestiona la anulación de una distribución ya existente, impidiendo modificar registros confirmados o anulados. Opera sobre las tablas de distribución de capitación, detalle de distribución y facturas de cobro del módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Guarda, actualiza o anula una distribución de factura de capitación, validando estado de la factura monto fijo y de los controles de servicio asociados, y recalculando los valores totales de la distribución.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de cabecera debe contener un nodo /InvoiceEntityCapitatedDistribution con los campos Id, Code, OperatingUnitId, DocumentDate, InvoiceEntityCapitatedId, Observation y Status; El XML de detalle debe contener nodos /Invoice con Id de facturas a distribuir; El registro identificado por @Id debe estar en Status=1 (Pendiente) para poder modificarse; La factura de capitación referenciada (@InvoiceEntityCapitatedId) debe existir y estar Confirmada (Status=2); Debe existir configuración de secuencia para el formulario IdForm=''2057'' acorde al Scope/OperatingUnit cuando se inserta nuevo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una factura de capitación (InvoiceEntityCapitated) solo puede distribuirse si está Confirmada (Status=2); Una misma factura de capitación no puede estar en más de una distribución activa (Status NOT IN (3,4)); Solo se aceptan como detalles facturas con DocumentType=5 (controles de servicio); Los controles de servicio incluidos deben estar en Status=1 (no anulados/facturado); Un mismo control de servicio no puede pertenecer a otra distribución activa (Status NOT IN (3,4)); InvoiceValue de la cabecera siempre refleja el TotalValue de la factura de capitación asociada; TotalControlValue de la cabecera siempre equivale a la suma de InvoiceValue de sus detalles; Si el registro ya no está en Status=1 (Pendiente), no se permite ninguna modificación; La numeración (Code) solo se asigna en alta nueva cuando @Code viene vacío, usando el formulario 2057 e incrementando Next en 1; AnnulmentUser y AnnulmentDate solo se setean cuando Status=3 (anulación)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de capitación (Monto Fijo); Distribución de factura capitada; Control de servicio (DocumentType=5); Secuencia de facturación; Unidad operativa; Anulación de documentos; Entidad administradora de salud (EPS/ARS)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe el registro y su Status <> 1 (no Pendiente) → Retorna CodeMessage=999 indicando que está Confirmado (Status=2) o Anulado, y termina sin cambios else Continúa el flujo de guardado/anulación; si @Status <> 3 (no se está anulando) → Ejecuta validaciones, inserta/actualiza cabecera y reconstruye los detalles de distribución else Solo actualiza Status, AnnulmentUser y AnnulmentDate de la cabecera; si @Id = 0 AND @Code = '''' → Consulta y consume la secuencia numérica del formulario 2057 (BillingSequence/BillingSequenceDetail/Sequense), incrementa Next e inserta nueva cabecera else Si @Id <> 0, hace UPDATE de la cabecera existente; si BillingSequence.Scope = ''O'' (organización) o Scope <> ''O'' AND BillingSequenceDetail.IdOperatingUnit = @OperatingUnitId → Selecciona la secuencia correspondiente al alcance organizacional o de unidad operativa else Si no hay secuencia, retorna ''Secuencia de Facturación Básica no encontrada''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceEntityCapitatedDistribution; Billing.InvoiceEntityCapitated; Billing.Invoice; Billing.InvoiceEntityCapitatedDistributionDetail; Billing.BillingSequenceDetail; Billing.BillingSequence; Common.Sequense; Billing.RevenueControlDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitatedDistribution';
-- GO
