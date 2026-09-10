

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
			WHEN VAF.IGE ='X' OR TRY_CAST(VAF.IRL AS INT) > 0 OR VAF.LMA ='X'  THEN 'IN'
			WHEN VAF.Retirement= 'X'THEN 'EX'
			ELSE 'SA'
    END AS ChangeType,
	CASE
        WHEN  TRY_CAST(VAF.IRL AS INT) > 0 THEN 'INS'
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
		WHEN TRY_CAST(VAF.IRL AS INT) > 0 THEN VAF.FechaInicioIRL
		WHEN VAF.LicenceDays > 0 THEN VAF.LicenceInitialDate
		WHEN VAF.Retirement = 'X' THEN C.RetirementDate
		ELSE vaf.PayrollDateLiquidated
	END AS InitialDate,
	Case
		WHEN VAF.LMA ='X'  THEN VAF.MaternityLeaveEndDate
		WHEN VAF.SLN ='X' THEN VAF.SanctionEndDate
		WHEN VAF.IGE = 'X' THEN VAF.AmbulatoryDisabiltyEndDate
		WHEN TRY_CAST(VAF.IRL AS INT) > 0 THEN VAF.FechaFinIRL
		WHEN VAF.LicenceDays > 0 THEN VAF.LicenceEndDate
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
LEFT JOIN Common.City AS ct ON P.IdentificacionCityId = ct.Id
LEFT JOIN Common.Department AS d ON ct.DepartamentId = d.Id
LEFT JOIN Common.Country AS co ON d.CountryId = co.Id
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
	VAF.LicenceInitialDate,
	VAF.LicenceEndDate,
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de verificación del archivo de autoliquidación de nómina (PILA/seguridad social) que consolida, por empleado y período de liquidación, todos los datos necesarios para revisar y validar el archivo de aportes antes de su envío. Integra información del registro de verificación de autoliquidación con los datos personales del empleado (nombre, tipo y número de documento, género, fecha de nacimiento, estado civil, teléfono, correo), datos del contrato y cargo, nacionalidad, y todos los conceptos de liquidación: días y bases de cotización a pensión, salud, riesgos laborales (ARL) y caja de compensación (CCF), novedades como ingreso, retiro, incapacidad general (IGE), accidente de trabajo o enfermedad laboral (IRL), licencia de maternidad (LMA), sanción (SLN) y vacaciones (VAC), tarifas y valores aportados, así como el tipo y código de novedad (cambio) calculado dinámicamente según el estado del empleado en el período. Se utiliza para auditoría, conciliación y generación de reportes de seguridad social de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewVerifyAutoliquidationFile';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'VIEW', @level1name = N'ViewVerifyAutoliquidationFile';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida, por empleado y período liquidado, los datos requeridos para verificar el archivo de autoliquidación (PILA): identidad, contrato, novedades (ingreso/retiro/incapacidad/maternidad/sanción/vacaciones/IRL), bases, IBC, tarifas y contacto.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe existir en Payroll.Employee y estar vinculado a un ThirdParty y a una Person.; La persona debe tener un IdentificationTypeId válido en DBO.ADTIPOIDENTIFICA.; La persona debe tener IdentificacionCityId resoluble hasta país (City→Department→Country son INNER JOIN).; El contrato (ContractId) y su Position son opcionales (LEFT JOIN); pueden ser nulos.; Phone y Email son opcionales; si existen varios, se prioriza el de mayor Id.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila corresponde a un registro de VerifyAutoliquidationFile con su empleado, tercero y persona obligatoriamente asociados (JOIN inner).; El tipo de identificación se obtiene de la sigla en ADTIPOIDENTIFICA (no se usa el CASE numérico comentado).; Solo se devuelve el teléfono y el email más recientes por persona (ROW_NUMBER ORDER BY Id DESC, RowNum=1).; FechaInicioVSP y ValueVSP siempre se exponen como NULL (no están implementados).; ChangeType y ChangeCode son mutuamente excluyentes y siguen una jerarquía fija de prioridades definida en los CASE.; La nacionalidad se deriva de la cadena Persona→Ciudad→Departamento→País usando StandardCode.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autoliquidación PILA; Aportes seguridad social (pensión, salud, riesgos, CCF, SENA, ICBF); Novedades de nómina (ingreso, retiro, incapacidad, maternidad, sanción, licencia); IBC (Ingreso Base de Cotización); Incapacidad general (IGE); Licencia de maternidad (LMA); Riesgo laboral (IRL); Sanción (SLN); Vacaciones (VAC); Variación salarial permanente/transitoria (VSP/VST); Pensionado; Tipo de identificación; Contrato laboral; Cargo (Position); Nacionalidad / código país', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ViewVerifyAutoliquidationFile: Devuelve un set tabular agrupado (GROUP BY de todas las columnas) con los datos del archivo de autoliquidación enriquecidos con datos de empleado, persona, contrato, cargo, nacionalidad, teléfono y email más recientes.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si VAF.EmployeeIngressNovelty = ''X'' → ChangeType=''IC'' (ingreso) e InitialDate = IngressDate; si VAF.NoveltyContract = 2 → ChangeType=''OC'' e InitialDate = ContractInitialDate; si VAF.NoveltyContract = 1 → ChangeType=''SA'' e InitialDate = ContractInitialDate; si VAF.SLN = ''X'' OR VAF.LicenceDays > 0 → ChangeType=''PE'' (permiso/licencia); si VAF.IGE=''X'' OR TRY_CAST(IRL AS INT) > 0 OR VAF.LMA=''X'' → ChangeType=''IN'' (incapacidad); si VAF.Retirement=''X'' → ChangeType=''EX'' (egreso) y FinalDate = C.RetirementDate else ChangeType=''SA'' (Sin Alteración); si TRY_CAST(IRL AS INT) > 0 → ChangeCode=''INS'' (riesgo laboral); si VAF.IGE=''X'' OR (VAF.LMA=''X'' AND P.Gender = 1) → ChangeCode=''SEM'' (enfermedad general; aplica LMA solo si género=1); si VAF.LMA=''X'' → ChangeCode=''MAT'' (maternidad); si VAF.LicenceDays > 0 → ChangeCode=''C'' (licencia); si VAF.SLN=''X'' → ChangeCode=''S'' (sanción) else ChangeCode=''''; si E.Pensionary = 1 → Se etiqueta como ''Si'' pensionado else ''No'' si E.Pensionary = 0', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VerifyAutoliquidationFile; Payroll.Employee; Common.ThirdParty; Common.Person; DBO.ADTIPOIDENTIFICA; Payroll.Contract; Payroll.Position; Common.City; Common.Department; Common.Country; Common.Phone; Common.Email', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'VIEW', @level1name=N'ViewVerifyAutoliquidationFile';
GO
