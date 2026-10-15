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