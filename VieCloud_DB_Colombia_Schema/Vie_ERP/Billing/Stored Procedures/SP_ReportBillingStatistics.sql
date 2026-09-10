

-- ================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 15/07/2019
-- Description:	Sp que se encarga de obtener los datos para el reporte estadístico de facturación
-- ================================================================================================

CREATE PROCEDURE [Billing].[SP_ReportBillingStatistics]
--@InitialDate varchar(20),
--@EndDate varchar(20),
--@ReportType tinyint,
--@AgrupedBy tinyint,
--@DocumentType varchar(50),
--@StatusInvoice tinyint,
--@CareCenterCodes varchar(max),
--@ThirdPartyIds varchar(max),
--@HealthAdministratorIds varchar(max),
--@CareGroupIds varchar(max),
--@UserCodes varchar(max)

@XmlCriterias XML,
@XmlFilters XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATETIME
		   ,@DateEnd DATETIME
		   ,@ReportType INT
		   ,@GroupBy INT
		   ,@InvoiceType INT
		   ,@Status INT
		   ,@CareCenter INT
		   ,@CurrencyReport INT
		   ,
			------FILTROS--------
			@ThirdParty VARCHAR(MAX)
		   ,@Entity VARCHAR(MAX)
		   ,@CareGroup VARCHAR(MAX)
		   ,@Users VARCHAR(MAX)

	--Se obtienen los criterios
	SELECT
		@DateStart = t.x.value('DateStart[1]', 'DATETIME')
	   ,@DateEnd = t.x.value('DateEnd[1]', 'DATETIME')
	   ,@ReportType = t.x.value('ReportType[1]', 'int')
	   ,@GroupBy = t.x.value('GroupBy[1]', 'int')
	   ,@InvoiceType = t.x.value('InvoiceType[1]', 'int')
	   ,@Status = t.x.value('Status[1]', 'int')
	   ,@CareCenter = t.x.value('CareCenter[1]', 'int')
	   ,@CurrencyReport = t.x.value('CurrencyReport[1]', 'int')
	FROM @XmlCriterias.nodes('/Data') t (x)

	--Se obtienen los filtros
	SELECT
		@ThirdParty = t.x.value('ThirdParty[1]', 'VARCHAR(MAX)')
	   ,@Entity = t.x.value('Entity[1]', 'VARCHAR(MAX)')
	   ,@CareGroup = t.x.value('CareGroup[1]', 'VARCHAR(MAX)')
	   ,@Users = t.x.value('Users[1]', 'VARCHAR(MAX)')
	FROM @XmlFilters.nodes('/Data') t (x)

	;
	WITH users_cte AS
	(SELECT
			u.UserCode
		   ,per.Fullname
		FROM Security.[UserInt] AS u
		INNER JOIN Security.PersonInt AS per
			ON per.Id = u.IdPerson),
	admission_cte AS
	(SELECT
			ing.NUMINGRES
		   ,ing.CODCENATE
		   ,CEN.NOMCENATE
		   ,ing.IFECHAING
		   ,ing.ICAUSAING
		   ,ing.TIPOINGRE
		   ,ing.CODDIAEGR
		   ,UF.UFUCODIGO
		   ,UF.UFUDESCRI
		   ,P.IPFECNACI
		   ,P.IPTIPODOC
		   ,P.IPPRINOMB
		   ,P.IPSEGNOMB
		   ,P.IPPRIAPEL
		   ,P.IPSEGAPEL
		   ,P.IPSEXOPAC
		   ,P.IPNOMCOMP
		   ,ing.IPCODPACI
		FROM dbo.ADINGRESO AS ing
		--WITH (nolock) ON CAST(ing.NUMINGRES AS int) = F.AdmissionNumber 
		INNER JOIN dbo.ADCENATEN AS CEN
			ON CEN.CODCENATE = ing.CODCENATE
		INNER JOIN dbo.INUNIFUNC AS UF
			ON UF.UFUCODIGO = ing.UFUCODIGO
		INNER JOIN dbo.INPACIENT AS P WITH (NOLOCK)
			ON P.IPCODPACI = ing.IPCODPACI),
	health_cte AS
	(SELECT
			ea.Id
		   ,ea.Code
		   ,ea.Name
		   ,tHA.Nit
		   ,tHA.[Name] AS NameThird
		   ,tHA.Id AS IdThirdParty
		FROM Contract.HealthAdministrator AS ea
		INNER JOIN Common.ThirdParty tHA WITH (NOLOCK)
			ON tHA.Id = ea.ThirdPartyId)

	SELECT
		F.Id
	   ,ing.CODCENATE CareCenterCode
	   ,ing.NOMCENATE AS CareCenterName
	   ,ing.CODCENATE + ' - ' + ing.NOMCENATE CareCenterDescription
	   ,hc.Nit ThirdPartyNit
	   ,hc.Name ThirdPartyName
	   ,hc.Nit + ' - ' + hc.Name ThirdPartyDescription
	   ,hc.Code HealthAdministratorCode
	   ,hc.Name HealthAdministratorName
	   ,hc.Code + ' - ' + hc.Name HealthAdministratorDescription
	   ,ga.Code CareGroupCode
	   ,ga.Name CareGroupName
	   ,ga.Code + ' - ' + ga.Name CareGroupDescription
	   ,F.Status StatusInvoice
	   ,IIF(F.Status = 1, 'Facturado', 'Anulado') StatusDescription
	   ,F.DocumentType
	   ,CASE F.DocumentType
			WHEN '1' THEN 'Factura EAPB con Contrato'
			WHEN '2' THEN 'Factura EAPB Sin Contrato'
			WHEN '3' THEN 'Factura Particular'
			WHEN '4' THEN 'Factura Capitada '
			WHEN '5' THEN 'Control de Capitacion'
			WHEN '6' THEN 'Factura Basica'
			WHEN '7' THEN 'Factura de Venta de Productos'
		END AS DocumentTypeDescription
	   ,F.InvoiceNumber
	   ,F.AdmissionNumber
	   ,ing.IFECHAING AdmissionDate
	   ,CASE ing.ICAUSAING
			WHEN '1' THEN 'Heridos en Combate'
			WHEN '2' THEN 'Enfermedad Profesional'
			WHEN '3' THEN 'Enfermedad General Adulto'
			WHEN '4' THEN 'Enfermedad General Pediatria'
			WHEN '5' THEN 'Odontología'
			WHEN '6' THEN 'Accidente Transito'
			WHEN '7' THEN 'Catastrofe/Fisalud'
			WHEN '8' THEN 'Quemados'
			WHEN '9' THEN 'Maternidad'
			WHEN '10' THEN 'Accidente Laboral'
			WHEN '11' THEN 'Cirugia Programada'
		END CauseIncomeDescription
	   ,CASE ing.TIPOINGRE
			WHEN '1' THEN 'Ambulatorio'
			WHEN '2' THEN 'Hospitalario'
		END AS AdmissionTypeDescription
	   ,F.PatientCode
	   ,CASE ing.IPSEXOPAC
			WHEN '1' THEN 'Hombre'
			ELSE 'Mujer'
		END SexDescription
	   ,F.TotalInvoice
	   ,F.InvoiceDate
	   ,F.TotalInvoice TotalValue
	   ,RTRIM(LTRIM(ing.UFUCODIGO)) FunctionalUnitCode
	   ,ing.UFUDESCRI FunctionalUnitName
	   ,salida.FECALTPAC HighMedicalDate
	   ,ing.CODDIAEGR DiagnosticCode
	   ,CASE F.IsCutAccount
			WHEN 'True' THEN 'Si'
			ELSE 'No'
		END IsCutAccountDescription
	   ,F.TotalPatientSalesPrice ValueCopay
	   ,us.UserCode
	   ,us.Fullname UserName
	   ,us.UserCode + ' - ' + us.Fullname UserDescription
	   ,hc.IdThirdParty ThirdPartyId
	   ,hc.Id HealthAdministratorId
	   ,ga.Id CareGroupId
	   ,F.ThirdPartySalesValue EntityValue
	   ,ing.IPNOMCOMP PatientName
	   ,ing.IPCODPACI + ' - ' + ing.IPNOMCOMP PatientDescription
	   ,ing.IPFECNACI BirthDate
	   ,ti.NOMBRE IdentificationTypeDescription
	   ,ing.IPPRINOMB FirstName
	   ,ing.IPSEGNOMB SecondName
	   ,ing.IPPRIAPEL FirstLastName
	   ,ing.IPSEGAPEL SecondLastName
	   ,(CAST(DATEDIFF(dd, ing.IPFECNACI, GETDATE()) / 365.25 AS INT)) PatientAge
	   ,F.AnnulmentUser + ISNULL(' - ' + usAn.Fullname, '') AnnulmentUser
	   ,F.AnnulmentDate
	   ,brr.Code + ' - ' + brr.Name ReversalReasonDescription
	   ,F.CurrencyId
	   ,c.Abbreviation
	FROM Billing.Invoice AS F WITH (NOLOCK)
	JOIN Common.Currency c ON f.CurrencyId = c.Id
	INNER JOIN users_cte us ON us.UserCode = F.InvoicedUser
	INNER JOIN Common.ThirdParty AS t WITH (NOLOCK) ON t.Id = F.ThirdPartyId
	LEFT JOIN admission_cte ing ON ing.NUMINGRES = F.AdmissionNumber
	LEFT JOIN Contract.CareGroup AS ga WITH (NOLOCK) ON ga.Id = F.CareGroupId
	LEFT JOIN health_cte hc ON hc.Id = F.HealthAdministratorId
	LEFT JOIN Billing.BillingReversalReason brr WITH (NOLOCK) ON brr.Id = F.ReversalReasonId
	LEFT JOIN dbo.HCREGEGRE AS salida WITH (NOLOCK) ON salida.NUMINGRES = F.AdmissionNumber AND salida.IPCODPACI = F.PatientCode
	LEFT JOIN users_cte usAn ON usAn.UserCode = F.AnnulmentUser
	LEFT JOIN ADTIPOIDENTIFICA ti WITH (NOLOCK) ON ti.CODIGO = ing.IPTIPODOC

----Filtro para el where
--declare @Filter varchar(max) = ''

----String del sql generado para ejecutar
--declare @SqlExecute nvarchar(max) = ''

----Tabla de resultado
--declare @TableResult table(DescriptionText varchar(500), TotalValue decimal(18, 2), ValueCopay decimal(18, 2), EntityValue decimal(18, 2), AdmissionNumber varchar(20), InvoiceNumber varchar(20),
--InvoiceDate date, PatientCode varchar(20), CareCenterCode varchar(20), CareCenterName varchar(500), ThirdPartyNit varchar(20), ThirdPartyName varchar(500), HealthAdministratorCode varchar(20), 
--HealthAdministratorName varchar(500), CareGroupCode varchar(20), CareGroupName varchar(500), StatusDescription varchar(20), DocumentTypeDescription varchar(50), 
--AdmissionDate date, CauseIncomeDescription varchar(50), AdmissionTypeDescription varchar(50), SexDescription varchar(20), TotalInvoice decimal(18, 2), FunctionalUnitCode varchar(20), 
--FunctionalUnitName varchar(500), UserCode varchar(20), UserName varchar(500), PatientDescription varchar(500), UserDescription varchar(500), CareCenterDescription varchar(500))

----Se establece el filtro empezando con los campos obligatorios
--set @Filter = 'cast(InvoiceDate as date) >= ''' + @InitialDate + ''' and cast(InvoiceDate as date) <= ''' + @EndDate + ''' and DocumentType in (' + @DocumentType + ')'

----Si viene el estado y se diferente a 'Todos'
--if ISNULL(@StatusInvoice, 0) > 0 and @StatusInvoice <> 3
--begin
--	set @Filter += ' and StatusInvoice = ' + cast(@StatusInvoice as varchar(5))
--end

----Si vienen los centros de atención
--if @CareCenterCodes <> ''
--begin
--	set @Filter += ' and CareCenterCode in (' + @CareCenterCodes + ')'
--end

----Si viene los terceros
--if @ThirdPartyIds <> ''
--begin
--	set @Filter += ' and ThirdPartyId in (' + @ThirdPartyIds + ')'
--end

----Si viene las entidades
--if @HealthAdministratorIds <> ''
--begin
--	set @Filter += ' and HealthAdministratorId in (' + @HealthAdministratorIds + ')'
--end

----Si viene los grupos de atención
--if @CareGroupIds <> ''
--begin
--	set @Filter += ' and CareGroupId in (' + @CareGroupIds + ')'
--end

----Si viene los usuarios
--if @UserCodes <> ''
--begin
--	set @Filter += ' and UserCode in (' + @UserCodes + ')'
--end

--if @ReportType = 1 --Si el tipo de reporte es resumido
--begin
--	if @AgrupedBy = 1 --Si el modo de agrupación es por tercero
--	begin
--		set @SqlExecute = 'select v.ThirdPartyNit + '' - '' +  v.ThirdPartyName DescriptionText,
--		SUM(v.TotalValue) TotalValue, SUM(v.ValueCopay) ValueCopay, SUM(v.EntityValue) EntityValue,
--		'''', '''', [Common].[GETDATE](), '''',
--		'''', '''', '''', '''', '''', '''', '''', '''', '''', '''', [Common].[GETDATE](), '''', '''', '''', 0, 
--		'''', '''', '''', '''', '''', '''', ''''
--		from Billing.ViewBillingStatistics v WITH(NOLOCK)
--		where ' + @Filter + '
--		group by v.ThirdPartyId, v.ThirdPartyNit, v.ThirdPartyName'
--	end
--	else if @AgrupedBy = 2 --Si el modo de agrupación es por entidad
--	begin
--		set @SqlExecute = 'select v.HealthAdministratorCode + '' - '' + v.HealthAdministratorName DescriptionText,
--		SUM(v.TotalValue) TotalValue, SUM(v.ValueCopay) ValueCopay, SUM(v.EntityValue) EntityValue,
--		'''', '''', [Common].[GETDATE](), '''',
--		'''', '''', '''', '''', '''', '''', '''', '''', '''', '''', [Common].[GETDATE](), '''', '''', '''', 0, 
--		'''', '''', '''', '''', '''', '''', ''''
--		from Billing.ViewBillingStatistics v WITH(NOLOCK)
--		where ' + @Filter + '
--		group by v.HealthAdministratorId, v.HealthAdministratorCode, v.HealthAdministratorName'
--	end
--	else if @AgrupedBy = 3 --Si el modo de agrupación es por grupo atención
--	begin
--		set @SqlExecute = 'select v.CareGroupCode + '' - '' + v.CareGroupName DescriptionText,
--		SUM(v.TotalValue) TotalValue, SUM(v.ValueCopay) ValueCopay, SUM(v.EntityValue) EntityValue,
--		'''', '''', [Common].[GETDATE](), '''',
--		'''', '''', '''', '''', '''', '''', '''', '''', '''', '''', [Common].[GETDATE](), '''', '''', '''', 0, 
--		'''', '''', '''', '''', '''', '''', ''''
--		from Billing.ViewBillingStatistics v WITH(NOLOCK)
--		where ' + @Filter + '
--		group by v.CareGroupId, v.CareGroupCode, v.CareGroupName'
--	end
--	else if @AgrupedBy = 4 --Si el modo de agrupación es por usuario
--	begin
--		set @SqlExecute = 'select v.UserCode + '' - '' + v.UserName DescriptionText,
--		SUM(v.TotalValue) TotalValue, SUM(v.ValueCopay) ValueCopay, SUM(v.EntityValue) EntityValue,
--		'''', '''', [Common].[GETDATE](), '''',
--		'''', '''', '''', '''', '''', '''', '''', '''', '''', '''', [Common].[GETDATE](), '''', '''', '''', 0, 
--		'''', '''', '''', '''', '''', '''', ''''
--		from Billing.ViewBillingStatistics v WITH(NOLOCK)
--		where ' + @Filter + '
--		group by v.UserCode, v.UserName'
--	end
--end
--else begin --Si el tipo de reporte es detallado
--	set @SqlExecute = 'select IIF(' + cast(@AgrupedBy as varchar(5)) + ' = 1, v.ThirdPartyNit + '' - '' + v.ThirdPartyName, IIF(' + cast(@AgrupedBy as varchar(5)) + ' = 2, v.HealthAdministratorCode + '' - '' + v.HealthAdministratorName, IIF(' + cast(@AgrupedBy as varchar(5)) + ' = 4, v.UserCode + '' - '' + v.UserName, v.CareGroupCode + '' - '' + v.CareGroupName))) DescriptionText,
--	v.TotalValue, v.ValueCopay, v.EntityValue, 
--	v.AdmissionNumber, v.InvoiceNumber, v.InvoiceDate, v.PatientCode,
--	v.CareCenterCode, v.CareCenterName, v.ThirdPartyNit, v.ThirdPartyName, v.HealthAdministratorCode, v.HealthAdministratorName, v.CareGroupCode, v.CareGroupName, v.StatusDescription,
--	v.DocumentTypeDescription, v.AdmissionDate, v.CauseIncomeDescription, v.AdmissionTypeDescription, v.SexDescription, v.TotalInvoice,
--	v.FunctionalUnitCode, v.FunctionalUnitName, v.UserCode, v.UserName, v.PatientCode + '' - '' + v.PatientName, v.UserCode + '' - '' + v.UserName, v.CareCenterCode + '' - '' + v.CareCenterName
--	from Billing.ViewBillingStatistics v WITH(NOLOCK)
--	where ' + @Filter
--end

--print @SqlExecute

----Se insertan los datos a la tabla
--insert into @TableResult
--exec sp_sqlexec @SqlExecute	

----Se retorna la tabla
--select * from @TableResult

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte estadístico de facturación del sistema. Recibe criterios de búsqueda y filtros en formato XML (rango de fechas, tipo de reporte, tipo de factura, estado, centro de atención, moneda, terceros, entidades, grupos de atención y usuarios) y consolida información de facturas con datos del ingreso o admisión del paciente, centro de atención, unidad funcional, datos demográficos del paciente (nombre completo, documento de identidad, fecha de nacimiento, sexo, edad calculada), entidad administradora de salud (EPS/EAPB) y contrato asociado. El resultado es un conjunto de filas detalladas que alimentan reportes gerenciales y operativos de facturación, permitiendo analizar montos facturados, tipos de documento, estados de factura (vigente/anulada), causa y tipo de ingreso, copagos, usuario facturador y motivos de anulación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBillingStatistics';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBillingStatistics';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el conjunto detallado de facturas con datos de paciente, admisión, centro de atención, tercero, administradora de salud, grupo de atención, usuario facturador y anulador, para alimentar el reporte estadístico de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los XML de entrada deben exponer un nodo /Data con los criterios (DateStart, DateEnd, ReportType, GroupBy, InvoiceType, Status, CareCenter, CurrencyReport) y los filtros (ThirdParty, Entity, CareGroup, Users); Las facturas en Billing.Invoice deben tener InvoicedUser correspondiente a un UserCode existente en Security.UserInt (de lo contrario se excluyen del reporte por el INNER JOIN); Cada factura debe referenciar un ThirdPartyId existente y una moneda válida en Common.Currency', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan facturas que tienen un usuario facturador válido existente en Security.UserInt (INNER JOIN obligatorio sobre InvoicedUser); Toda factura reportada debe tener un Tercero válido en Common.ThirdParty (INNER JOIN sobre F.ThirdPartyId); Toda factura reportada debe tener una moneda válida en Common.Currency (INNER JOIN); El estado de factura se interpreta como binario en el reporte: 1 = Facturado, cualquier otro valor = Anulado; La edad del paciente se calcula como DATEDIFF(días entre fecha de nacimiento y fecha actual) / 365.25 truncado a entero; El usuario de anulación se concatena con su nombre completo solo si existe correspondencia en Security.UserInt; si no, se muestra solo el código; El reporte expone TotalInvoice también como TotalValue (mismo valor bajo dos alias); La información de admisión, grupo de atención, administradora de salud, motivo de reversión, egreso y tipo de identificación es opcional (LEFT JOIN): la factura se reporta aunque falten estos datos', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Anulación de factura; Tipo de documento de facturación; EAPB / Administradora de salud; Capitación; Ingreso/Admisión del paciente; Causa de ingreso; Tipo de ingreso (ambulatorio/hospitalario); Centro de atención; Unidad funcional; Diagnóstico de egreso; Egreso hospitalario / alta médica; Paciente; Copago / valor del paciente; Cuenta de corte; Grupo de atención (CareGroup); Tercero; Motivo de reversión/anulación; Moneda; Usuario facturador / usuario anulador', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.Invoice: Retorna un resultset con una fila por factura (Billing.Invoice) enriquecida con descripciones de catálogos (tipo de documento, estado, causa/tipo de ingreso, sexo, cuenta de corte) y datos relacionados de admisión, paciente, unidad funcional, egreso, tercero, EAPB, grupo de atención y motivo de reversión', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si F.Status = 1 → Se etiqueta la factura como ''Facturado'' else Se etiqueta como ''Anulado''; si F.DocumentType en {1..7} → Se traduce el tipo de documento a su descripción de negocio (Factura EAPB con/sin Contrato, Particular, Capitada, Control de Capitación, Básica, Venta de Productos); si ing.ICAUSAING en {1..11} → Se traduce la causa de ingreso (Heridos en Combate, Enfermedad Profesional, Enfermedad General Adulto/Pediatría, Odontología, Accidente Tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada); si ing.TIPOINGRE = 1 → Tipo de admisión ''Ambulatorio'' else ''Hospitalario'' cuando = 2; si ing.IPSEXOPAC = 1 → Sexo ''Hombre'' else ''Mujer'' en cualquier otro valor; si F.IsCutAccount = ''True'' → Marca cuenta de corte como ''Si'' else ''No''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Security.UserInt; Security.PersonInt; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; Contract.HealthAdministrator; Common.ThirdParty; Billing.Invoice; Common.Currency; Contract.CareGroup; Billing.BillingReversalReason; dbo.HCREGEGRE; dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBillingStatistics';
-- GO
