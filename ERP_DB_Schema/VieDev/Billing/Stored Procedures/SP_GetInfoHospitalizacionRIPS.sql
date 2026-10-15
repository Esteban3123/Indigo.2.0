-- =============================================
-- Author:		Giovanny Plazas
-- Create date: 2024-09-12
-- Description:	Procedimiento que se encarga de el generar los datos relacionados a hospitalizacion para RIPS
-- LastModification: Jose Reyes Paez
-- ModificationDate: 2025-02-21
-- =============================================
CREATE PROCEDURE [Billing].[SP_GetInfoHospitalizacionRIPS] 
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
		OutputDate  DATETIME NOT NULL
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
								INDICAPAC VARCHAR(2),
								ESTPACEGR INT,
								NUMEFOLIO NCHAR(10),
								IAUTORIZA VARCHAR(15),
								FECALTPAC DATETIME NOT NULL
								)

	DECLARE @DIAGNOSTICOS TABLE(InvoiceId INT ,
								AdmissionNumber CHAR(10),
								IdPartition INT,
								DiagnosticCode CHAR(4),
								CODDIAPRI BIT,
								Diagnostic varchar(20))

	DECLARE @QueryDiagnostics TABLE(InvoiceId INT,
									AdmissionNumber CHAR(10),
									DiagnosticCode1 char(4),
									DiagnosticCode2 char(4),
									DiagnosticCode3 char(4),
									CODDIAPRI BIT)

	BEGIN TRY

		INSERT INTO @Invoices (	InvoiceId,		--1
								InvoiceNumber,	--2
								OutputDiagnosis,--3
								DocumentType,	--4
								AdmissionNumber,--5
								[Status],		--6
								IsCutAccount,	--7
								CutType,		--8
								InitialDate,	--9
								OutputDate)		--10
			SELECT 
				i.Id AS InvoiceId,------------------1
				i.InvoiceNumber AS InvoiceNumber,---2
				i.OutputDiagnosis,------------------3
				i.DocumentType,---------------------4
				i.AdmissionNumber,------------------5
				i.[Status],-------------------------6
				i.IsCutAccount,---------------------7
				i.CutType,--------------------------8
				i.InitialDate,----------------------9
				i.OutputDate-----------------------10
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

		/*******************************************************************************************/

		INSERT INTO @GeneralData
		SELECT	i.InvoiceId,
				i.InvoiceNumber,
				i.OutputDiagnosis,
				i.DocumentType,
				ad.CODCENATE,
				ad.ICAUSAING,
				ad.CODDIAEGR,
				CASE 
					WHEN i.CutType IN (1,2) THEN ad.IFECHAING
					ELSE i.InitialDate
				END IFECHAING,
				ad.IdEntryRoutesHealthServices,
				ad.NUMINGRES,
				IIF(i.IsCutAccount = 1,13,his.INDICAPAC) AS INDICAPAC,
				Egreso.ESTPACEGR,
				HCE.NUMEFOLIO,
				ISNULL(RTRIM(ad.IAUTORIZA), '') AS IAUTORIZA,
				CASE
					WHEN i.CutType IN(1,4) THEN COALESCE(HCE.FECALTPAC,i.OutputDate)
					ELSE i.OutputDate
				END FECALTPAC
		FROM @Invoices i
		JOIN ADINGRESO ad WITH(NOLOCK) on ad.NUMINGRES = i.AdmissionNumber
		LEFT JOIN dbo.CHREGEGRE Egreso WITH (NOLOCK) ON ad.NUMINGRES = Egreso.NUMINGRES
		LEFT JOIN HCREGEGRE HCE WITH(NOLOCK) on HCE.NUMINGRES = ad.NUMINGRES
		LEFT JOIN HCHISPACA his WITH(NOLOCK)
			on his.NUMINGRES = ad.NUMINGRES 
				and NOT his.INDICAPAC IN (1,2,3,4,5,6,7,8,13)
				and HCE.NUMEFOLIO = his.NUMEFOLIO
		WHERE ad.TIPOINGRE = 2 
			AND (HCE.FECALTPAC IS NULL OR (CAST(IIF(i.CutType IN (1,2),ad.IFECHAING,i.InitialDate) AS DATE) <= CAST(HCE.FECALTPAC AS DATE)))

		/*******************************************************************************************/

		INSERT INTO @DIAGNOSTICOS( InvoiceId,AdmissionNumber,IdPartition,DiagnosticCode,CODDIAPRI,Diagnostic)
			SELECT	T.InvoiceId,
						T.AdmissionNumber,
						T.IdPartition,
						T.DiagnosticCode,
						T.CODDIAPRI,
						CONCAT('DiagnosticCode',T.IdPartition) Diagnostic
				FROM
				(
					SELECT 
						gd.InvoiceId,
						gd.NUMINGRES AdmissionNumber,
						ROW_NUMBER() OVER (PARTITION BY gd.InvoiceId, gd.NUMINGRES,idp.CODDIAPRI ORDER BY
							CASE idp.DIAINGEGR 
								WHEN 'E' THEN 1 
								WHEN 'A' THEN 2 
								ELSE 3 
							END ASC,
							idp.FECDIAGNO DESC) AS IdPartition,
						idp.CODDIAGNO as DiagnosticCode,
						idp.CODDIAPRI
					FROM INDIAGNOP idp WITH(NOLOCK)
					JOIN @GeneralData gd on idp.NUMINGRES =gd.NUMINGRES
				) T

		/*******************************************************************************************/

		INSERT INTO @QueryDiagnostics
		select	InvoiceId,
				AdmissionNumber,
				DiagnosticCode1,
				DiagnosticCode2,
				DiagnosticCode3,
				CODDIAPRI
			from (select InvoiceId,
						 AdmissionNumber,
						 DiagnosticCode,
						 Diagnostic,
						 CODDIAPRI
				from @DIAGNOSTICOS) as t1
				pivot (MAX(DiagnosticCode)
						for Diagnostic IN (DiagnosticCode1,DiagnosticCode2,DiagnosticCode3)
						) as t2

		/*******************************************************************************************/

		select	LTRIM(RTRIM(ca.CODIPSSEC)) AS codPrestador,
				--Se agrega condicion para que cuando exista error en Ingresos se corrija la via de ingreso
				CASE when erhs.RIPSCode in ('01','04') then '03' else  erhs.ripscode end viaIngresoServicioSalud,
				CAST(FORMAT(g.IFECHAING, 'yyyy-MM-dd HH:mm') AS VARCHAR(50)) fechaInicioAtencion,
				g.IAUTORIZA AS numAutorizacion,		
				cfa.RIPSCode causaMotivoAtencion,
				COALESCE(qdPrin.DiagnosticCode2,qdPrin.DiagnosticCode1,g.CODDIAEGR,g.OutputDiagnosis,'Z000') codDiagnosticoPrincipal,
				COALESCE(qdPrin.DiagnosticCode1,g.CODDIAEGR,g.OutputDiagnosis) codDiagnosticoPrincipalE,
				qdRela.DiagnosticCode1 as codDiagnosticoRelacionadoE1,
				qdRela.DiagnosticCode2 as codDiagnosticoRelacionadoE2, 
				qdRela.DiagnosticCode3 as codDiagnosticoRelacionadoE3,
				null codComplicacion,
				CASE  
					WHEN g.INDICAPAC = 12 and refRegister.EXTRAMURAL = 1 THEN '03'
					WHEN g.INDICAPAC in( 12, 15, 16 ) then '01'
					WHEN g.INDICAPAC = 11 THEN '02' 
					WHEN g.INDICAPAC = 10 and erhs.RIPSCode = 13 THEN '05'
					WHEN g.INDICAPAC = 10 THEN '04'
					WHEN g.INDICAPAC = 9  THEN '06'
					WHEN g.INDICAPAC = 13 THEN '08'
				end condicionDestinoUsuarioEgreso,
				CASE g.ESTPACEGR WHEN 3 THEN g.CODDIAEGR ELSE NULL END codDiagnosticoCausaMuerte,
				CAST(FORMAT(IIF(g.IFECHAING > g.FECALTPAC,g.IFECHAING,g.FECALTPAC ), 'yyyy-MM-dd HH:mm')AS VARCHAR(50)) fechaEgreso,
				NULL consecutivo,
				g.InvoiceNumber AS InvoiceNumber,
				g.DocumentType  AS DocumentType
		from @GeneralData g
		JOIN dbo.[ADCENATEN] AS ca WITH (NOLOCK) ON g.CODCENATE = CA.CODCENATE 
		JOIN Causesofattention cfa WITH(NOLOCK) on g.ICAUSAING = cfa.Code
		JOIN @QueryDiagnostics qdPrin on qdPrin.InvoiceId=g.InvoiceId and qdPrin.AdmissionNumber=g.NUMINGRES and  qdPrin.CODDIAPRI=1
		LEFT JOIN EntryRoutesHealthServices erhs WITH (NOLOCK) on g.IdEntryRoutesHealthServices = erhs.Id
		LEFT JOIN HCREFCONP refRegister WITH (NOLOCK) on refRegister.NUMINGRES = g.NUMINGRES
		LEFT JOIN @QueryDiagnostics qdRela on qdRela.InvoiceId=g.InvoiceId and qdRela.AdmissionNumber=g.NUMINGRES and qdRela.CODDIAPRI=0
	
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() AS VARCHAR(20))
	END CATCH
END