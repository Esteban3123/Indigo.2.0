-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 06-11-2015
-- Description:	Procedimiento el cual se encarga de guardar un recaudo
-- =============================================
CREATE PROCEDURE [Budget].[SP_SaveCollection]
	@CollectionXML AS XML,
	@CollectionDetailForDeleteXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Budget].[SP_SaveCollection_Output] @CollectionXML, @CollectionDetailForDeleteXml, @CodeUser, @CodeResult OUTPUT, @MessageResult OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeResult AS CodeMessage, 
		@MessageResult AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda un recaudo (cobro o pago recibido) en el módulo de presupuesto. Recibe la información del recaudo y el detalle de elementos a eliminar en formato XML, junto con el usuario que realiza la operación. Delega el procesamiento real al procedimiento SP_SaveCollection_Output y retorna el resultado de la operación, incluyendo un código de mensaje, descripción, identificador y código del recaudo generado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollection';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_SaveCollection';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el guardado de un recaudo y expone como result set el código de mensaje, mensaje, Id y código generados por el procedimiento subyacente.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se requiere XML con la información del recaudo y un XML con el detalle a eliminar.; Se requiere el código de usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La persistencia del recaudo se delega íntegramente al procedimiento Budget.SP_SaveCollection_Output; este wrapper no realiza lógica adicional ni validaciones propias.; El resultado siempre se devuelve como un único result set con las columnas CodeMessage, Message, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recaudo; Detalle de recaudo', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras ejecutar SP_SaveCollection_Output, devuelve un SELECT con CodeMessage, Message, Id y Code provenientes de los parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollection_Output', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_SaveCollection';
-- GO
