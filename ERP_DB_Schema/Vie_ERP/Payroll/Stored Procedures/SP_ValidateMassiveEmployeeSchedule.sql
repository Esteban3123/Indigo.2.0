-- =============================================
-- Author:		Johan Carranza
-- Create date: 2020-03-26
-- Description:	Valida la información del archivo Excel para cargue masivo de ingreso y salida de empleados por biometrico.
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ValidateMassiveEmployeeSchedule]  
	@XMLObj XML,
	@Action VARCHAR(20),
	@Code VARCHAR(20),
	@Description VARCHAR(300),
	@Status TINYINT,
	@Id INT,
	@Prefix VARCHAR(20)
AS
BEGIN

Declare @Usuario Int
select @Usuario = ISNULL(TRY_CONVERT(INT,t.x.value('User[1]', 'VARCHAR(100)')),0)
FROM @XMLObj.nodes('/Data') t(x)

declare @Code_Output INT,
		@Message_Output VARCHAR(MAX)


If @Action <> 'Confirmar' AND @Action<> 'Consultar'
BEGIN
	;WITH cteXML AS(
		SELECT 
			ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('InitialDate[1]', 'VARCHAR(100)') AS InitialDate,
			t.x.value('InitialHourDate[1]', 'VARCHAR(100)') AS InitialHourDate,
			t.x.value('EndDate[1]', 'VARCHAR(100)') AS EndDate,
			t.x.value('EndHourDate[1]', 'VARCHAR(100)') AS EndHourDate,
			t.x.value('InitialDateComplete[1]', 'VARCHAR(100)') AS InitialDateComplete,
			t.x.value('EndDateComplete[1]', 'VARCHAR(100)') AS EndDateComplete
		FROM @XMLObj.nodes('/Data/Row') t(x)
		), cteType AS(
		SELECT
			Linea,
			'Caracteres inválidos/formato incorrecto/Campo Faltante' AS Mensaje,
			Columna, 
			Valor,
			CASE
				WHEN Columna = 'Nit' AND TRY_CONVERT(INT, Valor) IS NOT NULL THEN 1
				WHEN Columna = 'InitialDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'InitialHourDate' AND TRY_CONVERT(TIME(7), Valor) IS NOT NULL THEN 1
				WHEN Columna = 'EndDate' AND TRY_CONVERT(DATE, Valor, 103) IS NOT NULL THEN 1
				WHEN Columna = 'EndHourDate' AND TRY_CONVERT(TIME(7), Valor) IS NOT NULL THEN 1
				ELSE 0
			END AS TC
		FROM cteXML
		UNPIVOT(
			Valor FOR Columna IN(
				Nit,
				InitialDate,
				InitialHourDate,
				EndDate,
				EndHourDate
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
			CASE WHEN cntrc.EmployeeId IS NULL THEN 'El empleado no tiene un contrato activo'
			WHEN cteXML.EndDateComplete > cntrc.ContractEndingDate THEN 'La fecha de salida ('+CAST(cteXML.EndDateComplete AS VARCHAR(30))+') del empleado, no puede ser mayor a la fecha final de contrato ('+CAST(cntrc.ContractEndingDate AS VARCHAR(30))+').'
			END AS Mensaje,
			'Número Identificación' AS Columna,
			cteXML.Nit AS Valor
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		LEFT JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		WHERE (cntrc.EmployeeId IS NULL OR cteXML.EndDateComplete > cntrc.ContractEndingDate)
	),cte2Dates AS(
		SELECT
		cteXML.Linea,
		'La fecha entrada ('+CAST(cteXML.InitialDateComplete AS VARCHAR(30))+') no puede ser mayor a la fecha de salida ('+CAST(cteXML.EndDateComplete AS VARCHAR(30))+')' as Mensaje,
		'Número Identificación' AS Columna,
		cteXML.Nit AS Valor
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		WHERE cteXML.InitialDateComplete > cteXML.EndDateComplete
	), cte2Novelty AS(
		SELECT cteXML.Linea,
		'El empleado tiene '+ 
		CASE WHEN SD.Letter = 'I' THEN 'Incapacidad'
		WHEN SD.Letter = 'S' THEN 'sanción'
		WHEN SD.Letter = 'L' THEN 'licencia'
		WHEN SD.Letter = 'V' THEN 'vacaciones'
		WHEN SD.EmployeeId IS NULL THEN ' horario registrado, pero no tiene turno asignado '
		END + ' correspondiente a la fecha ' + CAST(ISNULL(SD.DateDetail, cteXML.InitialDate) AS VARCHAR(30)) As Mensaje,
		'Número Identificación' AS Columna,
		cteXML.Nit As Valor,
		CASE WHEN SD.EmployeeId IS NULL THEN 'Info' WHEN SD.EmployeeId IS NOT NULL AND SD.Letter IN('I','S','L','V') THEN 'Error' ELSE 'Otro' END As Tipo
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = emply.Id AND cntrc.Valid = 1 AND cntrc.[Status] = 1
		LEFT JOIN Payroll.ScheduleDetail SD ON DATEDIFF(DAY,SD.DateDetail,cteXML.InitialDate)=0 AND SD.EmployeeId = emply.Id AND SD.ContractId = cntrc.Id --AND (SD.Letter IN('I','S','L','V') OR SD.EmployeeId IS NULL)
	), cteOutput AS(
		SELECT Linea, cteXML.Nit, Employee = tPrty.Name , InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours] = DATEDIFF(HOUR, InitialDateComplete, EndDateComplete), Comments = 'Cargado correctamente'
		FROM cteXML
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = cteXML.Nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
	), cte AS(
		SELECT Linea, Nit= NULL, Employee=NULL, InitialDate= NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL,Mensaje, Columna, Valor, Tipo = 'Error'
		FROM cteType 
		WHERE TC = 0
		UNION ALL
		SELECT Linea, Nit= NULL, Employee=NULL, InitialDate= NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL,Mensaje, Columna, Valor, Tipo = 'Error'
		FROM cte1Employee
		UNION ALL
		SELECT Linea, Nit= NULL, Employee=NULL, InitialDate= NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL,Mensaje, Columna, Valor, Tipo = 'Error'
		FROM cte2Contract
		UNION ALL
		SELECT Linea, Nit= NULL, Employee=NULL, InitialDate= NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL,Mensaje, Columna, Valor, Tipo = 'Error'
		FROM cte2Dates
		UNION ALL
		SELECT Linea, Nit=NULL, Employee=NULL, InitialDate=NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL,Mensaje, Columna, Valor, Tipo
		FROM cte2Novelty
		UNION ALL
		SELECT Linea, Nit, Employee, InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours], Comments,Mensaje=NULL, Columna=NULL, Valor=NULL, Tipo = 'Datos' 
		FROM cteOutput
	) 
	--Validar ScheduleDetail y ScheduleDetailHour si tiene alguna novedad que no permite que sea cargada.

	SELECT  Linea,Nit, Employee, InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours], Comments, Mensaje, Columna, Valor, Tipo
	FROM cte
	ORDER BY Linea
END
ELSE IF @Action = 'Consultar'
BEGIN
	DECLARE @dataFileConsult TABLE(Linea BIGINT,  Nit VARCHAR(30), Employee VARCHAR(200), InitialDate VARCHAR(30) NULL, InitialHourDate VARCHAR(30) NULL, EndDate VARCHAR(30) NULL, EndHourDate VARCHAR(30) NULL, [Hours] INT, Comments VARCHAR(100),InitialDateComplete DATE, EndDateComplete DATE, Code varchar(20), [Description] varchar(300), [Status] bit)

	INSERT INTO @dataFileConsult(Linea,  Nit, Employee, InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours], Comments,InitialDateComplete, EndDateComplete, Code, [Description], [Status])
	SELECT ESD.Id, TP.Nit, tp.Name, ESD.InitialDate, ESD.InitialHourDate, ESD.EndDate, ESD.EndHourDate, ESD.[Hours], '', [Common].[GETDATE](), [Common].[GETDATE](), '001', 'OK', 1
	FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC, Payroll.Employee E, Common.ThirdParty TP
	WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.Code = @Code AND ESD.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id

	SELECT  Linea, Nit, Employee, InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours], Comments, Mensaje = null, Columna = null, Valor = null, Tipo='Info'
	FROM @dataFileConsult

END
ELSE IF @Action = 'Confirmar'
BEGIN
DECLARE @dataFile TABLE(Linea BIGINT,  Nit VARCHAR(30), InitialDate DATE, InitialHourDate TIME(7), EndDate DATE, EndHourDate TIME(7), [Hours] INT, Comments VARCHAR(100),InitialDateComplete DATE, EndDateComplete DATE, Code varchar(20), [Description] varchar(300), [Status] bit)

BEGIN TRAN [tran1]
BEGIN TRY

;WITH cteXML AS(
		SELECT 
			t.x.value('Linea[1]', 'VARCHAR(100)') AS Linea,
			t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
			t.x.value('InitialDate[1]', 'VARCHAR(100)') AS InitialDate,
			t.x.value('InitialHourDate[1]', 'VARCHAR(100)') AS InitialHourDate,
			t.x.value('EndDate[1]', 'VARCHAR(100)') AS EndDate,
			t.x.value('EndHourDate[1]', 'VARCHAR(100)') AS EndHourDate,
			t.x.value('Hours[1]', 'VARCHAR(100)') AS [Hours],
			t.x.value('Comments[1]', 'VARCHAR(100)') AS [Comments],
			t.x.value('InitialDateComplete[1]', 'VARCHAR(100)') AS InitialDateComplete,
			t.x.value('EndDateComplete[1]', 'VARCHAR(100)') AS EndDateComplete
		FROM @XMLObj.nodes('/Data/Row') t(x)
		), cteOutput AS(
			SELECT
			CASE WHEN TRY_CONVERT(INT, cteXML.Linea) IS NULL THEN 0 ELSE Linea END AS Linea,
			CASE WHEN TRY_CONVERT(INT, cteXML.Nit) IS NULL THEN 0 ELSE Nit END AS Nit,
			CASE WHEN TRY_CONVERT(DATE, cteXML.InitialDate, 103) IS NULL THEN [Common].[GETDATE]() ELSE cteXML.InitialDate END as InitialDate,
			CASE WHEN TRY_CONVERT(TIME(7), cteXMl.InitialHourDate) IS NULL THEN '00:00' ELSE cteXML.InitialHourDate END as InitialHourDate,
			CASE WHEN TRY_CONVERT(DATE, cteXMl.EndDate, 103) IS NULL THEN [Common].[GETDATE]() ELSE cteXML.EndDate END as EndDate,
			CASE WHEN TRY_CONVERT(TIME(7), cteXML.EndHourDate) IS NULL THEN '00:00' ELSE EndHourDate END as EndHourDate,
			CASE WHEN TRY_CONVERT(INT, cteXML.Hours) IS NULL THEN 0 ELSE [Hours] END as [Hours],
			cteXML.Comments, --Doesnt need conversion
			cteXML.InitialDateComplete,
			--CASE WHEN TRY_CONVERT(VARCHAR(30), cteXML.InitialDateComplete) IS NULL THEN [Common].[GETDATE]() ELSE cteXML.InitialDateComplete END as InitialDateComplete,
			CASE WHEN TRY_CONVERT(VARCHAR(30), cteXML.EndDateComplete) IS NULL THEN [Common].[GETDATE]() ELSE cteXML.EndDateComplete END as EndDateComplete
			FROM cteXML
		)
		INSERT INTO @dataFile(Linea,  Nit, InitialDate, InitialHourDate, EndDate, EndHourDate, [Hours], Comments,InitialDateComplete, EndDateComplete)
		SELECT * from cteOutput

		DECLARE @ConfirmationUser VARCHAR(20) = CASE WHEN @Status = 2 THEN @Usuario ELSE NULL END
		DECLARE @ConfirmationDate DATETIME = CASE WHEN @Status = 2 THEN [Common].[GETDATE]() ELSE NULL END

		IF @Id = 0
			BEGIN
				IF @Code = '' 
					BEGIN
						--Si se esta insertando por primera vez se consulta la secuencia numerica
						DECLARE @IsManual BIT
						DECLARE @OperatingUnitId INT = (SELECT TOP 1 Id FROM Common.OperatingUnit)
				
						EXEC Common.SP_GetSequence 120, 2141, @OperatingUnitId, @Prefix, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

						IF @Code_Output <> 0
						BEGIN
							SELECT Linea , Nit = CAST(Nit AS VARCHAR(30)) , Employee=NULL, InitialDate=NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours], Comments, Mensaje=REPLACE(@Message_Output, '{0}', 'Secuencias'), Columna=NULL, Valor=NULL, Tipo='Info'
							FROM @dataFile
							ORDER BY Linea
						END
				END

				PRINT '@@ConfirmationDate ' + convert(varchar(20),@ConfirmationDate)

				--Se inserta la cabecera
				INSERT INTO Payroll.EmployeeScheduleC
				(
					[Code],[Description], [Status], CreationDate, CreationUser
				)
				VALUES(@Code, @Description, @Status, [Common].[GETDATE](), @Usuario)

				--Obtengo el id de la cabcera
				SET @Id = SCOPE_IDENTITY()

				INSERT INTO Payroll.EmployeeScheduleDetail([IdEmployeeScheduleC],[EmployeeId],[ContractId],InitialContractNumber, [InitialDate],[InitialHourDate],[EndDate],[EndHourDate],[Hours],[CreationUser],[CreationDate])
				SELECT @Id, E.Id, cntrc.Id,cntrc.InitialContractNumber, df.InitialDate, df.InitialHourDate, df.EndDate, df.EndHourDate, df.Hours, @Usuario, [Common].[GETDATE]()
				FROM @dataFile df
				INNER JOIN Common.ThirdParty TP ON TP.Nit = df.Nit
				INNER JOIN Payroll.Employee E ON E.ThirdPartyId = TP.Id
				INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = E.Id 
				AND cntrc.Valid = 1 AND cntrc.[Status] = 1
						
			END
			ELSE --Si se esta actualizando
			BEGIN
				UPDATE [Payroll].[EmployeeScheduleC]
					SET [Code] = @Code,
						[Description] = @Description,
						[Status] = @Status,
						[ConfirmationUser] = @ConfirmationUser,
						[ConfirmationDate] = @ConfirmationDate
				WHERE Id = @Id

				INSERT INTO Payroll.EmployeeScheduleDetail([IdEmployeeScheduleC],[EmployeeId],[ContractId],InitialContractNumber, [InitialDate],[InitialHourDate],[EndDate],[EndHourDate],[Hours],[CreationUser],[CreationDate])
				SELECT @Id, E.Id, cntrc.Id,cntrc.InitialContractNumber, df.InitialDate, df.InitialHourDate, df.EndDate, df.EndHourDate, df.Hours, @Usuario, [Common].[GETDATE]()
				FROM @dataFile df
				INNER JOIN Common.ThirdParty TP ON TP.Nit = df.Nit
				INNER JOIN Payroll.Employee E ON E.ThirdPartyId = TP.Id
				INNER JOIN Payroll.[Contract] cntrc WITH(NOLOCK) ON cntrc.EmployeeId = E.Id 
				AND cntrc.Valid = 1 AND cntrc.[Status] = 1


			END

			IF @Status = 3 BEGIN
				UPDATE [Payroll].[EmployeeScheduleC] SET [Status] = 3, AnnulmentDate = [Common].[GETDATE](), AnnulmentUser = @Usuario WHERE Id = @Id
			END

			IF @Status = 2 BEGIN
				UPDATE [Payroll].[EmployeeScheduleC] SET [Status] = 2, ConfirmationDate = [Common].[GETDATE](), ConfirmationUser = @Usuario WHERE Id = @Id
			END

			SELECT TOP 1 Linea = null, Nit = CAST(Nit AS VARCHAR(30)) , Employee=NULL, InitialDate=NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]= NULL, Comments=NULL, Mensaje='Se Confirmó Correctamente', Columna=NULL, Valor='1', Tipo='Info'
			FROM Payroll.EmployeeScheduleDetail ESD, Payroll.EmployeeScheduleC EC, Payroll.Employee E, Common.ThirdParty TP
			WHERE EC.Id = ESD.IdEmployeeScheduleC AND EC.Code = @Code AND ESD.EmployeeId = E.Id AND E.ThirdPartyId = TP.Id

		COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
		SELECT TOP 1 Linea = 1 , Nit = NULL, Employee=NULL, InitialDate=NULL, InitialHourDate=NULL, EndDate=NULL, EndHourDate=NULL, [Hours]=NULL, Comments=NULL, Mensaje=ERROR_MESSAGE(), Columna=NULL, Valor=NULL, Tipo=NULL
		FROM @dataFile
		ORDER BY Linea
		ROLLBACK TRAN [tran1]
	END CATCH
END
END