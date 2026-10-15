

CREATE VIEW [Payroll].[ViewVerifyAutoliquidationFile]
AS



SELECT  
		-- DATOS DE EMPLEADO
		VAF.Id, 
		VAF.PayrollDateLiquidated,
		VAF.EmployeeId, 
		tp.Name,
		VAF.RegisterStatus, 
		ADT.SIGLA IdentificationType,
		--CASE P.IdentificationType 
		--	WHEN 0 THEN 'CC'
		--	WHEN 1 THEN 'CE'
		--	WHEN 2 THEN 'TI'
		--	WHEN 3 THEN 'RC'
		--	WHEN 4 THEN 'PA'
		--	WHEN 5 THEN 'AS'
		--	WHEN 6 THEN 'MS'
		--	WHEN 7 THEN 'NI'
		--END AS IdentificationType,
		TP.Nit, 
		VAF.TypeContractEmployee, 
		VAF.SubTypeEmployee, 
		VAF.ForeignNotBound, 
		VAF.ColombianForeignResident, 
		VAF.CodeCityBranchOffice, 
		TP.Name as NameEmployee,
		CASE E.Pensionary
			WHEN 0 THEN 'No'
			WHEN 1 THEN 'Si'
		END AS Pensionary,
	
		-- DATOS DE INGRESO Y RETIRO
		VAF.Entry, 
		VAF.IngressDate, 
		VAF.Retirement,
		VAF.DateRetirement,
		VAF.IBCOtrosParafiscales,
		VAF.VSP,
		NULL as FechaInicioVSP, 
		NULL as ValueVSP, 
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
		--DATOS DE VACACIONES
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
		-- DATOS DE FONDOS Y DIAS
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
		-------DATOS DE CONTRATO
		VAF.ContractId,
		PO.CCSSCode,
		C.HoursDaily,
		E.InsuredCCSSCode, 
		co.StandardCode AS NationalityCode,
		PO.INSCode,

		-----TIPOS DE CMABIO, CODIGOS Y FECHAS 
		CASE 
			WHEN VAF.EmployeeIngressNovelty = 'X' THEN 'IC'
			WHEN VAF.NoveltyContract = 2  THEN 'OC'
			WHEN VAF.NoveltyContract = 1  THEN 'SA'
			WHEN VAF.SLN = 'X' OR VAF.LicenceDays >0 THEN  'PE'
			WHEN VAF.IGE ='X' OR VAF.IRL = '1' OR VAF.LMA ='X'  THEN 'IN'
			WHEN VAF.Retirement= 'X'THEN 'EX'
			ELSE ''
    END AS ChangeType,
	CASE
        WHEN  VAF.IRL = '1' THEN 'INS'
		WHEN  VAF.IGE ='X'  OR ( VAF.LMA ='X' AND P.Gender = 1) THEN 'SEM'
		WHEN  VAF.LMA ='X' THEN 'MAT'
		WHEN  VAF.licenceDays > 0 THEN 'C'
		WHEN  VAF.SLN ='X'  THEN 'S'
        ELSE ''
    END AS ChangeCode,
	Case 
		WHEN VAF.EmployeeIngressNovelty = 'X' THEN VAF.IngressDate
		WHEN VAF.NoveltyContract = 2  THEN C.ContractInitialDate
		WHEN VAF.NoveltyContract = 1  THEN C.ContractInitialDate
		WHEN VAF.LMA ='X'  THEN VAF.MaternityLeaveInitialDate
		WHEN VAF.SLN ='X' THEN VAF.SanctionInitialDate
		WHEN VAF.IGE = 'X' THEN VAF.AmbulatoryDisabilityInitialDate
		WHEN VAF.IRL = '1' THEN VAF.FechaInicioIRL
		ELSE vaf.PayrollDateLiquidated
	END AS InitialDate,
	Case 
		WHEN VAF.LMA ='X'  THEN VAF.MaternityLeaveEndDate
		WHEN VAF.SLN ='X' THEN VAF.SanctionEndDate
		WHEN VAF.IGE = 'X' THEN VAF.AmbulatoryDisabiltyEndDate
		WHEN VAF.IRL = '1' THEN VAF.FechaFinIRL
		WHEN VAF.Retirement= 'X' THEN C.RetirementDate
		ELSE  null
	END AS FinalDate,
	p.FirstLastName,
	p.SecondLastName,
	concat(p.FirstName,' ',p.SecondName) CompleteName,
	P.BirthDate,
	P.Gender,
	P.MaritalStatus,
	PH.Phone,
	EM.Email
FROM Payroll.VerifyAutoliquidationFile VAF
JOIN Payroll.Employee E ON E.Id = VAF.EmployeeId
JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
JOIN Common.Person P on P.Id = TP.PersonId
--INNER JOIN Common.Email EM On EM.IdPerson = P.Id
JOIN DBO.ADTIPOIDENTIFICA ADT ON ADT.ID = P.IdentificationTypeId
LEFT JOIN Payroll.Contract C ON C.Id = vaf.ContractId  
LEFT JOIN Payroll.Position PO ON PO.Id = C.PositionId
INNER JOIN Common.City AS ct ON P.IdentificacionCityId = ct.Id 
INNER JOIN Common.Department AS d ON ct.DepartamentId = d.Id 
INNER JOIN Common.Country AS co ON d.CountryId = co.Id 
LEFT JOIN (
	Select  
		IdPerson
		,Phone
		,ROW_NUMBER() OVER(PARTITION BY Idperson ORDER BY Id desc) AS RowNum
	FROM Common.Phone
) ph on p.Id = ph.IdPerson AND ph.RowNum = 1
LEFT JOIN (
	Select  
		IdPerson
		,Email
		,ROW_NUMBER() OVER(PARTITION BY Idperson ORDER BY Id desc) AS RowNum
	FROM Common.Email
) EM on p.Id = EM.IdPerson AND EM.RowNum = 1
GROUP BY 
    -- Incluye todos los campos utilizados
    VAF.Id, 
    VAF.PayrollDateLiquidated,
    VAF.EmployeeId, 
    tp.Name,
    VAF.RegisterStatus, 
    P.IdentificationType,
    TP.Nit, 
    VAF.TypeContractEmployee, 
    VAF.SubTypeEmployee, 
    VAF.ForeignNotBound, 
    VAF.ColombianForeignResident, 
    VAF.CodeCityBranchOffice, 
    TP.Name,
    E.Pensionary,
    VAF.Entry, 
    VAF.IngressDate, 
    VAF.Retirement,
    VAF.DateRetirement,
    VAF.IBCOtrosParafiscales,
    VAF.VSP,
    VAF.VST,
    VAF.SLN, 
    VAF.SanctionInitialDate, 
    VAF.SanctionEndDate, 
    VAF.BaseLiquidacionSLN,
    VAF.IBCSLN,
    VAF.IGE, 
    VAF.AmbulatoryDisabilityInitialDate, 
    VAF.AmbulatoryDisabiltyEndDate,
    VAF.BaseLiquidacionIGE, 
    VAF.BaseIGE,
    VAF.LMA,
    VAF.MaternityLeaveInitialDate, 
    VAF.MaternityLeaveEndDate, 
    VAF.BaseLiquidacionLMA, 
    VAF.BaseLMA,
    VAF.VAC, 
    VAF.VacationInitialDate,
    VAF.VacationEndDate, 
    VAF.BaseLiquidacionVAC, 
    VAF.BaseVAC,
    VAF.IRL, 
    VAF.FechaInicioIRL,
    VAF.FechaFinIRL, 
    VAF.BaseLiquidacionIRL, 
    VAF.BaseIRL, 
    VAF.PensionAdministratorCode,
    VAF.EPSCode,
    VAF.CCFCode, 
    VAF.PensionDays, 
    VAF.HealthDays, 
    VAF.ProfessionalRiskDays,
    VAF.CompensationFundDays,
    VAF.BasicSalary,
    VAF.IntegralSalary,
    VAF.VSTValue, 
    VAF.IBCPension, 
    VAF.IBCHealth, 
    VAF.IBCProfessionalRisk,
    VAF.IBCCompensationFund, 
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
    VAF.ContractId,
    PO.CCSSCode,
    C.HoursDaily,
    E.InsuredCCSSCode, 
    co.StandardCode,
    PO.INSCode,
    VAF.IngressDate,
    C.ContractModificationReasonId,
    VAF.LMA,
    VAF.SLN,
    VAF.IGE,
    VAF.IRL,
    C.RetirementDate,
	C.ContractInitialDate,
	C.JobBondingDate,
	P.Gender,
	vaf.ConceptClass,
	VAF.LicenceDays,
	VAF.NoveltyContract,
	ADT.SIGLA,
	VAF.EmployeeIngressNovelty,
	p.FirstLastName,
	p.SecondLastName,
	p.FirstName,
	p.SecondName,
	P.BirthDate,
	P.MaritalStatus,
	PH.Phone,
	EM.Email;