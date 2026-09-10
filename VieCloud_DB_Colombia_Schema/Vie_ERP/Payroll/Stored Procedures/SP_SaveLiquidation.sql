-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-04-12
-- Description:	Procedimiento que se encarga de guardar, actualizar, anular, confirmar, reversar una liquidación de nómina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveLiquidation]
    @LiquidationXml AS XML,
	@UserCode AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables para obtener la cabecera
	DECLARE @Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)
			

	EXEC [Payroll].[SP_SaveLiquidation_Output]
		@LiquidationXml,
		@UserCode,
		--------------------------------------------
		@Code_Output OUT, 
		@Message_Output OUT,
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@Code_Output AS CodeResult, 
			@Message_Output AS MessageResult, 
			@Id as Id, 
			@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento central de gestión de liquidaciones de nómina que permite crear, actualizar, anular, confirmar y reversar una liquidación. Recibe los datos de la liquidación en formato XML y el código del usuario que ejecuta la operación. Delega el procesamiento principal al procedimiento SP_SaveLiquidation_Output, del cual obtiene el resultado de la operación (código de respuesta y mensaje) junto con el identificador y código de la liquidación procesada. Retorna al cliente el resultado de la operación indicando si fue exitosa o no, junto con los datos de identificación de la liquidación afectada.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLiquidation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveLiquidation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveLiquidation_Output el guardar/actualizar/anular/confirmar/reversar una liquidación de nómina y retorna como result set el código de resultado, mensaje, Id y código generados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de liquidación debe poder ser interpretado por SP_SaveLiquidation_Output (este wrapper no valida contenido).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la lógica de persistencia se delega al procedimiento SP_SaveLiquidation_Output; este wrapper solo expone los resultados como result set.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_SaveLiquidation_Output, devuelve un SELECT con CodeResult, MessageResult, Id y Code obtenidos de los OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.SP_SaveLiquidation_Output', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveLiquidation';
-- GO
