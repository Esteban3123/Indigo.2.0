

-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-05-27
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una conciliación bancaria
-- =============================================
CREATE PROCEDURE [Treasury].[SP_SaveBankReconciliation]
    @BankReconciliationXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeMessage INT,
			@Message VARCHAR(200),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Treasury].[SP_SaveBankReconciliation_Output] @BankReconciliationXml, @CodeUser, @CodeMessage OUTPUT, @Message OUTPUT, @Id OUTPUT, @Code OUTPUT

	SELECT 
		@CodeMessage AS CodeMessage, 
		@Message AS Message, 
		@Id as Id, 
		@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona el ciclo de vida de una conciliación bancaria en el módulo de Tesorería: permite crear, actualizar o confirmar un registro de conciliación. Recibe los datos de la conciliación en formato XML junto con el código del usuario que realiza la operación, y delega el procesamiento real al procedimiento interno SP_SaveBankReconciliation_Output, del cual obtiene el resultado de la operación (código de mensaje, descripción, identificador y código generado). Retorna al cliente el resultado de la transacción indicando si la conciliación bancaria fue guardada o procesada exitosamente.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBankReconciliation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBankReconciliation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega en SP_SaveBankReconciliation_Output la lógica de guardar/actualizar/confirmar una conciliación bancaria y devuelve el resultado como conjunto de filas.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un XML con la información de la conciliación bancaria.; Se debe proveer el código del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No realiza escrituras directamente; toda la persistencia se delega al procedimiento SP_SaveBankReconciliation_Output.; Siempre retorna exactamente cuatro columnas: CodeMessage, Message, Id y Code.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Conciliación bancaria; Usuario', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con CodeMessage, Message, Id y Code obtenidos del procedimiento interno invocado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Treasury.SP_SaveBankReconciliation_Output', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBankReconciliation';
-- GO
