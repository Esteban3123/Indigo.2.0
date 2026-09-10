-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-05-15
-- Description:	Recalcula los valores de un folio
-- =============================================
CREATE PROCEDURE [Billing].[SP_UpdateRevenueControlDetailValuesNew]
	-- Add the parameters for the stored procedure here
	@RevenueControlDetailId AS INT,
	@OperativeUnitId AS INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		DECLARE @CareGroupId INT,
				@PatientGenus INT,
				@PatientBirth DATE,
				@ThirdPartyPatientId INT,
				@HealthAdministratorId INT,
				@Message VARCHAR(MAX)

		DECLARE @ChangeRateServices_Result TABLE
		(
			StatusResult BIT,
			MessageResult VARCHAR(MAX),
			Message VARCHAR(MAX),
			ObjectEmbbeded XML,
			ObjectEmbbededAux XML
		)

		-----------------------------------------------------------------------------

		SELECT	@CareGroupId = rcd.CareGroupId,
				@PatientGenus = pat.IPSEXOPAC,
				@PatientBirth = pat.IPFECNACI,
				@ThirdPartyPatientId = tp.Id,
				@HealthAdministratorId = rcd.HealthAdministratorId
		FROM Billing.RevenueControlDetail rcd
		JOIN Billing.RevenueControl rc ON rcd.RevenueControlId = rc.Id
		LEFT JOIN dbo.INPACIENT pat ON rc.PatientCode = pat.IPCODPACI
		LEFT JOIN Common.ThirdParty tp ON rc.PatientCode = tp.Nit
		WHERE rcd.Id = @RevenueControlDetailId

		INSERT INTO @ChangeRateServices_Result
			EXEC [Billing].[SP_ChangeRateServices] 
				@RevenueControlDetailId,
				@CareGroupId,
				@PatientGenus,
				@PatientBirth,
				NULL,
				1,
				@ThirdPartyPatientId,
				@HealthAdministratorId,
				NULL

		-----------------------------------------------------------------------------
	
		IF NOT EXISTS (SELECT 1 FROM @ChangeRateServices_Result WHERE StatusResult = 1)
		BEGIN
			SELECT @Message = concat(Message,' - ',MessageResult)
			FROM @ChangeRateServices_Result

			Select CONVERT(BIT, 0) As StatusResult, ISNULL(@Message, 'No se encontró un resultado') As MessageResult
		END
		ELSE
		BEGIN
			Select CONVERT(BIT, 1) As StatusResult, 'Los valores del folio se calcularon exitosamente.' As MessageResult
		END
	END TRY
	BEGIN CATCH
		SELECT CONVERT(BIT, 0) As StatusResult, ERROR_MESSAGE() As MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recalcula los valores económicos (tarifas, cuotas, copagos) de un folio de facturación específico identificado por su ID de detalle de control de ingresos. Para ello, obtiene datos del paciente (sexo y fecha de nacimiento) desde el registro maestro de pacientes, su identificación como tercero en el sistema, y el grupo de atención y administrador de salud desde el detalle del folio; luego delega el cálculo de tarifas al procedimiento SP_ChangeRateServices con esos parámetros. Retorna un indicador de éxito o fracaso junto con un mensaje descriptivo del resultado, permitiendo así actualizar las tarifas de un folio cuando cambian condiciones del paciente, la entidad o el contrato.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recalcula los valores tarifarios de un detalle de folio delegando en SP_ChangeRateServices con datos del paciente y la entidad responsable, devolviendo un indicador de éxito y mensaje.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Billing.RevenueControlDetail con Id igual al detalle indicado, asociado a un Billing.RevenueControl válido; El paciente referenciado por RevenueControl.PatientCode puede existir o no en dbo.INPACIENT y/o Common.ThirdParty (joins LEFT, no obligatorios)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre devuelve un resultset con columnas StatusResult (BIT) y MessageResult (VARCHAR), tanto en éxito como en fallo o excepción; El recálculo se ejecuta siempre con flag fijo=1 y parámetros 5 y 9 en NULL al invocar SP_ChangeRateServices; Los datos del paciente (género, fecha de nacimiento) y tercero se obtienen del cabezote RevenueControl, no directamente del detalle', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'folio de facturación; control de ingresos; paciente; género del paciente; fecha de nacimiento; tercero responsable; administrador de salud; grupo de atención; recálculo de tarifas', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.SP_ChangeRateServices: Invoca SP_ChangeRateServices pasando datos del detalle, género y fecha de nacimiento del paciente, tercero y administrador de salud para recalcular tarifas del folio; [RETURN_RESULT] resultset: Si ningún registro retornado por SP_ChangeRateServices tiene StatusResult=1, devuelve StatusResult=0 con mensaje concatenado de Message y MessageResult, o ''No se encontró un resultado'' si es nulo; [RETURN_RESULT] resultset: Si existe al menos un resultado con StatusResult=1, devuelve StatusResult=1 con mensaje ''Los valores del folio se calcularon exitosamente.''; [RETURN_RESULT] resultset: Ante cualquier excepción capturada, devuelve StatusResult=0 con el ERROR_MESSAGE() del CATCH', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si NOT EXISTS resultados con StatusResult=1 en la tabla de resultados de SP_ChangeRateServices → Retorna StatusResult=0 con mensaje concatenado de errores devueltos por SP_ChangeRateServices else Retorna StatusResult=1 con mensaje de éxito de recálculo del folio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_ChangeRateServices', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; dbo.INPACIENT; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateRevenueControlDetailValuesNew';
-- GO
