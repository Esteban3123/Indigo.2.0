-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados a urgencias para RIPS
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInfoUrgenciasRIPS] 
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
		PrevOutputDate  DATETIME
	)
	Declare @GeneralData TABLE (InvoiceId INT NOT NULL,
								InvoiceNumber VARCHAR(20) NOT NULL,
								OutputDiagnosis VARCHAR(4),
								DocumentType TINYINT NOT NULL,
								CODCENATE VARCHAR(10) NOT NULL,
								ICAUSAING INT NOT NULL,
								CODDIAEGR VARCHAR(4),
								IFECHAING DATETIME NOT NULL,
								IdEntryRoutesHealthServices INT,
								NUMINGRES char(10) NOT NULL,
								INDICAPAC VARCHAr(2),
								ESTPACEGR INT
								)
	DECLARE @HCHISPACA TABLE(	NUMEFOLIO NCHAR(10) NOT NULL,
								NUMINGRES CHAR(10) NOT NULL, 
								ID INT NOT NULL,
								UFUTIPUNI INT NOT NULL,
								INDICAPAC CHAR(2) NOT NULL)

	DECLARE @Urgency TABLE(NUMINGRES CHAR(10) NOT NULL,
						   NUMEFOLIO_URG NCHAR(10) NOT null,
						   HC_UrgId INT NOT NULL,
						   HC_EgreId INT NULL,
						   NUMEFOLIO_EGRE NCHAR(10))

	DECLARE @INDIAGNOH TABLE(InvoiceId INT ,
							NUMINGRES CHAR(10) ,
							DiagnosticCode CHAR(4),
							CODDIAPRI BIT,
							DIAINGEGR CHAR(1),
							FECDIAGNO DATETIME,
							DiagnoType VARCHAR(2))

	DECLARE @DIAGNOSTICOS TABLE(InvoiceId INT ,
								AdmissionNumber CHAR(10),
								IdPartition INT,
								DiagnosticCode CHAR(4),
								CODDIAPRI BIT,
								Diagnostic varchar(20),
								DiagnoType VARCHAR(2))

	DECLARE @QueryDiagnostics TABLE(InvoiceId INT,
									AdmissionNumber CHAR(10),
									DiagnosticCode1 char(4),
									DiagnosticCode2 char(4),
									DiagnosticCode3 char(4),
									CODDIAPRI BIT,
									DiagnoType VARCHAR(2))

	DECLARE @ALTA TABLE (	NUMINGRES CHAR(10),
							FECALTPAC DATETIME,
							InvoiceId INT NOT NULL)
	--Tabla RESULTADO
	IF OBJECT_ID('tempdb..#ResultUrgencias') IS NOT NULL DROP TABLE #ResultUrgencias

	CREATE TABLE #ResultUrgencias  (	
										codPrestador VARCHAR(13),
										fechaInicioAtencion VARCHAR(17),
										causaMotivoAtencion VARCHAR(3),
										codDiagnosticoPrincipal VARCHAR(10),
										codDiagnosticoPrincipalE VARCHAR(10),
										codDiagnosticoRelacionadoE1 VARCHAR(10),
										codDiagnosticoRelacionadoE2 VARCHAR(10),
										codDiagnosticoRelacionadoE3 VARCHAR(10),
										condicionDestinoUsuarioEgreso VARCHAR(2),
										codDiagnosticoCausaMuerte VARCHAR(10),
										fechaEgreso VARCHAR(17),
										consecutivo INT,
										InvoiceNumber VARCHAR(20),
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
			group by 
					i.Id,
					i.InvoiceNumber,
					i.OutputDiagnosis,
					i.DocumentType,
					i.AdmissionNumber,
					i.[Status],
					i.IsCutAccount,
					i.CutType,
					i.InitialDate,
					i.OutputDate

		/*******_cte_facturas anteriores_***************/
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
												AND prevI.[Status] = 1
												ORDER BY prevI.OutputDate DESC)prev)

		UPDATE i SET	i.PrevInvoiceId = cte.PrevInvoiceId,
						i.PrevCutType = cte.PrevCutType,
						i.PrevInitialDate =cte.PrevInitialDate,
						i.PrevOutputDate = cte.PrevOutputDate
		from @Invoices i
		JOIN cte_behaindInvoice cte on i.InvoiceId=cte.InvoiceId
		
		/*******************************************************************************************/

		INSERT INTO @GeneralData
		SELECT	i.InvoiceId,
				i.InvoiceNumber,
				i.OutputDiagnosis,
				i.DocumentType,
				ad.CODCENATE,
				ad.ICAUSAING,
				ad.CODDIAEGR,
				IIF(i.CutType IN(1,2),ad.IFECHAING,i.InitialDate) AS IFECHAING,
				ad.IdEntryRoutesHealthServices,
				ad.NUMINGRES,
				IIF(subQHc.INDICAPAC IS NULL AND i.IsCutAccount =1,13,subQHc.INDICAPAC) AS INDICAPAC,
				Egreso.ESTPACEGR
		FROM @Invoices i
		JOIN ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = i.AdmissionNumber
		LEFT JOIN dbo.CHREGEGRE Egreso WITH (NOLOCK) ON ad.NUMINGRES = EGRESO.NUMINGRES
		OUTER APPLY(SELECT top 1 *
					FROM HCHISPACA his
					where his.NUMINGRES =ad.NUMINGRES
						AND INDICAPAC IN ('3','4','5','6','10','11','12')
						AND CAST(his.FECHISPAC AS DATE) <= CAST(i.OutputDate AS DATE)
					order by his.FECHISPAC DESC
					) subQHc
		WHERE ad.TIPOINGRE =2 and ad.IINGREPOR in (1,4,5)

		INSERT INTO @HCHISPACA
		SELECT hc.NUMEFOLIO, hc.NUMINGRES, hc.ID,inuni.UFUTIPUNI,hc.INDICAPAC
		from @GeneralData g 
		JOIN HCHISPACA hc WITH(NOLOCK) ON g.NUMINGRES = hc.NUMINGRES
		join INUNIFUNC inuni  WITH(NOLOCK) on hc.UFUCODIGO = inuni.UFUCODIGO

		/*******************************************************************************************/

		INSERT into @Urgency
		Select  maxHc.NUMINGRES,
				hc.NUMEFOLIO AS  NUMEFOLIO_URG,
				maxHc.HC_UrgId,
				EgreHc.HC_EgreId,
				hc2.NUMEFOLIO AS NUMEFOLIO_EGRE
		from (	select cteA.NUMINGRES, MAX(cteA.ID)  HC_UrgId
				from @HCHISPACA cteA
				where cteA.UFUTIPUNI =1
				GROUP by cteA.NUMINGRES) maxHc
		join @HCHISPACA hc on hc.Id = maxHc.HC_UrgId
		OUTER apply  (
				select cteB.NUMINGRES, min(cteB.ID) HC_EgreId
				from @HCHISPACA cteB
				where cteB.UFUTIPUNI <> 1 and cteB.NUMINGRES = maxHc.NUMINGRES and cteB.NUMEFOLIO > hc.NUMEFOLIO
				GROUP by cteB.NUMINGRES) EgreHc 
		left join @HCHISPACA hc2 on hc2.Id = EgreHc.HC_EgreId

		/*******************************************************************************************/


		INSERT INTO @INDIAGNOH
		SELECT *
		FROM (
		SELECT		g.InvoiceId,
					Diag.NUMINGRES,
					Diag.CODDIAGNO AS DiagnosticCode,
					Diag.CODDIAPRI,
					Diag.DIAINGEGR,
					Diag.FECDIAGNO,
					'U' DiagnoType
			from INDIAGNOH Diag WITH(NOLOCK)
			JOIN @Urgency  cte on Diag.NUMINGRES = cte.NUMINGRES and cte.NUMEFOLIO_URG = Diag.NUMEFOLIO
			JOIN @GeneralData g on g.NUMINGRES = cte.NUMINGRES

			UNION ALL

			SELECT	g.InvoiceId,
					Diag.NUMINGRES,
					Diag.CODDIAGNO AS DiagnosticCode,
					Diag.CODDIAPRI,
					Diag.DIAINGEGR,
					Diag.FECDIAGNO,
					'ET' DiagnoType
			from INDIAGNOH Diag WITH(NOLOCK)
			JOIN @Urgency  cte on Diag.NUMINGRES = cte.NUMINGRES and cte.NUMEFOLIO_EGRE = Diag.NUMEFOLIO
			JOIN @GeneralData g on g.NUMINGRES = cte.NUMINGRES
			where Diag.CODDIAPRI=1 

			UNION ALL

			SELECT	g.InvoiceId,
					Diag.NUMINGRES,
					Diag.CODDIAGNO AS DiagnosticCode,
					Diag.CODDIAPRI,
					Diag.DIAINGEGR,
					Diag.FECDIAGNO,
					'E' DiagnoType
			from HCREGEGRE HCE WITH(NOLOCK)
			JOIN @Urgency  cte on HCE.NUMINGRES = cte.NUMINGRES and cte.NUMEFOLIO_URG = HCE.NUMEFOLIO
			JOIN INDIAGNOH Diag WITH(NOLOCK) ON Diag.NUMINGRES = cte.NUMINGRES and cte.NUMEFOLIO_URG = Diag.NUMEFOLIO
			JOIN @GeneralData g on g.NUMINGRES = cte.NUMINGRES
			where Diag.CODDIAPRI=1) X
			/*******************************************************************************************/
			INSERT INTO @DIAGNOSTICOS( InvoiceId,AdmissionNumber,IdPartition,DiagnosticCode,CODDIAPRI,Diagnostic, DiagnoType)
			SELECT	T.InvoiceId,
						T.AdmissionNumber,
						T.IdPartition,
						T.DiagnosticCode,
						T.CODDIAPRI,
						CONCAT('DiagnosticCode',T.IdPartition) Diagnostic,
						DiagnoType
				FROM
				(
					SELECT 
						i.InvoiceId,
						I.NUMINGRES AdmissionNumber,
						ROW_NUMBER() OVER (PARTITION BY i.InvoiceId, i.NUMINGRES,i.DiagnoType,i.CODDIAPRI ORDER BY
							CASE i.DIAINGEGR 
								WHEN 'E' THEN 1 
								WHEN 'A' THEN 2 
								ELSE 3 
							END ASC,
							i.FECDIAGNO DESC) AS IdPartition,
						i.DiagnosticCode,
						i.CODDIAPRI,
						i.DiagnoType
					FROM @INDIAGNOH i
				) T

		/*******************************************************************************************/

		INSERT INTO @QueryDiagnostics
		select	InvoiceId,
				AdmissionNumber,
				DiagnosticCode1,
				DiagnosticCode2,
				DiagnosticCode3,
				CODDIAPRI,
				DiagnoType
			from (select  InvoiceId,
						AdmissionNumber,
						DiagnosticCode,
						Diagnostic,
						CODDIAPRI,
						DiagnoType
				from @DIAGNOSTICOS) as t1
				pivot (MAX(DiagnosticCode)
						for Diagnostic IN (DiagnosticCode1,DiagnosticCode2,DiagnosticCode3)
						) as t2
		/*******************************************************************************************/
		;
		WITH CTE_ALTA AS (	SELECT CHR.NUMINGRES, MAX(FECFINEST) AS FECALTPAC 
							FROM @GeneralData gd
							JOIN dbo.CHREGESTA AS CHR WITH (NOLOCK) ON CHR.NUMINGRES = gd.NUMINGRES
							JOIN dbo.CHCAMASHO AS CHC WITH (NOLOCK) ON CHR.CODICAMAS = CHC.CODICAMAS
							WHERE CHC.CODCLACAM  = 1 
							GROUP BY CHR.NUMINGRES)
		
		INSERT INTO @ALTA(NUMINGRES,FECALTPAC,InvoiceId)
		SELECT	gd.NUMINGRES,
				IIF(i.OutputDate < = COALESCE(cte.FECALTPAC,i.OutputDate),
					i.OutputDate,cte.FECALTPAC) AS FECALTPAC,
				gd.InvoiceId
		FROM @GeneralData gd
		JOIN @Invoices i ON gd.InvoiceId = i.InvoiceId
		LEFT JOIN CTE_ALTA cte on gd.NUMINGRES = cte.NUMINGRES
		WHERE cte.FECALTPAC IS NULL
			OR (
				(CAST(gd.IFECHAING AS DATE) <= cte.FECALTPAC)
				AND NOT (i.PrevInvoiceId IS not NULL 
							AND (	(cte.FECALTPAC
										BETWEEN i.PrevInitialDate  AND i.PrevOutputDate
									)
									OR 
									i.PrevInitialDate > cte.FECALTPAC 
								)
						)
				)


		/*******************************************************************************************/
		INSERT INTO #ResultUrgencias(
										codPrestador,--------------------1
										fechaInicioAtencion,-------------2
										causaMotivoAtencion,-------------3
										codDiagnosticoPrincipal,---------4
										codDiagnosticoPrincipalE,--------5
										codDiagnosticoRelacionadoE1,-----6
										codDiagnosticoRelacionadoE2,-----7
										codDiagnosticoRelacionadoE3,-----8
										condicionDestinoUsuarioEgreso,---9
										codDiagnosticoCausaMuerte,-------10
										fechaEgreso,---------------------11
										consecutivo,---------------------12
										InvoiceNumber,-------------------13
										DocumentType---------------------14
									)
		select	LTRIM(RTRIM(ca.CODIPSSEC)) AS codPrestador,---------------------1
				FORMAT(g.IFECHAING, 'yyyy-MM-dd HH:mm') fechaInicioAtencion,----2
				cfa.RIPSCode causaMotivoAtencion,-------------------------------3
				COALESCE(qdPrin.DiagnosticCode1,g.CODDIAEGR,g.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,-------------------4
				COALESCE(qdEgre.DiagnosticCode1,qdTras.DiagnosticCode1,g.CODDIAEGR,g.OutputDiagnosis) codDiagnosticoPrincipalE,--5
				qdRela.DiagnosticCode1 as codDiagnosticoRelacionadoE1,----------6
				qdRela.DiagnosticCode2 as codDiagnosticoRelacionadoE2,----------7
				qdRela.DiagnosticCode3 as codDiagnosticoRelacionadoE3,----------8
				CASE  
					WHEN g.INDICAPAC = 12 
					AND refRegister.EXTRAMURAL = 1 
					AND refRegister.FECSOLICIT <= ALTA.FECALTPAC
					AND refRegister.ESTADO = 5 THEN '03'
					WHEN g.INDICAPAC IN( 12, 15, 16 ) THEN '01'
					WHEN g.INDICAPAC = 11 THEN '02' 
					WHEN g.INDICAPAC = 10 and erhs.RIPSCode = 13 THEN '05'
					WHEN g.INDICAPAC = 10 THEN '04'
					WHEN g.INDICAPAC = 9  THEN '06'
					WHEN g.INDICAPAC IN (3,4) THEN  '03'
					WHEN g.INDICAPAC = 13 THEN '08'
					ELSE '01'
				end condicionDestinoUsuarioEgreso,-------------------------------------------------9
				CASE g.ESTPACEGR WHEN 3 THEN g.CODDIAEGR ELSE NULL END codDiagnosticoCausaMuerte,--10
				FORMAT(IIF(g.IFECHAING > ALTA.FECALTPAC,g.IFECHAING,ALTA.FECALTPAC ), 'yyyy-MM-dd HH:mm') AS fechaEgreso,-------------------------11
				NULL consecutivo,------------------------------------------------------------------12
				g.InvoiceNumber AS InvoiceNumber,--------------------------------------------------13
				g.DocumentType  AS DocumentType----------------------------------------------------14
		from @GeneralData g
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON g.CODCENATE = CA.CODCENATE 
		JOIN Causesofattention cfa WITH(NOLOCK) on g.ICAUSAING = cfa.Code
		JOIN @ALTA ALTA ON g.NUMINGRES = ALTA.NUMINGRES  AND ALTA.InvoiceId =g.InvoiceId
		LEFT JOIN @QueryDiagnostics qdPrin on qdPrin.InvoiceId=g.InvoiceId and qdPrin.AdmissionNumber=g.NUMINGRES and qdPrin.DiagnoType ='U' and qdPrin.CODDIAPRI=1
		LEFT JOIN EntryRoutesHealthServices erhs WITH (NOLOCK) on g.IdEntryRoutesHealthServices = erhs.Id
		LEFT JOIN HCREFCONP refRegister WITH (NOLOCK) on refRegister.NUMINGRES = g.NUMINGRES
		LEFT JOIN @QueryDiagnostics qdRela on qdRela.InvoiceId=g.InvoiceId and qdRela.AdmissionNumber=g.NUMINGRES and qdRela.DiagnoType ='U' and qdRela.CODDIAPRI=0
		LEFT JOIN @QueryDiagnostics qdEgre on qdEgre.InvoiceId=g.InvoiceId and qdEgre.AdmissionNumber=g.NUMINGRES and qdEgre.DiagnoType ='E'
		LEFT JOIN @QueryDiagnostics qdTras on qdTras.InvoiceId=g.InvoiceId and qdTras.AdmissionNumber=g.NUMINGRES and qdTras.DiagnoType ='ET'
			
		;
		WITH CTE_AllowUrgency as (	SELECT top 1 rshs.*,i.AdmissionNumber
									FROM @Invoices i
									JOIN Billing.Invoice i2 WITH(NOLOCK) on i.AdmissionNumber =i2.AdmissionNumber and i2.Status=1
									JOIN Billing.RIPSServiceHospitalRelation rshs WITH(NOLOCK)
										on i2.InvoiceNumber = rshs.InvoiceNumber AND rshs.ServiceNameJson='urgencias')

		DELETE rrn
		FROM #ResultUrgencias rrn
		JOIN @Invoices i on rrn.InvoiceNumber =i.InvoiceNumber
		JOIN CTE_AllowUrgency cte on cte.AdmissionNumber = i.AdmissionNumber
		where rrn.condicionDestinoUsuarioEgreso <>'08' AND i.InvoiceNumber <> cte.InvoiceNumber

		SELECT *
		from #ResultUrgencias

	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END