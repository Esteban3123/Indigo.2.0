-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-10
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una modificación de presupuesto
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveBudgetModification]
    @BudgetModificationXml AS XML,
	@BudgetModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveBudgetModification_Output] 
		@BudgetModificationXml, 
		@BudgetModificationDetailForDeleteXml, 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida de una modificación presupuestaria: permite crear, actualizar o confirmar cambios sobre un presupuesto existente. Recibe los datos de la modificación y sus detalles a eliminar en formato XML, junto con el código del usuario que ejecuta la operación. Delega el procesamiento real al procedimiento interno SP_SaveBudgetModification_Output y devuelve el resultado de la operación (código, mensaje, identificador y código del registro afectado). Es el punto de entrada principal para cualquier ajuste o corrección sobre el presupuesto en el módulo Budget.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBudgetModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBudgetModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveBudgetModification_Output el guardado/actualización/confirmación de una modificación de presupuesto y devuelve como resultset el código de resultado, mensaje, auxiliar, Id y Code generados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la cabecera/detalle de la modificación de presupuesto y, opcionalmente, un XML con los detalles a eliminar.; El código de usuario que ejecuta la operación debe estar definido.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento Budget.SP_SaveBudgetModification_Output; este wrapper solo proyecta sus resultados.; Siempre devuelve una única fila con CodeResult, MessageResult, AuxiliaryResult, Id y Code, independientemente del resultado de la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modificación de presupuesto; Usuario', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras invocar SP_SaveBudgetModification_Output, retorna un SELECT con @CodeResult, @MessageResult, @AuxiliaryResult, @Id y @Code obtenidos como OUTPUT del procedimiento delegado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveBudgetModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBudgetModification';
-- GO
