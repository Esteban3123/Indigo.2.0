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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para gestionar facturas de monto fijo (capitadas) a entidades. Según el estado recibido, realiza las operaciones de guardar, actualizar, confirmar, anular o reversar una factura capitada: si el estado es 4 delega el reverso al procedimiento SP_ReverseInvoiceEntityCapitated_Output, y para cualquier otro estado delega la operación de guardado o confirmación a SP_SaveInvoiceEntityCapitated_Output. Recibe los datos de la factura en formato XML y el código del usuario que ejecuta la acción, y retorna el resultado de la operación con el código y mensaje correspondiente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInvoiceEntityCapitated';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquesta el guardado, actualización, anulación, confirmación o reverso de una factura de monto fijo (capitated) delegando en los SP de guardado o reverso según el estado recibido y unifica la respuesta de resultado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @InvoiceEntityCapitatedXml debe contener el nodo /InvoiceEntityCapitated con los elementos Id, Code y Status; @CodeUser identifica al usuario que ejecuta la operación y se propaga a los SP delegados; El valor de Status debe corresponder a una operación soportada (2=confirmar, 3=anular, 4=reversar; otros=guardar)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado 4 siempre se enruta exclusivamente al flujo de reverso; el resto de estados se canaliza por el flujo de guardado; Cualquier error capturado o reportado por los SP delegados se traduce a CodeResult=999 con Id=0 y Code=''''; Los mensajes de éxito siempre incluyen el código de la factura resultante; El SP nunca propaga excepciones: el CATCH las convierte en un resultset de error con la línea del error', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura de monto fijo (capitated); Reverso de factura; Anulación de factura; Confirmación de factura', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.SP_ReverseInvoiceEntityCapitated_Output: Cuando @Status=4 ejecuta el SP de reverso; si retorna @Code_Output<>0 devuelve resultset con CodeResult=999 y mensaje ''No se pudo reversar la factura monto fijo'' (o el mensaje recibido); [RETURN_RESULT] Billing.SP_SaveInvoiceEntityCapitated_Output: Cuando @Status<>4 ejecuta el SP de guardado; si retorna @Code_Output<>0 devuelve resultset con CodeResult=999 y mensaje ''No se pudo guardar la factura monto fijo'' (o el mensaje recibido); [RETURN_RESULT] (resultset): En éxito devuelve CodeResult=0, Id y Code de la factura, y un MessageResult construido según @Status (guardó/confirmó/anuló/reversó) concatenando mensajes adicionales del SP delegado; [RETURN_RESULT] (resultset): En el bloque CATCH devuelve CodeResult=999 con ''SP_SaveInvoiceEntityCapitated: '' + ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE(), Id=0 y Code=''''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Status = 4 (estado de reverso) → Invoca SP_ReverseInvoiceEntityCapitated_Output para revertir la factura de monto fijo else Invoca SP_SaveInvoiceEntityCapitated_Output para guardar/actualizar/anular/confirmar la factura; si @Code_Output <> 0 tras la ejecución del SP delegado → Devuelve CodeResult=999 con el mensaje de error (o un texto por defecto según la operación) y termina con RETURN; si CASE @Status al construir el mensaje de éxito → Status=2 → ''Se guardó y confirmó''; Status=3 → ''Se anuló''; Status=4 → ''Se reversó''; cualquier otro → ''Se guardó''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_ReverseInvoiceEntityCapitated_Output; Billing.SP_SaveInvoiceEntityCapitated_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInvoiceEntityCapitated';
-- GO
