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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Informe de nómina por empleado y grupo de liquidación. Genera un reporte detallado del período de nómina para un grupo específico, consolidando por cada empleado su cédula, nombre, cargo, unidad funcional y centro de costo junto con todos los conceptos devengados (sueldo, auxilio de transporte, horas extras diurnas y nocturnas, recargos nocturnos y festivos, dominicales, bonificaciones, vacaciones, incapacidades ambulatorias y hospitalarias) y las deducciones (salud, pensión, retención en la fuente, libranzas, entre otras), calculando el neto a pagar. Cruza las tablas de empleados, contratos, liquidaciones, detalle de liquidación, terceros, cargos, unidades funcionales, centros de costo, grupos de nómina y tipos de contrato para identificar además si el empleado es aprendiz o tiene salario integral. Se usa para auditoría de nómina, soporte de pago de personal, conciliación de devengados y deducciones, y generación de informes laborales por grupo de liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'InformeEmpleados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'InformeEmpleados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un informe consolidado de nómina por grupo para la fecha de liquidación 2020-08-15, mostrando devengados, deducciones, aportes patronales, provisiones y totales por empleado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una liquidación (Payroll.Liquidation) con PayrollDateLiquidated = ''2020-08-15'' asociada al grupo recibido.; El grupo identificado por el código recibido debe existir en Payroll.Group.; Cada liquidación debe tener empleado, contrato, tercero, cargo, unidad funcional, tipo de contrato y centro de costo relacionados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha de liquidación está fija en ''2020-08-15'' (hard-coded), por lo que el informe siempre corresponde a ese corte.; El código ''04'' identifica el grupo de salario integral.; ContractClass = 2 identifica contratos de aprendices.; Conceptos de devengado se identifican por ConceptCode: ''001'' sueldo, ''008'' hora extra diurna, ''009'' hora extra nocturna, ''006'' hora extra festiva diurna, ''007'' hora extra festiva nocturna, ''010'' recargo nocturno normal, ''011'' recargo nocturno festivo, ''081'' recargo diurno dominical/festivo, ''022'' recargos extras del periodo, ''019'' valor dominical asistencial, ''013'' incapacidad ambulatoria, ''014'' incapacidad hospitalaria, ''015'' incapacidad riesgos, ''016'' licencia maternidad.; ConceptClass ''004'' agrupa bonificaciones; ConceptClass ''038'' agrupa fondo de solidaridad pensional.; TotalNomina = TotalAccrued + aportes patronales (salud + pensión + riesgos) + ParafiscalContribution + ProvisionsValue.; AportesPatronales = riesgos profesionales + pensión patrono + salud patrono.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Liquidación de nómina; Grupo de nómina; Salario integral; Contrato de aprendiz; Auxilio de transporte; Horas extras (diurnas, nocturnas, festivas); Recargos nocturnos y dominicales; Bonificaciones; Vacaciones; Incapacidad ambulatoria; Incapacidad hospitalaria; Incapacidad por riesgos profesionales; Licencia de maternidad; Aporte a salud (empleado y patrono); Aporte a pensión (empleado y patrono); Fondo de solidaridad pensional; Retención en la fuente; Riesgos profesionales (ARL); SENA; ICBF; Caja de compensación; Aportes parafiscales; Provisión de vacaciones; Provisión de primas; Provisión de cesantías e intereses de cesantías; Prestaciones sociales; Centro de costo; Unidad funcional; Cargo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados filtrado por G.Code = @Grupo y L.PayrollDateLiquidated = ''2020-08-15''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CT.ContractClass = 2 → Marca la columna SalarioAprendices con ''X'' else Deja SalarioAprendices vacío; si G.Code = ''04'' → Marca la columna SalarioIntegral con ''X'' else Deja SalarioIntegral vacío; si L.VacationDays > 0 → Marca Vacaciones con ''X'' else Deja Vacaciones vacío; si L.AmbulatoryDisabilityDays > 0 → Marca IncapaAmbulatoria con ''X'' else Vacío; si L.DisabilityHospitalDays > 0 → Marca IncapaHospitalaria con ''X'' else Vacío; si L.OccupationalRisksDays > 0 → Marca IncapaRiesgoProfesional con ''X'' else Vacío; si L.MaternityLeaveDays > 0 → Marca Licencia_Materna con ''X'' else Vacío', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Liquidation; Payroll.Employee; Common.ThirdParty; Payroll.Contract; Payroll.Position; Payroll.FunctionalUnit; Payroll.ContractType; Payroll.Group; Payroll.CostCenter; Payroll.LiquidationDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'InformeEmpleados';
-- GO
