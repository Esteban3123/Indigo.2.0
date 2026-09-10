

	CREATE VIEW [Inventory].[ViewRequestParamAuthUser] AS
WITH CombinedData AS (
    SELECT 
		rua.Id as IdRequestParamAuthUser,
        rp.Id AS IdRequestParam,
		fu.Id As IdType,
        rp.Code AS CodeRequestParam,
        fu.Code AS Code,
        fu.Code + ' - ' + fu.Name AS CodeName,
        'Unidad funcional' AS Type,
		1 as CodeType,
		rua.UserPrincipalFunctionalUnitId AS UserPrincipalId,
        rua.UserAlternateFunctionalUnitId AS UserAlternativeId
    FROM 
        Inventory.RequestParam AS rp
        INNER JOIN Inventory.RequestParamFuncionalUnit AS rpf ON rpf.RequestParamId = rp.Id
        INNER JOIN [Payroll].[FunctionalUnit] AS fu ON fu.Id = rpf.FunctionalUnitId
        LEFT JOIN Inventory.RequestParamAuthUser AS rua ON rp.Id = rua.RequestParamId 
         AND rua.FunctionalUnitId = rpf.FunctionalUnitId

    UNION ALL

    SELECT 
		rua.Id as IdRequestParamAuthUser,
        rp.Id AS IdRequestParam,
	    w.Id As IdType,
        rp.Code AS CodeRequestParam,
        w.Code AS Code,
        w.Code + ' - ' + w.Name AS CodeName,
        'Almacén' AS Type,
		2 as CodeType,
		rua.UserPrincipalWarehouseId AS UserPrincipalId,
        rua.UserAlternateWarehouseId AS UserAlternativeId
    FROM 
        Inventory.RequestParam AS rp
        INNER JOIN Inventory.RequestParamWarehouse AS rpw ON rpw.RequestParamId = rp.Id
        INNER JOIN [Inventory].[Warehouse] AS w ON w.Id = rpw.WarehouseId
        LEFT JOIN Inventory.RequestParamAuthUser AS rua ON rp.Id = rua.RequestParamId
          AND rua.WarehouseId = rpw.WarehouseId
)
SELECT 
    ROW_NUMBER() OVER (ORDER BY idRequestParam, code) AS Id,
	IdRequestParamAuthUser,
    IdRequestParam,
	IdType,
    CodeRequestParam,
    Code,
    CodeName,
    Type,
	CodeType,
    UserPrincipalId,
    UserAlternativeId
FROM CombinedData;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida en un único resultado los usuarios autorizados para realizar solicitudes de inventario, ya sea por unidad funcional (servicio o área) o por almacén/bodega. Combina los parámetros de solicitud con las unidades funcionales habilitadas y las bodegas asociadas, mostrando para cada combinación el usuario principal y el usuario alternativo autorizados. Sirve para administrar y consultar quiénes tienen permiso de generar pedidos o requisiciones de inventario según el área o almacén al que pertenecen, diferenciando el tipo de entidad (unidad funcional vs. almacén) mediante un código de tipo. Es útil para configuración de accesos, auditoría de autorizaciones y parametrización del flujo de solicitudes de materiales y medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamAuthUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamAuthUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en una sola lista los usuarios autorizados (principal y alterno) por parámetro de solicitud de inventario, tanto para unidades funcionales como para almacenes, presentando ambos orígenes con un tipo discriminador.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.RequestParam vinculados a unidades funcionales (RequestParamFuncionalUnit) o a almacenes (RequestParamWarehouse).; Las unidades funcionales referenciadas existen en Payroll.FunctionalUnit y los almacenes en Inventory.Warehouse.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila pertenece exclusivamente a una de dos categorías: ''Unidad funcional'' (CodeType=1) o ''Almacén'' (CodeType=2).; El campo CodeName siempre se compone como ''Code - Name'' del origen correspondiente.; El Id resultante es secuencial generado por ROW_NUMBER ordenado por IdRequestParam y Code, no es persistente.; Si no existe registro de autorización (RequestParamAuthUser) para la combinación, IdRequestParamAuthUser, UserPrincipalId y UserAlternativeId quedan en NULL pero la fila del parámetro-unidad/almacén se conserva.; El emparejamiento de autorizaciones de unidad funcional usa solo FunctionalUnitId (ignora WarehouseId) y el de almacén usa solo WarehouseId (ignora FunctionalUnitId).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Parámetro de solicitud de inventario; Unidad funcional; Almacén/Bodega; Usuario autorizado principal; Usuario autorizado alterno; Autorización de solicitudes', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación parámetro-unidad funcional y parámetro-almacén, con sus usuarios principal y alterno cuando exista autorización en RequestParamAuthUser (LEFT JOIN, por lo que puede ser NULL).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = unidad funcional (Type=''Unidad funcional'', CodeType=1) → Toma UserPrincipalFunctionalUnitId y UserAlternateFunctionalUnitId, emparejando RequestParamAuthUser por FunctionalUnitId. else Origen = almacén (Type=''Almacén'', CodeType=2): toma UserPrincipalWarehouseId y UserAlternateWarehouseId, emparejando por WarehouseId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.RequestParam; Inventory.RequestParamFuncionalUnit; Payroll.FunctionalUnit; Inventory.RequestParamAuthUser; Inventory.RequestParamWarehouse; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamAuthUser';
GO
