
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