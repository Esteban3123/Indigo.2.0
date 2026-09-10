-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-09-30
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliacion
-- =============================================
CREATE PROCEDURE [Glosas].[SP_SaveConciliation]
    @ConciliationXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Id INT,
			@ConciliationConsecutive NUMERIC(18,0),
			-------------------------------------------------------------------
			@CodeResult INT,
			@MessageResult VARCHAR(MAX)

	EXEC [Glosas].[SP_SaveConciliation_Output]
		@ConciliationXml,
		@CodeUser,
		-----------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT,
		@Id OUTPUT, 
		@ConciliationConsecutive OUTPUT

	SELECT	@CodeResult CodeResult, 
			@MessageResult MessageResult,
			@Id Id,
			@ConciliationConsecutive Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite guardar, actualizar o confirmar una conciliación de glosas médicas. Recibe los datos de la conciliación en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento principal al procedimiento [Glosas].[SP_SaveConciliation_Output], del cual obtiene el resultado de la operación, un mensaje descriptivo, el identificador interno y el consecutivo asignado a la conciliación. Retorna estos cuatro valores al llamador para que el sistema pueda informar al usuario si la conciliación fue procesada exitosamente, junto con el código o número de radicado generado. Es el punto de entrada transaccional para la gestión de conciliaciones en el módulo de glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_SaveConciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado/actualización/confirmación de una conciliación de glosas y devuelve el resultado y consecutivo al cliente.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información de la conciliación; Se debe proveer el código del usuario que ejecuta la operación', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega completamente al SP interno SP_SaveConciliation_Output; Siempre devuelve una única fila con el estado de la operación', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'conciliación; glosas', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Siempre retorna un resultset con CodeResult, MessageResult, Id y Code (consecutivo) provenientes de la ejecución del SP interno', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Glosas.SP_SaveConciliation_Output', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_SaveConciliation';
-- GO
