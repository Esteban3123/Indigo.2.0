-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 04/12/2023
---Update author: Oscar stiven astudillo
-- Update date: 17/03/2026
-- Description:	Store Procedure que valida y procesa datos de aumento de salarios desde archivo 
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SetIncreaseSalaryFromFile]
	@XmlObject Xml
AS 
BEGIN
	SET NOCOUNT ON;

		-- TABLA DEL XML CON NUEVA ESTRUCTURA
		DECLARE @TableXmlObject TABLE
		(
			NitEmployee VARCHAR(20),
			InitialDate DATETIME,
			ModificationReasonCode VARCHAR(20),
			PercentageIncrease DECIMAL(8,4),
			AproximationValue INT,
			BasicSalary DECIMAL(18, 3),
			ValueIncrease DECIMAL(18, 3),
			WithRetroactive INT,                -- 1 = Si, 0 = No
			RetroactiveInitialDate DATETIME,
			PayrollPaidRetroactive INT         -- 0 = Pago independiente, 1 = Con nómina
		)
		
		-- TABLA DE RETORNO CON CAMPOS ADICIONALES
		DECLARE @ReturnTable TABLE
		(
			Id int IDENTITY PRIMARY KEY, 
			StatusField varchar(3), 
			MessageField varchar(max), 
			ContractId INT, 
			InitialContractNumber INT, 
			NitEmployee VARCHAR(100), 
			EmployeeName VARCHAR(300), 
			JobBondingDate DATE, 
			BasicSalary NUMERIC(18,3), 
			ValueIncrease NUMERIC(18,3), 
			NewSalary NUMERIC(18,3), 
			PercentageIncrease DECIMAL(8,4),
			PositionCode VARCHAR(20), 
			PositionName VARCHAR(200), 
			FunctionalUnitCode VARCHAR(20), 
			FunctionalUnitName VARCHAR(100),
			InitialDate DATETIME,
			ModificationReasonId INT,
			ModificationReasonCode VARCHAR(20),        
			ModificationReasonName VARCHAR(200),      
			AproximationValue INT,
			WithRetroactive INT,
			RetroactiveInitialDate DATETIME,
			PayrollPaidRetroactive INT,
			GroupId INT,           
			EmployeeId INT
		)

		BEGIN TRY

			-- PARSEAR XML CON NUEVA ESTRUCTURA
			INSERT INTO @TableXmlObject
			(
				NitEmployee, 
				InitialDate, 
				ModificationReasonCode, 
				PercentageIncrease,
				AproximationValue,
				BasicSalary, 
				ValueIncrease,
				WithRetroactive,
				RetroactiveInitialDate,
				PayrollPaidRetroactive
			)
			SELECT	
				t.x.value('NitEmployee[1]','varchar(20)') as NitEmployee,
				TRY_PARSE (t.x.value('InitialDate[1]','varchar(500)') AS DATETIME) as InitialDate,
				t.x.value('ModificationReasonCode[1]','varchar(20)') as ModificationReasonCode,
				TRY_CONVERT(DECIMAL(8,4),  t.x.value('PercentageIncrease[1]','varchar(50)')) as PercentageIncrease,
				ISNULL(t.x.value('AproximationValue[1]','INT'), 0) as AproximationValue,
				TRY_CONVERT(DECIMAL(18,3), t.x.value('BasicSalary[1]','varchar(50)'))        as BasicSalary,
				TRY_CONVERT(DECIMAL(18,3), t.x.value('ValueIncrease[1]','varchar(50)'))      as ValueIncrease,
				ISNULL(t.x.value('WithRetroactive[1]','INT'), 0) as WithRetroactive,
				TRY_PARSE (t.x.value('RetroactiveInitialDate[1]','varchar(500)') AS DATETIME) as RetroactiveInitialDate,
				ISNULL(t.x.value('PayrollPaidRetroactive[1]','INT'), 0) as PayrollPaidRetroactive
			FROM @XmlObject.nodes('/Data/Row') t(x);

			-- Se eliminan los registros del XML que tienen cédula repetida
			WITH CTE AS (
				SELECT
					NitEmployee,
					ROW_NUMBER() OVER (PARTITION BY NitEmployee ORDER BY (SELECT NULL)) AS RowNum
				FROM
					@TableXmlObject
			)
			DELETE FROM CTE WHERE RowNum > 1


			-- VALIDACIÓN Y PROCESAMIENTO DE LA INFORMACIÓN
			INSERT INTO @ReturnTable
			SELECT 
				'001' as StatusField, 
				'Proceso Realizado Correctamente' as MessageField, 
				C.Id as ContractId, 
				C.InitialContractNumber as InitialContractNumber, 
				TP.Nit as NitEmployee, 
				TP.Name as EmployeeName, 
				C.JobBondingDate as JobBondingDate, 
				C.BasicSalary as BasicSalary, 
				TXO.ValueIncrease as ValueIncrease, 
				-- CALCULO NUEVO SALARIO CON APROXIMACIÓN
				CASE 
				  WHEN TXO.AproximationValue = 0 THEN
					   CAST(C.BasicSalary AS NUMERIC(18,3)) + CAST(TXO.ValueIncrease AS NUMERIC(18,3))
				  WHEN TXO.AproximationValue = 1 THEN 
					   ROUND(CAST(C.BasicSalary AS NUMERIC(18,3)) + CAST(TXO.ValueIncrease AS NUMERIC(18,3)), -1)  -- A la décima
				  WHEN TXO.AproximationValue = 2 THEN 
					   ROUND(CAST(C.BasicSalary AS NUMERIC(18,3)) + CAST(TXO.ValueIncrease AS NUMERIC(18,3)), -2)  -- A la centésima
				  WHEN TXO.AproximationValue = 3 THEN 
					   ROUND(CAST(C.BasicSalary AS NUMERIC(18,3)) + CAST(TXO.ValueIncrease AS NUMERIC(18,3)), -3)  -- Al miles
				  ELSE
					   CAST(C.BasicSalary AS NUMERIC(18,3)) + CAST(TXO.ValueIncrease AS NUMERIC(18,3))
				END AS NewSalary,
				-- PORCENTAJE DE AUMENTO (puede venir del archivo o calcularse)
				ISNULL(TXO.PercentageIncrease, (TXO.ValueIncrease / C.BasicSalary) * 100) as PercentageIncrease, 
				P.Code as PositionCode, 
				P.Name as PositionName, 
				FU.Code as FunctionalUnitCode, 
				FU.Name as FunctionalUnitName, 
				TXO.InitialDate as InitialDate,
				CMR.Id as ModificationReasonId,
				CMR.Code as ModificationReasonCode,        
				CMR.Name as ModificationReasonName,        
				TXO.AproximationValue,
				TXO.WithRetroactive,
				TXO.RetroactiveInitialDate,
				TXO.PayrollPaidRetroactive,
				C.GroupId,         
				E.Id as EmployeeId
			FROM 
				@TableXmlObject TXO
				INNER JOIN Common.ThirdParty TP ON TP.Nit = TXO.NitEmployee
				INNER JOIN Payroll.Employee E ON E.ThirdPartyId = TP.Id
				INNER JOIN Payroll.Contract C ON C.EmployeeId = E.Id AND C.Valid = 1 AND C.[Status] = 1
				INNER JOIN Payroll.Position P ON P.Id = C.PositionId
				INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId
			INNER JOIN Payroll.ContractModificationReason CMR ON CMR.Code = TXO.ModificationReasonCode AND CMR.State = 1
			WHERE TXO.InitialDate <= C.ContractEndingDate
			  AND ISNULL(TXO.ValueIncrease, 0) > 0
			ORDER BY TP.Name

		
			-- VALIDANDO DATOS QUE NO EXISTEN O TIENEN ERRORES
			IF (SELECT COUNT(*) FROM @ReturnTable) <> (SELECT COUNT(*) FROM @TableXmlObject)
			BEGIN
				-- Declarar una tabla temporal para almacenar los datos no encontrados o con errores
				DECLARE @DatosConError TABLE (
					NitEmployee VARCHAR(20),
					ErrorMessage VARCHAR(MAX)
				)

			-- Empleado no encontrado en ThirdParty
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Empleado con cédula ' + TXO.NitEmployee + ' no encontrado'
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND NOT EXISTS (SELECT 1 FROM Common.ThirdParty TP WHERE TXO.NitEmployee = TP.Nit)

			-- Empleado sin registro en nómina
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Empleado con cédula ' + TXO.NitEmployee + ' no tiene registro en nómina'
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND NOT EXISTS (SELECT 1 FROM @DatosConError DCE WHERE TXO.NitEmployee = DCE.NitEmployee)
			  AND EXISTS     (SELECT 1 FROM Common.ThirdParty TP WHERE TXO.NitEmployee = TP.Nit)
			  AND NOT EXISTS (SELECT 1 FROM Payroll.Employee E
								INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
								WHERE TP.Nit = TXO.NitEmployee)

			-- Empleado sin contrato activo
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Empleado con cédula ' + TXO.NitEmployee + ' no tiene contrato activo'
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND NOT EXISTS (SELECT 1 FROM @DatosConError DCE WHERE TXO.NitEmployee = DCE.NitEmployee)
			  AND NOT EXISTS (SELECT 1 FROM Payroll.Contract C
								INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
								INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
								WHERE TP.Nit = TXO.NitEmployee AND C.Valid = 1 AND C.[Status] = 1)

			-- Código de razón de modificación requerido
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'El código de razón de modificación es requerido para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND TXO.ModificationReasonCode IS NULL

			-- Código de razón de modificación no válido
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Código de razón de modificación ' + TXO.ModificationReasonCode + ' no encontrado o inválido'
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND TXO.ModificationReasonCode IS NOT NULL
			  AND NOT EXISTS (SELECT 1 FROM Payroll.ContractModificationReason CMR
								WHERE CMR.Code = TXO.ModificationReasonCode AND CMR.State = 1)

			-- Fecha de inicio requerida
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'La fecha de inicio es requerida para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND TXO.InitialDate IS NULL

			-- Valor de incremento inválido
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'El incremento debe ser mayor a cero para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND (TXO.ValueIncrease IS NULL OR TXO.ValueIncrease <= 0)

			-- Fecha retroactiva requerida cuando WithRetroactive = 1
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Si el retroactivo está marcado como 1 (Si), debe ingresar la fecha de inicio retroactivo para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND TXO.WithRetroactive = 1 AND TXO.RetroactiveInitialDate IS NULL

			-- Fecha de inicio mayor a la fecha de terminación del contrato
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee,
				'La fecha de inicio (' + CONVERT(VARCHAR(10), TXO.InitialDate, 120) + ') del incremento es mayor a la fecha de terminación ('
				+ CONVERT(VARCHAR(10), (
					SELECT TOP 1 C2.ContractEndingDate
					FROM Payroll.Contract C2
					INNER JOIN Payroll.Employee E2 ON E2.Id = C2.EmployeeId
					INNER JOIN Common.ThirdParty TP2 ON TP2.Id = E2.ThirdPartyId
					WHERE TP2.Nit = TXO.NitEmployee AND C2.Valid = 1 AND C2.[Status] = 1
				), 120)
				+ ') del contrato para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND TXO.InitialDate IS NOT NULL
			  AND TXO.InitialDate > (SELECT TOP 1 C2.ContractEndingDate
									FROM Payroll.Contract C2
									INNER JOIN Payroll.Employee E2 ON E2.Id = C2.EmployeeId
									INNER JOIN Common.ThirdParty TP2 ON TP2.Id = E2.ThirdPartyId
									WHERE TP2.Nit = TXO.NitEmployee AND C2.Valid = 1 AND C2.[Status] = 1)

			-- Registros sin ningún error identificado (fallaron por razón desconocida)
			INSERT INTO @DatosConError
			SELECT TXO.NitEmployee, 'Datos incorrectos o incompletos para cédula ' + TXO.NitEmployee
			FROM @TableXmlObject TXO
			WHERE NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)
			  AND NOT EXISTS (SELECT 1 FROM @DatosConError DCE WHERE TXO.NitEmployee = DCE.NitEmployee)

				-- Insertar en @ReturnTable los datos con error
				INSERT INTO @ReturnTable (StatusField, MessageField, NitEmployee)
				SELECT 
					'777', 
					DCE.ErrorMessage, 
					DCE.NitEmployee
				FROM @DatosConError DCE
			END

			-- RETORNAR RESULTADOS
			-- Los retroactivos se procesarán en el servicio llamando a ExecuteRetroactive
			SELECT * FROM @ReturnTable 
			RETURN

		END TRY
		BEGIN CATCH
			INSERT INTO @ReturnTable (StatusField, MessageField)
			SELECT '999', ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
			
			SELECT * FROM @ReturnTable
			RETURN
		END CATCH
END
