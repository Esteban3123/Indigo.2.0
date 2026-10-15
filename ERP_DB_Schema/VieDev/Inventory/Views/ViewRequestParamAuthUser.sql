



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