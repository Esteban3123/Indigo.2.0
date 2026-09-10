
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera y confirma traslados de anticipos de cartera hacia una cuenta por cobrar específica. Recibe una lista de anticipos (pagos adelantados de terceros o aseguradoras) en formato XML y, por cada uno, crea un registro de transferencia en PortfolioTransfer con su detalle en PortfolioTransferDetail, asociando la cuenta contable y el centro de costo tomados de la cuenta por cobrar origen (AccountReceivable / AccountReceivableAccounting). Obtiene el consecutivo del traslado mediante GetPortfolioSequenceByTag y persiste cada movimiento a través de SP_SaveTransfer_Output, acumulando errores si algún traslado falla. Se utiliza en el módulo de cartera y facturación cuando un anticipo recibido de un pagador debe cruzarse (aplicarse) contra una factura o cuenta de cobro pendiente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GeneratePortfolioTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'GeneratePortfolioTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera y confirma documentos de traslado de cartera para aplicar anticipos a una cuenta por cobrar específica, recorriendo los cruces enviados en XML.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una secuencia parametrizada con tag ''687'' para la unidad operativa (validada vía GetPortfolioSequenceByTag).; El AccountReceivableId debe tener registro en Portfolio.AccountReceivableAccounting para obtener cuenta contable y centro de costo.; Cada Id del XML debe corresponder a un Portfolio.PortfolioAdvance existente para resolver ThirdPartyId, MainAccountId y CostCenterId.; Se debe recibir CompanyType (1=Privada, 2=Pública) tomado de auditoría.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada traslado se crea siempre con TransferType=0, Status=1 y observación fija ''Traslado de anticipo modulo facturación''.; La fecha del documento se formatea como Varchar(19) con estilo 103 (dd/mm/yyyy) a partir de GETDATE().; El detalle del traslado siempre apunta a la misma cuenta por cobrar (@AccountReceivableId) con la cuenta contable y centro de costo provenientes de AccountReceivableAccounting.; ChangeTracker del detalle se inicializa siempre en 0.; Cualquier excepción no controlada se captura en CATCH devolviendo StatusResult=0 y ERROR_MESSAGE() en Message.; Los errores de múltiples iteraciones se acumulan; basta que una falle para retornar StatusResult=0 al final.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Anticipo de cartera; Traslado de cartera; Cruce de anticipo con factura; Cuenta contable principal; Centro de costo; Tercero; Tipo de empresa (Privada/Pública); Secuencia/Consecutivo de documentos; Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.PortfolioTransfer: Por cada anticipo del XML se construye un traslado con TransferType=0, Status=1, Observación ''Traslado de anticipo modulo facturación'' y se persiste vía SP_SaveTransfer_Output.; [INSERT] Portfolio.PortfolioTransferDetail: Para cada traslado se inserta un detalle con la cuenta por cobrar destino, su MainAccountId/CostCenterId contables y el valor de cruce (CrossingValue) recibido en el XML.; [UPDATE] Portfolio.PortfolioTransfer: Tras guardarlo, se invoca SP_ConfirmPortfolioTransfer_Out para confirmar el traslado (obteniendo Code y Consecutive).; [RETURN_RESULT] @OUTPUT: Devuelve StatusResult=1 y mensaje de éxito con código y consecutivo si todo se guarda y confirma; StatusResult=0 con lista acumulada de errores si alguna iteración falla o si la secuencia no existe.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @StatusSequence = 0 (no se obtuvo la secuencia tag ''687'') → Aborta retornando StatusResult=0 y el mensaje del SP de secuencia, sin procesar el XML. else Continúa con el procesamiento de los anticipos.; si EXISTS filas en @ListPortfolioAdvanceCrossing → Itera fila por fila generando un PortfolioTransfer por cada anticipo cruzado. else No genera ningún traslado.; si @CodeMessage <> 0 tras SP_SaveTransfer_Output → Acumula el mensaje en @errorList y NO ejecuta la confirmación. else Procede a confirmar mediante SP_ConfirmPortfolioTransfer_Out.; si @CodeMessage <> 0 tras SP_ConfirmPortfolioTransfer_Out → Acumula error indicando ''Se guardó el registro con código X pero no se pudo confirmar'' y conserva el código guardado. else Asigna ObjectEmbbeded con código y consecutivo del traslado generado.; si @errorList <> '''' al finalizar el bucle → StatusResult=0, Message=@errorList, ObjectEmbbeded vacío. else StatusResult=1 y Message vacío.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetPortfolioSequenceByTag; Portfolio.SP_SaveTransfer_Output; Portfolio.SP_ConfirmPortfolioTransfer_Out', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Portfolio.AccountReceivableAccounting; Portfolio.PortfolioAdvance; Portfolio.PortfolioTransfer; Portfolio.PortfolioTransferDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'GeneratePortfolioTransfer';
-- GO
