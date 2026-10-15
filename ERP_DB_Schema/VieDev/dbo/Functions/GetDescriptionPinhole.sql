-- =============================================
-- Author:      Duvan Felipe Chavarro Gutiérrez
-- Create Date: 10 Ene 2024
-- Description: Descripción de Pinhole según valor que recibe
-- =============================================
CREATE FUNCTION [dbo].[GetDescriptionPinhole] (@PinholeValue int)
RETURNS NVARCHAR(50)
AS
BEGIN

	DECLARE @DescripcionPinhole NVARCHAR(50)
	SELECT @DescripcionPinhole = 
        CASE @PinholeValue
            WHEN 1 THEN '20/10'
            WHEN 2 THEN '20/15'
            WHEN 3 THEN '20/20'
            WHEN 4 THEN '20/25'
            WHEN 5 THEN '20/30'
            WHEN 6 THEN '20/40'
            WHEN 7 THEN '20/50'
            WHEN 8 THEN '20/60'
            WHEN 9 THEN '20/70'
            WHEN 10 THEN '20/80'
			WHEN 11 THEN '20/100'
            WHEN 12 THEN '20/150'
			WHEN 13 THEN '20/200'
			WHEN 14 THEN '20/400'
			WHEN 15 THEN '20/800'
			WHEN 16 THEN 'Cuenta dedos a 5 m'
			WHEN 17 THEN 'Cuenta dedos a 4 m'
			WHEN 18 THEN 'Cuenta dedos a 3 m'
			WHEN 19 THEN 'Cuenta dedos a 2 m'
			WHEN 20 THEN 'Cuenta dedos a 1 m'
			WHEN 21 THEN 'Cuenta dedos a 50 cm'
			WHEN 22 THEN 'Movimiento de mano'
			WHEN 23 THEN 'Percibe luz'
			WHEN 24 THEN 'No percibe luz'
			WHEN 25 THEN 'Centra - Sigue - Mantiene'
			WHEN 26 THEN 'Centra - Sigue - No mantiene'
			WHEN 27 THEN 'Centra - No sigue - No mantiene'
			WHEN 28 THEN 'No centra - No sigue - No mantiene'
			WHEN 29 THEN 'Rechaza luz'
        END
    RETURN @DescripcionPinhole
END