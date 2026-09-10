-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-11
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de la disponibilidad
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveAvailabilityModification]
    @AvailabilityModificationXml AS XML,
	@AvailabilityModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveAvailabilityModification_Output] 
		@AvailabilityModificationXml, 
		@AvailabilityModificationDetailForDeleteXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@AuxiliaryResult OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@AuxiliaryResult AS AuxiliaryResult, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda, actualiza o confirma una modificación de disponibilidad presupuestal, actuando como punto de entrada público que delega la lógica principal al procedimiento interno SP_SaveAvailabilityModification_Output. Recibe los datos de la modificación y sus detalles a eliminar en formato XML, junto con el código del usuario que realiza la operación. Retorna un código de resultado, un mensaje de respuesta, un resultado auxiliar, el identificador y el código del registro afectado, permitiendo al sistema de presupuesto (Budget) registrar cambios en la disponibilidad de recursos financieros.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAvailabilityModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAvailabilityModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Wrapper que invoca el SP _Output para guardar, actualizar o confirmar una modificación de disponibilidad presupuestal y expone sus salidas como un result set."', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse un XML con la cabecera/detalle de la modificación de disponibilidad y un XML con los detalles a eliminar.; Debe recibirse el código del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al SP _Output; este wrapper no realiza escrituras propias.; El resultado se devuelve siempre como un único result set con CodeResult, MessageResult, AuxiliaryResult, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de disponibilidad presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_SaveAvailabilityModification_Output, retorna un SELECT con CodeResult, MessageResult, AuxiliaryResult, Id y Code obtenidos por OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveAvailabilityModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAvailabilityModification';
-- GO
