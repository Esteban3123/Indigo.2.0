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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que valida y carga masivamente familiares dependientes de empleados desde un archivo Excel (enviado como XML). En modo validación, verifica que cada fila del archivo cumpla reglas de negocio: que el empleado exista por NIT/cédula en Terceros y Empleados, que el código de parentesco sea válido, que la fecha de nacimiento del familiar no sea futura, que el empleado tenga un contrato activo vigente, y que el familiar no esté ya registrado; devuelve una lista de errores por línea con mensaje, columna y valor problemático. En modo confirmación, inserta los nuevos familiares a cargo en la tabla de Beneficiarios/Familiares (Relationship), asociando al empleado, el tipo de parentesco (Kinship) y la fecha de nacimiento del familiar, permitiendo así la carga masiva de beneficiarios para efectos de nómina, salud y beneficios.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveDependentRelatives';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida un XML con datos de familiares para cargue masivo de dependientes y, según la acción, retorna errores por línea o inserta los registros en la tabla de beneficiarios.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe tener estructura /Data/Row con nodos Nit, Kinship, RelationName y BirthDate.; El nodo Action determina el modo: ''Confirmar'' ejecuta inserción; cualquier otro valor (default ''Validar'') ejecuta validación.; El Nit debe corresponder a un tercero (Common.ThirdParty) vinculado a un empleado (Payroll.Employee).; Para confirmar la inserción, el código de parentesco debe existir en Payroll.Kinship usando el formato de 2 dígitos con ceros a la izquierda.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código de parentesco se normaliza siempre a 2 caracteres con relleno de ceros a la izquierda (REPLACE(STR(Kinship,2),'' '',''0'')) tanto en validación como en inserción.; Las inserciones de familiares siempre se registran con State=1 y con los flags ProvidesUPC, UPCValue, Dependent, DependsValue, DependsPercentage e IsEmergencyContact en 0.; La validación de fecha usa formato 103 (dd/mm/yyyy) y se compara contra Common.GETDATE().; La detección de duplicados de familiar compara nombres con RTRIM en ambos lados.; En modo ''Confirmar'' no se ejecutan las validaciones, por lo que los errores deben haberse resuelto en una invocación previa en modo ''Validar''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cargue masivo de dependientes familiares; empleado; parentesco (Kinship); contrato activo; beneficiario/familiar a cargo; fecha de nacimiento; contacto de emergencia; UPC', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Payroll.Relationship: Cuando @Action=''Confirmar'', por cada fila del XML se inserta un familiar con EmployeeId resuelto vía Nit→ThirdParty→Employee, KinshipId resuelto por código formateado a 2 dígitos, Name=RTRIM(RelationName), BirthDate del XML, y valores fijos ProvidesUPC=0, UPCValue=0, Dependent=0, DependsValue=0, State=1, DependsPercentage=0, IsEmergencyContact=0.; [RETURN_RESULT] (resultset): En modo distinto a ''Confirmar'', se devuelve la unión de errores de validación (Tipo=''Error'') más una copia de los datos originales (Tipo=''Datos''), ordenado por Linea.; [RETURN_RESULT] (resultset): En modo ''Confirmar'' se devuelve una fila por cada registro del XML con el mensaje ''Se inserto el familiar:'' confirmando la inserción.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Action <> ''Confirmar'' (incluido el default ''Validar'' cuando Action es NULL) → Ejecuta todas las validaciones (tipo de datos, existencia de empleado, existencia de parentesco, fecha de nacimiento, contrato activo, duplicidad de familiar) y retorna el listado de errores y datos. else Inserta los familiares en Payroll.Relationship y retorna confirmación por línea.; si Columna=''Kinship'' y TRY_CONVERT(INT,Valor) IS NOT NULL; o Columna=''BirthDate'' y TRY_CONVERT(DATE,Valor,103) IS NOT NULL → Se considera tipo válido (TC=1); en caso contrario se reporta ''Caracteres inválidos o formato incorrecto''.; si BirthDate del XML > Common.GETDATE() → Se reporta error indicando que la fecha de nacimiento no puede ser mayor a la actual.; si No existe Contract con EmployeeId, Valid=1 y Status=1 para el empleado → Se reporta ''El empleado no tiene un contrato activo''.; si Existe en Payroll.Relationship un registro con mismo Nit de empleado y mismo Name (RTRIM) → Se reporta que el familiar ya fue registrado con ese Nit de empleado.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Kinship; Payroll.Contract; Payroll.Relationship', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveDependentRelatives';
-- GO
