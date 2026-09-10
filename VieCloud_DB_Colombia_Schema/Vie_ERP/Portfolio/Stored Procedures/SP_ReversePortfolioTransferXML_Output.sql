-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-15
-- Description:	Procedimiento que se encarga de reversar el cruce de anticipo vs cxc
-- Update : Giovanny Plazas
-- Modification Date: 2024-06-24
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReversePortfolioTransferXML_Output]
	@XmlParameters XML,
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT,
			-----------------------------------------------------
			@PortfolioNoteId INT,
			@CodeUser VARCHAR(20),
			------------------------------------------------------
			@CompanyType TINYINT,
			@EntityName VARCHAR(100)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		
        SELECT	@PortfolioNoteId = t.x.value('Id[1]', 'INT'),
				@CodeUser = t.x.value('CodeUser[1]', 'VARCHAR(20)'),
				@CompanyType =  t.x.value('CompanyType[1]', 'TINYINT'),
				@EntityName =  t.x.value('EntityName[1]', 'VARCHAR(100)')
        FROM @XmlParameters.nodes('/PortfolioNote') t(x);

		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					PortfolioTransfer.*
				FROM 
				(
					SELECT 
						pn.PortfolioTransferId Id,
						pt.Code,
						pn.OperatingUnitId,
						pn.NoteDate DocumentDate,
						4 Status,
						pn.Id PortfolioNoteId,
						IIF(@EntityName='Invoice',@EntityName,'') EntityName,
						pa.CurrencyId
					FROM Portfolio.PortfolioNote pn
					LEFT JOIN Portfolio.PortfolioTransfer pt WITH(NOLOCK) ON pn.PortfolioTransferId = pt.Id
					LEFT JOIN Portfolio.PortfolioAdvance pa WITH(NOLOCK) on pt.PortfolioAdvanceId=pa.Id
					WHERE pn.Id = @PortfolioNoteId
				) PortfolioTransfer
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC Portfolio.SP_SavePortfolioTransfer_Output @SubXml, @CodeUser, @CompanyType, @Code_Output OUT, @Message_Output OUT, NULL, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, 'No se pudo reversar los cruces de anticipo asociados a la factura')
			RETURN
		END

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que revierte (reversa) el cruce entre un anticipo de cartera y una cuenta por cobrar (CxC). Recibe por parámetro XML el identificador de una nota de cartera y, a partir de ella, recupera el traslado de cartera asociado (PortfolioTransfer) junto con el anticipo y la unidad operativa relacionados, construyendo un nuevo XML con estado 4 (reversado) para luego invocar SP_SavePortfolioTransfer_Output que persiste la anulación del cruce. Se usa en el módulo de cartera cuando es necesario deshacer la aplicación de un anticipo contra una factura, devolviendo el saldo al estado anterior al cruce.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa el cruce de un anticipo contra cuentas por cobrar reconstruyendo el XML de la transferencia de cartera asociada a una nota y delegando el guardado con estado de reverso al SP de transferencia.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /PortfolioNote con Id, CodeUser, CompanyType y EntityName; Debe existir un Portfolio.PortfolioNote con Id = @PortfolioNoteId que tenga PortfolioTransferId asociado para que el reverso tenga sentido', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El reverso siempre se ejecuta forzando Status=4 sobre la transferencia de cartera asociada; Solo se propaga EntityName cuando es ''Invoice''; cualquier otro valor se normaliza a cadena vacía; Cualquier excepción se captura y se devuelve como CodeResult=999 con ERROR_MESSAGE y línea', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo vs CxC; Reverso de transferencia de cartera; Anticipo de cartera; Nota de cartera; Factura', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Portfolio.PortfolioTransfer: Vía EXEC Portfolio.SP_SavePortfolioTransfer_Output con Status=4, se reversa la transferencia de cartera vinculada al PortfolioNote indicado; [RETURN_RESULT] @MessageResult: Devuelve CodeResult=0 si el SP de guardado retorna 0; en caso contrario CodeResult=999 con mensaje ''No se pudo reversar los cruces de anticipo asociados a la factura'' o el mensaje propagado; [RAISERROR] @MessageResult: En CATCH retorna CodeResult=999 con ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE()', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EntityName = ''Invoice'' → Se propaga EntityName=''Invoice'' al XML enviado al SP de guardado else Se envía EntityName vacío (''''); si @Code_Output <> 0 tras invocar SP_SavePortfolioTransfer_Output → Retorna CodeResult=999 con mensaje de error (o ''No se pudo reversar los cruces de anticipo asociados a la factura'' si no hay mensaje) else Retorna CodeResult=0 indicando reverso exitoso', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioTransfer_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Portfolio.PortfolioTransfer; Portfolio.PortfolioAdvance', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransferXML_Output';
-- GO
