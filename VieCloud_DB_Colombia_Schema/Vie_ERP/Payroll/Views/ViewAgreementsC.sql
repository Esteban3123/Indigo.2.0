
CREATE VIEW [Payroll].[ViewAgreementsC]
AS
	SELECT	a.Id
		,a.Consecutive
		,a.GroupId
		,g.Code GroupCode
		,g.Name GroupName
		,a.EmployeeId
		,a.CompanyId		
		,cp.Nit CompanyNit
		,cp.Name CompanyName
		,a.ConceptId
		,con.Code AS ConceptCode
		,con.Name AS ConceptName
		,a.KindsAgreementsId
		,a.Comments
		,a.LiquidationType
		,a.TermType
		,a.AgreementValue
		,a.NumberShares
		,a.[State]
		,a.StartingDate
		,a.CurrentBalance
		,a.EndDateSuspend
		,a.CommentChangeState
		,a.PaidVacation
		,a.[TimeStamp]
		,bo.Id AS BranchOfficeID
		,bo.Code AS BranchOfficeCode
		,bo.[Name] AS BranchOffice
	FROM Payroll.AgreementsC AS a WITH (NOLOCK)
	JOIN Payroll.Employee AS e WITH (NOLOCK) ON a.EmployeeId = e.Id 
	JOIN Payroll.Company AS cp WITH (NOLOCK) ON a.CompanyId = cp.Id
	LEFT JOIN Payroll.Concept AS con WITH (NOLOCK) ON a.ConceptId = con.Id
	LEFT JOIN Payroll.[Group] g WITH (NOLOCK) ON a.GroupId = g.Id
	LEFT JOIN Payroll.Contract AS c WITH (NOLOCK) ON e.Id = c.EmployeeId AND c.Status = 1 AND c.Valid = 1
	LEFT JOIN Payroll.FunctionalUnit AS fu WITH (NOLOCK) ON c.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.BranchOffice AS bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de acuerdos de descuento o libranza de nómina por empleado, que consolida en una sola consulta los convenios pactados con cada trabajador (préstamos, embargos, cuotas sindicales, entre otros) junto con su valor, número de cuotas, saldo pendiente y estado. Integra información de la empresa empleadora (NIT y nombre), el grupo de nómina al que pertenece el empleado, y la sede o sucursal asociada al contrato laboral vigente. Sirve para reportería y consulta operativa de los acuerdos de pago activos o suspendidos dentro del módulo de nómina, permitiendo identificar rápidamente a qué compañía, grupo, unidad funcional y sede corresponde cada acuerdo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewAgreementsC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewAgreementsC';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista consolidada de acuerdos/libranzas de nómina enriquecidos con datos del grupo, empresa y sucursal derivada del contrato activo del empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas base de Payroll (AgreementsC, Employee, Company, Group, Contract, FunctionalUnit, BranchOffice) deben estar pobladas y referencialmente consistentes para que las uniones resuelvan correctamente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera el contrato del empleado con Status = 1 (contrato activo) para derivar la sucursal asociada al acuerdo.; Cada acuerdo expuesto requiere obligatoriamente empleado y empresa existentes (JOIN INNER sobre Employee y Company); acuerdos huérfanos no aparecen.; El grupo de nómina, la unidad funcional y la sucursal son opcionales: si no existen, las columnas correspondientes quedan en NULL (LEFT JOIN).; La sucursal del acuerdo se obtiene indirectamente vía Contrato activo → Unidad Funcional → Sucursal, no directamente desde el acuerdo.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'acuerdos de nómina; libranza/descuento; empleado; empresa empleadora; grupo de nómina; contrato laboral activo; unidad funcional; sucursal/sede', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.AgreementsC: Devuelve una fila por cada acuerdo en Payroll.AgreementsC con su empleado y empresa existentes, agregando datos de grupo, y resolviendo la sucursal a través del contrato con Status=1 → unidad funcional → sucursal.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.AgreementsC; Payroll.Employee; Payroll.Company; Payroll.Group; Payroll.Contract; Payroll.FunctionalUnit; Payroll.BranchOffice', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewAgreementsC';
GO
