
-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 10/08/2017
-- Description:	Elimina las Liquidaciones de Nomina NO Confirmadas
-- =============================================
CREATE PROCEDURE [Payroll].[SP_AutoliquidationFile]
	@PayrollDate date,
	@WorkCenterCode varchar(20)
AS 
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY

	declare @PayrollStarDate date
	declare @PayrollEndDate date

	IF @WorkCenterCode IS NULL BEGIN
		SET @WorkCenterCode = '%%'
	END

	SELECT @PayrollStarDate = DATEADD(mm, DATEDIFF(mm, 0, @PayrollDate), 0)
	PRINT @PayrollStarDate

	-- Valido que no esté Confirmado
	IF (SELECT COUNT(*) FROM Payroll.VerifyAutoliquidationFile where PayrollDateLiquidated = @PayrollDate AND WorkCenter = @WorkCenterCode and RegisterStatus = 1) > 0 BEGIN
		SELECT '888'  AS CodeMessage , 'El archivo de este mes ya se generó y se confirmó' as Message, cast(3 as tinyint) as [Status] 
		RETURN
	END

	SELECT @PayrollEndDate = dateadd( s, -1, dateadd( mm, datediff( m, 0, @PayrollDate ) + 1, 0 ) )

	-- Si no está confirmado, lo elimino de la tabla para volverlo a Generar
	DELETE Payroll.VerifyAutoliquidationFile where PayrollDateLiquidated = @PayrollDate AND WorkCenter = @WorkCenterCode and RegisterStatus = 0

	DECLARE @TmpAutoliquidationFortnightly table
		(IdEmployee int,
		IdContract int,
		IBCPension numeric(18,0), 
		IBCHealth numeric(18,0),
		WorkDays int,
		DaysVacation int,
		DaysMaternityLeave int,
		InitialDateOccupationalRisksDisability date,
		DaysAmbulatoryDisability int,
		DaysSanction int,
		DaysUnpaidLicenseDays int,
		VoluntaryContributionPensionValue numeric(18,0),
		PensionSolidarityFundValueContribution numeric(18,0)
		)

	DECLARE @TmpAutoliquidationDetailFortnightly table
	(
	IdEmployee int,
	TotalNumberHours int
	)
		
	declare @TmpAutoliquidation table
			(IdEmployee int,
			IdContract int,
			sequenceDetail int, -- 2
			typeDocument varchar(2), -- 3
			Nit varchar(18), -- 4
			typeContractEmployee varchar(2), -- 5
			subTypeEmployee varchar(2), -- 6
			foreignNotBound varchar(2), -- 7
			colombianForeignResident varchar(2), -- 8
			CodeCityBranchOffice varchar(5), -- 9 y 10
			FirstLastName varchar(20), -- 11
			SecondLastName varchar(30), -- 12
			FirstName varchar(20), -- 13
			SecondName varchar(30), -- 14
			[entry] varchar(2), -- 15
			retirement varchar(2), -- 16
			TDE varchar(2), -- 17
			TAE varchar(2), -- 18
			TDP varchar(2), -- 19
			TAP varchar(2), -- 20
			VSP varchar(2), -- 21
			Correcciones varchar(2), -- 22
			VST varchar(2), -- 23
			SLN varchar(2), -- 24
			IGE varchar(2), -- 25
			LMA varchar(2), -- 26
			VAC varchar(2), -- 27
			AVP varchar(2), -- 28
			VCT varchar(2), -- 29
			IRL int, -- 30
			pensionAdministratorCode varchar(6), -- 31
			transferPensionAdministratorCode varchar(6), -- 32
			EPSCode varchar(6), -- 33
			transferEPSCode varchar(6), -- 34
			CCFCode varchar(6), -- 35
			pensionDays int, -- 36
			healthDays int, -- 37
			professionalRiskDays int, -- 38
			compensationFundDays int, -- 39
			BasicSalary numeric(18,0), -- 40
			integralSalary varchar(2), -- 41
			IBCPension numeric(18,0), -- 42
			IBCHealth numeric(18,0), -- 43
			IBCProfessionalRisk numeric(18,0), -- 44
			IBCCompensationFund numeric(18,0), -- 45
			rateContributionsPension numeric(6,3), -- 46
			ValuePension numeric(18,0), -- 47
			voluntaryContributionPensionValue numeric(18,0), -- 48
			voluntaryContributionPensionValuePatron numeric(18,0), -- 49
			TotalPensionContribution numeric(18,0), -- 50
			PensionSolidarityFundValueContribution numeric(18,0), -- 51
			PensionSolidarityFundValueContributionSubSistence numeric(18,0), -- 52
			ValueNotRetainedByVoluntaryContributions numeric(18,0), -- 53
			rateContributionsHealth decimal(6,3), -- 54
			ValueHealth numeric(18,0), -- 55
			valueAditionalUPC numeric(18,0), -- 56
			authorizationNumberDisability varchar(30), -- 57
			valueGeneralDisability numeric(18,0), -- 58
			authorizationNumberMaternityLicense varchar(15), -- 59
			valueMaternityLicense numeric(18,0), -- 60
			rateContributionProfessionalRisk decimal(9,5), -- 61
			WorkCenter varchar(20), -- 62
			ValueContributionProfessionalRisk numeric(18,0), -- 63
			rateContributorCCF decimal(6,3), -- 64
			ValueContributionCCF numeric(18,0), -- 65
			rateContributorSENA decimal(6,3), -- 66
			ValueSena numeric(18,0), -- 67
			rateContributionICBF decimal(6,3), -- 68
			ValueICBF numeric(18,0), -- 69
			rateContributorESAP decimal(6,2), -- 70
			rateContributorEducationMinistry DECIMAL(7,2), -- 72
			MinistryCodeRiskFound varchar(6), -- 77
			ProfessionalRiskCode varchar(6), -- 78
			TarifaEspecialPensiones varchar(1), -- 79
			IngressDate date, -- 80
			DateRetirement date, -- 81
			SanctionInitialDate date, -- 83
			SanctionEndDate date, -- 84
			AmbulatoryDisabilityInitialDate date, -- 85
			AmbulatoryDisabilityEndDate date, -- 86
			MaternityLeaveInitialDate date, -- 87
			MaternityLeaveEndDate date, -- 88
			VacationInitialDate date, --89
			VacationEndDate date, -- 90
			FechaInicioVCT date, -- 91
			FechaFinVCT date, -- 92
			FechaInicioIRL date, -- 93
			FechaFinIRL date, -- 94
			IBCOtrosParafiscales numeric(18,0), -- 95
			TotalHours int, -- 96
			FechaEmpleadoExterior date, -- 97
			EconomicActivityARL varchar(7), --98
			FlagVacation bit,
			FlagLMA bit,
			FlagIRL bit,
			FlagIGE bit,
			FlagSanction bit
			)

		

		declare @TmpAutoliquidationNovelty table
		(IdEmployee int,
		IdContract int,
		SLN varchar(2), -- 24
		IGE varchar(2), -- 25
		LMA varchar(2), -- 26
		VAC varchar(2), -- 27
		IRL int, -- 30
		pensionDays int, -- 36
		healthDays int, -- 37
		professionalRiskDays int, -- 38
		compensationFundDays int, -- 39
		IBCPension numeric(18,0), -- 42
		IBCHealth numeric(18,0), -- 43
		IBCProfessionalRisk numeric(18,0), -- 44
		IBCCompensationFund numeric(18,0), -- 45
		rateContributionsPension numeric(6,3), -- 46
		ValuePension numeric(18,0), -- 47
		TotalPensionContribution numeric(18,0), -- 50
		PensionSolidarityFundValueContribution numeric(18,0), -- 51
		PensionSolidarityFundValueContributionSubSistence numeric(18,0), -- 52
		rateContributionsHealth decimal(6,3), -- 54
		ValueHealth numeric(18,0), -- 55
		authorizationNumberDisability varchar(30), -- 57
		valueGeneralDisability numeric(18,0), -- 58
		authorizationNumberMaternityLicense varchar(15), -- 59
		valueMaternityLicense numeric(18,0), -- 60
		rateContributionProfessionalRisk decimal(9,5), -- 61
		ValueContributionProfessionalRisk numeric(18,0), -- 63
		rateContributorCCF decimal(6,3), -- 64
		ValueContributionCCF numeric(18,0), -- 65
		rateContributorSENA decimal(6,3), -- 66
		ValueSena numeric(18,0), -- 67
		rateContributionICBF decimal(6,3), -- 68
		ValueICBF numeric(18,0), -- 69
		ProfessionalRiskCode varchar(6), -- 78
		SanctionInitialDate date, -- 83
		SanctionEndDate date, -- 84
		AmbulatoryDisabilityInitialDate date, -- 85
		AmbulatoryDisabilityEndDate date, -- 86
		MaternityLeaveInitialDate date, -- 87
		MaternityLeaveEndDate date, -- 88
		VacationInitialDate date, --89
		VacationEndDate date, -- 90
		FechaInicioIRL date, -- 93
		FechaFinIRL date, -- 94
		IBCOtrosParafiscales numeric(18,0) -- 95
		)

		declare @TmpAutoliquidationRetirement table
		(IdEmployee int,
		IdContract int,
		retirementDate date,
		typeDocument varchar(2),
		Nit varchar(18), 
		typeContractEmployee varchar(2), 
		subTypeEmployee varchar(2), 
		foreignNotBound varchar(2), 
		colombianForeignResident varchar(2), 
		CodeCityBranchOffice varchar(5), 
		FirstLastName varchar(20), 
		SecondLastName varchar(30), 
		FirstName varchar(20), 
		SecondName varchar(30),
		pensionDays int,
		healthDays int, 
		professionalRiskDays int, 
		compensationFundDays int, 
		IBCPension numeric(18,0), 
		IBCHealth numeric(18,0), 
		IBCProfessionalRisk numeric(18,0), 
		IBCCompensationFund numeric(18,0), 
		rateContributionsPension numeric(6,3), 
		ValuePension numeric(18,0), 
		TotalPensionContribution numeric(18,0), 
		PensionSolidarityFundValueContribution numeric(18,0), 
		PensionSolidarityFundValueContributionSubSistence numeric(18,0), 
		rateContributionsHealth decimal(6,3),
		ValueHealth numeric(18,0), 
		rateContributionProfessionalRisk decimal(9,5), 
		ValueContributionProfessionalRisk numeric(18,0), 
		rateContributorCCF decimal(6,3), 
		ValueContributionCCF numeric(18,0), 
		rateContributorSENA decimal(6,3), 
		ValueSena numeric(18,0), 
		rateContributionICBF decimal(6,3), 
		ValueICBF numeric(18,0), 
		ProfessionalRiskCode varchar(6), 
		IBCOtrosParafiscales numeric(18,0),
		BasicSalary NUMERIC(18,0),
		EPSCode varchar(6),
		CCFCode varchar(6),
		pensionAdministratorCode varchar(6),
		MinistryCodeRiskFound varchar(6),
		WorkCenter varchar(20),
		TarifaEspecialPensiones varchar(1),
		IntegralSalary varchar(2)
		)

		INSERT INTO @TmpAutoliquidationDetailFortnightly(
					IdEmployee,
					TotalNumberHours
		)

		SELECT 
		DISTINCT
		L.EmployeeId,
		ISNULL(SUM(LD.TotalNumberHours),0)
		FROM Payroll.Liquidation L
		INNER JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
		INNER JOIN Payroll.Concept C ON C.Id = LD.ConceptId
		WHERE L.PayrollDateLiquidated BETWEEN @PayrollStarDate AND @PayrollEndDate
		AND (C.ConceptClass IN('001','012','013','042','043','051','052'))
		GROUP BY L.EmployeeId
		
		INSERT INTO @TmpAutoliquidationFortnightly(
					IdEmployee,
					IdContract,
					IBCPension,
					IBCHealth,
					WorkDays,
					DaysVacation,
					DaysMaternityLeave,
					InitialDateOccupationalRisksDisability,
					DaysAmbulatoryDisability,
					DaysSanction,
					DaysUnpaidLicenseDays,
					VoluntaryContributionPensionValue,
					PensionSolidarityFundValueContribution
		)

		SELECT 
		DISTINCT
		L.EmployeeId, 
		L.ContractId,
		SUM(L.PensionJCB),
		SUM(L.HealthJCB),
		SUM(L.DaysWorked),
		SUM(L.VacationDays),
		SUM(L.MaternityLeaveDays),
		L.OccupationalRisksDisabilityInitialDate,
		SUM(L.AmbulatoryDisabilityDays),
		SUM(L.SanctionDays),
		SUM(L.UnpaidLicenseDays),
		SUM(L.VoluntaryContributionPensionValue),
		CASE
		WHEN SUM(L.PensionSolidarityFundValueContribution ) > 0 THEN CEILING((SUM(L.PensionJCB) * 0.01) / 2 / 100.0) * 100
		ELSE 0
		END
		FROM Payroll.Liquidation L
		WHERE L.PayrollDateLiquidated BETWEEN @PayrollStarDate AND @PayrollEndDate
		GROUP BY 
		L.EmployeeId, 
		L.ContractId,
		L.OccupationalRisksDisabilityInitialDate

		INSERT INTO @TmpAutoliquidation(
					IdEmployee,
					IdContract,
					sequenceDetail, -- 2
					typeDocument,
					Nit,
					typeContractEmployee,
					subTypeEmployee,
					foreignNotBound,
					colombianForeignResident,
					CodeCityBranchOffice,
					FirstLastName,
					SecondLastName,
					FirstName,
					SecondName,
					[entry],
					retirement, --16
					TDE,
					TAE,
					TDP,
					TAP,
					VSP,
					Correcciones,
					VST,
					SLN,
					IGE,
					LMA,
					VAC,
					AVP,
					VCT,
					IRL,
					pensionAdministratorCode,
					transferPensionAdministratorCode,
					EPSCode,
					transferEPSCode,
					CCFCode,
					pensionDays,
					healthDays,
					professionalRiskDays,
					compensationFundDays,
					BasicSalary,
					integralSalary,
					IBCPension,
					IBCHealth,
					IBCProfessionalRisk,
					IBCCompensationFund,
					rateContributionsPension,--46
					ValuePension, 
					voluntaryContributionPensionValue,
					voluntaryContributionPensionValuePatron,
					TotalPensionContribution,
					PensionSolidarityFundValueContribution,
					PensionSolidarityFundValueContributionSubSistence,
					ValueNotRetainedByVoluntaryContributions,
					rateContributionsHealth,--54
					ValueHealth,
					valueAditionalUPC,
					authorizationNumberDisability,
					valueGeneralDisability,
					authorizationNumberMaternityLicense,
					valueMaternityLicense,
					rateContributionProfessionalRisk,
					WorkCenter,
					ValueContributionProfessionalRisk,
					rateContributorCCF,
					ValueContributionCCF,
					rateContributorSENA,
					ValueSena,
					rateContributionICBF,
					ValueICBF,
					rateContributorESAP,
					rateContributorEducationMinistry,
					MinistryCodeRiskFound,
					ProfessionalRiskCode,
					TarifaEspecialPensiones,
					IngressDate,
					DateRetirement,
					SanctionInitialDate,
					SanctionEndDate,
					AmbulatoryDisabilityInitialDate,
					AmbulatoryDisabilityEndDate,
					MaternityLeaveInitialDate,
					MaternityLeaveEndDate,
					VacationInitialDate,
					VacationEndDate,
					FechaInicioVCT,
					FechaFinVCT,
					FechaInicioIRL,
					FechaFinIRL,
					IBCOtrosParafiscales,
					TotalHours,
					FechaEmpleadoExterior,
					EconomicActivityARL,
					FlagVacation,
					FlagLMA, 
					FlagIRL,
					FlagIGE,
					FlagSanction)

		SELECT 
		DISTINCT
		TLF.IdEmployee, 
		TLF.IdContract, 
		0, -- 2
		CASE P.IdentificationType 
			WHEN 0 THEN 'CC'
			WHEN 1 THEN 'CE'
			WHEN 2 THEN 'TI'
			WHEN 3 THEN 'RC'
			WHEN 4 THEN 'PA'
		END, -- 3
		TP.Nit, -- 4
		ET.EmployeeClass, -- 5
		'00', -- 6
		' ',  -- 7
		CASE e.ContributorAbroad
			WHEN 0 THEN ' '
			WHEN 1 THEN 'X'
		END,
		CityBranch.Code, -- 9 y 10
		P.FirstLastName, -- 11
		P.SecondLastName, -- 12
		p.FirstName, -- 13
		P.SecondName, -- 14
		CASE
			WHEN CONT.JobBondingDate >= @PayrollStarDate AND CONT.JobBondingDate <= @PayrollEndDate THEN 'X'
			ELSE ' '
		END, -- 15
		CASE
			WHEN CONT.RetirementDate >= @PayrollStarDate AND CONT.RetirementDate <= @PayrollEndDate THEN 'X'
			ELSE ' '
		END, -- 16
		' ', -- 17
		' ', -- 18
		' ', -- 19
		' ', -- 20
		' ', -- 21
		' ', -- 22
		CASE
			WHEN TLDF.TotalNumberHours > 0 THEN 'X'
			ELSE ' '
		END, -- 23,
		' ', -- 24
		' ', --25
		' ', -- 26
		' ', -- 27
		CASE
			WHEN TLF.VoluntaryContributionPensionValue > 0 THEN 'X'
			ELSE ' '
		END, -- 28
		' ', -- 29
		' ', -- 30
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.FundType = 2 AND VoluntaryContribution = 0), 'SINAFP'), -- 31
		' ', -- 32
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.FundType = 1 AND VoluntaryContribution = 0), 'SINEPS'), -- 33
		' ', -- 34
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.FundType = 5 AND VoluntaryContribution = 0), 'SINCCF'), -- 35

		TLF.WorkDays, -- 36
		TLF.WorkDays, -- 37
		TLF.WorkDays, -- 38
		TLF.WorkDays, -- 39
		CONT.BasicSalary, -- 40
		CASE 
			WHEN CT.SalaryType = 1 AND @PayrollStarDate > '2020-07-01' THEN 'F'
			WHEN CT.SalaryType = 2 THEN 'X'
			WHEN CT.SalaryType = 3 AND @PayrollStarDate > '2020-07-01' THEN 'V'
			ELSE ' '
		END, -- 41
		TLF.IBCPension, -- 42
		TLF.IBCHealth, -- 43
		TLF.IBCPension, -- 44
		TLF.IBCPension, -- 45
		(PP.EmployeePensionContributionPercentage + PP.EmployerPensionContributionPercentage) / 100, -- 46 	
		CEILING(((TLF.IBCPension) * ((PP.EmployeePensionContributionPercentage + PP.EmployerPensionContributionPercentage) / 100)) / 100) * 100, -- 47
		ISNULL(TLF.VoluntaryContributionPensionValue,0), -- 48
		0, -- 49
		CEILING(((TLF.IBCPension) * ((PP.EmployeePensionContributionPercentage + PP.EmployerPensionContributionPercentage) / 100)) + ISNULL(SUM(TLF.VoluntaryContributionPensionValue),0)) / 100 * 100, --50
		CASE
			WHEN CONT.BasicSalary > (25 * PP.LegalSalaryMinimum) THEN CEILING((SUM(25 * PP.LegalSalaryMinimum) * 0.01) / 2) / 100 * 100
			ELSE TLF.PensionSolidarityFundValueContribution
		End, -- 51
		CASE
			WHEN TLF.PensionSolidarityFundValueContribution > 0 AND TLF.IBCPension < (PP.LegalSalaryMinimum * 16) THEN PensionSolidarityFundValueContribution
			WHEN TLF.PensionSolidarityFundValueContribution > 0 AND TLF.IBCPension >= (PP.LegalSalaryMinimum * 16) THEN Payroll.[fnCalculateSolidarityFundSubsistence] (PP.LegalSalaryMinimum, CONT.BasicSalary, TLF.IBCPension)
			ELSE 0
		END, -- 52,
		0, -- 53
		CASE
			WHEN (CT.ContractClass = 2 OR CT.ContractClass = 5) AND CONT.BasicSalary <= PP.LegalSalaryMinimum THEN (PP.EmployerHealthContributionPercentage) / 100		
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) THEN (PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) THEN (PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary < (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) THEN (PP.EmployeeHealthContributionPercentage) / 100		
		END, -- 54
		CASE 
			WHEN (CT.ContractClass = 2 OR CT.ContractClass = 5) AND CONT.BasicSalary <= PP.LegalSalaryMinimum THEN CEILING(((TLF.IBCHealth) * ((PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5)THEN CEILING(((TLF.IBCHealth) * ((PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) THEN CEILING(((TLF.IBCHealth) * ((PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary < (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) THEN CEILING(((TLF.IBCHealth) * ((PP.EmployeeHealthContributionPercentage) / 100)) / 100) * 100			
		END, -- 55
		0, -- 56
		'', -- 57
		0, -- 58
		'', -- 59
		0, -- 60
		E.ProfessionalRiskPercentage / 100, -- 61 
		LTRIM(RTRIM(WC.Code)), -- 62
		CEILING((TLF.IBCPension * (E.ProfessionalRiskPercentage / 100)) / 100) * 100, -- 63
		pp.CompensationFundContributionPercentage / 100, -- 64
		CEILING((TLF.IBCPension * (pp.CompensationFundContributionPercentage / 100)) / 100) * 100, -- 65
		CASE
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 THEN PP.SenaContributionPercentage / 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND CT.ContractClass <> 2 THEN PP.SenaContributionPercentage / 100
			ELSE 0
		END, -- 66
		CASE
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 THEN CEILING(((PP.SenaContributionPercentage / 100) * TLF.IBCPension)) / 100 * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND CT.ContractClass <> 2 THEN CEILING((PP.SenaContributionPercentage / 100) * TLF.IBCPension) / 100 * 100
			ELSE 0
		END, -- 67
		CASE
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 THEN PP.ICBFContributionPercentage / 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND CT.ContractClass <> 2 THEN PP.ICBFContributionPercentage / 100
			ELSE 0
		END, -- 68
		CASE
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 THEN CEILING(((PP.ICBFContributionPercentage / 100) * TLF.IBCPension)) / 100 * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND CT.ContractClass <> 2 THEN CEILING((PP.ICBFContributionPercentage / 100) * TLF.IBCPension) / 100 * 100
			ELSE 0
		END, -- 69 
		0, -- 70
		0, -- 72
		ISNULL((SELECT top 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND F.State = 1 AND F.Risk = 1 AND FC.FundType = 4 AND VoluntaryContribution = 0), 'SINARL'), -- 77		
		pr.Code, --78
		 PP.SpecialPensionRateIndicator, -- 79
		CASE
			WHEN CONT.JobBondingDate >= @PayrollStarDate AND CONT.JobBondingDate <= @PayrollEndDate THEN CONT.JobBondingDate
			ELSE NULL
		END, -- 80
		CASE
			WHEN CONT.RetirementDate >= @PayrollStarDate AND CONT.RetirementDate <= @PayrollEndDate AND CT.ContractClass = 2 THEN CONT.RetirementDate
			ELSE NULL
		END, -- 81
		NULL, -- 83
		NULL, --84
		NULL, -- 85
		NULL, -- 86
		NULL, --87
		NULL, -- 88
		NULL, -- 89
		NULL, --90
		NULL, --91
		NULL, --92
		NULL, --93
		NULL, --94
		CASE
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 THEN TLF.IBCPension
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND CT.ContractClass <> 2 THEN TLF.IBCPension
			ELSE 0
		END, -- 95
		CASE
			WHEN ((CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT)) + (TLDF.TotalNumberHours)) > (30 * CONT.HoursDaily) THEN (30 * CONT.HoursDaily)
			WHEN TLDF.TotalNumberHours > 0 THEN (CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT)) + (TLDF.TotalNumberHours)
			WHEN TLDF.TotalNumberHours <= 0 THEN (CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT))
		END, -- 96
		CASE e.ContributorAbroad
			WHEN 0 THEN NULL
			WHEN 1 THEN e.DateFilingAbroad
		END, -- 97,
		CASE 
		WHEN @PayrollStarDate >= '2022-11-01' THEN ISNULL(PR.ArlCode,0)
		ELSE 0
		END, --98
		 
		CASE
			WHEN TLF.DaysVacation > 0 THEN 1
			ELSE 0
		END,
		CASE
			WHEN TLF.DaysMaternityLeave > 0 THEN 1
			ELSE 0
		END,
		CASE
			WHEN TLF.InitialDateOccupationalRisksDisability IS NOT NULL THEN 1
			ELSE 0
		END,
		CASE
			WHEN TLF.DaysAmbulatoryDisability > 0 THEN 1
			ELSE 0
		END,
		CASE
			WHEN (TLF.DaysSanction + TLF.DaysUnpaidLicenseDays) > 0 THEN 1
			ELSE 0
		END
		FROM Payroll.Employee E
		INNER JOIN Payroll.EmployeeType ET ON ET.Id = E.EmployeeTypeId													
		INNER JOIN @TmpAutoliquidationFortnightly TLF  ON E.Id = TLF.IdEmployee
		INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
		INNER JOIN Common.Person P ON P.Id = TP.PersonId
		INNER JOIN Payroll.Contract CONT ON CONT.ID = TLF.IdContract
		INNER JOIN Payroll.WorkCenter WC ON WC.Id = E.WorkCenterId
		INNER JOIN Payroll.ContractType CT ON CT.Id = CONT.ContractTypeId
		INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = CONT.FunctionalUnitId
		INNER JOIN Payroll.BranchOffice BA ON BA.ID = FU.BranchOfficeId
		INNER JOIN Common.City CityBranch ON CityBranch.Id = BA.CityId
		INNER JOIN Payroll.Position POS ON POS.Id = CONT.PositionId
		INNER JOIN Payroll.ProfessionalRisk PR ON PR.Id = pos.ProfessionalRiskLevelId
		LEFT JOIN Payroll.[Group] G ON G.Id = CONT.GroupId
		LEFT JOIN Payroll.PayrollParameter PP ON PP.ID = g.PayrollParameterId
		LEFT JOIN @TmpAutoliquidationDetailFortnightly TLDF ON E.Id = TLDF.IdEmployee
		WHERE
		WC.Code = @WorkCenterCode
		GROUP BY
		TLF.IdEmployee,
		TLF.IdContract,
		P.IdentificationType,
		TP.Nit,
		CT.ContractClass,
		ET.EmployeeClass,
		E.PensionaryStatus,
		CityBranch.Code,
		P.FirstLastName,
		P.SecondLastName,
		p.FirstName, 
		P.SecondName,
		CONT.JobBondingDate, 
		CONT.RetirementDate,
		CONT.Id,
		TLF.WorkDays,
		CONT.BasicSalary, 
		CT.SalaryType,
		TLF.IBCPension,
		TLF.IBCHealth, 
		PP.EmployeePensionContributionPercentage,
		PP.EmployerPensionContributionPercentage,
		PP.LegalSalaryMinimum,
		PP.EmployeeHealthContributionPercentage, 
		PP.EmployerHealthContributionPercentage, 
		E.ProfessionalRiskPercentage, 
		WC.Code,
		PP.SenaContributionPercentage,
		PP.CompensationFundContributionPercentage,
		PP.ICBFContributionPercentage,
		pr.Code,
		CONT.HoursDaily,
		TLF.InitialDateOccupationalRisksDisability,
		TLF.VoluntaryContributionPensionValue,
		TLF.PensionSolidarityFundValueContribution,
		TLF.DaysVacation,
		TLF.DaysMaternityLeave,
		TLF.DaysAmbulatoryDisability,
		TLF.DaysSanction,
		TLF.DaysUnpaidLicenseDays,
		TLDF.TotalNumberHours,
		PR.ArlCode,
		pp.SpecialPensionRateIndicator,
	    e.ContributorAbroad,
		e.DateFilingAbroad
		
		
		DECLARE @IdEmployee int
		DECLARE @IdContract int
		DECLARE @FlagIGE BIT
		DECLARE @FlagIRL BIT
		DECLARE @FlagLMA BIT
		DECLARE @FlagSanction BIT
		DECLARE @FlagVacation BIT
		DECLARE @IdLiquidation int
		DECLARE @PensionSolidarityFundValueContribution NUMERIC(18,0)
		DECLARE @PensionSolidarityFundValueContributionSubSistence NUMERIC(18,0)
		DECLARE @RatePension decimal(6,3)
		DECLARE @RateHealth decimal (6,3)
		DECLARE @rateContributionProfessionalRisk decimal(9,5)
		DECLARE @RateSena decimal(6,3)
		DECLARE @RateICBF decimal(6,3)
		DECLARE @RateCompensationFund decimal(6,3)
		DECLARE @ProfessionalRiskCode VARCHAR(6)
		
		declare C_Empleados cursor for	
		SELECT IdEmployee, IdContract, FlagIGE, FlagIRL, FlagLMA, FlagSanction, FlagVacation, PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsPension, rateContributionsHealth,
		rateContributionProfessionalRisk, rateContributorCCF, rateContributorSENA, rateContributionICBF, ProfessionalRiskCode
		FROM @TmpAutoliquidation
		
		open C_Empleados

			fetch next from C_Empleados into @IdEmployee, @IdContract, @FlagIGE, @FlagIRL, @FlagLMA, @FlagSanction, @FlagVacation, @PensionSolidarityFundValueContribution, @PensionSolidarityFundValueContributionSubSistence, @RatePension, @RateHealth,
			@rateContributionProfessionalRisk, @RateCompensationFund, @RateSena, @RateICBF, @ProfessionalRiskCode
			while @@FETCH_STATUS = 0 begin
				
				-- VACACIONES
				IF @FlagVacation = 1 BEGIN
					
					DECLARE @VacationInitialDate DATE
					DECLARE @healthVacationDays INT
					DECLARE @VacationEndDate DATE
					DECLARE @IBCVacationHealth NUMERIC(18,0)
					DECLARE @ValueVacationPension NUMERIC(18,0)
					DECLARE @ValueVacationHealth NUMERIC(18,0)
					DECLARE @ValueVacationSENA NUMERIC(18,0)
					DECLARE @ValueVacationICBF NUMERIC(18,0)
					DECLARE @ValueVacationCCF NUMERIC(18,0)
					
					SELECT @VacationInitialDate = VacationInitialDate
					FROM Payroll.Liquidation where Id = @IdLiquidation

					DECLARE @HealthValue DECIMAL(18,0)
					DECLARE @EnjoyDays int
					DECLARE @NotCount int = 0

					SELECT @HealthValue = V.HealthContribution, @EnjoyDays = V.EnjoyDays, @VacationEndDate = IIF(v.IncorporationDateReal = DATEFROMPARTS(0001, 1, 1), DATEFROMPARTS(0001, 1, 1), DATEADD(DAY,-1, v.IncorporationDateReal))
					FROM Payroll.Vacation V, Payroll.VacationPeriod VP
					WHERE VP.EmployeeId = @IdEmployee AND V.VacationPeriodId = VP.Id 
					AND V.VacationStartDate = @VacationInitialDate

					if MONTH(@VacationInitialDate) <> MONTH(@VacationEndDate) BEGIN
						if MONTH(@VacationInitialDate) in (1,3,5,7,8,10,12) BEGIN
							SET @NotCount = 1
						END
					END

					SET @EnjoyDays = @EnjoyDays - @NotCount

					IF @VacationInitialDate < @PayrollStarDate BEGIN
						SET @VacationInitialDate = @PayrollStarDate
					END

					IF @VacationEndDate > @PayrollEndDate BEGIN
						SET @VacationEndDate = @PayrollEndDate
					END

					SET @healthVacationDays = [Payroll].[fnCalculateDays360] (@VacationInitialDate, @VacationEndDate)

					SET @HealthValue = (@HealthValue * 30) / @EnjoyDays

					SET @IBCVacationHealth = (((@HealthValue * 100) / 4) * @healthVacationDays) / 30
					
					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IBCVacationHealth * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IBCVacationHealth * 0.01) / 2) / 100) * 100
						END
					END

					SET @ValueVacationPension = @IBCVacationHealth * @RatePension 
					SET @ValueVacationPension = CEILING(@ValueVacationPension / 100) * 100

					SET @ValueVacationHealth = @IBCVacationHealth * @RateHealth
					SET @ValueVacationHealth = CEILING(@ValueVacationHealth / 100) * 100

					SET @ValueVacationSENA = @IBCVacationHealth * @RateSena
					SET @ValueVacationSENA = CEILING(@ValueVacationSENA / 100) * 100

					SET @ValueVacationICBF = @IBCVacationHealth * @RateICBF
					SET @ValueVacationICBF = CEILING(@ValueVacationICBF / 100) * 100

					SET @ValueVacationCCF = @IBCVacationHealth * @RateCompensationFund
					SET @ValueVacationCCF = CEILING(@ValueVacationCCF / 100) * 100
					
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', 'X', 0, @healthVacationDays, @healthVacationDays, @healthVacationDays, @healthVacationDays,
							@IBCVacationHealth, @IBCVacationHealth, @IBCVacationHealth, @IBCVacationHealth, @RatePension, @ValueVacationPension, @ValueVacationPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueVacationHealth,
							'',0,'',0, 0,
							0, @RateCompensationFund, @ValueVacationCCF, @RateSena, @ValueVacationSENA, @RateICBF, @ValueVacationICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							@VacationInitialDate, @VacationEndDate, NULL, NULL, 0)

				END
				 

				IF @FlagLMA = 1 BEGIN
					
					DECLARE @MaternityInitialDate DATE
					DECLARE @MaterninyEndDate DATE
					DECLARE @ValueMaternity NUMERIC(18,0)
					DECLARE @IBCMaternityHealth NUMERIC(18,0)
					DECLARE @ValueMaternityPension NUMERIC(18,0)
					DECLARE @ValueMaternityHealth NUMERIC(18,0)
					DECLARE @ValueMaternitySENA NUMERIC(18,0)
					DECLARE @ValueMaternityICBF NUMERIC(18,0)
					DECLARE @ValueMaternityCCF NUMERIC(18,0)
					DECLARE @MaternityHealthDays INT 
					DECLARE @AuthorizationNumberMaternity varchar(15)
					
					SELECT @MaternityInitialDate = MaternityLeaveInitialDate, @MaterninyEndDate = MaternityLeaveEndDate, @AuthorizationNumberMaternity = MaternityLeaveAutorizationNumber
					FROM Payroll.Liquidation where Id = @IdLiquidation

					IF @MaternityInitialDate < @PayrollStarDate BEGIN
						SET @MaternityInitialDate = @PayrollStarDate
					END

					IF @MaterninyEndDate > @PayrollEndDate BEGIN
						SET @MaterninyEndDate = @PayrollEndDate
					END

					SET @MaternityHealthDays = [Payroll].[fnCalculateDays360] (@MaternityInitialDate, @MaterninyEndDate)

					SELECT @ValueMaternity = ISNULL(SUM(ConceptTotalValue), 0) FROM Payroll.LiquidationDetail WHERE PayrollId = @IdLiquidation and ConceptClass = '023'

					IF @ValueMaternity IS NULL OR @ValueMaternity = 0 BEGIN
						SELECT @ValueMaternity = ISNULL(SUM(ConceptTotalValue), 0) FROM Payroll.LiquidationDetail WHERE PayrollId = @IdLiquidation and ConceptCode = '013'
					END

					SET @IBCMaternityHealth = @ValueMaternity

					SET @ValueMaternityPension = @IBCMaternityHealth * @RatePension
					SET @ValueMaternityPension = CEILING(@ValueMaternityPension / 100) * 100

					SET @ValueMaternityHealth = @IBCMaternityHealth * @RateHealth
					SET @ValueMaternityHealth = CEILING(@ValueMaternityHealth / 100) * 100

					SET @ValueMaternitySENA = @IBCMaternityHealth * @RateSena
					SET @ValueMaternitySENA = CEILING(@ValueMaternitySENA / 100) * 100

					SET @ValueMaternityICBF = @IBCMaternityHealth * @RateICBF
					SET @ValueMaternityICBF = CEILING(@ValueMaternityICBF / 100) * 100

					SET @ValueMaternityCCF = @IBCMaternityHealth * @RateCompensationFund
					SET @ValueMaternityCCF = CEILING(@ValueMaternityCCF / 100) * 100

					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IBCMaternityHealth * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IBCMaternityHealth * 0.01) / 2) / 100) * 100
						END
					END
					
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', 'X', ' ', 0, @MaternityHealthDays, @MaternityHealthDays, @MaternityHealthDays, @MaternityHealthDays,
							@IBCMaternityHealth, @IBCMaternityHealth, @IBCMaternityHealth, @IBCMaternityHealth, @RatePension, @ValueMaternityPension, @ValueMaternityPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueMaternityHealth,
							'',0,@AuthorizationNumberMaternity,@ValueMaternity, 0,
							0, @RateCompensationFund, @ValueMaternityCCF, @RateSena, @ValueMaternitySENA, @RateICBF, @ValueMaternityICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, @MaternityInitialDate, @MaterninyEndDate,
							NULL, NULL, NULL, NULL, 0)

				END
				---SANCIONES O LICENCIAS NO REMUNERADAS
					
				IF @FlagSanction = 1 BEGIN
					DECLARE @SanctionInitialDate DATE
					DECLARE @SanctionEndDate DATE
					DECLARE @IBCSanctionHealth NUMERIC(18,0)
					DECLARE @ValueSanctionPension NUMERIC(18,0)
					DECLARE @ValueSanctionHealth NUMERIC(18,0)
					DECLARE @ValueSanctionSENA NUMERIC(18,0)
					DECLARE @ValueSanctionICBF NUMERIC(18,0)
					DECLARE @ValueSanctionCCF NUMERIC(18,0)
					DECLARE @SanctionHealthDays INT 

					DECLARE @IbcSancion NUMERIC(18,0) = 0
					DECLARE @IdNovelty int
					DECLARE @TypeNovelty tinyint
					DECLARE @EmployeeBaseSalary NUMERIC(18,0)
					DECLARE @IBCTable NUMERIC(18,0)
					DECLARE @RateContributionsHealth DECIMAL(6,3)
					DECLARE @RateContributionsPension DECIMAL(6,3)
					DECLARE @MinimumSalary NUMERIC(18,0)
					DECLARE @CreeTaxL BIT
					DECLARE @BasicSalaryL NUMERIC
					

					SELECT @SanctionInitialDate = SanctionInitialDate, @SanctionEndDate = SanctionEndDate
					FROM Payroll.Liquidation where Id = @IdLiquidation

					IF @SanctionInitialDate IS NULL BEGIN
						SELECT @SanctionInitialDate = UnpaidLicenseInitialDate, @SanctionEndDate = UnpaidLicenseEndDate
						FROM Payroll.Liquidation where Id = @IdLiquidation
					END
	
					SELECT @EmployeeBaseSalary = EmployeeBaseSalary, @IBCTable = IBC, @TypeNovelty = TypeNovelty 
					FROM Payroll.Novelty 
					where EmployeeId = @IdEmployee and RealDate = @SanctionInitialDate

					Select @RateContributionsHealth = PP.EmployerHealthContributionPercentage/100,
					@rateContributionsPension= PP.EmployerPensionContributionPercentage/100,
					@MinimumSalary = PP.LegalSalaryMinimum,
					@BasicSalaryL = con.BasicSalary
					FROM Payroll.PayrollParameter PP 
					LEFT JOIN Payroll.[Group] G ON G.PayrollParameterId = PP.Id
					LEFT JOIN Payroll.Contract CON on CON.GroupId = G.Id
					LEFT JOIN Payroll.Employee E ON E.Id = CON.EmployeeId
					where e.Id = @IdEmployee AND CON.ID = @IdContract
					--Borrar
					SELECT @CreeTaxL = CreeTax FROM Payroll.PayrollSettings

					IF @EmployeeBaseSalary > 0 AND  @IBCTable = 0 BEGIN
						SET @IbcSancion = @EmployeeBaseSalary
					END

					IF @EmployeeBaseSalary = 0 AND  @IBCTable > 0 BEGIN
						SET @IbcSancion = @IBCTable
					END

					IF @EmployeeBaseSalary > 0 AND  @IBCTable > 0 BEGIN
						SET @IbcSancion = @EmployeeBaseSalary
					END
					
					IF @SanctionInitialDate < @PayrollStarDate BEGIN
						SET @SanctionInitialDate = @PayrollStarDate
					END

					IF @SanctionEndDate > @PayrollEndDate BEGIN
						SET @SanctionEndDate = @PayrollEndDate
					
					END

					
					SET @IBCSanctionHealth = @IbcSancion

					SET @SanctionHealthDays = [Payroll].[fnCalculateDays360] (@SanctionInitialDate, @SanctionEndDate)

					SET @RatePension = @RateContributionsPension

					SET @ValueSanctionPension = @IbcSancion * @RatePension
					SET @ValueSanctionPension = CEILING(@ValueSanctionPension / 100) * 100

					----------------------------------------------------------
					IF (@MinimumSalary*10) > @BasicSalaryL  AND @CreeTaxL= 1  BEGIN 

						SET @RateHealth = 0.00
					END
						ELSE 
						BEGIN 
					
						Set @RateHealth = @RateContributionsHealth  

					END					
					--------------------------------------------------------

					SET @ValueSanctionHealth = 0
					SET @ValueSanctionHealth = CEILING(@ValueSanctionHealth / 100) * 100

					SET @ValueSanctionSENA = 0
					SET @ValueSanctionSENA = CEILING(@ValueSanctionSENA / 100) * 100

					SET @ValueSanctionICBF = 0
					SET @ValueSanctionICBF = CEILING(@ValueSanctionICBF / 100) * 100

					SET @ValueSanctionICBF = 0
					SET @ValueSanctionICBF = CEILING(@ValueSanctionICBF / 100) * 100
					
					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IbcSancion * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IbcSancion * 0.01) / 2) / 100) * 100
						END
					END
					
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales)
					VALUES(@IdEmployee, @IdContract, 'X', ' ', ' ', ' ', 0, @SanctionHealthDays, @SanctionHealthDays, @SanctionHealthDays, @SanctionHealthDays,
							@IBCSanctionHealth, @IBCSanctionHealth, @IBCSanctionHealth, @IBCSanctionHealth, @RatePension, @ValueSanctionPension, @ValueSanctionPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueMaternityHealth,
							'',0,'',0, 0,
							0, 0, @ValueSanctionICBF, 0, @ValueSanctionSENA, 0, @ValueSanctionICBF,
							@ProfessionalRiskCode, @SanctionInitialDate, @SanctionEndDate, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, 0)

				END

				IF @FlagIRL = 1 BEGIN

					DECLARE @IRLInitialDate DATE
					DECLARE @IRLEndDate DATE
					DECLARE @ValueIRL NUMERIC(18,0)
					DECLARE @IBCIRLHealth NUMERIC(18,0)
					DECLARE @ValueIRLPension NUMERIC(18,0)
					DECLARE @ValueIRLHealth NUMERIC(18,0)
					DECLARE @ValueIRLSENA NUMERIC(18,0)
					DECLARE @ValueIRLICBF NUMERIC(18,0)
					DECLARE @ValueIRLCCF NUMERIC(18,0)
					DECLARE @IRLHealthDays INT 
					DECLARE @ConsultIRlDays INT
					
					SELECT @IRLInitialDate = OccupationalRisksDisabilityInitialDate, @IRLEndDate = OccupationalRisksDisabilityEndDate
					FROM Payroll.Liquidation where Id = @IdLiquidation

					SET @ConsultIRlDays = DATEDIFF(DAY, @IRLInitialDate, @IRLEndDate)

					IF @IRLInitialDate < @PayrollStarDate BEGIN
						SET @IRLInitialDate = @PayrollStarDate
					END

					IF @IRLEndDate > @PayrollEndDate BEGIN
						SET @IRLEndDate = @PayrollEndDate
					END

					IF @IRLEndDate < @PayrollStarDate BEGIN
						SET @IRLEndDate = DATEADD(DAY, @ConsultIRlDays, @IRLInitialDate)
					END

					SET @IRLHealthDays = [Payroll].[fnCalculateDays360] (@IRLInitialDate, @IRLEndDate)

					SELECT @ValueIRL = SUM(ConceptTotalValue) FROM Payroll.LiquidationDetail WHERE PayrollId = @IdLiquidation and ConceptClass = '027'

					IF @ValueIRL IS NULL BEGIN
						SET @ValueIRL = 0
					END

					SET @IBCIRLHealth = @ValueIRL
					 

					SET @ValueIRLPension = @IBCIRLHealth * @RatePension
					SET @ValueIRLPension = CEILING(@ValueIRLPension / 100) * 100

					SET @ValueIRLHealth = @IBCIRLHealth * @RateHealth
					SET @ValueIRLHealth = CEILING(@ValueIRLHealth / 100) * 100

					SET @ValueIRLSENA = @IBCIRLHealth * @RateSena
					SET @ValueIRLSENA = CEILING(@ValueIRLSENA / 100) * 100

					SET @ValueIRLICBF = @IBCIRLHealth * @RateICBF
					SET @ValueIRLICBF = CEILING(@ValueIRLICBF / 100) * 100

					SET @ValueIRLCCF = @IBCIRLHealth * @RateCompensationFund
					SET @ValueIRLCCF = CEILING(@ValueIRLCCF / 100) * 100

					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IBCIRLHealth * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IBCIRLHealth * 0.01) / 2) / 100) * 100
						END
					END
					
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', ' ', @IRLHealthDays, @IRLHealthDays, @IRLHealthDays, @IRLHealthDays, @IRLHealthDays,
							@IBCIRLHealth, @IBCIRLHealth, @IBCIRLHealth, @IBCIRLHealth, @RatePension, @ValueIRLPension, @ValueIRLPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueIRLHealth,
							'',0,'',0, 0,
							0, @RateCompensationFund, @ValueIRLCCF, @RateSena, @ValueIRLSENA, @RateICBF, @ValueIRLICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							NULL, NULL, @IRLInitialDate, @IRLEndDate, 0)
				END

				IF @FlagIGE = 1 BEGIN
					
					DECLARE @IGEInitialDate DATE
					DECLARE @IGEEndDate DATE
					DECLARE @ValueIGE NUMERIC(18,0)
					DECLARE @IBCIGEHealth NUMERIC(18,0)
					DECLARE @ValueIGEPension NUMERIC(18,0)
					DECLARE @ValueIGEHealth NUMERIC(18,0)
					DECLARE @ValueIGESENA NUMERIC(18,0)
					DECLARE @ValueIGEICBF NUMERIC(18,0)
					DECLARE @ValueIGECCF NUMERIC(18,0)
					DECLARE @IGEHealthDays INT 
					DECLARE @ConsultIGEDays INT
					DECLARE @AuthorizationNumberDisability varchar(30)

					DECLARE @CodeGroup VARCHAR(20)
					DECLARE @BasicSalary NUMERIC(18,0)
					DECLARE @LegalSalaryMininum NUMERIC(18,0)
					DECLARE @EmployerHealthContributionPercentage DECIMAL (6,3)
					
					SELECT @IGEInitialDate = AmbulatoryDisabilityInitialDate, @IGEEndDate = AmbulatoryDisabilityEndDate, 
					@AuthorizationNumberDisability = AmbulatoryDisabilityAuthorizationNumber
					FROM Payroll.Liquidation where Id = @IdLiquidation

					SET @ConsultIGEDays = DATEDIFF(DAY, @IGEInitialDate, @IGEEndDate)

					IF @IGEInitialDate < @PayrollStarDate BEGIN
						SET @IGEInitialDate = @PayrollStarDate
					END

					IF @IGEEndDate > @PayrollEndDate BEGIN
						SET @IGEEndDate = @PayrollEndDate
					END

					IF @IGEEndDate < @PayrollStarDate BEGIN
						SET @IGEEndDate = DATEADD(DAY, @ConsultIGEDays, @IGEInitialDate)
					END

					SET @IGEHealthDays = [Payroll].[fnCalculateDays360] (@IGEInitialDate, @IGEEndDate)

					SELECT @ValueIGE = SUM(ConceptTotalValue) FROM Payroll.LiquidationDetail WHERE PayrollId = @IdLiquidation and ConceptClass = '021'

					IF @ValueIGE IS NULL BEGIN
						SET @ValueIGE = 0
					END

					DECLARE @AjusteIncapacidad NUMERIC(18,0) = 0

					SELECT @AjusteIncapacidad = isnull(LD.ConceptTotalValue,0)
					FROM Payroll.LiquidationDetail LD
					where LD.PayrollDate = @PayrollEndDate and ld.PayrollId = @IdLiquidation
					and LD.ConceptCode = '1009'

					SET @ValueIGE = @ValueIGE + @AjusteIncapacidad

					SET @IBCIGEHealth = @ValueIGE

					SELECT @CodeGroup = g.Code, @BasicSalary = C.BasicSalary, @LegalSalaryMininum = PP.LegalSalaryMinimum, @EmployerHealthContributionPercentage = PP.EmployerHealthContributionPercentage 
					FROM Payroll.Contract C, Payroll.[Group] G, Payroll.PayrollParameter PP 
					where C.Id = @IdContract AND G.Id = C.GroupId AND PP.Id = G.PayrollParameterId

					IF @CodeGroup = '09' BEGIN

						SET @IGEInitialDate = @PayrollStarDate
						SET @IGEEndDate = @PayrollEndDate

						IF (@BasicSalary / 2) <= @LegalSalaryMininum begin
							SET @IBCIGEHealth = @LegalSalaryMininum
						END ELSE BEGIN
							SET @IBCIGEHealth = (@BasicSalary / 2)
						END

						SET @RateHealth = @EmployerHealthContributionPercentage
					END

					SET @ValueIGEPension = @IBCIGEHealth * @RatePension
					SET @ValueIGEPension = CEILING(@ValueIGEPension / 100) * 100

					SET @ValueIGEHealth = @IBCIGEHealth * @RateHealth
					SET @ValueIGEHealth = CEILING(@ValueIGEHealth / 100) * 100

					SET @ValueIGESENA = @IBCIGEHealth * @RateSena
					SET @ValueIGESENA = CEILING(@ValueIGESENA / 100) * 100

					SET @ValueIGEICBF = @IBCIGEHealth * @RateICBF
					SET @ValueIRLICBF = CEILING(@ValueIGEICBF / 100) * 100

					SET @ValueIGECCF = @IBCIGEHealth * @RateCompensationFund
					SET @ValueIGECCF = CEILING(@ValueIGECCF / 100) * 100

					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IBCIGEHealth * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IBCIGEHealth * 0.01) / 2) / 100) * 100
						END
					END
					
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales)
					VALUES(@IdEmployee, @IdContract, ' ', 'X', ' ', ' ', 0, @IGEHealthDays, @IGEHealthDays, @IGEHealthDays, @IGEHealthDays,
							@IBCIGEHealth, @IBCIGEHealth, @IBCIGEHealth, @IBCIGEHealth, @RatePension, @ValueIGEPension, @ValueIGEPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueIGEHealth,
							@AuthorizationNumberDisability,@ValueIGE,'',0, 0,
							0, @RateCompensationFund, @ValueIGECCF, @RateSena, @ValueIGESENA, @RateICBF, @ValueIGEICBF,
							@ProfessionalRiskCode, NULL, NULL, @IGEInitialDate, @IGEEndDate, NULL, NULL,
							NULL, NULL, NULL, NULL, 0)
					
				END

			fetch next from C_Empleados into @IdEmployee, @IdContract, @FlagIGE, @FlagIRL, @FlagLMA, @FlagSanction, @FlagVacation, @PensionSolidarityFundValueContribution, @PensionSolidarityFundValueContributionSubSistence, @RatePension, @RateHealth,
			@rateContributionProfessionalRisk, @RateCompensationFund, @RateSena, @RateICBF, @ProfessionalRiskCode
			end -- fin while de C_Empleados
		close C_Empleados 
		deallocate C_Empleados
	
		INSERT INTO @TmpAutoliquidation(IdEmployee, IdContract, sequenceDetail, typeDocument, Nit, typeContractEmployee, subTypeEmployee, foreignNotBound, colombianForeignResident, CodeCityBranchOffice, 	
					FirstLastName, SecondLastName, FirstName, SecondName, [entry], retirement, TDE, TAE, TDP, TAP, VSP, Correcciones, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRL, pensionAdministratorCode,
					transferPensionAdministratorCode, EPSCode, transferEPSCode, CCFCode, pensionDays, healthDays, professionalRiskDays, compensationFundDays, BasicSalary, integralSalary, IBCPension,
					IBCHealth, IBCProfessionalRisk, IBCCompensationFund, rateContributionsPension, ValuePension, voluntaryContributionPensionValue, voluntaryContributionPensionValuePatron,
					TotalPensionContribution, PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, ValueNotRetainedByVoluntaryContributions,
					rateContributionsHealth, ValueHealth, valueAditionalUPC, authorizationNumberDisability, valueGeneralDisability, authorizationNumberMaternityLicense,
					valueMaternityLicense, rateContributionProfessionalRisk, WorkCenter, ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF,
					rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF, rateContributorESAP, rateContributorEducationMinistry, MinistryCodeRiskFound,
					ProfessionalRiskCode,  TarifaEspecialPensiones, IngressDate, DateRetirement, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate,
					MaternityLeaveInitialDate, MaternityLeaveEndDate, VacationInitialDate, VacationEndDate, FechaInicioVCT, FechaFinVCT, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales, TotalHours,
					FechaEmpleadoExterior, EconomicActivityARL, FlagVacation, FlagLMA, FlagIRL, FlagIGE,FlagSanction)
	
		SELECT	TLN.IdEmployee, TLN.IdContract, 0, TA.typeDocument, TA.Nit, TA.typeContractEmployee, TA.subTypeEmployee, TA.foreignNotBound, TA.colombianForeignResident, TA.CodeCityBranchOffice,
				TA.FirstLastName, TA.SecondLastName, TA.FirstName, TA.SecondName, TA.[entry], TA.retirement, TA.TDE, TA.TAE, TA.TDP, TA.TAP, TA.VSP, TA.Correcciones, ' ', TLN.SLN, TLN.IGE, TLN.LMA, TLN.VAC, TA.AVP, TA.VCT, TLN.IRL, TA.pensionAdministratorCode,
				TA.transferPensionAdministratorCode, TA.EPSCode, TA.transferEPSCode, TA.CCFCode, TLN.pensionDays, TLN.healthDays, TLN.professionalRiskDays, TLN.compensationFundDays, TA.BasicSalary, TA.integralSalary, TLN.IBCPension,
				tln.IBCHealth, tln.IBCProfessionalRisk, tln.IBCCompensationFund, tln.rateContributionsPension, TLN.ValuePension, TA.voluntaryContributionPensionValue, TA.voluntaryContributionPensionValuePatron,
				TLN.TotalPensionContribution, TLN.PensionSolidarityFundValueContribution, TLN.PensionSolidarityFundValueContributionSubSistence, TA.ValueNotRetainedByVoluntaryContributions,
				TLN.rateContributionsHealth, TLN.ValueHealth, TA.valueAditionalUPC, TLN.authorizationNumberDisability, TLN.valueGeneralDisability, TLN.authorizationNumberMaternityLicense,
				TLN.valueMaternityLicense, TLN.rateContributionProfessionalRisk, TA.WorkCenter, TLN.ValueContributionProfessionalRisk, TLN.rateContributorCCF, TLN.ValueContributionCCF,
				TLN.rateContributorSENA, TLN.ValueSena, TLN.rateContributionICBF, TLN.ValueICBF, TA.rateContributorESAP, TA.rateContributorEducationMinistry, TA.MinistryCodeRiskFound,
				TLN.ProfessionalRiskCode,TA.TarifaEspecialPensiones, TA.IngressDate, TA.DateRetirement, TLN.SanctionInitialDate, TLN.SanctionEndDate, TLN.AmbulatoryDisabilityInitialDate, TLN.AmbulatoryDisabilityEndDate,
				TLN.MaternityLeaveInitialDate, TLN.MaternityLeaveEndDate, TLN.VacationInitialDate, TLN.VacationEndDate, TA.FechaInicioVCT, TA.FechaFinVCT, TLN.FechaInicioIRL, TLN.FechaFinIRL, TLN.IBCOtrosParafiscales, TA.TotalHours,
				TA.FechaEmpleadoExterior, TA.EconomicActivityARL, TA.FlagVacation, TA.FlagLMA, TA.FlagIRL, TA.FlagIGE, TA.FlagSanction
		FROM @TmpAutoliquidationNovelty TLN, @TmpAutoliquidation TA
		WHERE TA.IdEmployee = TLN.IdEmployee and tln.IdContract = ta.IdContract
			
		--SELECT * FROM @TmpAutoliquidation
		
	-- Ahora analizamos los retiros
	
		DECLARE @IdEmployeeRetirement int
		DECLARE @IdContractRetirement int
		DECLARE @IdContractLiquidation int
		
		declare C_Retiros cursor for	

		SELECT CL.EmployeeId, CL.ContractId, CL.Id FROM Payroll.ContractLiquidation CL, Payroll.Employee E, Payroll.WorkCenter WC
		where RetirementDate >= @PayrollStarDate and RetirementDate <= @PayrollEndDate AND E.Id = CL.EmployeeId AND E.WorkCenterId = WC.Id AND WC.Code = @WorkCenterCode
		
		open C_Retiros
			fetch next from C_Retiros into @IdEmployeeRetirement, @IdContractRetirement, @IdContractLiquidation
			while @@FETCH_STATUS = 0 begin

				DECLARE @Cantidad INT = 0
				DECLARE @RetirementDate date
				DECLARE @IbcRetirementCompensationFund NUMERIC(18,0)
				DECLARE @RateCCF DECIMAL(6,3)
				DECLARE @CCfContributionValue NUMERIC(18,0)
				DECLARE @IdVacationConcept INT
				DECLARE @VacationValue NUMERIC(18,0) = 0

				SELECT @RetirementDate = RetirementDate 
				FROM Payroll.ContractLiquidation where Id = @IdContractLiquidation

				-- Averiguo el Concepto de Vacaciones
				SELECT @IdVacationConcept = IdVacationConcept FROM Payroll.PayrollSettings

				-- Averiguo el Valor de las Vacaciones
				SELECT @VacationValue = Accrued FROM Payroll.ContractLiquidationDetail where ContractLiquidationId = @IdContractLiquidation and IdConcept = @IdVacationConcept

				SELECT @Cantidad = COUNT(*) FROM Payroll.Liquidation where PayrollDateLiquidated = @PayrollEndDate and RegisterStatus = 'C' and EmployeeId = @IdEmployeeRetirement

				IF @Cantidad > 0 BEGIN
					-- Se pagaron días por Nómina y deben estar en la tabla de Liquidación
					
					DECLARE @IBCCompensationFund NUMERIC(18,0) = 0
					
					SELECT @IBCCompensationFund = IBCCompensationFund, @RateCCF = rateContributorCCF FROM @TmpAutoliquidation WHERE IdEmployee = @IdEmployeeRetirement AND retirement = 'X'
					
					SET @IbcRetirementCompensationFund = @IBCCompensationFund + ISNULL(@VacationValue,0)

					SET @CCfContributionValue = @IbcRetirementCompensationFund * @RateCCF
					SET @CCfContributionValue = CEILING(@CCfContributionValue) / 100 * 100

					UPDATE @TmpAutoliquidation 
					SET DateRetirement = @RetirementDate, IBCCompensationFund = @IbcRetirementCompensationFund, ValueContributionCCF = @CCfContributionValue
					WHERE IdEmployee = @IdEmployeeRetirement AND IdContract = @IdContractRetirement AND retirement = 'X'

				END ELSE BEGIN
					-- No está en el listado, hay que armar toda la tabla de Autoliquidacion

					DECLARE @IBCHealthRetirement NUMERIC(18,0)
					DECLARE @ValueHealthRetirement NUMERIC(18,0)
					DECLARE @HealthRetirementDays INT
					DECLARE @ValuePensionRetirement NUMERIC(18,0)
					DECLARE @RateHealthRetirement DECIMAL (6,3)
					DECLARE @RatePensionRetirement DECIMAL (6,3)
					DECLARE @CreeTax BIT
					DECLARE @ContractClass TINYINT
					DECLARE @rateContributionRetirementICBF DECIMAL(6,3)
					DECLARE @rateContributorRetirementSENA DECIMAL(6,3)
					DECLARE @rateContributorRetirementCCF DECIMAL(6,3)
					DECLARE @LegalSalaryMininumRetirement NUMERIC(18,0)
					DECLARE @EmployeeHealthContributionPercentage DECIMAL(6,3)
					DECLARE @EmployeePensionContributionPercentage DECIMAL(6,3)
					DECLARE @EmployerPensionContributionPercentage DECIMAL(6,3)
					DECLARE @EmployerHealthContributionRetirementPercentage DECIMAL(6,3)
					DECLARE @PensionSolidarityFundValueContributionRetirement NUMERIC(18,0) = 0
					DECLARE @PensionSolidarityFundValueContributionSubSistenceRetirement NUMERIC(18,0) = 0
					DECLARE @ProfessionalRiskRateRetirement DECIMAL(9,5)
					DECLARE @ProfessionalRiskValueRetirement NUMERIC(18,0)
					DECLARE @CCFRetirementContributionValue NUMERIC(18,0)
					DECLARE @SenaRetirementContributionValue NUMERIC(18,0)
					DECLARE @ICBFRetirementContributionValue NUMERIC(18,0)

					SELECT @CreeTax = CreeTax FROM Payroll.PayrollSettings

					SELECT @ContractClass = CT.ContractClass, @LegalSalaryMininumRetirement = PP.LegalSalaryMinimum, 
					@rateContributionRetirementICBF = PP.ICBFContributionPercentage, @rateContributorRetirementSENA = pp.SenaContributionPercentage, 
					@rateContributorRetirementCCF = pp.CompensationFundContributionPercentage, @EmployeeHealthContributionPercentage = PP.EmployeeHealthContributionPercentage,
					@EmployerPensionContributionPercentage = pp.EmployerPensionContributionPercentage, @EmployeePensionContributionPercentage = PP.EmployeePensionContributionPercentage,
					@EmployerHealthContributionRetirementPercentage = PP.EmployerHealthContributionPercentage, @ProfessionalRiskRateRetirement = E.ProfessionalRiskPercentage
					FROM Payroll.Contract C, Payroll.[Group] G, Payroll.PayrollParameter PP, Payroll.ContractType CT, Payroll.Employee E
					WHERE C.GroupId = G.Id AND G.PayrollParameterId = PP.Id AND CT.Id = C.ContractTypeId AND  C.Id = @IdContractRetirement AND C.EmployeeId = E.Id

					SELECT @ValueHealthRetirement = SUM(CLD.Deducted) - SUM(CLD.Accrued)
					FROM Payroll.ContractLiquidationDetail CLD, Payroll.Concept CONC
					WHERE CLD.ContractLiquidationId = @IdContractLiquidation
					AND CONC.Id = CLD.IdConcept AND CONC.ConceptClass = '017'

					SELECT @PensionSolidarityFundValueContributionRetirement = ISNULL(SUM(CLD.Deducted),0)
					FROM Payroll.ContractLiquidationDetail CLD, Payroll.Concept CONC
					WHERE CLD.ContractLiquidationId = @IdContractLiquidation
					AND CONC.Id = CLD.IdConcept AND CONC.ConceptClass = '038'

					SET @IBCHealthRetirement = ABS((@ValueHealthRetirement * 100) / 4)

					SET @HealthRetirementDays = [Payroll].[fnCalculateDays360] (@PayrollStarDate, @RetirementDate)

					SET @IbcRetirementCompensationFund = @IBCHealthRetirement + @VacationValue

					SET @ProfessionalRiskRateRetirement = @ProfessionalRiskRateRetirement / 100
					SET @rateContributorRetirementCCF = @rateContributorRetirementCCF / 100
					set @rateContributorRetirementSENA = @rateContributorRetirementSENA / 100
					SET @rateContributionRetirementICBF = @rateContributionRetirementICBF / 100

					IF @ContractClass <> 2 BEGIN
					-- EL CONTRATO NO ES DE APRENDÍZ
					IF @CreeTax = 1 BEGIN
						IF @BasicSalary < (10 * @LegalSalaryMininumRetirement) BEGIN
							set @rateContributionRetirementICBF = 0
							set @EmployerHealthContributionRetirementPercentage = 0
							set @rateContributorRetirementSENA = 0
						END
					END
				END ELSE BEGIN
					SET @EmployeeHealthContributionPercentage = 0
					SET @EmployeePensionContributionPercentage = 0
					SET @rateContributorRetirementCCF = 0
					SET @rateContributionRetirementICBF = 0
					SET @rateContributorRetirementSENA = 0
				END

				-- DEFINO LAS SUMATORIAS DE LOS PORCENTAJES
				SET @RatePensionRetirement =( @EmployeePensionContributionPercentage + @EmployerPensionContributionPercentage ) / 100
				SET @RateHealthRetirement = (@EmployeeHealthContributionPercentage + @EmployerHealthContributionRetirementPercentage) /100

				SET @ValuePensionRetirement = @IBCHealthRetirement * @RatePensionRetirement 
				SET @ValuePensionRetirement = CEILING(@ValuePensionRetirement / 100) * 100

				SET @ValueHealthRetirement = @IBCHealthRetirement * @RateHealthRetirement 
				SET @ValueHealthRetirement = CEILING(@ValueHealthRetirement / 100) * 100

				SET @ProfessionalRiskValueRetirement = @IBCHealthRetirement * @ProfessionalRiskRateRetirement 
				SET @ProfessionalRiskValueRetirement = CEILING(@ProfessionalRiskValueRetirement / 100) * 100

				SET @CCFRetirementContributionValue = @IbcRetirementCompensationFund * @rateContributorRetirementCCF
				SET @CCFRetirementContributionValue = CEILING(@CCFRetirementContributionValue / 100) * 100

				SET @SenaRetirementContributionValue = @IBCHealthRetirement * @rateContributorRetirementSENA
				SET @SenaRetirementContributionValue = CEILING(@SenaRetirementContributionValue / 100) * 100

				SET @ICBFRetirementContributionValue = @IBCHealthRetirement * @rateContributionRetirementICBF
				SET @ICBFRetirementContributionValue = CEILING(@ICBFRetirementContributionValue / 100) * 100

				IF @PensionSolidarityFundValueContributionRetirement > 0 BEGIN
					SET @PensionSolidarityFundValueContributionRetirement = CEILING(((@IBCHealthRetirement * 0.01) / 2) / 100) * 100
					IF @PensionSolidarityFundValueContributionSubSistenceRetirement > 0 BEGIN
						SET @PensionSolidarityFundValueContributionSubSistenceRetirement= CEILING(((@IBCHealthRetirement * 0.01) / 2) / 100) * 100
					END
				END
				
				INSERT INTO @TmpAutoliquidationRetirement(IdEmployee,IdContract,retirementDate, typeDocument,
							Nit,typeContractEmployee,subTypeEmployee, foreignNotBound, colombianForeignResident, CodeCityBranchOffice, 
							FirstLastName, SecondLastName, FirstName, SecondName, pensionDays,healthDays,professionalRiskDays,compensationFundDays,
							IBCPension, IBCHealth, IBCProfessionalRisk, IBCCompensationFund, rateContributionsPension, ValuePension,
							TotalPensionContribution, PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, 
							rateContributionsHealth, ValueHealth, rateContributionProfessionalRisk, ValueContributionProfessionalRisk,
							rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
							ProfessionalRiskCode, IBCOtrosParafiscales, BasicSalary, EPSCode,CCFCode,pensionAdministratorCode, MinistryCodeRiskFound,
							WorkCenter, TarifaEspecialPensiones, IntegralSalary)
					SELECT CL.EmployeeId, 
						CL.ContractId, 
						@RetirementDate,
						CASE P.IdentificationType 
							WHEN 0 THEN 'CC'
							WHEN 1 THEN 'CE'
							WHEN 2 THEN 'TI'
							WHEN 3 THEN 'RC'
							WHEN 4 THEN 'PA'
						END, 
						TP.Nit, 
						CASE 
							WHEN CT.ContractClass = 2 THEN '12'
							WHEN CT.ContractClass = 3 THEN '01'
							WHEN CT.ContractClass = 4 THEN '01'
							WHEN E.PensionaryStatus IS NOT NULL THEN '05'
						END, 
						'00', 
						' ',  
						CASE e.ContributorAbroad
							WHEN 0 THEN ' '
							WHEN 1 THEN 'X'
						END,
						CityBranch.Code, 
						P.FirstLastName, 
						P.SecondLastName,
						p.FirstName, 
						P.SecondName,
						@HealthRetirementDays,
						@HealthRetirementDays,
						@HealthRetirementDays,
						@HealthRetirementDays,
						@IBCHealthRetirement,
						@IBCHealthRetirement,
						@IBCHealthRetirement,
						@IbcRetirementCompensationFund,
						@RatePensionRetirement,
						@ValuePensionRetirement,
						@ValuePensionRetirement,
						@PensionSolidarityFundValueContributionRetirement,
						@PensionSolidarityFundValueContributionSubSistenceRetirement,
						@RateHealthRetirement,
						@ValueHealthRetirement,
						@ProfessionalRiskRateRetirement,
						@ProfessionalRiskValueRetirement,
						@rateContributorRetirementCCF,
						@CCFRetirementContributionValue,
						@rateContributorRetirementSENA,
						@SenaRetirementContributionValue,
						@rateContributionRetirementICBF,
						@ICBFRetirementContributionValue,
						PR.Code,
						0,
						CONT.BasicSalary,
						ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.State = 1 and FC.FundType = 1 AND VoluntaryContribution = 0), 'SINEPS'), 
						ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.State = 1 and FC.FundType = 5 AND VoluntaryContribution = 0), 'SINCCF'), 
						ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.State = 1 and FC.FundType = 2 AND VoluntaryContribution = 0), 'SINAFP'),
						ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND FC.State = 1 and FC.FundType = 4 AND VoluntaryContribution = 0), 'SINARL'),
						WC.Code,
						CASE
							WHEN PP.SpecialPensionRateIndicator = 0 THEN ' '
							ELSE PP.SpecialPensionRateIndicator
						END,
						CASE 
							WHEN CT.SalaryType = 1 AND @PayrollStarDate > '2020-07-01' THEN 'F'
							WHEN CT.SalaryType = 2 THEN 'X'
							WHEN CT.SalaryType = 3 AND @PayrollStarDate > '2020-07-01' THEN 'V'
							ELSE ' '
						END -- 41
					FROM Payroll.ContractLiquidation CL
					INNER JOIN Payroll.Employee E ON E.Id = CL.EmployeeId
					INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
					INNER JOIN Common.Person P ON P.Id = TP.PersonId
					INNER JOIN Payroll.Contract CONT ON CONT.ID = CL.ContractId
					INNER JOIN Payroll.WorkCenter WC ON WC.Id = E.WorkCenterId
					INNER JOIN Payroll.ContractType CT ON CT.Id = CONT.ContractTypeId
					INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = CONT.FunctionalUnitId
					INNER JOIN Payroll.BranchOffice BA ON BA.ID = FU.BranchOfficeId
					INNER JOIN Common.City CityBranch ON CityBranch.Id = BA.CityId
					INNER JOIN Payroll.[Group] G ON G.Id = CONT.GroupId
					INNER JOIN Payroll.PayrollParameter PP ON PP.ID = g.PayrollParameterId
					INNER JOIN Payroll.Position POS ON POS.Id = CONT.PositionId
					INNER JOIN Payroll.ProfessionalRisk PR ON PR.Id = pos.ProfessionalRiskLevelId
					WHERE CL.id = @IdContractLiquidation

				END
				
			fetch next from C_Retiros into @IdEmployeeRetirement, @IdContractRetirement, @IdContractLiquidation
			end -- fin while de C_Empleados
		close C_Retiros 
		deallocate C_Retiros
		

		INSERT INTO @TmpAutoliquidation(IdEmployee, IdContract, sequenceDetail, typeDocument, Nit, typeContractEmployee, subTypeEmployee, foreignNotBound, colombianForeignResident, CodeCityBranchOffice, 	
					FirstLastName, SecondLastName, FirstName, SecondName, [entry], retirement, TDE, TAE, TDP, TAP, VSP, Correcciones, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRL, pensionAdministratorCode,
					transferPensionAdministratorCode, EPSCode, transferEPSCode, CCFCode, pensionDays, healthDays, professionalRiskDays, compensationFundDays, BasicSalary, integralSalary, IBCPension,
					IBCHealth, IBCProfessionalRisk, IBCCompensationFund, rateContributionsPension, ValuePension, voluntaryContributionPensionValue, voluntaryContributionPensionValuePatron,
					TotalPensionContribution, PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, ValueNotRetainedByVoluntaryContributions,
					rateContributionsHealth, ValueHealth, valueAditionalUPC, authorizationNumberDisability, valueGeneralDisability, authorizationNumberMaternityLicense,
					valueMaternityLicense, rateContributionProfessionalRisk, WorkCenter, ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF,
					rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF, rateContributorESAP, rateContributorEducationMinistry, MinistryCodeRiskFound,
					ProfessionalRiskCode, TarifaEspecialPensiones, IngressDate, DateRetirement, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate,
					MaternityLeaveInitialDate, MaternityLeaveEndDate, VacationInitialDate, VacationEndDate, FechaInicioVCT, FechaFinVCT, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales, TotalHours,
					FechaEmpleadoExterior, FlagVacation, FlagLMA, FlagIRL, FlagIGE,FlagSanction)
		SELECT	TLN.IdEmployee, TLN.IdContract, 0, TLN.typeDocument, TLN.Nit, TLN.typeContractEmployee, TLN.subTypeEmployee, TLN.foreignNotBound, TLN.colombianForeignResident, TLN.CodeCityBranchOffice,
				TLN.FirstLastName, TLN.SecondLastName, TLN.FirstName, TLN.SecondName, ' ', 'X', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 0, TLN.pensionAdministratorCode,
				'', TLN.EPSCode, '', TLN.CCFCode, TLN.pensionDays, TLN.healthDays, TLN.professionalRiskDays, TLN.compensationFundDays, TLN.BasicSalary, TLN.integralSalary, TLN.IBCPension,
				tln.IBCHealth, tln.IBCProfessionalRisk, tln.IBCCompensationFund, tln.rateContributionsPension, TLN.ValuePension, 0, 0,
				TLN.TotalPensionContribution, TLN.PensionSolidarityFundValueContribution, TLN.PensionSolidarityFundValueContributionSubSistence, 0,
				TLN.rateContributionsHealth, TLN.ValueHealth, 0, NULL, 0, NULL,
				0, TLN.rateContributionProfessionalRisk, TLN.WorkCenter, TLN.ValueContributionProfessionalRisk, TLN.rateContributorCCF, TLN.ValueContributionCCF,
				TLN.rateContributorSENA, TLN.ValueSena, TLN.rateContributionICBF, TLN.ValueICBF, 0, 0, TLN.MinistryCodeRiskFound,
				TLN.ProfessionalRiskCode, TLN.TarifaEspecialPensiones, NULL, TLN.retirementDate, NULL, NULL, NULL, NULL,
				NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, TLN.IBCOtrosParafiscales, 0,
				NULL, 0, 0, 0, 0, 0
		FROM @TmpAutoliquidationRetirement TLN

		
		

		INSERT INTO Payroll.VerifyAutoliquidationFile(RegisterStatus, PayrollDateLiquidated, EmployeeID, TypeContractEmployee, SubTypeEmployee, ForeignNotBound, ColombianForeignResident, CodeCityBranchOffice, [Entry], [Retirement], 
		TDE, TAE, TDP, TAP, VSP, Correcciones, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRL, PensionAdministratorCode, TransferPensionAdministratorCode, EPSCode, TransferEPSCode,
		CCFCode, PensionDays, HealthDays, ProfessionalRiskDays, CompensationFundDays, BasicSalary, IntegralSalary, IBCPension, IBCHealth, IBCProfessionalRisk, IBCCompensationFund,
		rateContributionPension, ValuePension, VoluntaryContributionPensionValue, VoluntaryContributionPensionValuePatron, TotalPensionContribution, PensionSolidarityFundValueContribution,
		PensionSolidarityFundValueContributionSubsistence, ValueNotRetainedByVoluntaryContributions, RateContributionHealth, ValueHealth, ValueAditionalUPC, AuthorizationNumberDisability,
		ValueGeneralDisability, AuthorizationNumberMaternityLicense, ValueMaternityLicense, RateContributionProfessionalRisk, WorkCenter, ValueContributionProfessionalRisk, RateContributorCCF,
		ValueContributionCCF, RateContributorSENA, ValueSena, RateContributionICBF, ValueICBF, RateContributorESAP, RateContributorEducationMinistry, MinistryCodeRiskFound, ProfessionalRiskCode,
		TarifaEspecialPensiones, IngressDate, DateRetirement, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabiltyEndDate,
		MaternityLeaveInitialDate, MaternityLeaveEndDate, VacationInitialDate, VacationEndDate, FechaInicioVCT, FechaFinVCT, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales, TotalHours, FechaEmpleadoExterior,
		EconomicActivityARL)
		SELECT 0, @PayrollStarDate, IdEmployee, typeContractEmployee, subTypeEmployee, foreignNotBound, colombianForeignResident, CodeCityBranchOffice, [entry], retirement,
		TDE, TAE, TDP, TAP, VSP, Correcciones, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRL, pensionAdministratorCode, transferPensionAdministratorCode, EPSCode, transferEPSCode,
		CCFCode, ISNULL(pensionDays,0), ISNULL(healthDays,0), ISNULL(professionalRiskDays,0), ISNULL(compensationFundDays,0), BasicSalary, integralSalary, ISNULL(IBCPension,0), ISNULL(IBCHealth,0), ISNULL(IBCProfessionalRisk,0), ISNULL(IBCCompensationFund,0), 
		rateContributionsPension, isnull(ValuePension,0), voluntaryContributionPensionValue, voluntaryContributionPensionValuePatron, ISNULL(TotalPensionContribution,0), ISNULL(PensionSolidarityFundValueContribution,0),
		PensionSolidarityFundValueContributionSubSistence, ValueNotRetainedByVoluntaryContributions, rateContributionsHealth, ISNULL(ValueHealth,0), valueAditionalUPC, ISNULL(authorizationNumberDisability,''),
		valueGeneralDisability, ISNULL(authorizationNumberMaternityLicense,''), valueMaternityLicense, rateContributionProfessionalRisk, WorkCenter, ISNULL(ValueContributionProfessionalRisk,0), rateContributorCCF,
		ISNULL(ValueContributionCCF,0), rateContributorSENA, ISNULL(ValueSena,0), rateContributionICBF, ISNULL(ValueICBF,0), rateContributorESAP, rateContributorEducationMinistry, MinistryCodeRiskFound, ProfessionalRiskCode,
		TarifaEspecialPensiones, IngressDate, DateRetirement, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, 
		MaternityLeaveInitialDate, MaternityLeaveEndDate, VacationInitialDate, VacationEndDate, FechaInicioVCT, FechaFinVCT, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales, ISNULL(TotalHours,0), FechaEmpleadoExterior, EconomicActivityARL
		FROM @TmpAutoliquidation ORDER BY IdEmployee
		

		SELECT '0' AS CodeMessage, 'Proceso Finalizado Correctamente ' AS Message, cast(1 as tinyint) as [Status] 
		

	END TRY
	BEGIN CATCH

		SELECT '999'  AS CodeMessage , ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) AS Message, cast(3 as tinyint) as [Status] 
		
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo de autoliquidación de aportes a la seguridad social (PILA) para un período de nómina y centro de trabajo específicos. Antes de procesar, verifica en el registro de verificación de autoliquidación (VerifyAutoliquidationFile) si el archivo del mes ya fue confirmado; si es así, retorna un mensaje de error impidiendo la regeneración. Si no está confirmado, elimina el registro previo pendiente y recalcula la autoliquidación consolidando los datos de liquidaciones de nómina (Liquidation y LiquidationDetail) junto con sus conceptos, para construir las bases de cotización (IBC) de pensión, salud, ARL y caja de compensación, así como los días cotizados, novedades (incapacidades, licencias, vacaciones, sanciones) y valores de aportes por cada empleado en el período indicado. Es el procedimiento central para la generación del archivo plano PILA de seguridad social y parafiscales (SENA, ICBF, CCF) que debe presentarse mensualmente ante los operadores de información.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_AutoliquidationFile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_AutoliquidationFile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe en VerifyAutoliquidationFile un registro con PayrollDateLiquidated=@PayrollDate, WorkCenter=@WorkCenterCode y RegisterStatus=1 (confirmado) → Retorna mensaje ''888'' indicando que el archivo del mes ya fue generado y confirmado, y termina sin recalcular else Elimina registros previos no confirmados (RegisterStatus=0) y procede a recalcular la autoliquidación; si @WorkCenterCode IS NULL → Se asigna ''%%'' como filtro genérico de centro de trabajo; si CT.SalaryType=1 y @PayrollStarDate>''2020-07-01'' → integralSalary=''F'' else Si SalaryType=2 → ''X''; si SalaryType=3 y fecha>2020-07-01 → ''V''; en otro caso '' ''; si CT.ContractClass IN (2,5) y BasicSalary <= LegalSalaryMinimum → Tasa de salud = solo aporte patronal (EmployerHealthContributionPercentage); si PayrollSettings.CreeTax=1 y BasicSalary < 10*LegalSalaryMinimum y ContractClass<>2 → No se aplican aportes de SENA, ICBF ni salud patronal (exoneración Ley 1607/CREE); IBCOtrosParafiscales=0 else Se aplican tasas completas de SENA, ICBF y salud sobre IBC; si CONT.BasicSalary > 25 * LegalSalaryMinimum → PensionSolidarityFundValueContribution se recalcula con tope de 25 SMLMV; si TLF.PensionSolidarityFundValueContribution > 0 y IBCPension >= 16*LegalSalaryMinimum → Se calcula aporte a Subsistencia mediante fnCalculateSolidarityFundSubsistence else Si IBCPension < 16 SMLMV se usa el valor base; si no hay aporte solidaridad → 0; si CONT.JobBondingDate dentro del rango [@PayrollStarDate,@PayrollEndDate] → Marca novedad ''X'' de ingreso (entry) y registra IngressDate; si CONT.RetirementDate dentro del rango del período y ContractClass=2 (aprendiz) → Registra DateRetirement; en otros casos retirement=''X'' pero DateRetirement queda NULL en la fila base; si FlagVacation=1 (TLF.DaysVacation>0) → Calcula IBC, días, valores de pensión/salud/SENA/ICBF/CCF para vacaciones e inserta fila VAC=''X'' en @TmpAutoliquidationNovelty; si FlagLMA=1 (TLF.DaysMaternityLeave>0) → Toma valor de licencia de maternidad (ConceptClass=''023'' o ConceptCode=''013''), calcula IBC y aportes, inserta fila LMA=''X''; si FlagSanction=1 (DaysSanction+DaysUnpaidLicenseDays>0) → Toma fechas de Sanción o de Licencia No Remunerada y EmployeeBaseSalary/IBC desde Novelty; aporta solo pensión y solidaridad; salud/SENA/ICBF/CCF=0; inserta fila SLN=''X''; si FlagSanction=1 y (LegalSalaryMinimum*10) > BasicSalary y CreeTax=1 → RateHealth se fuerza a 0 else RateHealth = EmployerHealthContributionPercentage/100; si FlagIRL=1 (InitialDateOccupationalRisksDisability NOT NULL) → Calcula IBC con suma de LiquidationDetail ConceptClass=''027'' y aportes; inserta fila con IRL=días; si FlagIGE=1 (DaysAmbulatoryDisability>0) → Suma ConceptClass=''021'' + ajuste ConceptCode=''1009'' como IBC; si Group.Code=''09'' fuerza fechas al período completo y usa max(BasicSalary/2, SMLMV) como IBC; inserta fila IGE=''X''; si Para cada retiro existe Liquidation con RegisterStatus=''C'' y PayrollDateLiquidated=@PayrollEndDate → Solo actualiza la fila existente con DateRetirement, IBCCompensationFund (sumando vacaciones) y ValueContributionCCF else Construye fila completa de retiro en @TmpAutoliquidationRetirement con cálculos propios de IBC, días, aportes y la inserta luego en @TmpAutoliquidation; si ContractClass<>2 y CreeTax=1 y BasicSalary < 10*SMLMV (en retiro) → Tasas ICBF, SENA y salud patronal se ponen a 0 else Si ContractClass=2 (aprendiz) se anulan aportes empleado salud/pensión, CCF, ICBF y SENA; si @PayrollStarDate >= ''2022-11-01'' → EconomicActivityARL = ProfessionalRisk.ArlCode else EconomicActivityARL = 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AutoliquidationFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Payroll.fnCalculateDays360; Payroll.fnCalculateSolidarityFundSubsistence', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AutoliquidationFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VerifyAutoliquidationFile; Payroll.Liquidation; Payroll.LiquidationDetail; Payroll.Concept; Payroll.Employee; Payroll.EmployeeType; Common.ThirdParty; Common.Person; Payroll.Contract; Payroll.WorkCenter; Payroll.ContractType; Payroll.FunctionalUnit; Payroll.BranchOffice; Common.City; Payroll.Position; Payroll.ProfessionalRisk; Payroll.Group; Payroll.PayrollParameter; Payroll.FundContract; Payroll.Fund; Payroll.PayrollSettings; Payroll.Vacation; Payroll.VacationPeriod; Payroll.Novelty; Payroll.ContractLiquidation; Payroll.ContractLiquidationDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AutoliquidationFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_AutoliquidationFile';
-- GO
