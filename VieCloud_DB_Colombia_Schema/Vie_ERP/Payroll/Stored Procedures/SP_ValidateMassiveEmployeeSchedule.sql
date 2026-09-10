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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que valida el archivo Excel de cargue masivo de marcaciones biométricas (entradas y salidas) de empleados. Verifica que cada registro tenga formato correcto (NIT, fechas y horas válidas), que el empleado exista en el sistema cruzando con la tabla de terceros y empleados, que tenga un contrato activo vigente y que la fecha de salida no supere la fecha fin del contrato. Además, detecta novedades en el horario del empleado como incapacidades, sanciones, licencias o vacaciones que coincidan con la fecha de la marcación, clasificando cada hallazgo como Error o Información. Sirve como paso previo de validación antes de confirmar el ingreso masivo de registros de turno en la programación de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y procesa el cargue masivo (desde Excel/XML) de marcaciones de ingreso y salida de empleados por biométrico, gestionando la cabecera y detalle de programación, su confirmación y anulación.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@XMLObj debe contener nodo /Data con User y nodos /Data/Row con campos Nit, InitialDate, InitialHourDate, EndDate, EndHourDate, InitialDateComplete, EndDateComplete (y Linea/Hours/Comments en Confirmar).; Para ''Confirmar'' con @Id=0 y @Code vacío debe existir al menos una fila en Common.OperatingUnit y Common.SP_GetSequence debe poder generar la secuencia (tipo 120, sub 2141).; Para ''Consultar'' debe existir una cabecera Payroll.EmployeeScheduleC con el @Code dado.; Los empleados referenciados deben existir en Common.ThirdParty + Payroll.Employee y tener un Payroll.Contract con Valid=1 y Status=1 para que el detalle pueda insertarse.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los detalles (EmployeeScheduleDetail) siempre se vinculan a un empleado con contrato Valid=1 y Status=1 (INNER JOIN obligatorio).; El Nit del archivo siempre debe corresponder a un ThirdParty con Employee asociado para poder insertar.; Los CreationUser/CreationDate de cabecera y detalle se asignan con @Usuario y Common.GETDATE().; La acción ''Confirmar'' siempre se ejecuta dentro de una transacción nombrada [tran1] con TRY/CATCH y rollback ante excepción.; ConfirmationUser/ConfirmationDate sólo se setean cuando @Status=2; AnnulmentUser/AnnulmentDate sólo cuando @Status=3.; La secuencia automática sólo se solicita cuando @Id=0 y @Code es vacío.; Las consultas de validación usan WITH(NOLOCK) sobre ThirdParty, Employee y Contract.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cargue masivo biométrico de ingreso/salida de empleados; Empleado; Contrato laboral activo; Novedades de nómina (incapacidad, sanción, licencia, vacaciones); Turno/horario programado; Cabecera y detalle de programación de empleados; Secuencia numérica de documento; Confirmación y anulación de documento', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Action distinto de ''Confirmar'' y ''Consultar'' → Ejecuta validaciones del archivo Excel (formato de campos, existencia de empleado, contrato activo, fechas coherentes, novedades en ScheduleDetail) y devuelve filas con Tipo=''Error''/''Info''/''Datos''.; si @Action = ''Consultar'' → Lista los detalles ya cargados (Payroll.EmployeeScheduleDetail) asociados al @Code de la cabecera EmployeeScheduleC.; si @Action = ''Confirmar'' → Procesa el cargue dentro de TRY/CATCH con transacción [tran1]: inserta/actualiza cabecera y detalles; en error hace ROLLBACK y retorna ERROR_MESSAGE().; si @Action=''Confirmar'' AND @Id = 0 → Crea nueva cabecera EmployeeScheduleC; si @Code='''' obtiene secuencia mediante Common.SP_GetSequence (tipo 120, 2141, OperatingUnit top 1) y luego inserta detalles. else Actualiza cabecera existente (Code, Description, Status, ConfirmationUser, ConfirmationDate) e inserta nuevos detalles.; si @Status = 3 → Marca la cabecera EmployeeScheduleC como anulada (Status=3, AnnulmentDate=GETDATE, AnnulmentUser=@Usuario).; si @Status = 2 → Confirma la cabecera EmployeeScheduleC (Status=2, ConfirmationDate=GETDATE, ConfirmationUser=@Usuario); además ConfirmationUser/Date se asignan en el UPDATE previo cuando @Status=2.; si Validación: TRY_CONVERT del valor según columna (Nit→INT, InitialDate/EndDate→DATE estilo 103, InitialHourDate/EndHourDate→TIME(7)) → Si la conversión falla (TC=0) se reporta ''Caracteres inválidos/formato incorrecto/Campo Faltante'' como Error.; si No existe empleado para el Nit (LEFT JOIN ThirdParty/Employee con ThirdPartyId IS NULL) → Reporta ''El empleado no existe'' como Error.; si Empleado sin contrato Valid=1 y Status=1, o EndDateComplete > ContractEndingDate → Reporta ''El empleado no tiene un contrato activo'' o que la fecha de salida supera la fecha final del contrato.; si InitialDateComplete > EndDateComplete → Reporta que la fecha de entrada no puede ser mayor a la fecha de salida.; si ScheduleDetail con Letter IN (''I'',''S'',''L'',''V'') para la fecha y empleado → Reporta novedad (''Incapacidad'',''sanción'',''licencia'',''vacaciones'') como Error; si SD.EmployeeId IS NULL reporta como Info que tiene horario registrado pero sin turno asignado.; si Llamado a Common.SP_GetSequence devuelve @Code_Output <> 0 → Devuelve mensaje de error de secuencia (REPLACE con ''Secuencias'') como fila Tipo=''Info''.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.ScheduleDetail; Payroll.EmployeeScheduleDetail; Payroll.EmployeeScheduleC; Common.OperatingUnit', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ValidateMassiveEmployeeSchedule';
-- GO
