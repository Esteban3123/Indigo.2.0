-- ===============================================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-02-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un compromiso
-- ===============================================================================================================================
CREATE PROCEDURE [Budget].[SP_SaveCommitment_Output]
	@CommitmentXml xml,
	@CommitmentDetailForDeleteXml AS XML,
	@UserCode varchar(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@Code VARCHAR(20) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @OperatingUnitId INT,
			@BudgetaryValidityId INT,
			@DocumentDate DATETIME,
			@ThirdPartyId INT,
			@DocumentSource TINYINT,
			@Document VARCHAR(100),			
			@CommitmentType TINYINT,
			@Observations VARCHAR(MAX),
			@Status TINYINT,
			@EntityId INT,
			@EntityCode VARCHAR(20),
			@EntityName VARCHAR(250),
			@AnnulmentConceptId INT,
			@AnnulmentDescription VARCHAR(MAX),
			@AutomaticObligation BIT, 
			@AutomaticPaymentOrder BIT, 
			------------------------------
			@IdForm INT = 231,
			@DocumentType INT = 11,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @Details TABLE
	(
		Id INT,
		CommitmentId INT,
		AvailabilityDetailId INT,
		CategoryId INT,
		RevenueTypeId INT,
		ExpiredDate DATETIME,
		InitialValue DECIMAL(18,2)
	)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),			
			@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
			@ThirdPartyId = t.x.value('ThirdPartyId[1]','int'),
			@DocumentSource = t.x.value('DocumentSource[1]','tinyint'),
			@Document = t.x.value('Document[1]','varchar(100)'),
			@CommitmentType = t.x.value('CommitmentType[1]','tinyint'),
			@Observations = t.x.value('Observations[1]','varchar(max)'),
			@Status = t.x.value('Status[1]','tinyint'),
			@EntityId = t.x.value('EntityId[1]','int'),
			@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
			@EntityName = t.x.value('EntityName[1]','varchar(250)'),
			@AnnulmentConceptId = t.x.value('AnnulmentConceptId[1]','int'),
			@AnnulmentDescription = t.x.value('AnnulmentDescription[1]','varchar(max)'),
			@AutomaticObligation = ISNULL(t.x.value('AutomaticObligation[1]','BIT'), 0),
			@AutomaticPaymentOrder = ISNULL(t.x.value('AutomaticPaymentOrder[1]','BIT'), 0)
		FROM @CommitmentXml.nodes('/Commitment') t(x)

		IF EXISTS (SELECT 1 FROM Budget.Commitment c WHERE c.Id = @Id AND c.Status <> 1)
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = 'El Compromiso se encuentra en estado: ' + IIF(om.Status = 2, 'Confirmado', 'Anulado')
			FROM Budget.Obligation om 
			WHERE om.Id = @Id
			RETURN
		END
		
		IF @Status = 3
		BEGIN
			UPDATE [Budget].[Commitment]
				SET [Status] = @Status,
					[ModificationUser] = @UserCode,
					[ModificationDate] = [Common].[GETDATE](),
					[AnnulmentUser] = @UserCode,
					[AnnulmentDate] = [Common].[GETDATE](),
					[AnnulmentConceptId] = @AnnulmentConceptId,
					[AnnulmentDescription] = @AnnulmentDescription
			WHERE Id = @Id

			UPDATE [Budget].[CommitmentDetail]
				SET [DebitModificationValue] = [InitialValue],
					[TotalCommitment] = 0,
					[Balance] = 0
			WHERE CommitmentId = @Id
		END
		ELSE
		BEGIN
			--Eliminamos los detalles indicados
			DELETE cd
			FROM @CommitmentDetailForDeleteXml.nodes('/CommitmentDetail') t(x)
			JOIN Budget.CommitmentDetail cd ON t.x.value('Id[1]','int') = cd.Id
			WHERE cd.CommitmentId = @Id

			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Details
				SELECT
					t.x.value('Id[1]','int'),
					t.x.value('CommitmentId[1]','int'),
					t.x.value('AvailabilityDetailId[1]','int'),
					t.x.value('CategoryId[1]','int'),
					t.x.value('RevenueTypeId[1]','int'),
					t.x.value('ExpiredDate[1]','datetime'),
					t.x.value('InitialValue[1]','decimal(18,2)')
				FROM @CommitmentXml.nodes('/Commitment/CommitmentDetail') t(x)

			--Se obtiene los detalles previamente insertados que no han sido modificados
			INSERT INTO @Details
				SELECT 
					cd.Id, 
					cd.CommitmentId, 
					cd.AvailabilityDetailId,
					cd.CategoryId,
					cd.RevenueTypeId,
					cd.ExpiredDate,
					cd.InitialValue
				FROM Budget.CommitmentDetail cd
				LEFT JOIN @Details d ON cd.Id = d.Id
				WHERE cd.CommitmentId = @Id AND ISNULL(d.Id, 0) = 0

			/*************************************VALIDACIONES************************************/

			--- Valido el Periodo de la Vigencia
			IF NOT EXISTS 
			(
				SELECT 1 
				FROM Budget.BudgetaryValidity bv 
				WHERE bv.Id = @BudgetaryValidityId
					AND bv.Year = YEAR(@DocumentDate)
					AND bv.ExpenseMonth <= MONTH(@DocumentDate)
			)
			BEGIN
				SELECT @Message = 'La Fecha del Compromiso (' + CONVERT(VARCHAR, @DocumentDate, 23) + ') no coincide con la vigencia de presupuesto (' + CONCAT(bv.Year, '-', RIGHT('00' + CAST(bv.ExpenseMonth AS VARCHAR), 2)) + ').'
				FROM Budget.BudgetaryValidity bv
				WHERE bv.Id = @BudgetaryValidityId

				SELECT @CodeResult = 999, 
					   @MessageResult = ISNULL(@Message, 'Periodo Presupuestal no encontrado.')
				RETURN
			END

			--- Valido que existan detalles
			IF NOT EXISTS (SELECT 1 FROM @Details)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El Compromiso no tiene detalles.'
				RETURN
			END

			-- Valido que los registros editados no hayan cambiado sus valores base
			IF EXISTS (SELECT 1 FROM Budget.CommitmentDetail od JOIN @Details d ON od.Id = d.Id WHERE od.CommitmentId <> @Id OR ISNULL(od.AvailabilityDetailId, 0) <> ISNULL(d.AvailabilityDetailId, 0) OR od.CategoryId <> d.CategoryId OR od.RevenueTypeId <> d.RevenueTypeId) 
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'Los detalles del Compromiso han sido alterados.'
				RETURN
			END

			--- Valido que no existan detalles duplicados
			IF EXISTS (SELECT 1 FROM @Details d GROUP BY d.AvailabilityDetailId HAVING COUNT(*) > 1)
			BEGIN
				SELECT @CodeResult = 999, 
					   @MessageResult = 'El Compromiso tiene detalles duplicados.'
				RETURN
			END

			-- Valido que la fecha de vencimiento del detalle no sea menor a la fecha del compromiso
			IF EXISTS 
			(
				SELECT 1 
				FROM @Details d
				JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON d.AvailabilityDetailId = ad.Id
				JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id
				WHERE CAST(a.ExpirationDate AS DATE) < CAST(@DocumentDate AS DATE)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
						FROM @Details d
						JOIN Budget.Category c WITH (NOLOCK) ON d.CategoryId = c.Id
						JOIN Budget.RevenueType rt WITH (NOLOCK) ON d.RevenueTypeId = rt.Id
						WHERE CAST(d.ExpiredDate AS DATE) < CAST(@DocumentDate AS DATE)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT @CodeResult = 999, 
					   @MessageResult = 'La fecha de vencimiento de las siguientes detalles es inferior a la fecha del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
				RETURN
			END

			--- Valido el tipo de Compromiso
			IF @CommitmentType = 1
			BEGIN
				--- Si los detalles afectan una disponibilidad
				IF EXISTS (SELECT 1 FROM @Details d WHERE d.AvailabilityDetailId IS NULL)
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = 'El Compromiso tiene detalles sin disponibilidad.'
					RETURN
				END

				-- Valido que la fecha de la disponibilidad no sea mayor a la del compromiso
				IF EXISTS 
				(
					SELECT 1 
					FROM @Details d
					JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON d.AvailabilityDetailId = ad.Id
					JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id
					WHERE CAST(a.DocumentDate AS DATE) > CAST(@DocumentDate AS DATE)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Disponibilidad ' + a.Code + ', Fecha: ' + CONVERT(VARCHAR, a.DocumentDate, 23)
							FROM @Details d
							JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON d.AvailabilityDetailId = ad.Id
							JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id
							WHERE CAST(a.DocumentDate AS DATE) > CAST(@DocumentDate AS DATE)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999, 
							@MessageResult = 'La fecha de las siguientes disponibilidades es superior a la fecha del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END

				-- Valido que la fecha de la disponibilidad no sea mayor a la del compromiso
				IF EXISTS 
				(
					SELECT 1 
					FROM @Details d
					JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON d.AvailabilityDetailId = ad.Id
					JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id
					WHERE CAST(a.ExpirationDate AS DATE) < CAST(@DocumentDate AS DATE)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Disponibilidad ' + a.Code + ', Fecha: ' + CONVERT(VARCHAR, a.ExpirationDate, 23)
							FROM @Details d
							JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON d.AvailabilityDetailId = ad.Id
							JOIN Budget.Availability a WITH (NOLOCK) ON ad.AvailabilityId = a.Id
							WHERE CAST(a.ExpirationDate AS DATE) < CAST(@DocumentDate AS DATE)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999, 
							@MessageResult = 'La fecha de vencimiento de las siguientes disponibilidades es inferior a la fecha del compromiso: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT 
							d.AvailabilityDetailId,
							SUM(d.InitialValue) Value
						FROM @Details d 
						WHERE d.AvailabilityDetailId IS NOT NULL
						GROUP BY d.AvailabilityDetailId
					) od 
					JOIN Budget.AvailabilityDetail cd WITH (NOLOCK) ON od.AvailabilityDetailId = cd.Id
					WHERE od.Value > cd.Balance
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
							FROM 
							(
								SELECT 
									d.AvailabilityDetailId,
									SUM(d.InitialValue) Value
								FROM @Details d 
								WHERE d.AvailabilityDetailId IS NOT NULL
								GROUP BY d.AvailabilityDetailId
							) od 
							JOIN Budget.AvailabilityDetail cd WITH (NOLOCK) ON od.AvailabilityDetailId = cd.Id
							JOIN Budget.Budget b ON cd.BudgetId = b.Id
							JOIN Budget.Category c WITH (NOLOCK) ON b.CategoryId = c.Id
							JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id					
							WHERE od.Value > cd.Balance
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999, 
							@MessageResult = 'El valor del detalle de los siguientes rubros del Compromiso no pueden ser mayor que el saldo de la disponibilidad: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END
			ELSE
			BEGIN
				--- Si los detalles afectan directamente el presupuesto
				IF EXISTS (SELECT 1 FROM @Details d WHERE d.AvailabilityDetailId IS NOT NULL)
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = 'El Compromiso tiene detalles asociadas a una disponibilidad.'
					RETURN
				END

				IF EXISTS 
				(
					SELECT 1 
					FROM 
					(
						SELECT 
							d.CategoryId,
							d.RevenueTypeId,
							SUM(d.InitialValue) Value
						FROM @Details d
						WHERE d.AvailabilityDetailId IS NULL
						GROUP BY d.CategoryId, d.RevenueTypeId
					) od
					LEFT JOIN Budget.Budget b WITH (NOLOCK) ON od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
					WHERE od.Value > ISNULL(b.Balance, 0)
				)
				BEGIN
					SELECT @Message = STUFF((
							SELECT DISTINCT CHAR(13) + CHAR(10) + ' - Rubro ' + c.Code + ' - ' + c.Name + ' de tipo ' + rt.Code + ' - ' + rt.Name
							FROM 
							(
								SELECT 
									d.CategoryId,
									d.RevenueTypeId,
									SUM(d.InitialValue) Value
								FROM @Details d
								WHERE d.AvailabilityDetailId IS NULL
								GROUP BY d.CategoryId, d.RevenueTypeId
							) od
							JOIN Budget.Category c WITH (NOLOCK) ON od.CategoryId = c.Id
							JOIN Budget.RevenueType rt WITH (NOLOCK) ON od.RevenueTypeId = rt.Id
							LEFT JOIN Budget.Budget b WITH (NOLOCK) ON od.CategoryId = b.CategoryId AND od.RevenueTypeId = b.RevenueTypeId
							WHERE od.Value > ISNULL(b.Balance, 0)
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SELECT	@CodeResult = 999, 
							@MessageResult = 'El valor del detalle de los siguientes rubros del Compromiso no pueden ser mayor que el saldo del presupuesto: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
					RETURN
				END
			END

			/*************************************************************************************/

			DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @UserCode ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @IsManual BIT
				
				EXEC Common.SP_GetSequence 200, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999, 
							@MessageResult = REPLACE(@Message_Output, '{0}', 'Compromiso')
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Budget].[Commitment]
				(
					[Code],[BudgetaryValidityId],[DocumentDate],[ThirdPartyId],[DocumentSource],[Document],[CommitmentType],[Observations],
					[Status],[CreationUser],[CreationDate],[ModificationUser],[ModificationDate],[ConfirmationUser],[ConfirmationDate],
					[EntityId],[EntityCode],[EntityName],[AutomaticObligation],[AutomaticPaymentOrder]
				)
				SELECT @Code,@BudgetaryValidityId,@DocumentDate,@ThirdPartyId,@DocumentSource,@Document,@CommitmentType,@Observations,
					@Status,@UserCode,[Common].[GETDATE](),@ConfirmationUser,@ConfirmationDate,@ConfirmationUser,@ConfirmationDate,
					@EntityId,@EntityCode,@EntityName,@AutomaticObligation,@AutomaticPaymentOrder

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Budget].[Commitment]
					SET [Code] = @Code,
						[BudgetaryValidityId] = @BudgetaryValidityId,
						[DocumentDate] = @DocumentDate,
						[ThirdPartyId] = @ThirdPartyId,
						[DocumentSource] = @DocumentSource,
						[Document] = @Document,
						[CommitmentType] = @CommitmentType,
						[Observations] = @Observations,
						[Status] = @Status,
						[ModificationUser] = @UserCode,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate,
						[AutomaticObligation] = @AutomaticObligation,
						[AutomaticPaymentOrder] = @AutomaticPaymentOrder
				WHERE Id = @Id
			END

			/*************************************************************************************/

			INSERT INTO Budget.CommitmentDetail 
				(
					CommitmentId, AvailabilityDetailId, CategoryId, RevenueTypeId, ExpiredDate, InitialValue, DebitModificationValue, CreditModificationValue, TotalCommitment, ExecutedValue, Balance
				)
				SELECT
					@Id CommitmentId,
					d.AvailabilityDetailId,
					d.CategoryId,
					d.RevenueTypeId,
					d.ExpiredDate,
					d.InitialValue, 0, 0,
					d.InitialValue, 0, d.InitialValue
				FROM @Details d
				WHERE ISNULL(d.Id, 0) = 0

			UPDATE cd
				SET cd.ExpiredDate = d.ExpiredDate,
					cd.InitialValue = d.InitialValue, 
					cd.TotalCommitment = d.InitialValue,
					cd.Balance = d.InitialValue
			FROM Budget.CommitmentDetail cd
			JOIN @Details d ON cd.Id = d.Id

			/*************************************************************************************/

			IF @Status = 2
			BEGIN
				IF @CommitmentType = 1
				BEGIN
					UPDATE ad
						SET
							ad.ExecutedValue = ad.ExecutedValue + cd.InitialValue,
							ad.Balance = ad.Balance - cd.InitialValue
					FROM Budget.CommitmentDetail  cd
					JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
					WHERE cd.CommitmentId = @Id
				END
				ELSE
				BEGIN
					UPDATE b
						SET
							b.ExecutedValue = b.ExecutedValue + cd.InitialValue,
							b.Balance = b.Balance - cd.InitialValue,
							b.ModificationUser = @UserCode,
							b.ModificationDate = [Common].[GETDATE]()
					FROM Budget.CommitmentDetail  cd
					JOIN Budget.Budget b ON cd.CategoryId = b.CategoryId AND cd.RevenueTypeId = b.RevenueTypeId
					WHERE cd.CommitmentId = @Id
				END

				IF @AutomaticObligation = 1
				BEGIN
					SELECT @SubXml = CONVERT
					(
						XML, 
						(
							SELECT 
								Obligation.*,
								ObligationDetail.*
							FROM
							(
								SELECT 
									0 Id,
									@OperatingUnitId OperatingUnitId,
									'' Code,
									@BudgetaryValidityId BudgetaryValidityId,
									c.DocumentDate,
									c.ThirdPartyId,
									c.Document,
									1 ObligationType,
									CONCAT('Obligación automática: ', c.Observations) Observations,
									2 Status,
									@AutomaticPaymentOrder AutomaticPaymentOrder
								FROM Budget.Commitment c
								WHERE c.Id = @Id
							) Obligation
							JOIN
							( 
								SELECT
									0 ObligationId,
									cd.Id CommitmentDetailId,
									cd.CategoryId,
									cd.RevenueTypeId,
									cd.ExpiredDate,
									cd.InitialValue,
									@Id EntityId,
									@Code EntityCode,
									'Commitment' EntityName
								FROM Budget.CommitmentDetail cd
								WHERE CommitmentId = @Id
							) ObligationDetail ON Obligation.Id = ObligationDetail.ObligationId
							For xml AUTO,TYPE, ELEMENTS
						)
					)

					EXEC [Budget].[SP_SaveObligation_Output] @SubXml, '', @UserCode, @Code_Output OUT, @Message_Output OUT, NULL, NULL

					IF @Code_Output <> 0
					BEGIN
						SELECT	@CodeResult = 999,
								@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la obligación presupuestal')
						RETURN
					END

					SET @Message = ISNULL(@Message, '') + CHAR(13) + CHAR(10) + ISNULL(@Message_Output, 'Obligacion generada')
				END
			END
		END

		IF @Status = 1
		BEGIN
			IF NOT EXISTS (SELECT 1 FROM Budget.BudgetControl WHERE DocumentType = @DocumentType AND DocumentNumber = @Code)
			BEGIN
				INSERT INTO Budget.BudgetControl (DocumentNumber, DocumentType, DocumentUser, DocumentDate)
				SELECT @Code, @DocumentType, @UserCode, @DocumentDate
			END
		END
		ELSE
		BEGIN
			DELETE FROM Budget.BudgetControl WHERE DocumentType = @DocumentType AND DocumentNumber = @Code
		END

		SELECT @CodeResult = 0, 
			   @MessageResult = CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó el compromiso con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló el compromiso con código ', @Code)
				   ELSE CONCAT('Se guardó el compromiso con código ', @Code)
			   END + ISNULL(@Message, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999, 
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar, confirmar o anular un compromiso presupuestal a partir de un XML con la cabecera y el detalle del compromiso. Gestiona el ciclo de vida completo del documento: valida la vigencia presupuestal y la fecha, procesa los detalles de desglose por categoría e ingreso, y actualiza los saldos y valores en las tablas de compromisos (Budget.Commitment) y sus líneas de detalle (Budget.CommitmentDetail). En caso de anulación (estado 3), cierra el saldo disponible y registra el usuario, fecha y concepto de anulación; también puede generar automáticamente obligaciones y órdenes de pago asociadas al compromiso confirmado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitment_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCommitment_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de cabecera debe contener un nodo /Commitment con los datos básicos (Id, Code, BudgetaryValidityId, DocumentDate, CommitmentType, Status, etc.).; Si Id>0, el compromiso debe existir en Budget.Commitment y estar en Status=1 (en proceso) para poder modificarse.; La BudgetaryValidity referenciada debe existir y cubrir el año y mes de DocumentDate (Year=YEAR(@DocumentDate) y ExpenseMonth<=MONTH(@DocumentDate)).; Cuando Status<>3 debe haber al menos un detalle (en el XML o ya persistido).; Los detalles editados no pueden haber alterado CommitmentId, AvailabilityDetailId, CategoryId ni RevenueTypeId respecto a los valores almacenados.; No pueden existir detalles duplicados por AvailabilityDetailId.; La fecha de vencimiento (ExpiredDate) de cada detalle no puede ser inferior a DocumentDate del compromiso.; Si CommitmentType=1: todos los detalles deben referenciar una disponibilidad, la fecha de la disponibilidad debe ser <= DocumentDate, su ExpirationDate >= DocumentDate y la suma de valores por AvailabilityDetailId no puede superar el Balance de AvailabilityDetail.; Si CommitmentType<>1: ningún detalle puede tener AvailabilityDetailId y la suma por (CategoryId, RevenueTypeId) no puede superar el Balance del Budget correspondiente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Un compromiso ya confirmado (Status=2) o anulado (Status=3) no puede modificarse: solo se permiten cambios si Status=1.; Al anular (Status=3), todos los detalles quedan con TotalCommitment=0 y Balance=0, devolviendo el valor inicial como DebitModificationValue.; La fecha del compromiso debe estar dentro de la vigencia presupuestal (Year y ExpenseMonth de BudgetaryValidity).; Un compromiso no puede tener detalles duplicados sobre el mismo AvailabilityDetailId.; Las claves base de un detalle existente (CommitmentId, AvailabilityDetailId, CategoryId, RevenueTypeId) no pueden alterarse al editar.; La fecha de vencimiento de cada detalle nunca puede ser anterior a la fecha del compromiso.; Si CommitmentType=1, todos los detalles deben tener AvailabilityDetailId; en caso contrario, ninguno puede tenerlo.; El valor comprometido por rubro nunca puede exceder el saldo de la disponibilidad (CommitmentType=1) o del presupuesto (otros tipos).; Al confirmar (Status=2) se descuenta el InitialValue del Balance de AvailabilityDetail o de Budget según el tipo, y se acumula en ExecutedValue.; ConfirmationUser y ConfirmationDate solo se asignan cuando el compromiso entra en Status=2.; Mientras el compromiso esté en Status=1 se mantiene un registro de control en Budget.BudgetControl (DocumentType=11); al cambiar a otro estado se elimina.; El compromiso debe tener al menos un detalle para poder guardarse en estados distintos a anulación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Compromiso presupuestal; Obligación presupuestal; Disponibilidad presupuestal (CDP); Vigencia presupuestal; Rubro / Categoría presupuestal; Tipo de ingreso (RevenueType); Saldo de presupuesto; Anulación de compromiso; Confirmación de compromiso; Generación automática de obligación y orden de pago; Control presupuestario (BudgetControl)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Status = 3 (anulación) → Actualiza Commitment con datos de anulación (AnnulmentUser/Date/Concept/Description) y pone DebitModificationValue=InitialValue, TotalCommitment=0, Balance=0 en CommitmentDetail else Procesa guardado/confirmación con validaciones y reescritura de detalles; si Id = 0 → Solicita secuencia con Common.SP_GetSequence (tipo 200, IdForm=231) e inserta nueva cabecera en Budget.Commitment else Actualiza la cabecera existente en Budget.Commitment; si CommitmentType = 1 (afecta disponibilidad) → Valida que cada detalle tenga AvailabilityDetailId, que la fecha de la disponibilidad no sea posterior al compromiso, que la fecha de vencimiento no sea anterior, y que la suma por AvailabilityDetailId no exceda el Balance de AvailabilityDetail else Valida que ningún detalle tenga AvailabilityDetailId y que la suma por (CategoryId, RevenueTypeId) no exceda el Balance de Budget.Budget; si Status = 2 (confirmado) y CommitmentType = 1 → Aumenta ExecutedValue y disminuye Balance en Budget.AvailabilityDetail por el InitialValue de cada detalle del compromiso else Si Status=2 y CommitmentType<>1, hace lo mismo sobre Budget.Budget (joineando por CategoryId y RevenueTypeId); si Status = 2 y AutomaticObligation = 1 → Construye un XML de obligación (ObligationType=1, Status=2, Observations=''Obligación automática: ''+observaciones) y ejecuta Budget.SP_SaveObligation_Output; si retorna error, aborta con CodeResult=999; si Status = 1 (borrador) → Inserta en Budget.BudgetControl (DocumentType=11) si no existe ya el documento else Elimina de Budget.BudgetControl el registro con DocumentType=11 y DocumentNumber=@Code', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Budget.SP_SaveObligation_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Commitment; Budget.Obligation; Budget.CommitmentDetail; Budget.BudgetaryValidity; Budget.AvailabilityDetail; Budget.Availability; Budget.Category; Budget.RevenueType; Budget.Budget; Budget.BudgetControl', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCommitment_Output';
-- GO
