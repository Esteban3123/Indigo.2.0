-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-09-30
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliacion
-- =============================================
CREATE PROCEDURE [Glosas].[SP_SaveConciliation_Output]
    @ConciliationXml AS XML,
	@CodeUser AS VARCHAR(20),
	------------------------------------------------------
	@CodeResult Int OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@Id INT OUTPUT,
	@ConciliationConsecutive NUMERIC(18,0) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Nit VARCHAR(20), 
			@NitName VARCHAR(150),
			@ConciliationDate DATETIME,
			@DocumentDate DATETIME, 
			@DocumentNumber VARCHAR(50),
			@Comment VARCHAR(500),			
			@State CHAR(1),
			@ConfirmUser INT,
			@RadicatedUser INT,
			------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los participantes
	DECLARE @ConciliationParticipants TABLE
	(
		Id INT,
		Type CHAR(1),
		FullName VARCHAR(200),
		Position VARCHAR(200),
		ChangeTracker VARCHAR(30)
	)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@ConciliationConsecutive = t.x.value('ConciliationConsecutive[1]','NUMERIC(18,0)'),
				@Nit = t.x.value('Nit[1]','VARCHAR(20)'),
				@NitName = t.x.value('NitName[1]','VARCHAR(150)'),
				@ConciliationDate = t.x.value('ConciliationDate[1]','DATETIME'),
				@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
				@DocumentNumber = t.x.value('DocumentNumber[1]','VARCHAR(50)'),
				@Comment = t.x.value('Comment[1]','VARCHAR(500)'),
				@State = t.x.value('State[1]','CHAR(1)'),
				@ConfirmUser = t.x.value('ConfirmUser[1]','INT'),
				@RadicatedUser = t.x.value('RadicatedUser[1]','INT')
		FROM @ConciliationXml.nodes('/ConciliationC') t(x)

		--Valido que el estado corresponda con la accion a realizar
		IF EXISTS (SELECT 1 FROM Glosas.ConciliationC WITH (NOLOCK) WHERE Id = @Id AND State <> '1')
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('La conciliación se encuentra en estado: ', CASE State
																			WHEN '2' THEN 'Confirmado'
																			WHEN '3' THEN 'Anulado'
																			ELSE 'N/A'
																		END)
			FROM Glosas.ConciliationC WITH (NOLOCK)
			WHERE Id = @Id
			RETURN
		END

		/****************************************************** ******************************************************/

		IF @State = '3'
		BEGIN
			--Valido que los detalles no hayan sido confirmados
			IF EXISTS (SELECT 1 FROM Glosas.ConciliationD WITH (NOLOCK) WHERE ConciliationCId = @Id AND State <> '1')
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existen facturas que ya fueron conciliadas'
				RETURN
			END

			--Valido que los movimientos de glosa no hayan sido confirmados
			IF EXISTS (SELECT 1 FROM Glosas.GlosaMovementGlosa WITH (NOLOCK) WHERE ConciliationCId = @Id AND State = '6')
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existen movimientos de glosa ya conciliados'
				RETURN
			END

			--Valido que los movimientos parciales de glosa no hayan sido confirmados
			IF EXISTS (SELECT 1 FROM Glosas.GlosaMovementGlosaConciliation WITH (NOLOCK) WHERE ConciliationCId = @Id AND State = 2)
			BEGIN
				SELECT	@CodeResult = 999, 
						@MessageResult = 'Existen movimientos parciales de glosa ya conciliados'
				RETURN
			END

			UPDATE gpg
				SET gpg.State = IIF(gpg.TempState = '7', gpg.State, gpg.TempState)
			FROM Glosas.ConciliationD cd WITH (NOLOCK)
			JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON cd.GlosaPortfolioId = gpg.Id
			WHERE cd.ConciliationCId = @Id

			UPDATE gmg
				SET gmg.ValueAcceptedIPSconciliation = gmg.ValueAcceptedIPSconciliation - ISNULL(gmgc.ValueAcceptedIPSconciliation, gmg.ValueAcceptedIPSconciliation),
					gmg.ValueAcceptedEAPBconciliation = gmg.ValueAcceptedEAPBconciliation - ISNULL(gmgc.ValueAcceptedEAPBconciliation, gmg.ValueAcceptedEAPBconciliation),
					gmg.State = IIF(gmg.TempState = '5', gmg.State, gmg.TempState),
					gmg.rationaleConciliation = NULL,
					gmg.RationaleDateConciliation = NULL,
					gmg.ConciliationCId = NULL
			FROM Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) 
			LEFT JOIN Glosas.GlosaMovementGlosaConciliation gmgc WITH (NOLOCK) ON gmg.Id = gmgc.GlosaMovementGlosaId
			WHERE ISNULL(gmgc.ConciliationCId, gmg.ConciliationCId) = @Id

			UPDATE Glosas.ConciliationD
				SET State = @State
			WHERE ConciliationCId = @Id

			UPDATE Glosas.ConciliationC
				SET State = @State,
					ModificationUser = @CodeUser,
					ModificationDate = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			INSERT INTO @ConciliationParticipants 
			(
				Id, Type, FullName, Position, ChangeTracker
			)
				SELECT	t.x.value('Id[1]','INT'),
						t.x.value('Type[1]','CHAR(1)'),
						t.x.value('FullName[1]','VARCHAR(200)'),
						t.x.value('Position[1]','VARCHAR(200)'),
						t.x.value('ChangeTracker[1]', 'VARCHAR(30)')
				FROM @ConciliationXml.nodes('/ConciliationC/ConciliationParticipants') t(x)

			SET @ConfirmUser = CASE WHEN @State = '2' THEN @ConfirmUser ELSE NULL END
			DECLARE @ConfirmationDate DATETIME = CASE WHEN @State = '2' THEN [Common].[GETDATE]() ELSE NULL END

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				DECLARE @SequenceConciliation DECIMAL(20)
				
				UPDATE c
					SET @SequenceConciliation = c.NumberConsecutive += 1
				FROM Common.Consecutive c 
				WHERE c.Code = '2'

				

				SELECT @ConciliationConsecutive = CAST(@SequenceConciliation AS BIGINT)

				--Validar que no exista ya un documento con el mismo codigo
				IF EXISTS (SELECT 1 FROM Glosas.ConciliationC WHERE ConciliationConsecutive = @ConciliationConsecutive)
				BEGIN
					SELECT @CodeResult = 999, 
						   @MessageResult = CONCAT('Ya existe una conciliación con codigo: ', @ConciliationConsecutive)
					RETURN
				END

				--Se inserta la cabecera
				INSERT INTO [Glosas].[ConciliationC]
				(
					[ConciliationConsecutive],[Nit],[NitName],[ConciliationDate],[DocumentDate],[DocumentNumber],[Comment],
					[State],[CreationUser],[CreationDate],[ConfirmUser],[ConfirmDate],[RadicatedUser]
				)
				SELECT @ConciliationConsecutive,@Nit, @NitName,@ConciliationDate,@DocumentDate,@DocumentNumber,@Comment,
					@State,@CodeUser,[Common].[GETDATE](),@ConfirmUser,@ConfirmationDate,@RadicatedUser

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Glosas].[ConciliationC]
					SET [ConciliationConsecutive] = @ConciliationConsecutive,
						[Nit] = @Nit,
						[NitName] = @NitName,
						[ConciliationDate] = @ConciliationDate,
						[DocumentDate] = @DocumentDate,
						[DocumentNumber] = @DocumentNumber,
						[Comment] = @Comment,
						[State] = @State,
						[ModificationUser] = @CodeUser,
						[ModificationDate] = [Common].[GETDATE](),
						[ConfirmUser] = @ConfirmUser,
						[ConfirmDate] = @ConfirmationDate,
						[RadicatedUser] = @RadicatedUser
				WHERE Id = @Id
			END

			/*************************************************************************************/

			--- Inserto los detalles de la dispensacion
			INSERT INTO [Glosas].[ConciliationParticipants]
			(
				ConciliationCId, Type, FullName, Position
			)
				SELECT	@Id, Type, FullName, Position
				FROM @ConciliationParticipants tcp
				WHERE tcp.ChangeTracker = 'Added'

			-- Actualizo los detalles de los lotes
			UPDATE cp
				SET cp.Type = tcp.Type,
					cp.FullName = tcp.FullName,
					cp.Position = tcp.Position
			FROM @ConciliationParticipants tcp
			JOIN Glosas.ConciliationParticipants cp WITH (NOLOCK) ON tcp.Id = cp.Id
			WHERE cp.ConciliationCId = @Id AND tcp.ChangeTracker = 'Modified'

			--eliminamos los detalles
			DELETE cp
			FROM @ConciliationParticipants tcp
			JOIN Glosas.ConciliationParticipants cp ON tcp.Id = cp.Id
			WHERE cp.ConciliationCId = @Id AND tcp.ChangeTracker = 'Deleted'

			/*************************************************************************************/

			IF @State = '2'
			BEGIN
				/*******************************  MOVIMIENTO GLOSA *******************************/
				-- Se guarda el estado del movimiento de glosa antes de la conciliacion
				UPDATE gmg
					SET gmg.TempState = gmg.State
				FROM Glosas.ConciliationD cd
				JOIN Glosas.GlosaMovementGlosa gmg ON cd.InvoiceNumber = gmg.InvoiceNumber
				WHERE cd.ConciliationCId = @Id
					AND gmg.ValuePendingConciliation = 0

				-- Se actualiza el estado del movimiento de glosa conciliado
				UPDATE gmg
					SET gmg.State = 6
				FROM Glosas.ConciliationD cd
				JOIN Glosas.GlosaMovementGlosa gmg ON cd.InvoiceNumber = gmg.InvoiceNumber
				WHERE cd.ConciliationCId = @Id
					AND gmg.ValuePendingConciliation = 0

				/********************************  CABECERA GLOSA ********************************/
				-- Se guarda el estado del la glosa antes de la conciliacion
				UPDATE gpg
					SET gpg.TempState = gpg.State
				FROM Glosas.ConciliationD cd
				JOIN Glosas.GlosaPortfolioGlosada gpg ON cd.InvoiceNumber = gpg.InvoiceNumber
				WHERE cd.ConciliationCId = @Id
					AND gpg.BalanceGlosa = 0

				-- Se actualiza el estado del la glosa conciliada
				UPDATE gpg
					SET gpg.State = 8
				FROM Glosas.ConciliationD cd
				JOIN Glosas.GlosaPortfolioGlosada gpg ON cd.InvoiceNumber = gpg.InvoiceNumber
				WHERE cd.ConciliationCId = @Id
					AND gpg.BalanceGlosa = 0
			END
		END

		/**************************************** RESULTADO ****************************************/

		SELECT	@CodeResult = 0, 
				@MessageResult = CASE @State
					WHEN '2' THEN CONCAT('Se guardó y confirmó la Conciliacion con consecutivo ', @ConciliationConsecutive)
					WHEN '3' THEN CONCAT('Se anuló la Conciliacion con consecutivo ', @ConciliationConsecutive)
					ELSE CONCAT('Se guardó la Conciliacion con consecutivo ', @ConciliationConsecutive)
				END
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999, 
				@MessageResult = CONCAT('SP_SaveConciliation: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH
END