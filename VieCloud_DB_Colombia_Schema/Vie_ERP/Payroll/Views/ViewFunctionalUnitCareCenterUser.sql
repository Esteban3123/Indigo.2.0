

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que combina las unidades funcionales de nómina activas con los centros de atención a los que pertenecen y los usuarios del sistema asignados a cada una. Integra la relación entre centros de atención y unidades funcionales (áreas o servicios), la configuración de unidades funcionales de nómina y la asignación de usuarios a dichas unidades. Se usa para saber qué unidades funcionales tiene habilitadas un usuario específico dentro de cada centro de atención, facilitando el control de acceso y la asignación de turnos o costos laborales por área y sede.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewFunctionalUnitCareCenterUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewFunctionalUnitCareCenterUser';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la relación entre usuarios, unidades funcionales activas y centros de atención, para resolver qué unidades funcionales y centros tiene asignado cada usuario en nómina.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las unidades funcionales deben tener correspondencia por código entre dbo.INCENUNFU.UFUCODIGO y Payroll.FunctionalUnit.Code; Debe existir asignación de usuario en Payroll.FunctionalUnitUser para que la unidad funcional aparezca', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca expone unidades funcionales con State distinto de 1 (inactivas); Solo retorna combinaciones donde la unidad funcional esté simultáneamente vinculada a un centro de atención (INCENUNFU) y a un usuario (FunctionalUnitUser); usa INNER JOIN en ambas relaciones; Construye una descripción concatenada ''Código - Nombre'' como etiqueta combinada de la unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Centro de atención; Usuario de nómina; Asignación usuario-unidad funcional', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si fu.State = 1 → Solo se incluyen unidades funcionales activas; las inactivas quedan excluidas del resultado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCENUNFU; Payroll.FunctionalUnit; Payroll.FunctionalUnitUser', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewFunctionalUnitCareCenterUser';
GO
