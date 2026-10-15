-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-05-03
-- Description:	De acuerdo con el tipo de identificación recibido devolver el respectivo en Letras
-- =============================================
CREATE FUNCTION [Common].[GetIdentificationTypeCode]
(	
	@IdentificationType INT,
	@Environment TINYINT
)
RETURNS VARCHAR(2) 
BEGIN

	DECLARE @IdentificationTypeCode VARCHAR(2)

	IF @Environment = 1
	BEGIN
		SELECT @IdentificationTypeCode = CASE @IdentificationType
			WHEN 1 THEN 'CC' 
			WHEN 2 THEN 'CE' 
			WHEN 3 THEN 'TI' 
			WHEN 4 THEN 'RC' 
			WHEN 5 THEN 'PA' 		
			WHEN 6 THEN 'AS' 
			WHEN 7 THEN 'MS' 
			WHEN 8 THEN 'NU' 
			WHEN 9 THEN 'CN' 
			WHEN 10 THEN 'CD' 
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE'
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			ELSE 'SI' 
		END
	END
	ELSE IF @Environment = 2
	BEGIN
		SELECT @IdentificationTypeCode = CASE @IdentificationType
			WHEN 0 THEN 'CC' 
			WHEN 1 THEN 'CE' 
			WHEN 2 THEN 'TI' 
			WHEN 3 THEN 'RC' 
			WHEN 4 THEN 'PA' 		
			WHEN 5 THEN 'AS' 
			WHEN 6 THEN 'MS' 
			WHEN 7 THEN 'NI' 
			WHEN 8 THEN 'NU' 
			WHEN 9 THEN 'CN' 
			WHEN 10 THEN 'CD' 
			WHEN 11 THEN 'SC' 
			WHEN 12 THEN 'PE'
			WHEN 13 THEN 'PT'
			WHEN 14 THEN 'DE'
			ELSE 'SI' 
		END
	END

	 RETURN @IdentificationTypeCode
END