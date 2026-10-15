
CREATE PROCEDURE [Payroll].[SP_GetElectronicPayrollPaymentSupportDetails]
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

	BEGIN TRY
		INSERT INTO @Table_Result
		SELECT	1, 'OK',
				v.Nature,
				CASE v.ConceptClass
				WHEN '005' THEN 1
				WHEN '006' THEN 2
				WHEN '054' THEN 4
				WHEN '001' THEN 5
				WHEN '012' THEN 6
				WHEN '042' THEN 7
				WHEN '013' THEN 8
				WHEN '051' THEN 9
				WHEN '052' THEN 9
				WHEN '050' THEN 10
				WHEN '043' THEN 11
				WHEN '030' THEN 13 --provision de vacaciones [Tipo Compensadas]
				WHEN '031' THEN 13 --provision de vacaciones [Tipo Compensadas]
				WHEN '002' THEN 14
				WHEN '003' THEN 14
				WHEN '033' THEN 14 -- Provisión Primas
				WHEN '008' THEN 15 -- Provisión Cesantías
				WHEN '034' THEN 16 --Se agrega la clase de concepto 'Intereses de Cesantías' y se deja de tipo Intereses de Cesantías
				WHEN '021' THEN 17
				WHEN '022' THEN 17
				WHEN '027' THEN 17
				WHEN '023' THEN 18
				WHEN '024' THEN 19
				WHEN '028' THEN 19
				WHEN '025' THEN 20
				WHEN '004' THEN 21
				WHEN '046' THEN 21
				WHEN '049' THEN 21 --Se agrega la clase de concepto 'Prima de Vacaciones' y se deja de tipo Bonificación Salarial
				WHEN '055' THEN 21
				WHEN '064' THEN 21 --Se agrega la clase de concepto 'Incremento de Vacaciones' y se deja de tipo Bonificación Salarial
				WHEN '063' THEN 22 --Se agrega la clase de concepto 'Bonificación Especial para la Recreación' y se deja de tipo Bonificación Salarial
				WHEN '047' THEN 23
				WHEN '010' THEN 26
				WHEN '066' THEN 34 --Apoyo sostenimiento (contratos de aprendizaje)
				WHEN '007' THEN 37
				WHEN '017' THEN 39
				WHEN '014' THEN 40
				WHEN '038' THEN 41
				WHEN '044' THEN 43
				WHEN '026' THEN 44
				WHEN '041' THEN 45
				WHEN '011' THEN 48
				WHEN '019' THEN 48			
				WHEN '016' THEN 49
				WHEN '020' THEN 50
				WHEN '048' THEN 50
				WHEN '045' THEN 51
				WHEN '053' THEN 53
				WHEN '065' THEN 22 -- PRUEBA BONO NS
			END Type,
			CASE v.ConceptClass
				WHEN '021' THEN 1
				WHEN '022' THEN 1
				WHEN '027' THEN 3
				ELSE 0
			END Subtype,
			v.Detail,
			v.DateStart,
			v.DateEnd,
			v.Quantity,
			v.Percentage,
			v.Value
		FROM
		(
			SELECT	v.Nature, v.ConceptClass, v.Detail, 
					IIF(ISDATE(CAST(v.DateStart AS VARCHAR(20))) = 1, v.DateStart, NULL) DateStart, 
					IIF(ISDATE(CAST(v.DateEnd AS VARCHAR(20))) = 1, v.DateEnd, NULL) DateEnd,
					SUM(v.Quantity) Quantity, v.Percentage, SUM(v.Value) Value
			FROM Payroll.ElectronicPayrollPaymentSupportDetail eppsd WITH (NOLOCK)
			JOIN [Payroll].[ViewElectronicPayrollPaymentSupportDetail] v WITH (NOLOCK) ON eppsd.EntityId = v.EntityId AND eppsd.EntityName = v.EntityName
			WHERE @ElectronicPayrollPaymentSupportId = eppsd.ElectronicPayrollPaymentSupportId
				AND v.Value > 0
			GROUP BY v.Nature, v.ConceptClass, v.Detail, v.DateStart, v.DateEnd, v.Percentage
		) v
	END TRY
	BEGIN CATCH	
		DELETE FROM @Table_Result

		INSERT INTO @Table_Result (StateResult, MessageResult)
			SELECT 0, CONCAT('Se presento un error obteniendo informacion del soporte de pago: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE())
	END CATCH

	SELECT * FROM @Table_Result
END