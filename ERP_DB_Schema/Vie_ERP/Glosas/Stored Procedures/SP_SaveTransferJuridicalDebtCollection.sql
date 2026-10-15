--=============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-04-05
-- Description:	Procedimiento que se encarga de guardar, actualizar un traslado a cobro jurídico
-- =============================================
CREATE PROCEDURE [Glosas].[SP_SaveTransferJuridicalDebtCollection] 
    @TransferJuridicalDebtCollectionXml AS XML,
	@UserCode AS VARCHAR(20),
	@IndigoGlossesIntegration TINYINT,
	@CompanyType TINYINT
AS
BEGIN
	SET NOCOUNT ON

	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@OperatingUnitId INT, 
			@JuridicalTransferConsecutive DECIMAL(18,0),
			@CustomerId INT,			
			@JuridicalTransferDate DATETIME,
			@DocumentNumber VARCHAR(50),			
			@FilingUnitSourceId INT,
			@FilingUnitTargetId INT,
			@LawyerId INT,
			@DemandStatusId INT,
			@DocumentDate DATETIME,
			@Comment VARCHAR(500),
			@State CHAR(1),
			------------------------------
			@UnReconciledInvoice BIT,
			@Reclassified BIT,
			------------------------------
			@Message VARCHAR(MAX)

	--Tabla temporal para obtener los detalles
	DECLARE @TransferJuridicalDebtCollectionDetail TABLE
	(
		Id INT, 
		PortfolioGlosaId INT,
		AccountReceivableId INT,
		LegalTransferValue MONEY,
		InvoiceNumber VARCHAR(50),
		AccountReceivableDate DATETIME,
		ChangeTracker VARCHAR(50)
	)

	BEGIN TRY		
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','INT'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','INT'),
				@JuridicalTransferConsecutive = t.x.value('JuridicalTransferConsecutive[1]','DECIMAL(18,0)'),
				@CustomerId = t.x.value('CustomerId[1]','INT'),
				@JuridicalTransferDate = t.x.value('JuridicalTransferDate[1]','DATETIME'),
				@DocumentNumber = t.x.value('DocumentNumber[1]','VARCHAR(300)'),
				@FilingUnitSourceId = t.x.value('FilingUnitSourceId[1]','INT'),
				@FilingUnitTargetId = t.x.value('FilingUnitTargetId[1]','INT'),
				@LawyerId = t.x.value('LawyerId[1]','INT'),
				@DemandStatusId = t.x.value('DemandStatusId[1]','INT'),
				@DocumentDate = t.x.value('DocumentDate[1]','DATETIME'),
				@Comment = t.x.value('Comment[1]','VARCHAR(500)'),
				@State = t.x.value('State[1]','CHAR(1)')
		FROM @TransferJuridicalDebtCollectionXml.nodes('/TransferJuridicalDebtCollectionC') t(x)

		IF EXISTS (SELECT 1 FROM Glosas.TransferJuridicalDebtCollectionC WHERE Id = @Id AND State <> 1) and @CompanyType <> 2
		BEGIN
			SELECT	999 AS CodeResult,
					'El Traslado a Cobro Jurídico se encuentra en estado: ' + IIF(State = 2, 'Confirmado', IIF(State = 3, 'Anulado', 'Reversado')) AS MessageResult,
					@Id AS Id,
					@JuridicalTransferConsecutive AS Code
			FROM Glosas.TransferJuridicalDebtCollectionC
			WHERE Id = @Id AND State <> 1
			RETURN
		END

		IF @State = 3
		BEGIN
			UPDATE Glosas.TransferJuridicalDebtCollectionC
				SET State = @State,
					ModificationUser = @UserCode,
					ModificationDate = [Common].[GETDATE]()
			WHERE Id = @Id
		END
		ELSE
		BEGIN
			--Se obtiene los detalles que vienen en el xml
			INSERT INTO @TransferJuridicalDebtCollectionDetail
				SELECT 
					ISNULL(t.x.value('Id[1]','INT'), 0) AS Id,
					t.x.value('PortfolioGlosaId[1]','INT') AS PortfolioGlosaId,
					t.x.value('AccountReceivableId[1]','INT') AS AccountReceivableId,
					t.x.value('LegalTransferValue[1]','MONEY') AS LegalTransferValue,
					t.x.value('InvoiceNumber[1]','VARCHAR(50)') InvoiceNumber,
					t.x.value('AccountReceivableDate[1]','DATETIME') AS AccountReceivableDate,
					t.x.value('ChangeTracker[1]','VARCHAR(50)') AS ChangeTracker
				FROM @TransferJuridicalDebtCollectionXml.nodes('/TransferJuridicalDebtCollectionC/TransferJuridicalDebtCollectionD') t(x)

			--------------------------------- DETALLES A ELIMINAR ---------------------------------

			DELETE tjdcd
			FROM @TransferJuridicalDebtCollectionDetail d
			JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON d.Id = tjdcd.Id
			WHERE d.ChangeTracker = 'Deleted' AND tjdcd.TransferJuridicalDebtCollectionCId = @Id

			DELETE d
			FROM @TransferJuridicalDebtCollectionDetail d
			WHERE d.ChangeTracker = 'Deleted' OR d.Id > 0

			------------------------------  OBTENGO DATOS NECESARIOS ------------------------------

			-- Se obtiene parametro que indica si se puede trasladar facturas con glosas
			SELECT	@UnReconciledInvoice = sp.UnReconciledInvoice,
					@Reclassified = sp.LegalCollection
			FROM Portfolio.SettingPortfolio sp
			--WHERE sp.OperatingUnitId = @OperatingUnitId
			

			------------------------------------  VALIDACIONES ------------------------------------
			
			-- Validar la factura ingresada existe
			IF EXISTS 
			(
				SELECT 1
				FROM @TransferJuridicalDebtCollectionDetail d
				LEFT JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND D.InvoiceNumber = ar.InvoiceNumber AND ar.Balance > 0
				WHERE
				(
					ar.Id IS NULL
					OR ar.PortfolioStatus IN (1, 2, 4, 16)
				)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + IIF(
								ar.Id IS NULL, 
								CONCAT(' - La factura ', d.InvoiceNumber, ' no existe o no esta asignada al tercero seleccionado'), 
								IIF
								(
									NOT (ar.Balance > 0),
									CONCAT(' - La factura ', d.InvoiceNumber, ' no tiene saldo'),
									CONCAT(' - La factura ', d.InvoiceNumber, ' se encuentra ', CASE ar.PortfolioStatus 
																										WHEN 1 THEN 'sin radicar'
																										WHEN 2 THEN 'radicada sin confirmar'
																										WHEN 4 THEN 'radicada sin confirmar'
																										WHEN 16 THEN 'trasladada a cobro jurídico'
																									  END)
								)
							)
						FROM @TransferJuridicalDebtCollectionDetail d
						LEFT JOIN Portfolio.AccountReceivable ar ON @CustomerId = ar.CustomerId AND D.InvoiceNumber = ar.InvoiceNumber AND ar.Balance > 0
						WHERE
						(
							ar.Id IS NULL
							OR ar.PortfolioStatus IN (1, 2, 4, 16)
						)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	999 AS CodeResult,
						ISNULL(@Message, 'Existen errores en los facturas cargadas') AS MessageResult,
						@Id AS Id,
						@JuridicalTransferConsecutive AS Code
				RETURN 
			END

			-- Validar las facturas glosadas
			IF EXISTS 
			(
				SELECT 1
				FROM @TransferJuridicalDebtCollectionDetail d
				JOIN Glosas.GlosaPortfolioGlosada gpg ON d.InvoiceNumber = gpg.InvoiceNumber
				WHERE
				(
					(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 9, 13))
					OR
					(@UnReconciledInvoice = 1 AND gpg.State IN (13))
				)
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - La factura ', D.InvoiceNumber, ' se encuentra ', CASE gpg.State
																											WHEN 1 THEN 'pendiente de confirmar glosa'																										
																											WHEN 4 THEN 'pendiente de confirmar reiteración'
																											WHEN 7 THEN 'pendiente confirmar conciliacón'
																											WHEN 8 THEN 'conciliada'
																											WHEN 9 THEN 'en conciliación parcial'
																											WHEN 13 THEN 'pendiente confirmar pago parcial'
																										END)
						FROM @TransferJuridicalDebtCollectionDetail d
						JOIN Glosas.GlosaPortfolioGlosada gpg ON d.InvoiceNumber = gpg.InvoiceNumber
						WHERE
						(
							(@UnReconciledInvoice = 0 AND gpg.State IN (1, 4, 7, 8, 9, 13))
							OR
							(@UnReconciledInvoice = 1 AND gpg.State IN (13))
						)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	999 AS CodeResult,
						ISNULL(@Message, 'Existen errores en los facturas con glosas') AS MessageResult,
						@Id AS Id,
						@JuridicalTransferConsecutive AS Code
				RETURN 
			END

			-- Validar si la factura ya esta agregada en un traslado a cobro jurídico
			IF EXISTS 
			(
				SELECT 1
				FROM @TransferJuridicalDebtCollectionDetail d
				JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON d.InvoiceNumber = tjdcd.InvoiceNumber
				JOIN Glosas.TransferJuridicalDebtCollectionC tjdc ON tjdcd.TransferJuridicalDebtCollectionCId = tjdc.Id
				WHERE (tjdc.State IN (1, 2) OR (tjdc.State IN (4) AND tjdcd.ReversedUser IS NULL))
			)
			BEGIN
				SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT('La factura ', D.InvoiceNumber, ' ya esta en el traslado a cobro jurídico: ', tjdc.JuridicalTransferConsecutive)
						FROM @TransferJuridicalDebtCollectionDetail d
						JOIN Glosas.TransferJuridicalDebtCollectionD tjdcd ON d.InvoiceNumber = tjdcd.InvoiceNumber
						JOIN Glosas.TransferJuridicalDebtCollectionC tjdc ON tjdcd.TransferJuridicalDebtCollectionCId = tjdc.Id
						WHERE (tjdc.State IN (1, 2) OR (tjdc.State IN (4) AND tjdcd.ReversedUser IS NULL))
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT	999 AS CodeResult,
						ISNULL(@Message, 'Existen facturas que ya estan en un traslado a cobro jurídico') AS MessageResult,
						@Id AS Id,
						@JuridicalTransferConsecutive AS Code
				RETURN 
			END

			-- Validar si existen detalles
			IF NOT (EXISTS (SELECT 1 FROM @TransferJuridicalDebtCollectionDetail d) OR EXISTS (SELECT 1 FROM Glosas.TransferJuridicalDebtCollectionD WHERE TransferJuridicalDebtCollectionCId = @Id))
			BEGIN
				SELECT	999 AS CodeResult,
						'El traslado a cobro jurídico no tiene detalles' AS MessageResult,
						@Id AS Id,
						@JuridicalTransferConsecutive AS Code
				RETURN 
			END

			------------------------------------ GUARDAR DATOS ------------------------------------

			IF @Id = 0
			BEGIN
				--Si se esta insertando por primera vez se consulta la secuencia numerica
				UPDATE c SET @JuridicalTransferConsecutive = NumberConsecutive = c.NumberConsecutive + 1
				FROM Common.Consecutive c
				WHERE c.Code = 6

				--Se inserta la cabecera
				INSERT INTO Glosas.TransferJuridicalDebtCollectionC
				(
					JuridicalTransferConsecutive,CustomerId,JuridicalTransferDate,DocumentNumber,FilingUnitSourceId,FilingUnitTargetId,LawyerId,
					DemandStatusId,DocumentDate,Comment,Reclassified,State,CreationUser,CreationDate
				)
				SELECT @JuridicalTransferConsecutive,@CustomerId,@JuridicalTransferDate,@DocumentNumber,@FilingUnitSourceId,@FilingUnitTargetId,@LawyerId,
					@DemandStatusId,@DocumentDate,@Comment,@Reclassified,@State,@UserCode,[Common].[GETDATE]()

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE Glosas.TransferJuridicalDebtCollectionC
					SET CustomerId = @CustomerId,
						JuridicalTransferDate = @JuridicalTransferDate,
						DocumentNumber = @DocumentNumber,
						FilingUnitSourceId = @FilingUnitSourceId,
						FilingUnitTargetId = @FilingUnitTargetId,
						LawyerId = @LawyerId,
						DemandStatusId = @DemandStatusId,
						DocumentDate = @DocumentDate,
						Comment = @Comment,
						Reclassified = @Reclassified,
						State = @State,
						ModificationUser = @UserCode,
						ModificationDate = [Common].[GETDATE]()
				WHERE Id = @Id
			END

			-- Se insertan los detalles
			INSERT INTO Glosas.TransferJuridicalDebtCollectionD
			(
				TransferJuridicalDebtCollectionCId,PortfolioGlosaId,AccountReceivableId,LegalTransferValue,InvoiceNumber,AccountReceivableDate
			)
			SELECT @Id,PortfolioGlosaId,AccountReceivableId,LegalTransferValue,InvoiceNumber,AccountReceivableDate
			FROM @TransferJuridicalDebtCollectionDetail
		END

		update d set d.ReversedUser = null, d.ReversedDate = null
		from Glosas.TransferJuridicalDebtCollectionC c
		inner join Glosas.TransferJuridicalDebtCollectionD d on d.TransferJuridicalDebtCollectionCId = c.Id
		where c.Id = @Id
			   
		print '@State: ' + @State
		print '@CompanyType: ' + cast(@CompanyType as varchar(20))
		print '@Reclassified: ' + cast(@Reclassified as varchar(20))

		--Si se esta confirmando el traslado y el tipo de empresa es pública
		if @State = '2' and @CompanyType = 2
		begin
			
			--**Aqui falta las validaciones del copyPaste**--
		update d set d.LegalTransferValue = temp.Balance
				from Glosas.TransferJuridicalDebtCollectionD d
				inner join
				(
					select t.Id, SUM(ara.Balance) Balance
					from Glosas.TransferJuridicalDebtCollectionD t
					inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = t.InvoiceNumber
					inner join Portfolio.AccountReceivableAccounting ara on ara.AccountReceivableId = ar.Id
					where ar.AccountReceivableType in (1, 2) and ara.Balance > 0 and t.TransferJuridicalDebtCollectionCId = @Id
					Group By ar.InvoiceNumber, t.Id
				) temp on temp.Id = d.Id
				where d.TransferJuridicalDebtCollectionCId = @Id
				
			--Si es true Reclassified
			if @Reclassified = 1
			begin		
			
				--update ara set ara.Balance = 0
				--from Glosas.TransferJuridicalDebtCollectionD t
				--inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = t.InvoiceNumber
				--inner join Portfolio.AccountReceivableAccounting ara on ara.AccountReceivableId = ar.Id
				--where ar.AccountReceivableType in (1, 2) and ara.Balance > 0 and t.TransferJuridicalDebtCollectionCId = @Id
				
				update g set g.LegalTransferValue = g.BalanceGlosa, g.BalanceGlosa = 0, g.TempState = g.State, g.State = 15
				from Glosas.TransferJuridicalDebtCollectionD t
				inner join Glosas.GlosaPortfolioGlosada g on g.InvoiceNumber = t.InvoiceNumber
				where t.TransferJuridicalDebtCollectionCId = @Id

				update g set g.LegalTransferValue = g.ValuePendingConciliation, g.ValuePendingConciliation = 0
				from Glosas.TransferJuridicalDebtCollectionD t
				inner join Glosas.GlosaMovementGlosa g on g.InvoiceNumber = t.InvoiceNumber
				where t.TransferJuridicalDebtCollectionCId = @Id
				
				update ar set ar.PortfolioStatus = 16
				from Glosas.TransferJuridicalDebtCollectionD t
				inner join Portfolio.AccountReceivable ar on ar.InvoiceNumber = t.InvoiceNumber
				where t.TransferJuridicalDebtCollectionCId = @Id
			end

			insert into Glosas.DemandTransferJuridical(TransferJuridicalDebtCollectionCId, FilingUnitSourceId, FilingUnitTargetId, LawyerId, DemandStatusId,
			CreationDate, CreationUser)
			select top 1 @Id, t.FilingUnitSourceId, t.FilingUnitTargetId, t.LawyerId, t.DemandStatusId, [Common].[GETDATE](), @UserCode
			from Glosas.TransferJuridicalDebtCollectionC t
			where t.Id = @Id

			update Glosas.TransferJuridicalDebtCollectionC set State = 2 where Id = @Id
		end

		SELECT 0 AS CodeResult, '' AS MessageResult, @Id as Id, @JuridicalTransferConsecutive as Code
	END TRY
	BEGIN CATCH
		SELECT 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult, 0 AS Id, CAST(0 AS DECIMAL) AS Code
	END CATCH
END