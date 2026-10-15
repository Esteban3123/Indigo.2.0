
CREATE  VIEW [Payroll].[ViewVerifyAutoliquidationFileINS]
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

        -- DATOS DE SANCI�N
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

        -- DATOS DE FONDOS Y D�AS
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
        co.StandardCode AS NationalityCode,
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

        -- FECHAS SEG�N TIPO
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
    LEFT JOIN Common.City ct ON P.IdentificacionCityId = ct.Id 
    LEFT JOIN Common.Department d ON ct.DepartamentId = d.Id 
    LEFT JOIN Common.Country co ON d.CountryId = co.Id 
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
    PensionAdministratorCode, EPSCode, CCFCode, PensionDays, HealthDays,
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