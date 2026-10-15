
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