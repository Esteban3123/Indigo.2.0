CREATE PROCEDURE [dbo].[InformeEmpleados] @Grupo VARCHAR(5)
AS

     --SELECT   
     --DISTINCT  
     --tp.Nit AS Cedula_Empleado, tp.Name AS Nombre_Empleado, CC.Code AS Código_Centros_de_Costo, CC.Name AS NombreCentrosdeCosto, FU.Code AS CódigoUnidadFuncional, FU.Name AS NombreUnidadFuncional,L.DaysWorked AS Días_Trabajados,  
     --LD.ConceptDetail AS Concepto, LD.AccruedValue AS Devengado, LD.DeductedValue AS Deduccido, LD.ConceptTotalValue AS TotalValorConcepto, SUM(LD.AccruedValue - ld.DeductedValue)  
     --FROM Common.ThirdParty TP  
     --INNER JOIN Payroll.Employee E ON E.ThirdPartyId = TP.Id  
     --INNER JOIN Payroll.CostCenter CC ON CC.Id = E.CostCenterId  
     --INNER JOIN Payroll.[Contract] C ON C.EmployeeId = E.Id  
     --LEFT JOIN Payroll.Liquidation L ON L.ContractId = C.Id  
     --INNER JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id  
     --INNER JOIN Payroll.[Group] G ON G.Id = L.GroupId  
     --LEFT JOIN Payroll.Company COM ON COM.ThirdPartyId = TP.Id  
     --LEFT JOIN Payroll.BranchOffice BO ON BO.CompanyId = COM.Id  
     --INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId  
     --WHERE G.Code = @Grupo AND YEAR(L.PayrollDateLiquidated) = 2020 AND MONTH(L.PayrollDateLiquidated) = 8  
     --GROUP BY tp.Nit, tp.Name, CC.Code, CC.Name, FU.Code, FU.Name,L.DaysWorked,  
     --LD.ConceptDetail, LD.AccruedValue, LD.DeductedValue, LD.ConceptTotalValue  
     --SELECT TOP 1 * FROM Payroll.Liquidation  
     --SELECT TOP 1 * FROM Payroll.LiquidationDetail  
     --SELECT * FROM Payroll.BranchOffice  
     --SELECT * FROM Payroll.Company  
     --GROUP BY G.Code   
     --from DWH..DWH_VistaFacturacionDiaria  
     --where Año between @AnoIni and @AnoFin  
     --order by Mes asc  
     --cedula, nombre, centros de costo, unidades funcionales, dias laborados, todos los devengados con el total y todos los deducidos con el total y el neto a pagar, sedes  
     --tercero, empleado, centro de costos, CONTRATOS, unidades funcionales, liquidación, detalle de liquidación, BranchOffice  

     SELECT DISTINCT 
            L.RegisterStatus AS Estado, 
            G.Code AS CodigoGrupo, 
            G.Name AS NombreGrupo, 
            TP.Nit AS cod_empl, 
            TP.Name AS NombreEmp, 
            POS.Name AS nom_carg, 
            FU.Name AS UnidadFuncional, 
            CC.Name AS CentroCosto, 
            L.PayrollDateLiquidated AS fec_acum,
            CASE
                WHEN CT.ContractClass = 2
                THEN 'X'
                ELSE ''
            END AS SalarioAprendices,
            CASE
                WHEN G.Code = '04'
                THEN 'X'
                ELSE ''
            END AS SalarioIntegral, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '001'
               AND PayrollDate = '2020-08-15'
     ) AS SUELDO, 
            L.ValueTransportingRelief AS AuxTransporte, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '008'
               AND PayrollDate = '2020-08-15'
     ) AS HORA_EXTRA_DIURNA, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '009'
               AND PayrollDate = '2020-08-15'
     ) AS HORA_EXTRA_NOCTURNA, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '006'
               AND PayrollDate = '2020-08-15'
     ) AS HORAS_EXTRAS_FESTIVAS_DIURNAS, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '007'
               AND PayrollDate = '2020-08-15'
     ) AS HORA_EXTRA_FESTIVA_NOCTURNA, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '010'
               AND PayrollDate = '2020-08-15'
     ) AS RECARGO_NOCTURNO_NORMAL, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '011'
               AND PayrollDate = '2020-08-15'
     ) AS RECARGO_NOCTURNO_FESTIVO, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '081'
               AND PayrollDate = '2020-08-15'
     ) AS RECARGO_DIURNO_DOMINICAL_FESTIVO, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '022'
               AND PayrollDate = '2020-08-15'
     ) AS RECARGOS_EXTRAS_EN_PERIODO, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '019'
               AND PayrollDate = '2020-08-15'
     ) AS VALOR_DOMINICAL_ASISTENCIAL, 
            L.DaysWorked AS DiasLaborados,
            CASE
                WHEN L.VacationDays > 0
                THEN 'X'
                ELSE ''
            END AS Vacaciones, 
            L.VacationDays AS Dia_Vaca, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptClass = '004'
               AND PayrollDate = '2020-08-15'
     ) AS Bonificaciones, 
            L.AccumulatedOtherAccrued AS OtrosDevengados,
            CASE
                WHEN L.AmbulatoryDisabilityDays > 0
                THEN 'X'
                ELSE ''
            END AS IncapaAmbulatoria, 
            L.AmbulatoryDisabilityDays AS diaIA, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '013'
               AND PayrollDate = '2020-08-15'
     ) AS VALOR_INCAPACIDAD_AMBULATORIA,
            CASE
                WHEN L.DisabilityHospitalDays > 0
                THEN 'X'
                ELSE ''
            END AS IncapaHospitalaria, 
            L.DisabilityHospitalDays AS DiaIH, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '014'
               AND PayrollDate = '2020-08-15'
     ) AS VALOR_INCAPACIDAD_HOSPITALARIA,
            CASE
                WHEN L.OccupationalRisksDays > 0
                THEN 'X'
                ELSE ''
            END AS IncapaRiesgoProfesional, 
            L.OccupationalRisksDays AS diaIP, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '015'
               AND PayrollDate = '2020-08-15'
     ) AS VALOR_INCAPACIDAD_RIESGOS,
            CASE
                WHEN L.MaternityLeaveDays > 0
                THEN 'X'
                ELSE ''
            END AS Licencia_Materna, 
     (
         SELECT SUM(AccruedValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptCode = '016'
               AND PayrollDate = '2020-08-15'
     ) AS VALOR_LICENCIA_MATERNIDAD, 
            L.TotalAccrued AS Total_Devengados, 
            L.EmployeeHealthContributionValue AS AporteSalud, 
            L.PensionContributionValue AS AportesPension, 
     (
         SELECT SUM(ConceptTotalValue)
         FROM Payroll.LiquidationDetail
         WHERE PayrollId = L.Id
               AND ConceptClass = '038'
               AND PayrollDate = '2020-08-15'
     ) AS Fondo_Solidaridad_Pensional, 
            L.CalculatedWithholdingValue AS Retencion_Fuente, 
            L.AccumulatedOtherDeducted AS OtrosDeducidos, 
            L.TotalDeducted AS TotalDeducido, 
            L.TotalPaid AS TotalPagado, 
            L.EmployerHealthContributionValue AS SaludPatrono, 
            L.EmployerPensionContributionValue AS PensionPatrono, 
            L.OccupationalRisksContributionValue AS RiesgosProfesionales, 
            (L.OccupationalRisksContributionValue + L.EmployerPensionContributionValue + L.EmployerHealthContributionValue) AS AportesPatronales, 
            L.SenaContributionValue AS Sena, 
            L.ICBFContributionValue AS ICBF, 
            L.FamilyCompensationFundContributionValue AS Caja_Compensacion, 
            L.ParafiscalContribution AS AportesParafiscales, 
            L.ProvisionVacation AS P_Vacaciones, 
            L.ProvisionIncentive AS P_Primas, 
            L.ProvisionInterestsUnemployment AS P_ICesantias, 
            L.UnemploymentAccumulated AS P_Cesantias, 
            L.ProvisionsValue AS PrestacionesSociales, 
            L.TotalAccrued + (L.OccupationalRisksContributionValue + L.EmployerPensionContributionValue + L.EmployerHealthContributionValue) + L.ParafiscalContribution + L.ProvisionsValue AS TotalNomina
     FROM Payroll.Liquidation L, 
          Payroll.Employee E, 
          Common.ThirdParty TP, 
          Payroll.[Contract] CONT, 
          Payroll.Position POS, 
          Payroll.FunctionalUnit FU, 
          Payroll.ContractType CT, 
          Payroll.[Group] G, 
          Payroll.CostCenter CC, 
          Payroll.LiquidationDetail LD
     WHERE L.EmployeeId = E.Id
           AND E.ThirdPartyId = TP.Id
           AND CONT.EmployeeId = e.Id
           AND CONT.Id = L.ContractId
           AND CONT.PositionId = POS.Id
           AND FU.Id = CONT.FunctionalUnitId
           AND CONT.ContractTypeId = CT.Id
           AND L.GroupId = G.Id
           AND FU.CostCenterId = CC.Id
           AND L.Id = LD.PayrollId
           AND L.PayrollDateLiquidated = '2020-08-15' -- Fecha de la Nómina???  
           AND G.Code = @Grupo;