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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o registra un comprobante contable en el libro mayor general, recibiendo los datos del comprobante en formato XML y el código del usuario que realiza la operación. Actúa como punto de entrada simplificado que delega la lógica real al procedimiento SP_SaveJournalVoucher_Output, el cual retorna el resultado mediante parámetros de salida en lugar de una tabla, evitando problemas de ejecución en llamadas anidadas. Devuelve un código de respuesta, un mensaje descriptivo del resultado y el identificador del comprobante contable creado o actualizado.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_SaveJournalVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que guarda un comprobante contable invocando el SP de salida y devuelve el resultado como conjunto de filas (en lugar de parámetros OUTPUT) para evitar problemas con SP anidados.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibir un XML de comprobante contable y un código de usuario válido para que el SP interno procese el guardado.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Actúa como wrapper: delega toda la lógica de negocio al SP interno y expone los valores de salida como result set.; Siempre retorna exactamente una fila con los tres campos: código de mensaje, mensaje e identificador del comprobante.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante contable; Journal Voucher; Libro mayor', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar el SP interno, devuelve un SELECT con CodeMessage, Message e IdJournalVoucher obtenidos de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'GeneralLedger.SP_SaveJournalVoucher_Output', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_SaveJournalVoucher';
-- GO
