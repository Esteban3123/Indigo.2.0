-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados a hospitalizacion para RIPS
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
		PrevInvoiceId INT,
		PrevCutType	tinyint,
		PrevInitialDate DATETIME,
		PrevOutputDate  DATETIME)

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
								VITANACIM VARCHAR(50),
								NUMCONSEC INT,
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

		/*******_cte_facturas anteriores_Corte_***************/
		;WITH cte_behaindInvoice as (
									select i.InvoiceId, prev.*
									from @Invoices i
									CROSS APPLY (SELECT TOP 1	prevI.Id AS PrevInvoiceId,
																prevI.InitialDate as PrevInitialDate,
																prevI.OutputDate AS PrevOutputDate,
																prevI.CutType AS PrevCutType
												FROM Billing.Invoice prevI WITH(NOLOCK)
												WHERE i.Status = 1
												AND	prevI.AdmissionNumber = i.AdmissionNumber
												AND prevI.Id <> i.InvoiceId
												AND prevI.OutputDate < i.OutputDate
												ORDER BY prevI.OutputDate DESC)prev)

		UPDATE i SET	i.PrevInvoiceId = cte.PrevInvoiceId,
						i.PrevCutType = cte.PrevCutType,
						i.PrevInitialDate =cte.PrevInitialDate,
						i.PrevOutputDate = cte.PrevOutputDate
		from @Invoices i
		JOIN cte_behaindInvoice cte on i.InvoiceId=cte.InvoiceId

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
								VITANACIM,--------------------15
								NUMCONSEC,--------------------16
								MedicalDischargeDate,---------17
								CODIPSSEC,--------------------18
								RIPSCode---------------------19
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
				pacient.IPCODPACI IdentificationNumberChild,----12
				tip.SIGLA TypeIdentificationChild,--------------13
				RECINAC.PESORECNA WeightChild,------------------14
				RECINAC.VITANACIM,------------------------------15
				RECINAC.NUMCONSEC,------------------------------16
				HCEGRE.FECALTPAC AS MedicalDischargeDate,-------17
				LTRIM(RTRIM(ca.CODIPSSEC)) as CODIPSSEC,--------18
				erhs.RIPSCode-----------------------------------19
		FROM @Invoices i
		JOIN [dbo].ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = i.AdmissionNumber
		JOIN [dbo].HCRECINAC RECINAC WITH (NOLOCK) ON RECINAC.NUMINGRES = ad.NUMINGRES
		JOIN [dbo].ADINGRESO adChild WITH(NOLOCK) ON RECINAC.NUMINGRESHIJO = adChild.NUMINGRES
		JOIN [dbo].[INPACIENT] pacient WITH(NOLOCK) ON RECINAC.IPCODPACIHIJO= pacient.IPCODPACI
		JOIN dbo.ADTIPOIDENTIFICA tip WITH (NOLOCK) ON tip.CODIGO = pacient.IPTIPODOC
		JOIN dbo.ADCENATEN ca WITH (NOLOCK) ON ca.CODCENATE = ad.CODCENATE
		LEFT JOIN [dbo].EntryRoutesHealthServices erhs WITH (NOLOCK) on adChild.IdEntryRoutesHealthServices = erhs.Id
		LEFT JOIN HCREGEGRE HCEGRE WITH (NOLOCK) ON HCEGRE.NUMINGRES = RECINAC.NUMINGRESHIJO 

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
				CASE WHEN (hcan.NOMSEMGES=0.0 or hcan.nomsemges is null) then 38.0 else hcan.NOMSEMGES end AS NOMSEMGES,
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
			JOIN [dbo].HCRECNADI hc WITH(NOLOCK) ON gd.NUMCONSEC = hc.CONSECREC
			JOIN [dbo].INDIAGNOP idp WITH(NOLOCK) 
				ON gd.AdmissionNumberChild = idp.NUMINGRES 
					and hc.CODDIAGNO = idp.CODDIAGNO 
					and idp.CODDIAPRI=1)	

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
				on t1.InvoiceId = df.InvoiceId and t1.AdmissionNumberChild = df.AdmissionNumberChild
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
									and INDICAPAC IN ('3','4','5','6','8','10','12','15','16','19','20','21')
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
				subHc.AdmissionNumberChild,
				hc.NUMEFOLIO,
				case 
					when hc.INDICAPAC in('5','6','8','19','20','21') then '03'
					when hc.INDICAPAC in( '12', '15', '16') then '01'
					when hc.INDICAPAC = '11' then '02' 
					when hc.INDICAPAC = '10' and hc.RIPSCode = 13 then '05'
					when hc.INDICAPAC = '10' then '04'
					when hc.INDICAPAC = '9' then '06'
				end conditionDestination,
				hc.INDICAPAC,
				hc.FECHISPAC
		FROM (	SELECT hc.AdmissionNumberChild,MIN(hc.ID) AS Id
				FROM CTE_HisP hc WITH(NOLOCK)
				GROUP by hc.AdmissionNumberChild) subHc
		JOIN CTE_HisP hc WITH(NOLOCK)
			ON subHc.Id = hc.ID
		GROUP by 
			subHc.AdmissionNumberChild,
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
											DocumentType--------------------15
										)
		SELECT
				gd.InvoiceNumber AS InvoiceNumber,---------------------------------------------1
				LTRIM(RTRIM(gd.CODIPSSEC))AS codPrestador,-------------------------------------2
				gd.TypeIdentificationChild tipoDocumentoIdentificacion,------------------------3
				LTRIM(RTRIM(gd.IdentificationNumberChild)) numDocumentoIdentificacion,---------4
				FORMAT(gd.FECHANACIM, 'yyyy-MM-dd HH:mm') fechaNacimiento,---------------------5
				CAST(gh.NOMSEMGES AS INT) edadGestacional,-------------------------------------6
				isnull(gh.CANTPRENA, 00 ) numConsultasCPrenatal,-------------------------------7
				CASE gd.SEXRECNAC 
					WHEN 1 THEN '01' 
					WHEN 2 THEN '02' 
					ELSE '03'
				END codSexoBiologico,-----------------------------------------------------------8
				gd.WeightChild peso,------------------------------------------------------------9
				COALESCE(qd.DiagnosticCode,gd.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,--10
				CASE
					WHEN i.IsCutAccount =1 and gd.conditionDestination is NULL THEN '08'
					WHEN i.OutputDate <= COALESCE(gd.RealMedicalDischargeDate,i.OutputDate) THEN '08'
					ELSE gd.conditionDestination
				END condicionDestinoUsuarioEgreso,-----------------------------------------------------------11
				CASE gd.VITANACIM 
					WHEN 'MUERTO' THEN qd.DiagnosticCode
					ELSE NULL 
				END codDiagnosticoCausaMuerte,---------------------------------------------------------------12
				FORMAT(IIF(i.OutputDate <= COALESCE(gd.RealMedicalDischargeDate,i.OutputDate),
							i.OutputDate,gd.RealMedicalDischargeDate) , 'yyyy-MM-dd HH:mm') AS fechaEgreso,--13
				null consecutivo,----------------------------------------------------------------------------14
				gd.DocumentType------------------------------------------------------------------------------15
			FROM Cte_General_Info gd 
			JOIN @Invoices i on gd.InvoiceId = i.InvoiceId
			JOIN @GynecologicalHistory gh on gd.InvoiceId = gh.InvoiceId
											AND gh.AdmissionNumberChild = gd.AdmissionNumberChild
											AND gh.AdmissionNumber = gd.AdmissionNumber
			JOIN @QueryDiagnostics qd ON qd.InvoiceId = gd.InvoiceId
											AND qd.AdmissionNumberChild = gd.AdmissionNumberChild
											and qd.CODDIAPRI = 1
			WHERE gd.RealMedicalDischargeDate IS NULL
					OR (
						(CAST(i.InitialDate AS DATE) <= CAST( gd.RealMedicalDischargeDate AS DATE))
						AND NOT (i.PrevInvoiceId IS NOT NULL 
									AND (	( gd.RealMedicalDischargeDate 
											 BETWEEN i.PrevInitialDate AND i.PrevOutputDate
											 )
											OR i.PrevInitialDate > gd.RealMedicalDischargeDate
										)
								)
						)
		;

		

		WITH CTE_AllowNewBorn as (	SELECT top 1 rshs.*,i.AdmissionNumber
									FROM @Invoices i
									JOIN Billing.Invoice i2 WITH(NOLOCK) on i.AdmissionNumber =i2.AdmissionNumber and i2.Status=1
									JOIN Billing.RIPSServiceHospitalRelation rshs WITH(NOLOCK)
										on i2.InvoiceNumber = rshs.InvoiceNumber AND rshs.ServiceNameJson='recienNacidos')

		DELETE rrn
		FROM #ResultRecienNacido rrn
		JOIN @Invoices i on rrn.InvoiceNumber =i.InvoiceNumber
		JOIN CTE_AllowNewBorn cte on cte.AdmissionNumber = i.AdmissionNumber
		where rrn.condicionDestinoUsuarioEgreso <>'08' AND i.InvoiceNumber <> cte.InvoiceNumber

		SELECT *
		from #ResultRecienNacido

	END TRY
	BEGIN CATCH
		IF OBJECT_ID('tempdb..#ResultRecienNacido') IS NOT NULL DROP TABLE #ResultRecienNacido

		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END