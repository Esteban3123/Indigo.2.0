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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o actualiza registros de provisiones contables y deterioro de cartera en la institución. Permite crear nuevas provisiones (generando un código consecutivo automático mediante la secuencia configurada en PortfolioSequence y PortfolioSequenceDetail) o modificar provisiones existentes, siempre que estén en estado borrador; si ya están confirmadas o anuladas, rechaza la operación. Registra tanto la cabecera de la provisión (fecha de documento, fecha judicial, tipo de documento, descripción, unidad operativa y si aplica deterioro) como el detalle por cada cuenta por cobrar o factura afectada, incluyendo días de mora, rango de edad de la deuda, valor facturado, saldo, valor glosado, expectativa de cobro, porcentaje de provisión, valor presente neto, valor provisionado, deterioro acumulado y clasificación de deterioro de cartera. También permite eliminar líneas de detalle previamente guardadas a través de un XML de eliminación. Es el punto central de registro contable de provisiones y deterioro de la cartera por cobrar de la institución.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SaveProvisionAndDeterioration';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (alta, edición o anulación) un documento de provisión y deterioro de cartera con su cabecera y líneas de detalle por cuenta por cobrar, generando consecutivo si aplica y sincronizando inserts/updates/deletes de detalle.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@PortfolioProvisionXml debe contener un nodo /PortfolioProvision con los campos de cabecera (Id, Code, DocumentDate, CourtDate, DocumentType, Description, OperatingUnitId, Status, ApplyDeterioration).; Para alta sin código (@Code='''' y @Id=0), debe existir una configuración de secuencia en PortfolioSequenceDetail/PortfolioSequence para IdForm=''1927''.; Para actualizar una provisión existente, su Status debe ser 1 (Pendiente); de lo contrario la operación se rechaza.; Cada detalle del XML debe referenciar un AccountReceivableId existente en Portfolio.AccountReceivable (JOIN obligatorio en INSERT y UPDATE de detalle).; @CodeUser debe identificar al usuario que crea/modifica/anula para registrar trazabilidad.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se modifica una provisión cuyo Status sea distinto de 1 (Pendiente): se rechaza con CodeMessage=999.; El Status no puede cambiarse cuando el registro ya está Confirmado (Status=2): el UPDATE preserva el valor existente vía IIF.; AnnulmentUser y AnnulmentDate solo se asignan cuando @Status=3; en cualquier otro caso quedan en NULL.; ApplyDeterioration nunca se almacena con valor 0; se normaliza a NULL.; Los detalles solo se sincronizan (insert/update/delete) cuando el documento NO se está anulando (@Status<>3).; Los campos de control contable de deterioro (DeteriorationBalanceCurrentYear/PreviousYear, CurrentDeteriorationYear) solo se copian de AccountReceivable cuando @DocumentType=2; en otro caso son 0/NULL.; El consecutivo solo se genera cuando el código viene vacío y es un alta (@Id=0), incrementando [Next] en 1 en PortfolioSequenceDetail.; El INSERT de detalles nuevos se hace solo para registros del XML cuyo Id no existe ya en PortfolioProvisionDetail para esa provisión (LEFT JOIN con ppd.Id IS NULL).; Toda excepción se captura y se devuelve como CodeMessage=999 con ERROR_MESSAGE() y línea, sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'provisión de cartera; deterioro de cartera; cuenta por cobrar; factura; glosa (ValueGlosado); edad de cartera (AgesId); días de mora; valor presente neto; expectativa de cobro; clasificación de deterioro de cartera; anulación de documento; secuencia/consecutivo por formulario; unidad operativa; libro legal (LegalBookId)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe PortfolioProvision con Id=@Id y Status<>1 (no Pendiente) → Retorna CodeMessage=999 con mensaje indicando si está ''Confirmado'' (Status=2) o ''Anulado'' y termina sin grabar else Continúa con el flujo de guardado/actualización; si @Code='''' AND @Id=0 (registro nuevo sin código asignado) → Busca secuencia en PortfolioSequenceDetail/PortfolioSequence donde IdForm=''1927'', genera el código con dbo.GetSequence e incrementa el [Next] de la secuencia en 1 else Usa el @Code recibido sin generar consecutivo; si No se encontró secuencia (@idSequenceDetail IS NULL) durante la generación de consecutivo → Retorna CodeMessage=999 con mensaje ''No se encontró secuencia numérica para el formulario'' y termina; si @ApplyDeterioration = 0 → Se reemplaza por NULL antes de persistir else Se conserva el valor original; si @Id = 0 → INSERT en Portfolio.PortfolioProvision con CreationUser/CreationDate y obtiene @Id por SCOPE_IDENTITY() else UPDATE de la cabecera con ModificationUser/ModificationDate; si En UPDATE de cabecera: @Status = 2 (Confirmado) → Mantiene el Status actual (IIF(@Status=2, Status, @Status)) y no permite confirmar por esta vía else Asigna el nuevo @Status; si En UPDATE de cabecera: @Status = 3 (Anulado) → Asigna AnnulmentUser=@CodeUser y AnnulmentDate=GETDATE() else AnnulmentUser y AnnulmentDate quedan NULL; si @Status <> 3 (no se está anulando) → Procesa detalles: DELETE de los indicados en XML de eliminación, UPDATE de existentes e INSERT de nuevos en PortfolioProvisionDetail else Omite por completo el procesamiento de detalles; si @DocumentType = 2 al persistir detalle → Copia desde AccountReceivable los campos DeteriorationBalanceCurrentYear, DeteriorationBalancePreviousYear y CurrentDeteriorationYear else Asigna 0 a los saldos de deterioro y NULL a CurrentDeteriorationYear; si Al retornar mensaje de éxito: @Status = 3 → Mensaje ''Se anuló el registro con código ...'' else Mensaje ''Se guardó el registro con código ...''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioProvision; Portfolio.PortfolioSequenceDetail; Portfolio.PortfolioSequence; Common.Sequense; Portfolio.PortfolioProvisionDetail; Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SaveProvisionAndDeterioration';
-- GO
