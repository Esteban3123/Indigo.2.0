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