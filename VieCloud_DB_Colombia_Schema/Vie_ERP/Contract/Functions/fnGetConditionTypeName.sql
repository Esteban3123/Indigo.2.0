

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 05/08/2020
-- Description:	Función para obtener el nombre del tipo de condición de la definición de tarifas
-- =============================================
CREATE FUNCTION [Contract].[fnGetConditionTypeName]
(
	@ConditionType tinyint
)
RETURNS varchar (50)
AS
BEGIN
	declare @ConditionTypeName varchar(50) = case @ConditionType
												when 1 then 'Horario'
												when 2 then 'Especialidad'
												when 3 then 'Unidad Funcional'
												when 4 then 'Tipo de Unidad'
												when 5 then 'Ninguna'
												when 6 then 'RIAS'
												when 7 then 'Descripción'
											end 

	RETURN @ConditionTypeName

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un código numérico de tipo de condición tarifaria en su nombre legible en español. Recibe un número del 1 al 7 y devuelve el nombre correspondiente: Horario, Especialidad, Unidad Funcional, Tipo de Unidad, Ninguna, RIAS o Descripción. Se usa en la gestión de contratos y definición de tarifas para mostrar al usuario la descripción del tipo de condición aplicada a una tarifa, en lugar del código interno.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'fnGetConditionTypeName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'FUNCTION', @level1name = N'fnGetConditionTypeName';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce el código numérico del tipo de condición de definición de tarifas a su nombre legible.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reconocen siete tipos de condición válidos (1 a 7); valores fuera de ese dominio resultan en NULL.; El nombre devuelto nunca excede 50 caracteres.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Definición de tarifas; Tipo de condición; Horario; Especialidad; Unidad Funcional; Tipo de Unidad; RIAS', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Devuelve el nombre del tipo de condición según el código: 1=''Horario'', 2=''Especialidad'', 3=''Unidad Funcional'', 4=''Tipo de Unidad'', 5=''Ninguna'', 6=''RIAS'', 7=''Descripción''; cualquier otro valor retorna NULL.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ConditionType = 1 → Retorna ''Horario''; si @ConditionType = 2 → Retorna ''Especialidad''; si @ConditionType = 3 → Retorna ''Unidad Funcional''; si @ConditionType = 4 → Retorna ''Tipo de Unidad''; si @ConditionType = 5 → Retorna ''Ninguna''; si @ConditionType = 6 → Retorna ''RIAS''; si @ConditionType = 7 → Retorna ''Descripción'' else Retorna NULL', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'FUNCTION', @level1name=N'fnGetConditionTypeName';
GO
