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