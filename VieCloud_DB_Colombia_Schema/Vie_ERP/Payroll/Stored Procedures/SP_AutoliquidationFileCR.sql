-- =============================================
-- Author:		Juan Pablo Daza Medina 
-- Create date: 16/01/2025
-- Description:	Obtiene la data de autoliquidacion Costa Rica
-- =============================================
CREATE PROCEDURE [Payroll].[SP_AutoliquidationFileCR]
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
	SELECT @PayrollEndDate = dateadd( s, -1, dateadd( mm, datediff( m, 0, @PayrollDate ) + 1, 0 ) )

	 --Valido que no esté Confirmado
	IF (SELECT COUNT(*) FROM Payroll.VerifyAutoliquidationFile where PayrollDateLiquidated = @PayrollDate AND WorkCenter = @WorkCenterCode and RegisterStatus = 1) > 0 BEGIN
		SELECT '888'  AS CodeMessage , 'El archivo de este mes ya se generó y se confirmó' as Message, cast(3 as tinyint) as [Status] 
		RETURN
	END

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
		DaysHospitalaryDisability int,
		DaysSanction int,
		DaysUnpaidLicenseDays int,
		VoluntaryContributionPensionValue numeric(18,0),
		PensionSolidarityFundValueContribution numeric(18,0),
		LicenceDays int
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
			FlagSanction bit,
			FlagLicenceDays bit,
			LicenceDays int,
			FlagContractModificationReason bit,
			NoveltyType TINYINT,
			FlagEmployeeIngress Bit,
			EmployeeIngress varchar(2),
			LicenceInitialDate Date,
			LicenceEndDate Date,
			LRM varchar(1)
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
		IBCOtrosParafiscales numeric(18,0), -- 95
		LicenceDays INT,
		NoveltyType TINYINT NULL,
		EmployeeIngressN varchar(2) NULL,
		LicenceInitialDate date,
		LicenceEndate date,
		LRM varchar(1)
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
		IntegralSalary varchar(2),
	   FlagRetirement varchar (1)
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
					PensionSolidarityFundValueContribution,
					LicenceDays

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
		MIN(L.OccupationalRisksDisabilityInitialDate) as OccupationalRisksDisabilityInitialDate,
		SUM(L.AmbulatoryDisabilityDays),
		SUM(L.SanctionDays),
		SUM(L.UnpaidLicenseDays),
		SUM(L.VoluntaryContributionPensionValue),
		CASE
		WHEN SUM(L.PensionSolidarityFundValueContribution ) > 0 THEN CEILING((SUM(L.PensionJCB) * 0.01) / 2) / 100 * 100
		ELSE 0
		END,
		SUM(L.LicenseDays)
		FROM Payroll.Liquidation L 
		INNER JOIN Payroll.Contract C ON L.ContractId = C.ID
		WHERE L.PayrollDateLiquidated BETWEEN @PayrollStarDate AND @PayrollEndDate
		GROUP BY 
		L.EmployeeId, 
		L.ContractId

		
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
					FlagSanction,
					FlagLicenceDays,
					LicenceDays,
					FlagContractModificationReason,
					NoveltyType,
					FlagEmployeeIngress,
					EmployeeIngress,
					LicenceInitialDate,
					LicenceEndDate,
					LRM)

		SELECT 
		DISTINCT
		TLF.IdEmployee, 
		TLF.IdContract, 
		0, -- 2
		ADT.SIGLA IdentificationType ,--3
		TP.Nit, -- 4
		ET.EmployeeClass, -- 5
		'00', -- 6
		' ',  -- 7
		CASE e.ContributorAbroad
			WHEN 0 THEN ' '
			WHEN 1 THEN 'X'
		END,-- 8
		CityBranch.Code, -- 9 y 10
		P.FirstLastName, -- 11
		P.SecondLastName, -- 12
		p.FirstName, -- 13
		P.SecondName, -- 14
		CASE
			WHEN CONT.JobBondingDate >= @PayrollStarDate AND CONT.JobBondingDate <= @PayrollEndDate THEN 'X'
			ELSE ' '
		END, -- 15
		' ',-- 16
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
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND  FC.FundType = 2 AND VoluntaryContribution = 0), 'SINAFP'), -- 31
		' ', -- 32
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND  FC.FundType = 1 AND VoluntaryContribution = 0), 'SINEPS'), -- 33
		' ', -- 34
		ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F WHERE FC.ContractId = CONT.Id AND F.Id = FC.FundId AND  FC.FundType = 5 AND VoluntaryContribution = 0), 'SINCCF'), -- 35
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
		-- IBCPension: Agregar valor adicional de ContractLiquidationDetail si hay retiro en segunda quincena
		TLF.IBCPension + ISNULL(CL_Additional.IBCPensionAdditional, 0), -- 42
		-- IBCHealth: Agregar valor adicional de ContractLiquidationDetail si hay retiro en segunda quincena
		TLF.IBCHealth + ISNULL(CL_Additional.IBCHealthAdditional, 0), -- 43
		-- IBCProfessionalRisk: Agregar valor adicional de ContractLiquidationDetail si hay retiro en segunda quincena
		TLF.IBCPension + ISNULL(CL_Additional.IBCProfessionalRiskAdditional, 0), -- 44
		-- IBCCompensationFund: Agregar valor adicional de ContractLiquidationDetail si hay retiro en segunda quincena
		TLF.IBCPension + ISNULL(CL_Additional.IBCCompensationFundAdditional, 0), -- 45

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
			WHEN TLF.PensionSolidarityFundValueContribution > 0 AND TLF.IBCPension < (PP.LegalSalaryMinimum * 16) THEN TLF.PensionSolidarityFundValueContribution
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
				WHEN (CT.ContractClass = 2 OR CT.ContractClass = 5) AND CONT.BasicSalary <= PP.LegalSalaryMinimum 
			THEN CEILING(((TLF.IBCHealth + ISNULL(CL_Additional.IBCHealthAdditional, 0)) * ((PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 0 AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5)
			THEN CEILING(((TLF.IBCHealth + ISNULL(CL_Additional.IBCHealthAdditional, 0)) * ((PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary > (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) 
			THEN CEILING(((TLF.IBCHealth + ISNULL(CL_Additional.IBCHealthAdditional, 0)) * ((PP.EmployeeHealthContributionPercentage + PP.EmployerHealthContributionPercentage) / 100)) / 100) * 100
			WHEN (SELECT CreeTax FROM Payroll.PayrollSettings) = 1 AND CONT.BasicSalary < (10 * PP.LegalSalaryMinimum) AND (CT.ContractClass <> 2 OR CT.ContractClass <> 5) 
			THEN CEILING(((TLF.IBCHealth + ISNULL(CL_Additional.IBCHealthAdditional, 0)) * ((PP.EmployeeHealthContributionPercentage) / 100)) / 100) * 100			
		END,
	
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
			WHEN ((CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT)) + (isnull(TLDF.TotalNumberHours,0))) > (30 * CONT.HoursDaily) THEN (30 * CONT.HoursDaily)
			WHEN TLDF.TotalNumberHours > 0 THEN (CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT)) + TLDF.TotalNumberHours
			WHEN isnull(TLDF.TotalNumberHours,0) <= 0 THEN (CAST(CONT.HoursDaily AS INT) * CAST(TLF.WorkDays AS INT))
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
			WHEN (TLF.DaysAmbulatoryDisability > 0)  THEN 1
			ELSE 0
		END,
		CASE
			WHEN (TLF.DaysSanction + TLF.DaysUnpaidLicenseDays) > 0 THEN 1
			ELSE 0
		END,
		CASE
			WHEN TLF.LicenceDays > 0 THEN 1
			ELSE 0
		END,
		TLF.LicenceDays,
		CASE
			WHEN  (Cont.ContractInitialDate >=@PayrollStarDate AND Cont.ContractInitialDate <=@PayrollEndDate) AND CM.NoveltyType IN(1 ,2)  THEN 1
			ELSE 0
		END,
		NULL,
		CASE
			WHEN  (Cont.JobBondingDate >= @PayrollStarDate AND Cont.JobBondingDate <= @PayrollEndDate)   THEN 1
			ELSE 0
		END,
		CASE
			WHEN  (Cont.JobBondingDate >= @PayrollStarDate AND Cont.JobBondingDate <= @PayrollEndDate)   THEN 'X'
			ELSE NULL
		END,
		NULL,
		NULL,
		NULL--LRM
		FROM Payroll.Employee E
		INNER JOIN Payroll.EmployeeType ET ON ET.Id = E.EmployeeTypeId													
		INNER JOIN @TmpAutoliquidationFortnightly TLF  ON E.Id = TLF.IdEmployee
		INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
		INNER JOIN Common.Person P ON P.Id = TP.PersonId
		INNER JOIN DBO.ADTIPOIDENTIFICA ADT ON ADT.ID = P.IdentificationTypeId
		INNER JOIN Payroll.Contract CONT ON CONT.ID = TLF.IdContract
		LEFT JOIN Payroll.ContractModificationReason CM ON CONT.ContractModificationReasonId = CM.ID
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
			-- LEFT JOINs para calcular valores adicionales de ContractLiquidationDetail (solo cuando hay retiro en segunda quincena)
		LEFT JOIN (
			SELECT 
				CL.ContractId,
				SUM(CASE WHEN CONC.AffectIBCPension = 1 THEN CLD.Accrued - CLD.Deducted ELSE 0 END) AS IBCPensionAdditional,
				SUM(CASE WHEN CONC.AffectIBCHealth = 1 THEN CLD.Accrued - CLD.Deducted ELSE 0 END) AS IBCHealthAdditional,
				SUM(CASE WHEN CONC.AffectIBCARP = 1 THEN CLD.Accrued - CLD.Deducted ELSE 0 END) AS IBCProfessionalRiskAdditional,
				SUM(CASE WHEN CONC.AffectIBCCompensationFund = 1 THEN CLD.Accrued - CLD.Deducted ELSE 0 END) AS IBCCompensationFundAdditional
			FROM Payroll.ContractLiquidation CL
			JOIN Payroll.ContractLiquidationDetail CLD ON CLD.ContractLiquidationId = CL.Id
			JOIN Payroll.Concept CONC ON CONC.Id = CLD.IdConcept
			WHERE CL.RetirementDate >= @PayrollStarDate 
			  AND CL.RetirementDate <= @PayrollEndDate
			GROUP BY CL.ContractId
		) CL_Additional ON CL_Additional.ContractId = CONT.Id
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
		e.DateFilingAbroad,
		TLF.LicenceDays,
		Cont.ContractInitialDate,
		CM.NoveltyType,
		ADT.SIGLA,
		CL_Additional.IBCPensionAdditional,
		CL_Additional.IBCHealthAdditional,
		CL_Additional.IBCProfessionalRiskAdditional,
		CL_Additional.IBCCompensationFundAdditional

		-- =============================================
		-- CONSOLIDACIÓN: Empleados con 2+ contratos en el mismo periodo
		-- Escenario: CAMBIO DE SUELDO — primer contrato pagado en quincena 1 y
		-- nuevo contrato en quincena 2 del mismo mes. Ambos comparten JobBondingDate
		-- (misma vinculación laboral, mismo patrono).
		-- Se fusionan en un solo registro base sumando IBC y días cotizados,
		-- conservando los datos del contrato más reciente (MAX IdContract).
		-- =============================================
		DECLARE @MultiContrato TABLE (
			IdEmployee              INT,
			IdContractKeep          INT,
			IBCHealthSum            NUMERIC(18,0),
			IBCPensionSum           NUMERIC(18,0),
			IBCProfRiskSum          NUMERIC(18,0),
			IBCCompFundSum          NUMERIC(18,0),
			IBCOtrosSum             NUMERIC(18,0),
			PensionDaysSum          INT,
			HealthDaysSum           INT,
			ProfRiskDaysSum         INT,
			CompFundDaysSum         INT,
			ValueHealthSum          NUMERIC(18,0),
			ValuePensionSum         NUMERIC(18,0),
			TotalPensionSum         NUMERIC(18,0),
			ValueContribProfRiskSum NUMERIC(18,0),
			ValueContribCCFSum      NUMERIC(18,0),
			ValueSenaSum            NUMERIC(18,0),
			ValueICBFSum            NUMERIC(18,0),
			PensionSolidSum         NUMERIC(18,0),
			PensionSolidSubSum      NUMERIC(18,0),
			TotalHoursSum           INT
		)

		-- 1. Calcular totales consolidados por empleado + fecha de vinculación
		INSERT INTO @MultiContrato
		SELECT
			TA.IdEmployee,
			MAX(TA.IdContract)                                        AS IdContractKeep,
			SUM(TA.IBCHealth)                                         AS IBCHealthSum,
			SUM(TA.IBCPension)                                        AS IBCPensionSum,
			SUM(TA.IBCProfessionalRisk)                               AS IBCProfRiskSum,
			SUM(TA.IBCCompensationFund)                               AS IBCCompFundSum,
			SUM(TA.IBCOtrosParafiscales)                              AS IBCOtrosSum,
			SUM(TA.pensionDays)                                       AS PensionDaysSum,
			SUM(TA.healthDays)                                        AS HealthDaysSum,
			SUM(TA.professionalRiskDays)                              AS ProfRiskDaysSum,
			SUM(TA.compensationFundDays)                              AS CompFundDaysSum,
			SUM(TA.ValueHealth)                                       AS ValueHealthSum,
			SUM(TA.ValuePension)                                      AS ValuePensionSum,
			SUM(TA.TotalPensionContribution)                          AS TotalPensionSum,
			SUM(TA.ValueContributionProfessionalRisk)                 AS ValueContribProfRiskSum,
			SUM(TA.ValueContributionCCF)                              AS ValueContribCCFSum,
			SUM(TA.ValueSena)                                         AS ValueSenaSum,
			SUM(TA.ValueICBF)                                         AS ValueICBFSum,
			SUM(TA.PensionSolidarityFundValueContribution)            AS PensionSolidSum,
			SUM(TA.PensionSolidarityFundValueContributionSubSistence) AS PensionSolidSubSum,
			SUM(TA.TotalHours)                                        AS TotalHoursSum
		FROM @TmpAutoliquidation TA
		INNER JOIN Payroll.Contract C ON C.Id = TA.IdContract
		WHERE TA.retirement = ' '
		GROUP BY TA.IdEmployee, C.JobBondingDate
		HAVING COUNT(*) > 1

		-- 2. Actualizar el contrato más reciente con los valores consolidados
		UPDATE TA
		SET
			IBCHealth                                    = MC.IBCHealthSum,
			IBCPension                                   = MC.IBCPensionSum,
			IBCProfessionalRisk                          = MC.IBCProfRiskSum,
			IBCCompensationFund                          = MC.IBCCompFundSum,
			IBCOtrosParafiscales                         = MC.IBCOtrosSum,
			pensionDays                                  = MC.PensionDaysSum,
			healthDays                                   = MC.HealthDaysSum,
			professionalRiskDays                         = MC.ProfRiskDaysSum,
			compensationFundDays                         = MC.CompFundDaysSum,
			ValueHealth                                  = MC.ValueHealthSum,
			ValuePension                                 = MC.ValuePensionSum,
			TotalPensionContribution                     = MC.TotalPensionSum,
			ValueContributionProfessionalRisk            = MC.ValueContribProfRiskSum,
			ValueContributionCCF                         = MC.ValueContribCCFSum,
			ValueSena                                    = MC.ValueSenaSum,
			ValueICBF                                    = MC.ValueICBFSum,
			PensionSolidarityFundValueContribution       = MC.PensionSolidSum,
			PensionSolidarityFundValueContributionSubSistence = MC.PensionSolidSubSum,
			TotalHours                                   = MC.TotalHoursSum
		FROM @TmpAutoliquidation TA
		INNER JOIN @MultiContrato MC
			ON TA.IdEmployee = MC.IdEmployee AND TA.IdContract = MC.IdContractKeep

		-- 3. Eliminar los contratos anteriores (los que no se mantienen)
		DELETE TA
		FROM @TmpAutoliquidation TA
		INNER JOIN @MultiContrato MC ON TA.IdEmployee = MC.IdEmployee
		WHERE TA.IdContract <> MC.IdContractKeep
		  AND TA.retirement = ' '
		-- FIN CONSOLIDACIÓN

		DECLARE @IdEmployee INT
		DECLARE @IdContract INT
		DECLARE @FlagIGE BIT
		DECLARE @FlagIRL BIT
		DECLARE @FlagLMA BIT
		DECLARE @FlagSanction BIT
		DECLARE @FlagVacation BIT
		DECLARE @IdLiquidation INT
		DECLARE @PensionSolidarityFundValueContribution NUMERIC(18,0)
		DECLARE @PensionSolidarityFundValueContributionSubSistence NUMERIC(18,0)
		DECLARE @RatePension DECIMAL(6,3)
		DECLARE @RateHealth DECIMAL (6,3)
		DECLARE @rateContributionProfessionalRisk DECIMAL(9,5)
		DECLARE @RateSena DECIMAL(6,3)
		DECLARE @RateICBF DECIMAL(6,3)
		DECLARE @RateCompensationFund DECIMAL(6,3)
		DECLARE @ProfessionalRiskCode VARCHAR(6)
		DECLARE @FlagLicenceDays BIT
		DECLARE @FlagContractModificationReason BIT
		DECLARE @FlagEmployeeIngress BIT
		DECLARE @IBCHealth DECIMAL


		

		declare C_Empleados cursor for	
		SELECT IdEmployee, IdContract, FlagIGE, FlagIRL, FlagLMA, FlagSanction, FlagVacation, t.PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsPension, rateContributionsHealth,
		rateContributionProfessionalRisk, rateContributorCCF, rateContributorSENA, rateContributionICBF, ProfessionalRiskCode,l.Id,FlagLicenceDays,FlagContractModificationReason,FlagEmployeeIngress,T.IBCHealth
		FROM @TmpAutoliquidation T
		INNER JOIN  Payroll.Liquidation L ON t.IdEmployee = L.EmployeeId AND t.IdContract = L.ContractId AND L.PayrollDateLiquidated BETWEEN @PayrollStarDate AND @PayrollEndDate

		open C_Empleados 

			fetch next from C_Empleados into @IdEmployee, @IdContract, @FlagIGE, @FlagIRL, @FlagLMA, @FlagSanction, @FlagVacation, @PensionSolidarityFundValueContribution, @PensionSolidarityFundValueContributionSubSistence, @RatePension, @RateHealth,
			@rateContributionProfessionalRisk, @RateCompensationFund, @RateSena, @RateICBF, @ProfessionalRiskCode,@IdLiquidation,@FlagLicenceDays,@FlagContractModificationReason,@FlagEmployeeIngress,@IBCHealth
			while @@FETCH_STATUS = 0 begin
				
						DECLARE C_Novedades CURSOR FOR
						SELECT N.Id, N.TypeNovelty, N.RealDate, N.EndDate,N.AutorizationNumber,N.InabilityClass,N.LicenseClass
						FROM Payroll.Novelty N
						WHERE N.EmployeeId = @IdEmployee
						AND (
							N.RealDate <= @PayrollEndDate
							AND N.EndDate >= @PayrollStarDate
						)

						DECLARE @IdNovelty1 INT
						DECLARE @TypeNovelty1 TINYINT
						DECLARE @RealDate DATE
						DECLARE @EndDate DATE
						DECLARE @AutorizationNumber VARCHAR(30)
						DECLARE @InabilityClass INT
						DECLARE @LicenseClass INT

			OPEN C_Novedades
				FETCH NEXT FROM C_Novedades INTO @IdNovelty1, @TypeNovelty1, @RealDate,@EndDate, @AutorizationNumber,@InabilityClass ,@LicenseClass

				WHILE @@FETCH_STATUS = 0
				BEGIN
				

				--LICENCIAS DE MATERNIDAD O PATERNIDAD
				IF ((@FlagLMA = 1 AND @InabilityClass = 3) OR (@FlagLicenceDays = 1 AND @InabilityClass = 5)) AND @TypeNovelty1 = 1 BEGIN
					
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
				

					SET @MaternityInitialDate = @RealDate 
					SET	@MaterninyEndDate = @EndDate 
					SET	@AuthorizationNumberMaternity = @AutorizationNumber 					
					

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

					SET @IBCMaternityHealth = @IBCHealth

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
					IF (SELECT COUNT(*) FROM @TmpAutoliquidationNovelty WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract AND LMA = 'X'
						AND MaternityLeaveInitialDate = @MaternityInitialDate AND MaternityLeaveEndDate = @MaterninyEndDate) > 0
					BEGIN
						-- Período ya procesado: solo acumular valores para maternidad (dos quincenas mismo período)
						IF @InabilityClass = 3
						BEGIN
							UPDATE @TmpAutoliquidationNovelty
								SET
									valueMaternityLicense = valueMaternityLicense + @ValueMaternity,
									ValueICBF = ValueICBF + @ValueMaternityICBF,
									ValueContributionCCF = ValueContributionCCF + @ValueMaternityCCF,
									ValueHealth = ValueHealth + @ValueMaternityHealth,
									ValuePension = ValuePension + @ValueMaternityPension
							WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract AND LMA = 'X'
							AND MaternityLeaveInitialDate = @MaternityInitialDate AND MaternityLeaveEndDate = @MaterninyEndDate
						END
						-- Paternidad (InabilityClass=5): período ya insertado, no duplicar
					END
					ELSE
					BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,LicenceInitialDate,LicenceEndate,LRM)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', 'X', ' ', 0, @MaternityHealthDays, @MaternityHealthDays, @MaternityHealthDays, @MaternityHealthDays,
							@IBCMaternityHealth, @IBCMaternityHealth, @IBCMaternityHealth, @IBCMaternityHealth, @RatePension, @ValueMaternityPension, @ValueMaternityPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueMaternityHealth,
							'',0,@AuthorizationNumberMaternity,@ValueMaternity, 0,
							0, @RateCompensationFund, @ValueMaternityCCF, @RateSena, @ValueMaternitySENA, @RateICBF, @ValueMaternityICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, @MaternityInitialDate, @MaterninyEndDate,
							NULL, NULL, NULL, NULL, 0,0,NULL,NULL,' ')

							END

					-- Paternidad: limpiar LicenceDays del registro base para evitar doble reporte como PE/C con fecha nula
					IF @InabilityClass = 5
					BEGIN
						UPDATE @TmpAutoliquidation
						SET LicenceDays = 0
						WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract
					END
				END

				--LICENCIAS REMUNERADAS
				IF @TypeNovelty1 = 3 AND @LicenseClass IN( 1,3,5,6,7)  BEGIN
					DECLARE @LicenceInitialDate DATE
					DECLARE @LicenceEndDate DATE
					DECLARE @IBCLicenceHealth NUMERIC(18,0)
					DECLARE @ValueLicencePension NUMERIC(18,0)
					DECLARE @ValueLicenceHealth NUMERIC(18,0)
					DECLARE @ValueLicenceSENA NUMERIC(18,0)
					DECLARE @ValueLicenceICBF NUMERIC(18,0)
					DECLARE @ValueLicenceCCF NUMERIC(18,0)
					DECLARE @LicenceHealthDays INT 

					DECLARE @IbcLicence NUMERIC(18,0) = 0
					DECLARE @IdNoveltyL int
					DECLARE @TypeNoveltyL tinyint
					DECLARE @EmployeeBaseSalaryL NUMERIC(18,0)
					DECLARE @IBCTableL NUMERIC(18,0)
					DECLARE @RateContributionsHealthL DECIMAL(6,3)
					DECLARE @RateContributionsPensionL DECIMAL(6,3)
					DECLARE @MinimumSalaryL NUMERIC(18,0)
					DECLARE @CreeTaxLI BIT
					DECLARE @BasicSalaryLI NUMERIC
					
					
					
					 
					SET @LicenceInitialDate = @RealDate
					SET @LicenceEndDate = @EndDate
					SET @AuthorizationNumberMaternity = @AutorizationNumber     
									

	
					SELECT @EmployeeBaseSalaryL = EmployeeBaseSalary, @IBCTableL = IBC, @TypeNoveltyL = TypeNovelty 
					FROM Payroll.Novelty 
					where EmployeeId = @IdEmployee and RealDate = @LicenceInitialDate

					Select @RateContributionsHealthL = PP.EmployerHealthContributionPercentage/100,
					@rateContributionsPensionL= PP.EmployerPensionContributionPercentage/100,
					@MinimumSalaryL = PP.LegalSalaryMinimum,
					@BasicSalaryLI = con.BasicSalary
					FROM Payroll.PayrollParameter PP 
					LEFT JOIN Payroll.[Group] G ON G.PayrollParameterId = PP.Id
					LEFT JOIN Payroll.Contract CON on CON.GroupId = G.Id
					LEFT JOIN Payroll.Employee E ON E.Id = CON.EmployeeId
					where e.Id = @IdEmployee AND CON.ID = @IdContract

					SELECT @CreeTaxLI = CreeTax FROM Payroll.PayrollSettings

					IF @EmployeeBaseSalaryL > 0 AND  @IBCTableL = 0 BEGIN
						SET @IbcLicence = @EmployeeBaseSalaryL
					END

					IF @EmployeeBaseSalaryL = 0 AND  @IBCTableL > 0 BEGIN
						SET @IbcLicence = @IBCTableL
					END

					IF @EmployeeBaseSalaryL > 0 AND  @IBCTableL > 0 BEGIN
						SET @IbcLicence = @EmployeeBaseSalaryL
					END
					
					IF @LicenceInitialDate < @PayrollStarDate BEGIN
						SET @LicenceInitialDate = @PayrollStarDate
					END

					IF @LicenceEndDate > @PayrollEndDate BEGIN
						SET @LicenceEndDate = @PayrollEndDate
					
					END
	
					SET @IBCLicenceHealth = @IBCHealth

					SET @LicenceHealthDays = [Payroll].[fnCalculateDays360] (@LicenceInitialDate, @LicenceEndDate)

					SET @RatePension = @RateContributionsPensionL

					SET @ValueLicencePension = @IbcLicence * @RatePension
					SET @ValueLicencePension = CEILING(@ValueLicencePension / 100) * 100

					----------------------------------------------------------
					IF (@MinimumSalaryL*10) > @BasicSalaryLI  AND @CreeTaxLI= 1  BEGIN 

						SET @RateHealth = 0.00
					END
						ELSE 
						BEGIN 
					
						Set @RateHealth = @RateContributionsHealthL  

					END					
					--------------------------------------------------------


					SET @ValueLicenceHealth = 0
					SET @ValueLicenceHealth = CEILING(@ValueLicenceHealth / 100) * 100


					SET @ValueLicenceSENA = 0
					SET @ValueLicenceSENA = CEILING(@ValueLicenceSENA / 100) * 100

					SET @ValueLicenceICBF = 0
					SET @ValueLicenceICBF = CEILING(@ValueLicenceICBF / 100) * 100


					IF @PensionSolidarityFundValueContribution > 0 BEGIN
						SET @PensionSolidarityFundValueContribution = CEILING(((@IbcLicence * 0.01) / 2) / 100) * 100
						IF @PensionSolidarityFundValueContributionSubSistence > 0 BEGIN
							SET @PensionSolidarityFundValueContributionSubSistence= CEILING(((@IbcLicence * 0.01) / 2) / 100) * 100
						END
					END
					IF(SELECT COUNT(*) FROM @TmpAutoliquidationNovelty WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract AND LicenceInitialDate = @LicenceInitialDate AND LicenceEndate = @LicenceEndDate ) = 0
					--If
					-- @LicenceInitialDate IS NOT NULL
					--AND @LicenceEndDate IS NOT NULL
					--AND NOT EXISTS (
					--	SELECT 1
					--	FROM @TmpAutoliquidationNovelty T
					--	WHERE 
					--		T.IdEmployee        = @IdEmployee
					--	AND T.IdContract        = @IdContract
					--	AND T.LicenceInitialDate = @LicenceInitialDate
					--	AND T.LicenceEndate    = @LicenceEndDate
					--)
					BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,LicenceInitialDate,LicenceEndate,LRM)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', ' ', 0, @LicenceHealthDays, @LicenceHealthDays, @LicenceHealthDays, @LicenceHealthDays,
							@IBCLicenceHealth, @IBCLicenceHealth, @IBCLicenceHealth, @IBCLicenceHealth, @RatePension, @ValueLicencePension, @ValueLicencePension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueMaternityHealth,
							'',0,'',0, 0,
							0, 0, @ValueLicenceICBF, 0, @ValueLicenceSENA, 0, @ValueLicenceICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, 0,@LicenceHealthDays,@LicenceInitialDate,@LicenceEndDate,'X')
					END
				END


				--FIN LICENCIAS REMUNERADAS

				---LICENCIAS NO REMUNERADAS (sanciones TypeNovelty=2 excluidas del archivo CCSS)
				IF @LicenseClass = 2    BEGIN
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
					
					
					
					 
						SET @SanctionInitialDate = @RealDate
						SET @SanctionEndDate = @EndDate
						SET @AuthorizationNumberMaternity = @AutorizationNumber     
									

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
	
					SET @IBCSanctionHealth = @IBCHealth

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
					IF(SELECT COUNT(*) FROM @TmpAutoliquidationNovelty WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract AND SanctionInitialDate = @SanctionInitialDate AND SanctionEndDate = @SanctionEndDate ) = 0
					BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,LicenceInitialDate,LicenceEndate,LRM)
					VALUES(@IdEmployee, @IdContract, 'X', ' ', ' ', ' ', 0, @SanctionHealthDays, @SanctionHealthDays, @SanctionHealthDays, @SanctionHealthDays,
							@IBCSanctionHealth, @IBCSanctionHealth, @IBCSanctionHealth, @IBCSanctionHealth, @RatePension, @ValueSanctionPension, @ValueSanctionPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueMaternityHealth,
							'',0,'',0, 0,
							0, 0, @ValueSanctionICBF, 0, @ValueSanctionSENA, 0, @ValueSanctionICBF,
							@ProfessionalRiskCode, @SanctionInitialDate, @SanctionEndDate, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, 0,0,NULL,NULL,' ')
					END
				END

				--INCAPACIDAD PROFESIONAL / CCSS ESPECIAL (InabilityClass=4 riesgo laboral, InabilityClass=6 código 999)
				IF (@FlagIRL = 1 AND @InabilityClass = 4) OR (@TypeNovelty1 = 1 AND @InabilityClass = 6) BEGIN

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

					
					SET @IRLInitialDate = @RealDate 
					SET	@IRLEndDate = @EndDate 
					SET	@AuthorizationNumberMaternity = @AutorizationNumber     				

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

					SET @IBCIRLHealth = @IBCHealth
					 

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
					IF NOT EXISTS (
						SELECT 1
						FROM @TmpAutoliquidationNovelty
						WHERE IdEmployee = @IdEmployee
						AND IdContract = @IdContract
						AND FechaInicioIRL = @IRLInitialDate
						AND FechaFinIRL = @IRLEndDate
					)
					BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,LicenceInitialDate,LicenceEndate,LRM)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', ' ', isnull(@IRLHealthDays,0), @IRLHealthDays, @IRLHealthDays, @IRLHealthDays, @IRLHealthDays,
							@IBCIRLHealth, @IBCIRLHealth, @IBCIRLHealth, @IBCIRLHealth, @RatePension, @ValueIRLPension, @ValueIRLPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueIRLHealth,
							'',0,'',0, 0,
							0, @RateCompensationFund, @ValueIRLCCF, @RateSena, @ValueIRLSENA, @RateICBF, @ValueIRLICBF,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							NULL, NULL, @IRLInitialDate, @IRLEndDate, 0,0,NULL,NULL,' ')
					END
				END

				--INCAPACIDAD GENERAL (excluye InabilityClass=6 que se maneja como IRL arriba)
				IF @FlagIGE = 1 AND @InabilityClass <> 6 BEGIN
					
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
					

					SET @IGEInitialDate = @RealDate
					SET @IGEEndDate = @EndDate
					SET @AuthorizationNumberDisability = @AutorizationNumber

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

					SELECT @ValueIGE = SUM(ConceptTotalValue) FROM Payroll.LiquidationDetail WHERE PayrollId = @IdLiquidation and ConceptClass IN ('021','022')

					IF @ValueIGE IS NULL BEGIN
						SET @ValueIGE = 0
					END

					DECLARE @AjusteIncapacidad NUMERIC(18,0) = 0

					SELECT @AjusteIncapacidad = isnull(LD.ConceptTotalValue,0)
					FROM Payroll.LiquidationDetail LD
					where LD.PayrollDate = @PayrollEndDate and ld.PayrollId = @IdLiquidation
					and LD.ConceptCode = '1009'

					SET @ValueIGE = @ValueIGE + @AjusteIncapacidad

					SET @IBCIGEHealth = @IBCHealth

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
				IF NOT EXISTS (
					SELECT 1
					FROM @TmpAutoliquidationNovelty
					WHERE IdEmployee = @IdEmployee
					AND IdContract = @IdContract
					AND AmbulatoryDisabilityInitialDate = @IGEInitialDate
					AND AmbulatoryDisabilityEndDate = @IGEEndDate
				)
				BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,LicenceInitialDate,LicenceEndate,LRM)

					VALUES(@IdEmployee, @IdContract, ' ', 'X', ' ', ' ', 0, @IGEHealthDays, @IGEHealthDays, @IGEHealthDays, @IGEHealthDays,
							@IBCIGEHealth, @IBCIGEHealth, @IBCIGEHealth, @IBCIGEHealth, @RatePension, @ValueIGEPension, @ValueIGEPension,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, @ValueIGEHealth,
							@AuthorizationNumberDisability,@ValueIGE,'',0, 0,
							0, @RateCompensationFund, @ValueIGECCF, @RateSena, @ValueIGESENA, @RateICBF, @ValueIGEICBF,
							@ProfessionalRiskCode, NULL, NULL, @IGEInitialDate, @IGEEndDate, NULL, NULL,
							NULL, NULL, NULL, NULL, 0,0,NULL,NULL,' ')
					END	
				END

							FETCH NEXT FROM C_Novedades INTO @IdNovelty, @TypeNovelty, @RealDate, @EndDate,@AutorizationNumber,@InabilityClass,@LicenseClass
						END

						CLOSE C_Novedades
						DEALLOCATE C_Novedades
				
				---MODIFICACION DE CONTRATO
				IF @FlagContractModificationReason = 1 BEGIN
					DECLARE @NoveltyType TINYINT
					SELECT @NoveltyType = CM.NoveltyType
					FROM Payroll.Contract C
					JOIN Payroll.ContractModificationReason CM ON C.ContractModificationReasonId = CM.Id
					WHERE C.ID= @IdContract AND  (C.ContractInitialDate >=@PayrollStarDate AND C.ContractInitialDate <=@PayrollEndDate) AND CM.NoveltyType = 2 --Se deja un y dos porque son lasnovedades por cambio de salario o cargo
					
				

					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,NoveltyType)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', ' ', 0, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, @RatePension, NULL, NULL,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, NULL,
							'',0,'',0, 0,
							0, @RateCompensationFund, NULL, @RateSena, NULL, @RateICBF, NULL,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, 0,0,@NoveltyType)


				END

				--INGRESO EN LA NOMINA A PAGAR
				IF @FlagEmployeeIngress = 1 BEGIN				

				IF NOT EXISTS(SELECT 1 FROM @TmpAutoliquidationNovelty WHERE IdEmployee = @IdEmployee AND EmployeeIngressN IS NOT NULL)
				   AND NOT EXISTS(SELECT 1 FROM @TmpAutoliquidation WHERE IdEmployee = @IdEmployee AND IdContract = @IdContract AND EmployeeIngress IS NOT NULL)
				BEGIN
					INSERT INTO @TmpAutoliquidationNovelty(IdEmployee, IdContract, SLN, IGE, LMA , VAC , IRL, pensionDays , healthDays , professionalRiskDays, compensationFundDays, 
								IBCPension, IBCHealth , IBCProfessionalRisk , IBCCompensationFund, rateContributionsPension , ValuePension , TotalPensionContribution,
								PensionSolidarityFundValueContribution, PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth,
								authorizationNumberDisability , valueGeneralDisability , authorizationNumberMaternityLicense, valueMaternityLicense, rateContributionProfessionalRisk,
								ValueContributionProfessionalRisk, rateContributorCCF, ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF,
								ProfessionalRiskCode, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, MaternityLeaveInitialDate, MaternityLeaveEndDate,
								VacationInitialDate, VacationEndDate, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales,LicenceDays,NoveltyType,EmployeeIngressN)
					VALUES(@IdEmployee, @IdContract, ' ', ' ', ' ', ' ', 0, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, @RatePension, NULL, NULL,
							ISNULL(@PensionSolidarityFundValueContribution,0), ISNULL(@PensionSolidarityFundValueContributionSubSistence,0), @RateHealth, NULL,
							'',0,'',0, 0,
							0, @RateCompensationFund, NULL, @RateSena, NULL, @RateICBF, NULL,
							@ProfessionalRiskCode, NULL, NULL, NULL, NULL, NULL, NULL,
							NULL, NULL, NULL, NULL, 0,0,0,'X')
					END
				END

			fetch next from C_Empleados into @IdEmployee, @IdContract, @FlagIGE, @FlagIRL, @FlagLMA, @FlagSanction, @FlagVacation, @PensionSolidarityFundValueContribution, @PensionSolidarityFundValueContributionSubSistence, @RatePension, @RateHealth,
			@rateContributionProfessionalRisk, @RateCompensationFund, @RateSena, @RateICBF, @ProfessionalRiskCode,@IdLiquidation,@FlagLicenceDays,@FlagContractModificationReason,@FlagEmployeeIngress,@IBCHealth
			end -- fin while de C_Empleados
		close C_Empleados 
		deallocate C_Empleados


	

		DELETE @TmpAutoliquidationNovelty WHERE pensionDays IS NULL AND healthDays IS NULL AND professionalRiskDays IS NULL AND compensationFundDays IS NULL AND EmployeeIngressN IS NULL
		DELETE @TmpAutoliquidationNovelty WHERE pensionDays = 0 AND healthDays = 0 AND professionalRiskDays = 0 AND compensationFundDays = 0 AND EmployeeIngressN IS NULL

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
					FechaEmpleadoExterior, EconomicActivityARL, FlagVacation, FlagLMA, FlagIRL, FlagIGE,FlagSanction,LicenceDays,NoveltyType,EmployeeIngress,LicenceInitialDate,LicenceEndDate,LRM)
	
		SELECT	TLN.IdEmployee, TLN.IdContract, 0, TA.typeDocument, TA.Nit, TA.typeContractEmployee, TA.subTypeEmployee, TA.foreignNotBound, TA.colombianForeignResident, TA.CodeCityBranchOffice,
				TA.FirstLastName, TA.SecondLastName, TA.FirstName, TA.SecondName, TA.[entry], TA.retirement, TA.TDE, TA.TAE, TA.TDP, TA.TAP, TA.VSP, TA.Correcciones, TA.VST, TLN.SLN, TLN.IGE, TLN.LMA, TLN.VAC, TA.AVP, TA.VCT, TLN.IRL, TA.pensionAdministratorCode,
				TA.transferPensionAdministratorCode, TA.EPSCode, TA.transferEPSCode, TA.CCFCode, TLN.pensionDays, TLN.healthDays, TLN.professionalRiskDays, TLN.compensationFundDays, TA.BasicSalary, TA.integralSalary, TLN.IBCPension,
				tln.IBCHealth, tln.IBCProfessionalRisk, tln.IBCCompensationFund, tln.rateContributionsPension, TLN.ValuePension, TA.voluntaryContributionPensionValue, TA.voluntaryContributionPensionValuePatron,
				TLN.TotalPensionContribution, TLN.PensionSolidarityFundValueContribution, TLN.PensionSolidarityFundValueContributionSubSistence, TA.ValueNotRetainedByVoluntaryContributions,
				TLN.rateContributionsHealth, TLN.ValueHealth, TA.valueAditionalUPC, TLN.authorizationNumberDisability, TLN.valueGeneralDisability, TLN.authorizationNumberMaternityLicense,
				TLN.valueMaternityLicense, TLN.rateContributionProfessionalRisk, TA.WorkCenter, TLN.ValueContributionProfessionalRisk, TLN.rateContributorCCF, TLN.ValueContributionCCF,
				TLN.rateContributorSENA, TLN.ValueSena, TLN.rateContributionICBF, TLN.ValueICBF, TA.rateContributorESAP, TA.rateContributorEducationMinistry, TA.MinistryCodeRiskFound,
				TLN.ProfessionalRiskCode,TA.TarifaEspecialPensiones, TA.IngressDate, TA.DateRetirement, TLN.SanctionInitialDate, TLN.SanctionEndDate, TLN.AmbulatoryDisabilityInitialDate, TLN.AmbulatoryDisabilityEndDate,
				TLN.MaternityLeaveInitialDate, TLN.MaternityLeaveEndDate, TLN.VacationInitialDate, TLN.VacationEndDate, TA.FechaInicioVCT, TA.FechaFinVCT, TLN.FechaInicioIRL, TLN.FechaFinIRL, TLN.IBCOtrosParafiscales, TA.TotalHours,
				TA.FechaEmpleadoExterior, TA.EconomicActivityARL, TA.FlagVacation, TA.FlagLMA, TA.FlagIRL, TA.FlagIGE, TA.FlagSanction,TLN.LicenceDays,TLN.NoveltyType,TLN.EmployeeIngressN,TLN.LicenceInitialDate,TLN.LicenceEndate,TLN.LRM
		FROM @TmpAutoliquidationNovelty TLN, @TmpAutoliquidation TA
		WHERE TA.IdEmployee = TLN.IdEmployee and tln.IdContract = ta.IdContract
	


DECLARE @IdEmployeeRetirement int
DECLARE @IdContractRetirement int
DECLARE @IdContractLiquidation int
DECLARE @IsFromContractLiquidation BIT -- Flag para identificar si viene de ContractLiquidation (1) o Contract directamente (0)

declare C_Retiros cursor for	
-- Contratos con liquidación confirmada (ContractLiquidation)
SELECT CL.EmployeeId, CL.ContractId, CL.Id, 1 AS IsFromContractLiquidation
FROM Payroll.ContractLiquidation CL
INNER JOIN Payroll.Employee E ON E.Id = CL.EmployeeId
INNER JOIN Payroll.WorkCenter WC ON WC.Id = E.WorkCenterId
WHERE CL.RetirementDate >= @PayrollStarDate 
  AND CL.RetirementDate <= @PayrollEndDate 
  AND WC.Code = @WorkCenterCode

UNION ALL

-- Contratos parcialmente retirados (Status = 5) que NO están en ContractLiquidation
SELECT C.EmployeeId, C.Id AS ContractId, NULL AS ContractLiquidationId, 0 AS IsFromContractLiquidation
FROM Payroll.Contract C
INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
WHERE C.Status = 5 -- Parcialmente retirado
  AND C.RetirementDate >= @PayrollStarDate 
  AND C.RetirementDate <= @PayrollEndDate
  AND C.Valid = 1 -- Contrato válido
  AND NOT EXISTS (
      -- Excluir si ya está en ContractLiquidation
      SELECT 1 
      FROM Payroll.ContractLiquidation CL 
      WHERE CL.ContractId = C.Id 
        AND CL.RetirementDate = C.RetirementDate
  )

--Liquidacion de contrato 
OPEN C_Retiros
FETCH NEXT FROM C_Retiros INTO @IdEmployeeRetirement, @IdContractRetirement, @IdContractLiquidation, @IsFromContractLiquidation
WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @Cantidad INT = 0
    DECLARE @RetirementDate DATE
    DECLARE @IbcRetirementCompensationFund NUMERIC(18,0)
    DECLARE @RateCCF DECIMAL(6,3)
    DECLARE @CCfContributionValue NUMERIC(18,0)
    DECLARE @RetirementDay INT
    DECLARE @PayrollIdC INT

    DECLARE @IBCHealthRetirement NUMERIC(18,0) = 0
    DECLARE @ValueHealthRetirement NUMERIC(18,0) = 0
    DECLARE @HealthRetirementDays INT
    DECLARE @ValuePensionRetirement NUMERIC(18,0)= 0
    DECLARE @RateHealthRetirement DECIMAL (6,3) = 0
    DECLARE @RatePensionRetirement DECIMAL (6,3) = 0
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

    SELECT
        @ContractClass = CT.ContractClass,
        @LegalSalaryMininumRetirement = PP.LegalSalaryMinimum,
        @rateContributionRetirementICBF = PP.ICBFContributionPercentage,
        @rateContributorRetirementSENA = PP.SenaContributionPercentage,
        @rateContributorRetirementCCF = PP.CompensationFundContributionPercentage,
        @EmployeeHealthContributionPercentage = PP.EmployeeHealthContributionPercentage,
        @EmployerPensionContributionPercentage = PP.EmployerPensionContributionPercentage,
        @EmployeePensionContributionPercentage = PP.EmployeePensionContributionPercentage,
        @EmployerHealthContributionRetirementPercentage = PP.EmployerHealthContributionPercentage,
        @ProfessionalRiskRateRetirement = E.ProfessionalRiskPercentage
    FROM Payroll.Contract C
    JOIN Payroll.[Group] G ON C.GroupId = G.Id
    JOIN Payroll.PayrollParameter PP ON G.PayrollParameterId = PP.Id
    JOIN Payroll.ContractType CT ON CT.Id = C.ContractTypeId
    JOIN Payroll.Employee E ON C.EmployeeId = E.Id
    WHERE C.Id = @IdContractRetirement

    -- Obtener la fecha de retiro según el origen
    IF @IsFromContractLiquidation = 1
    BEGIN
        SELECT @RetirementDate = RetirementDate 
        FROM Payroll.ContractLiquidation 
        WHERE Id = @IdContractLiquidation

        SELECT @IBCHealthRetirement = ISNULL(SUM(CLD.Accrued - CLD.Deducted), 0)
        FROM Payroll.ContractLiquidationDetail CLD
        JOIN Payroll.Concept CONC ON CONC.Id = CLD.IdConcept
        WHERE CLD.ContractLiquidationId = @IdContractLiquidation
          AND CONC.AffectIBCHealth = 1

        SET @HealthRetirementDays = [Payroll].[fnCalculateDays360](@PayrollStarDate, @RetirementDate)

        IF DAY(@RetirementDate) <= 15
        BEGIN
            IF @IBCHealthRetirement = 0 -- Si en la liquidacion de contrato no existe dias trabajados, entonces se buscan en la nomina del empleado
            BEGIN
                SELECT @IBCHealthRetirement = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
                FROM Payroll.Liquidation L
                JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
                JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
                WHERE L.ContractId = @IdContractRetirement
                  AND L.PayrollDateLiquidated = DATEFROMPARTS(YEAR(@RetirementDate), MONTH(@RetirementDate), 15)
            END
        END
        ELSE
        BEGIN
            DECLARE @IBCHealthFirstHalf NUMERIC(18,0) = 0
            DECLARE @IBCHealthSecondHalf NUMERIC(18,0) = 0
            DECLARE @IBCHealthTotal NUMERIC(18,0) = 0
            
            -- Caso B: Retiro en segunda quincena 
            -- 1. IBC salud primera quincena (nómina normal)
            SELECT @IBCHealthFirstHalf = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
            FROM Payroll.Liquidation L
            JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
            JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
            WHERE L.ContractId = @IdContractRetirement
              AND L.PayrollDateLiquidated = DATEFROMPARTS(YEAR(@RetirementDate), MONTH(@RetirementDate), 15)

            -- 2. IBC salud segunda quincena (siempre intentar obtener de nómina primero)
            SELECT @IBCHealthSecondHalf = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
            FROM Payroll.Liquidation L
            JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
            JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
            WHERE L.ContractId = @IdContractRetirement
              AND L.PayrollDateLiquidated = EOMONTH(@RetirementDate)

            -- Si no se encontró segunda quincena en nómina, usar ContractLiquidationDetail como respaldo 
            IF @IBCHealthSecondHalf = 0 AND @IBCHealthRetirement <> 0
            BEGIN
                SET @IBCHealthSecondHalf = @IBCHealthRetirement
            END

            -- Total: Primera quincena + Segunda quincena (o ContractLiquidationDetail si no hay segunda quincena) 
            SET @IBCHealthTotal = @IBCHealthFirstHalf + @IBCHealthSecondHalf
            SET @IBCHealthRetirement = @IBCHealthTotal
            SET @HealthRetirementDays = [Payroll].[fnCalculateDays360](@PayrollStarDate, @RetirementDate)
        END

        SET @IbcRetirementCompensationFund = @IBCHealthRetirement
    END
    ELSE
    BEGIN
        -- Si viene de Contract directamente (Status = 5), obtener la fecha desde Contract
        SELECT @RetirementDate = RetirementDate 
        FROM Payroll.Contract 
        WHERE Id = @IdContractRetirement

        SET @HealthRetirementDays = [Payroll].[fnCalculateDays360](@PayrollStarDate, @RetirementDate)

        -- Para contratos Status = 5, calcular IBC desde las liquidaciones del mes
        -- No hay ContractLiquidationDetail, así que se calcula desde LiquidationDetail
        IF DAY(@RetirementDate) <= 15
        BEGIN
            -- Caso A: Retiro en primera quincena
            SELECT @IBCHealthRetirement = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
            FROM Payroll.Liquidation L
            JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
            JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
            WHERE L.ContractId = @IdContractRetirement
              AND L.PayrollDateLiquidated = DATEFROMPARTS(YEAR(@RetirementDate), MONTH(@RetirementDate), 15)
        END
        ELSE
        BEGIN
            DECLARE @IBCHealthFirstHalf_Status5 NUMERIC(18,0) = 0
            DECLARE @IBCHealthSecondHalf_Status5 NUMERIC(18,0) = 0
            
            -- Caso B: Retiro en segunda quincena
            -- 1. IBC salud primera quincena (nómina normal)
            SELECT @IBCHealthFirstHalf_Status5 = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
            FROM Payroll.Liquidation L
            JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
            JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
            WHERE L.ContractId = @IdContractRetirement
              AND L.PayrollDateLiquidated = DATEFROMPARTS(YEAR(@RetirementDate), MONTH(@RetirementDate), 15)

            -- 2. IBC salud segunda quincena
            SELECT @IBCHealthSecondHalf_Status5 = ISNULL(SUM(LD.AccruedValue - LD.DeductedValue), 0)
            FROM Payroll.Liquidation L
            JOIN Payroll.LiquidationDetail LD ON LD.PayrollId = L.Id
            JOIN Payroll.Concept C ON C.Id = LD.ConceptId AND C.AffectIBCHealth = 1
            WHERE L.ContractId = @IdContractRetirement
              AND L.PayrollDateLiquidated = EOMONTH(@RetirementDate)

            -- Total: Primera quincena + Segunda quincena
            SET @IBCHealthRetirement = @IBCHealthFirstHalf_Status5 + @IBCHealthSecondHalf_Status5
        END

        -- Para Status = 5, las tasas y valores se mantienen en 0 (igual que en la lógica original)
        SET @IbcRetirementCompensationFund = @IBCHealthRetirement
    END

    IF (
        SELECT COUNT(*) 
        FROM @TmpAutoliquidationRetirement 
        WHERE IdEmployee = @IdEmployeeRetirement 
          AND IdContract = @IdContractRetirement 
          AND FlagRetirement IS NULL
    ) = 0
    BEGIN
        INSERT INTO @TmpAutoliquidationRetirement (
            IdEmployee, IdContract, retirementDate, typeDocument, Nit, typeContractEmployee, subTypeEmployee, 
            foreignNotBound, colombianForeignResident, CodeCityBranchOffice, FirstLastName, SecondLastName, 
            FirstName, SecondName, pensionDays, healthDays, professionalRiskDays, compensationFundDays,
            IBCPension, IBCHealth, IBCProfessionalRisk, IBCCompensationFund, rateContributionsPension, 
            ValuePension, TotalPensionContribution, PensionSolidarityFundValueContribution, 
            PensionSolidarityFundValueContributionSubSistence, rateContributionsHealth, ValueHealth, 
            rateContributionProfessionalRisk, ValueContributionProfessionalRisk, rateContributorCCF, 
            ValueContributionCCF, rateContributorSENA, ValueSena, rateContributionICBF, ValueICBF, 
            ProfessionalRiskCode, IBCOtrosParafiscales, BasicSalary, EPSCode, CCFCode, pensionAdministratorCode, 
            MinistryCodeRiskFound, WorkCenter, TarifaEspecialPensiones, IntegralSalary, FlagRetirement
        )
        SELECT 
            ISNULL(CL.EmployeeId, C.EmployeeId), 
            ISNULL(CL.ContractId, C.Id), 
            @RetirementDate,
            ADT.SIGLA, 
            TP.Nit, 
            CASE 
                WHEN CT.ContractClass = 2 THEN '12'
                WHEN CT.ContractClass = 3 THEN '01'
                WHEN CT.ContractClass = 4 THEN '01'
                WHEN E.PensionaryStatus IS NOT NULL THEN '05'
                ELSE '01'
            END, 
            '00', 
            ' ',  
            CASE E.ContributorAbroad WHEN 0 THEN ' ' WHEN 1 THEN 'X' END,
            CityBranch.Code, 
            P.FirstLastName, 
            P.SecondLastName,
            P.FirstName, 
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
            ISNULL(CONT.BasicSalary, C.BasicSalary),
            ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F 
                    WHERE FC.ContractId = ISNULL(CONT.Id, C.Id) 
                      AND F.Id = FC.FundId AND FC.State = 1 AND FC.FundType = 1 AND VoluntaryContribution = 0), 'SINEPS'), 
            ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F 
                    WHERE FC.ContractId = ISNULL(CONT.Id, C.Id) 
                      AND F.Id = FC.FundId AND FC.State = 1 AND FC.FundType = 5 AND VoluntaryContribution = 0), 'SINCCF'), 
            ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F 
                    WHERE FC.ContractId = ISNULL(CONT.Id, C.Id) 
                      AND F.Id = FC.FundId AND FC.State = 1 AND FC.FundType = 2 AND VoluntaryContribution = 0), 'SINAFP'),
            ISNULL((SELECT TOP 1 F.MinistryCode FROM Payroll.FundContract FC, Payroll.Fund F 
                    WHERE FC.ContractId = ISNULL(CONT.Id, C.Id) 
                      AND F.Id = FC.FundId AND FC.State = 1 AND FC.FundType = 4 AND VoluntaryContribution = 0), 'SINARL'),
            WC.Code,
            CASE WHEN PP.SpecialPensionRateIndicator = 0 THEN ' ' ELSE PP.SpecialPensionRateIndicator END,
            CASE 
                WHEN CT.SalaryType = 1 AND @PayrollStarDate > '2020-07-01' THEN 'F'
                WHEN CT.SalaryType = 2 THEN 'X'
                WHEN CT.SalaryType = 3 AND @PayrollStarDate > '2020-07-01' THEN 'V'
                ELSE ' '
            END,
            'X'
        FROM Payroll.Employee E
        INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
        INNER JOIN Common.Person P ON P.Id = TP.PersonId
        INNER JOIN DBO.ADTIPOIDENTIFICA ADT ON ADT.ID = P.IdentificationTypeId
        INNER JOIN Payroll.WorkCenter WC ON WC.Id = E.WorkCenterId
        LEFT JOIN Payroll.ContractLiquidation CL ON CL.Id = @IdContractLiquidation AND @IsFromContractLiquidation = 1
        LEFT JOIN Payroll.Contract CONT ON CONT.Id = CL.ContractId AND @IsFromContractLiquidation = 1
        LEFT JOIN Payroll.Contract C ON C.Id = @IdContractRetirement AND @IsFromContractLiquidation = 0
        INNER JOIN Payroll.ContractType CT ON CT.Id = ISNULL(CONT.ContractTypeId, C.ContractTypeId)
        INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = ISNULL(CONT.FunctionalUnitId, C.FunctionalUnitId)
        INNER JOIN Payroll.BranchOffice BA ON BA.ID = FU.BranchOfficeId
        INNER JOIN Common.City CityBranch ON CityBranch.Id = BA.CityId
        INNER JOIN Payroll.[Group] G ON G.Id = ISNULL(CONT.GroupId, C.GroupId)
        INNER JOIN Payroll.PayrollParameter PP ON PP.ID = G.PayrollParameterId
        INNER JOIN Payroll.Position POS ON POS.Id = ISNULL(CONT.PositionId, C.PositionId)
        INNER JOIN Payroll.ProfessionalRisk PR ON PR.Id = POS.ProfessionalRiskLevelId
        WHERE E.Id = @IdEmployeeRetirement
          AND (
              (@IsFromContractLiquidation = 1 AND CL.Id IS NOT NULL)
              OR 
              (@IsFromContractLiquidation = 0 AND C.Id IS NOT NULL)
          )
    END

    FETCH NEXT FROM C_Retiros INTO @IdEmployeeRetirement, @IdContractRetirement, @IdContractLiquidation, @IsFromContractLiquidation
END -- fin while de C_Retiros
CLOSE C_Retiros
DEALLOCATE C_Retiros



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
		EconomicActivityARL,ContractId,LicenceDays,NoveltyContract,EmployeeIngressNovelty,LicenceInitialDate,LicenceEndDate,LRM)
		SELECT 0, @PayrollStarDate, IdEmployee, typeContractEmployee, subTypeEmployee, foreignNotBound, colombianForeignResident, CodeCityBranchOffice, [entry], retirement,
		TDE, TAE, TDP, TAP, VSP, Correcciones, VST, SLN, IGE, LMA, VAC, AVP, VCT, IRL, pensionAdministratorCode, transferPensionAdministratorCode, EPSCode, transferEPSCode,
		CCFCode, ISNULL(pensionDays,0), ISNULL(healthDays,0), ISNULL(professionalRiskDays,0), ISNULL(compensationFundDays,0), BasicSalary, integralSalary, ISNULL(IBCPension,0), ISNULL(IBCHealth,0), ISNULL(IBCProfessionalRisk,0), ISNULL(IBCCompensationFund,0), 
		rateContributionsPension, isnull(ValuePension,0), voluntaryContributionPensionValue, voluntaryContributionPensionValuePatron, ISNULL(TotalPensionContribution,0), ISNULL(PensionSolidarityFundValueContribution,0),
		PensionSolidarityFundValueContributionSubSistence, ValueNotRetainedByVoluntaryContributions, rateContributionsHealth, ISNULL(ValueHealth,0), valueAditionalUPC, ISNULL(authorizationNumberDisability,''),
		valueGeneralDisability, ISNULL(authorizationNumberMaternityLicense,''), valueMaternityLicense, rateContributionProfessionalRisk, WorkCenter, ISNULL(ValueContributionProfessionalRisk,0), rateContributorCCF,
		ISNULL(ValueContributionCCF,0), rateContributorSENA, ISNULL(ValueSena,0), rateContributionICBF, ISNULL(ValueICBF,0), rateContributorESAP, rateContributorEducationMinistry, MinistryCodeRiskFound, ProfessionalRiskCode,
		TarifaEspecialPensiones, IngressDate, DateRetirement, SanctionInitialDate, SanctionEndDate, AmbulatoryDisabilityInitialDate, AmbulatoryDisabilityEndDate, 
		MaternityLeaveInitialDate, MaternityLeaveEndDate, VacationInitialDate, VacationEndDate, FechaInicioVCT, FechaFinVCT, FechaInicioIRL, FechaFinIRL, IBCOtrosParafiscales, ISNULL(TotalHours,0), FechaEmpleadoExterior
		, EconomicActivityARL, IdContract,LicenceDays,NoveltyType,EmployeeIngress,LicenceInitialDate,LicenceEndDate,LRM
		FROM @TmpAutoliquidation ORDER BY retirement, IdEmployee
		
		

		SELECT '0' AS CodeMessage, 'Proceso Finalizado Correctamente ' AS Message, cast(1 as tinyint) as [Status] 
		
		
	END TRY
	BEGIN CATCH

		SELECT '999'  AS CodeMessage , ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(5)) AS Message, cast(3 as tinyint) as [Status] 
		
	END CATCH
END