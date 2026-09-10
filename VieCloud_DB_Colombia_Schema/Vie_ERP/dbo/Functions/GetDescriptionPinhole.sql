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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un código numérico en su descripción textual de agudeza visual medida con test de Pinhole (estenopeico), utilizado en evaluaciones oftalmológicas. Recibe un valor entero del 1 al 29 y retorna la escala de visión correspondiente, que puede ser una fracción de Snellen (ej: 20/20), un nivel de visión funcional (cuenta dedos a determinada distancia, movimiento de mano, percepción de luz) o un patrón de seguimiento visual pediátrico (centra, sigue, mantiene). Se usa en la historia clínica oftalmológica para registrar y mostrar en forma legible el resultado del examen de agudeza visual con oclusor estenopeico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetDescriptionPinhole';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetDescriptionPinhole';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de medición de pinhole a su descripción textual estandarizada de agudeza visual (escala Snellen, conteo de dedos, percepción de luz o fijación).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de entrada debe ser un entero correspondiente a un código de agudeza visual válido (1-29) para obtener descripción; otros valores retornarán NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es una cadena de máximo 50 caracteres o NULL; Los códigos válidos son enteros del 1 al 29; cualquier otro valor produce NULL; Cada código numérico mapea a una única descripción de agudeza visual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pinhole; Agudeza visual; Escala Snellen; Cuenta dedos; Percepción de luz; Fijación visual (Centra-Sigue-Mantiene)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Según el valor entero recibido (1-29), retorna la descripción textual asociada de agudeza visual; si no coincide, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor entre 1 y 15 → Retorna escala de agudeza visual tipo Snellen (20/10 hasta 20/800); si Valor entre 16 y 21 → Retorna descripción de conteo de dedos a distintas distancias (5 m a 50 cm); si Valor entre 22 y 24 → Retorna percepción visual cualitativa (movimiento de mano, percibe luz, no percibe luz); si Valor entre 25 y 28 → Retorna evaluación de fijación visual pediátrica (Centra/Sigue/Mantiene); si Valor = 29 → Retorna ''Rechaza luz'' else Si el valor no está entre 1 y 29, retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetDescriptionPinhole';
GO
