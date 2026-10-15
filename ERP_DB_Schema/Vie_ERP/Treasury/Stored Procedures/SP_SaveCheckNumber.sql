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