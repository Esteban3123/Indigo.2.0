CREATE PROCEDURE [dbo].[InformeRetirados]
--@AnioRetiro AS Int
--@MesRetiro  AS Int

AS

SELECT 
TP.Nit AS CÉDULA_CIUDADANÍA, TP.NAME NOMBRE_EMPLEADO, BO.Name AS SEDE, pos.Name AS CARGO, CC.Name AS CENTRO_DE_COSTO, C.JobBondingDate AS FECHA_INGRESO, 
RR.Name AS RAZÓN_RETIRO, 
YEAR(C.RetirementDate) AS AÑO_RETIRO,
(SELECT DATENAME (MONTH, DATEADD(MONTH, MONTH(C.RetirementDate)- 1,'1900-01-01')) MES_RETIRO) AS MES_RETIRO,
DAY(C.RetirementDate) AS DÍA_RETIRO,
ET.Name AS TIPO_EMPLEADO
FROM Payroll.Employee e
Inner Join Common.ThirdParty tp ON tp.Id = e.ThirdPartyId
Inner Join Payroll.[Contract] c ON c.EmployeeId = e.Id
Inner Join Payroll.FunctionalUnit fu ON fu.Id = c.FunctionalUnitId
Inner Join Payroll.BranchOffice bo ON bo.Id = fu.BranchOfficeId
Inner Join Payroll.CostCenter cc ON cc.Id = e.CostCenterId
Inner Join Payroll.Position pos ON pos.Id = c.PositionId
Inner Join Payroll.RetirementReason rr ON rr.Id = c.RetirementReasonId
Inner Join Payroll.EmployeeType et ON et.Id = e.EmployeeTypeId
WHERE c.Status IN (2,5) And Valid = 0 --And YEAR(c.RetirementDate) = @AnioRetiro --And MONTH(c.RetirementDate) = @MesRetiro
ORDER BY c.RetirementDate Desc
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe de empleados retirados o desvinculados de la organización. Consolida información de nómina cruzando datos del empleado (cédula, nombre, tipo de vinculación, centro de costo), su contrato (cargo, sede, fecha de ingreso, fecha de retiro y motivo de terminación) para mostrar el listado histórico de personal que ya no está activo. Filtra únicamente los contratos con estado de retiro (estados 2 y 5) y sin vigencia, ordenados del retiro más reciente al más antiguo. Sirve como reporte gerencial y de recursos humanos para analizar rotación de personal, causales de retiro y períodos de desvinculación por año, mes y día.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'InformeRetirados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'InformeRetirados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe de empleados retirados con sus datos personales, contractuales, sede, cargo, centro de costo, motivo y fecha de retiro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen contratos con Status 2 o 5 marcados como retirados; Los contratos tienen referenciados motivo de retiro, cargo, unidad funcional y sucursal; El empleado tiene tercero, centro de costo y tipo de empleado asociados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera contratos con Status 2 o 5 y Valid = 0 (asumidos como estados de retiro vigentes); Descompone la fecha de retiro en año, mes (nombre) y día como columnas separadas; Requiere obligatoriamente joins internos con motivo de retiro, cargo, sucursal, centro de costo y tipo de empleado (excluye registros sin estas referencias); Los resultados se entregan ordenados de retiros más recientes a más antiguos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; contrato laboral; retiro de personal; motivo de retiro; fecha de ingreso; fecha de retiro; sede/sucursal; cargo; centro de costo; tipo de empleado; nómina', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna registros de contratos donde Status IN (2,5) AND Valid = 0, ordenados por RetirementDate descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Contract.Status IN (2,5) AND Valid = 0 → Se incluye el empleado/contrato en el informe de retirados else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.FunctionalUnit; Payroll.BranchOffice; Payroll.CostCenter; Payroll.Position; Payroll.RetirementReason; Payroll.EmployeeType', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeRetirados';
-- GO
