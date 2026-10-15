
CREATE  PROCEDURE [Payroll].[SP_GetElectronicPayrollPaymentSupportDetails]
	@ElectronicPayrollPaymentSupportId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Table_Result AS TABLE
	(
		StateResult BIT,
		MessageResult VARCHAR(500),
		-------------------------- DATOS DEL DETALLE --------------------------
		Nature TINYINT,
		Type INT,
		Subtype TINYINT,
		Detail VARCHAR(500),
		DateStart DATETIME,
		DateEnd DATETIME,
		Quantity INT,
		Percentage DECIMAL(5,2),
		Value DECIMAL(18,0)
	)

	-- Obtener empleado y periodo del soporte para consultar vacaciones
	DECLARE @ThirdPartyId INT, @Year INT, @Month TINYINT

	SELECT	@ThirdPartyId = epps.EmployeePartyId,
			@Year         = epps.Year,
			@Month        = epps.Month
	FROM Payroll.ElectronicPayrollPaymentSupport epps WITH (NOLOCK)
	WHERE epps.Id = @ElectronicPayrollPaymentSupportId

	BEGIN TRY

		-- =====================================================================
		-- INSERT 1: Liquidación regular, Incentivos, Liquidación de Contrato
		-- =====================================================================
		INSERT INTO @Table_Result
		SELECT	1, 'OK',
				v.Nature,
				v.Type,
				v.Subtype,
				v.Detail,
				v.DateStart,
				v.DateEnd,
				v.Quantity,
				v.Percentage,
				v.Value
		FROM
		(
			SELECT	v.Nature,v.Type, v.Subtype, v.ConceptClass, v.Detail,
					IIF(ISDATE(CAST(v.DateStart AS VARCHAR(20))) = 1, v.DateStart, NULL) DateStart,
					IIF(ISDATE(CAST(v.DateEnd AS VARCHAR(20))) = 1, v.DateEnd, NULL) DateEnd,
					SUM(v.Quantity) Quantity, v.Percentage, SUM(v.Value) Value
			FROM Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK)
			JOIN [Payroll].[ViewElectronicPayrollPaymentSupportDetail] v WITH (NOLOCK) ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
			WHERE @ElectronicPayrollPaymentSupportId = eppsd.ElectronicPayrollPaymentSupportId
				AND v.Value > 0
			GROUP BY v.Nature,v.Type, v.Subtype, v.ConceptClass, v.Detail, v.DateStart, v.DateEnd, v.Percentage
		) v

		-- =====================================================================
		-- INSERT 2: Vacaciones pago inmediato (TypePayment = 1, State = 2)
		-- =====================================================================
		INSERT INTO @Table_Result
		SELECT	1, 'OK',
				epc.ConceptType,
				epc.InternalCode,
				ISNULL(epcs.InternalSubCode, 0),
				c.Name,
				IIF(c.ConceptClass = '030', v.VacationStartDate, NULL),
				IIF(c.ConceptClass = '030', v.VacationEndDate,   NULL),
				IIF(c.ConceptClass = '030', SUM(v.EnjoyDays), 0),
				0,
				SUM(IIF(epc.ConceptType = 1, vd.Accrued, vd.Deducted))
		FROM Payroll.Vacation v                                         WITH (NOLOCK)
		JOIN Payroll.VacationPeriod vp                                  WITH (NOLOCK) ON vp.Id = v.VacationPeriodId
		JOIN Payroll.Employee e                                         WITH (NOLOCK) ON e.Id  = vp.EmployeeId
		JOIN Payroll.VacationDetail vd                                  WITH (NOLOCK) ON vd.IdVacation = v.Id
		JOIN Payroll.Concept c                                          WITH (NOLOCK) ON c.Id  = vd.IdConcept
		INNER JOIN Payroll.ElectronicPayrollConcepts epc                WITH (NOLOCK) ON epc.Id = c.IdElectronicPayrollConcepts
		LEFT  JOIN Payroll.ElectronicPayrollConceptSubtype epcs         WITH (NOLOCK) ON epcs.Id = c.IdElectronicPayrollConceptSubtype
		WHERE e.ThirdPartyId             = @ThirdPartyId
		  AND YEAR(v.VacationStartDate)   = @Year
		  AND MONTH(v.VacationStartDate) = @Month
		  AND v.TypePayment              = 1		-- Solo pago inmediato
		  AND v.State                    = 2		-- Solo confirmadas/pagadas
		  AND vd.IdConcept               IS NOT NULL
		  AND epc.ConceptType            IN (1, 2)
		  AND epc.State                  = 1
		  AND IIF(epc.ConceptType = 1, vd.Accrued, vd.Deducted) > 0
		GROUP BY epc.ConceptType, epc.InternalCode, epcs.InternalSubCode,
				 c.Name, c.ConceptClass,
				 IIF(c.ConceptClass = '030', v.VacationStartDate, NULL),
				 IIF(c.ConceptClass = '030', v.VacationEndDate,   NULL)

	END TRY
	BEGIN CATCH
		DELETE FROM @Table_Result

		INSERT INTO @Table_Result (StateResult, MessageResult)
			SELECT 0, CONCAT('Se presento un error obteniendo informacion del soporte de pago: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	SELECT * FROM @Table_Result
END