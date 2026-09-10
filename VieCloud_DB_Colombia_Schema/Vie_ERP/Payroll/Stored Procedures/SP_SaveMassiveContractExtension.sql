-- =============================================
-- Author:		Iván Ospina
-- Create date: 2019-12-21
-- Description:	
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveMassiveContractExtension]
	@XMLObj XML 
AS
BEGIN

	set nocount on;
	declare @trancount int;
    set @trancount = @@trancount;

	

	Declare @Usuario Int
	select @Usuario = ISNULL(TRY_CONVERT(INT,t.x.value('User[1]', 'VARCHAR(100)')),0)
	FROM @XMLObj.nodes('/Data') t(x)

	DECLARE @dataFile TABLE(
		[Nit] VARCHAR(20),
		[ProfessionalRiskPercentage] VARCHAR(20),
		[Relocation] BIT,
		[Workcenter] VARCHAR(20),
		[ContractModificationReasonCode] VARCHAR(20),
		[PositionCode] VARCHAR(20),
		[FunctionalUnitCode] VARCHAR(20),
		[ContractTypeCode] VARCHAR(20),
		[ContractInitialDate] DATE,
		[ContractEndingDate] DATE,
		[BasicSalary] NUMERIC(18, 0),
		[PaymentPeriod] TINYINT,
		[PaymentType] TINYINT,
		[TypeOfPensionContribution] TINYINT,
		[GroupCode] VARCHAR(20),
		[BankCode] VARCHAR(20),
		[BankAccountNumber] VARCHAR(20),
		[BankAccountType] INT,
		[HoursDaily] TINYINT,
		[Contingency] VARCHAR(100),
		[HealthCode] VARCHAR(20),
		[PensionCode] VARCHAR(20),
		[UnemploymentCode] VARCHAR(20),
		[OccupationalAccidentInsurance] VARCHAR(20),
		[FamilyWelfare] VARCHAR(20),
		[accion] VARCHAR (20),
		InitialContractNumber INT,
		JobBondingDate DATE
	)

	BEGIN TRY
	if @trancount = 0
	BEGIN TRAN [tran1]

		;WITH cteXML AS(
			SELECT 
				ROW_NUMBER() OVER(ORDER BY (SELECT 0)) AS Linea,
				t.x.value('Nit[1]', 'VARCHAR(100)') AS Nit,
				t.x.value('ProfessionalRiskPercentage[1]', 'VARCHAR(100)') AS ProfessionalRiskPercentage,
				CASE WHEN (t.x.value('Workcenter[1]', 'VARCHAR(100)')) = ''  THEN '0' ELSE (t.x.value('Workcenter[1]', 'VARCHAR(100)')) END AS Workcenter,
				t.x.value('ContractModificationReasonCode[1]', 'VARCHAR(100)') AS ContractModificationReasonCode,
				t.x.value('PositionCode[1]', 'VARCHAR(100)') AS PositionCode,
				t.x.value('FunctionalUnitCode[1]', 'VARCHAR(100)') AS FunctionalUnitCode,
				t.x.value('ContractTypeCode[1]', 'VARCHAR(100)') AS ContractTypeCode,
				t.x.value('ContractInitialDate[1]', 'VARCHAR(100)') AS ContractInitialDate,
				t.x.value('ContractEndingDate[1]', 'VARCHAR(100)') AS ContractEndingDate,
				t.x.value('BasicSalary[1]', 'VARCHAR(100)') AS BasicSalary,
				t.x.value('PaymentPeriod[1]', 'VARCHAR(100)') AS PaymentPeriod,
				t.x.value('PaymentType[1]', 'VARCHAR(100)') AS PaymentType,
				t.x.value('GroupCode[1]', 'VARCHAR(100)') AS GroupCode,
				t.x.value('BankCode[1]', 'VARCHAR(100)') AS BankCode,
				t.x.value('BankAccountNumber[1]', 'VARCHAR(100)') AS BankAccountNumber,
				t.x.value('BankAccountType[1]', 'VARCHAR(100)') AS BankAccountType,
				t.x.value('HoursDaily[1]', 'VARCHAR(100)') AS HoursDaily,
				t.x.value('Contingency[1]', 'VARCHAR(100)') AS Contingency,
				t.x.value('HealthCode[1]', 'VARCHAR(100)') AS HealthCode,
				t.x.value('PensionCode[1]', 'VARCHAR(100)') AS PensionCode,
				t.x.value('UnemploymentCode[1]', 'VARCHAR(100)') AS UnemploymentCode,
				t.x.value('OccupationalAccidentInsurance[1]', 'VARCHAR(100)') AS OccupationalAccidentInsurance,
				t.x.value('FamilyWelfare[1]', 'VARCHAR(100)') AS FamilyWelfare
			FROM @XMLObj.nodes('/Data/Row') t(x)
		)

		INSERT INTO @dataFile(
			Nit
			,ProfessionalRiskPercentage
			,Relocation
			,Workcenter
			,ContractModificationReasonCode
			,PositionCode
			,FunctionalUnitCode
			,ContractTypeCode
			,ContractInitialDate
			,ContractEndingDate
			,BasicSalary
			,PaymentPeriod
			,PaymentType
			,TypeOfPensionContribution
			,GroupCode
			,BankCode
			,BankAccountNumber
			,BankAccountType
			,HoursDaily
			,Contingency
			,HealthCode
			,PensionCode
			,UnemploymentCode
			,OccupationalAccidentInsurance
			,FamilyWelfare
			,InitialContractNumber
			,JobBondingDate
		)
		SELECT 
			cte.[Nit],
			TRY_CONVERT(VARCHAR(20), cte.ProfessionalRiskPercentage) AS [ProfessionalRiskPercentage],
			0,
			TRY_CONVERT(VARCHAR(20), cte.Workcenter) AS [Workcenter],
			cte.[ContractModificationReasonCode],
			cte.[PositionCode],
			cte.[FunctionalUnitCode],
			cte.[ContractTypeCode],
			TRY_CONVERT(DATE, cte.ContractInitialDate, 103) AS [ContractInitialDate],
			TRY_CONVERT(DATE, cte.ContractEndingDate, 103) AS [ContractEndingDate],
			TRY_CONVERT(NUMERIC(18, 0), cte.BasicSalary) AS [BasicSalary],
			TRY_CONVERT(TINYINT, cte.PaymentPeriod) AS [PaymentPeriod],
			TRY_CONVERT(TINYINT, cte.PaymentType) AS [PaymentType],
			1,
			cte.[GroupCode],
			cte.[BankCode],
			cte.[BankAccountNumber],
			TRY_CONVERT(INT, cte.BankAccountType) AS [BankAccountType],
			TRY_CONVERT(TINYINT, cte.HoursDaily) AS [HoursDaily],
		    cte.Contingency AS [Contingency],
			cte.[HealthCode],
			cte.[PensionCode],
			cte.[UnemploymentCode],
			cte.[OccupationalAccidentInsurance],
			cte.[FamilyWelfare],
			C.InitialContractNumber,
			C.JobBondingDate
		FROM cteXML cte, Payroll.Contract C, PAyroll.Employee E, Common.ThirdParty TP
		WHERE TP.Id = E.ThirdPartyId AND E.Id = C.EmployeeId AND CTE.Nit = TP.Nit AND C.Valid = 1 AND C.Status = 1
		
		-- HASTA AQUI SE TENDRIA LA TABLA @dataFile CON TODOS LOS DATOS BIEN, SE PROCEDE A SABER SI ES CAMBIO DE CONTRATO, SALARIO, NIVEL DE RIESGO, UBICACION O CUALQUIER OTRO CAMPO

		UPDATE @dataFile set accion = df.accion
		FROM (
		Select cntrc.Id, df.nit, tPrty.[Name] ,cntrc.valid, cntrc.Status,cntrc.InitialContractNumber ,cntrc.JobBondingDate, cntrc.ContractInitialDate,df.ContractInitialDate As ContractInitialDateNew,cntrc.ContractEndingDate,df.ContractEndingDate 
		As ContractEndingDateNew, cntrc.BasicSalary,df.BasicSalary As BasicSalaryNew, cntrc.ContractTypeId, ct.id As ContractTypeIdNew, ct.Name as ContractTypeName, cntrc.PositionId as PositionId,Pos.Id as PositionIdNew, Pos.Name PositionName,
		Pr.Code as CodigoRiesgo, REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0') RiesgoCode, Pr.Name as Riesgo, wc.Code as Worcentercode, df.Workcenter as WorkcentercodeNew, wc.Name as WorkcenterName,
		case when DATEDIFF(day,cntrc.ContractEndingDate,df.ContractInitialDate)=1 then 'INSERT' else
		case when cntrc.BasicSalary<>df.BasicSalary then 'UPDATE' when cntrc.ContractTypeId <> cntrc.ContractTypeId then 'UPDATE' when Pos.Code<> df.PositionCode then 'UPDATE'  when Pr.Code <> REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0')
		then 'UPDATE' when wc.Code <> df.Workcenter then 'UPDATE' else 'Otro' end  end as accion
		--,DATEDIFF(DAY,cntrc.ContractEndingDate,df.ContractInitialDate) as diferencia,df.OccupationalAccidentInsurance
		from Payroll.[Contract] cntrc 
		INNER Join  Payroll.Employee emply WITH(NOLOCK) ON emply.Id = cntrc.EmployeeId
		INNER JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Id = emply.ThirdPartyId
		INNER JOIN @dataFile df ON df.Nit = tPrty.Nit
		LEFT OUTER JOIN Payroll.ContractType ct WITH(NOLOCK) On cntrc.ContractTypeId = ct.id
		LEFT OUTER JOIN Payroll.Position Pos WITH(NOLOCK) On Pos.id = cntrc.PositionId
		LEFT OUTER JOIN Payroll.ProfessionalRisk Pr WITH(NOLOCK) on Pr.Percentage = emply.ProfessionalRiskPercentage
		LEFT OUTER JOIN Payroll.WorkCenter wc WITH(NOLOCK) on wc.id = emply.WorkCenterId
		where cntrc.Valid = 1 and cntrc.Status = 1
		) df where [@dataFile].Nit = df.Nit
		

		----BLOQUE PARA INSERTAR

		--Se invalidan los contratos de los empleados que se van a insertar.
		UPDATE cntrc
		SET 
			cntrc.Valid = 0,
			cntrc.[Status] = 4
		FROM Payroll.[Contract] cntrc
		JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.Id = cntrc.EmployeeId
		JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Id = emply.ThirdPartyId
		JOIN @dataFile df ON df.Nit = tPrty.Nit
		WHERE cntrc.[Status] = 1 AND cntrc.Valid = 1 AND df.accion = 'INSERT'

		INSERT INTO Payroll.[Contract]( [RowType], [InitialContractNumber], [EmployeeId], [PositionId], [FunctionalUnitId], [ContractTypeId], [JobBondingDate], [ContractInitialDate], [ContractEndingDate], [BasicSalary], [Status], 	[PaymentPeriod], [PaymentType],
			[TrialPeriod], [ContractCreationDate], [CreationUserId], [ModificationDate], [TypeOfPensionContribution], [Valid], [GroupId], [BankId], [BankAccountNumber], [BankAccountType], [LiquidationPayroll], [HoursDaily],	[ModificationUserId],
			[LastModificationDate],	[Contingency]
		)
		SELECT
			2 AS [RowType],
			df.InitialContractNumber AS [InitialContractNumber], --Se debe poner el numero anterior del contrato
			emply.Id AS [EmployeeId],
			pstn.Id [PositionId],
			fnt.Id AS [FunctionalUnitId],
			cntrcTp.Id AS [ContractTypeId],
			df.JobBondingDate AS [JobBondingDate],
			df.[ContractInitialDate],
			df.[ContractEndingDate],
			df.[BasicSalary],
			1 AS [Status],
			df.[PaymentPeriod],
			df.[PaymentType],
			0 AS [TrialPeriod],
			[Common].[GETDATE]() AS [ContractCreationDate],
			@Usuario  AS [CreationUserId], 
			[Common].[GETDATE]() AS [ModificationDate],
			0 as [TypeOfPensionContribution],
			1 AS [Valid],
			grp.Id AS [GroupId],
			bnk.Id AS [BankId],
			df.[BankAccountNumber],
			df.[BankAccountType],
			1 AS [LiquidationPayroll],
			df.[HoursDaily],
			@Usuario AS [ModificationUserId], 
			[Common].[GETDATE]() AS [LastModificationDate],
			CASE df.Contingency
				WHEN 'NINGUNO' THEN 0
				WHEN 'LICENCIAS' THEN 1
				WHEN 'VACACIONES' THEN 2
				WHEN 'INCAPACIDADES' THEN 3
				ELSE 0 END AS [Contingency]
		FROM @dataFile df
		JOIN Common.ThirdParty tPrty WITH(NOLOCK) ON tPrty.Nit = df.Nit
		JOIN Payroll.Employee emply WITH(NOLOCK) ON emply.ThirdPartyId = tPrty.Id
		JOIN Payroll.Position pstn WITH(NOLOCK) ON pstn.Code = df.PositionCode
		JOIN Payroll.FunctionalUnit fnt WITH(NOLOCK) ON fnt.Code = df.FunctionalUnitCode
		JOIN Payroll.ContractType cntrcTp WITH(NOLOCK) ON cntrcTp.Code = df.ContractTypeCode
		JOIN Payroll.[Group] grp WITH(NOLOCK) ON grp.Code = df.GroupCode
		JOIN Payroll.Bank bnk WITH(NOLOCK) ON bnk.Code = df.BankCode
		WHERE df.accion = 'INSERT'

		--BLOQUE PARA ACTUALIZAR LA TABLA Contract y Employee

		UPDATE Payroll.[Contract] Set BasicSalary = df.BasicSalary, ContractTypeId = ct.id, PositionId = Pos.id, ModificationDate = [Common].[GETDATE]() ,LastModificationDate = [Common].[GETDATE](), ModificationUserId = @Usuario
		FROM @dataFile df
		INNER JOIN Common.ThirdParty Tp WITH(NOLOCK) On Tp.Nit = df.nit
		INNER JOIN Payroll.Employee emply WITH(NOLOCK) On Tp.Id = emply.ThirdPartyId
		INNER JOIN Payroll.ContractType ct WITH(NOLOCK) On df.ContractTypeCode = ct.code
		INNER JOIN Payroll.Position Pos WITH(NOLOCK) On Pos.Code = df.PositionCode
		WHERE df.accion = 'UPDATE' And Payroll.[Contract].EmployeeId = emply.id and Payroll.[Contract].Valid = 1 and Payroll.[Contract].Status = 1

		--Actualizamos codigo de riesgo del empleado.
		UPDATE Payroll.Employee Set ProfessionalRiskPercentage = Pr.[Percentage], DateModified = [Common].[GETDATE](), UserModified = @Usuario
		FROM @dataFile df
		INNER JOIN Common.ThirdParty Tp WITH(NOLOCK) On Tp.Nit = df.nit
		INNER JOIN Payroll.ProfessionalRisk Pr WITH(NOLOCK) On Pr.Code = REPLACE (str( df.ProfessionalRiskPercentage,3),SPACE(1),'0')
		WHERE df.accion = 'UPDATE' and Payroll.Employee.ThirdPartyId = Tp.Id --Solo hay un empleado por Nit

		UPDATE Payroll.Employee Set WorkCenterId = ISNULL(wc.id,WorkCenterId), DateModified = [Common].[GETDATE](), UserModified = @Usuario
		FROM @dataFile df
		INNER JOIN Common.ThirdParty Tp WITH(NOLOCK) On Tp.Nit = df.nit
		LEFT OUTER JOIN Payroll.WorkCenter wc WITH(NOLOCK) on wc.Code = df.Workcenter
		WHERE df.accion = 'UPDATE' and Payroll.Employee.ThirdPartyId = Tp.Id and df.Relocation<>0--Solo hay un empleado por Nit 

		if @trancount = 0
		COMMIT TRAN [tran1]
	END TRY
	BEGIN CATCH
	--print 'hola'
	select ERROR_NUMBER(),  ERROR_MESSAGE(), XACT_STATE();
       
        if XACT_STATE() = -1
            rollback;
        if XACT_STATE() = 1 
            rollback
        if XACT_STATE() = 1 
            rollback transaction [tran1];
			 
		--ROLLBACK TRAN [tran1]
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de nómina que procesa masivamente extensiones y modificaciones de contratos laborales a partir de un archivo XML con múltiples registros de empleados. Recibe los datos del contrato nuevo (NIT del empleado, tipo de contrato, cargo, salario básico, fechas de inicio y fin, período y forma de pago, afiliaciones a salud, pensión, ARL y caja de compensación, centro de trabajo y datos bancarios), los compara contra los contratos vigentes en Payroll.Contract y Payroll.Employee para determinar automáticamente qué tipo de novedad aplica (cambio de salario, cambio de tipo de contrato, cambio de cargo, cambio de nivel de riesgo profesional, reubicación, entre otros), y registra la modificación contractual correspondiente. Se usa en procesos de recursos humanos para renovar, prorrogar o modificar contratos de varios trabajadores al mismo tiempo sin tener que hacerlo uno a uno.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveContractExtension';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMassiveContractExtension';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa masivamente, a partir de un XML, prórrogas o modificaciones de contratos laborales: detecta si cada fila es una nueva prórroga (INSERT) o una actualización de contrato/empleado (UPDATE) y aplica los cambios correspondientes en nómina.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe traer el nodo /Data con User y filas /Data/Row con los campos del contrato; Debe existir un Common.ThirdParty con Nit coincidente, vinculado a un Payroll.Employee y a un Payroll.Contract con Valid=1 y Status=1; Para INSERT deben existir catálogos referenciados por código: Position, FunctionalUnit, ContractType, Group, Bank; Los valores numéricos y de fecha del XML deben ser convertibles (TRY_CONVERT); fechas en formato 103 (dd/mm/yyyy); ProfessionalRiskPercentage debe existir como Code en Payroll.ProfessionalRisk tras el formato str(...,3) con ceros a la izquierda', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan empleados con un contrato activo (Contract.Valid=1 AND Contract.Status=1) emparejado por Nit del tercero; El nuevo contrato insertado preserva InitialContractNumber y JobBondingDate del contrato anterior, garantizando continuidad histórica de la vinculación; Al insertar un nuevo contrato, el contrato anterior queda invalidado con Status=4 y Valid=0 (no coexisten dos contratos activos para el mismo empleado); Las nuevas filas insertadas se marcan con RowType=2, Status=1, Valid=1, TrialPeriod=0, LiquidationPayroll=1 y TypeOfPensionContribution=0; Una prórroga (INSERT) se reconoce únicamente cuando la fecha inicial nueva es exactamente el día siguiente a la fecha final del contrato vigente; El centro de trabajo del empleado solo se reubica cuando df.Relocation<>0 (mediante ISNULL para no perder el valor previo); Las fechas del XML se interpretan en formato 103 (dd/mm/yyyy); La auditoría de creación/modificación se sella con Common.GETDATE() y el usuario provisto en el XML', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prórroga masiva de contratos; Contrato laboral; Empleado; Tercero (Nit); Tipo de contrato; Cargo; Unidad funcional; Centro de trabajo; Riesgo profesional (ARL); Grupo de nómina; Banco / cuenta bancaria; Contingencia (licencias, vacaciones, incapacidades); Reubicación laboral', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DATEDIFF(day, cntrc.ContractEndingDate, df.ContractInitialDate) = 1 → Marca la fila con accion=''INSERT'' (prórroga: el nuevo contrato empieza el día siguiente al fin del actual) else Evalúa cambios materiales (salario, tipo de contrato, cargo, riesgo, centro de trabajo); si hay diferencia marca ''UPDATE'', en caso contrario ''Otro''; si df.accion = ''INSERT'' → Invalida el contrato vigente (Valid=0, Status=4) e inserta un nuevo Payroll.Contract con RowType=2, Status=1, Valid=1, conservando InitialContractNumber y JobBondingDate del contrato anterior; si df.accion = ''UPDATE'' → Actualiza salario básico, tipo de contrato y cargo del contrato vigente; actualiza también ProfessionalRiskPercentage en Employee y, si Relocation<>0, el WorkCenterId; si CASE df.Contingency WHEN ''NINGUNO''/''LICENCIAS''/''VACACIONES''/''INCAPACIDADES'' → Mapea a 0/1/2/3 respectivamente; cualquier otro valor se guarda como 0; si @trancount = 0 → Abre y commitea su propia transacción [tran1]; si ya hay transacción activa al entrar, se acopla a la externa', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Contract; Payroll.Employee; Common.ThirdParty; Payroll.ContractType; Payroll.Position; Payroll.ProfessionalRisk; Payroll.WorkCenter; Payroll.FunctionalUnit; Payroll.Group; Payroll.Bank', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMassiveContractExtension';
-- GO
