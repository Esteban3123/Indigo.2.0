-- =============================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2021-03-08
-- Description:	Procedimiento que se encarga de guardar, actualizar, confirmar una convenio de nomina
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveAgreement]
    @AgreementXml AS XML,
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE @CodeResult INT,
			@MessageResult VARCHAR(MAX),
			@Id INT,
			@Code VARCHAR(20)

	EXEC [Payroll].[SP_SaveAgreement_Output]
		@AgreementXml, 
		@CodeUser, 
		--------------------------------------------
		@CodeResult OUTPUT, 
		@MessageResult OUTPUT, 		
		--------------------------------------------
		@Id OUTPUT, 
		@Code OUTPUT

	SELECT	@CodeResult AS CodeResult, 
			@MessageResult AS MessageResult, 
			@Id as Id, 
			@Code as Code
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que permite crear, actualizar o confirmar un convenio de nómina en el módulo de Payroll. Recibe la información del convenio en formato XML y el código del usuario que realiza la operación. Delega la lógica principal al procedimiento SP_SaveAgreement_Output, del cual obtiene el resultado de la operación (código de resultado, mensaje, identificador y código del convenio guardado). Retorna al llamador el estado del proceso y los datos del convenio afectado, siendo el punto de entrada estándar para la gestión de convenios de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAgreement';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAgreement';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que invoca el procedimiento de persistencia de convenios de nómina y devuelve como resultset el código de resultado, mensaje, identificador y código generados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer el XML con la información del convenio y el código del usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La lógica de persistencia se delega íntegramente al procedimiento Payroll.SP_SaveAgreement_Output; este wrapper no aplica reglas propias.; Siempre retorna un resultset con cuatro columnas: CodeResult, MessageResult, Id y Code, capturadas de los OUTPUT del procedimiento invocado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Convenio de nómina', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Tras ejecutar SP_SaveAgreement_Output, expone los parámetros de salida (@CodeResult, @MessageResult, @Id, @Code) mediante un SELECT como resultset al cliente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.SP_SaveAgreement_Output', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAgreement';
-- GO
