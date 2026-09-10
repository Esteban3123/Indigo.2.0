
CREATE PROCEDURE [Glosas].[SP_GenerateUSFileData] 
	@RadicateInvoiceId AS INT,
	@XmlInvoices AS XML
AS
BEGIN
	SET NOCOUNT ON;
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Invoices TABLE
	(
		InvoiceId INT
	)

	--Tabla para devolver los resultados
	DECLARE @TableResult TABLE
	(
		IdentificationTypeCode VARCHAR(50), 
		IdentificationNumber VARCHAR(50), 
		HealthEntityCode VARCHAR(50), 
		UserType INT, 
		FirstLastName VARCHAR(50), 
		SecondLastName VARCHAR(50), 
		FirstName VARCHAR(50), 
		SecondName VARCHAR(50), 
		Age INT, 
		UnitMeasureAge INT, 
		Gender VARCHAR(50), 
		Department VARCHAR(50), 
		City VARCHAR(50), 
		ResidentialZone VARCHAR(50),
		Nationality VARCHAR(50),
		GeographicLocation VARCHAR(50),
		TypeUser VARCHAR(50)
	)

	BEGIN TRY
		
		INSERT INTO @Invoices (InvoiceId)

			SELECT DISTINCT
				i.Id AS InvoiceId
			FROM @XmlInvoices.nodes('/Data') t(x)
			JOIN Billing.Invoice i ON t.x.value('InvoiceId[1]','int') = i.Id
			WHERE i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT i.Id
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType <> 4
		UNION ALL
			SELECT DISTINCT cc.Id
			FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
			JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
			JOIN Billing.Invoice i WITH (NOLOCK) ON rid.InvoiceNumber = i.InvoiceNumber
			JOIN Billing.Invoice cc WITH (NOLOCK) ON i.ThirdPartyId = cc.ThirdPartyId
				AND i.CareGroupId = cc.CareGroupId
				AND i.InvoiceCategoryId = cc.InvoiceCategoryId
				AND CAST(cc.InvoiceDate AS DATE) BETWEEN i.CapitationInitialDate AND i.CapitationEndDate
			WHERE ri.Id = @RadicateInvoiceId
				AND rid.State <> 4
				AND i.DocumentType = 4
				AND cc.DocumentType = 5

		/*******************************************************************************************/
		
		INSERT INTO @TableResult
			 SELECT DISTINCT 
				TIP.SIGLA AS IdentificationTypeCode,
				LTRIM(RTRIM(pacient.IPCODPACI)) AS IdentificationNumber, 
				i.HealthEntityCode AS HealthEntityCode, 
				IPTIPOPAC AS UserType, 
				LTRIM(RTRIM(pacient.IPPRIAPEL)) AS FirstLastName, 
				LTRIM(RTRIM(pacient.IPSEGAPEL)) AS SecondLastName, 
				LTRIM(RTRIM(pacient.IPPRINOMB)) AS FirstName, 
				LTRIM(RTRIM(pacient.IPSEGNOMB)) AS SecondName, 
				gap.Age,
				gap.UnitMeasureAge, 
				CASE IPSEXOPAC 
					WHEN 1 THEN 'M' 
					WHEN 2 THEN 'F' 
					ELSE 'F' 
				END AS Gender, 
				SUBSTRING(ubication.DEPMUNCOD,0,3) AS Department, 
				SUBSTRING(ubication.DEPMUNCOD,3,5) AS City, 
				CASE ubication.TIPOUBICA 
					WHEN 2 THEN 'R' 
					ELSE 'U' 
				END AS ResidentialZone,
				nationality.code AS Nationality,
				mc.GeographicLocation AS GeographicLocation,
				[Admissions].[UserType](pacient.IPTIPOPAC, pacient.IPTIPOAFI) AS TypeUser
			 FROM 
			 (
				SELECT i.PatientCode, ha.HealthEntityCode, MAX(I.InvoiceDate) InvoiceDate
				FROM @Invoices xi
				JOIN Billing.Invoice i WITH (NOLOCK) ON i.Id = xi.InvoiceId 
				JOIN [Contract].HealthAdministrator ha WITH (NOLOCK) ON i.HealthAdministratorId = ha.ID 
				WHERE I.[Status] = 1
				GROUP BY i.PatientCode, ha.HealthEntityCode
			 ) i
			 JOIN .INPACIENT pacient WITH (NOLOCK) ON I.PatientCode = pacient.IPCODPACI 
			 JOIN dbo.ADTIPOIDENTIFICA TIP WITH (NOLOCK) ON TIP.CODIGO = pacient.IPTIPODOC
			 LEFT JOIN Common.Country nationality WITH(NOLOCK) on nationality.id = pacient.IDPAIS
			 JOIN [dbo].[INUBICACI] ubication WITH (NOLOCK) ON pacient.AUUBICACI = ubication.AUUBICACI
			 LEFT JOIN (
				SELECT 
					a.IPCODPACI as identificacion,
					P.Code AS GeographicLocation
				FROM Admissions.PatientAddress A
					JOIN INUBICACI U ON A.IdUbication = U.ID
					JOIN INMUNICIP M ON U.DEPMUNCOD = M.DEPMUNCOD
					JOIN INDEPARTA D ON M.DEPCODIGO = D.DEPCODIGO
					JOIN Common.Country P ON D.IDPAIS = P.Id
				where a.isMain = 1
			 ) mc on mc.identificacion = pacient.IPCODPACI
			 CROSS APPLY Glosas.CalculateAgePlane(pacient.IPFECNACI, i.InvoiceDate) gap
				
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH

	--Se retorna la tabla con los resultados
	SELECT * 
	FROM @TableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el archivo de datos de usuarios (archivo US) requerido para el proceso de glosas y radicación de facturas ante entidades pagadoras (EPS, aseguradoras). A partir de un identificador de radicado de cartera o de una lista de facturas enviada en formato XML, consolida las facturas activas —incluyendo facturas de capitación relacionadas— y extrae la información demográfica de cada paciente: tipo y número de cédula, nombre completo, edad, sexo, municipio y departamento de residencia, zona urbana o rural, nacionalidad, ubicación geográfica y tipo de usuario/afiliado. Combina datos de facturación (Billing.Invoice), radicación de cartera (RadicateInvoiceC y RadicateInvoiceD), historia clínica del paciente (INPACIENT), tipo de identificación, ubicación y la función de cálculo de edad Glosas.CalculateAgePlane para producir el conjunto de registros que se reporta en el archivo plano US de RIPS o soportes de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateUSFileData';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateUSFileData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el dataset de usuarios/pacientes (datos demográficos y de ubicación) asociados a las facturas de un radicado, para generar el archivo plano de usuarios exigido en el proceso de radicación a entidades de salud.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El radicado debe existir en Portfolio.RadicateInvoiceC y tener detalles en RadicateInvoiceD con State distinto de 4 (no anulado).; Las facturas referenciadas deben tener Status = 1 (activas) para ser incluidas.; El paciente debe existir en INPACIENT y tener tipo de documento válido en ADTIPOIDENTIFICA y ubicación en INUBICACI.; El XML de entrada debe seguir el esquema /Data con nodo InvoiceId.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con Status = 1.; Nunca se incluyen facturas con DocumentType = 4 directamente; se reemplazan por su factura de consumo (DocumentType = 5) dentro del rango de fechas de capitación.; Nunca se incluyen detalles de radicado anulados (State = 4).; Cada paciente aparece una sola vez por combinación PatientCode/HealthEntityCode, tomando la fecha de factura más reciente (MAX(InvoiceDate)).; Solo se considera la dirección principal del paciente (isMain = 1) para la localización geográfica.; El género nunca queda nulo: por defecto se asigna ''F'' si no es 1 ni 2.; La zona residencial nunca queda nula: por defecto ''U'' si TIPOUBICA no es 2.; Los nombres y apellidos se entregan sin espacios en blanco al inicio o final (LTRIM/RTRIM).; Los errores no propagan; el procedimiento siempre retorna un result set (posiblemente vacío).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Factura; Radicación de cuentas; Capitación; Consumo de capitación; Administradora de salud (EPS); Tipo de identificación; Tipo de usuario/afiliación; Nacionalidad; Ubicación geográfica (departamento/municipio); Zona residencial (rural/urbana); Edad y unidad de medida de edad; Género del paciente', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableResult: Inserta una fila por paciente único con sus datos demográficos, ubicación, nacionalidad y tipo de usuario, derivados de las facturas activas del radicado y/o del XML.; [RETURN_RESULT] RESULT: Devuelve el contenido de @TableResult como result set final del procedimiento.; [RAISERROR] ERROR_OUTPUT: En CATCH se imprime (PRINT) el mensaje de error y la línea; no se relanza la excepción, por lo que los errores se silencian devolviendo resultado vacío.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Factura proviene del XML y i.DocumentType <> 4 → Se incluye la factura directamente en el conjunto a procesar.; si Factura del radicado con rid.State <> 4 y i.DocumentType <> 4 → Se incluye la factura del detalle del radicado.; si Factura del radicado con i.DocumentType = 4 (capitación) y existe factura cc con DocumentType = 5 del mismo tercero, grupo de atención y categoría, cuya InvoiceDate cae entre CapitationInitialDate y CapitationEndDate → Se incluye la factura cc (consumo de capitación) en lugar de la factura cabecera.; si IPSEXOPAC = 1 → Gender=''M'' else IPSEXOPAC=2 → ''F''; cualquier otro valor también se mapea a ''F''; si ubication.TIPOUBICA = 2 → ResidentialZone=''R'' (rural) else ResidentialZone=''U'' (urbana)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Admissions.UserType; Glosas.CalculateAgePlane', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Portfolio.RadicateInvoiceC; Portfolio.RadicateInvoiceD; Contract.HealthAdministrator; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; Common.Country; dbo.INUBICACI; Admissions.PatientAddress; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateUSFileData';
-- GO
