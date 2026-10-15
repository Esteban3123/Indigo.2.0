-- =============================================
-- Author:		Johan Carranza
-- Create date: 2019-01-28
-- Description:	Valida la información del archivo Excel para cargue masivo de dependientes familiares
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveDependentRelatives] 
	@XMLObj XML

AS
BEGIN

Declare @Action varchar(15)
select @Action = ISNULL(TRY_CONVERT(Varchar(15),t.x.value('Action[1]', 'VARCHAR(100)')),'Validar')
FROM @XMLObj.nodes('/Data') t(x)

If @Action <> 'Confirmar'
BEGIN

;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('Kinship[1]', 'VARCHAR(100)') AS Kinship,
			t.x.value('RelationName[1]', 'VARCHAR(100)') AS RelationName,
			t.x.value('BirthDate[1]', 'VARCHAR(100)') AS BirthDate
		FROM @XMLObj.nodes('/Data/Row') t(x)
	), cteType AS(
		SELECT
			Linea,
			'Caracteres inválidos o formato incorrecto' AS Mensaje,
			Columna, 
			Valor,
			CASE
				WHEN Columna = 'Kinship' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'BirthDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				ELSE 0
			END AS TC
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				Kinship,
				BirthDate	
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
	), cte1Kinship AS(
		SELECT
			cteXML.Linea,
			'El codigo de parentesco no existe.' AS Mensaje,
			'Codigo de parentesco.' AS Columna,
			cteXML.Kinship AS Valor
		FROM cteXML
		LEFT JOIN Payroll.Kinship ks WITH(NOLOCK) ON ks.Code = REPLACE (str( cteXML.Kinship,2),SPACE(1),'0') 
		WHERE ks.Code IS NULL
	), cte1BirthDate AS(
		SELECT
			cteXML.Linea,
			'La fecha de nacimiento no puede ser mayor a la actual:' + CAST([Common].[GETDATE]() AS VARCHAR(30)) AS Mensaje,
			'Fecha de nacimiento del familiar.' AS Columna,
			cteXML.BirthDate AS Valor
		FROM cteXML
		WHERE cteXML.BirthDate > [Common].[GETDATE]()
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

	
	), cte1InsertOrUpdate AS( --Linea para determinar si es insercion o actualizacion.
		--SELECT cntrc.Valid,cntrc.Status, DATEDIFF(day,cntrc.ContractEndingDate,df.ContractInitialDate) AS DIFERENCIA, cntrc.BasicSalary , df.BasicSalary as Newsalary, cntrc.ContractTypeId , cntrc.ContractTypeId as newcontracttype , Pos.Code , df.PositionCode , Pr.Code as riesgocode , REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0') as ProfessionalRiskPercentage , wc.Code as workcentercode, df.Workcenter
			SELECT
			df.Linea,
			'El familiar ya fue registrado con Nit de empleado: ' + CAST(df.Nit AS VARCHAR(30)) AS Mensaje,
			'Nombre familiar - ' AS Columna,
			df.RelationName AS Valor

		FROM Payroll.Relationship R
		INNER JOIN  Payroll.Employee emply WITH(NOLOCK) ON emply.Id = R.EmployeeId
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Id = emply.ThirdPartyId
		INNER JOIN cteXML df WITH(NOLOCK) ON df.Nit = tPrty.Nit AND RTRIM(df.RelationName) = RTRIM(R.Name)

	), cte AS(
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error'FROM cteType WHERE TC = 0
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error' FROM cte1Employee
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error' FROM cte1Kinship
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error' FROM cte1BirthDate
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error' FROM cte2Contract
		UNION ALL
		SELECT Linea, Mensaje, Columna, Valor, Tipo='Error' FROM cte1InsertOrUpdate
		UNION ALL 
		SELECT Linea=Nit,Mensaje=Kinship,Columna=RelationName,BirthDate=BirthDate, Tipo='Datos' FROM cteXML
	)


	SELECT 
		Linea,
		Mensaje,
		Columna,
		Valor,
		Tipo
	FROM cte
	ORDER BY Linea
END
ELSE
BEGIN
	;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('Kinship[1]', 'VARCHAR(100)') AS Kinship,
			t.x.value('RelationName[1]', 'VARCHAR(100)') AS RelationName,
			t.x.value('BirthDate[1]', 'VARCHAR(100)') AS BirthDate
		FROM @XMLObj.nodes('/Data/Row') t(x)
		)
		
		INSERT INTO Payroll.Relationship(EmployeeId, [Name], KinshipId, BirthDate, ProvidesUPC, UPCValue, [Dependent], DependsValue, [State], DependsPercentage, IsEmergencyContact)
		SELECT emply.Id as EmployeeId, RTRIM(df.RelationName) as Name, ks.Id as KinshipId, df.BirthDate, 0,0,0,0,1,0,0
		FROM cteXML df
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = df.nit
		INNER JOIN  Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.id
		INNER JOIN Payroll.Kinship Ks WITH(NOLOCK) ON ks.Code = REPLACE (str( df.Kinship,2),SPACE(1),'0') 

		;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('Kinship[1]', 'VARCHAR(100)') AS Kinship,
			t.x.value('RelationName[1]', 'VARCHAR(100)') AS RelationName,
			t.x.value('BirthDate[1]', 'VARCHAR(100)') AS BirthDate
		FROM @XMLObj.nodes('/Data/Row') t(x)
		)

		SELECT Linea = Linea, Mensaje = 'Se inserto el familiar:', Columna = RelationName, Valor = 'Cedula Empleado:', Tipo = Nit
		FROM cteXML
END
END