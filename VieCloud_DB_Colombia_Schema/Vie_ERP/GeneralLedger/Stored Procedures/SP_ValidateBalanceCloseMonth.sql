

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ValidateBalanceCloseMonth]
	@Period as varchar(2),
	@Year as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--- Estado del mensaje 1 - Correcto 2 - Advertencia 3 - Error 4 - Desbalanceado
	declare @TableResult table(CodeMessage varchar(10), Message varchar(max), [Status] tinyint,Consecutive bigint null,CodeNameJournalVoucherTye varchar(100) null, ConfirmationDate datetime null,Detail varchar(500) null,DebitValue numeric(18,2) null,CreditValue numeric(18,2) null)
    
	begin try
		declare @debitvalue as numeric(18,2)
		declare @creditValue as numeric(18,2)

		select @debitvalue = ROUND(sum(DebitValue),-1),
			   @creditValue = ROUND(sum(CreditValue),-1)
		from GeneralLedger.JournalVoucherDetails jvd 
		inner join GeneralLedger.JournalVouchers jv on jvd.IdAccounting = jv.id 
		inner join GeneralLedger.LegalBook lg on jv.legalBookId = lg.id
		where MONTH( jv.VoucherDate ) = @Period  and YEAR(jv.VoucherDate ) = @Year and lg.OfficialBook = 1 and jv.Status = 2

		
		if (@debitvalue <> @creditValue ) begin
			--si los detalles de los comprobantes estan desbalanceados
			insert into @TableResult (CodeMessage,Message,[Status],Consecutive,CodeNameJournalVoucherTye,ConfirmationDate,Detail,DebitValue,CreditValue)
			SELECT '999',
					'', 
					cast(4 as tinyint) as [Status], 
					jv.Consecutive, jvt.Code + ' - ' + jvt.Name as CodeNameJournalVoucherTye, 
					jv.ConfirmationDate,
					CAST(jv.Detail AS VARCHAR(500)), 
					ROUND(sum(DebitValue),-1) AS DebitValue, 
					ROUND(sum(CreditValue),-1) as CreditValue
			FROM GeneralLedger.JournalVouchers AS JV 
			INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id = JVD.IdAccounting 
			INNER JOIN GeneralLedger.JournalVoucherTypes jvt on jv.IdJournalVoucher = jvt.id 
			inner join GeneralLedger.LegalBook lg on jv.legalBookId = lg.id
			WHERE JV.ID IN (SELECT DISTINCT JV.ID FROM GeneralLedger.JournalVouchers AS JV INNER JOIN GeneralLedger.JournalVoucherDetails AS JVD ON JV.Id = JVD.IdAccounting 
			WHERE lg.OfficialBook = 1 and MONTH( jv.VoucherDate ) = @Period  and YEAR(jv.VoucherDate ) = @Year and jv.Status = 2)
			GROUP BY  jv.IdJournalVoucher, jv.Consecutive, jv.ConfirmationDate, jv.Detail,jvt.Code , jvt.Name

			select * from @TableResult 
			return 
		end
		--valido que los debitos y los creditos para el periodo seleccionado sean iguales
		select @debitvalue = ROUND(sum(DebitValue),-1),@creditValue = ROUND(sum(CreditValue),-1)
		from GeneralLedger.GeneralLedgerBalance 
		where [Month] =@Period  and [Year] =@Year

		if (@debitvalue <> @creditValue ) begin
			insert into @TableResult 
			select '999' as CodeMessage,'Los saldos para el periodo seleccionado estan desbalanceados' as Message, cast(4 as tinyint) as [Status]  ,null,null,null,null,null,null 
			select * from @TableResult 
			return		
		end

		--Proceso termino correctamente
		insert into @TableResult (CodeMessage,Message,[Status])		
		select '0' as  CodeMessage,'Proceso finalizado correctamente ' as Message ,cast(1 as tinyint) as [Status] 		
		select * from @TableResult 
		return 
	end try
	begin catch
	select '999' CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) Message, cast(3 as tinyint) as [Status]
	end catch
    
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que valida el balance contable (débitos vs. créditos) antes de cerrar un período mensual en el libro mayor oficial. Recibe como parámetros el mes (@Period) y el año (@Year), y verifica dos niveles: primero que la sumatoria de débitos y créditos de los comprobantes contables confirmados del libro oficial cuadre correctamente, y luego que los saldos consolidados del balance general para ese período también estén balanceados. Retorna una tabla de resultados con códigos de estado: 1=correcto, 2=advertencia, 3=error técnico, 4=desbalanceado; en caso de desbalance, detalla los comprobantes contables descuadrados con su consecutivo, tipo de comprobante, fecha de confirmación y los valores de débito y crédito individuales. Se usa en el proceso de cierre contable mensual para garantizar la integridad del libro mayor oficial antes de ejecutar el cierre.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateBalanceCloseMonth';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida que los débitos y créditos del libro oficial cuadren para un periodo (mes/año) antes del cierre contable mensual, detallando los comprobantes desbalanceados o reportando descuadre de saldos.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen comprobantes en GeneralLedger.JournalVouchers con Status = 2 (confirmados) para el mes/año indicado; El libro contable asociado debe estar marcado como OfficialBook = 1; Deben existir saldos en GeneralLedger.GeneralLedgerBalance para el mes y año consultados', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La validación solo considera comprobantes con Status=2 (confirmados); La validación solo considera libros marcados como OfficialBook=1; Las comparaciones de débito/crédito se realizan con redondeo a la decena (ROUND(...,-1)); Estados de mensaje: 1=Correcto, 2=Advertencia, 3=Error, 4=Desbalanceado; Ante desbalance se aborta el flujo (RETURN) sin ejecutar validaciones posteriores; El procedimiento no modifica datos persistentes; solo produce un resultset de diagnóstico', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre contable mensual; Libro mayor oficial; Comprobante contable (Journal Voucher); Débito y crédito; Saldos contables por periodo; Tipo de comprobante; Confirmación de comprobante; Desbalance contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableResult: Cuando la suma de débitos redondeada distinta de la suma de créditos redondeada en los detalles de comprobantes oficiales confirmados del periodo, retorna listado de comprobantes con Status=4 (Desbalanceado) y CodeMessage=''999''; [RETURN_RESULT] @TableResult: Cuando los débitos y créditos de GeneralLedgerBalance para el periodo no coinciden, retorna mensaje ''Los saldos para el periodo seleccionado estan desbalanceados'' con Status=4 y CodeMessage=''999''; [RETURN_RESULT] @TableResult: Cuando ambas validaciones pasan, retorna ''Proceso finalizado correctamente'' con Status=1 y CodeMessage=''0''; [RETURN_RESULT] (resultset): Si ocurre excepción en el TRY, retorna CodeMessage=''999'' con ERROR_MESSAGE + línea y Status=3 (Error)', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Suma de DebitValue <> suma de CreditValue en detalles de comprobantes confirmados (Status=2) del libro oficial (OfficialBook=1) para el mes/año → Inserta y retorna detalle de comprobantes desbalanceados con Status=4 y termina la ejecución else Continúa a validar saldos en GeneralLedgerBalance; si Suma de DebitValue <> suma de CreditValue en GeneralLedgerBalance para el mes/año → Retorna mensaje de saldos desbalanceados con Status=4 y termina else Retorna mensaje de proceso finalizado correctamente con Status=1', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVoucherDetails; GeneralLedger.JournalVouchers; GeneralLedger.LegalBook; GeneralLedger.JournalVoucherTypes; GeneralLedger.GeneralLedgerBalance', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateBalanceCloseMonth';
-- GO
