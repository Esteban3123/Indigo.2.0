-- =============================================
-- Author:		Andrés Steven Rojas 
-- Create date: 04/12/2023
-- Description:	Store Procedure que 
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SetIncreaseSalaryFromFile]
	@XmlObject Xml
AS 
BEGIN
	SET NOCOUNT ON;

		-- TABLA DEL XML
		DECLARE @TableXmlObject TABLE
		(
			NitEmployee VARCHAR(20),
			EmployeeName VARCHAR(300),
			FunctionalUnitCode VARCHAR(20),
			PositionCode VARCHAR(20),
			InitialDate DATETIME,
			BasicSalary DECIMAL(18, 2),
			ValueIncrease DECIMAL(18, 2),
			NewSalary DECIMAL (18, 2)
		)
		-- TABLA RETURN
		-- TABLA DE RETORNO 
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
			BasicSalary NUMERIC(18,0), 
			ValueIncrease NUMERIC(18,0), 
			NewSalary NUMERIC(18,0), 
			PercentageIncrease DECIMAL(8,4),
			PositionCode VARCHAR(20), 
			PositionName VARCHAR(200), 
			FunctionalUnitCode VARCHAR(20), 
			FunctionalUnitName VARCHAR(100),
			InitialDate DATETIME
		)

		BEGIN TRY

			INSERT INTO @TableXmlObject
		(
			NitEmployee, EmployeeName, FunctionalUnitCode, PositionCode,
			InitialDate, BasicSalary, ValueIncrease, NewSalary
		)
		SELECT	t.x.value('NitEmployee[1]','varchar(20)') as NitEmployee,
				t.x.value('EmployeeName[1]','varchar(300)') as EmployeeName,
				t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
				t.x.value('PositionCode[1]','varchar(20)') as PositionCode,
				TRY_PARSE (t.x.value('InitialDate[1]','varchar(500)') AS DATETIME) as InitialDate,
				t.x.value('BasicSalary[1]','DECIMAL(18, 2)') as BasicSalary,
				t.x.value('ValueIncrease[1]','DECIMAL(18, 2)') as ValueIncrease,
				t.x.value('NewSalary[1]','DECIMAL(18, 2)') as NewSalary
				
		FROM @XmlObject.nodes('/Data/Row') t(x);

		-- Se eliminan los registros del XML que tienen cédula repetida
		WITH CTE AS (
			SELECT
				NitEmployee,
				EmployeeName,
				FunctionalUnitCode,
				PositionCode,
				InitialDate,
				BasicSalary,
				ValueIncrease,
				NewSalary,
				ROW_NUMBER() OVER (PARTITION BY NitEmployee ORDER BY (SELECT NULL)) AS RowNum
			FROM
				@TableXmlObject
				)
				DELETE FROM CTE WHERE RowNum > 1


		--Validación de la información
		
		INSERT INTO @ReturnTable
			SELECT '001' as StatusField, 'Proceso Realizado Correctamente' as MessageField, C.Id as ContractId, C.InitialContractNumber as InitialContractNumber, TP.Nit as NitEmployee, TP.Name as EmployeeName, 
			C.JobBondingDate as JobBondingDate, C.BasicSalary as BasicSalary, TXO.ValueIncrease as ValueIncrease, 
			-- CALCULO NUEVO SALARIO
			C.BasicSalary + TXO.ValueIncrease as NewSalary,
			-- CALCULO PORCENTAJE DE AUMENTO DE SALARIO
			(TXO.ValueIncrease / C.BasicSalary) * 100 as PercentageIncrease, 
			P.Code as PositionCode, P.Name as PositionName, FU.Code as FunctionalUnitCode, FU.Name as FunctionalUnitName, TXO.InitialDate as InitialDate 
		FROM Payroll.Contract C 
			INNER JOIN Payroll.Position P ON P.Id = C.PositionId
			INNER JOIN Payroll.FunctionalUnit FU ON FU.Id = C.FunctionalUnitId
			INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
			INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
			INNER JOIN @TableXmlObject TXO ON TXO.FunctionalUnitCode = FU.Code
			WHERE P.Code = TXO.PositionCode AND TP.Nit = TXO.NitEmployee
			AND C.Valid = 1 AND C.[Status] = 1
			ORDER BY TP.Name

		
			-- VALIDANDO DATOS QUE NO EXISTEN
			IF (SELECT COUNT(*) FROM @ReturnTable) <> (SELECT COUNT(*) FROM @TableXmlObject)
			BEGIN
				-- Declarar una tabla temporal para almacenar los datos no encontrados
				DECLARE @DatosNoEncontrados TABLE (
					NitEmployee VARCHAR(20))

				-- Insertar en la tabla temporal los datos no encontrados
				INSERT INTO @DatosNoEncontrados
				SELECT 
					TXO.NitEmployee
				FROM 
					@TableXmlObject TXO
				WHERE 
					NOT EXISTS (SELECT 1 FROM @ReturnTable RT 
								WHERE TXO.NitEmployee = RT.NitEmployee 
								   AND TXO.FunctionalUnitCode = RT.FunctionalUnitCode 
								   AND TXO.PositionCode = RT.PositionCode)
					OR NOT EXISTS (SELECT 1 FROM Common.ThirdParty TP 
									WHERE TXO.NitEmployee = TP.Nit)
					OR NOT EXISTS (SELECT 1 FROM Payroll.FunctionalUnit FU 
									WHERE TXO.FunctionalUnitCode = FU.Code)
					OR NOT EXISTS (SELECT 1 FROM Payroll.Position P 
									WHERE TXO.PositionCode = P.Code);

				

				-- Insertar en @ReturnTable los datos no encontrados
				INSERT INTO @ReturnTable (StatusField, MessageField, NitEmployee)
				SELECT '777', 'Alguno de los datos de esta cédula o la cédula es incorrecta', 
					   DNE.NitEmployee
				FROM @DatosNoEncontrados DNE
			END



			SELECT * FROM @ReturnTable 
			RETURN


		END TRY
		BEGIN CATCH
			INSERT INTO @ReturnTable (StatusField, MessageField)
				SELECT '999', ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10))
		END CATCH
END