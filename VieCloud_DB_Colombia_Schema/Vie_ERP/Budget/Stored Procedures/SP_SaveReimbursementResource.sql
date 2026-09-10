-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-07-19
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar un reintegro
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveReimbursementResource]
    @ReimbursementResourceXml AS XML,
	@ReimbursementResourceDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@AuxiliaryResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveReimbursementResource_Output] 
		@ReimbursementResourceXml, 
		@ReimbursementResourceDetailForDeleteXml,
		@CodeUser,
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 
		@AuxiliaryResult OUTPUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT 
		@CodeResult AS CodeMessage, 
		@MessageResult AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar un reintegro de recursos presupuestales. Recibe la información del reintegro y sus detalles a eliminar en formato XML, junto con el código del usuario que realiza la operación. Delega la lógica principal al procedimiento SP_SaveReimbursementResource_Output y retorna el resultado de la operación, incluyendo el identificador y código del reintegro procesado. Es el punto de entrada para la gestión de reintegros dentro del módulo de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveReimbursementResource';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveReimbursementResource';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveReimbursementResource_Output la operación de guardar, actualizar o confirmar un reintegro y expone el resultado como conjunto de resultados (CodeMessage, Message, Id, Code).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere recibir el XML con la información del reintegro y, opcionalmente, el XML con el detalle a eliminar, además del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La persistencia (guardar/actualizar/confirmar) del reintegro se delega íntegramente al procedimiento Budget.SP_SaveReimbursementResource_Output; este wrapper no realiza DML por sí mismo.; Siempre devuelve un result set único con las columnas CodeMessage, Message, Id y Code obtenidas de los OUTPUT del procedimiento invocado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reintegro', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveReimbursementResource_Output, retorna SELECT con @CodeResult AS CodeMessage, @MessageResult AS Message, @Id, @Code.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveReimbursementResource_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveReimbursementResource';
-- GO
