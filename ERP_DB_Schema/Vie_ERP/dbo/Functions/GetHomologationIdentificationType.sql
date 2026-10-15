-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-06-19
-- Description:	Homologa el tipo de documento de crystal de acuerdo a los tipos de documento de VIE
-- =============================================
CREATE FUNCTION [dbo].[GetHomologationIdentificationType]
(	
	@IdentificationTypeHIS INT
)
RETURNS INT
BEGIN

	DECLARE @IdentificationType INT

	SELECT @IdentificationType = CASE @IdentificationTypeHIS
		WHEN 1 THEN 0
		WHEN 2 THEN 1
		WHEN 3 THEN 2
		WHEN 4 THEN 3
		WHEN 5 THEN 4
		WHEN 6 THEN 5
		WHEN 7 THEN 6
		WHEN 8 THEN 8
		WHEN 9 THEN 9
		WHEN 10 THEN 10
		WHEN 11 THEN 11
		WHEN 12 THEN 12
		WHEN 13 THEN 13
		WHEN 14 THEN 14
		WHEN 15 THEN 15
		WHEN 16 THEN 16
		WHEN 17 THEN 17
		WHEN 18 THEN 18
		WHEN 19 THEN 19
		WHEN 20 THEN 20
		ELSE 0
	END

	 RETURN @IdentificationType
END