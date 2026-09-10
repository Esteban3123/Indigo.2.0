
--/****** Object:  View [Payroll].[ViewVerifyAutoliquidationFile]    Script Date: 02/07/2025 11:34:28 a. m. ******/
--SET ANSI_NULLS ON
--GO

CREATE    VIEW [Payroll].[ViewVerifyAutoliquidationFileINS]
AS
WITH Ranked AS
(
    SELECT  
        -- DATOS DE EMPLEADO
        VAF.Id, 
        VAF.PayrollDateLiquidated,
        VAF.EmployeeId, 
        TP.Name AS ThirdPartyName,
        VAF.RegisterStatus, 
        ADT.SIGLA AS IdentificationType,
        TP.Nit, 
        VAF.TypeContractEmployee, 
        VAF.SubTypeEmployee, 
        VAF.ForeignNotBound, 
        VAF.ColombianForeignResident, 
        VAF.CodeCityBranchOffice, 
        TP.Name AS NameEmployee,
        CASE E.Pensionary WHEN 0 THEN 'No' WHEN 1 THEN 'Si' END AS Pensionary,
    
        -- DATOS DE INGRESO Y RETIRO
        VAF.Entry, 
        VAF.IngressDate, 
        VAF.Retirement,
        VAF.DateRetirement,
        VAF.IBCOtrosParafiscales,
        VAF.VSP,
        NULL AS FechaInicioVSP, 
        NULL AS ValueVSP, 
        VAF.VST, 

        -- DATOS DE SANCIÓN
        VAF.SLN, 
        VAF.SanctionInitialDate, 
        VAF.SanctionEndDate, 
        VAF.BaseLiquidacionSLN,
        VAF.IBCSLN,

        -- DATOS DE INCAPACIDAD
        VAF.IGE, 
        VAF.AmbulatoryDisabilityInitialDate, 
        VAF.AmbulatoryDisabiltyEndDate,
        VAF.BaseLiquidacionIGE, 
        VAF.BaseIGE,

        -- DATOS DE MATERNIDAD
        VAF.LMA,
        VAF.MaternityLeaveInitialDate, 
        VAF.MaternityLeaveEndDate, 
        VAF.BaseLiquidacionLMA, 
        VAF.BaseLMA,

        -- DATOS DE VACACIONES
        VAF.VAC, 
        VAF.VacationInitialDate,
        VAF.VacationEndDate, 
        VAF.BaseLiquidacionVAC, 
        VAF.BaseVAC,

        -- DATOS DE RIESGOS
        VAF.IRL, 
        VAF.FechaInicioIRL,
        VAF.FechaFinIRL, 
        VAF.BaseLiquidacionIRL, 
        VAF.BaseIRL, 

        -- DATOS DE FONDOS Y DÍAS
        VAF.PensionAdministratorCode,
        VAF.EPSCode,
        VAF.CCFCode, 
        VAF.PensionDays, 
        VAF.HealthDays, 
        VAF.ProfessionalRiskDays,
        VAF.CompensationFundDays,

        -- SALARIOS E IBCS
        VAF.BasicSalary,
        VAF.IntegralSalary,
        VAF.VSTValue, 
        VAF.IBCPension, 
        VAF.IBCHealth, 
        VAF.IBCProfessionalRisk,
        VAF.IBCCompensationFund, 

        -- TARIFAS
        VAF.RateContributionPension,
        VAF.ValuePension, 
        VAF.PensionSolidarityFundValueContribution,
        VAF.PensionSolidarityFundValueContributionSubsistence, 
        VAF.RateContributionHealth,
        VAF.ValueHealth, 
        VAF.ValueSena, 
        VAF.RateContributionProfessionalRisk, 
        VAF.WorkCenter, 
        VAF.ValueContributionProfessionalRisk, 
        VAF.RateContributorCCF,
        VAF.ValueContributionCCF,
        VAF.RateContributorSENA, 
        VAF.RateContributionICBF, 
        VAF.ValueICBF, 
        VAF.TarifaEspecialPensiones,
        VAF.Observations,

        -- DATOS DE CONTRATO
        VAF.ContractId,
        PO.CCSSCode,
        C.HoursDaily,
        E.InsuredCCSSCode, 
        coNat.StandardCode AS NationalityCode,
        PO.INSCode,

        -- TIPOS DE CAMBIO
        CASE 
            WHEN VAF.EmployeeIngressNovelty = 'X' THEN 'IC'
            WHEN VAF.NoveltyContract = 2 THEN 'OC'
            WHEN VAF.SLN = 'X' OR VAF.LRM = 'X' THEN 'PE'
            WHEN VAF.IGE = 'X' OR VAF.IRL <> '0' OR VAF.LMA = 'X' THEN 'IN'
            WHEN VAF.Retirement = 'X' THEN 'EX'
            ELSE 'SA'
        END AS ChangeType,

        CASE
            WHEN VAF.IRL <> '0' THEN 'INS'
            WHEN VAF.IGE = 'X' OR (VAF.LMA = 'X' AND P.Gender = 1) THEN 'SEM'
            WHEN VAF.LMA = 'X' THEN 'MAT'
            WHEN VAF.LRM = 'X' THEN 'C'
            WHEN VAF.SLN = 'X' THEN 'S'
            ELSE ''
        END AS ChangeCode,

        -- FECHAS SEGÚN TIPO
        CASE 
            WHEN VAF.EmployeeIngressNovelty = 'X' THEN VAF.IngressDate
            WHEN VAF.NoveltyContract = 2 THEN C.ContractInitialDate
            WHEN VAF.LMA = 'X' THEN VAF.MaternityLeaveInitialDate
            WHEN VAF.SLN = 'X' THEN VAF.SanctionInitialDate
            WHEN VAF.IGE = 'X' THEN VAF.AmbulatoryDisabilityInitialDate
            WHEN VAF.IRL <> '0' THEN VAF.FechaInicioIRL
            WHEN VAF.LRM = 'X' THEN VAF.LicenceInitialDate
            ELSE VAF.PayrollDateLiquidated
        END AS InitialDate,

        CASE 
            WHEN VAF.LMA = 'X' THEN VAF.MaternityLeaveEndDate
            WHEN VAF.SLN = 'X' THEN VAF.SanctionEndDate
            WHEN VAF.IGE = 'X' THEN VAF.AmbulatoryDisabiltyEndDate
            WHEN VAF.IRL <> '0' THEN VAF.FechaFinIRL
            WHEN VAF.LRM = 'X' THEN VAF.LicenceEndDate
            WHEN VAF.Retirement = 'X' THEN C.RetirementDate
            ELSE NULL
        END AS FinalDate,

        -- DATOS PERSONALES
        P.FirstLastName,
        P.SecondLastName,
        CONCAT(P.FirstName,' ',P.SecondName) AS CompleteName,
        P.BirthDate,
        P.Gender,
        P.MaritalStatus,
        PH.Phone,
        EM.Email,

        -- NoveltyCode y ranking
        CASE
            WHEN VAF.LMA = 'X' THEN '07'
            WHEN VAF.EmployeeIngressNovelty = 'X' AND VAF.Retirement = 'X' THEN '05'
            WHEN VAF.Retirement = 'X' THEN '02'
            WHEN VAF.IRL <> '0' THEN '04'
            WHEN VAF.SLN = 'X' THEN '06'
            WHEN VAF.EmployeeIngressNovelty = 'X' THEN '01'
            WHEN VAF.IGE = 'X' THEN '03'
            ELSE '00'
        END AS NoveltyCode,

        -- Días de vacaciones del mes: calculado en el CTE para evitar ambigüedad
        -- de nombres en el SELECT externo (Liquidation también tiene EmployeeId, etc.)
        ISNULL((
            SELECT SUM(L.VacationDays)
            FROM Payroll.Liquidation L
            WHERE L.EmployeeId  = VAF.EmployeeId
              AND L.ContractId  = VAF.ContractId
              AND YEAR(L.PayrollDateLiquidated)  = YEAR(VAF.PayrollDateLiquidated)
              AND MONTH(L.PayrollDateLiquidated) = MONTH(VAF.PayrollDateLiquidated)
        ), 0) AS VacationDaysSum,

        -- Días trabajados base (filas SA sin novedades IGE/IRL/LMA): para que cuando
        -- la fila seleccionada sea una novedad, HealthDays muestre días reales trabajados.
        ISNULL((
            SELECT SUM(VAF2.HealthDays)
            FROM Payroll.VerifyAutoliquidationFile VAF2
            WHERE VAF2.EmployeeId = VAF.EmployeeId
              AND VAF2.ContractId = VAF.ContractId
              AND YEAR(VAF2.PayrollDateLiquidated)  = YEAR(VAF.PayrollDateLiquidated)
              AND MONTH(VAF2.PayrollDateLiquidated) = MONTH(VAF.PayrollDateLiquidated)
              AND VAF2.IRL = '0'
              AND VAF2.IGE != 'X'
              AND VAF2.LMA != 'X'
        ), VAF.HealthDays) AS WorkedDaysBase,

        ROW_NUMBER() OVER(
            PARTITION BY VAF.EmployeeId , VAF.PayrollDateLiquidated
            ORDER BY 
                CASE 
                    WHEN VAF.LMA = 'X' THEN 8
                    WHEN VAF.EmployeeIngressNovelty = 'X' AND VAF.Retirement = 'X' THEN 7
                    WHEN VAF.Retirement = 'X' THEN 6
                    WHEN VAF.IRL <> '0' THEN 5
                    WHEN VAF.SLN = 'X' THEN 4
                    WHEN VAF.EmployeeIngressNovelty = 'X' THEN 3
                    WHEN VAF.IGE = 'X' THEN 2
                    ELSE 1
                END DESC
        ) AS RN

    FROM Payroll.VerifyAutoliquidationFile VAF
    JOIN Payroll.Employee E ON E.Id = VAF.EmployeeId
    JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
    JOIN Common.Person P ON P.Id = TP.PersonId
    JOIN DBO.ADTIPOIDENTIFICA ADT ON ADT.ID = P.IdentificationTypeId
    LEFT JOIN Payroll.Contract C ON C.Id = VAF.ContractId  
    LEFT JOIN Payroll.Position PO ON PO.Id = C.PositionId
    LEFT JOIN (
        SELECT PersonId, CountryId,
               ROW_NUMBER() OVER(PARTITION BY PersonId ORDER BY Id DESC) AS RowNum
        FROM Common.PersonNationality
    ) PN ON P.Id = PN.PersonId AND PN.RowNum = 1
    LEFT JOIN Common.Country coNat ON PN.CountryId = coNat.Id
    LEFT JOIN (
        SELECT IdPerson, Phone,
               ROW_NUMBER() OVER(PARTITION BY IdPerson ORDER BY Id DESC) AS RowNum
        FROM Common.Phone
    ) PH ON P.Id = PH.IdPerson AND PH.RowNum = 1
    LEFT JOIN (
        SELECT IdPerson, Email,
               ROW_NUMBER() OVER(PARTITION BY IdPerson ORDER BY Id DESC) AS RowNum
        FROM Common.Email
    ) EM ON P.Id = EM.IdPerson AND EM.RowNum = 1
)
SELECT
    /* todas las columnas excepto RN */
    Id, PayrollDateLiquidated, EmployeeId, ThirdPartyName, RegisterStatus, IdentificationType, Nit,
    TypeContractEmployee, SubTypeEmployee, ForeignNotBound, ColombianForeignResident,
    CodeCityBranchOffice, NameEmployee, Pensionary, Entry, IngressDate, Retirement,
    DateRetirement, IBCOtrosParafiscales, VSP, FechaInicioVSP, ValueVSP, VST, SLN,
    SanctionInitialDate, SanctionEndDate, BaseLiquidacionSLN, IBCSLN, IGE,
    AmbulatoryDisabilityInitialDate, AmbulatoryDisabiltyEndDate, BaseLiquidacionIGE,
    BaseIGE, LMA, MaternityLeaveInitialDate, MaternityLeaveEndDate, BaseLiquidacionLMA,
    BaseLMA, VAC, VacationInitialDate, VacationEndDate, BaseLiquidacionVAC, BaseVAC,
    IRL, FechaInicioIRL, FechaFinIRL, BaseLiquidacionIRL, BaseIRL,
    PensionAdministratorCode, EPSCode, CCFCode, PensionDays,
    CASE
        WHEN IGE = 'X' OR IRL <> '0' OR LMA = 'X'
        THEN WorkedDaysBase + VacationDaysSum
        ELSE HealthDays + VacationDaysSum
    END AS HealthDays,
    ProfessionalRiskDays, CompensationFundDays, BasicSalary, IntegralSalary,
    VSTValue, IBCPension, IBCHealth, IBCProfessionalRisk, IBCCompensationFund,
    RateContributionPension, ValuePension, PensionSolidarityFundValueContribution,
    PensionSolidarityFundValueContributionSubsistence, RateContributionHealth,
    ValueHealth, ValueSena, RateContributionProfessionalRisk, WorkCenter,
    ValueContributionProfessionalRisk, RateContributorCCF, ValueContributionCCF,
    RateContributorSENA, RateContributionICBF, ValueICBF, TarifaEspecialPensiones,
    Observations, ContractId, CCSSCode, HoursDaily, InsuredCCSSCode, NationalityCode,
    INSCode, ChangeType, ChangeCode, InitialDate, FinalDate, FirstLastName,
    SecondLastName, CompleteName, BirthDate, Gender, MaritalStatus, Phone, Email,
    NoveltyCode
FROM Ranked
WHERE RN = 1;
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de verificación del archivo de autoliquidación de seguridad social (PILA) para reportes INS, que consolida por empleado y período de liquidación todos los datos requeridos para el proceso de autoliquidación: aportes a pensión, salud, riesgos laborales (ARL) y caja de compensación, junto con novedades laborales como ingresos, retiros, incapacidades, licencias de maternidad, sanciones y vacaciones. Integra información del empleado (desde Payroll.Employee y Common.Person), su contrato activo (Payroll.Contract), el cargo desempeñado (Payroll.Position), datos de identificación y tipo de documento (ADTIPOIDENTIFICA), ciudad y país de expedición del documento, y teléfono de contacto. Aplica lógica de clasificación para determinar el tipo de novedad (ingreso, egreso, incapacidad, sanción, etc.), el código de cambio PILA y las fechas de inicio y fin correspondientes a cada novedad, priorizando mediante un ranking (ROW_NUMBER) cuando un empleado tiene múltiples novedades en el mismo período. Está diseñada para alimentar la generación y validación del archivo plano de autoliquidación de seguridad social exigido por la normativa colombiana (PILA).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewVerifyAutoliquidationFileINS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewVerifyAutoliquidationFileINS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida, por empleado y período liquidado, un único registro de autoliquidación enriquecido con datos personales, contractuales y de novedades, clasificándolo con tipo/código de novedad para reportes tipo PILA/INS.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de Payroll.VerifyAutoliquidationFile debe tener EmployeeId válido en Payroll.Employee con ThirdParty y Person relacionados (INNER JOIN); El IdentificationTypeId de la persona debe existir en DBO.ADTIPOIDENTIFICA; Para Phone/Email se requiere que existan registros en Common.Phone/Common.Email asociados al IdPerson; si no existen, vendrán como NULL', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna un registro por combinación (EmployeeId, PayrollDateLiquidated): el de mayor prioridad de novedad; La licencia de maternidad (LMA) tiene la mayor prioridad sobre cualquier otra novedad concurrente; Cuando coinciden Ingreso y Retiro en el mismo período, se reporta como un único evento con NoveltyCode=''05''; Pensionary se expone como texto: 0=''No'', 1=''Si''; FechaInicioVSP y ValueVSP siempre se devuelven como NULL (no calculados en esta vista); Si Gender=1 (masculino) y LMA=''X'', se interpreta como licencia de paternidad → ChangeCode=''SEM'' en lugar de ''MAT''; Solo se toma el último Phone y Email registrados por persona (mayor Id) vía ROW_NUMBER', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autoliquidación de nómina (PILA); Aportes a seguridad social (pensión, salud, riesgos profesionales, CCF, SENA, ICBF); Novedades de nómina (Ingreso, Retiro, Incapacidad IGE, Maternidad LMA, Sanción SLN, Licencia LRM, Riesgo Laboral IRL, Vacaciones, VSP, VST); IBC (Ingreso Base de Cotización); Tipo de contrato y subtipo de empleado; Fondo de Solidaridad Pensional; Tarifa especial de pensiones; Centro de trabajo (Work Center); Identificación tipo y nacionalidad; Códigos CCSS/INS (seguridad social)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewVerifyAutoliquidationFileINS: Devuelve solo la fila con RN=1 según ROW_NUMBER particionado por (EmployeeId, PayrollDateLiquidated), priorizando novedades en este orden descendente: LMA(8) > Ingreso+Retiro(7) > Retiro(6) > IRL(5) > SLN(4) > Ingreso(3) > IGE(2) > resto(1)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VAF.EmployeeIngressNovelty = ''X'' → ChangeType=''IC'' (Ingreso) y InitialDate=IngressDate else Se evalúan otras novedades en cascada; si VAF.NoveltyContract = 2 → ChangeType=''OC'' y InitialDate=ContractInitialDate; si VAF.SLN = ''X'' OR VAF.LRM = ''X'' → ChangeType=''PE'' (Permiso/Sanción/Licencia remunerada); si VAF.IGE = ''X'' OR VAF.IRL <> ''0'' OR VAF.LMA = ''X'' → ChangeType=''IN'' (Incapacidad); si VAF.Retirement = ''X'' → ChangeType=''EX'' (Egreso) else ChangeType=''SA'' por defecto; si VAF.IRL <> ''0'' → ChangeCode=''INS'' (riesgo laboral); si VAF.IGE = ''X'' OR (VAF.LMA = ''X'' AND P.Gender = 1) → ChangeCode=''SEM'' (enfermedad/licencia masculina); si VAF.LMA = ''X'' (con Gender ≠ 1) → ChangeCode=''MAT'' (Maternidad); si VAF.LRM = ''X'' → ChangeCode=''C''; si VAF.SLN = ''X'' → ChangeCode=''S'' (Sanción) else ChangeCode=''''; si Asignación de NoveltyCode por prioridad → LMA→07, Ingreso+Retiro→05, Retiro→02, IRL→04, SLN→06, Ingreso→01, IGE→03, default→00', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VerifyAutoliquidationFile; Payroll.Employee; Common.ThirdParty; Common.Person; DBO.ADTIPOIDENTIFICA; Payroll.Contract; Payroll.Position; Common.City; Common.Department; Common.Country; Common.Phone; Common.Email', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFileINS';
GO
