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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un número de la escala del dolor (del 0 al 10) en su descripción textual clínica, según los niveles: sin dolor, dolor suave (1-3), dolor moderado (4-6) y dolor intenso (7-10). Se utiliza en la historia clínica del paciente para mostrar de forma legible el puntaje de dolor registrado durante la evaluación o seguimiento clínico. Recibe como parámetro el valor numérico del dolor (@NumDolor) y retorna la etiqueta descriptiva correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetDescriptionPain';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetDescriptionPain';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un valor numérico de la escala de dolor (0-10) a una descripción textual que clasifica la intensidad del dolor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El valor numérico recibido debe estar entre 0 y 10; valores fuera de ese rango retornan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La escala de dolor solo reconoce valores enteros de 0 a 10; Cada nivel numérico se mapea siempre a una de cuatro categorías: sin dolor, suave, moderado o intenso; El texto de salida siempre incluye el número de puntos seguido de la categoría descriptiva', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Escala de dolor; Clasificación de intensidad del dolor (sin dolor, suave, moderado, intenso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor = 0 → Clasifica como ''Sin dolor''; si Valor entre 1 y 3 → Clasifica como ''Dolor suave''; si Valor entre 4 y 6 → Clasifica como ''Dolor moderado''; si Valor entre 7 y 10 → Clasifica como ''Dolor intenso'' else Si el valor no está en 0-10, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPain';
GO
