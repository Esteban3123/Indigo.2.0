-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 2015-11-26
-- Description:	Procedimiento ejecutado para cerrar un ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_CloseAdmission_Output]
	@AdmissionNumber VARCHAR(20),
	@ContainerNameCrystal VARCHAR(50),
	@CodeUser AS VARCHAR(20),
	@StatusResult BIT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		
		/***********************************************  VALIDACIONES ***********************************************/

		EXEC [Billing].[SP_CloseAdmissionValidations_Output] @AdmissionNumber, @StatusResult OUT, @MessageResult OUT

		IF @StatusResult = 0
		BEGIN
			RETURN
		END

		/*********************************************** ACTUALIZACION ***********************************************/

		UPDATE ing 
			SET IESTADOIN = IIF(rcd.Id IS NULL, 'C', 'F'),
				CODUSUMOD = @CodeUser,
				FECREGMOD = Common.GETDATE(),
				IJUSTIFIC = 'El ingreso fue cerrado por el usuario ' + @CodeUser                                                                                                                                                                                                                   
		FROM [dbo].[ADINGRESO] ing
		LEFT JOIN Billing.RevenueControl rc ON ing.NUMINGRES = rc.AdmissionNumber
		LEFT JOIN Billing.RevenueControlDetail rcd ON rc.Id = rcd.RevenueControlId
		WHERE ing.NUMINGRES = @AdmissionNumber

		SELECT	@StatusResult = 1, 
				@MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT	@StatusResult = 0, 
				@MessageResult = 'SP_CloseAdmission_Output: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cierra un ingreso o admisión de paciente en el sistema de facturación. Primero ejecuta validaciones previas al cierre mediante SP_CloseAdmissionValidations_Output; si las validaciones son exitosas, actualiza el estado del ingreso en ADINGRESO: lo marca como ''Cerrado'' (C) si no tiene folios de facturación detallados asociados en RevenueControlDetail, o como ''Facturado'' (F) si los tiene, registrando además el usuario que realizó el cierre y la fecha de modificación. Retorna indicadores de éxito o mensaje de error según el resultado de la operación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmission_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmission_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Cierra un ingreso aplicando la marca de estado correspondiente según existan o no folios de facturación detallados, registrando usuario y fecha de modificación, previa validación de reglas de cierre.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso en dbo.ADINGRESO identificado por NUMINGRES; Las validaciones de Billing.SP_CloseAdmissionValidations_Output deben retornar @StatusResult = 1 para continuar', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado del ingreso solo se actualiza si las validaciones previas son exitosas (@StatusResult <> 0); El estado final es ''C'' cuando no existen detalles de folios asociados, y ''F'' cuando sí existen; Toda modificación queda trazada con el usuario (CODUSUMOD), la fecha (FECREGMOD = Common.GETDATE()) y una justificación que indica que el ingreso fue cerrado por el usuario; Cualquier excepción captura el error y lo retorna en @MessageResult con el prefijo ''SP_CloseAdmission_Output:'' junto con la línea del error, fijando @StatusResult = 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión de paciente; Cierre de ingreso; Facturación; Folios de facturación; Control de ingresos (RevenueControl)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.ADINGRESO: Cuando NUMINGRES = @AdmissionNumber y las validaciones pasan, fija IESTADOIN = ''C'' si no hay RevenueControlDetail asociado al ingreso, o ''F'' si lo hay; además asigna CODUSUMOD = @CodeUser, FECREGMOD = Common.GETDATE() e IJUSTIFIC con texto que incluye al usuario que cerró el ingreso; [RETURN_RESULT] (output): En éxito retorna @StatusResult = 1 y @MessageResult = ''''; en error captura ERROR_MESSAGE/ERROR_LINE y retorna @StatusResult = 0 con mensaje prefijado ''SP_CloseAdmission_Output:''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Las validaciones previas devuelven @StatusResult = 0 → Termina la ejecución con RETURN sin aplicar la actualización else Procede a actualizar el estado del ingreso en ADINGRESO; si El ingreso no tiene registros asociados en RevenueControlDetail (rcd.Id IS NULL) vía RevenueControl → Marca el ingreso con IESTADOIN = ''C'' (Cerrado) else Marca el ingreso con IESTADOIN = ''F'' (Facturado)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_CloseAdmissionValidations_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Billing.RevenueControl; Billing.RevenueControlDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmission_Output';
-- GO
