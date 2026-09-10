-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-22
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una obligacion
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveObligation]
    @ObligationXml AS XML,
	@ObligationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveObligation_Output] @ObligationXml, @ObligationDetailForDeleteXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar una obligación presupuestaria. Recibe como parámetros un XML con los datos principales de la obligación, un XML con los detalles a eliminar y el código del usuario que realiza la operación. Delega toda la lógica de procesamiento al procedimiento interno SP_SaveObligation_Output y retorna el resultado de la operación indicando código de mensaje, descripción, identificador y código generado. Pertenece al módulo de presupuesto (Budget) y es el punto de entrada para gestionar compromisos u obligaciones de gasto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveObligation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el procedimiento de persistencia de obligaciones y expone como result set los valores de salida (código de mensaje, mensaje, Id y código generado).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer el XML con la cabecera/datos de la obligación a guardar, actualizar o confirmar.; Se debe proveer el XML con los detalles de obligación a eliminar (puede venir vacío si no aplica).; Se debe proveer el código del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento Budget.SP_SaveObligation_Output; este wrapper no implementa reglas propias.; Siempre se devuelve un único result set con CodeMessage, Message, Id y Code provenientes de los OUTPUT del procedimiento invocado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Obligación presupuestal; Detalle de obligación', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_SaveObligation_Output, se retorna un SELECT con CodeMessage, Message, Id y Code obtenidos como parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveObligation_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveObligation';
-- GO
