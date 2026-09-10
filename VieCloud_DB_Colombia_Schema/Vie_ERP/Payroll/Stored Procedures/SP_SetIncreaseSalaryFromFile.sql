
-- =============================================
-- Author:		Andrés Steven Rojas
-- Create date: 04/12/2023
-- Update date: 16/10/2025
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
				t.x.value('PercentageIncrease[1]','DECIMAL(8, 4)') as PercentageIncrease,
				t.x.value('AproximationValue[1]','INT') as AproximationValue,
				t.x.value('BasicSalary[1]','DECIMAL(18, 3)') as BasicSalary,
				t.x.value('ValueIncrease[1]','DECIMAL(18, 3)') as ValueIncrease,
				t.x.value('WithRetroactive[1]','INT') as WithRetroactive,
				TRY_PARSE (t.x.value('RetroactiveInitialDate[1]','varchar(500)') AS DATETIME) as RetroactiveInitialDate,
				t.x.value('PayrollPaidRetroactive[1]','INT') as PayrollPaidRetroactive
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
				LEFT JOIN Payroll.ContractModificationReason CMR ON CMR.Code = TXO.ModificationReasonCode AND CMR.State = 1
			ORDER BY TP.Name

		
			-- VALIDANDO DATOS QUE NO EXISTEN O TIENEN ERRORES
			IF (SELECT COUNT(*) FROM @ReturnTable) <> (SELECT COUNT(*) FROM @TableXmlObject)
			BEGIN
				-- Declarar una tabla temporal para almacenar los datos no encontrados o con errores
				DECLARE @DatosConError TABLE (
					NitEmployee VARCHAR(20),
					ErrorMessage VARCHAR(MAX)
				)

				-- Validar empleados no encontrados
				INSERT INTO @DatosConError
				SELECT 
					TXO.NitEmployee,
					CASE 
						WHEN NOT EXISTS (SELECT 1 FROM Common.ThirdParty TP WHERE TXO.NitEmployee = TP.Nit)
							THEN 'Empleado con cédula ' + TXO.NitEmployee + ' no encontrado'
						WHEN NOT EXISTS (SELECT 1 FROM Payroll.Employee E 
										 INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId 
										 WHERE TP.Nit = TXO.NitEmployee)
							THEN 'Empleado con cédula ' + TXO.NitEmployee + ' no tiene registro en nómina'
						WHEN NOT EXISTS (SELECT 1 FROM Payroll.Contract C
										 INNER JOIN Payroll.Employee E ON E.Id = C.EmployeeId
										 INNER JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
										 WHERE TP.Nit = TXO.NitEmployee AND C.Valid = 1 AND C.[Status] = 1)
							THEN 'Empleado con cédula ' + TXO.NitEmployee + ' no tiene contrato activo'
						WHEN TXO.ModificationReasonCode IS NOT NULL 
							AND NOT EXISTS (SELECT 1 FROM Payroll.ContractModificationReason CMR 
										   WHERE CMR.Code = TXO.ModificationReasonCode AND CMR.State = 1)
							THEN 'Código de razón de modificación ' + TXO.ModificationReasonCode + ' no encontrado o inválido'
						WHEN TXO.InitialDate IS NULL
							THEN 'La fecha de inicio es requerida para cédula ' + TXO.NitEmployee
						WHEN TXO.ValueIncrease IS NULL OR TXO.ValueIncrease <= 0
							THEN 'El incremento debe ser mayor a cero para cédula ' + TXO.NitEmployee
						WHEN TXO.PercentageIncrease IS NULL OR TXO.PercentageIncrease <= 0
							THEN 'El porcentaje de aumento debe ser mayor a cero para cédula ' + TXO.NitEmployee
						WHEN TXO.WithRetroactive = 1 AND TXO.RetroactiveInitialDate IS NULL
							THEN 'Si el retroactivo está marcado como 1 (Si), debe ingresar la fecha de inicio retroactivo para cédula ' + TXO.NitEmployee
						ELSE 'Datos incorrectos o incompletos para cédula ' + TXO.NitEmployee
					END
				FROM 
					@TableXmlObject TXO
				WHERE 
					NOT EXISTS (SELECT 1 FROM @ReturnTable RT WHERE TXO.NitEmployee = RT.NitEmployee)

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que recibe un archivo XML con aumentos salariales masivos de empleados y los valida antes de aplicarlos. Para cada registro del archivo, identifica al empleado por su cédula o NIT, consulta su contrato laboral vigente (cargo, unidad funcional, salario básico actual) y calcula el nuevo salario aplicando el incremento indicado (por porcentaje o valor absoluto) con opciones de redondeo o aproximación. Valida que los empleados existan, que los contratos estén activos y que el motivo de modificación contractual sea válido; los registros con errores se retornan con mensajes descriptivos separados de los procesados correctamente. También admite configuración de pago retroactivo, indicando si el período anterior se liquida de forma independiente o junto con la nómina regular.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SetIncreaseSalaryFromFile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara, a partir de un XML, los datos de aumento salarial por empleado (cálculo de nuevo salario con aproximación, porcentaje, retroactivo y motivo) devolviendo un resultado por fila con éxito o error sin aplicar los cambios.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XmlObject debe seguir la estructura /Data/Row con los nodos esperados (NitEmployee, InitialDate, ModificationReasonCode, PercentageIncrease, AproximationValue, BasicSalary, ValueIncrease, WithRetroactive, RetroactiveInitialDate, PayrollPaidRetroactive).; El empleado debe existir en Common.ThirdParty (por Nit), tener registro en Payroll.Employee y poseer un contrato en Payroll.Contract con Valid=1 y Status=1.; Si se envía ModificationReasonCode, debe existir en Payroll.ContractModificationReason con State=1.; InitialDate, ValueIncrease (>0) y PercentageIncrease (>0) son requeridos para considerar válida la fila.; Si WithRetroactive = 1, RetroactiveInitialDate no puede ser nula.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada NitEmployee se procesa una sola vez: se eliminan duplicados del XML conservando solo la primera fila por NitEmployee.; Solo se consideran contratos con Valid = 1 y Status = 1 (contrato activo).; Solo se aceptan motivos de modificación con State = 1 en Payroll.ContractModificationReason.; Las filas exitosas se marcan con StatusField=''001'' y mensaje ''Proceso Realizado Correctamente''; las erróneas con ''777''; los errores de ejecución con ''999''.; El procedimiento no aplica los cambios de salario ni los retroactivos: solo valida y retorna el resultado para que un servicio externo procese (ExecuteRetroactive se invoca fuera).; El nuevo salario nunca se redondea cuando AproximationValue=0 o tiene un valor fuera de {0,1,2,3}.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'empleado; nómina; contrato laboral; salario básico; aumento salarial; porcentaje de aumento; aproximación/redondeo salarial; retroactivo; motivo de modificación contractual; cargo; unidad funcional; cédula (NIT del empleado)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] @TableXmlObject: Tras parsear el XML, mediante CTE con ROW_NUMBER() PARTITION BY NitEmployee se eliminan los duplicados (RowNum > 1), conservando una sola fila por cédula.; [INSERT] @ReturnTable: Por cada fila del XML que hace match con ThirdParty + Employee + Contract activo, se inserta una fila con StatusField=''001'', datos del empleado/contrato/cargo/unidad funcional, NewSalary calculado según AproximationValue y PercentageIncrease (recibido o calculado).; [INSERT] @ReturnTable: Cuando hay filas del XML sin correspondencia en @ReturnTable, se insertan con StatusField=''777'' y un MessageField que describe la causa específica del fallo (empleado no encontrado, sin nómina, sin contrato activo, motivo inválido, fecha/valor/porcentaje inválidos o retroactivo sin fecha).; [INSERT] @ReturnTable: En el bloque CATCH se inserta una fila con StatusField=''999'' y MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE().; [RETURN_RESULT] @ReturnTable: Al final (en TRY o CATCH) se retorna SELECT * FROM @ReturnTable como conjunto de resultados.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AproximationValue = 0 → NewSalary = BasicSalary + ValueIncrease (sin redondeo) else Si =1 redondea a decenas, =2 a centenas, =3 a miles; otro valor → suma sin redondeo; si PercentageIncrease IS NULL en el XML → Se calcula como (ValueIncrease / BasicSalary) * 100 else Se usa el porcentaje recibido en el XML; si COUNT(@ReturnTable) <> COUNT(@TableXmlObject) (hay registros sin match) → Se construye @DatosConError clasificando la causa (empleado inexistente, sin registro en nómina, sin contrato activo, motivo de modificación inválido, fecha inicial nula, incremento ≤0, porcentaje ≤0, retroactivo sin fecha) y se insertan con StatusField=''777''; si BEGIN CATCH (excepción en ejecución) → Se inserta una fila con StatusField=''999'' y MessageField = ERROR_MESSAGE() + '' Linea: '' + ERROR_LINE(), y se retorna @ReturnTable', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Position; Payroll.FunctionalUnit; Payroll.ContractModificationReason', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SetIncreaseSalaryFromFile';
-- GO
