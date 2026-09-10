-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-15
-- Description:	Procedimiento que se encarga de reversar el cruce de anticipo vs cxc
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_ReversePortfolioTransfer]
	@PortfolioNoteId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT,
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id_Output INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					PortfolioNote.*
				FROM 
				(
					SELECT 
						pn.Id,
						@CodeUser as CodeUser,
						@CompanyType as CompanyType,
						'' as EntityName
					FROM Portfolio.PortfolioNote pn
					WHERE pn.Id = @PortfolioNoteId
				) PortfolioNote
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC Portfolio.SP_ReversePortfolioTransferXML_Output @SubXml, @Code_Output OUT, @Message_Output OUT

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa o anula el cruce entre un anticipo y una cuenta por cobrar (CxC) en el módulo de cartera. Recibe el identificador de una nota de cartera, el usuario que ejecuta la acción y el tipo de empresa, luego consulta la nota en Portfolio.PortfolioNote y delega el procesamiento al procedimiento Portfolio.SP_ReversePortfolioTransferXML_Output mediante un XML generado dinámicamente. Si la reversión falla, retorna un código de error con el mensaje correspondiente; si es exitosa, confirma el resultado al proceso llamador. Se utiliza cuando es necesario deshacer un cruce de anticipo ya aplicado contra una factura o cuenta por cobrar de un cliente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReversePortfolioTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reversa un cruce de anticipo aplicado contra una factura/CxC delegando la operación a un procedimiento interno mediante un payload XML construido a partir de la nota de cartera.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Portfolio.PortfolioNote con Id igual al identificador recibido para que el XML generado contenga datos.; Se requiere un código de usuario y tipo de compañía válidos para incluir en el XML enviado al procedimiento de reverso.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la lógica de reverso se delega al SP interno SP_ReversePortfolioTransferXML_Output; este procedimiento solo orquesta y traduce resultados.; Cualquier fallo (controlado o por excepción) se uniformiza con CodeResult=999.; El XML siempre incluye EntityName como cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cruce de anticipo; Cuentas por cobrar (CxC); Factura; Nota de cartera; Reverso de aplicación de anticipo', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioNote: Construye un XML con Id de la nota, CodeUser, CompanyType y EntityName vacío y lo entrega al SP interno de reverso.; [RETURN_RESULT] @CodeResult/@MessageResult: Si SP_ReversePortfolioTransferXML_Output retorna Code_Output<>0, devuelve CodeResult=999 con mensaje ''No se pudo reversar los cruces de anticipo asociados a la factura'' (o el mensaje recibido).; [RETURN_RESULT] @CodeResult/@MessageResult: Si la ejecución es exitosa, devuelve CodeResult=0 y el mensaje proveniente del SP interno (o cadena vacía).; [RETURN_RESULT] @CodeResult/@MessageResult: Ante excepción capturada, devuelve CodeResult=999 con ERROR_MESSAGE() concatenado con el número de línea.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Code_Output <> 0 tras ejecutar SP_ReversePortfolioTransferXML_Output → Retorna error 999 con mensaje por defecto de fallo en reverso de cruces de anticipo else Retorna 0 con el mensaje devuelto por el SP interno', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_ReversePortfolioTransferXML_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReversePortfolioTransfer';
-- GO
