
CREATE view [Inventory].[ViewRequestParamFunctionalUnit]
as
select 
	fu.Id
	, fu.Id as FunctionalUnitId
	, fu.Code as FunctionalUnitCode
	, fu.[Name] as FunctionalUnitName
	, rf.Id as RequestParamFuncionalUnitId
	, rp.Id as RequestParamId
	, cast(case when rf.Id is null then 0 else 1 end as bit) as IsSelected
	, rp.RequiredAuthorization
from Payroll.FunctionalUnit fu
left join Inventory.RequestParamFuncionalUnit rf on rf.FunctionalUnitId = fu.Id
left join Inventory.RequestParam rp on rf.RequestParamId = rp.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que muestra todas las unidades funcionales (áreas, servicios o departamentos) junto con su vinculación a los parámetros de solicitud de inventario. Para cada unidad funcional indica si está seleccionada o asociada a un parámetro de solicitud (IsSelected), el identificador de dicho parámetro y si requiere autorización para realizar pedidos. Combina las unidades funcionales de nómina con la tabla de relación de parámetros de inventario por unidad funcional, permitiendo visualizar de forma consolidada qué servicios o áreas tienen configuración activa de solicitud de inventario y cuáles aún no. Sirve para la configuración y consulta de permisos de solicitud de inventario por unidad funcional en pantallas administrativas y reportes de parametrización.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamFunctionalUnit';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para cada unidad funcional, si está vinculada a un parámetro de solicitud de inventario y si éste requiere autorización, marcando su estado de selección.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se listan todas las unidades funcionales existentes, estén o no asociadas a un parámetro de solicitud (LEFT JOIN desde FunctionalUnit).; El indicador IsSelected siempre es bit (0/1) y refleja si existe vínculo en RequestParamFuncionalUnit.; Los datos del parámetro de solicitud (RequestParamId, RequiredAuthorization) solo aparecen cuando hay vínculo; en caso contrario son NULL.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Parámetro de solicitud de inventario; Autorización requerida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por cada unidad funcional, con IsSelected=1 cuando existe relación en RequestParamFuncionalUnit y 0 cuando no.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rf.Id IS NULL (no existe vínculo entre la unidad funcional y un parámetro de solicitud) → IsSelected = 0 (no seleccionada) else IsSelected = 1 (seleccionada)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.FunctionalUnit; Inventory.RequestParamFuncionalUnit; Inventory.RequestParam', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamFunctionalUnit';
GO
