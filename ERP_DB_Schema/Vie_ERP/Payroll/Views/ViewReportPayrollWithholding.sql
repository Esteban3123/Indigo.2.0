



CREATE VIEW [Payroll].[ViewReportPayrollWithholding]
AS
select
ROW_NUMBER() OVER(ORDER BY Identificacion ASC) as Row,
IdEmpleado,
Identificacion,
Nombre,
Procedimiento,
FechaLiquidacion,
IdGrupo,
CodigoGrupo,
NombreGrupo,
TotalDevengado,
TotalDevengadoRTF,
AportePension,
AportePensionVoluntaria,
FonsoSolidaridad,
AFC,
AportesSalud,
MedicinaPrepagada,
DeduccionDependiente,
DeduccionVivienda,
BaseRetencion as BaseGravable,
TotalRetencion,
Articulo,
DeclarantType,
BranchOfficeId
from (
select 
l.Id as IdLiquidacion,
e.Id as IdEmpleado,
e.ProcedureTypeRTF as Procedimiento,
t.Nit as Identificacion,
t.Name as Nombre,
l.PayrollDateLiquidated as FechaLiquidacion,
g.Id as IdGrupo,
g.Code as CodigoGrupo,
g.Name as NombreGrupo,
sum(ld.AccruedValue) as TotalDevengado,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail opld, Payroll.Concept opconc where opld.PayrollId = l.Id and opld.ConceptId = opconc.Id and opconc.AffectIBCRTF = 1 and opld.ConceptType = 1),0) as TotalDevengadoRTF,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '014'),0) as AportePension,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '016'),0) as AportePensionVoluntaria,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '038'),0) as FonsoSolidaridad,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '045'),0) as AFC,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '017'),0) as AportesSalud,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '019'),0) as MedicinaPrepagada,
l.DependentsDeduction as DeduccionDependiente,
l.HousingDeductionValue as DeduccionVivienda,
isnull((select RetentionBase from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '020' and TypeArticleRTF <> 0),0) as BaseRetencion,
isnull((select sum(ConceptTotalValue) from Payroll.LiquidationDetail where PayrollId = l.Id and ConceptClass = '020'),0) as TotalRetencion,
ISNULL(msgRenta.ExemptValueMensaje, ISNULL(l.ExemptValueRetention, 0)) as ExemptValueRetention,
(select top 1 TypeArticleRTF from Payroll.LiquidationDetail where PayrollId = l.Id and TypeArticleRTF <> 0) as Articulo,
e.DeclarantType,
cntrc.BranchOfficeId
from Payroll.Liquidation l
inner join Payroll.LiquidationDetail ld on ld.PayrollId = l.Id
inner join Payroll.Concept c on c.Id = ld.ConceptId
inner join Payroll.[Group] g on g.Id = l.GroupId
inner join Payroll.Employee e on e.Id = l.EmployeeId
inner join Common.ThirdParty t on t.Id = e.ThirdPartyId
OUTER APPLY(
	SELECT TOP 1 fUnit.BranchOfficeId
	FROM Payroll.[Contract] cntrc
	JOIN Payroll.FunctionalUnit fUnit ON fUnit.Id = cntrc.FunctionalUnitId
	WHERE cntrc.EmployeeId = e.Id
	ORDER BY cntrc.Id DESC
		
) cntrc
OUTER APPLY (
    SELECT TOP 1
        CASE 
            WHEN CHARINDEX('Renta de Trabajo Excenta ', m.Description) > 0
             AND CHARINDEX(' Subtotal', m.Description, 
                     CHARINDEX('Renta de Trabajo Excenta ', m.Description)) > 0
            THEN
                CAST(
                    SUBSTRING(
                        m.Description,
                        CHARINDEX('Renta de Trabajo Excenta ', m.Description) 
                            + LEN('Renta de Trabajo Excenta '),
                        CHARINDEX(' Subtotal', m.Description,
                            CHARINDEX('Renta de Trabajo Excenta ', m.Description))
                        - (CHARINDEX('Renta de Trabajo Excenta ', m.Description) 
                            + LEN('Renta de Trabajo Excenta '))
                    ) AS DECIMAL(18,2)
                )
            ELSE NULL
        END AS ExemptValueMensaje
    FROM Payroll.Message m
    WHERE m.LiquitadionId = l.Id
      AND m.Description LIKE '%Renta de Trabajo Excenta%'
) msgRenta
where ld.ConceptType <> 3
group by l.Id,e.Id, e.ProcedureTypeRTF,l.DependentsDeduction,l.HousingDeductionValue,t.Nit, t.Name, l.PayrollDateLiquidated, g.Id, g.Code, g.Name,e.DeclarantType, cntrc.BranchOfficeId,msgRenta.ExemptValueMensaje,l.ExemptValueRetention
) as data where TotalRetencion > 0