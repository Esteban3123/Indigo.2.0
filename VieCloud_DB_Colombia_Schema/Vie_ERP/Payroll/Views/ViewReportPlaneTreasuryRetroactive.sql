CREATE VIEW [Payroll].[ViewReportPlaneTreasuryRetroactive]
AS
SELECT RC.Id,C.BankAccountNumber, B.Name as BankName, TP.Nit, TP.Name as ThirdPartyName, 
(SELECT SUM(RD.ValueConceptWithRetroactive) from Payroll.RetroactiveD RD, Payroll.Concept CONC WHERE CONC.Id = RD.IdConcept and RD.IdRetroactiveC = RC.ID AND CONC.ConceptType = 1) - (SELECT SUM(RD.ValueConceptWithRetroactive) from Payroll.RetroactiveD RD, Payroll.Concept CONC WHERE CONC.Id = RD.IdConcept and RD.IdRetroactiveC = RC.ID AND CONC.ConceptType = 2) as TotalRetroactiveValue,
--RC.TotalRetroactiveValue, 
RC.InitialDateRetroactive, G.Code as CodeGroup, G.Name as NameGroup 
FROM Payroll.RetroactiveC RC,
Payroll.[Group] G, 
Payroll.[Contract] C, 
Common.ThirdParty TP, 
Payroll.Employee E, 
Payroll.Bank B
WHERE RC.IdContract = C.Id and RC.IdEmployee = E.Id AND E.ThirdPartyId = TP.Id AND C.BankId = B.Id AND G.Id = RC.IdGroup
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista para el reporte plano de tesorería de retroactivos de nómina. Consolida, por cada liquidación retroactiva aprobada, los datos del empleado (NIT y nombre obtenidos desde el tercero asociado), su cuenta bancaria y banco de pago, el grupo de nómina al que pertenece, la fecha de inicio del período retroactivo y el valor neto total a pagar (calculado como la suma de todos los conceptos devengados menos la suma de todos los conceptos deducidos incluidos en el retroactivo). Está diseñada para alimentar archivos planos o reportes de tesorería que permiten gestionar el pago efectivo de retroactivos salariales a los empleados.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPlaneTreasuryRetroactive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPlaneTreasuryRetroactive';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, por cada liquidación retroactiva de nómina, los datos bancarios y del tercero beneficiario junto con el valor neto retroactivo (devengos menos deducciones) para generar el plano de pagos a tesorería.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada RetroactiveC debe tener IdContract, IdEmployee e IdGroup válidos referenciando Contract, Employee y Group.; El Employee referenciado debe tener ThirdPartyId válido en Common.ThirdParty.; El Contract referenciado debe tener BankId válido en Payroll.Bank.; Los conceptos en RetroactiveD deben existir en Payroll.Concept con ConceptType clasificado como 1 (suma) o 2 (resta).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor neto a pagar por retroactivo se calcula como la suma de conceptos con ConceptType=1 (devengos) menos la suma de conceptos con ConceptType=2 (deducciones) sobre Payroll.RetroactiveD para el mismo IdRetroactiveC.; La cuenta bancaria reportada para el pago proviene del contrato del empleado (Contract.BankAccountNumber) y el banco asociado al contrato (Contract.BankId).; El beneficiario del pago se identifica por el tercero asociado al empleado (Employee.ThirdPartyId → ThirdParty.Nit/Name).; Solo se incluyen liquidaciones retroactivas que tengan contrato, empleado, tercero, banco y grupo correctamente relacionados (INNER JOIN implícito vía cláusula WHERE).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'liquidación retroactiva de nómina; conceptos de nómina (devengados y deducciones); tercero (NIT); cuenta bancaria del contrato; banco pagador; grupo de nómina; empleado; contrato', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.RetroactiveC: Devuelve una fila por cada Payroll.RetroactiveC con su valor neto calculado como SUM(ValueConceptWithRetroactive) de conceptos tipo 1 menos SUM de conceptos tipo 2 en Payroll.RetroactiveD.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.RetroactiveC; Payroll.RetroactiveD; Payroll.Concept; Payroll.Group; Payroll.Contract; Common.ThirdParty; Payroll.Employee; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPlaneTreasuryRetroactive';
GO
