
-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-07-19
-- Description:	Liquidación
-- =============================================

CREATE Procedure [Portfolio].[GetPortfolioSequenceByTag]
	@Tag Varchar(5),
	@OperativeUnitId Int,
	@StatusResult Bit Output,
	@Message Varchar(255) Output,
	@IdSequence Int Output
AS
Begin
	Set Nocount On;
	Begin Try

		Declare @PortfolioSequenceId Int,
			@Scope Varchar(2)

		Select @PortfolioSequenceId = ps.Id,
			@Scope = ps.Scope
		From Portfolio.PortfolioSequence ps With(Nolock) Where IdForm = @Tag

		If @PortfolioSequenceId Is Not Null And @PortfolioSequenceId > 0 Begin
			If @Scope = 'O' Begin --El ambito es a nivel de organización				
				Select Top 1 @IdSequence = psd.Id 
				From Portfolio.PortfolioSequenceDetail psd With(Nolock) 
				Where IdSequensePortfolioC = @PortfolioSequenceId
			End
			Else If @Scope = 'OU' Begin --El ambito es a nivel de unidad operativa
				Select Top 1 @IdSequence = psd.Id 
				From Portfolio.PortfolioSequenceDetail psd With(Nolock) 
				Where IdSequensePortfolioC = @PortfolioSequenceId And IdOperatingUnit = @OperativeUnitId
			End
			If @IdSequence Is Null Or @IdSequence <= 0 Begin
				Set @IdSequence = -1
				Set @StatusResult = 0
				Set @Message = 'No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar' + @Tag
			End
			Else Begin
				Set @StatusResult = 1
				Set @Message = ''
			End			
			Return
		End
		Else Begin
			Set @StatusResult = 0
			Set @Message = 'No se encontró secuencia numérica para el Tag ' + @Tag + ' de Cuentas por Cobrar'
			Set @IdSequence = -1
		End
	End Try
	Begin Catch		
		Set @StatusResult = 0
		Set @Message = 'No se encontró secuencia numérica para el Tag {0} de Cuentas por Cobrar' + @Tag
		Set @IdSequence = -1
	End Catch	
End