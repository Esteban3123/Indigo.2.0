-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveCheckNumber]
	@OperatingUnitId int,
	@EntityBankAccountId int,
	@CheckNumber as bigint,
	@UserCode as varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	begin try
	
		declare @CheckbookControl bit
		declare @CheckNumberEnd bigint
		declare @CheckbookId int
		declare @CurrentCheckNumber bigint

		select @CheckbookControl=coalesce(CheckBookControl, -1) from Treasury.SettingsTreasury where IdOperatingUnit = @OperatingUnitId
		if @CheckbookControl is null Or @CheckbookControl = -1 begin
			select '999' as StatusResult, 'No se encontraron parámetros de tesorería para la unidad operativa seleccionada' as MessageResult, null as CheckNumberToAssing
			return
		end

		if @CheckbookControl = 1 begin
			--selecciono la chequera activa
			select @CheckNumberEnd=EndNumber,@CheckbookId = Id, @CurrentCheckNumber=CurrentNumber from Treasury.Checkbooks where IdEntityBanckAccount = @EntityBankAccountId And Status = 1
			if @CheckNumberEnd < @CheckNumber begin
				select '999' as StatusResult, 'El cheque a asignar es mayor al último de la chequera' as MessageResult, null as CheckNumberToAssing
				return
			end
			--si el número de cheque es cero entonces es porque se está generando desde dispersión de fondos y debe asignarse
			if @CheckNumber = 0 begin
				--se busca el número de cheque a asignar
				
				declare @CheckToAssign bigint = @CurrentCheckNumber
				while @CheckToAssign <= @CheckNumberEnd begin
					--buscamos que el cheque a asignar no este ni bloqueado ni anulado
					if (select coalesce(count(1), 0) from Treasury.CheckBlock where IdCheckbook = @CheckbookId And CheckNumber = @CheckToAssign) = 0 
					--And (select coalesce(count(*), 0) from Treasury.CancellationChecks where IdCheckBook = @CheckbookId And CheckNumber = @CheckToAssign) = 0 
					And (select coalesce(count(1), 0) from Treasury.CheckCashingControlDetail where IdCheckBook = @CheckbookId And CheckNumber = @CheckToAssign and CurrentCheckStatus = cast(5 as tinyint)) = 0 
					begin
						--buscamos si el cheque en la tabla de pendientes y lo eliminamos
						delete Treasury.OutstandingChecks where IdCheckBook = @CheckbookId And CheckNumber = @CheckToAssign
						--además bloqueamos el cheque para que otro comprobante no lo utilice
						insert into Treasury.CheckBlock (IdCheckbook,CheckNumber,CodUser) values(@CheckbookId,@CheckToAssign,@UserCode)
						break;
					end
					set @CheckToAssign += 1
				end

				--verificamos que no se halla pasado del # final de la chequera
				if @CheckToAssign = 0 Or @CheckToAssign > @CheckNumberEnd begin
					select '999' as StatusResult, 'No se encontró un consecutivo de cheque disponible para asignar' as MessageResult, null as CheckNumberToAssing
					return
				end

				/*******************************************ESTE CÓDIGO ESTA REPETIDO MAS ABAJO POR TANTO SI SE MODIFICA DEBE MODIFICAR EL OTRO********************************************/
				if @CheckToAssign = @CheckNumberEnd begin
					--actualizamos el estado de la chequera si el cheque a utilizar es el último
					if (select coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckBook = @CheckbookId) = 0 And (select coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckBook = @CheckbookId) = 0
						update Treasury.Checkbooks set Status = 3, CurrentNumber = @CheckToAssign where Id = @CheckbookId
					else 
						update Treasury.Checkbooks set CurrentNumber = @CheckToAssign where Id = @CheckbookId
				end else begin
					--solo si el cheque que tenemos asignado es mayor o igual al actual de la chequera buscamos aumentar ese consecutivo, sino no
					if @CheckToAssign >= @CurrentCheckNumber begin
						--aumentamos el # de cheque actual que va en la chequera
						declare @newCurrentCheck1 as bigint = @CurrentCheckNumber
						declare @countCheckBlock1 as int = 0
						declare @countCheckCancellation1 as int = 0
						declare @countCheckOutstanding1 as int = 0
						--buscamos el nuevo # de cheque actual
						while @newCurrentCheck1 <= @CheckNumberEnd begin							
							select @countCheckBlock1=coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckbook = @CheckbookId And CheckNumber = @newCurrentCheck1
							--select @countCheckCancellation1=coalesce(count(*), 0) from Treasury.CancellationChecks where IdCheckbook = @CheckbookId And CheckNumber = cast(@newCurrentCheck1 as varchar)
							select @countCheckCancellation1=coalesce(count(1), 0) from Treasury.CheckCashingControlDetail where IdCheckBook = @CheckbookId And CheckNumber = cast(@newCurrentCheck1 as varchar) and CurrentCheckStatus = cast(5 as tinyint)
							select @countCheckOutstanding1=coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckbook = @CheckbookId And CheckNumber = @newCurrentCheck1
							
							if @countCheckCancellation1 > 0 begin
								select '999' as StatusResult, 'El cheque ' + cast(@newCurrentCheck1 as varchar) + 'se encuentra anulado pero el consecutivo actual de la chequera es menor' as MessageResult, null as CheckNumberToAssing
								return
							end
							if @countCheckBlock1 = 0 And @countCheckOutstanding1 = 0
								break;
							set @newCurrentCheck1 += 1
						end
						--si el nuevo # actual es mayor al # final entonces es porque ya todos los cheques están en uso por lo cual se finaliza la chequera y se establece el actual en el # final
						if @newCurrentCheck1 > @CheckNumberEnd
							set @newCurrentCheck1 = @CheckNumberEnd

						if @newCurrentCheck1 = @CheckNumberEnd
							update Treasury.Checkbooks set CurrentNumber = @newCurrentCheck1, Status = 3 where Id = @CheckbookId --finalizamos la chequera
						else if  @newCurrentCheck1 < @CheckNumberEnd
							update Treasury.Checkbooks set CurrentNumber = @newCurrentCheck1 where Id = @CheckbookId
					end else begin
						--si la chequera esta en el ultimo consecutivo y ademas no hay cheques pendientes y no hay cheques bloqueados entonces finalizamos la chequera
						if (select coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckBook = @CheckbookId) = 0 And (select coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckBook = @CheckbookId) = 0
							And @CurrentCheckNumber = @CheckNumberEnd begin
							update Treasury.Checkbooks set Status = 3 where Id = @CheckbookId --finalizamos la chequera
						end
					end
				end
				/**********************************************************************************************************************************************************************************/

				select '000' as StatusResult, '' as MessageResult, @CheckToAssign as CheckNumberToAssing
				return
			end
			else begin
				--quiere decir que se genera desde comprobantes de egreso
				
				--if (select coalesce(count(*), 0) from Treasury.CancellationChecks where IdEntityAccount = @EntityBankAccountId And IdCheckBook = @CheckbookId And CheckNumber = cast(@CheckNumber as varchar)) <> 0 
				if (select coalesce(count(1), 0) from Treasury.CheckCashingControl CCC inner join Treasury.CheckCashingControlDetail CCCD ON CCCD.IdCheckCashingControl = CCC.Id
				where CCC.IdEntityAccount = @EntityBankAccountId And CCCD.IdCheckBook = @CheckbookId And CCCD.CheckNumber = cast(@CheckNumber as varchar) and CurrentCheckStatus = cast(5 as tinyint)) <> 0
				begin
					select '999' as StatusResult, 'El cheque ' + cast(@CheckNumber as varchar) + 'se encuentra anulado, seleccione otro' as MessageResult, null as CheckNumberToAssing
					return
				end
				delete Treasury.OutstandingChecks where IdCheckBook = @CheckbookId And CheckNumber = @CheckNumber
				/*if (select coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckBook = @CheckbookId And CheckNumber = @CheckNumber) > 0 begin
					select '999' as StatusResult, 'El cheque ' + cast(@CheckNumber as varchar) + 'se encuentra en espera. Contacte con el administrador del sistema para corregir este error' as MessageResult
					return
				end*/
				--bloqueamos el cheque que se va a utilizar si antes no esta bloqueado
				if (select coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckbook = @CheckbookId And CheckNumber = @CheckNumber) = 0 begin
					insert into Treasury.CheckBlock (IdCheckbook,CheckNumber,CodUser) values(@CheckbookId,@CheckNumber,@UserCode)
				end

				/*******************************************ESTE CÓDIGO ESTA REPETIDO MAS ARRIBA POR TANTO SI SE MODIFICA DEBE MODIFICAR EL OTRO********************************************/

				if @CheckNumber = @CheckNumberEnd begin
					--actualizamos el estado de la chequera si el cheque a utilizar es el último
					if (select coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckBook = @CheckbookId) = 0 And (select coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckBook = @CheckbookId) = 0
						update Treasury.Checkbooks set Status = 3, CurrentNumber = @CheckNumber where Id = @CheckbookId
					else 
						update Treasury.Checkbooks set CurrentNumber = @CheckNumber where Id = @CheckbookId
				end else begin
					--solo si el cheque que tenemos asignado es mayor o igual al actual de la chequera buscamos aumentar ese consecutivo, sino no
					if @CheckNumber >= @CurrentCheckNumber begin
						--aumentamos el # de cheque actual que va en la chequera
						declare @newCurrentCheck as bigint = @CurrentCheckNumber
						declare @countCheckBlock as int = 0
						declare @countCheckCancellation as int = 0
						declare @countCheckOutstanding as int = 0
						--buscamos el nuevo # de cheque actual
						while @newCurrentCheck <= @CheckNumberEnd begin							
							select @countCheckBlock=coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckbook = @CheckbookId And CheckNumber = @newCurrentCheck
							--select @countCheckCancellation=coalesce(count(*), 0) from Treasury.CancellationChecks where IdCheckbook = @CheckbookId And CheckNumber = cast(@newCurrentCheck as varchar)
							select @countCheckCancellation=coalesce(count(1), 0) from Treasury.CheckCashingControlDetail where IdCheckbook = @CheckbookId And CheckNumber = cast(@newCurrentCheck as varchar)
							and CurrentCheckStatus = cast(5 as tinyint)
							select @countCheckOutstanding=coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckbook = @CheckbookId And CheckNumber = @newCurrentCheck
							
							if @countCheckCancellation > 0 begin
								select '999' as StatusResult, 'El cheque ' + cast(@newCurrentCheck as varchar) + 'se encuentra anulado pero el consecutivo actual de la chequera es menor' as MessageResult, null as CheckNumberToAssing
								return
							end
							if @countCheckBlock = 0 And @countCheckOutstanding = 0
								break;
							set @newCurrentCheck += 1
						end
						--si el nuevo # actual es mayor al # final entonces es porque ya todos los cheques están en uso por lo cual se finaliza la chequera y se establece el actual en el # final
						if @newCurrentCheck > @CheckNumberEnd
							set @newCurrentCheck = @CheckNumberEnd

						if @newCurrentCheck = @CheckNumberEnd
							update Treasury.Checkbooks set CurrentNumber = @newCurrentCheck, Status = 3 where Id = @CheckbookId --finalizamos la chequera
						else if  @newCurrentCheck < @CheckNumberEnd
							update Treasury.Checkbooks set CurrentNumber = @newCurrentCheck where Id = @CheckbookId
					end else begin
						--si la chequera esta en el ultimo consecutivo y ademas no hay cheques pendientes y no hay cheques bloqueados entonces finalizamos la chequera
						if (select coalesce(count(*), 0) from Treasury.OutstandingChecks where IdCheckBook = @CheckbookId) = 0 And (select coalesce(count(*), 0) from Treasury.CheckBlock where IdCheckBook = @CheckbookId) = 0
							And @CurrentCheckNumber = @CheckNumberEnd begin
							update Treasury.Checkbooks set Status = 3 where Id = @CheckbookId --finalizamos la chequera
						end
					end
				end

				/**********************************************************************************************************************************************************************************/

				select '000' as StatusResult, '' as MessageResult, null as CheckNumberToAssing
				return
			end

		end else begin
			select '000' as StatusResult, '' as MessageResult, null as CheckNumberToAssing
		end

	end try
	begin catch
		select '999' as StatusResult, ERROR_MESSAGE() as MessageResult, null as CheckNumberToAssing
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de tesorería que asigna y valida el número de cheque a utilizar en un comprobante de pago, para una cuenta bancaria y unidad operativa específicas. Si el número de cheque enviado es cero (generado desde dispersión de fondos), busca automáticamente el siguiente consecutivo disponible en la chequera activa, descartando cheques bloqueados, anulados o ya procesados. Una vez encontrado el cheque disponible, lo reserva registrándolo en el control de cheques bloqueados (CheckBlock), elimina su pendiente de cobro si existía (OutstandingChecks), y actualiza el estado y consecutivo actual de la chequera (Checkbooks), cerrándola si se trata del último cheque. Si el número de cheque es enviado directamente, valida que no supere el límite de la chequera y que el control de chequeras esté habilitado en la configuración de tesorería (SettingsTreasury) para la unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCheckNumber';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCheckNumber';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reserva o asigna un número de cheque dentro de la chequera activa de una cuenta bancaria, validando bloqueos y anulaciones, actualizando el consecutivo y finalizando la chequera cuando corresponde.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Treasury.SettingsTreasury para la unidad operativa con CheckBookControl no nulo; Debe existir una chequera activa (Status=1) asociada a la cuenta bancaria cuando CheckBookControl=1; El número de cheque solicitado (si > 0) no debe exceder EndNumber de la chequera activa; El cheque solicitado no debe figurar como anulado en CheckCashingControlDetail (CurrentCheckStatus=5)', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo opera sobre la chequera activa de la cuenta bancaria (Checkbooks.Status = 1); Un cheque se considera ''anulado'' cuando existe en CheckCashingControlDetail con CurrentCheckStatus = 5; La chequera se marca como finalizada con Status = 3 cuando se consume el último número o ya no quedan cheques disponibles ni pendientes; Antes de asignar un cheque, este se elimina de OutstandingChecks y se inserta en CheckBlock para reservarlo; Nunca asigna un número de cheque mayor a EndNumber de la chequera; Nunca duplica el bloqueo: solo inserta en CheckBlock si no existe previamente para ese (chequera, número); CurrentNumber nunca retrocede salvo cuando ya está al final de la chequera; Si CheckBookControl no está activo en SettingsTreasury, no se realiza ninguna asignación ni mutación', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'chequera; cheque; bloqueo de cheque; anulación de cheque; cheques pendientes (outstanding); dispersión de fondos; comprobante de egreso; control de cobro de cheques; consecutivo de cheque; unidad operativa; tesorería', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Treasury.SettingsTreasury.CheckBookControl es NULL o no existe registro para la unidad operativa → Devuelve StatusResult=''999'' con mensaje ''No se encontraron parámetros de tesorería...'' y aborta; si CheckBookControl <> 1 (control de chequera desactivado) → Devuelve StatusResult=''000'' sin asignar número y sin tocar chequeras; si CheckBookControl = 1 y EndNumber de la chequera activa < @CheckNumber → Devuelve ''999'' con mensaje ''El cheque a asignar es mayor al último de la chequera''; si @CheckNumber = 0 (flujo desde dispersión de fondos) → Itera desde CurrentNumber hasta EndNumber buscando un cheque libre (no bloqueado y no anulado/estado 5), lo elimina de OutstandingChecks y lo inserta en CheckBlock else Flujo desde comprobantes de egreso: valida que el cheque no esté anulado en CheckCashingControlDetail (CurrentCheckStatus=5); si no lo está, elimina de OutstandingChecks y lo bloquea en CheckBlock si no estaba; si Tras la búsqueda iterativa, @CheckToAssign = 0 o > EndNumber → Devuelve ''999'' con ''No se encontró un consecutivo de cheque disponible para asignar''; si El cheque asignado/recibido = EndNumber de la chequera Y no quedan registros en OutstandingChecks ni CheckBlock para esa chequera → UPDATE Checkbooks SET Status=3, CurrentNumber=<cheque> (finaliza la chequera) else UPDATE Checkbooks SET CurrentNumber=<cheque> (solo avanza consecutivo); si El cheque asignado >= CurrentNumber y no es el último → Recalcula el nuevo CurrentNumber avanzando hasta el primer cheque que no esté ni bloqueado ni outstanding; si encuentra uno anulado (CheckCashingControlDetail.CurrentCheckStatus=5) antes, retorna ''999'' indicando inconsistencia de consecutivo; si Nuevo CurrentNumber calculado = EndNumber → UPDATE Checkbooks SET CurrentNumber, Status=3 (finaliza chequera) else UPDATE Checkbooks SET CurrentNumber; si El cheque asignado < CurrentNumber, CurrentNumber = EndNumber y no hay outstanding ni bloqueados → UPDATE Checkbooks SET Status=3 (finaliza la chequera); si En flujo de egreso, el cheque ya está anulado en CheckCashingControlDetail (CurrentCheckStatus=5) ligado a CheckCashingControl de la cuenta y chequera → Devuelve ''999'' con ''El cheque X se encuentra anulado, seleccione otro''', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.SettingsTreasury; Treasury.Checkbooks; Treasury.CheckBlock; Treasury.CheckCashingControlDetail; Treasury.CheckCashingControl; Treasury.OutstandingChecks', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCheckNumber';
-- GO
