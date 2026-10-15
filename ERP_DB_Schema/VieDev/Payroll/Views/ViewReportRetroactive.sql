/****** Object:  View [Payroll].[ViewReportRetroactive]    Script Date: 3/11/2023 3:56:25 p. m. ******/
CREATE VIEW [Payroll].[ViewReportRetroactive]
AS
SELECT rd.id,
	   ET.Id as IdTipoEmpleado, 
	   CONC.ConceptType as TipoConcepto,
	   TP.Nit, TP.Name as NombreEmpleado,
	   G.Name as NombreGrupo,
	   ET.Name as EmployeeType,
	   RC.InitialDateRetroactive, 
	   FU.Name as FuncionalUnitName, 
	   CC.Code as CodeCostCenter,
	   CC.Name as NameCostCenter, 
	   CONT.BasicSalary,
	   CONC.Code + ' - ' + CONC.Name as Concepto,
	   RD.ValueConceptwithRetroactive as ValorConcepto,
	   (SELECT top 1 TP.Nit from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 1 AND TP.Id = F.ThirdPartyId) as NitFondoSalud,
	   (SELECT top 1 F.Code + ' - ' + F.Name from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 1 AND TP.Id = F.ThirdPartyId) as NombreFondoSalud,
	   (SELECT top 1 TP.Nit from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 2 AND TP.Id = F.ThirdPartyId) as NitFondoPension,
	   (SELECT top 1 F.Code + ' - ' + F.Name from Payroll.FundContract FC, Payroll.Fund F, Common.ThirdParty TP WHERE FC.ContractId = RC.IdContract AND F.Id = FC.fundId AND FC.Fundtype = 2 AND TP.Id = F.ThirdPartyId) as NombreFondoPension,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '035') as Sena,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '036') as CompensationFundValue,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '037') as ICBF,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '018') as HealthValueEmployer,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '017') as HealthValueEmployee,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '015') as PensionValueEmployer,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '014') as PensionValueEmployee,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '008') as UnemploymentProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '034') as UnemploymentInterestProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '031') as UnemploymentVacationProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '033') as IncentivePaymentProvision,
	   (SELECT RDopc.ValueConceptwithRetroactive FROM Payroll.RetroactiveC RCOpc,  Payroll.RetroactiveD RDopc, Payroll.Concept OpcConc where RCOpc.Id =RC.Id and  RDopc.IdRetroactiveC = RCOpc.Id AND OpcConc.Id = RDopc.IdConcept AND OpcConc.ConceptClass = '009') as ARL,
	   FU.BranchOfficeId,
	   CONT.GroupId
FROM Common.ThirdParty TP, Payroll.Employee E, Payroll.[Group] G, Payroll.EmployeeType ET, Payroll.CostCenter CC, 
Payroll.FunctionalUnit FU, Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.[Contract] CONT, Payroll.Concept CONC
WHERE TP.Id = E.ThirdPartyId
AND E.Id = CONT.EmployeeId
AND E.Id = RC.IdEmployee 
AND G.Id = CONT.GroupId
AND CC.Id = E.CostCenterId
AND CONT.FunctionalUnitId = FU.Id
AND ET.Id = E.EmployeeTypeId
AND RC.Id = RD.IdRetroactiveC
AND RC.IdContract = CONT.Id
AND RD.IdConcept = CONC.Id