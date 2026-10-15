-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar, reversar una factura de monto fijo
-- =============================================
CREATE PROCEDURE [Billing].[SP_SaveInvoiceEntityCapitated]
    @InvoiceEntityCapitatedXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@Code VARCHAR(20),
			@Status TINYINT,
			-------------------------------------------------------------------
			@Message VARCHAR(MAX),
			-------------------------------------------------------------------
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	BEGIN TRY
		--Se obtienen los datos de la cabecera
		SELECT	@Id = t.x.value('Id[1]','int'),
				@Code = t.x.value('Code[1]','varchar(20)'),
				@Status = t.x.value('Status[1]','tinyint')
		FROM @InvoiceEntityCapitatedXml.nodes('/InvoiceEntityCapitated') t(x)
		
		/********************************** *************************************** **********************************/

		IF @Status = 4
		BEGIN
			EXEC [Billing].[SP_ReverseInvoiceEntityCapitated_Output] @InvoiceEntityCapitatedXml, @CodeUser, @Code_Output OUT, @Message_Output OUT
					
			IF @Code_Output <> 0
			BEGIN
				SELECT	999 CodeResult, 
						ISNULL(@Message_Output, 'No se pudo reversar la factura monto fijo') MessageResult,
						0 Id,
						'' Code
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END
		ELSE
		BEGIN
			EXEC [Billing].[SP_SaveInvoiceEntityCapitated_Output] @InvoiceEntityCapitatedXml, @CodeUser, @Code_Output OUT, @Message_Output OUT, @Id OUT, @Code OUT
					
			IF @Code_Output <> 0
			BEGIN
				SELECT	999 CodeResult, 
						ISNULL(@Message_Output, 'No se pudo guardar la factura monto fijo') MessageResult,
						0 Id,
						'' Code
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/********************************** *************************************** **********************************/		

		SELECT 0 CodeResult, 
			   CASE @Status
				   WHEN 2 THEN CONCAT('Se guardó y confirmó la factura monto fijo con código ', @Code)
				   WHEN 3 THEN CONCAT('Se anuló la factura monto fijo con código ', @Code)
				   WHEN 4 THEN CONCAT('Se reversó la factura monto fijo con código ', @Code)
				   ELSE CONCAT('Se guardó la factura monto fijo con código ', @Code)
			   END + IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10) + ISNULL(@Message, '')) MessageResult,
			   @Id Id,
			   @Code Code
	END TRY
	BEGIN CATCH
		SELECT 999 CodeResult, 
			   'SP_SaveInvoiceEntityCapitated: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) MessageResult,
			   0 Id,
			   '' Code
	END CATCH
END