-- =============================================
-- Author:		Johan Carranza
-- Create date: 2019-01-30
-- Description:	Valida la información del archivo Excel para cargue masivo de entidades externas.
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveExternalEntities] 
	@XMLObj XML

AS
BEGIN

Declare @Action varchar(15)
select @Action = ISNULL(TRY_CONVERT(Varchar(15),t.x.value('Action[1]', 'VARCHAR(100)')),'Validar')
FROM @XMLObj.nodes('/Data') t(x)

Declare @Usuario Int
select @Usuario = ISNULL(TRY_CONVERT(INT,t.x.value('User[1]', 'VARCHAR(100)')),0)
FROM @XMLObj.nodes('/Data') t(x)

If @Action <> 'Confirmar'
BEGIN

;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			CASE WHEN (t.x.value('FundCode[1]', 'VARCHAR(100)')) = '' THEN 'x' ELSE (t.x.value('FundCode[1]', 'VARCHAR(100)')) END AS FundCode,
			t.x.value('FundType[1]', 'VARCHAR(100)') AS FundType,
			t.x.value('FundInitialDate[1]', 'VARCHAR(100)') AS FundInitialDate,
			t.x.value('VoluntaryContribution[1]', 'VARCHAR(100)') AS VoluntaryContribution,
			CASE WHEN (t.x.value('VoluntaryContributionValue[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('VoluntaryContributionValue[1]', 'VARCHAR(100)')) END AS VoluntaryContributionValue,
			CASE WHEN (t.x.value('BankCode[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('BankCode[1]', 'VARCHAR(100)')) END AS BankCode,
			CASE WHEN (t.x.value('BankAccountNumberType[1]', 'VARCHAR(100)')) = '' THEN '1' ELSE (t.x.value('BankAccountNumberType[1]', 'VARCHAR(100)')) END AS BankAccountNumberType,
			t.x.value('BankAccountNumber[1]', 'VARCHAR(100)') AS BankAccountNumber
			--CASE WHEN (t.x.value('BankAccountNumber[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('BankAccountNumber[1]', 'VARCHAR(100)')) END AS BankAccountNumber
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteType AS(
		SELECT
			Linea,
			'Caracteres inválidos o formato incorrecto' AS Mensaje,
			Columna, 
			Valor,
			CASE
				WHEN Columna = 'Nit' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'FundCode' AND TRY_CONVERT(VARCHAR(20), Valor) IS NOT NULL OR Valor = 'x' THEN 1
				WHEN Columna = 'FundType' AND TRY_CONVERT(INT, Valor) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'FundInitialDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'VoluntaryContribution' AND TRY_CONVERT(BIT, Valor) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'VoluntaryContributionValue' AND TRY_CONVERT(NUMERIC(18,0), Valor) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'BankCode' AND TRY_CONVERT(INT, Valor) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'BankAccountNumberType' AND TRY_CONVERT(INT, Valor) IS NOT NULL OR Valor = '' THEN 1
				WHEN Columna = 'BankAccountNumber' AND TRY_CONVERT(INT, Valor) IS NOT NULL OR Valor = '' THEN 1
				ELSE 0
			END AS TC
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				Nit,
				FundCode,
				FundType,
				FundInitialDate,
				VoluntaryContribution,
				VoluntaryContributionValue,
				BankCode,
				BankAccountNumberType,
				BankAccountNumber
			)
		) u
	), cte1Employee AS(
		SELECT
			cteXML.Linea,
			'El empleado no existe' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		WHERE emply.ThirdpartyId IS NULL
	), cte2Contract AS(
		SELECT
			cteXML.Linea,
			'El empleado no tiene un contrato activo' AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		LEFT JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		LEFT JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		LEFT JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		WHERE cntrc.EmployeeId IS NULL
	), ctrFundContract AS(
			SELECT
				cteXML.Linea,
				CASE WHEN  (F.Id IS NULL AND cteXML.FundCode <> 'x') THEN 'El codigo del fondo no existe.' 
				WHEN ((cteXML.FundType <> '' OR cteXML.FundInitialDate <> '' OR VoluntaryContribution<> '') AND cteXML.FundCode = 'x') THEN 'El codigo del fondo no puede estar vacio.' END AS Mensaje,
				'Codigo fondo.' AS Columna,
				cteXML.FundCode AS Valor
			FROM cteXML 
			LEFT JOIN Payroll.Fund F WITH(NOLOCK) ON F.Code = cteXML.FundCode  --No venga vacio. 
			WHERE (F.Id IS NULL AND cteXML.FundCode <> 'x') OR ((cteXML.FundType <> '' OR cteXML.FundInitialDate <> '' OR VoluntaryContribution<> '') AND cteXML.FundCode = 'x')
	), cteBankCode As(
			SELECT
				cteXML.Linea,
				CASE WHEN (B.Id IS NULL AND cteXML.BankCode <> 0) THEN 'El codigo del banco no existe.' 
				WHEN (cteXML.BankAccountNumber <> '' AND cteXML.BankCode = 0) THEN 'El codigo del banco no puede estar vacio.' END AS Mensaje,
				'Codigo banco.' AS Columna,
				cteXML.BankCode AS Valor
			FROM cteXML 
			LEFT JOIN Payroll.Bank B WITH(NOLOCK) ON B.Code = cteXML.BankCode 
			WHERE (B.Id IS NULL AND cteXML.BankCode <> 0) OR (cteXML.BankAccountNumber <> '' AND cteXML.BankCode = 0)
	), cteVoluntaryContributionValue As(
			SELECT
				cteXML.Linea,
				'Debe tener valor de aporte voluntario mayor a 0.' AS Mensaje,
				'Valor Aporte Voluntario.' AS Columna,
				cteXML.VoluntaryContributionValue AS Valor
			FROM cteXML 
			WHERE VoluntaryContribution = 1 AND VoluntaryContributionValue <=0
	), cteAtLeastOne As (
			Select cteXML.Linea,
				'Debe ingresar al menos codigo de banco o codigo de fondo.' AS Mensaje,
				'Codigo banco/Codigo Fondo.' AS Columna,
				cteXML.BankCode + '/' + cteXML.FundCode AS Valor
			FROM cteXML
			WHERE BankCode=0 AND FundCode = 'x'
	), cteOutput As (
			SELECT cteXML.*, F.Name as FundName, B.Name As BankName, TP.Name as Employee
			FROM cteXML
			INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON cteXML.nit = TP.Nit
			LEFT JOIN Payroll.Fund F WITH(NOLOCK) ON cteXML.FundCode = F.Code
			LEFT JOIN Payroll.Bank B WITH(NOLOCK) ON cteXML.BankCode = B.Code
	), cteRepeatFundtype As (
			--select Linea,FundType, count(FundType) as Cantidad
			Select 'Nit:' + Nit as Linea,
				'El tipo de fondo no puede repetirse.' AS Mensaje,
				'Codigo fondo.' AS Columna,
				cteXML.FundType+' - Numero de veces'+ CAST(count(FundType) AS VARCHAR(5)) AS Valor
			from cteXML group by FundType,Nit having count(FundType) > 1
	), cteFundInitialDate As (
			Select cteXML.Linea,
				CASE 
				WHEN (cteXML.FundInitialDate = '' AND cteXML.FundCode <> 'x') THEN 'La fecha no puede estar vacia.'  
				WHEN DATEDIFF(day,[Common].[GETDATE](),cteXML.FundInitialDate)>1 THEN 'La fecha inicial del fondo, no puede ser mayor a la fecha actual.' + CAST([Common].[GETDATE]() AS VARCHAR(30))
				WHEN DATEDIFF(day,FC.InitialDate,cteXML.FundInitialDate)<1 THEN 'La fecha inicial del nuevo contrato: '+ CAST(cteXML.FundInitialDate AS VARCHAR(20))
				+ ' debe ser mayor a la fecha inicial del pasado contrato. (' + CONVERT(varchar,FC.InitialDate, 103)  + ')' 
				END AS Mensaje,
				'Fecha inicial del fondo.' AS Columna,
				cteXML.FundInitialDate AS Valor
			from cteXML
			INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON cteXML.nit = TP.Nit
			INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = TP.Id
			LEFT JOIN Payroll.Contract C WITH(NOLOCK) ON C.EmployeeId = emply.Id AND C.valid = 1 AND C.Status = 1
			LEFT JOIN Payroll.FundContract FC ON FC.ContractId = C.Id and FC.State =1
			where (DATEDIFF(day,[Common].[GETDATE](),cteXML.FundInitialDate)>1 OR cteXML.FundInitialDate = '' OR DATEDIFF(day,FC.InitialDate,cteXML.FundInitialDate)<1) AND cteXML.FundCode <> 'x'
	), cteBankAccountNumber As (
			Select cteXML.Linea,
				'El numero de cuenta bancaria no puede estar vacia.' AS Mensaje,
				'#Cuenta bancaria.' AS Columna,
				cteXML.BankAccountNumber AS Valor
			from cteXML
			where BankCode<>0 AND BankAccountNumber=''

	), cte AS(
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteType WHERE TC = 0
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cte1Employee
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cte2Contract
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM ctrFundContract
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteBankCode
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteVoluntaryContributionValue
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteAtLeastOne
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteRepeatFundtype
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteFundInitialDate
		UNION ALL
		SELECT Linea, Nit=Linea, Fundcode=Mensaje, FundType=Columna, FundInitialDate=Valor, VoluntaryContribution=NULL, VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, FundName=NULL, Bankname=NULL, Employee= NULL, Tipo='Error' FROM cteBankAccountNumber
		UNION ALL
		SELECT Linea, Nit, Fundcode, FundType,FundInitialDate, VoluntaryContribution, VoluntaryContributionValue, BankCode,  BankAccountNumberType,BankAccountNumber, FundName, Bankname, Employee , Tipo='Datos' FROM cteOutput
	) 

	SELECT  Linea,Employee, FundCode, FundType, FundInitialDate, VoluntaryContribution, VoluntaryContributionValue, BankCode, BankAccountNumber, BankAccountNumberType, Nit, FundName, Bankname, Tipo
	FROM cte
	ORDER BY Linea
END
ELSE
BEGIN

BEGIN TRAN [tran1]

	DECLARE @dataFile TABLE(Linea BIGINT,  Nit INT, FundCode VARCHAR(20), FundType INT, FundInitialDate DATE, VoluntaryContribution BIT, VoluntaryContributionValue NUMERIC(18,0), BankCode INT, BankAccountNumberType INT, BankAccountNumber INT, ActionResult INT, Fundname VARCHAR(100), BankName VARCHAR(100))

BEGIN TRY
;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			CASE WHEN (t.x.value('FundCode[1]', 'VARCHAR(100)')) = '' THEN 'x' ELSE (t.x.value('FundCode[1]', 'VARCHAR(100)')) END AS FundCode,
			t.x.value('FundType[1]', 'VARCHAR(100)') AS FundType,
			t.x.value('FundInitialDate[1]', 'VARCHAR(100)') AS FundInitialDate,
			t.x.value('VoluntaryContribution[1]', 'VARCHAR(100)') AS VoluntaryContribution,
			CASE WHEN (t.x.value('VoluntaryContributionValue[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('VoluntaryContributionValue[1]', 'VARCHAR(100)')) END AS VoluntaryContributionValue,
			CASE WHEN (t.x.value('BankCode[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('BankCode[1]', 'VARCHAR(100)')) END AS BankCode,
			CASE WHEN (t.x.value('BankAccountNumberType[1]', 'VARCHAR(100)')) = '' THEN '1' ELSE (t.x.value('BankAccountNumberType[1]', 'VARCHAR(100)')) END AS BankAccountNumberType,
			t.x.value('BankAccountNumber[1]', 'VARCHAR(100)') AS BankAccountNumber
			--CASE WHEN (t.x.value('BankAccountNumber[1]', 'VARCHAR(100)')) = '' THEN '0' ELSE (t.x.value('BankAccountNumber[1]', 'VARCHAR(100)')) END AS BankAccountNumber
		FROM @XMLObj.nodes('/Data/Row') t(x)
		)

		INSERT INTO @dataFile(Linea,Nit, FundCode , FundType , FundInitialDate , VoluntaryContribution , VoluntaryContributionValue , BankCode, BankAccountNumberType, BankAccountNumber)
		SELECT
			cteXML.Linea as Linea,
			CASE WHEN TRY_CONVERT(INT, cteXML.Nit) IS NULL THEN 0 ELSE Nit END AS Nit,
			CASE WHEN TRY_CONVERT(VARCHAR(20), cteXML.FundCode) IS NULL THEN '' ELSE FundCode END as FundCode,
			CASE WHEN TRY_CONVERT(INT, cteXMl.FundType) IS NULL THEN 0 ELSE FundType END as FundType,
			CASE WHEN TRY_CONVERT(DATE, cteXMl.FundInitialDate, 103) IS NULL THEN '' ELSE FundInitialDate END as FundInitialDate,
			CASE WHEN TRY_CONVERT(BIT, VoluntaryContribution) IS NULL THEN 0 ELSE VoluntaryContribution END as VoluntaryContribution,
			CASE WHEN TRY_CONVERT(NUMERIC(18,0), VoluntaryContributionValue) IS NULL THEN 0 ELSE VoluntaryContributionValue END as VoluntaryContributionValue,
			CASE WHEN TRY_CONVERT(INT, BankCode) IS NULL THEN 0 ELSE BankCode END as BankCode,
			CASE WHEN TRY_CONVERT(INT, BankAccountNumberType) IS NULL THEN 0 ELSE BankAccountNumberType END as BankAccountNumberType,
			CASE WHEN TRY_CONVERT(INT, BankAccountNumber) IS NULL THEN 0 ELSE BankAccountNumber END as BankAccountNumber
			FROM cteXML

		--Buscar el contrato activo (valid = 1 & status = 1) del empleado, con ese id de contrato desactivo el anterior, busco e tipo de fondo, codigo de fondo ya se que contrato de fondo desactivar.

		UPDATE Payroll.FundContract Set State = 0, EndingDate = DATEADD(day,-1,df.FundInitialDate), ModificationUser = @Usuario, ModificationDate = [Common].[GETDATE]()
		FROM @dataFile df
		INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON TP.Nit = df.Nit
		INNER JOIN Payroll.Employee E WITH(NOLOCK) ON TP.id = E.ThirdPartyId
		INNER JOIN Payroll.Contract C WITH(NOLOCK) ON C.EmployeeId = E.id AND C.valid = 1 AND C.Status = 1 -- Contrato activo
		INNER JOIN Payroll.Fund F WITH(NOLOCK) ON F.Code = df.FundCode --Obliga a que exista el codigo en la tabla Fund.
		WHERE Payroll.FundContract.ContractId = C.Id AND Payroll.FundContract.FundId = F.Id AND Payroll.FundContract.FundType = df.FundType

		--Insertar datos para el fondo e inhabilitar anteriores contratos: tabla.fundcontract.

		INSERT INTO Payroll.FundContract (FundId, ContractId, FundType, InitialDate, EndingDate, MembershipNumber,VoluntaryContribution,
		VoluntaryContributionValue, State, CreationUser, CreationDate, ModificationUser, ModificationDate,ParameterACCAI)
		SELECT FundId=F.Id, ContractId=C.Id, cteXML.FundType,  InitialDate = ctexml.FundInitialDate, NULL, '0' , cteXML.VoluntaryContribution,cteXML.VoluntaryContributionValue, 1, @Usuario, [Common].[GETDATE](), NULL, NULL,NULL
		FROM @dataFile cteXML
		INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON TP.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee E WITH(NOLOCK) ON TP.id = E.ThirdPartyId
		INNER JOIN Payroll.Contract C WITH(NOLOCK) ON C.EmployeeId = E.id AND C.valid = 1 AND C.Status = 1 -- Contrato activo
		INNER JOIN Payroll.Fund F WITH(NOLOCK) ON F.Code = cteXML.FundCode --Obliga a que exista el codigo en la tabla Fund.
		
		--Actualizar datos para el banco.

		UPDATE C Set BankId = B.Id, BankAccountNumber = df.BankAccountNumber, BankAccountType = df.BankAccountNumberType
		FROM @dataFile df
		INNER JOIN Common.ThirdParty TP WITH(NOLOCK) ON TP.Nit = df.Nit
		INNER JOIN Payroll.Employee E WITH(NOLOCK) ON TP.id = E.ThirdPartyId
		INNER JOIN Payroll.Bank B WITH(NOLOCK) ON B.Code = df.BankCode AND df.BankCode <> 0
		INNER JOIN Payroll.Contract C ON C.EmployeeId = E.id AND C.valid = 1 AND C.Status = 1 -- Contrato activo

		UPDATE @dataFile set ActionResult = 1 , Fundname = F.Name --Inserto fondo.
		FROM Payroll.Fund F
		INNER JOIN @dataFile df ON df.FundCode = F.Code

		UPDATE @dataFile set ActionResult = 2 , BankName = B.Name --Actulizo Banco.
		FROM Payroll.Bank B
		INNER JOIN @dataFile df ON df.BankCode = B.Code

		UPDATE @dataFile set ActionResult = 3 , Fundname = F.Name, BankName = B.Name  --Inserto Fondo y Actulizo Banco.
		FROM Payroll.Bank B
		INNER JOIN @dataFile df ON df.BankCode = B.Code
		INNER JOIN Payroll.Fund F ON F.Code = df.FundCode

		SELECT  Linea, NULL as Nit, 
		CASE 
		WHEN ActionResult = 1 THEN 'Se inserto el fondo: ' + Fundname 
		WHEN ActionResult = 2 THEN 'Se actualizo el banco: ' + BankName
		WHEN ActionResult = 3 THEN 'Se inserto el fondo: ' + Fundname + '/ Se actualizo el banco ' + BankName 
		ELSE 'Actualizado correctamente.'
		END as Fundcode,
		FundType='Cedula empleado: ', FundInitialDate=CAST(Nit AS VARCHAR(20)) , VoluntaryContribution=NULL, 
		VoluntaryContributionValue=NULL, BankCode=NULL, BankAccountNumberType=NULL, BankAccountNumber=NULL, 
		FundName=NULL, Bankname=NULL, Employee= NULL, Tipo=NULL
		FROM @dataFile df
		ORDER BY Linea

		COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN [tran1]
	END CATCH
END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que valida y confirma la carga masiva de entidades externas (fondos de pensión, cesantías y bancos) para empleados, a partir de un archivo Excel transformado en XML. Recibe un conjunto de filas con NIT del empleado, código de fondo, tipo de fondo, fecha de inicio, aportes voluntarios, código de banco y número de cuenta, y ejecuta múltiples validaciones: formato y tipo de dato de cada campo, existencia del empleado en el maestro de terceros y en nómina, vigencia de su contrato laboral activo, existencia del código de fondo en el catálogo de fondos, existencia del código de banco en el catálogo de bancos, coherencia entre aportes voluntarios y su valor, y que al menos se informe un fondo o un banco por fila. Cuando la acción es ''Confirmar'', aplica los cambios; de lo contrario, devuelve un reporte de errores por línea indicando columna, valor inválido y mensaje descriptivo, junto con los nombres resueltos de fondo, banco y empleado para retroalimentar al usuario antes de persistir los datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveExternalEntities';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida un lote XML para cargue masivo de fondos y datos bancarios de empleados, devolviendo errores por línea en modo ''Validar'' o aplicando los cambios (cierra fondo previo, inserta nuevo FundContract y actualiza banco del contrato) en modo ''Confirmar''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener nodo /Data y filas /Data/Row con los campos esperados (Nit, FundCode, FundType, FundInitialDate, VoluntaryContribution, VoluntaryContributionValue, BankCode, BankAccountNumberType, BankAccountNumber).; La acción se controla mediante /Data/Action; si no viene se asume ''Validar''.; Para confirmar el cargue se requiere /Data/User (se usa como CreationUser/ModificationUser); si no viene queda en 0.; Deben existir previamente los catálogos Payroll.Fund y Payroll.Bank con los códigos referenciados, y el empleado debe tener un contrato activo en Payroll.Contract.; Las fechas se interpretan en formato 103 (dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan empleados cuyo Nit existe en Common.ThirdParty y tienen un Payroll.Contract con Valid=1 y Status=1 (contrato activo).; El FundCode debe existir en Payroll.Fund y el BankCode (cuando es distinto de 0) en Payroll.Bank; de lo contrario se reporta error en modo Validar.; Cada línea debe traer al menos un código de banco o un código de fondo (no ambos vacíos).; Si VoluntaryContribution = 1, el VoluntaryContributionValue debe ser > 0.; El FundType no puede repetirse para un mismo Nit dentro del mismo archivo.; La FundInitialDate no puede ser mayor a la fecha actual ni menor o igual a la InitialDate del FundContract previo activo del mismo contrato.; Si BankCode <> 0, el BankAccountNumber no puede estar vacío.; Al confirmar, el FundContract previo se cierra con EndingDate = FundInitialDate - 1 día y State = 0 antes de insertar el nuevo registro con State = 1.; El nuevo FundContract se crea siempre con MembershipNumber=''0'', EndingDate=NULL, State=1 y ParameterACCAI=NULL.; La actualización bancaria solo se aplica cuando df.BankCode <> 0 (INNER JOIN sobre Payroll.Bank con esa condición).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cargue masivo de entidades externas de nómina; Empleado; Contrato laboral activo; Fondo (EPS/AFP/Cesantías/Caja); Aporte voluntario; Banco y cuenta bancaria del empleado; Validación de archivo Excel', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Payroll.FundContract: En modo ''Confirmar'': para cada fila del archivo, sobre el FundContract cuyo ContractId = contrato activo del empleado (Valid=1 y Status=1), FundId coincide con Fund.Code = FundCode y FundType = df.FundType, se establece State=0, EndingDate = FundInitialDate - 1 día, ModificationUser=@Usuario y ModificationDate=GETDATE(), inhabilitando el fondo anterior.; [INSERT] Payroll.FundContract: En modo ''Confirmar'': inserta un nuevo FundContract por cada línea con FundId resuelto desde Payroll.Fund, ContractId del contrato activo, InitialDate=FundInitialDate, EndingDate=NULL, MembershipNumber=''0'', State=1, CreationUser=@Usuario, CreationDate=GETDATE() y ParameterACCAI=NULL.; [UPDATE] Payroll.Contract: En modo ''Confirmar'' y solo cuando df.BankCode <> 0: actualiza BankId=Bank.Id, BankAccountNumber=df.BankAccountNumber y BankAccountType=df.BankAccountNumberType del contrato activo del empleado.; [RETURN_RESULT] RESULT_SET: En modo Validar devuelve un único result set por línea con Tipo=''Error'' (mensaje, columna y valor inválido) por cada regla incumplida más Tipo=''Datos'' con FundName/BankName/Employee resueltos; en modo Confirmar devuelve un mensaje por línea según ActionResult (fondo insertado, banco actualizado o ambos).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Action <> ''Confirmar'' (modo Validar, valor por defecto) → Ejecuta todas las CTE de validación y devuelve un único result set con filas Tipo=''Error'' por cada regla incumplida y filas Tipo=''Datos'' con la información resuelta (FundName, BankName, Employee). else @Action = ''Confirmar'': abre transacción [tran1], carga @dataFile saneando tipos, desactiva el FundContract previo, inserta el nuevo FundContract, actualiza datos bancarios del Contract y devuelve mensajes de resultado por línea.; si Cargue de @dataFile: cada columna se sanea con TRY_CONVERT; si falla la conversión se sustituye por 0 / '''' según el tipo objetivo → Permite continuar el proceso aun con valores no convertibles, asumiendo que la fase Validar ya rechazó esos casos.; si ActionResult en @dataFile según existencia de FundCode y BankCode en catálogos → 1 = solo fondo insertado; 2 = solo banco actualizado; 3 = fondo insertado y banco actualizado; en otro caso ''Actualizado correctamente.''; si BLOQUE TRY/CATCH en modo Confirmar → Si ocurre cualquier error durante UPDATEs/INSERT se hace ROLLBACK TRAN [tran1] silenciosamente (sin RAISERROR ni mensaje al cliente).', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Fund; Payroll.Bank; Payroll.FundContract', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveExternalEntities';
-- GO
