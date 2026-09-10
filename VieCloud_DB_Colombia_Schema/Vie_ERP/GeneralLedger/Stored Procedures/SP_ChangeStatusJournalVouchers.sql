
CREATE PROCEDURE [GeneralLedger].[SP_ChangeStatusJournalVouchers]
	
	@StatusJournalVouchers as int,
	@IdDocuments as varchar(max),
	@Month as int
	as
	begin	
		CREATE TABLE #TableIdDocuments(
		Id integer,
		Data integer	
		)

		CREATE TABLE #JOURNALVOUCHERS
		(
			Position integer identity (1,1),
			Id integer,	
			Consecutive integer,
			Status  integer,
			CreditValue Decimal(18,2),
			DebitValue Decimal (18,2),
			IdAccounting integer,
			IdCostCenter integer,
			IdMainAccount integer,			
			IdThirdParty integer		
		)
	BEGIN TRY
		BEGIN TRANSACTION UpdteJV;
		
			insert into #TableIdDocuments select Id,Data from dbo.Split(@IdDocuments ,',')	
			declare @count as integer = (select count(*) from #TableIdDocuments)
			declare @contador as integer = 0
			declare @idJournalVouchers as int;				
			WHILE @contador <> @count 
			BEGIN			
				set @contador = @contador + 1						
				set @idJournalVouchers = (select #TableIdDocuments.Data from #TableIdDocuments where Id = @contador)
				--consulto la cabecera y el detalle de los comprobantes contables					
				insert into #JOURNALVOUCHERS select JV.Id,JV.Consecutive,JV.Status,JVD.CreditValue,JVD.DebitValue,JVD.IdAccounting,JVD.IdCostCenter,JVD.IdMainAccount,JVD.IdThirdParty      
						   from GeneralLedger.JournalVouchers as JV join GeneralLedger.JournalVoucherDetails as JVD on JV.Id = JVD.IdAccounting
						   where JV.Id = @idJournalVouchers	and MONTH(JV.VoucherDate) = @Month  				  							  
			END	
			--select * from #JOURNALVOUCHERS 	
			declare @CountJournalVouchers as integer = (select COUNT (*) from #JOURNALVOUCHERS )
			declare @CountTempJournalVouchers as integer = 0;
			--itero la tabla donde se guardaron los resultados de la tabla de journal vouchers
					
			while @CountJournalVouchers <> @CountTempJournalVouchers 
				begin
				Declare @CreditValue as decimal (18,2);
				Declare @DebitValue as decimal (18,2);
				Declare @IdCostCenter as int = NULL;
				Declare @IdMainAccount as int;
				Declare @IdThidParty as int = NULL;
				Declare @CreditBalance as decimal (18,2);
				declare @DebitBalance as decimal (18,2);
				declare @temCredit as decimal (18,2);
				declare @temDebit as decimal (18,2);	
				DECLARE @IdBalance AS INT;				
				set @CountTempJournalVouchers = @CountTempJournalVouchers + 1 
				select @idJournalVouchers = Id , @CreditValue = CreditValue ,@DebitValue = DebitValue,@IdCostCenter = IdCostCenter ,@IdMainAccount = IdMainAccount ,@IdThidParty = IdThirdParty  
				from #JOURNALVOUCHERS where Position = @CountTempJournalVouchers 				
				
				set @IdBalance = NULL
				--print 'contador ' + cast( @CountTempJournalVouchers as varchar(5)) + 'cuenta' + cast (@IdMainAccount  as nvarchar (8)) + ' centro de costo' + cast (@IdCostCenter  as nvarchar (8)) + 'tercero ' + cast (@IdThidParty  as nvarchar (8))								
				select @IdBalance = JLB.Id , @CreditBalance = JLB.CreditValue , @DebitBalance = JLB.DebitValue from  GeneralLedger.GeneralLedgerBalance as JLB
						 where JLB.IdMainAccount = @IdMainAccount and ISNULL(JLB.IdThirdParty,0)  = ISNULL ( @IdThidParty,0)  and ISNULL ( JLB.IdCostCenter,0) = ISNULL ( @IdCostCenter,0) and JLB.Month = @Month   								
				if @IdBalance IS NULL
				BEGIN					    
						INSERT INTO [GeneralLedger].[GeneralLedgerBalance]
							 ([Month]
							 ,[IdMainAccount]
							 ,[IdThirdParty]
							 ,[IdCostCenter]
							 ,[DebitValue]
							 ,[CreditValue])
						VALUES
							 (@Month ,
							 @IdMainAccount,
							 @IdThidParty,
							 @IdCostCenter,
							 @DebitValue,
							 @CreditValue)							 
				END
				ELSE
				BEGIN					
				 set @temCredit =  @CreditBalance + @CreditValue				 
				 set @temDebit = @DebitBalance + @DebitValue 					
					update GeneralLedger.GeneralLedgerBalance 
					set CreditValue = @temCredit,
						DebitValue = @temDebit 
						where IdMainAccount =  @IdMainAccount and ISNULL ( IdThirdParty,0) =ISNULL ( @IdThidParty,0) and ISNULL ( IdCostCenter,0) = ISNULL ( @IdCostCenter,0)
				END
				update GeneralLedger .JournalVouchers 
				set Status  = 2
				where Id = @idJournalVouchers 									
			end
		
		COMMIT transaction  UpdateJV ;		
			select * from  #TableIdDocuments 
	END TRY	
	BEGIN CATCH
      ROLLBACK	TRANSACTION UpdateJV ;
	  SELECT  'Se ha producido un error!',  ERROR_MESSAGE()	as mensajeError, ERROR_NUMBER() NumeroError 
	END CATCH
	select count(*) from #TableIdDocuments
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento contable que cambia el estado de uno o varios comprobantes contables (journal vouchers) a ''contabilizado'' (estado 2) y actualiza el saldo del libro mayor general. Recibe una lista de identificadores de comprobantes separados por comas, los separa usando la función Split, y por cada línea de detalle (débito/crédito, cuenta contable, centro de costo, tercero) del mes indicado, inserta o acumula el movimiento en la tabla de saldos del libro mayor (GeneralLedgerBalance). Existe para garantizar que al aprobar o contabilizar comprobantes, los saldos contables por cuenta, centro de costo y tercero queden actualizados de forma transaccional y consistente.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ChangeStatusJournalVouchers';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Aplica los movimientos débito/crédito de comprobantes contables seleccionados al saldo del libro mayor del mes indicado y marca dichos comprobantes como contabilizados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de identificadores de comprobantes debe venir como cadena separada por comas y ser parseable por dbo.Split.; Los comprobantes deben existir en GeneralLedger.JournalVouchers con detalle en JournalVoucherDetails.; La fecha del comprobante (VoucherDate) debe corresponder al mes recibido; en caso contrario el comprobante se ignora.; El mes parametrizado debe coincidir con el campo Month de GeneralLedgerBalance para reutilizar el saldo existente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las operaciones se ejecutan dentro de una transacción explícita; ante cualquier error se revierten todos los cambios.; La igualdad de claves de saldo trata NULL como 0 tanto en IdThirdParty como en IdCostCenter (ISNULL(col,0) = ISNULL(@var,0)).; El estado final de todo comprobante procesado con detalle del mes es 2.; Los saldos se mantienen particionados por Mes + Cuenta principal + Tercero + Centro de costo.; El parámetro @StatusJournalVouchers se recibe pero no se utiliza: el estado siempre se fija a 2.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable (Journal Voucher); Detalle de comprobante contable; Libro mayor / Saldo contable; Cuenta principal; Tercero; Centro de costo; Débito y crédito; Período contable (mes); Estado de comprobante', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] GeneralLedger.GeneralLedgerBalance: Si no existe un saldo para la combinación (IdMainAccount, IdThirdParty, IdCostCenter, Month) — tratando NULL como 0 en tercero y centro de costo —, se crea un nuevo registro con los valores débito y crédito del detalle del comprobante.; [UPDATE] GeneralLedger.GeneralLedgerBalance: Si ya existe saldo para esa combinación cuenta/tercero/centro de costo en el mes, se acumulan los valores: CreditValue += CreditValue del detalle, DebitValue += DebitValue del detalle.; [UPDATE] GeneralLedger.JournalVouchers: Por cada línea de detalle procesada se fija Status = 2 al comprobante (estado contabilizado/aplicado al saldo).; [RETURN_RESULT] : Al finalizar correctamente devuelve el contenido de la tabla temporal de IDs procesados y el conteo de los mismos.; [RETURN_RESULT] : En caso de error se hace ROLLBACK y se retorna un resultset con mensaje y número de error de ERROR_MESSAGE()/ERROR_NUMBER().', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH(JV.VoucherDate) = @Month → Se incluye el comprobante y su detalle en el procesamiento de saldos. else El comprobante no se carga al staging y no afecta saldos ni cambia su estado.; si No existe registro en GeneralLedgerBalance para (IdMainAccount, IdThirdParty, IdCostCenter, Month) tratando NULL como 0 → INSERT de un nuevo saldo con los valores del detalle. else UPDATE acumulando débito y crédito sobre el saldo existente.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.JournalVouchers; GeneralLedger.JournalVoucherDetails; GeneralLedger.GeneralLedgerBalance; dbo.Split', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ChangeStatusJournalVouchers';
-- GO
