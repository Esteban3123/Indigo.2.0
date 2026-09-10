

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
BranchOfficeId,
ExemptValueRetention
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de retención en la fuente sobre nómina. Consolida, por empleado y período de liquidación, los valores necesarios para calcular y verificar la retención en la fuente (RTF): total devengado, total devengado que afecta base RTF, deducciones permitidas (aportes a pensión obligatoria y voluntaria, fondo de solidaridad, AFC, salud, medicina prepagada, dependientes y vivienda), base gravable, valor total de retención y renta de trabajo exenta. Integra las tablas de liquidación de nómina, detalle de conceptos, catálogo de conceptos, grupos de nómina, empleados y terceros (para obtener NIT y nombre del trabajador), y extrae la sede (sucursal) desde el último contrato activo del empleado. Solo muestra registros donde efectivamente se generó retención en la fuente (TotalRetencion mayor a cero), siendo útil para reportería tributaria, declaraciones de renta del empleador y conciliación de retenciones practicadas a trabajadores.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPayrollWithholding';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewReportPayrollWithholding';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte de retención en la fuente sobre nómina por empleado y liquidación, consolidando devengados, deducciones depurables (pensión, AFC, salud, medicina prepagada, dependientes, vivienda) y la base/total de retención.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen liquidaciones en Payroll.Liquidation con detalles en Payroll.LiquidationDetail asociados a conceptos en Payroll.Concept.; Cada empleado tiene un tercero asociado (Common.ThirdParty) y pertenece a un grupo de nómina (Payroll.Group).; Las clases de concepto (''014'',''016'',''017'',''019'',''020'',''038'',''045'') están parametrizadas en el catálogo de conceptos para identificar pensión, pensión voluntaria, salud, medicina prepagada, retención, fondo de solidaridad y AFC respectivamente.; El campo TypeArticleRTF está marcado (<>0) en al menos un detalle para poder reportar el artículo aplicable de retención.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las clases de concepto (''014'' pensión obligatoria, ''016'' pensión voluntaria, ''017'' salud, ''019'' medicina prepagada, ''020'' retención, ''038'' fondo de solidaridad, ''045'' AFC) están codificadas como literales fijos en la vista.; Los importes nulos en sumas se reemplazan por 0 mediante ISNULL en todas las deducciones depurables y en la retención.; Solo se reporta una sucursal (BranchOfficeId) por empleado: la del contrato más reciente (mayor Id).; El reporte siempre filtra empleados con retención efectiva (TotalRetencion > 0).; Articulo proviene de un único detalle por liquidación (TOP 1 con TypeArticleRTF<>0).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Retención en la fuente sobre nómina (RTF); Liquidación de nómina; Devengado; Aporte a pensión obligatoria; Aporte a pensión voluntaria; Fondo de solidaridad pensional; AFC (Ahorro para el Fomento de la Construcción); Aporte a salud; Medicina prepagada; Deducción por dependientes; Deducción por vivienda; Base gravable de retención; Tipo de declarante; Procedimiento de retención (Art. 1 / 2); Sucursal / unidad funcional; Grupo de nómina; Tercero / empleado', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewReportPayrollWithholding: Solo retorna empleados/liquidaciones cuyo TotalRetencion (suma de ConceptTotalValue con ConceptClass=''020'') sea mayor que 0; quienes no tienen retención son excluidos del reporte.; [RETURN_RESULT] Payroll.ViewReportPayrollWithholding: Excluye los detalles con ConceptType = 3 al calcular el TotalDevengado y agregaciones del nivel principal (where ld.ConceptType <> 3).; [RETURN_RESULT] Payroll.ViewReportPayrollWithholding: TotalDevengadoRTF se calcula sumando solo conceptos con AffectIBCRTF=1 y ConceptType=1 (devengados que afectan la base de retención en la fuente).; [RETURN_RESULT] Payroll.ViewReportPayrollWithholding: BaseGravable se toma del campo RetentionBase del detalle con ConceptClass=''020'' y TypeArticleRTF<>0; si no existe, se devuelve 0.; [RETURN_RESULT] Payroll.ViewReportPayrollWithholding: BranchOfficeId se obtiene de la unidad funcional del último contrato del empleado (TOP 1 ORDER BY cntrc.Id DESC en Payroll.Contract JOIN Payroll.FunctionalUnit).; [RETURN_RESULT] Payroll.ViewReportPayrollWithholding: Numera las filas con ROW_NUMBER() ordenando por Identificacion ASC para entregar un consecutivo en el reporte.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TotalRetencion > 0 (existe valor sumado de conceptos clase ''020'') → Se incluye el registro del empleado/liquidación en la vista. else El registro se descarta y no aparece en el reporte de retención.; si ld.ConceptType <> 3 → El detalle se considera para el cálculo del TotalDevengado y agrupaciones principales. else Se omite del cálculo principal (típicamente excluye un tipo de concepto no aplicable).; si Concepto con AffectIBCRTF = 1 y ConceptType = 1 → Su ConceptTotalValue se suma al TotalDevengadoRTF (base de retención en la fuente). else No participa en el TotalDevengadoRTF.; si Detalle con ConceptClass=''020'' y TypeArticleRTF <> 0 → Su RetentionBase se toma como BaseGravable y su TypeArticleRTF como Articulo del reporte. else BaseGravable=0 y Articulo nulo si no hay coincidencia.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.Group; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewReportPayrollWithholding';
GO
