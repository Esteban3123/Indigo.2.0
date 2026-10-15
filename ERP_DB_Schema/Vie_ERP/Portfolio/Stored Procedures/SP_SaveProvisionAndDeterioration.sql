-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 03/12/2015
-- Description:	Procedimiento que se encarga de guardar y actualizar la provision y deterioro
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SaveProvisionAndDeterioration] 
	@PortfolioProvisionXml AS XML,
	@PortfolioProvisionDetailForDeleteXml AS XML,
	@CodeUser as varchar(20)
AS
BEGIN
	
	--Se declaran las variables para la cabecera de la provision
	declare @Id int, 
			@Code varchar(20), 
			@DocumentDate date, 
			@CourtDate as date, 
			@DocumentType tinyint, 
			@Description varchar(500), 
			@OperatingUnitId int,
			@Status tinyint, 
			@ApplyDeterioration tinyint

	--Tabla para almacenar los detalles de la provision
	declare @PortfolioProvisionDetail table
	(
		Id int
		, PortfolioProvisionId int
		, ConfirmDateAccountReceivable datetime
		, AccountReceivableId int
		, InvoiceNumber varchar(50)
		, Days int
		, AgesId int
		, FacturerValue decimal(18,2)
		, BalanceAccountReceivable numeric(18,2)
		, ValueGlosado decimal(18,2)		
		, Expectative int
		, [Percentage] numeric(7,4)
		, NetPresentValue decimal(18,2)
		, [Value] numeric(18,2)
		, AccumulatedDeterioration decimal(18,2)
		, LegalBookId int
		, PortfolioDeteriorationClassificationId int
	)

	BEGIN TRY		
		--Se obtiene la cabecera del xml(PortfolioProvision)
		SELECT 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@DocumentDate = t.x.value('DocumentDate[1]','varchar(20)'),
			@CourtDate = t.x.value('CourtDate[1]','varchar(20)'),
			@DocumentType = t.x.value('DocumentType[1]','tinyint'),
			@Description = t.x.value('Description[1]','varchar(500)'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@Status = t.x.value('Status[1]','tinyint'),
			@ApplyDeterioration = t.x.value('ApplyDeterioration[1]','tinyint')
		FROM @PortfolioProvisionXml.nodes('/PortfolioProvision') t(x)

		IF EXISTS (SELECT 1 FROM Portfolio.PortfolioProvision pp WHERE pp.Id = @Id AND pp.Status <> 1)
		BEGIN
			SELECT	999 as CodeMessage, 
					'La Modificación de Obligación se encuentra en estado: ' + IIF(pp.Status = 2, 'Confirmado', 'Anulado') as Message, 
					'' as Code, 0 as Id
			FROM Portfolio.PortfolioProvision pp 
			WHERE pp.Id = @Id
			RETURN
		END

		--Se obtiene los detalles del xml(PortfolioProvisionDetail)
		INSERT INTO @PortfolioProvisionDetail
			(
				Id, 
				PortfolioProvisionId, 
				ConfirmDateAccountReceivable, 
				AccountReceivableId, 
				InvoiceNumber, 
				Days,
				AgesId, 
				FacturerValue, 
				BalanceAccountReceivable, 
				ValueGlosado,
				Expectative, 
				Percentage, 
				NetPresentValue,
				Value,
				AccumulatedDeterioration,
				LegalBookId,
				PortfolioDeteriorationClassificationId
			)
			SELECT 
				t.x.value('Id[1]','int') as Id,
				t.x.value('PortfolioProvisionId[1]','int') as PortfolioProvisionId,
				CASE 
				  WHEN NULLIF(LTRIM(RTRIM(t.x.value('(ConfirmDateAccountReceivable/text())[1]', 'nvarchar(50)'))), '') IS NULL 
					THEN NULL
				  ELSE COALESCE(
						 TRY_CONVERT(datetime, NULLIF(LTRIM(RTRIM(t.x.value('(ConfirmDateAccountReceivable/text())[1]', 'nvarchar(50)'))), ''), 126), -- 2026-03-05T10:20:30
						 TRY_CONVERT(datetime, NULLIF(LTRIM(RTRIM(t.x.value('(ConfirmDateAccountReceivable/text())[1]', 'nvarchar(50)'))), ''), 121), -- 2026-03-05 10:20:30.000
						 TRY_CONVERT(datetime, NULLIF(LTRIM(RTRIM(t.x.value('(ConfirmDateAccountReceivable/text())[1]', 'nvarchar(50)'))), ''), 120), -- 2026-03-05 10:20:30
						 TRY_CONVERT(datetime, NULLIF(LTRIM(RTRIM(t.x.value('(ConfirmDateAccountReceivable/text())[1]', 'nvarchar(50)'))), ''), 103)  -- 05/03/2026
					   )
				END AS ConfirmDateAccountReceivable,
				t.x.value('AccountReceivableId[1]','int') as AccountReceivableId,
				t.x.value('InvoiceNumber[1]','varchar(50)') as InvoiceNumber,
				t.x.value('Days[1]','int') as Days,
				t.x.value('AgesId[1]','int') as AgesId,
				t.x.value('FacturerValue[1]','decimal(18,2)') AS FacturerValue,
				t.x.value('BalanceAccountReceivable[1]','numeric(18,2)') as BalanceAccountReceivable,
				t.x.value('ValueGlosado[1]','decimal(18,2)') AS ValueGlosado,				
				t.x.value('Expectative[1]','int') AS Expectative,
				t.x.value('Percentage[1]','numeric(7,4)') as Percentage,
				t.x.value('NetPresentValue[1]','decimal(18,2)') NetPresentValue,
				t.x.value('Value[1]','numeric(18,2)') as Value,
				t.x.value('AccumulatedDeterioration[1]','numeric(18,2)') as AccumulatedDeterioration,
				t.x.value('LegalBookId[1]','int') as LegalBookId,
				t.x.value('PortfolioDeteriorationClassificationId[1]','int') as PortfolioDeteriorationClassificationId
			from @PortfolioProvisionXml.nodes('/PortfolioProvision/PortfolioProvisionDetail') t(x)
		
		--Se crea el consecutivo siempre y cuando el código este vacío
		IF @Code = '' And @Id = 0
		BEGIN
			-- Consultamos la secuencia numerica del form
			DECLARE @idSequenceDetail int,
					@pattern varchar(300),
					@NextS int

			SELECT @pattern = cs.Pattern, @NextS = bsd.[Next] , @idSequenceDetail = bsd.Id  
			FROM Portfolio.PortfolioSequenceDetail bsd 
			JOIN Portfolio.PortfolioSequence bs on bs.Id = bsd.IdSequensePortfolioC 
			JOIN Common.Sequense cs on cs.Id = bsd.IdSequense
			WHERE bs.IdForm = '1927'
			
			IF (@idSequenceDetail is null)
			BEGIN
			 SELECT 999 as CodeMessage, 'No se encontró secuencia numérica para el formulario' as Message, '' as Code, 0 as Id
			 RETURN
			END

			SELECT @Code = dbo.GetSequence('',@pattern,@NextS)
			UPDATE Portfolio.PortfolioSequenceDetail SET [Next] += 1 WHERE Id = @idSequenceDetail
		END
		
		SELECT @ApplyDeterioration = CASE @ApplyDeterioration WHEN 0 THEN null ELSE @ApplyDeterioration END

		IF @Id = 0 --Si el registro es nuevo guardo
		BEGIN
			INSERT INTO Portfolio.PortfolioProvision
					(Code, DocumentDate, CourtDate, DocumentType, Description, OperatingUnitId, Status, CreationUser, CreationDate, ApplyDeterioration)
			VALUES (@Code,@DocumentDate,@CourtDate,@DocumentType,@Description,@OperatingUnitId,@Status, @CodeUser, [Common].[GETDATE](),@ApplyDeterioration)

			SET @Id = SCOPE_IDENTITY()
		END
		ELSE
		BEGIN
			declare @AnnulmentUser as varchar(20) = case when @Status <> 3 then null else @CodeUser end
			declare @AnnulmentDate as datetime = case when @Status <> 3 then null else [Common].[GETDATE]() end

			Update Portfolio.PortfolioProvision
				set Code = @Code, 
					DocumentDate = @DocumentDate, 
					CourtDate = @CourtDate, 
					DocumentType = @DocumentType, 
					[Description] = @Description, 
					OperatingUnitId = @OperatingUnitId, 
					[Status] = IIF(@Status = 2, Status, @Status), 
					ModificationUser = @CodeUser, ModificationDate = [Common].[GETDATE](),
					AnnulmentUser = @AnnulmentUser, 
					AnnulmentDate = @AnnulmentDate, 
					ApplyDeterioration = @ApplyDeterioration
			WHERE Id = @Id
		END
		
		--Se guardan los detalles siempre y cuando no se esté anulando
		IF @Status <> 3
		BEGIN
			--Eliminamos los detalles indicados
			DELETE ppd
			FROM @PortfolioProvisionDetailForDeleteXml.nodes('/PortfolioProvisionDetail') t(x)
			JOIN Portfolio.PortfolioProvisionDetail ppd ON t.x.value('Id[1]','int') = ppd.Id
			WHERE ppd.PortfolioProvisionId = @Id;

			/* ===========================================================
			   UPSERT POR LLAVE ÚNICA:
			   (PortfolioProvisionId, AccountReceivableId, LegalBookId) NULL-safe
			   Evita duplicados por UQ_* y actualiza campos no clave.
			   =========================================================== */
			;WITH Src AS
			(
				SELECT
					@Id AS PortfolioProvisionId,
					d.ConfirmDateAccountReceivable,
					d.AccountReceivableId,
					d.InvoiceNumber,
					d.Days,
					d.AgesId,
					d.FacturerValue,
					d.BalanceAccountReceivable,
					d.ValueGlosado,
					d.Expectative,
					d.[Percentage],
					d.NetPresentValue,
					d.[Value],
					d.AccumulatedDeterioration,
					d.LegalBookId,
					d.PortfolioDeteriorationClassificationId,
					-- Datos para control contabilización deterioro
					IIF(@DocumentType = 2, ar.DeteriorationBalanceCurrentYear, 0)  AS DeteriorationBalanceCurrentYear,
					IIF(@DocumentType = 2, ar.DeteriorationBalancePreviousYear, 0) AS DeteriorationBalancePreviousYear,
					IIF(@DocumentType = 2, ar.CurrentDeteriorationYear, NULL)      AS CurrentDeteriorationYear,
					ROW_NUMBER() OVER
					(
						PARTITION BY d.AccountReceivableId, d.LegalBookId
						ORDER BY ISNULL(NULLIF(d.Id,0), 0) DESC
					) AS rn
				FROM @PortfolioProvisionDetail d
				JOIN Portfolio.AccountReceivable ar ON d.AccountReceivableId = ar.Id
			),
			SrcDedup AS
			(
				SELECT *
				FROM Src
				WHERE rn = 1
			)
			MERGE Portfolio.PortfolioProvisionDetail WITH (HOLDLOCK) AS tgt
			USING SrcDedup AS src
				ON  tgt.PortfolioProvisionId  = src.PortfolioProvisionId
				AND tgt.AccountReceivableId   = src.AccountReceivableId
				AND (
						(tgt.LegalBookId = src.LegalBookId)
					 OR (tgt.LegalBookId IS NULL AND src.LegalBookId IS NULL)
					)
			WHEN MATCHED THEN
				UPDATE SET
					tgt.ConfirmDateAccountReceivable = src.ConfirmDateAccountReceivable,
					tgt.InvoiceNumber                = src.InvoiceNumber,
					tgt.Days                         = src.Days,
					tgt.AgesId                       = src.AgesId,
					tgt.FacturerValue                = src.FacturerValue,
					tgt.BalanceAccountReceivable     = src.BalanceAccountReceivable,
					tgt.ValueGlosado                 = src.ValueGlosado,
					tgt.Expectative                  = src.Expectative,
					tgt.Percentage                   = src.[Percentage],
					tgt.NetPresentValue              = src.NetPresentValue,
					tgt.Value                        = src.[Value],
					tgt.AccumulatedDeterioration     = src.AccumulatedDeterioration,
					-- No cambia la llave, pero se mantiene consistente (siempre igual al match)
					tgt.LegalBookId                  = src.LegalBookId,
					tgt.PortfolioDeteriorationClassificationId = src.PortfolioDeteriorationClassificationId,
					-- Control contabilización deterioro
					tgt.DeteriorationBalanceCurrentYear  = src.DeteriorationBalanceCurrentYear,
					tgt.DeteriorationBalancePreviousYear = src.DeteriorationBalancePreviousYear,
					tgt.CurrentDeteriorationYear         = src.CurrentDeteriorationYear
			WHEN NOT MATCHED BY TARGET THEN
				INSERT
				(
					PortfolioProvisionId,
					ConfirmDateAccountReceivable,
					AccountReceivableId,
					InvoiceNumber,
					Days,
					AgesId,
					FacturerValue,
					BalanceAccountReceivable,
					ValueGlosado,
					Expectative,
					Percentage,
					NetPresentValue,
					Value,
					AccumulatedDeterioration,
					DeteriorationBalanceCurrentYear,
					DeteriorationBalancePreviousYear,
					CurrentDeteriorationYear,
					LegalBookId,
					PortfolioDeteriorationClassificationId
				)
				VALUES
				(
					src.PortfolioProvisionId,
					src.ConfirmDateAccountReceivable,
					src.AccountReceivableId,
					src.InvoiceNumber,
					src.Days,
					src.AgesId,
					src.FacturerValue,
					src.BalanceAccountReceivable,
					src.ValueGlosado,
					src.Expectative,
					src.[Percentage],
					src.NetPresentValue,
					src.[Value],
					src.AccumulatedDeterioration,
					src.DeteriorationBalanceCurrentYear,
					src.DeteriorationBalancePreviousYear,
					src.CurrentDeteriorationYear,
					src.LegalBookId,
					src.PortfolioDeteriorationClassificationId
				);

		END

		--Se retorna el ok
		SELECT	0 as CodeMessage, 
				CASE @Status
				   WHEN 3 THEN CONCAT('Se anuló el registro con código ', @Code)
				   ELSE CONCAT('Se guardó el registro con código ', @Code)
			   END as Message, @Code as Code, @Id as Id		
	END TRY
	BEGIN CATCH
		SELECT 999 as CodeMessage, ERROR_MESSAGE() + ' linea: ' + cast(ERROR_LINE() as varchar(20) )  as Message, '' as Code, 0 as Id
	END CATCH
END