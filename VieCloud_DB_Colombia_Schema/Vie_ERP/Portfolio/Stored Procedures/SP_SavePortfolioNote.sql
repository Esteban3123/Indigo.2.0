-- =============================================
-- Author:		Carlos Ernesto Cordoba
-- Create date:	2016-07-11
-- Description:	Procedimiento para guardar, actualizar, confirmar o anular la nota
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SavePortfolioNote]
    @PortfolioNoteXml AS XML,
    @User VARCHAR(20),
	------------------------------------------------------
	@CompanyType TINYINT
AS
BEGIN
    SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Portfolio].[SP_SavePortfolioNote_Output] @PortfolioNoteXml, @User, @CompanyType, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para guardar, actualizar, confirmar o anular una nota de cartera (portfolio note). Recibe los datos de la nota en formato XML junto con el usuario que realiza la operación y el tipo de empresa, y delega el procesamiento real al procedimiento interno SP_SavePortfolioNote_Output. Retorna un código de resultado, un mensaje, el identificador generado y el código de la nota afectada, permitiendo al sistema conocer el resultado de la operación sobre la entidad de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioNote';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SavePortfolioNote';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SavePortfolioNote_Output el guardado, actualización, confirmación o anulación de una nota de cartera y expone el resultado (código, mensaje, Id y Code) como result set.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con la información de la nota de cartera y un usuario responsable de la operación.; Debe especificarse el tipo de compañía (@CompanyType) para enrutar la operación en el SP interno.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia (guardar/actualizar/confirmar/anular) se delega íntegramente al SP interno SP_SavePortfolioNote_Output; este wrapper no aplica reglas adicionales.; Siempre devuelve un único result set con CodeResult, MessageResult, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de cartera (PortfolioNote); Confirmación/anulación de nota; Compañía (CompanyType)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SavePortfolioNote_Output, retorna SELECT con CodeResult, MessageResult, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.SP_SavePortfolioNote_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SavePortfolioNote';
-- GO
