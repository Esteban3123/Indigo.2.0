-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que genera los datos de recién nacidos para RIPS
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInfoRecienNacidosRIPS] 
	@Parameters AS XML
AS
BEGIN
	SET NOCOUNT ON;
	
	/************************************* VARIABLES *************************************/

	--Tabla para almacenar los items del listado que viene en el xml
	DECLARE @Invoices TABLE
	(
		InvoiceId INT not null,
		InvoiceNumber VARCHAR(20) not null,
		OutputDiagnosis char(4),
		DocumentType tinyint not null,
		AdmissionNumber char(10) not null,
		[Status] tinyint not null,
		[IsCutAccount] [bit] NOT NULL,
		CutType	tinyint not null,
		InitialDate DATETIME NOT NULL,
		OutputDate  DATETIME NOT NULL,
		HasDeliveryProcedure BIT NOT NULL DEFAULT(0))

	DECLARE @DeliveryCUPSPatterns TABLE
	(
		Pattern VARCHAR(6) NOT NULL PRIMARY KEY
	)

	INSERT INTO @DeliveryCUPSPatterns(Pattern)
	VALUES ('735%'), ('740%'), ('721%'), ('725%'), ('732201')

	--Consulta de datos generales a nivel de ingreso BINOMIO MADRE HIJO
	Declare @GeneralData TABLE (InvoiceId INT,
								InvoiceNumber VARCHAR(20),
								OutputDiagnosis VARCHAR(4),
								DocumentType TINYINT,
								CODCENATE VARCHAR(10),
								CODDIAEGR VARCHAR(4),
								IdEntryRoutesHealthServices INT,
								AdmissionNumber char(10),
								AdmissionNumberChild char(10),
								FECHANACIM DATETIME,
								SEXRECNAC CHAR(1),
								IdentificationNumberChild VARCHAR(25),
								TypeIdentificationChild VARCHAR(2),
								WeightChild NUMERIC(18,3),
								EDADGESNAC VARCHAR(50),
								VITANACIM VARCHAR(50),
								NUMCONSEC INT,
								NUMHIJREG INT,
								DeathDate DATETIME,
								MedicalDischargeDate DATETIME,
								CODIPSSEC VARCHAR(13),
								RIPSCode varchar(5)
								)

	-- Consulta de la historia Ginecologica
	DECLARE @GynecologicalHistory TABLE(InvoiceId INT ,
										AdmissionNumber CHAR(10),
										AdmissionNumberChild char(10),
										NUMEFOLIO NCHAR(10),
										NOMSEMGES NUMERIC(3,1),
										CANTPRENA INT)

	--Consulta Diagnosticos asociado al Recien Nacido
	DECLARE @QueryDiagnostics TABLE(InvoiceId INT,
									AdmissionNumberChild CHAR(10),
									DiagnosticCode char(4),
									CODDIAPRI BIT)

	--Consulta Ultima Historia Recien Nacido
	DECLARE @HCHISPACA_Child TABLE(
									AdmissionNumberChild CHAR(10),
									NUMEFOLIO NCHAR(10),
									conditionDestination VARCHAR(2),
									INDICAPAC CHAR(2),
									DateHISPACA DATETIME)

	--Tabla RESULTADO
	IF OBJECT_ID('tempdb..#ResultRecienNacido') IS NOT NULL DROP TABLE #ResultRecienNacido

	CREATE TABLE #ResultRecienNacido  ( InvoiceNumber VARCHAR(20),
										codPrestador VARCHAR(13),
										tipoDocumentoIdentificacion VARCHAR(2),
										numDocumentoIdentificacion VARCHAR(25),
										fechaNacimiento VARCHAR(17),
										edadGestacional INT,
										numConsultasCPrenatal INT,
										codSexoBiologico VARCHAR(2),
										peso NUMERIC(18,3),
										codDiagnosticoPrincipal VARCHAR(10),
										condicionDestinoUsuarioEgreso VARCHAR(2),
										codDiagnosticoCausaMuerte VARCHAR(4),
										fechaEgreso VARCHAR(17),
										consecutivo INT,
										codDiagnosticoPrincipalCIE11 VARCHAR(20),
										nomCodDiagnosticoPrincipalCIE11 VARCHAR(250),
										codDiagnosticoCausaMuerteCIE11 VARCHAR(20),
										nomCodDiagnosticoCausaMuerteCIE11 VARCHAR(250),
										codigoVIDA VARCHAR(36),
										DocumentType TINYINT
										)

	BEGIN TRY

		INSERT INTO @Invoices (	InvoiceId,--------1
								InvoiceNumber,----2
								OutputDiagnosis,--3
								DocumentType,-----4
								AdmissionNumber,--5
								[Status],---------6
								IsCutAccount,-----7
								CutType,----------8
								InitialDate,------9
								OutputDate)-------10
			SELECT 
				i.Id AS InvoiceId,-----------------1
				i.InvoiceNumber AS InvoiceNumber,--2
				i.OutputDiagnosis,-----------------3
				i.DocumentType,--------------------4
				i.AdmissionNumber,-----------------5
				i.[Status],------------------------6
				i.IsCutAccount,--------------------7
				i.CutType,-------------------------8
				i.InitialDate,---------------------9
				i.OutputDate----------------------10
			FROM @Parameters.nodes('/Data/Document') t(x)
			JOIN Billing.Invoice i WITH(NOLOCK) ON t.x.value('InvoiceNumber[1]','VARCHAR(20)') = i.InvoiceNumber AND t.x.value('DocumentType[1]','TINYINT') = i.DocumentType
			where i.[Status] =1
			group by i.Id,
				i.InvoiceNumber,
				i.OutputDiagnosis,
				i.DocumentType,
				i.AdmissionNumber,
				i.[Status],
				i.IsCutAccount,
				i.CutType,
				i.InitialDate,
				i.OutputDate

		UPDATE i SET HasDeliveryProcedure = 1
		FROM @Invoices i
		WHERE EXISTS (	SELECT 1
						FROM Billing.InvoiceDetail id WITH(NOLOCK)
						JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
						JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
						JOIN @DeliveryCUPSPatterns cupsPattern ON LTRIM(RTRIM(ce.Code)) LIKE cupsPattern.Pattern
						WHERE id.InvoiceId = i.InvoiceId
							AND sod.IsDelete = 0)

		/*******************************************************************************************/

		INSERT INTO @GeneralData(InvoiceId,--------------------1
								InvoiceNumber,-----------------2
								OutputDiagnosis,---------------3
								DocumentType,------------------4
								CODCENATE,---------------------5
								CODDIAEGR,---------------------6
								IdEntryRoutesHealthServices,---7
								AdmissionNumber,---------------8
								AdmissionNumberChild,----------9
								FECHANACIM,-------------------10
								SEXRECNAC,--------------------11
								IdentificationNumberChild,----12
								TypeIdentificationChild,------13
								WeightChild,------------------14
								EDADGESNAC,------------------15
								VITANACIM,--------------------16
								NUMCONSEC,--------------------17
								NUMHIJREG,--------------------18
								DeathDate,--------------------19
								MedicalDischargeDate,---------20
								CODIPSSEC,--------------------21
								RIPSCode---------------------22
								)
		SELECT	i.InvoiceId,------------------------------------1
				i.InvoiceNumber,--------------------------------2
				i.OutputDiagnosis,------------------------------3
				i.DocumentType,---------------------------------4
				ad.CODCENATE,-----------------------------------5
				ad.CODDIAEGR,-----------------------------------6
				ad.IdEntryRoutesHealthServices,-----------------7
				ad.NUMINGRES,-----------------------------------8
				RECINAC.NUMINGRESHIJO,--------------------------9
				RECINAC.FECHANACIM,-----------------------------10
				RECINAC.SEXRECNAC,------------------------------11
				CASE
					WHEN UPPER(LTRIM(RTRIM(RECINAC.VITANACIM))) = 'MUERTO' AND RECINAC.IPCODPACIHIJO IS NULL
						THEN CONCAT(COALESCE(NULLIF(LTRIM(RTRIM(RECINAC.IPCODPACI)), ''), LTRIM(RTRIM(ad.NUMINGRES))), RECINAC.NUMHIJREG)
					ELSE pacient.IPCODPACI
				END IdentificationNumberChild,------------------12
				COALESCE(tip.SIGLA, CASE WHEN UPPER(LTRIM(RTRIM(RECINAC.VITANACIM))) = 'MUERTO' THEN 'MS' END) TypeIdentificationChild,--13
				RECINAC.PESORECNA WeightChild,------------------14
				CONVERT(VARCHAR(50), RECINAC.EDADGESNAC),-------15
				UPPER(LTRIM(RTRIM(RECINAC.VITANACIM))),---------16
				RECINAC.NUMCONSEC,------------------------------17
				RECINAC.NUMHIJREG,------------------------------18
				RECINAC.FECMUEPAC,------------------------------19
				HCEGRE.FECALTPAC AS MedicalDischargeDate,-------20
				LTRIM(RTRIM(ca.CODIPSSEC)) as CODIPSSEC,--------21
				erhs.RIPSCode-----------------------------------22
		FROM @Invoices i
		JOIN [dbo].ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = i.AdmissionNumber
		JOIN [dbo].HCRECINAC RECINAC WITH (NOLOCK) ON RECINAC.NUMINGRES = ad.NUMINGRES
		LEFT JOIN [dbo].ADINGRESO adChild WITH(NOLOCK) ON RECINAC.NUMINGRESHIJO = adChild.NUMINGRES
		LEFT JOIN [dbo].[INPACIENT] pacient WITH(NOLOCK) ON RECINAC.IPCODPACIHIJO= pacient.IPCODPACI
		LEFT JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON tip.CODIGO = pacient.IPTIPODOC
		JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON ca.CODCENATE = ad.CODCENATE
		LEFT JOIN [dbo].EntryRoutesHealthServices erhs WITH (NOLOCK) on adChild.IdEntryRoutesHealthServices = erhs.Id
		LEFT JOIN HCREGEGRE HCEGRE WITH (NOLOCK) ON HCEGRE.NUMINGRES = RECINAC.NUMINGRESHIJO 
		WHERE UPPER(LTRIM(RTRIM(RECINAC.VITANACIM))) = 'MUERTO'
			OR (
				RECINAC.NUMINGRESHIJO IS NOT NULL
				AND RECINAC.IPCODPACIHIJO IS NOT NULL
				AND pacient.IPCODPACI IS NOT NULL
				AND tip.SIGLA IS NOT NULL
			)

		;

		/*******************************************************************************************/
		WITH cte_Gyn as (SELECT NUMINGRES, 
								MAX(NUMEFOLIO) AS NUMEFOLIO,
								gd.InvoiceId,
								gd.AdmissionNumberChild
							FROM dbo.HCANTGINE hcan WITH (NOLOCK) 
							JOIN @GeneralData gd ON hcan.NUMINGRES = gd.AdmissionNumber
							GROUP BY NUMINGRES,	gd.InvoiceId,gd.AdmissionNumberChild )

		INSERT INTO @GynecologicalHistory(InvoiceId,AdmissionNumber,AdmissionNumberChild,NUMEFOLIO,NOMSEMGES,CANTPRENA)
		SELECT	gyn.InvoiceId,
				gyn.NUMINGRES,
				gyn.AdmissionNumberChild,
				gyn.NUMEFOLIO,
				hcan.NOMSEMGES,
				hcan.CANTPRENA
		from cte_Gyn gyn WITH(NOLOCK)
		JOIN dbo.HCANTGINE hcan WITH (NOLOCK) 
			on gyn.NUMINGRES = hcan.NUMINGRES and hcan.NUMEFOLIO = gyn.NUMEFOLIO;

		/*******************************************************************************************/
		WITH CTE_Diagno as (
			SELECT ROW_NUMBER() OVER (PARTITION BY gd.InvoiceId, 
													gd.AdmissionNumberChild,
													idp.CODDIAPRI
										ORDER BY
										CASE idp.DIAINGEGR 
											WHEN 'E' THEN 1 
											WHEN 'A' THEN 2 
											ELSE 3 
										END ASC,
										idp.FECDIAGNO DESC) AS IdPartition,
					gd.InvoiceId,
					gd.AdmissionNumberChild,
					idp.CODDIAGNO,
					idp.CODDIAPRI
			FROM @GeneralData gd 
			JOIN [dbo].INDIAGNOP idp WITH(NOLOCK) 
				ON gd.AdmissionNumberChild = idp.NUMINGRES 
					and idp.CODDIAPRI=1
			UNION ALL
			SELECT ROW_NUMBER() OVER (PARTITION BY gd.InvoiceId,
													gd.AdmissionNumberChild
										ORDER BY
										CASE
											WHEN normalizedDiag.CODDIAGNO NOT LIKE 'Z37%' THEN 1
											ELSE 2
										END ASC,
										hc.NUMCONSEC ASC) AS IdPartition,
					gd.InvoiceId,
					gd.AdmissionNumberChild,
					normalizedDiag.CODDIAGNO,
					1 AS CODDIAPRI
			FROM @GeneralData gd
			JOIN [dbo].HCRECNADI hc WITH(NOLOCK) ON gd.NUMCONSEC = hc.CONSECREC
			CROSS APPLY (
				SELECT TOP 1 diag.CODDIAGNO
				FROM (VALUES
						(LTRIM(RTRIM(hc.CODDIAGNO)), 1),
						(CASE WHEN RIGHT(LTRIM(RTRIM(hc.CODDIAGNO)), 1) = 'X'
							THEN LEFT(LTRIM(RTRIM(hc.CODDIAGNO)), 3)
							ELSE NULL END, 2)
					) candidate(CODDIAGNO, Priority)
				JOIN dbo.INDIAGNOS diag WITH(NOLOCK) ON diag.CODDIAGNO = candidate.CODDIAGNO
				WHERE candidate.CODDIAGNO IS NOT NULL
					AND ISNULL(diag.ESTADO, 1) = 1
				ORDER BY candidate.Priority
			) normalizedDiag
			WHERE gd.VITANACIM = 'MUERTO'
				AND gd.AdmissionNumberChild IS NULL)

		/*******************************************************************************************/

		INSERT INTO @QueryDiagnostics
		select	df.InvoiceId,
				df.AdmissionNumberChild,
				df.CODDIAGNO,
				df.CODDIAPRI
			from (select InvoiceId,
						 AdmissionNumberChild,
						 MIN(d.IdPartition) IdPartition
					from CTE_Diagno d
					GROUP by d.InvoiceId,d.AdmissionNumberChild) as t1
			JOIN CTE_Diagno df
				on t1.InvoiceId = df.InvoiceId
					AND (t1.AdmissionNumberChild = df.AdmissionNumberChild
						OR (t1.AdmissionNumberChild IS NULL AND df.AdmissionNumberChild IS NULL))
					AND t1.IdPartition = df.IdPartition;

		/*******************************************************************************************/
		WITH CTE_HisP AS (SELECT 
								gd.AdmissionNumberChild,
								gd.RIPSCode,
								hc.ID,
								hc.INDICAPAC,
								hc.NUMEFOLIO,
								hc.FECHISPAC
							FROM @GeneralData gd
							JOIN dbo.HCHISPACA hc WITH(NOLOCK)
								ON hc.NUMINGRES = gd.AdmissionNumberChild 
									and INDICAPAC IN ('3','4','5','6','8','9','10','11','12','15','16','19','20','21')
							GROUP by
								gd.AdmissionNumberChild,
								gd.RIPSCode,
								hc.ID,
								hc.INDICAPAC,
								hc.NUMEFOLIO,
								hc.FECHISPAC)
		
		INSERT INTO @HCHISPACA_Child(
										AdmissionNumberChild,
										NUMEFOLIO,
										conditionDestination,
										INDICAPAC,
										DateHISPACA)
		SELECT	
				hc.AdmissionNumberChild,
				hc.NUMEFOLIO,
				case 
					when hc.INDICAPAC in('3','4','5','6','8','19','20','21') then '03'
					when hc.INDICAPAC in( '12', '15', '16') then '01'
					when hc.INDICAPAC = '11' then '02' 
					when hc.INDICAPAC = '10' and hc.RIPSCode = 13 then '05'
					when hc.INDICAPAC = '10' then '04'
					when hc.INDICAPAC = '9' then '06'
				end conditionDestination,
				hc.INDICAPAC,
				hc.FECHISPAC
		FROM (	SELECT	hc.*,
						ROW_NUMBER() OVER(PARTITION BY hc.AdmissionNumberChild
											ORDER BY hc.FECHISPAC DESC, hc.ID DESC) AS RowNumber
				FROM CTE_HisP hc WITH(NOLOCK)) hc
		WHERE hc.RowNumber = 1
		GROUP by 
			hc.AdmissionNumberChild,
			hc.NUMEFOLIO,
			hc.INDICAPAC,
			hc.FECHISPAC,
			hc.RIPSCode
		;

		/*******************************************************************************************/

		WITH Cte_General_Info AS (
								SELECT	gd.*,
										COALESCE(his.DateHISPACA,gd.MedicalDischargeDate) as RealMedicalDischargeDate,
										his.conditionDestination
								FROM @GeneralData gd
								LEFT JOIN @HCHISPACA_Child his on his.AdmissionNumberChild=gd.AdmissionNumberChild)

		INSERT INTO #ResultRecienNacido(	InvoiceNumber,------------------1
											codPrestador,-------------------2
											tipoDocumentoIdentificacion,----3
											numDocumentoIdentificacion,-----4
											fechaNacimiento,----------------5
											edadGestacional,----------------6
											numConsultasCPrenatal,----------7
											codSexoBiologico,---------------8
											peso,---------------------------9
											codDiagnosticoPrincipal,--------10
											condicionDestinoUsuarioEgreso,--11
											codDiagnosticoCausaMuerte,------12
											fechaEgreso,--------------------13
											consecutivo,--------------------14
											codDiagnosticoPrincipalCIE11,
											nomCodDiagnosticoPrincipalCIE11,
											codDiagnosticoCausaMuerteCIE11,
											nomCodDiagnosticoCausaMuerteCIE11,
											codigoVIDA,
											DocumentType--------------------15
										)
		SELECT
				gd.InvoiceNumber AS InvoiceNumber,---------------------------------------------1
				LTRIM(RTRIM(gd.CODIPSSEC))AS codPrestador,-------------------------------------2
				gd.TypeIdentificationChild tipoDocumentoIdentificacion,------------------------3
				LTRIM(RTRIM(gd.IdentificationNumberChild)) numDocumentoIdentificacion,---------4
				FORMAT(gd.FECHANACIM, 'yyyy-MM-dd HH:mm') fechaNacimiento,---------------------5
				CAST(COALESCE(
						CASE
							WHEN gestationalAge.EDADGESNAC BETWEEN 20.0 AND 40.0
								THEN gestationalAge.EDADGESNAC
						END,
						CASE
							WHEN gestationalAge.NOMSEMGES BETWEEN 20.0 AND 40.0
								THEN gestationalAge.NOMSEMGES
						END,
						38.0
					) AS INT) edadGestacional,-------------------------------------6
				isnull(gh.CANTPRENA, 00 ) numConsultasCPrenatal,-------------------------------7
				CASE gd.SEXRECNAC 
					WHEN 1 THEN '01' 
					WHEN 2 THEN '02' 
					ELSE '03'
				END codSexoBiologico,-----------------------------------------------------------8
				gd.WeightChild peso,------------------------------------------------------------9
				COALESCE(NULLIF(LTRIM(RTRIM(qd.DiagnosticCode)), ''),
						NULLIF(LTRIM(RTRIM(gd.OutputDiagnosis)), ''),
						'Z000') codDiagnosticoPrincipal,---------------------------------------10
				CASE
					WHEN gd.VITANACIM = 'MUERTO' THEN '02'
					WHEN i.IsCutAccount =1 and gd.conditionDestination is NULL THEN '08'
					WHEN i.OutputDate <= COALESCE(gd.RealMedicalDischargeDate,i.OutputDate) THEN '08'
					ELSE COALESCE(gd.conditionDestination, '01')
				END condicionDestinoUsuarioEgreso,-----------------------------------------------------------11
				CASE 
					WHEN gd.VITANACIM = 'MUERTO' THEN COALESCE(NULLIF(LTRIM(RTRIM(qd.DiagnosticCode)), ''), NULLIF(LTRIM(RTRIM(gd.OutputDiagnosis)), ''))
					WHEN gd.conditionDestination = '02' THEN COALESCE(NULLIF(LTRIM(RTRIM(qd.DiagnosticCode)), ''), NULLIF(LTRIM(RTRIM(gd.OutputDiagnosis)), ''))
					ELSE NULL 
				END codDiagnosticoCausaMuerte,---------------------------------------------------------------12
				FORMAT(CASE
							WHEN gd.VITANACIM = 'MUERTO' THEN COALESCE(gd.DeathDate, gd.FECHANACIM, i.OutputDate)
							WHEN i.OutputDate <= COALESCE(gd.RealMedicalDischargeDate,i.OutputDate) THEN i.OutputDate
							ELSE gd.RealMedicalDischargeDate
						END, 'yyyy-MM-dd HH:mm') AS fechaEgreso,--13
				null consecutivo,----------------------------------------------------------------------------14
				CAST(NULL AS VARCHAR(20)) codDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoPrincipalCIE11,
				CAST(NULL AS VARCHAR(20)) codDiagnosticoCausaMuerteCIE11,
				CAST(NULL AS VARCHAR(250)) nomCodDiagnosticoCausaMuerteCIE11,
				[rda].[GetCodigoVIDAByDocumentNumber](gd.IdentificationNumberChild) codigoVIDA,
				gd.DocumentType------------------------------------------------------------------------------15
			FROM Cte_General_Info gd 
			JOIN @Invoices i on gd.InvoiceId = i.InvoiceId
			JOIN @GynecologicalHistory gh on gd.InvoiceId = gh.InvoiceId
											AND (gh.AdmissionNumberChild = gd.AdmissionNumberChild
												OR (gh.AdmissionNumberChild IS NULL AND gd.AdmissionNumberChild IS NULL))
											AND gh.AdmissionNumber = gd.AdmissionNumber
			LEFT JOIN @QueryDiagnostics qd ON qd.InvoiceId = gd.InvoiceId
											AND (qd.AdmissionNumberChild = gd.AdmissionNumberChild
																							OR (qd.AdmissionNumberChild IS NULL AND gd.AdmissionNumberChild IS NULL))
																								and qd.CODDIAPRI = 1
			CROSS APPLY (
				SELECT
					EDADGESNAC = TRY_CONVERT(DECIMAL(5,1), NULLIF(LTRIM(RTRIM(gd.EDADGESNAC)), '')),
					NOMSEMGES = TRY_CONVERT(DECIMAL(5,1), gh.NOMSEMGES)
			) gestationalAge
			CROSS APPLY (
				SELECT
					DiagnosticCode = CASE
						WHEN LEN(LTRIM(RTRIM(qd.DiagnosticCode))) = 4
							AND RIGHT(LTRIM(RTRIM(qd.DiagnosticCode)), 1) = 'X'
							THEN LEFT(LTRIM(RTRIM(qd.DiagnosticCode)), 3)
						ELSE LTRIM(RTRIM(qd.DiagnosticCode))
					END,
					OutputDiagnosis = CASE
						WHEN LEN(LTRIM(RTRIM(gd.OutputDiagnosis))) = 4
							AND RIGHT(LTRIM(RTRIM(gd.OutputDiagnosis)), 1) = 'X'
							THEN LEFT(LTRIM(RTRIM(gd.OutputDiagnosis)), 3)
						ELSE LTRIM(RTRIM(gd.OutputDiagnosis))
					END
			) normalizedDiagnosis
			WHERE i.HasDeliveryProcedure = 1
		;

		/*
		Guard historico por Billing.RIPSServiceHospitalRelation.

		Se deja comentado porque, para RVC023, la regla vigente del SP es que cada factura
		con CUPS de parto debe retornar su segmento de recien nacido. Si se reactiva este
		bloque, una segunda factura del mismo ingreso con procedimiento de parto podria quedar
		sin recienNacidos y ser rechazada por el validador.

		WITH CTE_AllowNewBorn as (	SELECT *
									FROM (
										SELECT	rshs.*,
												i.AdmissionNumber,
												ROW_NUMBER() OVER(PARTITION BY i.AdmissionNumber
																	ORDER BY rshs.CreationDate ASC, rshs.Id ASC) AS RowNumber
										FROM @Invoices i
										JOIN Billing.Invoice i2 WITH(NOLOCK) on i.AdmissionNumber =i2.AdmissionNumber and i2.Status=1
																					AND i2.DocumentType IN (1, 2, 4)
																					AND EXISTS (	SELECT 1
																									FROM Billing.InvoiceDetail id WITH(NOLOCK)
																									JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON sod.Id = id.ServiceOrderDetailId
																									JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON ce.Id = sod.CUPSEntityId
																									JOIN @DeliveryCUPSPatterns cupsPattern ON LTRIM(RTRIM(ce.Code)) LIKE cupsPattern.Pattern
																									WHERE id.InvoiceId = i2.Id
																										AND sod.IsDelete = 0)
										JOIN Billing.RIPSServiceHospitalRelation rshs WITH(NOLOCK)
											on i2.InvoiceNumber = rshs.InvoiceNumber AND rshs.ServiceNameJson='recienNacidos'
									) as allowed
									WHERE allowed.RowNumber = 1)

		DELETE rrn
		FROM #ResultRecienNacido rrn
		JOIN @Invoices i on rrn.InvoiceNumber =i.InvoiceNumber
		JOIN CTE_AllowNewBorn cte on cte.AdmissionNumber = i.AdmissionNumber
		where ISNULL(rrn.condicionDestinoUsuarioEgreso, '') <>'08' AND i.InvoiceNumber <> cte.InvoiceNumber
		*/

		SELECT *
		from #ResultRecienNacido

	END TRY
	BEGIN CATCH
		IF OBJECT_ID('tempdb..#ResultRecienNacido') IS NOT NULL DROP TABLE #ResultRecienNacido

		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que genera los datos de recién nacidos requeridos para el reporte RIPS (Registro Individual de Prestación de Servicios de Salud). A partir de una lista de facturas enviadas como parámetro XML, extrae la información del binomio madre-hijo: datos del neonato (fecha de nacimiento, sexo biológico, peso, tipo y número de identificación del recién nacido), historia ginecológica de la madre (semanas de gestación, número de controles prenatales), diagnósticos asociados al recién nacido y condición de egreso. Solo retorna registros para facturas activas que contienen procedimientos de parto según prefijos CUPS configurados en el procedimiento, contempla mortinatos sin ingreso hijo y produce un conjunto de resultados estructurado según el estándar RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el bloque RIPS de recién nacidos (binomio madre-hijo) con datos de identificación, gestación, peso, diagnóstico principal, condición de egreso y causa de muerte para las facturas que contienen procedimientos de parto identificados por patrones CUPS 735%, 740%, 721%, 725% o 732201.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas recibidas en @Parameters deben existir en Billing.Invoice y estar en Status=1 (activas).; Para retornar recién nacido, la factura debe tener al menos un detalle con CUPS cuyo código coincida con los patrones 735%, 740%, 721%, 725% o 732201 en Billing.InvoiceDetail/Billing.ServiceOrderDetail/Contract.CUPSEntity.; El ingreso de la madre (ADINGRESO) debe tener un registro asociado en HCRECINAC.; Para recién nacidos vivos, normalmente HCRECINAC.NUMINGRESHIJO apunta al ingreso del hijo y HCRECINAC.IPCODPACIHIJO al paciente hijo en INPACIENT.; Para mortinatos, NUMINGRESHIJO e IPCODPACIHIJO pueden venir nulos; en ese caso el SP conserva el registro de HCRECINAC, deriva identificación como menor sin identificación (MS) a partir del código de la madre + NUMHIJREG, toma FECMUEPAC como egreso y toma el diagnóstico desde HCRECNADI asociado al NUMCONSEC del registro neonatal.; Debe existir al menos un registro en HCANTGINE para el ingreso de la madre.; Si existe ingreso del hijo, se toma el diagnóstico principal (CODDIAPRI=1) desde INDIAGNOP para ese ingreso.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan facturas con Status=1 (activas).; Solo se retorna recién nacido para facturas que tienen procedimientos de parto con patrones CUPS 735%, 740%, 721%, 725% o 732201.; Si más de una factura activa del mismo ingreso tiene procedimiento de parto, cada factura puede retornar su propio segmento de recién nacido, porque RVC023 valida la presencia del segmento contra el CUPS de parto de la factura evaluada.; La relajación de ingreso/paciente hijo nulo aplica únicamente para mortinatos; los recién nacidos vivos deben conservar NUMINGRESHIJO, IPCODPACIHIJO, paciente hijo y tipo de documento.; La selección del diagnóstico principal del recién nacido con ingreso hijo prioriza tipo ''E'' (egreso), luego ''A'' (admisión), luego otros, y dentro de cada categoría la fecha de diagnóstico más reciente.; Para mortinatos sin ingreso hijo, la selección de diagnóstico usa HCRECNADI por CONSECREC, valida el código contra INDIAGNOS y prioriza códigos distintos de Z37 sobre códigos Z37.; Antes de retornar diagnósticos a RIPS, el SP elimina la X final usada como relleno interno en códigos de 3 caracteres (ej: P95X -> P95, O16X -> O16).; Cuando no existe diagnóstico ni OutputDiagnosis, el código de diagnóstico principal por defecto es ''Z000''.; Para mortinatos sin ingreso hijo, la condición de destino al egreso siempre es ''02'' y la fecha de egreso se toma de COALESCE(FECMUEPAC, FECHANACIM, OutputDate).; El alta médica real se toma como COALESCE(DateHISPACA del recién nacido, MedicalDischargeDate de HCREGEGRE).; Solo se consideran historias clínicas del recién nacido cuyo INDICAPAC esté en (''3'',''4'',''5'',''6'',''8'',''9'',''10'',''11'',''12'',''15'',''16'',''19'',''20'',''21''); de ellas se toma la última por FECHISPAC e ID.; Las fechas de nacimiento y egreso se formatean como ''yyyy-MM-dd HH:mm''.; El número de consultas prenatales por defecto es 0 cuando no hay registro.; Los errores capturados en CATCH liberan la tabla temporal y se imprimen, pero no se relanzan.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS; binomio madre-hijo; recién nacido; edad gestacional; controles prenatales; sexo biológico; diagnóstico principal CIE-10; condición de destino al egreso; causa de muerte; cuenta de corte; factura hospitalaria; alta médica; prestador de salud; vía de ingreso a servicios de salud', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #ResultRecienNacido: Inserta una fila por factura que tenga procedimiento de parto con patrones CUPS 735%, 740%, 721%, 725% o 732201.; [RETURN_RESULT] #ResultRecienNacido: Devuelve el contenido final de #ResultRecienNacido como resultado del procedimiento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si la factura no tiene procedimiento de parto con patrones CUPS 735%, 740%, 721%, 725% o 732201 → No retorna registro de recién nacido para esa factura; si hcan.NOMSEMGES = 0.0 OR hcan.NOMSEMGES IS NULL → Asume edad gestacional de 38.0 semanas por defecto else Toma el valor de NOMSEMGES registrado en la historia ginecológica; si gd.SEXRECNAC = 1 / = 2 / otro → Mapea sexo biológico a ''01'' (masculino), ''02'' (femenino) o ''03'' (indeterminado) respectivamente; si el recién nacido es mortinato y no tiene paciente hijo → tipoDocumentoIdentificacion = ''MS'' y numDocumentoIdentificacion = IPCODPACI de la madre + NUMHIJREG; si el mortinato tiene diagnósticos en HCRECNADI → valida cada candidato contra INDIAGNOS, luego prioriza el primer código válido que no sea Z37, y si todos son Z37 toma Z37; si el diagnóstico elegido o OutputDiagnosis termina en X → se remueve la X antes de retornarlo en codDiagnosticoPrincipal/codDiagnosticoCausaMuerte; si INDICAPAC IN (''3'',''4'',''5'',''6'',''8'',''19'',''20'',''21'') → conditionDestination = ''03'' else Si INDICAPAC en (''12'',''15'',''16'') → ''01''; ''11'' → ''02''; ''10'' con RIPSCode=13 → ''05''; ''10'' → ''04''; ''9'' → ''06''; si gd.VITANACIM = ''MUERTO'' → condicionDestinoUsuarioEgreso = ''02'', codDiagnosticoCausaMuerte = COALESCE(DiagnosticCode, OutputDiagnosis) y fechaEgreso = COALESCE(DeathDate, FECHANACIM, OutputDate); si i.IsCutAccount=1 y gd.conditionDestination is NULL, o i.OutputDate <= RealMedicalDischargeDate → condicionDestinoUsuarioEgreso = ''08'' (cuenta de corte / sin alta médica registrada) else Toma COALESCE(conditionDestination, ''01'') para evitar egresos vivos sin condición destino; si conditionDestination=''02'' usa COALESCE(DiagnosticCode, OutputDiagnosis); en otro caso NULL; si i.OutputDate <= COALESCE(RealMedicalDischargeDate, i.OutputDate) → fechaEgreso = i.OutputDate (factura de corte sin alta clínica final) else fechaEgreso = RealMedicalDischargeDate (fecha del alta médica real)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.CUPSEntity; dbo.ADINGRESO; dbo.HCRECINAC; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.ADCENATEN; dbo.EntryRoutesHealthServices; dbo.HCREGEGRE; dbo.HCANTGINE; dbo.HCRECNADI; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_GetInfoRecienNacidosRIPS';
-- GO
