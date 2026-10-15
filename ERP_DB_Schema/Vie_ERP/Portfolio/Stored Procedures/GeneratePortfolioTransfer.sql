
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Genera Cuenta por cobrar
-- =============================================

CREATE Procedure [Portfolio].[GeneratePortfolioTransfer]
	@ListPortfolioAdvanceCrossingXml Xml,
	@OperativeUnitId Int,
	@UserCode Varchar(20),
	@AccountReceivableId Int,
	@CompanyType Tinyint, --Se saca de la auditoria 1 - Privada, 2 - Publica
	
	@StatusResult Bit Output,
	@Message Varchar(Max) Output,
	@ObjectEmbbeded Varchar(Max) Output
AS
Begin
	Set Nocount On;
	Begin Try

		Declare @AccountReceivableAccountingMainAccountId Int,
			@AccountReceivableAccountingCostCenterId Int

		Select @AccountReceivableAccountingMainAccountId = ara.MainAccountId,
			@AccountReceivableAccountingCostCenterId = ara.CostCenterId
		From Portfolio.AccountReceivable ar With(Nolock) 
		Inner Join Portfolio.AccountReceivableAccounting ara With(Nolock) On ara.AccountReceivableId = ar.Id
		Where ar.Id = @AccountReceivableId

		Declare @StatusSequence Bit,
			@MessageSequence Varchar(255),
			@IdSequence Int

		Exec [Portfolio].[GetPortfolioSequenceByTag] '687', @OperativeUnitId, @StatusSequence Output, @MessageSequence Output, @IdSequence Output

		If @StatusSequence = 0 begin
			Set @StatusResult = 0
			Set @Message = @MessageSequence
			Set @ObjectEmbbeded = ''
			Return
		End
		
		Declare @errorList Varchar(Max) = ''

		Declare @ListPortfolioAdvanceCrossing Table(
			RowId Int Identity(1,1) Primary Key,
			Id Int,
			CrossingValue Decimal(18, 2)
		)
		Delete From @ListPortfolioAdvanceCrossing
		Insert Into @ListPortfolioAdvanceCrossing
		Select t.x.value('Id[1]', 'Int'),
			t.x.value('CrossingValue[1]', 'Decimal(18, 2)')
		From @ListPortfolioAdvanceCrossingXml.nodes('ListPortfolioAdvanceCrossing') t(x)
		
		Declare @CodeMessage Int, 
			@MessageTransfer Varchar(Max) = '',
			@PortfolioTransferIdSave Int,
			@PortfolioTransferCodeSave Varchar(20),
			@PortfolioTransferId Int,
			@PortfolioTransferCode Varchar(20),
			@Consecutive Varchar(20)

		If Exists (Select 1 From @ListPortfolioAdvanceCrossing) Begin
			
			Declare @Rows Int, @RowId Int
			Set @Rows = 1
			Set @RowId = 1

			Declare @PortfolioAdvanceId Int,
				@CrossingValue Decimal(18, 2)

			While @Rows > 0
			Begin
				
				Select Top 1 @RowId = RowId, 
					@PortfolioAdvanceId = Id,
					@CrossingValue = CrossingValue
				From @ListPortfolioAdvanceCrossing Where RowId >= @RowId Order By RowId

				Set @Rows = @@ROWCOUNT
				If @Rows = 0 
					Break
					
				Declare @ThirdPartyId Int,
					@MainAccountId Int,
					@CostCenterId Int

				Select @ThirdPartyId = ThirdPartyId,
					@MainAccountId = MainAccountId,
					@CostCenterId = CostCenterId
				From Portfolio.PortfolioAdvance With(Nolock) Where Id = @PortfolioAdvanceId
		
				--Insertamos en una tabla temporal los portfoliotransfer
				Declare @PortfolioTransfer Table(
					Id Int,
					Code varchar(20),
					DocumentDate Varchar(19),
					CustomerId Int Null,
					ThirdPartyId Int,
					PortfolioAdvanceId Int,
					TransferType Tinyint,
					MainAccountId Int,
					CostCenterId Int Null,
					Observations varchar(100),
					OperatingUnitId Int,
					[Status] Tinyint
				)
				Delete From @PortfolioTransfer

				Insert Into @PortfolioTransfer
				--Values (0,'',GetDate(),Null,@ThirdPartyId,@PortfolioAdvanceId,0,
				Values (0,'',Convert(Varchar(19), GetDate(), 103),Null,@ThirdPartyId,@PortfolioAdvanceId,0,
				@MainAccountId,@CostCenterId,
				'Traslado de anticipo modulo facturación',@OperativeUnitId,1)

				Declare @PortfolioTransferDetail Table(
					Id Int,
					PortfolioTrasferId Int,
					AccountReceivableId Int,
					MainAccountId Int,
					CostCenterId Int Null,
					[Value] Decimal(18,2),
					ChangeTracker Tinyint
				)
				Delete From @PortfolioTransferDetail
				Insert Into @PortfolioTransferDetail
				Values (0,0,@AccountReceivableId, @AccountReceivableAccountingMainAccountId, @AccountReceivableAccountingCostCenterId, @CrossingValue, 0)
				
				/*Proceso de confirmacion de PortfolioTransfer*/
				Declare @resultConfirm Varchar(255)
				--SavePortfolioTransfer
				Declare @PortfolioTransferXml Xml = (
					Select *
					From @PortfolioTransfer As PortfolioTransfer
					Cross Apply @PortfolioTransferDetail As PortfolioTransferDetail For Xml Auto, Elements
				)
				
				Exec Portfolio.SP_SaveTransfer_Output @PortfolioTransferXml, 
					@UserCode, 
					@CodeMessage Output, 
					@MessageTransfer Output, 
					@PortfolioTransferIdSave Output, 
					@PortfolioTransferCodeSave Output
				
				--Select @CodeMessage = CodeMessage, @MessageTransfer = [Message], @PortfolioTransferIdSave = Id, @PortfolioTransferCodeSave = CodeTransfer From @tmpResultSave
				
				If @CodeMessage <> 0 Begin
					Set @errorList += @MessageTransfer + Char(13) + Char(10)
				End
				Else Begin
				
					/*Proceso de confirmación*/
					Set @PortfolioTransferXml = (
						Select PortfolioTransfer.Id, 
							PortfolioTransfer.Code,
							--PortfolioTransfer.DocumentDate,
							Convert(Varchar(19), PortfolioTransfer.DocumentDate, 103) As DocumentDate,
							PortfolioTransfer.ThirdPartyId,
							PortfolioTransfer.PortfolioAdvanceId,
							PortfolioTransfer.TransferType,
							PortfolioTransfer.MainAccountId,
							PortfolioTransfer.CostCenterId,
							PortfolioTransfer.Observations,
							PortfolioTransfer.OperatingUnitId,
							PortfolioTransfer.[Status],
							PortfolioTransferDetail.Id,
							PortfolioTransferDetail.PortfolioTrasferId,
							PortfolioTransferDetail.AccountReceivableId,
							PortfolioTransferDetail.MainAccountId,
							PortfolioTransferDetail.CostCenterId,
							PortfolioTransferDetail.[Value],
							0 As 'ChangeTracker'
						From Portfolio.PortfolioTransfer As PortfolioTransfer
						Inner Join Portfolio.PortfolioTransferDetail As PortfolioTransferDetail On PortfolioTransfer.Id = PortfolioTransferDetail.PortfolioTrasferId
						Where PortfolioTransfer.Id = @PortfolioTransferIdSave For Xml Auto, Elements
					)

					Exec Portfolio.SP_ConfirmPortfolioTransfer_Out @PortfolioTransferXml, 
						@UserCode, 
						0, 
						0, 
						@CompanyType,
						@CodeMessage Output,
						@MessageTransfer Output,
						@PortfolioTransferId Output,
						@PortfolioTransferCode Output,
						@Consecutive Output					
					
					If @CodeMessage <> 0 Begin
						Set @errorList += 'Se guardo el registro con el codigo ' + @PortfolioTransferCodeSave + ', pero no se pudo confirmar por: ' + @MessageTransfer + Char(13) + Char(10)
					End
					--Set @ObjectEmbbeded = @MessageTransfer
					Set @ObjectEmbbeded = 'Se ha generado un documento de traslado con los siguientes datos: Código (' + @PortfolioTransferCode + ') Consecutivo (' + @Consecutive + ')'
				End
				Set @RowId += 1
			End
		End

		If @errorList <>  '' Begin
			Set @StatusResult = 0
			Set @Message = @errorList
			Set @ObjectEmbbeded = ''
		End
		Else Begin
			Set @StatusResult = 1
			Set @Message = ''
			--Set @ObjectEmbbeded = 'El documento se ha guardado con los siguientes datos: Código ' + @PortfolioTransferCode + ' Consecutivo ' + @Consecutive
			--
		End
	End Try
	Begin Catch
		Set @StatusResult = 0
		Set @Message = (Select Error_Message())
		Set @ObjectEmbbeded = ''
	End Catch	
End