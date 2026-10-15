-- =============================================
-- Author:		Carlos Jhefersson Muñoz
-- Create date: 07/03/2017
-- Description:	Procedimiento Almacenado para Certificado de Ingresos Y Retenciones
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ReportIncomeAndWithholding]
	@Year INT,
	@EmployeeId INT
AS 
BEGIN	
/* -------------------- DECLARACION DE VARIABLES -------------------- */	
	--DECLARE @InitialDate date = '01-01-' + cast(@Year AS varchar(4))
	--DECLARE @EndDate date = '31-12-' + cast(@Year AS varchar(4))
	 DECLARE @InitialDate date = CAST(CAST(@Year AS varchar(4)) + '-01-01' AS date)
	 DECLARE @EndDate date = CAST(CAST(@Year AS varchar(4)) + '-12-31' AS date)
 

	-- Creamos la tabla temporal donde almacenaremos los conceptos por casillas
	DECLARE @tableConceptsByBox TABLE (
		ConceptClass CHAR(3) NOT NULL,
		box INT NOT NULL
	)

	--Insertamos los conceptos correspondientes a las casillas
	INSERT INTO @tableConceptsByBox (ConceptClass, box)
		--Pagos por salarios
			  SELECT '001', 37		-- Horas Extras
		UNION SELECT '004', 37		-- Bonificación por Servicios
		UNION SELECT '005', 37		-- Sueldo
		UNION SELECT '006', 37		-- Auxilio de Transporte
		UNION SELECT '007', 37		-- Indemnizaciones	
		UNION SELECT '010', 37		-- Otros Devengados *Ojo, si es parte de otro renglon asignar los conceptos acordes
		UNION SELECT '012', 37		-- Horas Extras Nocturnas
		UNION SELECT '013', 37		-- Horas Extras Dominicales
		UNION SELECT '021', 37		-- Incapacidad Ambulatoria
		UNION SELECT '022', 37		-- Incapacidad Hospitalaria
		UNION SELECT '023', 37		-- Maternidad
		UNION SELECT '027', 37		-- Incapacidad Riesgos Profesionales
		UNION SELECT '042', 37		-- Recargo Nocturno Normal
		UNION SELECT '043', 37		-- Recargo Nocturno Festivo
		UNION SELECT '046', 37		-- Bonificacion x Año de Servicio
		--Pagos por honorarios
		UNION SELECT '000', 38		-- Honorarios
		--Pagos por servicios
		UNION SELECT '000', 39		-- Servicios
		--Pagos por comisiones
		UNION SELECT '000', 40		-- Comisiones
		--Pagos por prestaciones sociales
		UNION SELECT '002', 41		-- Primas de Servicios
		UNION SELECT '003', 41		-- Otras Primas
		UNION SELECT '030', 41		-- Vacaciones
		UNION SELECT '049', 41		-- Prima de Vacaciones
		--Pagos por viaticos
		UNION SELECT '054', 42		-- Viaticos
		--Pagos por gastos de representación
		UNION SELECT '047', 43		-- Gastos de representación
		--Pagos por compensaciones por el trabajo asociado cooperativo
		UNION SELECT '000', 44		-- compensaciones por el trabajo asociado cooperativo
		--Otros pagos
		UNION SELECT '065', 45		--Otros pagos
		--Cesantías e intereses de cesantías efectivamente pagadas, consignadas o reconocidas en el periodo
		UNION SELECT '000', 46		-- Cesantías e intereses
		--Pensiones de jubilación, vejez o invalidez
		UNION SELECT '000', 47		--Pensiones de jubilación, vejez o invalidez
		--Aportes obligatorios por salud
		UNION SELECT '017', 49		-- Salud Empleado
		--Aportes obligatorios a fondos de pensiones
		UNION SELECT '014', 50		-- Pensión Empleado
		UNION SELECT '038', 50		-- Pensión Empleado
		--Aportes obligatorios a fondos de pensiones
		UNION SELECT '016', 51		-- Pensión Voluntaria
		--Aportes obligatorios a fondos de pensiones
		UNION SELECT '045', 52		-- Cuentas AFC
		--Valor de la retención en la fuente por rentas de trabajo y pensiones
		UNION SELECT '020', 53		-- Retencion
		--Campo nuevo de cesantias consignadas al fondo de cesanntias
		UNION SELECT '000', 70 --cesantias ya pagadas al fondo
		--campo 59 " 
		UNION SELECT '000', 71 

		

/* ------------------------------- CONSULTAS ------------------------------ */
	
	SELECT *
	FROM 
	(
		SELECT 
    ct1.Nit AS NitCompany,
    ct1.DigitVerification AS DigitVerification,
    ct1.[Name] AS NameCompany,
    cp.IdentificationType AS IdentificationType,
    l.EmployeeId,
    ct.Nit AS NitEmployee,
    cp.FirstLastName,
    cp.SecondLastName,
    cp.FirstName,
    cp.SecondName,
    @InitialDate AS PeriodInitial,
    @EndDate AS PeriodEnd,
    cd.Code AS Department,
    cc.Code AS City,
    cc.[Name] AS CityName,
    R.Name AS RelationshipName,
    R.IdentificationNumber AS RelationshipNumber,
    K.Name AS KinshipName,
    CASE 
        WHEN R.IdentificationType = 0 THEN 'CC'
        WHEN R.IdentificationType = 1 THEN 'CE'
        WHEN R.IdentificationType = 2 THEN 'TI'
        WHEN R.IdentificationType = 3 THEN 'RC'
        WHEN R.IdentificationType = 4 THEN 'PA'
        WHEN R.IdentificationType = 5 THEN 'AS'
        WHEN R.IdentificationType = 6 THEN 'MS'
        WHEN R.IdentificationType = 7 THEN 'NI'
        WHEN R.IdentificationType = 8 THEN 'NU'
        WHEN R.IdentificationType = 9 THEN 'CN'
        WHEN R.IdentificationType = 10 THEN 'CD'
        WHEN R.IdentificationType = 11 THEN 'SC'
        WHEN R.IdentificationType = 12 THEN 'PE'
        WHEN R.IdentificationType = 13 THEN 'PT'
        WHEN R.IdentificationType = 14 THEN 'DE'
        WHEN R.IdentificationType = 15 THEN 'SI'
        ELSE '' 
    END AS IdentificationTypeRelationship
	FROM Payroll.Liquidation AS l WITH (NOLOCK)
	INNER JOIN Payroll.[Group] AS pg WITH (NOLOCK) ON pg.Id = l.GroupId
	INNER JOIN Payroll.Company AS pcy WITH (NOLOCK) ON pcy.Id = pg.CompanyId
	INNER JOIN Common.ThirdParty AS ct1 WITH (NOLOCK) ON ct1.Id = pcy.ThirdPartyId
	INNER JOIN Common.City AS cc WITH (NOLOCK) ON cc.Id = pcy.CityId
	INNER JOIN Common.Department AS cd WITH (NOLOCK) ON cd.Id = cc.DepartamentId
	INNER JOIN Payroll.Employee AS pe WITH (NOLOCK) ON pe.Id = l.EmployeeId
	INNER JOIN Common.ThirdParty AS ct WITH (NOLOCK) ON ct.Id = pe.ThirdPartyId
	INNER JOIN Common.Person AS cp WITH (NOLOCK) ON cp.Id = ct.PersonId
	LEFT JOIN Payroll.Relationship R ON pe.id = R.EmployeeId AND r.Dependent = 1
	LEFT JOIN Payroll.Kinship K ON R.KinshipId = K.id
	WHERE YEAR(l.PayrollDateLiquidated) = @Year 
		AND l.EmployeeId = @EmployeeId  
		AND l.RegisterStatus = 'C'
	GROUP BY 
		ct1.Nit,
		ct1.DigitVerification,
		ct1.[Name],
		cp.IdentificationType,
		l.EmployeeId,
		ct.Nit,
		cp.FirstLastName,
		cp.SecondLastName,
		cp.FirstName,
		cp.SecondName,
		cd.Code,
		cc.Code,
		cc.[Name],
		R.Name,
		R.IdentificationNumber,
		K.Name,
		R.IdentificationType		
	) e
	JOIN
	(
		SELECT @EmployeeId AS EmployeeId, *
		FROM
		(
			--Pagos segun conceptos
			SELECT t.box, ISNULL(SUM(ld.ConceptTotalValue), 0) AS Value
			FROM Payroll.Liquidation AS l with(nolock)
			JOIN Payroll.LiquidationDetail AS ld with(nolock) ON l.Id = ld.PayrollId
				AND YEAR(l.PayrollDateLiquidated) = @Year
				AND l.EmployeeId = @EmployeeId
				AND l.RegisterStatus = 'C'
			JOIN Payroll.Concept AS pc with(nolock) ON pc.Id = ld.ConceptId
			RIGHT JOIN @tableConceptsByBox t ON pc.ConceptClass = t.ConceptClass			
			GROUP BY l.EmployeeId, t.box

			UNION ALL

			--Pagos retroactivos segun conceptos
			SELECT t.box, ISNULL(SUM(rd.ValueConceptWithRetroactive), 0) AS Value
			FROM Payroll.RetroactiveC AS r with(nolock)
			JOIN Payroll.RetroactiveD AS rd with(nolock) ON r.Id = rd.IdRetroactiveC
				AND YEAR(r.InitialDateRetroactive) = @Year
				AND r.IdEmployee = @EmployeeId
				AND r.Status = 2
			JOIN Payroll.Concept AS pc with(nolock) ON pc.Id = rd.IdConcept
			RIGHT JOIN @tableConceptsByBox t ON pc.ConceptClass = t.ConceptClass			
			GROUP BY r.IdEmployee, t.box

			UNION ALL

			--Pagos por prestaciones sociales (PRIMAS)
			SELECT 41, SUM(ip.TotalAccrued) Value
			FROM Payroll.IncentivePayment ip with(nolock)
			JOIN Payroll.Contract c with(nolock) ON ip.ContractId = c.Id
			WHERE YEAR(ip.PeriodEndDate) = @Year
				AND c.EmployeeId = @EmployeeId
				AND RegisterStatus = 2
			GROUP BY c.EmployeeId

			UNION ALL

			--Cesantías e intereses de cesantías efectivamente pagadas, consignadas o reconocidas en el periodo
			SELECT 46,
				CASE 
					WHEN @Year = 2022 THEN
						ISNULL((SELECT [Payroll].[GetUnemployedLiquidation](@Year, @EmployeeId)), 0)
					ELSE
						ISNULL(
							(SELECT SUM(ul.TotalUnemployed + ul.UnemployedInterestTotal)
							 FROM Payroll.UnemployedLiquidation ul with(nolock)	
							 WHERE ul.Year = @Year
								 AND ul.EmployeeId = @EmployeeId
								 AND ul.Status = 1), 0)
				END AS Result

			UNION ALL
			Select 70,
					CASE
						WHEN @YEAR = 2022 THEN
							ISNULL(
							(SELECT SUM(ul.TotalUnemployed + ul.UnemployedInterestTotal)
							 FROM Payroll.UnemployedLiquidation ul with(nolock)	
							 WHERE ul.Year = @Year -1
								 AND ul.EmployeeId = @EmployeeId
								 AND ul.Status = 1), 0)
						ELSE 0
				END AS VALUE
			Union ALL
			--campo 59 "Ingreso laboral promedio de los últimos seis meses anteriores (numeral 4 art. 206 E.T.)
			SELECT 71 ,
					CASE
						WHEN @Year = 2023 THEN
							ISNULL((SELECT [Payroll].[AverageLaborIncomeSixMonths](@Year, @EmployeeId)),0)
					END AS VALUE
						
			Union ALL
			--Valor de la retención en la fuente por rentas de trabajo y pensiones (PRIMAS)
			SELECT 53, SUM(ip.RetentionValue) Value
			FROM Payroll.IncentivePayment ip with(nolock)
			JOIN Payroll.Contract c with(nolock) ON ip.ContractId = c.Id
			WHERE YEAR(ip.PeriodEndDate) = @Year
				AND c.EmployeeId = @EmployeeId
				AND RegisterStatus = 2
			GROUP BY c.EmployeeId
		) b	
		PIVOT
		(
			SUM([value])
			FOR [box] IN ([37], [38], [39], [40], [41], [42], [43], [44], [45], [46], [47], [49], [50], [51], [52], [53],[70],[71])
		) piv
	) b ON e.EmployeeId = b.EmployeeId

END