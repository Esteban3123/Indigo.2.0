-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-11-01
-- Description:	Cambia el estado de un cheque
-- =============================================
CREATE PROCEDURE [Treasury].[SP_ChangeCheckCashingStatus]
	-- Add the parameters for the stored procedure here
	@Parameters as xml
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Opcion tinyint = 0
	DECLARE @Id as int

	DECLARE @TABLE AS TABLE
	(	IdVoucherTransaction	int
		,PreviousCheckStatus	tinyint
		,CurrentCheckStatus	tinyint
	)

	select 
	@Opcion = t.x.value('Opcion[1]','tinyint'),
	@Id = t.x.value('Id[1]','int')
	from @Parameters.nodes('/CCC') t(x)

	IF @Opcion IS NULL SET @Opcion = 0
	
	IF @Opcion = 0
	BEGIN
		begin try

			insert into @TABLE 
			select 
			t.x.value('IdVT[1]','int'),
			t.x.value('PCS[1]','tinyint'),
			t.x.value('CCS[1]','tinyint')
			from @Parameters.nodes('/CCC/CCCD') t(x)

			if Exists (select 1
			from @TABLE cd 
			inner join Treasury.VoucherTransaction vd With(Nolock) on vd.Id = cd.IdVoucherTransaction 
			where vd.IdCheckCashingStatus <> cd.PreviousCheckStatus) begin
			declare @StringContract varchar(max)
			set @StringContract = (select vd.Code + ', ' from @TABLE cd 
			inner join Treasury.VoucherTransaction vd With(Nolock) on vd.Id = cd.IdVoucherTransaction 
			where vd.IdCheckCashingStatus <> cd.PreviousCheckStatus for XML PATH(''))
			select '999' as CodeMessage, 'Los siguientes comprobantes de egreso tienen otro estado de cheque: ' + @StringContract as Message, cast(3 as tinyint) as [Status]
			return
			end
			
			update vd set IdCheckCashingStatus = cd.CurrentCheckStatus
			from @TABLE cd 
			inner join Treasury.VoucherTransaction vd With(Nolock) on vd.Id = cd.IdVoucherTransaction 
			where vd.IdCheckCashingStatus = cd.PreviousCheckStatus

			select '0' as CodeMessage, 'Se actualizó correctamente el estado de los cheques' as Message, cast(1 as tinyint) as [Status]
		end try
		begin catch
			select '0' as CodeMessage, ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) as Message, cast(3 as tinyint) as [Status]
		end catch
	END

	IF @Opcion = 1
	BEGIN

		SELECT 
		Banco = B.Name 
		,CuentaBancaria = EBA.Number
		,NroCheque = CCCD.CheckNumber
		,CompEgreso = VT.Code
		,IdentificacionTercero = TP.Nit
		,Tercero = tp.Name
		,FechaGirado = vt.ConfirmationDate
		,Valor = vt.Value
		,Estado = CCS.Description
		,SaldoCuenta = CCC.BalanceAccount
		,SobreGiroAutorizado = EBA.Quota
		,FechaCorte = CCC.DueDate
		FROM
		Treasury.CheckCashingControl CCC
		INNER JOIN Treasury.EntityBankAccounts EBA
		ON EBA.Id = CCC.IdEntityAccount
		INNER JOIN Payroll.Bank B
		ON B.ID = EBA.IdBank
		INNER JOIN Treasury.CheckCashingControlDetail CCCD
		ON CCCD.IdCheckCashingControl = CCC.Id
		INNER JOIN Treasury.VoucherTransaction VT
		ON VT.Id = CCCD.IdVoucherTransaction
		INNER JOIN Common.ThirdParty TP
		ON TP.Id = VT.IdThirdParty
		INNER JOIN Treasury.CheckCashingStatus CCS
		ON CCS.Id = VT.IdCheckCashingStatus
		WHERE CCC.Id = @Id
	END
END