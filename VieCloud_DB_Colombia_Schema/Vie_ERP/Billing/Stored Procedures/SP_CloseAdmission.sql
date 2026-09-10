-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 2015-11-26
-- Description:	Procedimiento ejecutado para cerrar un ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_CloseAdmission]
	@AdmissionNumber varchar(20),
	@ContainerNameCrystal varchar(50),
	@CodeUser AS VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult BIT,
			@MessageResult VARCHAR(MAX)

	EXEC [Billing].[SP_CloseAdmission_Output] 
		@AdmissionNumber, 
		@ContainerNameCrystal, 
		@CodeUser,
		--------------------------------------------
		@StatusResult OUTPUT, 
		@MessageResult OUTPUT

	SELECT	@StatusResult AS StatusResult,
			@MessageResult AS MessageResult	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cierra un ingreso o admisión hospitalaria en el módulo de facturación. Recibe el número de admisión, el nombre del contenedor Crystal Reports para la impresión del documento de cierre, y el código del usuario que ejecuta la acción. Delega la lógica principal al procedimiento SP_CloseAdmission_Output y retorna un indicador de éxito o fallo junto con un mensaje descriptivo del resultado. Se usa para finalizar el proceso de egreso del paciente desde el punto de vista administrativo y de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmission';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmission';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el cierre de un ingreso al procedimiento de salida y expone el estado y mensaje del resultado como result set en lugar de parámetros OUTPUT.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Billing.SP_CloseAdmission_Output que soporta los parámetros OUTPUT de estado y mensaje.; Se requiere identificador de admisión, contenedor de Crystal y código de usuario para delegar la operación de cierre.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un result set con dos columnas: StatusResult y MessageResult, capturadas desde los OUTPUT del procedimiento delegado.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso (admisión); Cierre de ingreso; Usuario operador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras EXEC SP_CloseAdmission_Output, hace SELECT @StatusResult AS StatusResult, @MessageResult AS MessageResult devolviendo un único renglón con el resultado del cierre.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_CloseAdmission_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission';
-- GO
