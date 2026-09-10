-- =============================================
-- Author:      Giovanny Plazas
-- Create Date: 22/07/2024
-- Description: Procedimiento que se usa para revalorizar un documento a la fecha actual (TRM)
-- =============================================

/* @ListAccountPayable =
'<Data>
<DeferredCausationRevaluation>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</DeferredCausationRevaluation>
<DeferredCausationRevaluation>
<Id></Id>
<ValueAdjustment></ValueAdjustment>
<EntityName></EntityName>
<EntityId></EntityId>
<DocumentDate></DocumentDate>
</DeferredCausationRevaluation>
</Data>*/
CREATE PROCEDURE [Payments].[SP_DeferredCausationRevaluation]
(
    @ListDeferredCausationXml Xml,
	@UserCode VARCHAR(25)
)
AS
BEGIN
	DECLARE @XmlOutPut xml 
	DECLARE @responseRevaluation table (Code varchar(20),MessageResult varchar(max),IdJournalVoucher integer)

	BEGIN TRY
			
			EXEC [Payments].[SP_DeferredCausationRevaluation_Output] @ListDeferredCausationXml,@UserCode,@XmlOutPut OUTPUT

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza la revalorización de documentos de causación diferida a la fecha actual, aplicando la Tasa Representativa del Mercado (TRM) vigente. Recibe una lista de registros de causación diferida en formato XML (con identificador, valor de ajuste, nombre y código de entidad, y fecha del documento) junto con el código del usuario que ejecuta la operación. Internamente delega el procesamiento contable al procedimiento SP_DeferredCausationRevaluation_Output y captura el resultado, devolviendo un código de respuesta, un mensaje de resultado y el identificador del comprobante contable (voucher) generado. Se utiliza en el módulo de Pagos para ajustar el valor en libros de obligaciones o derechos diferidos cuando existe variación cambiaria, garantizando que los saldos contables reflejen el valor correcto a la fecha de revalorización.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_DeferredCausationRevaluation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_DeferredCausationRevaluation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Revaloriza documentos diferidos a la TRM de la fecha actual delegando el cálculo a un procedimiento interno y devuelve el resultado parseado desde XML.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura Data/DeferredCausationRevaluation con los nodos esperados (Id, ValueAdjustment, EntityName, EntityId, DocumentDate); Debe existir un usuario válido que ejecute la operación', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un result-set con las columnas Code/MessageCode, MessageResult/MessageVoucher e IdJournalVoucher/JournalVoucherId, incluso ante errores; Los errores nunca se propagan al llamador: se capturan y transforman en un registro con código 999; El procedimiento no realiza directamente operaciones DML sobre tablas físicas; toda la lógica de persistencia se delega al procedimiento _Output', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Revalorización de documentos; TRM (Tasa Representativa del Mercado); Causación diferida; Comprobante contable (JournalVoucher); Ajuste de valor (ValueAdjustment)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @responseRevaluation: Se inserta una fila por cada nodo /TableResult del XML retornado por el procedimiento de cálculo, extrayendo Code, MessageOutput y JournalVoucherId; [RETURN_RESULT] result-set: Devuelve Code, MessageResult e IdJournalVoucher resultantes de la revalorización; [RETURN_RESULT] result-set: Si ocurre una excepción en el TRY, retorna un único registro con MessageCode=999, el ERROR_MESSAGE() y JournalVoucherId=0', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ejecución exitosa del procedimiento de revalorización (TRY) → Parsea el XML de salida y retorna los resultados de la revalorización else En el CATCH retorna código 999 con el mensaje de error y JournalVoucherId en 0', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payments.SP_DeferredCausationRevaluation_Output', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_DeferredCausationRevaluation';
-- GO
