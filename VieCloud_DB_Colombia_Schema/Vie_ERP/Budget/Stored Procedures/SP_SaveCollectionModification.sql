-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-11
-- Description:	Procedimiento el cual se encarga de guardar la modificación de un recaudo
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCollectionModification]
	@CollectionModificationXml AS XML,
	@CollectionModificationDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCollectionModification_Output] @CollectionModificationXml, @CollectionModificationDetailForDeleteXml, @CodeUser, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeResult, 
		@MessageResult AS MessageResult, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza la modificación de un recaudo (cobro) en el módulo de presupuesto. Recibe los datos de la modificación y el detalle de elementos a eliminar en formato XML, junto con el código del usuario que realiza el cambio. Delega el procesamiento real al procedimiento interno SP_SaveCollectionModification_Output y retorna el resultado de la operación con un código de estado, un mensaje, el identificador y el código del registro afectado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollectionModification';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollectionModification';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega la persistencia de una modificación de recaudo en el SP _Output y retorna como result set el código de resultado, mensaje, Id y Code generados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere XML con la cabecera/detalle de la modificación del recaudo y un XML con los detalles a eliminar.; Debe proveerse el código de usuario que realiza la modificación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento _Output; este wrapper solo expone el resultado como result set.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'recaudo; modificación de recaudo; presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Siempre retorna un único result set con columnas CodeResult, MessageResult, Id y Code obtenidos de los OUTPUT del SP delegado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollectionModification_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollectionModification';
-- GO
