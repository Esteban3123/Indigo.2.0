CREATE PROCEDURE [GeneralLedger].[SP_FiscalYearClose]
	@LegalBookId int,
	@Year int,
	@IdOperatingUnit int,
	@NitCompany varchar(20),
	@User varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRY

/******************************** DECLARACION DE VARIABLES ********************************/

		DECLARE @errors VARCHAR(MAX),
				@IdVoucherTypeClose INT, 
				@IdAccountSuperAvit INT, 
				@IdAccounttDeficit INT,
				@JournalXml xml,
				@Utility DECIMAL(20,4),
				@MessageReturn VARCHAR(MAX) = 'Se realizo correctamente el cierre anual',
				------------------------------------------------------
				@CodeMessage VARCHAR(20),
				@Message VARCHAR(MAX),
				@IdJournalVoucherResult INT

		DECLARE @TableJournalVoucher TABLE
		(
			IdJournalVoucher INT NOT NULL, 
			LegalBookId INT, 
			VoucherDate DATETIME NOT NULL, 
			Imported bit NOT NULL, 
			[Status] TINYINT NOT NULL, 
			Detail VARCHAR(500) NULL, 
			EntityCode VARCHAR(20) NULL, 
			EntityId INT NULL, 
			EntityName VARCHAR(250) NULL, 
			IsClosedYear TINYINT NOT NULL
		)

		DECLARE @TableJournalVoucherDetail TABLE
		(
			IdMainAccount INT NOT NULL, 
			IdThirdParty INT NULL, 
			IdCostCenter INT NULL,
			DebitValue DECIMAL(20, 4) NOT NULL,
			CreditValue DECIMAL(20, 4) NOT NULL,
			Detail VARCHAR(MAX) NULL,
			IdRetention INT NULL,
			RetentionRate DECIMAL(5, 2) NULL,
			BaseValue DECIMAL(18, 0) NULL,
			BillingValue DECIMAL(18, 0) NULL
		)
		--tabla temporal para almacenar el resultado del movimiento contable
		declare @resultJournalVoucher table (code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

/******************************** VALIDACIONES ********************************/

		--- Valido que el cierre no se haya hecho antes
		IF EXISTS (SELECT Id FROM GeneralLedger.LegalBook WITH (NOLOCK) WHERE Id = @LegalBookId AND @Year <= LastYearClose)
		BEGIN
			SELECT '999' AS CodeMessage, CONCAT('No se puede realizar el cierre de la vigencia ', @Year, ' debido a que ya fue realizada') AS Message, 0 IdJournalVoucher
			RETURN
		END

		--- Valido que todos los meses esten cerrados 
		IF EXISTS (SELECT Id FROM [GeneralLedger].[ClosedMonth] WITH (NOLOCK) WHERE [Year] = @Year AND Status = 1)
		BEGIN			
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + CAST([Month] AS varchar(20))
					FROM [GeneralLedger].[ClosedMonth] WITH (NOLOCK)
					WHERE [Year] = @Year AND Status = 1
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Los siguientes meses se encuentran abiertos ', CHAR(13) + CHAR(10), @errors, ', por favor cierrelos') AS Message, 0 IdJournalVoucher
			RETURN
		END

		--- Valido los comprobantes contables que se encuentran sin confirmar
		IF EXISTS (SELECT Id FROM GeneralLedger.JournalVouchers WITH (NOLOCK) WHERE [Status] = 1 AND YEAR(VoucherDate) = @Year)
		BEGIN			
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - Consecutivo: ' + CAST(jv.Consecutive AS VARCHAR(20)) + ' Tipo Comprobante: ' + jvt.Code
					FROM GeneralLedger.JournalVouchers jv WITH (NOLOCK)
					JOIN GeneralLedger.JournalVoucherTypes jvt WITH (NOLOCK) ON jv.IdJournalVoucher = jvt.Id
					WHERE jv.[Status] = 1 AND YEAR(jv.VoucherDate) = @Year
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Los siguientes documentos contables se encuentran sin confirmar:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			RETURN
		END

		--- Valido que no hayan documentos sin confirmar en tesoreria
		IF EXISTS (SELECT Id FROM Treasury.TreasuryControl WITH (NOLOCK) WHERE YEAR(DocumentDate) = @Year)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - '+ CASE DocumentType 
							WHEN 1 THEN 'Recibo Caja' 
							WHEN 2 THEN 'comprobante de egreso' 
							WHEN 3 THEN 'Notas' 
							WHEN 4 THEN 'Consignaciones' 
							WHEN 5 THEN 'Reembolso' 
							WHEN 6 THEN 'Cruce de CxC vs CxP' 
							WHEN 7 THEN 'Dispercion de fondos' 
						END  +' Codigo: ' + CAST(DocumentNumber AS VARCHAR(20))
					FROM Treasury.TreasuryControl WITH (NOLOCK)
					WHERE YEAR(DocumentDate) = @Year
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Los siguientes documentos de tesoreria se encuentran sin confirmar:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			RETURN
		END

		--- Valido que no hayan documentos sin confirmar en pagos
		IF EXISTS (SELECT Id FROM Payments.PaymentsControl WITH (NOLOCK) WHERE YEAR(DocumentDate) = @Year) 
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - '+ CASE DocumentType 
							WHEN 1 THEN 'Cuenta por pagar' 
							WHEN 2 THEN 'Notas' 
							WHEN 3 THEN 'Anticipos' 
							WHEN 4 THEN 'Traslado' 
							WHEN 5 THEN 'Saldo Inicial' 
						END  +' Codigo: ' + CAST(DocumentNumber AS VARCHAR(20))
					FROM Payments.PaymentsControl WITH (NOLOCK)
					WHERE YEAR(DocumentDate) = @Year
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			select '999' AS CodeMessage, CONCAT('Los siguientes documentos de pagos se encuentran sin confirmar:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			return
		end

		--- Valido que no hayan documentos sin confirmar en cartera
		IF EXISTS (SELECT Id FROM Portfolio.PortfolioControl WITH (NOLOCK) WHERE YEAR(DocumentDate) = @Year)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - '+ CASE DocumentType 
							WHEN 1 THEN 'Notas' 
							WHEN 2 THEN 'Traslado' 
							WHEN 3 THEN 'Documento Cuenta X Cobrar' 
						END  +' Codigo: ' + CAST(DocumentNumber AS VARCHAR(20))
					FROM Portfolio.PortfolioControl WITH (NOLOCK) 
					WHERE YEAR(DocumentDate) = @Year
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Los siguientes documentos de cartera se encuentran sin confirmar:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			RETURN
		end

		--- Valido que no hayan documentos sin confirmar en inventarios
		IF EXISTS (SELECT Id FROM Inventory.InventoryControlDocument WITH (NOLOCK) WHERE YEAR(DocumentDate) = @Year)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + case DocumentType 
							WHEN 1 THEN 'Orden de compra' 
							WHEN 2 THEN 'Remision de Entrada' 
							WHEN 3 THEN 'Ajuste de Inventario'
							WHEN 4 THEN 'Solicitudes'
							WHEN 5 THEN 'Dispensacion Farmaceutica'
							WHEN 6 THEN 'Remision de Salida'
							WHEN 7 THEN 'Devolucion de Remisiones'
							WHEN 8 THEN 'Comprobante de entrada'
							WHEN 9 THEN 'Devolucion de compra'
							WHEN 10 THEN 'Prestamo de mercancia'
							WHEN 11 THEN 'Devolucion de suministro'
							WHEN 12 THEN 'Devolucion de prestamo'
							WHEN 13 THEN 'Ordenes de Traslado'
							WHEN 14 THEN 'Devoluciones Ordenes de Traslado'
							WHEN 50 THEN 'Dispensacion Farmaceutica por paciente'
						END  +' Codigo: ' + CAST(DocumentNumber AS VARCHAR(20))
					FROM Inventory.InventoryControlDocument WITH (NOLOCK) 
					WHERE year(DocumentDate) = @Year
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Los siguientes documentos de inventario se encuentran sin confirmar:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			RETURN
		END

		---- Valido que todas las cuentas que manejen cierre por tercero tengan un tercero de cierre
		IF EXISTS (SELECT Id FROM GeneralLedger.MainAccounts ma WITH (NOLOCK) WHERE LegalBookId = @LegalBookId AND ma.CloseThirdParty = 1 AND ma.IdThirdParty IS NULL)
		BEGIN
			SELECT @errors = STUFF((
					SELECT CHAR(13) + CHAR(10) + ' - ' + ma.Number
					FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
					WHERE LegalBookId = @LegalBookId AND ma.CloseThirdParty = 1 AND ma.IdThirdParty IS NULL
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT '999' AS CodeMessage, CONCAT('Las siguientes cuentas manejan cierre de tercero pero no tienen un tercero asociado:', CHAR(13) + CHAR(10), @errors) AS Message, 0 IdJournalVoucher
			RETURN
		END

		--- Valido que existan parametros para la unidad operativa
		IF NOT EXISTS (SELECT Id FROM GeneralLedger.GeneralLedgerSettings WITH (NOLOCK) WHERE IdOperatingUnit = @IdOperatingUnit)
		BEGIN
			SELECT '999' AS CodeMessage, 'No existe parametros de contabilidad para la unidad operativa ' AS Message, 0 IdJournalVoucher
			RETURN
		END

/******************************** ASIGNACION DE VALORES A LAS VARIABLES ********************************/
				
		SELECT	@IdVoucherTypeClose = IdCloseDocument, 
				@IdAccountSuperAvit = IdSuperavitAccount, 
				@IdAccounttDeficit = IdDeficitAccount 
		FROM GeneralLedger.GeneralLedgerSettings WITH (NOLOCK) 
		WHERE IdOperatingUnit = @IdOperatingUnit

/******************************** RECLASIFICACIÓN DE LAS CUENTAS CON CIERRE POR TERCERO ********************************/
		
		--- Inserto la cabecera del comprobante contable
		INSERT @TableJournalVoucher (IdJournalVoucher, LegalBookId, VoucherDate, Imported, [Status], Detail, EntityName, IsClosedYear)
		VALUES(@IdVoucherTypeClose, @LegalBookId, CAST(CAST(@Year AS varchar(4)) + '-12-31' AS DATE), 0, 2, 'Cierre anual contable, reclasificacion de terceros', 'JournalVouchers', 2)
		
		--- Inserto los detalles del comprobante para cancelar la cuenta y el tercero
		INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
			SELECT 
				gb.IdMainAccount, 
				gb.IdThirdParty, 
				gb.IdCostCenter, 
				IIF(SUM(gb.CreditValue) - SUM(gb.DebitValue) > 0, SUM(gb.CreditValue) - SUM(gb.DebitValue), 0) AS DebitValue, 
				IIF(SUM(gb.DebitValue) - SUM(gb.CreditValue) > 0, SUM(gb.DebitValue) - SUM(gb.CreditValue), 0) AS CreditValue,
				'Reclasificacion de cuentas que manejan terceros'
			FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
			JOIN GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK) ON ma.Id = gb.IdMainAccount AND gb.IdThirdParty <> ma.IdThirdParty
			WHERE ma.CloseThirdParty = 1 AND ma.LegalBookId = @LegalBookId AND (gb.[Year] = @Year OR (gb.[Year] = @Year -1 AND gb.[Month] = 14))
			GROUP BY gb.IdMainAccount, gb.IdThirdParty, gb.IdCostCenter, ma.Nature
			HAVING ((sum(gb.DebitValue) - sum(gb.CreditValue)) <> 0)
	
		---- Inserto los detalles del comprobante para crear el saldo con el tercero del cierre
		INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
			SELECT 
				gb.IdMainAccount, 
				ma.IdThirdParty, 
				gb.IdCostCenter, 
				IIF(SUM(gb.DebitValue) - SUM(gb.CreditValue) > 0, SUM(gb.DebitValue) - SUM(gb.CreditValue), 0) AS DebitValue, 
				IIF(SUM(gb.CreditValue) - SUM(gb.DebitValue) > 0, SUM(gb.CreditValue) -SUM(gb.DebitValue), 0) AS CreditValue,
				'Reclasificacion de cuentas que manejan terceros'
			FROM GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK)
			JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = gb.IdMainAccount AND gb.IdThirdParty <> ma.IdThirdParty
			WHERE ma.CloseThirdParty = 1 AND ma.LegalBookId = @LegalBookId AND (gb.[Year] = @Year OR (gb.[Year] = @Year -1 AND gb.[Month] = 14))
			GROUP BY gb.IdMainAccount, ma.IdThirdParty, gb.IdCostCenter
			HAVING ((SUM(gb.DebitValue) - SUM(gb.CreditValue)) <> 0)

		IF EXISTS (SELECT 1 FROM @TableJournalVoucherDetail)
		BEGIN			
			SET @JournalXml = (SELECT * FROM @TableJournalVoucher AS JournalVoucher CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail FOR XML AUTO, ELEMENTS)
						
			--Se consume el sp que guarda el movimiento contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalXml,@User 
			select @CodeMessage = rjv.code,
			@Message = rjv.MessageResult,
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
			
			IF ISNULL(@CodeMessage, '999') <> '0'
			BEGIN
				SELECT @CodeMessage CodeMessage, @Message Message, 0 AS IdJournalVoucher
				RETURN
			END

			SET @MessageReturn += ', ' + @Message
			delete from @resultJournalVoucher
		END

/******************************** COMPROBANTE DE CIERRE CONTABLE ********************************/

		DELETE FROM @TableJournalVoucher
		DELETE FROM @TableJournalVoucherDetail

		--- Inserto la cabecera del comprobante contable
		INSERT @TableJournalVoucher (IdJournalVoucher, LegalBookId, VoucherDate, Imported, [Status], Detail, EntityName, IsClosedYear)
		VALUES(@IdVoucherTypeClose, @LegalBookId, CAST(CAST(@Year AS varchar(4)) + '-12-31' AS DATE), 0, 2, 'Cierre anual contable, Anulacion de cuentas de resultados', 'JournalVouchers', 2)
		
		--- Inserto los detalles del comprobante contable 
		INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
			SELECT 
				gb.IdMainAccount, 
				gb.IdThirdParty, 
				gb.IdCostCenter, 
				IIF(SUM(gb.CreditValue) - SUM(gb.DebitValue) > 0, SUM(gb.CreditValue) - SUM(gb.DebitValue), 0) AS DebitValue, 
				IIF(SUM(gb.DebitValue) - SUM(gb.CreditValue) > 0, SUM(gb.DebitValue) - SUM(gb.CreditValue), 0) AS CreditValue,
				'Cierre del año fiscal'
			FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
			JOIN GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK) ON ma.Id = gb.IdMainAccount
			JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass
			WHERE gb.[Year] = @Year and mac.[Type] = 2 AND ma.LegalBookId = @LegalBookId
			GROUP BY gb.IdMainAccount, gb.IdThirdParty, gb.IdCostCenter
			HAVING ((SUM(gb.DebitValue) - SUM(gb.CreditValue)) <> 0)

		---- Obtengo el valor de la operacion Ingresos - Gastos - Costos 
		SET @Utility = 
		(
			SELECT
				SUM(gb.CreditValue) - SUM(gb.DebitValue)
			FROM GeneralLedger.MainAccounts ma WITH (NOLOCK)
			JOIN GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK) ON ma.Id = gb.IdMainAccount
			JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass
			WHERE gb.[Year] = @Year and mac.[Type] = 2 AND ma.LegalBookId = @LegalBookId
		)

		---- Inserto los detalles del comprobante para crear el saldo con el tercero del cierre
		IF @Utility > 0 
		BEGIN
			IF EXISTS (SELECT Id from GeneralLedger.MainAccounts WITH (NOLOCK) WHERE Id = @IdAccountSuperAvit AND LegalBookId = @LegalBookId)
			BEGIN
				INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
				VALUES (@IdAccountSuperAvit, (SELECT Id FROM Common.ThirdParty WITH (NOLOCK) WHERE Nit = @NitCompany), NULL, 0, @Utility, 'Movimiento de la utilidad')
			END
			ELSE 
			BEGIN
				INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
				VALUES ((SELECT MainAccountId FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK) JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = ha.MainAccountId WHERE OfficialMainAccountId = @IdAccountSuperAvit AND ma.LegalBookId = @LegalBookId), (SELECT Id FROM Common.ThirdParty WITH (NOLOCK) WHERE Nit = @NitCompany), null, 0, @Utility, 'Movimiento de la utilidad')
			END			
		END
		ELSE IF @Utility < 0 
		BEGIN
			if(select count(*) from GeneralLedger.MainAccounts WITH (NOLOCK) where Id = @IdAccounttDeficit and LegalBookId = @LegalBookId) > 0 begin
				INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
				values(@IdAccounttDeficit, (select Id from Common.ThirdParty WITH (NOLOCK) where Nit = @NitCompany), null, abs(@Utility), 0, 'Movimiento del deficit')
			end
			else begin
				INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
				values((select MainAccountId from GeneralLedger.HomologationAccount ha WITH (NOLOCK) inner join GeneralLedger.MainAccounts ma WITH (NOLOCK) on ma.Id = ha.MainAccountId where OfficialMainAccountId = @IdAccounttDeficit and ma.LegalBookId = @LegalBookId), (select Id from Common.ThirdParty WITH (NOLOCK) where Nit = @NitCompany), null, abs(@Utility), 0, 'Movimiento del deficit')
			end			
		END
		
		IF EXISTS (SELECT 1 FROM @TableJournalVoucherDetail)
		BEGIN
			SET @JournalXml = (SELECT * FROM @TableJournalVoucher AS JournalVoucher CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail FOR XML AUTO, ELEMENTS)

			--Se consume el sp que guarda el movimiento contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalXml,@User 
			select @CodeMessage = rjv.code,
			@Message = rjv.MessageResult,
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
				
			IF ISNULL(@CodeMessage, '999') <> '0'
			BEGIN
				SELECT @CodeMessage CodeMessage, @Message Message, 0 AS IdJournalVoucher
				RETURN
			END

			SET @MessageReturn += ', ' + @Message
			delete from @resultJournalVoucher
		END

/******************************** COMPROBANTE DE SALDOS INICIALES ********************************/

		DELETE FROM @TableJournalVoucher
		DELETE FROM @TableJournalVoucherDetail

		--- Inserto la cabecera del comprobante contable
		INSERT @TableJournalVoucher (IdJournalVoucher, LegalBookId, VoucherDate, Imported, [Status], Detail, EntityName, IsClosedYear)
		VALUES(@IdVoucherTypeClose, @LegalBookId, CAST(CAST(@Year AS varchar(4)) + '-12-31' AS DATE), 0, 2, 'Saldos Iniciales ' + cast(@Year AS varchar(4)), 'JournalVouchers', 1)

		--- Inserto los detalles del comprobante contable 
		INSERT INTO @TableJournalVoucherDetail(IdMainAccount, IdThirdParty, IdCostCenter,DebitValue,CreditValue,Detail)
			SELECT data1.IdMainAccount, 
				data1.IdThirdParty, 
				data1.IdCostCenter, 
				IIF(SUM(data1.DebitValue) - SUM(data1.CreditValue) > 0, SUM(data1.DebitValue) - SUM(data1.CreditValue), 0) AS DebitValue, 
				IIF(SUM(data1.CreditValue) - SUM(data1.DebitValue) > 0, SUM(data1.CreditValue) - SUM(data1.DebitValue), 0) AS CreditValue,
				'Saldos Iniciales' + CAST(@Year AS varchar(4)) AS Detail
			FROM 
			(
				SELECT 
					gb.IdMainAccount, 
					gb.IdThirdParty, 
					gb.IdCostCenter, 
					IIF(SUM(gb.DebitValue) - SUM(gb.CreditValue) > 0, SUM(gb.DebitValue) - SUM(gb.CreditValue), 0) AS DebitValue, 
					IIF(SUM(gb.CreditValue) - SUM(gb.DebitValue) > 0, SUM(gb.CreditValue) - SUM(gb.DebitValue), 0) AS CreditValue
				FROM GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK)
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = gb.IdMainAccount
				JOIN GeneralLedger.MainAccountClasses mac WITH (NOLOCK) ON mac.Id = ma.IdAccountClass
				WHERE gb.[Year] = @Year and mac.[Type] IN (1, 3) AND ma.LegalBookId = @LegalBookId
				GROUP BY gb.IdMainAccount, gb.IdThirdParty, gb.IdCostCenter
				HAVING ((SUM(gb.DebitValue) - SUM(gb.CreditValue)) <> 0)

				UNION ALL 

				SELECT
					gb.IdMainAccount, 
					gb.IdThirdParty, 
					gb.IdCostCenter, 
					IIF(SUM(gb.DebitValue) - SUM(gb.CreditValue) > 0, SUM(gb.DebitValue) - SUM(gb.CreditValue), 0) AS DebitValue, 
					IIF(SUM(gb.CreditValue) - SUM(gb.DebitValue) > 0, SUM(gb.CreditValue) - SUM(gb.DebitValue), 0) AS CreditValue
				FROM GeneralLedger.GeneralLedgerBalance gb WITH (NOLOCK)
				JOIN GeneralLedger.MainAccounts ma WITH (NOLOCK) ON ma.Id = gb.IdMainAccount
				WHERE gb.[Year] = (@Year - 1) AND gb.[Month] = 14 AND ma.LegalBookId = @LegalBookId
				GROUP BY gb.IdMainAccount, gb.IdThirdParty, gb.IdCostCenter
				HAVING ((SUM(gb.DebitValue) - SUM(gb.CreditValue)) <> 0)
			) AS data1
			GROUP BY data1.IdMainAccount, data1.IdThirdParty, data1.IdCostCenter
			HAVING ((SUM(data1.DebitValue) - SUM(data1.CreditValue)) <> 0)
		
		IF EXISTS (SELECT 1 FROM @TableJournalVoucherDetail)
		BEGIN
			SET @JournalXml = (SELECT * FROM @TableJournalVoucher AS JournalVoucher CROSS APPLY @TableJournalVoucherDetail AS JournalVoucherDetail FOR XML AUTO, ELEMENTS)

			--Se consume el sp que guarda el movimiento contable
			insert @resultJournalVoucher exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalXml,@User 
			select @CodeMessage = rjv.code,
			@Message = rjv.MessageResult,
			@IdJournalVoucherResult = rjv.IdJournalVoucher
			from @resultJournalVoucher rjv
			
			IF ISNULL(@CodeMessage, '999') <> '0'
			BEGIN
				SELECT @CodeMessage CodeMessage, @Message Message, 0 AS IdJournalVoucher
				RETURN
			END

			SET @MessageReturn += ', ' + @Message
			delete from @resultJournalVoucher
		END
/******************************** ACTUALIZACION DEL AÑO DEL ULTIMO CIERRE DEL LIBRO ********************************/
		
		UPDATE GeneralLedger.LegalBook SET LastYearClose = @Year WHERE Id = @LegalBookId

		-- Asegurar la existencia de secuencias para los próximos dos años fiscales
		EXEC [GeneralLedger].[SP_EnsureFutureSequences] @Year;

		SELECT '0' AS CodeMessage, @MessageReturn AS Message, 0 IdJournalVoucher
		RETURN
	END TRY
	BEGIN CATCH
		SELECT '999' AS CodeMessage, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(5)) AS Message, 0 AS IdJournalVoucher
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta el cierre contable anual (cierre de vigencia fiscal) en el libro legal de contabilidad. Antes de proceder, valida una serie de condiciones obligatorias: que la vigencia no haya sido cerrada previamente, que todos los meses del año estén cerrados, y que no existan comprobantes contables, documentos de tesorería (recibos de caja, egresos, consignaciones, reembolsos), documentos de pagos (cuentas por pagar, anticipos, traslados) ni documentos de cartera pendientes de confirmar en el año a cerrar. Una vez superadas todas las validaciones, genera el asiento contable de cierre del ejercicio calculando la utilidad o déficit del período y registra el año como cerrado en el libro legal. Es el punto de control central que garantiza la integridad contable antes de iniciar una nueva vigencia fiscal.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_FiscalYearClose';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_FiscalYearClose';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Ejecuta el cierre contable anual de un libro legal: valida la integridad operativa del año, reclasifica cuentas con cierre por tercero, cancela cuentas de resultados contra superávit/déficit, genera saldos iniciales y actualiza el último año cerrado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El año a cerrar debe ser mayor a LastYearClose del libro legal indicado.; Todos los meses del año deben estar cerrados (ClosedMonth.Status<>1) para ese año.; No deben existir comprobantes contables sin confirmar (JournalVouchers.Status=1) en el año.; No deben existir documentos sin confirmar en Tesorería, Pagos, Cartera ni Inventario para el año.; Toda cuenta con CloseThirdParty=1 del libro debe tener IdThirdParty configurado.; Debe existir un registro en GeneralLedgerSettings para la unidad operativa (con IdCloseDocument, IdSuperavitAccount, IdDeficitAccount).; Debe existir un tercero en Common.ThirdParty con Nit igual al NIT de la compañía para registrar la utilidad/déficit.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los comprobantes generados llevan fecha 31 de diciembre del año cerrado.; El comprobante de reclasificación por terceros y el de cancelación de resultados se marcan IsClosedYear=2; el de saldos iniciales con IsClosedYear=1.; Los detalles de comprobantes solo se insertan cuando (Débito-Crédito) <> 0 (HAVING).; La reclasificación por terceros incluye saldos del año (@Year) y los del año anterior con Month=14 (saldo de cierre).; Los saldos iniciales contemplan únicamente cuentas de clase Type IN (1,3) (activos/pasivos-patrimonio) y excluyen cuentas de resultados (Type=2).; El procedimiento es transaccional vía TRY/CATCH: cualquier error o validación fallida retorna sin actualizar LastYearClose.; Solo se actualiza LastYearClose si los tres bloques de comprobantes se procesan sin errores.; Tras el cierre se invoca SP_EnsureFutureSequences para garantizar secuencias de los siguientes años.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.JournalVouchers: Cuando existen saldos en cuentas con CloseThirdParty=1 cuyo IdThirdParty difiere del configurado, se genera (vía SP_CreateAndValidateJournalVoucherMovement) un comprobante con fecha 31/12 del año, IsClosedYear=2, que cancela los saldos por tercero original y los reasigna al tercero de cierre de la cuenta.; [INSERT] GeneralLedger.JournalVouchers: Se genera un comprobante de cierre (IsClosedYear=2, fecha 31/12) que invierte los saldos de cuentas cuya MainAccountClasses.Type=2 (resultados) del año, registrando la utilidad (Crédito-Débito>0) en IdSuperavitAccount o el déficit (<0) en IdDeficitAccount, asociado al tercero de la compañía (Nit=@NitCompany).; [INSERT] GeneralLedger.JournalVouchers: Cuando @IdAccountSuperAvit/@IdAccounttDeficit no existe en MainAccounts del libro, se utiliza la cuenta homologada vía GeneralLedger.HomologationAccount (OfficialMainAccountId).; [INSERT] GeneralLedger.JournalVouchers: Se genera un comprobante de Saldos Iniciales (IsClosedYear=1, fecha 31/12 del año cerrado) con los saldos de cuentas de Type IN (1,3) del año, unidos con los saldos del año anterior con Month=14, agrupados por cuenta/tercero/centro de costo.; [UPDATE] GeneralLedger.LegalBook: Tras procesar exitosamente los comprobantes, se actualiza LastYearClose=@Year para el libro legal indicado.; [RETURN_RESULT] JournalVouchers: Devuelve CodeMessage=''0'' y mensaje acumulado al finalizar correctamente; ''999'' con detalle de la validación fallida o ERROR_MESSAGE+línea ante excepción; e IdJournalVoucher=0 en todos los casos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Year <= LegalBook.LastYearClose → Aborta retornando código 999 indicando que la vigencia ya fue cerrada.; si Existen registros en ClosedMonth con Status=1 para el año → Aborta listando los meses abiertos.; si Existen JournalVouchers con Status=1 en el año → Aborta listando consecutivo y tipo de comprobantes sin confirmar.; si Existen documentos en TreasuryControl/PaymentsControl/PortfolioControl/InventoryControlDocument con DocumentDate del año → Aborta listando los documentos sin confirmar de cada módulo.; si Existen MainAccounts con CloseThirdParty=1 e IdThirdParty NULL en el libro → Aborta listando las cuentas que requieren tercero de cierre.; si No existe GeneralLedgerSettings para la unidad operativa → Aborta indicando ausencia de parámetros contables.; si @Utility > 0 (ingresos>gastos+costos) → Acredita la utilidad en IdSuperavitAccount (o su homologación) else Si @Utility<0 debita el valor absoluto en IdAccounttDeficit (o su homologación); si =0 no inserta movimiento de utilidad/déficit.; si @IdAccountSuperAvit/@IdAccounttDeficit existe en MainAccounts del libro → Usa la cuenta directa else Resuelve la cuenta vía HomologationAccount.OfficialMainAccountId.; si Resultado de SP_CreateAndValidateJournalVoucherMovement con Code <> ''0'' → Aborta devolviendo el código y mensaje del SP hijo.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_CreateAndValidateJournalVoucherMovement; GeneralLedger.SP_EnsureFutureSequences', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_FiscalYearClose';
-- GO
