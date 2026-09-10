

CREATE VIEW [Payroll].[ViewBankFileIncentivePaymentWithoutConfirmated]
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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los pagos de incentivos y bonificaciones de empleados que aún no han sido confirmados para la generación del archivo bancario de dispersión de nómina. Integra información del pago de incentivos (valor pagado, período, totales devengados y deducidos) con los datos del contrato laboral (número de cuenta bancaria, tipo de cuenta, salario básico, estado y vigencia), el cargo del empleado, el banco receptor, el grupo de nómina y la unidad funcional a la que pertenece cada trabajador. Combina los registros de IncentivePayment, Contract, Employee, ThirdParty, Position, Bank, Group y FunctionalUnit para ofrecer una vista completa del empleado y su cuenta destino, permitiendo al área de nómina identificar los pagos de incentivos pendientes de confirmar antes de enviar el archivo al banco.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los pagos de incentivos de nómina pendientes de confirmación bancaria, consolidando datos del empleado, contrato, cargo, banco, grupo y unidad funcional para generar el archivo plano de pago.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada IncentivePayment debe tener un Contract válido y éste debe referenciar Employee, ThirdParty, Position, Bank, Group y FunctionalUnit existentes; de lo contrario el registro queda excluido.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen pagos de incentivo que tengan contrato, empleado, tercero, cargo, banco, grupo y unidad funcional asociados (todos los JOIN son INNER), excluyendo registros sin estas relaciones completas.; Todos los registros se proyectan con BankFileStatus=0 y Process=1 fijos, indicando que aún no han sido confirmados/procesados en el archivo bancario.; Los datos bancarios para el pago provienen del contrato (BankAccountNumber, BankAccountType, BankId), no del empleado directamente.; La identificación del empleado (NIT y nombre) se toma del tercero asociado, no de la entidad Employee.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pago de incentivos; Nómina; Archivo bancario; Contrato laboral; Cuenta bancaria del empleado; Unidad funcional; Grupo de nómina; Cargo; Período de liquidación', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.IncentivePayment: Devuelve cada pago de incentivo con sus valores liquidados (PaidValue, TotalAccrued, TotalDeducted), período y datos bancarios del contrato, marcando BankFileStatus=0 y Process=1 como literales fijos para identificarlos como ''sin confirmar''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Position; Payroll.Bank; Payroll.Group; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentWithoutConfirmated';
GO
