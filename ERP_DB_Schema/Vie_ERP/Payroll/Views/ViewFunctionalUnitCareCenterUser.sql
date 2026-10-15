


CREATE VIEW [Payroll].[ViewFunctionalUnitCareCenterUser]
AS
SELECT fuu.Id AS FunctionalUnitUserId,
       fu.Id,
       fu.Code AS Codigo,
       fu.Name AS Descripcion,
       fu.Code + ' - ' + fu.Name AS CodeDescription,
       fu.UnitType,
       fuc.CODCENATE,
       fuu.UserCode
FROM dbo.INCENUNFU AS fuc
    INNER JOIN Payroll.FunctionalUnit AS fu
        ON fuc.UFUCODIGO = fu.Code
    INNER JOIN Payroll.FunctionalUnitUser AS fuu
        ON fu.Id = fuu.FunctionalUnitId
WHERE fu.State = 1;