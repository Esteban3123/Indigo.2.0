-- =============================================
-- Author:      Giovanny Plazas
-- Create Date: 11/07/2024
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListAccountPayable =
'<AccountPayable>
<Id></Id>
<ValuePaid></ValuePaid>
<EntityName></EntityName>
<EntityId></EntityId>
</AccountPayable>'*/
CREATE PROCEDURE [Payments].[SP_AccountPayableRevaluation_WithOut_Output]
(
    @ListAccountPayableXml Xml,
	@UserCode VARCHAR(25)
)
AS
BEGIN
	DECLARE @XmlOutPut xml 
	DECLARE @responseRevaluation table (Code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
			
			EXEC [Payments].[SP_AccountPayableRevaluation] @ListAccountPayableXml,@UserCode,@XmlOutPut OUTPUT

			INSERT @responseRevaluation
			SELECT
					t.x.value('Code[1]', 'Varchar(20)')  Code,
					t.x.value('MessageOutput[1]', 'varchar(max)')  MessageOutput,
					t.x.value('JournalVoucherId[1]', 'INT')  JournalVoucherId
			from @XmlOutput.nodes('/TableResult') t(x);
					

			SELECT Code,MessageResult,IdJournalVoucher
			FROM @responseRevaluation
			
	END TRY
	BEGIN CATCH
		select 999 MessageCode, ERROR_MESSAGE() as MessageVoucher, 0 as JournalVoucherId
	END CATCH
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la revalorización de cuentas por pagar a la tasa de cambio vigente (TRM) del día, sin exponer parámetros de salida (OUTPUT) al llamador. Recibe una lista de documentos de cuentas por pagar en formato XML y el código del usuario que ejecuta la operación, luego delega el procesamiento real al procedimiento [Payments].[SP_AccountPayableRevaluation], capturando su resultado en una tabla temporal para retornarlo como conjunto de filas con el código de respuesta, mensaje y el identificador del comprobante contable (voucher) generado. Existe como variante simplificada para consumidores que no soportan parámetros OUTPUT, devolviendo el resultado de la revalorización directamente como un SELECT; ante cualquier error, retorna un código 999 con el mensaje de excepción.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que ejecuta la revalorización de cuentas por pagar a la TRM actual y devuelve el resultado como conjunto tabular en lugar de XML de salida.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura <AccountPayable> con Id, ValuePaid, EntityName, EntityId.; Debe existir el procedimiento Payments.SP_AccountPayableRevaluation que retorna un XML con nodos /TableResult conteniendo Code, MessageOutput y JournalVoucherId.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve un único resultset, ya sea con datos parseados del XML o con la fila de error.; Los errores no se propagan: se capturan y traducen al código 999.; El procedimiento no escribe directamente en tablas; delega toda la lógica de negocio al SP interno.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar (AccountPayable); Revalorización a TRM; Comprobante contable (JournalVoucher); Entidad / tercero', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Si la ejecución es exitosa, retorna filas (Code, MessageResult, IdJournalVoucher) extraídas de los nodos /TableResult del XML devuelto por el SP de revalorización.; [RETURN_RESULT] resultset: En el bloque CATCH, ante cualquier error retorna una única fila con MessageCode=999, MessageVoucher=ERROR_MESSAGE() y JournalVoucherId=0.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRY ejecuta SP_AccountPayableRevaluation sin error → Parsea el XML de salida y devuelve el resultset con Code, MessageResult e IdJournalVoucher. else CATCH retorna fila con código de error 999 y el mensaje de la excepción.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payments.SP_AccountPayableRevaluation', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_AccountPayableRevaluation_WithOut_Output';
-- GO
