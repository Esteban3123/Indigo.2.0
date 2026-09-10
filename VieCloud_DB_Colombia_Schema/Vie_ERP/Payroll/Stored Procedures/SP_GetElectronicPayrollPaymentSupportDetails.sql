
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
		  -- BUG-39883: el concepto de vacaciones (clase '030') gobierna todo el VacationDetail.
		  -- Si no esta clasificado para nomina electronica, no viaja ningun concepto de la vacacion.
		  AND EXISTS
		  (
			SELECT 1
			FROM Payroll.VacationDetail vdr                       WITH (NOLOCK)
			JOIN Payroll.Concept cr                               WITH (NOLOCK) ON cr.Id   = vdr.IdConcept
			JOIN Payroll.ElectronicPayrollConcepts epcr           WITH (NOLOCK) ON epcr.Id = cr.IdElectronicPayrollConcepts
			WHERE vdr.IdVacation   = v.Id
			  AND cr.ConceptClass  = '030'
			  AND epcr.ConceptType IN (1, 2)
			  AND epcr.State       = 1
		  )
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle completo de los conceptos de pago asociados a un soporte de nómina electrónica específico, identificado por su ID. Combina dos fuentes: los rubros generales (devengados y deducciones) registrados en el detalle del comprobante electrónico, y las liquidaciones de vacaciones de pago inmediato confirmadas del empleado para el mismo año y mes del soporte. Retorna por cada línea la naturaleza del concepto (devengado o deducción), tipo, subtipo, descripción, fechas, cantidad de días, porcentaje y valor, permitiendo reconstruir el desglose completo del comprobante DIAN de nómina electrónica para un trabajador en un período dado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle agregado (por concepto, fechas y porcentaje) de un soporte de pago de nómina electrónica, filtrando conceptos con valor positivo y reportando estado de éxito o error de la consulta.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el soporte de pago electrónico identificado, con detalles asociados en Payroll.ElectronicPayrollPaymentSupportDetail.; La vista Payroll.ViewElectronicPayrollPaymentSupportDetail debe poder enlazarse por (EntityId, EntityName) con los registros del detalle.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen detalles cuyo Value sea estrictamente mayor que 0 (se excluyen conceptos con valor 0 o negativo).; Las fechas DateStart/DateEnd se sanean: si no son fechas válidas se devuelven como NULL.; Quantity y Value se entregan agregados (SUM) por la combinación Nature, Type, Subtype, ConceptClass, Detail, DateStart, DateEnd, Percentage.; En caso de error, el resultado nunca contiene filas de datos: solo una fila de estado de error.; Las lecturas se realizan con NOLOCK, por lo que el resultado puede incluir lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina electrónica; Soporte de pago de nómina; Detalle de soporte de pago; Conceptos de nómina (devengados/deducciones); Naturaleza/Tipo/Subtipo de concepto', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @Table_Result: Cuando la consulta es exitosa, retorna filas con StateResult=1, MessageResult=''OK'' y los conceptos agregados (SUM(Quantity), SUM(Value)) cuyo v.Value > 0, vinculados al ElectronicPayrollPaymentSupportId recibido.; [RETURN_RESULT] @Table_Result: Cuando ocurre una excepción, retorna una única fila con StateResult=0 y MessageResult formateado como ''Se presento un error obteniendo informacion del soporte de pago: <ERROR_MESSAGE> - Linea: <ERROR_LINE>''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISDATE(CAST(v.DateStart AS VARCHAR(20))) = 1 → Se conserva el valor de DateStart else Se devuelve NULL en DateStart; si ISDATE(CAST(v.DateEnd AS VARCHAR(20))) = 1 → Se conserva el valor de DateEnd else Se devuelve NULL en DateEnd; si Ocurre excepción durante la consulta (BEGIN CATCH) → Se vacía el resultado y se devuelve una única fila con StateResult=0 y MessageResult con ERROR_MESSAGE() y ERROR_LINE() else Se devuelven las filas agrupadas con StateResult=1 y MessageResult=''OK''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ElectronicPayrollPaymentSupportDetail; Payroll.ViewElectronicPayrollPaymentSupportDetail', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_GetElectronicPayrollPaymentSupportDetails';
-- GO
