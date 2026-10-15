-- =============================================
-- Author:      
-- Create Date: 
-- Description: 
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_UpdateSafeStatus]
(
    @Id Int,
	@SafeStatus TINYINT,
	@CodeUser VARCHAR(20)
)
AS
BEGIN
	UPDATE MedicalHistory.ProductSusceptibleMixingStation
		SET	SafeStatus = @SafeStatus
	WHERE Id = @Id		
END