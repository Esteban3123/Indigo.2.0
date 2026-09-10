

CREATE VIEW [Payroll].[ViewBankFileIncentivePaymentConfirmated]
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
			BF.Status BankFileStatus,
			Bf.Process
		from Payroll.IncentivePayment I 
		INNER JOIN  Payroll.[Contract] C ON I.ContractId = C.Id
		INNER JOIN Payroll.Employee E ON C.EmployeeId= E.Id
		INNER JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
		INNER JOIN Payroll.Position P ON P.Id = C.PositionId
		INNER JOIN Payroll.Bank B ON B.Id = C.BankId
		INNER JOIN Payroll.[Group] G on G.Id = C.GroupId
		INNER JOIN Payroll.FunctionalUnit F ON F.id = C.FunctionalUnitId	
		INNER JOIN Payroll.BankFileDetail BFD ON e.Id = BFD.EmployeeId
		INNER JOIN Payroll.BankFile BF ON BFD.BankFileId = BF.Id 		
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
         B.id,
		 BF.Status,
		 Bf.Process
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de pagos de incentivos y bonificaciones confirmados en archivos bancarios de nómina. Integra los pagos de incentivos liquidados por período con el contrato laboral, los datos del empleado (NIT, nombre), el cargo, el banco y la cuenta destino, el grupo de nómina y la unidad funcional, cruzando además con el detalle del archivo bancario generado para la dispersión. Permite consultar el estado de cada pago de incentivo (valor pagado, total devengado, total deducido, salario básico) junto con el estado del archivo bancario correspondiente, facilitando la conciliación y confirmación de transferencias de incentivos al banco por empleado y período.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePaymentConfirmated';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewBankFileIncentivePaymentConfirmated';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los pagos de incentivos liquidados con los datos del contrato, empleado, tercero, cargo, grupo, unidad funcional, banco y archivo bancario, para reportar pagos cuya confirmación bancaria ya fue procesada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo IncentivePayment debe tener un Contract válido (ContractId no nulo y existente).; Todo Contract debe tener Employee, Position, Bank, Group y FunctionalUnit asociados (los INNER JOIN exigen integridad referencial).; Todo Employee debe tener un ThirdParty asociado para exponer NIT y nombre.; El empleado debe tener al menos un registro en Payroll.BankFileDetail vinculado a un Payroll.BankFile para aparecer en la vista.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen pagos de incentivo cuyo empleado tiene un registro asociado en un detalle de archivo bancario (BankFileDetail) y por ende un BankFile (los INNER JOIN excluyen incentivos sin archivo bancario generado).; Cada fila combina la información del incentivo con los datos contractuales vigentes del empleado (cuenta bancaria, banco, cargo, grupo, unidad funcional) tomados del contrato referenciado por el incentivo.; Se reportan los estados Status y Process del archivo bancario (BankFile) junto con el RegisterStatus del pago de incentivo, permitiendo filtrar los pagos confirmados desde el consumidor.; El NIT y nombre del empleado provienen del Tercero (ThirdParty) asociado al Employee, no del empleado directamente.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'pago de incentivos; archivo bancario de nómina; contrato laboral; empleado; cuenta bancaria; unidad funcional; grupo de nómina; cargo; tercero (NIT); período de liquidación; devengado; deducción; salario básico', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewBankFileIncentivePaymentConfirmated: Devuelve un set de filas únicas (vía GROUP BY de todas las columnas seleccionadas) con la información del pago de incentivo, contrato, empleado, tercero, cargo, banco, grupo, unidad funcional y estado/proceso del archivo bancario.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.IncentivePayment; Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.Position; Payroll.Bank; Payroll.Group; Payroll.FunctionalUnit; Payroll.BankFileDetail; Payroll.BankFile', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewBankFileIncentivePaymentConfirmated';
GO
