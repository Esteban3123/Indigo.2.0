CREATE Procedure [GeneralLedger].[SP_SaveJournalVoucher]
	@JournalVoucherXml as Xml,
	@CodeUser as varchar(20)
As
Begin	
	--se refactoriza el sp de comprobantes contables creando un nuevo sp que devuelva valores concretos y no una tabla debido a problemas de ejecucion de sp anidados
	Declare @CodeMessage Int,
		@Message Varchar(Max),
		@IdJournalVoucherResult Int

	Exec [GeneralLedger].[SP_SaveJournalVoucher_Output] @JournalVoucherXml, @CodeUser, @CodeMessage Output, @Message Output, @IdJournalVoucherResult Output

	Select @CodeMessage as CodeMessage, @Message as [Message], @IdJournalVoucherResult IdJournalVoucher
	Return
End