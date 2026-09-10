-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 07/11/2019
-- Description:	Devuelve la descripción del tipo de unidad
-- =============================================
CREATE FUNCTION [Billing].[fnGetDescriptionUnitTypeFunctionalUnit]
(
	@UnitType tinyint
)
RETURNS varchar(50)
AS
BEGIN
	declare @DescriptionUnitType varchar(50) = ''
	
	set @DescriptionUnitType = case @UnitType
								   when 1 then 'Urgencias'
								   when 2 then 'Hospitalizacion'
								   when 3 then 'Apoyo Dx'
								   when 4 then 'Apoyo Terapeutico'
								   when 5 then 'Unidades de Cuidado Intensivo Adulto'
								   when 6 then 'Unidades de Cuidado Intermedio Adulto'
								   when 7 then 'Unidades de Cuidado Intensivo Pediatrica'
								   when 8 then 'Unidades de Cuidado Intermedio Pediatrica'
								   when 9 then 'Unidades de Cuidado Intensivo Neonatal'
								   when 10 then 'Unidades de Cuidado Intermedio Neonatal'
								   when 11 then 'Unidades de Cuidado Basico Neonatal'
								   when 12 then 'Unidad Renal'
								   when 13 then 'Unidad Oncologica'
								   when 14 then 'Unidad Medicina Nuclear'
								   when 15 then 'Consulta Externa'
								   when 16 then 'Unidad Mental'
								   when 17 then 'Unidad de Quemados'
								   when 18 then 'Unidad de Cuidado Paliativo'
								   when 19 then 'Cirugia'
								   when 20 then 'Laboratorio'
								   when 21 then 'Cardiologia No Invasiva'
								   when 22 then 'Cardiologia Invasiva'
								   when 23 then 'Gineco-Obstetricia'
								   when 24 then 'Consulta Externa - Gineco-Obstetricia'
								   when 30 then 'Otras'
								   when 31 then 'Consulta Prioritaria'
								   else ''
							   end

	return @DescriptionUnitType
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico de tipo de unidad funcional en su descripción legible en español. Recibe un número entero (por ejemplo: 1 = Urgencias, 2 = Hospitalización, 15 = Consulta Externa, 19 = Cirugía, 20 = Laboratorio, entre otros) y retorna el nombre del servicio o unidad asistencial correspondiente. Se usa en el módulo de facturación para mostrar el nombre del tipo de unidad en reportes, liquidaciones y documentos de cobro, evitando manejar códigos crípticos en la interfaz o en los RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de unidad funcional a su descripción legible para usarse en facturación y reportes clínico-administrativos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de tipos de unidad funcional reconocidos es fijo: 1-24, 30 y 31; cualquier otro valor produce cadena vacía.; Los códigos 25 a 29 no están definidos y se tratan como inválidos.; La descripción retornada nunca excede 50 caracteres (varchar(50)).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Urgencias; Hospitalización; Apoyo diagnóstico; Apoyo terapéutico; Unidad de Cuidado Intensivo (Adulto/Pediátrica/Neonatal); Unidad de Cuidado Intermedio; Unidad de Cuidado Básico Neonatal; Unidad Renal; Unidad Oncológica; Medicina Nuclear; Consulta Externa; Unidad Mental; Unidad de Quemados; Cuidado Paliativo; Cirugía; Laboratorio; Cardiología Invasiva/No Invasiva; Gineco-Obstetricia; Consulta Prioritaria', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve cadena vacía cuando el código no coincide con ningún WHEN del CASE (incluye gaps 25-29 y >31).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UnitType IN (1..24, 30, 31) → Retorna la descripción textual mapeada (Urgencias, Hospitalización, UCI Adulto, etc.) else Retorna cadena vacía ''''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetDescriptionUnitTypeFunctionalUnit';
GO
