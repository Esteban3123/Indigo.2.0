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