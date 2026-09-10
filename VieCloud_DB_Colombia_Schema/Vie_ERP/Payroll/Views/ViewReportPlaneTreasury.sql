CREATE VIEW [Payroll].[ViewReportPlaneTreasury]
AS
SELECT
ROW_NUMBER() OVER(ORDER BY thi.Nit ASC) as Row, 
thi.Nit,
  thi.Name, 
  li.TotalPaid,
  BankAccountNumber,
  ba.Name AS 'NameBank',
  g.Code,
  li.PayrollDateLiquidated,
  li.LiquidationPeriod,
  g.Name AS 'NameGroup'   
  FROM 
  Payroll.Liquidation AS li
  INNER JOIN Payroll.[Group] AS g ON g.Id = li.GroupId
  LEFT JOIN Payroll.Bank AS ba ON ba.Id = li.BankId
  INNER JOIN Payroll.Employee AS em ON em.Id = li.EmployeeId
  INNER JOIN Common.ThirdParty AS thi ON thi.Id = em.ThirdPartyId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte plano de tesorería para el pago de nómina. Consolida la información necesaria para el desembolso de salarios cruzando las liquidaciones de nómina con los empleados, sus datos de identificación (NIT/cédula), número de cuenta bancaria y entidad bancaria destino, junto con el grupo de nómina al que pertenecen y el período liquidado. Se utiliza para generar el archivo o reporte plano que tesorería entrega al banco para procesar los pagos de nómina, incluyendo el monto total a pagar a cada empleado, el banco receptor y la fecha de liquidación.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPlaneTreasury';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPlaneTreasury';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de liquidaciones de nómina con datos del empleado, su tercero, banco y grupo, para generar el archivo plano de pagos a tesorería.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Toda Liquidation debe estar vinculada a un Group y a un Employee existentes; de lo contrario la fila se excluye del reporte.; Todo Employee referenciado debe tener un ThirdParty asociado en Common.ThirdParty para que su liquidación aparezca en el reporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa una liquidación que obligatoriamente tiene grupo de nómina, empleado y tercero asociado (INNER JOIN sobre Group, Employee y ThirdParty).; El banco es opcional: si la liquidación no tiene BankId asignado, la fila se conserva pero NameBank y BankAccountNumber pueden ser nulos (LEFT JOIN sobre Bank).; La numeración de filas (Row) se asigna ordenando ascendentemente por el NIT del tercero asociado al empleado.; La identificación del beneficiario del pago (Nit, Name) proviene de Common.ThirdParty a través del empleado, no de tablas propias de nómina.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'liquidación de nómina; empleado; tercero (NIT); banco; cuenta bancaria; grupo de nómina; período de liquidación; tesorería', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Liquidation: Devuelve una fila por cada registro en Payroll.Liquidation cuyo empleado, tercero y grupo existan; incluye monto pagado, cuenta bancaria, banco, código y nombre del grupo, fecha y período de liquidación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Group; Payroll.Bank; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasury';
GO
