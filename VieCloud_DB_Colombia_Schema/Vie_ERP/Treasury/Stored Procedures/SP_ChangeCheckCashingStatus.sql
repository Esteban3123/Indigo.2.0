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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de tesorería que gestiona el cambio de estado de cheques girados (por ejemplo: pendiente, cobrado, anulado, rechazado) en los comprobantes de egreso. Tiene dos modos de operación: el primero (Opción 0) actualiza masivamente el estado de cobro de uno o varios cheques en la tabla de transacciones contables, validando previamente que el estado anterior declarado coincida con el real para evitar inconsistencias; si hay discrepancias, informa los comprobantes conflictivos sin realizar cambios. El segundo modo (Opción 1) consulta el detalle de un control de canje de cheques específico, retornando información consolidada del banco, número de cuenta, número de cheque, comprobante de egreso, identificación y nombre del tercero beneficiario, fecha de giro, valor, estado actual del cheque, saldo de la cuenta y fecha de corte. Es utilizado en el módulo de tesorería para trazabilidad y control del ciclo de vida de los cheques emitidos a proveedores, contratistas u otros terceros.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeCheckCashingStatus';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeCheckCashingStatus';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Permite cambiar masivamente el estado de canje de cheques validando consistencia con el estado previo, o consultar el detalle consolidado de un control de canje de cheques.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodo /CCC con Opcion e Id; para Opcion=0 además debe traer nodos /CCC/CCCD con IdVT, PCS y CCS.; Los IdVoucherTransaction provistos deben existir en Treasury.VoucherTransaction.; Para Opcion=1, el Id debe corresponder a un Treasury.CheckCashingControl existente.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca actualiza el estado de un cheque si el estado actual no coincide con el PreviousCheckStatus declarado por el cliente (control optimista de concurrencia).; Cuando se detecta al menos una discrepancia de estado, ningún registro es actualizado (operación todo-o-nada implícita).; El Status devuelto sigue la convención: 1=éxito, 3=error/validación.; Las consultas de validación y lectura usan WITH (NOLOCK) sobre VoucherTransaction.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cheque; Estado de canje de cheque; Comprobante de egreso; Control de canje de cheques; Cuenta bancaria de la entidad; Banco; Tercero beneficiario; Sobregiro autorizado (Quota); Saldo de cuenta; Fecha de corte; Fecha de giro', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Treasury.VoucherTransaction: Cuando @Opcion=0 y vd.IdCheckCashingStatus = cd.PreviousCheckStatus, se actualiza IdCheckCashingStatus al CurrentCheckStatus enviado.; [RETURN_RESULT] Treasury.VoucherTransaction: Si algún comprobante tiene IdCheckCashingStatus distinto al PreviousCheckStatus enviado, retorna CodeMessage=''999'', Status=3 listando los códigos de comprobantes conflictivos y NO ejecuta el UPDATE.; [RETURN_RESULT] Treasury.VoucherTransaction: Si la actualización es exitosa, retorna CodeMessage=''0'', Status=1 con mensaje ''Se actualizó correctamente el estado de los cheques''.; [RETURN_RESULT] Treasury.VoucherTransaction: Ante excepción en el bloque try, retorna CodeMessage=''0'', Status=3 con ERROR_MESSAGE() y ERROR_LINE().; [RETURN_RESULT] Treasury.CheckCashingControl: Cuando @Opcion=1, retorna información consolidada (banco, cuenta, número de cheque, comprobante, tercero, fecha giro, valor, estado, saldo y fecha de corte) del control de canje filtrado por CCC.Id = @Id.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Opcion IS NULL → Se asigna @Opcion = 0 (modo actualización por defecto).; si @Opcion = 0 → Valida coherencia de estado previo y actualiza IdCheckCashingStatus en VoucherTransaction.; si @Opcion = 0 y existe alguna fila con vd.IdCheckCashingStatus <> cd.PreviousCheckStatus → Aborta sin actualizar y retorna lista de comprobantes con estado divergente (Status=3, CodeMessage=''999''). else Ejecuta el UPDATE masivo de estados.; si @Opcion = 1 → Devuelve consulta de detalle del control de canje de cheques identificado por @Id.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.VoucherTransaction; Treasury.CheckCashingControl; Treasury.CheckCashingControlDetail; Treasury.EntityBankAccounts; Treasury.CheckCashingStatus; Payroll.Bank; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeCheckCashingStatus';
-- GO
