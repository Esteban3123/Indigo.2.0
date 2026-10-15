-- =============================================
-- Author:      Duvan Felipe Chavarro Gutiérrez
-- Create Date: 16 Nov 2023
-- Description: Descripción en escala del dolor según el número del dolor que recibe
-- =============================================
CREATE FUNCTION [dbo].[GetDescriptionPain] (@NumDolor INT)
RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @Descripcion NVARCHAR(100)
	SELECT @Descripcion = 
        CASE @NumDolor
            WHEN 0 THEN '0 puntos - Sin dolor'
            WHEN 1 THEN '1 punto - Dolor suave'
            WHEN 2 THEN '2 puntos - Dolor suave'
            WHEN 3 THEN '3 puntos - Dolor suave'
            WHEN 4 THEN '4 puntos - Dolor moderado'
            WHEN 5 THEN '5 puntos - Dolor moderado'
            WHEN 6 THEN '6 puntos - Dolor moderado'
            WHEN 7 THEN '7 puntos - Dolor intenso'
            WHEN 8 THEN '8 puntos - Dolor intenso'
            WHEN 9 THEN '9 puntos - Dolor intenso'
            WHEN 10 THEN '10 puntos - Dolor intenso'
        END

    RETURN @Descripcion
END