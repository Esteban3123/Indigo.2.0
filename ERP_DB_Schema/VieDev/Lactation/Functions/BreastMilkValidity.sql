CREATE FUNCTION [Lactation].[BreastMilkValidity]
(
    @ID INT
)
RETURNS INT
AS
BEGIN
	-- Valida la vigencia de las leches maternas almacenadas 
	-- 1) verde
	-- 2) Naranja
	-- 3) Rojo
    DECLARE @result INT;

    SELECT @result = IIF(DateADD(DAY, C.ExpiredQuantity,A.ExtractionDate) <= Common.GETDATE(),3,IIF(DATEADD(DAY, C.ValidQuantity - C.WarningQuantity, A.ExtractionDate) <= Common.GETDATE(), 2, 1)) 
	FROM Lactation.BreastMilkIntakeRecords A
	INNER JOIN Lactation.ParametersCareCenters B ON A.CODCENATE = B.CenterCode
	INNER JOIN Lactation.ParametersBreastMilkValidity C ON B.ParametersConfigurationId = C.ParametersConfigurationId AND A.StorageType = C.MilkTypeCode
	WHERE A.Id = @ID

	SET @result = IIF(@result IS NULL,0,@result) 

    RETURN @result;
END