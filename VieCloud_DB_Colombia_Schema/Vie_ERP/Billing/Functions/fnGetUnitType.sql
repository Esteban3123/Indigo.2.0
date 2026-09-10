-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 03-10-2017
-- Description:	Devuelve la categoria del tipo de unidad
-- =============================================
CREATE FUNCTION [Billing].[fnGetUnitType]
(
	@UnitType tinyint
)
RETURNS int
AS
BEGIN
	declare @UnitTypeFilter int
	if @UnitType = 1 or @UnitType = 23 begin
		set @UnitTypeFilter = 1 --1 - Urgencias
	end
	else if @UnitType = 2 or @UnitType = 5 or @UnitType = 6 or @UnitType = 7 or @UnitType = 8 or @UnitType = 9 or @UnitType = 10 or @UnitType = 11 or @UnitType = 16 or @UnitType = 17 or @UnitType = 18 begin
		set @UnitTypeFilter = 2 --2 - Hospitalizacion
	end
	else if @UnitType = 19 begin
		set @UnitTypeFilter = 3 --3 - Quirofanos
	end
	else begin
		set @UnitTypeFilter = 4 --4 - Servicios Ambulatorios
	end
	return @UnitTypeFilter
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasifica un tipo de unidad funcional en una de cuatro categorías de atención: Urgencias (tipos 1 y 23), Hospitalización (tipos 2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18), Quirófanos (tipo 19) o Servicios Ambulatorios (cualquier otro tipo). Recibe el código numérico del tipo de unidad y devuelve el identificador de categoría correspondiente. Se usa en el módulo de Facturación para agrupar y filtrar atenciones según la modalidad de prestación del servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetUnitType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'FUNCTION', @level1name = N'fnGetUnitType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica un tipo de unidad operativa en una de cuatro categorías de servicio asistencial: Urgencias, Hospitalización, Quirófanos o Servicios Ambulatorios.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un valor entre 1 y 4, clasificando el tipo de unidad en una de cuatro categorías asistenciales.; Cualquier tipo de unidad no contemplado explícitamente cae por defecto en la categoría 4 (Servicios Ambulatorios).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Urgencias; Hospitalización; Quirófanos; Servicios Ambulatorios; Tipo de unidad', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Mapea códigos de tipo de unidad a categoría: {1,23}→1 Urgencias; {2,5,6,7,8,9,10,11,16,17,18}→2 Hospitalización; {19}→3 Quirófanos; resto→4 Servicios Ambulatorios.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @UnitType IN (1, 23) → Retorna 1 (Urgencias); si @UnitType IN (2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 18) → Retorna 2 (Hospitalización); si @UnitType = 19 → Retorna 3 (Quirófanos) else Cualquier otro valor retorna 4 (Servicios Ambulatorios)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'FUNCTION', @level1name=N'fnGetUnitType';
GO
