


CREATE VIEW [Payroll].[ViewReportWithholdingRetroactive]
AS

SELECT RD.id, E.Id as IdEmployee, G.Id as IdGroup, RC.InitialDateRetroactive, TP.Nit, TP.Name as NombreEmpleado, E.ProcedureTypeRTF as ProcedimientoRete, G.Name as NombreGrupo, 
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1) as TotalDevengado,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502') as AportePension,
0 as AportePensionVoluntaria, 
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') as FondoSolidaridad,
0 as AFC,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') as AporteSalud,
0 as MedicinaPrepagada,
0 as DeduccionDependendiente,
E.HousingDeductionValue as DeduccionVivienda,
(((SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC WHERE opcRD.IdRetroactiveC = RC.Id AND opcCONC.Id = opcRD.IdConcept AND opcCONC.ConceptType = 1 AND opcCONC.AffectIBCRTF = 1) - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC WHERE opcRD.IdRetroactiveC = RC.Id AND opcCONC.Id = opcRD.IdConcept AND opcCONC.Code = '502') - (SELECT ISNULL(SUM(opcRD.ValueConceptWithRetroactive),0) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC WHERE opcRD.IdRetroactiveC = RC.Id AND opcCONC.Id = opcRD.IdConcept AND opcCONC.Code = '503') - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC WHERE opcRD.IdRetroactiveC = RC.Id AND opcCONC.Id = opcRD.IdConcept AND opcCONC.Code = '501') - (ISNULL(E.HousingDeductionValue,0)))*0.25) as ExemptValueRetention,
(SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1 AND opcCONC.AffectIBCRTF = 1) -  (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502') - (SELECT ISNULL(SUM(opcRD.ValueConceptWithRetroactive),0) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') - (ISNULL(E.HousingDeductionValue,0)) - (((SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.ConceptType = 1 AND opcCONC.AffectIBCRTF = 1) -  (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '502')- (SELECT ISNULL(SUM(opcRD.ValueConceptWithRetroactive),0) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '503') - (SELECT SUM(opcRD.ValueConceptWithRetroactive) FROM Payroll.RetroactiveD opcRD, Payroll.Concept opcCONC where opcRD.IdRetroactiveC = RC.Id and opcCONC.Id = opcRD.IdConcept and opcCONC.Code = '501') - (ISNULL(E.HousingDeductionValue,0)) )*0.25) as BaseGravable,
RD.ValueConceptWithRetroactive as TotalRetencion,
E.ProcedureTypeRTF as Articulo,
E.DeclarantType,
fUnit.BranchOfficeId
FROM Common.ThirdParty TP, Payroll.Employee E, Payroll.[Group] G, Payroll.RetroactiveC RC, Payroll.RetroactiveD RD, Payroll.[Contract] CONT, Payroll.Concept C, Payroll.FunctionalUnit fUnit 

WHERE TP.Id = E.ThirdPartyId and RC.Id = RD.IdRetroactiveC AND RC.IdContract = CONT.Id AND CONT.EmployeeId = E.Id and G.Id = CONT.GroupId AND RD.IdConcept = C.Id
and C.Code = '701'
AND fUnit.Id = CONT.FunctionalUnitId