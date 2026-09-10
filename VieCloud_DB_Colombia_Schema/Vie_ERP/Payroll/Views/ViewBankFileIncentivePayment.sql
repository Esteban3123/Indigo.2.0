

CREATE VIEW [Payroll].[ViewBankFileIncentivePayment]
AS
		SELECT 
			I.Id Id,
			P.Name Position ,
			P.id PositionId,
			TP.NIT Nit,
			TP.NAME EmployeeName,
			B.Name Bank,
			C.BankAccountNumber BankAccount,
			I.PaidValue PaidValue,
			C.Status Status,
			C.Valid Valid,
			C.Id ContractId,
			C.BankAccountType EmployeeBankTypeAccount,
			I.RegisterStatus RegisterStatus, 
			I.Period [Period], 
			I.PeriodEndDate PeriodEndDate,	
			G.[Name] GroupName,
			G.id GroupId,
			F.[Name] FunctionalUnitName,
			F.id FunctionalUnitId,
			I.TotalAccrued,
			I.TotalDeducted,
			C.BasicSalary,
			E.id EmployeeId,
			B.id BankId,
			0 BankFileStatus,
			1 Process
		from Payroll.IncentivePayment I 
		INNER JOIN  Payroll.[Contract] C ON I.ContractId = C.Id
		INNER JOIN Payroll.Employee E ON C.EmployeeId= E.Id 
		INNER JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
		INNER JOIN Payroll.Position P ON P.Id = C.PositionId
		INNER JOIN Payroll.Bank B ON B.Id = C.BankId
		INNER JOIN Payroll.[Group] G on G.Id = C.GroupId
		INNER JOIN Payroll.FunctionalUnit F ON F.id = C.FunctionalUnitId
		GROUP BY TP.NIT,  
         I.Id,
         P.Name,
         P.id,
         TP.NAME,
         B.Name,
         C.BankAccountNumber,
         I.PaidValue,
         C.Status,
         C.Valid,
         C.Id,
         C.BankAccountType,
         I.RegisterStatus, 
         I.Period, 
         I.PeriodEndDate,	
         G.[Name],
         G.id,
         F.[Name],
         F.id,
         I.TotalAccrued,
         I.TotalDeducted,
         C.BasicSalary,
         E.id,
         B.id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información necesaria para generar el archivo bancario de pago de incentivos y bonificaciones del personal. Integra los pagos de incentivos liquidados con los datos contractuales del empleado (cargo, grupo de nómina, unidad funcional, salario básico), su información bancaria (banco, número y tipo de cuenta) y sus datos de identificación personal (NIT/cédula y nombre). Se utiliza en el proceso de dispersión bancaria de incentivos, permitiendo conocer el valor a pagar, los totales devengados y deducidos, el período liquidado y el estado del registro para cada contrato de empleado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePayment';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los pagos de incentivos liquidados con los datos bancarios y laborales del empleado necesarios para generar el archivo de dispersión bancaria, marcando cada registro como pendiente de proceso.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada pago de incentivo debe tener un ContractId válido en Payroll.Contract.; El contrato debe referenciar un EmployeeId existente en Payroll.Employee.; El empleado debe tener un ThirdPartyId existente en Common.ThirdParty (fuente de NIT y nombre).; El contrato debe tener PositionId, BankId, GroupId y FunctionalUnitId válidos en sus respectivos catálogos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen pagos de incentivo que tengan contrato, empleado, tercero, cargo, banco, grupo y unidad funcional asociados (todos los joins son INNER), excluyendo registros con datos maestros incompletos.; El campo BankFileStatus se entrega siempre con valor fijo 0 (estado inicial de archivo bancario aún no procesado).; El campo Process se entrega siempre con valor fijo 1 (marca de inclusión por defecto en el proceso de generación del archivo bancario).; La identidad del empleado para el archivo bancario se toma del NIT y nombre registrados en Common.ThirdParty, no de Payroll.Employee.; Los datos bancarios del pago (número de cuenta, tipo de cuenta, banco) se obtienen del contrato vigente del empleado, no del maestro de empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'pago de incentivos; nómina; contrato laboral; empleado; cuenta bancaria; banco; cargo; grupo de nómina; unidad funcional; período de liquidación; tercero (NIT); archivo bancario', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewBankFileIncentivePayment: Devuelve una fila por cada Payroll.IncentivePayment cruzada con su contrato, empleado, tercero, cargo, banco, grupo y unidad funcional, agregando BankFileStatus=0 y Process=1 como marcas para el proceso de generación de archivo bancario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Position; Payroll.Bank; Payroll.Group; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePayment';
GO
