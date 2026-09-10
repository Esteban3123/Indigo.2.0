CREATE   PROCEDURE [rda].[sp_rda_Urgencias]
(
    @IPCODPACI varchar(50),
	@Ingreso varchar(10),
	@NUMEFOLIO varchar(10),
    @IdHispaca INT	
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Rango de folios para tecnologías en el transcurso de la urgencia
    DECLARE @PrimerFolio   VARCHAR(10)
    DECLARE @FolioAnterior VARCHAR(10)

    -- Se obtienen el primer y último folio de urgencias ANTERIORES al folio de egreso (@NUMEFOLIO).
    -- Si no existen folios anteriores (folio único), ambas variables quedan NULL:
    --   → MedTranscurso / ProcTranscurso no retornarán registros (BETWEEN NULL AND NULL = sin match)
    --   → MedEgreso / ProcEgreso siempre usa @NUMEFOLIO directamente y sí retorna datos.
    SELECT
        @PrimerFolio   = CAST(MIN(TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT)) AS VARCHAR(10)),
        @FolioAnterior = CAST(MAX(TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT)) AS VARCHAR(10))
    FROM dbo.HCHISPACA HC
    INNER JOIN dbo.INUNIFUNC UF ON UF.UFUCODIGO = HC.UFUCODIGO
    WHERE HC.NUMINGRES  = @Ingreso
      AND HC.IPCODPACI  = @IPCODPACI
      AND UF.UFUTIPUNI  = 1 -- Solo folios de urgencias
      AND TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT) < TRY_CAST(RTRIM(@NUMEFOLIO) AS INT)

    -- NIT empresa (constante por ejecución)
    DECLARE @NitEmpresa VARCHAR(20)
    SELECT @NitEmpresa = INDNITEMP FROM INEMPRESU

    -- Precálculo de funciones escalares para evitar múltiples ejecuciones no paralelizables
    DECLARE @ProcTranscurso  NVARCHAR(MAX)
    DECLARE @MedTranscurso   NVARCHAR(MAX)
    DECLARE @ProcEgreso      NVARCHAR(MAX)
    DECLARE @MedEgreso       NVARCHAR(MAX)

    SELECT @ProcTranscurso = rda.sf_rda_OrdenesMedicasProcedimientos(@Ingreso, @IPCODPACI, @PrimerFolio, @FolioAnterior, 0)
    SELECT @MedTranscurso  = rda.sf_rda_OrdenesMedicasMedicamentos(@Ingreso, @IPCODPACI, @PrimerFolio, @FolioAnterior, 0)
    SELECT @ProcEgreso     = rda.sf_rda_OrdenesMedicasProcedimientos(@Ingreso, @IPCODPACI, @NUMEFOLIO, NULL, 0)
    SELECT @MedEgreso      = rda.sf_rda_OrdenesMedicasMedicamentos(@Ingreso, @IPCODPACI, @NUMEFOLIO, NULL, 0)

    BEGIN TRY

		SELECT

			/* =========================================================
				NIT Empresa
				========================================================= */
			@NitEmpresa AS RDA_NIT_Empresa,
			(SELECT IIF(ESTPACEGR = 3, CAST(1 AS BIT), CAST(0 AS BIT)) FROM HCREGEGRE EG WHERE IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_PacienteFallecido,
			(SELECT IIF(ESTPACEGR = 3, FORMAT(EG.FECMUEPAC, 'dd/MM/yyy HH:mm'), NULL) FROM HCREGEGRE EG WHERE IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_FechaPacienteFallecido,

			/* =========================================================
               Identificación del Prestador de Servicios de Salud
               ========================================================= */
            cen.CODIPSSEC AS RDA_16_PrestadorCodigo,
            cen.NOMCENATE AS RDA_16_PrestadorNombre,

			/* =========================================================
               Entidad responsable por el plan de beneficios
               ========================================================= */
            hea.Code AS RDA_15_EAPBCodigo,
            hea.Name AS RDA_15_EAPBNombre,

			/* =========================================================
               Identificación del Paciente
               ========================================================= */
            td.CODIGO AS RDA_2_1_TipoDocumentoCodigo,
            td.NOMBRE AS RDA_2_1_TipoDocumentoNombre,
            p.IPCODPACI AS RDA_2_2_NumeroDocumento,
            p.IPPRIAPEL AS RDA_3_1_PrimerApellido,
            p.IPSEGAPEL AS RDA_3_2_SegundoApellido,
            p.IPPRINOMB AS RDA_3_3_PrimerNombre,
            p.IPSEGNOMB AS RDA_3_4_SegundoNombre,
            p.IPFECNACI AS RDA_4_FechaHoraNacimiento,
            cNat.Code AS RDA_1_1_PaisNacionalidadCodigo,
            cNat.Name AS RDA_1_2_PaisNacionalidadNombre,
            p.IPSEXOPAC AS RDA_5_SexoBiologicoCodigo,
            CASE p.IPSEXOPAC WHEN 1 THEN 'Hombre' WHEN 2 THEN 'Mujer' ELSE NULL END AS RDA_5_SexoBiologicoNombre,
            gt.Code AS RDA_6_IdentidadGeneroCodigo,
            gt.Name AS RDA_6_IdentidadGeneroNombre,   

			/* =========================================================
				Comunidad étnica (ajuste solicitado)
				========================================================= */
            ge.CODGRUPOE AS RDA_13_1_EtniaCodigo,
            ge.DESGRUPET AS RDA_13_1_EtniaNombre,

			p.EthnicCommunity AS RDA_13_2_ComunidadEtnica,

            dc.DISCCODIGO AS RDA_10_DiscapacidadCodigo,
            dc.DISCDESCRI AS RDA_10_DiscapacidadNombre,

			/* =========================================================
				Ocupación
				========================================================= */			
			act.CODACTIVI AS RDA_7_1_OcupacionCodigo,
			act.DESACTIVI AS RDA_7_2_OcupacionNombre,

			/* =========================================================
				Residencia habitual
				========================================================= */
            adr.PaisCodigo AS RDA_11_1_PaisResidenciaCodigo,
            adr.PaisNombre AS RDA_11_2_PaisResidenciaNombre,
            adr.MunicipioCodigo AS RDA_12_1_MunicipioResidenciaCodigo,
            adr.MunicipioNombre AS RDA_12_2_MunicipioResidenciaNombre,
            adr.ZonaCodigo AS RDA_14_ZonaResidenciaCodigo,
            adr.ZonaNombre AS RDA_14_ZonaResidenciaNombre,

			/* =========================================================
				Datos atención (pendientes)
				========================================================= */
            hin.FECINIATE AS RDA_17_FechaHoraInicioAtencion,
            (
                -- Fecha de la HC del folio actual con INDICAPAC de traslado o egreso
                SELECT TOP 1 HC.FECHISPAC
                FROM dbo.HCHISPACA HC
                WHERE HC.IPCODPACI = his.IPCODPACI
                  AND HC.NUMINGRES = his.NUMINGRES
                  AND HC.NUMEFOLIO = his.NUMEFOLIO
                  AND HC.INDICAPAC IN ('1','3','4','5','6','7','8','9','10','11','12','15','16','17','19','20','21')
                ORDER BY HC.FECHISPAC DESC
            ) AS RDA_43_FechaHoraFinAtencion,
			am.Code as RDA_18_1_ModalidadAtencionCodigo,
            am.Name AS RDA_18_1_ModalidadAtencionNombre,
			rda.Service_Group(Inu.UFUTIPUNI) AS RDA_18_2_GrupoServiciosNombre,

			CASE ing.CareSettingCode WHEN 1 THEN '01: Hogar' WHEN 2 THEN '02: Comunitario' WHEN 3 THEN '03: Escolar' 
			WHEN 4 THEN '04: Laboral' WHEN 5 THEN '05: Institucional' ELSE 'No aplica' END AS RDA_19_EntornoAtencion,

			ER.Id AS RDA_20_ViaIngresoCode,
			ER.Name AS RDA_20_ViaIngresoNombre,

		AC.Code AS RDA_21_CausaAtencionCode,
		AC.Name AS RDA_21_CausaAtencionName,

		/* =========================================================
			Triage
			========================================================= */
		tri.TRIAGECLA AS RDA_22_1_PrimerTriageClasificacionCodigo,
		CASE tri.TRIAGECLA
			WHEN 1 THEN '1: Emergencia'
			WHEN 2 THEN '2: Urgencia Médica'
			WHEN 3 THEN '3: Urgencia Diferida'
			WHEN 4 THEN '4: No Urgente'
		END           AS RDA_22_2_PrimerTriageClasificacionNombre,
		tri.TRIAFECHA AS RDA_22_3_PrimerTriageFechaHora,

		/* =========================================================
			Diagnósticos 
			========================================================= */
			IND.CODDIAGNO  AS RDA_23_1_DxPrincipalCIE10Codigo,
            IND.NOMDIAGNO AS RDA_23_2_DxPrincipalCIE10Nombre,

			INP.TIPDIAGNO  AS RDA_23_3_TipoDxPrincipalCodigo,
            CASE INP.TIPDIAGNO WHEN 'I' THEN 'Impresion Diagnostica' WHEN 'C' THEN 'Confirmado Nuevo' WHEN 'R' THEN 'Confirmado Repetido' END  AS RDA_23_3_TipoDxPrincipalNombre,

			/* =========================================================
               Antecedentes / Medicamentos / Profesional
               ========================================================= */
            al.AlergiasJson AS RDA_47_1_2_TieneAlergia,
			fam.FamiliaresJson AS RDA_47_3_4_AntecedentesFamiliares,
			fr.FactoresRiesgoJson AS RDA_48_1_2_FactoresRiesgo,

			/* =========================================================
               Tecnologias de salud en Urgencias(Órdenes médicas)
               Rango: desde el primer folio del ingreso hasta el folio anterior al actual
               ========================================================= */
			@ProcTranscurso AS RDA_24_1_2_3_25_33_36_1_2_39_1_Procedimientos_Tecnologias_Salud_Urgencias,

			/* =========================================================
               Tecnologias de salud en Urgencias(Medicamentos)
               Rango: desde el primer folio del ingreso hasta el folio anterior al actual
               ========================================================= */
			@MedTranscurso AS RDA_24_1_2_3_26_27_28_1_2_29_31_1_2_32_1_2_Medicamentos_Tecnologias_Salud_Urgencias,

			/* =========================================================
               Otras tecnologias de salud en Urgencias(Órdenes médicas->dispositivos médicos->servicio complementeario,etc)
               ========================================================= */
			oth.OtrasTecnologiasJson AS RDA_24_1_2_3_27_33_36_1_2_25_Otras_Tecnologias_Salud_Urgencias,

			/* =========================================================
               Diagnosticos al egreso
               ========================================================= */
			IND.CODDIAGNO AS RDA_37_1_DxPrincipalCIE10Codigo,
			IND.NOMDIAGNO AS RDA_37_2_DxPrincipalCIE10Nombre,
			INP.TIPDIAGNO AS RDA_37_3_TipoDxPrincipalCodigo,
			dr.DiagnosticosRelacionadosJson AS RDA_38_1_2_CodigoNombreDiagnosticoRelacionado,

			(SELECT RTRIM(EG.CODDIAGNOCOMPLICACION) FROM HCREGEGRE EG WHERE IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_40_1_CodigoDiagnosticoComplicacion,
			(SELECT RTRIM(NOMDIAGNO) from HCREGEGRE EG LEFT OUTER JOIN INDIAGNOS A ON EG.CODDIAGNOCOMPLICACION = A.CODDIAGNO 
				WHERE EG.IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_40_2_NombreDiagnosticoComplicacion,

			(SELECT CASE RE.ESTPACEGR WHEN 1 THEN '1: Mejor' WHEN 2 THEN '2: Igual o Peor' WHEN 3 THEN '3: Fallecido' WHEN 4 THEN '4: Remitido' 
			WHEN 5 THEN '5: Hospitalizacion en Casa' END + ' - ' + dbo.DestinoPaciente(his.INDICAPAC) FROM HCREGEGRE RE WHERE RE.IPCODPACI = his.IPCODPACI AND RE.NUMINGRES = his.NUMINGRES) AS RDA_41_CondicionDestinoEgreso,

			(SELECT C.CODDIAGNO + ' - ' + C.NOMDIAGNO from HCREGEGRE A INNER JOIN HCHISPACA B ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO
			LEFT OUTER JOIN INDIAGNOS C ON B.CODDIAGNO = C.CODDIAGNO WHERE A.ESTPACEGR = 3 AND A.IPCODPACI = his.IPCODPACI) AS RDA_42_1_2_CodigoNombreDiagnosticoMuerte,

			/* =========================================================
               Prestador remisión
               ========================================================= */
			(SELECT E.AIPSREMIS FROM HCREGEGRE E WHERE E.ESTPACEGR = 4 AND E.IPCODPACI = his.IPCODPACI AND E.NUMINGRES = his.NUMINGRES AND E.NUMEFOLIO = his.NUMEFOLIO) AS RDA_44_CodigoPrestadorRemision,

			/* =========================================================
               Tecnologias de salud al egreso(Órdenes médicas)
               Rango: únicamente el folio actual
               ========================================================= */
			@ProcEgreso AS RDA_24_1_2_3_25_33_36_1_2_39_1_Procedimientos_Tecnologias_Salud_Egreso,

			/* =========================================================
               Tecnologias de salud al egreso(Medicamentos)
               Rango: únicamente el folio actual
               ========================================================= */
			@MedEgreso AS RDA_24_1_2_3_26_27_28_1_2_29_31_1_2_32_1_2_Medicamentos_Tecnologias_Salud_Egreso,

			/* =========================================================
               Otras tecnologias de salud al egreso(Órdenes médicas->dispositivos médicos->servicio complementeario,etc)
               ========================================================= */
			ote.OtrasTecnologiasJson AS RDA_24_1_2_3_27_33_36_1_2_25_Otras_Tecnologias_Salud_Egreso,

			/* =========================================================
               Incapacidades
               ========================================================= */
			inc.IncapacidadesJson AS RDA_45_1_2_46_Incapacidades,

			/* =========================================================
               Egreso
               ========================================================= */
			CAST(prof.IDADTIPOIDENTIFICA AS varchar(20))  AS RDA_49_1_ProfTipoDocumentoCodigo,
            CAST(tdProf.NOMBRE AS varchar(100)) AS RDA_49_1_ProfTipoDocumentoNombre,
			CAST(COALESCE(profEst.CODIGONIT,  prof.CODIGONIT)  AS varchar(25))  AS RDA_49_2_ProfNumeroDocumento,
			CAST(COALESCE(profEst.MEDPRINOM,  prof.MEDPRINOM)  AS varchar(100)) AS RDA_49_3_ProfPrimerNombre,
			CAST(COALESCE(profEst.MEDSEGNOM,  prof.MEDSEGNOM)  AS varchar(100)) AS RDA_49_4_ProfSegundoNombre,
			CAST(COALESCE(profEst.MEDPRIAPEL, prof.MEDPRIAPEL) AS varchar(100)) AS RDA_49_5_ProfPrimerApellido,
			CAST(COALESCE(profEst.MEDSEGAPEL, prof.MEDSEGAPEL) AS varchar(100)) AS RDA_49_6_ProfSegundoApellido

			FROM HCHISPACA his			
			-- HCURGING1 solo existe en el primer folio de urgencias; se busca el primer registro
			-- disponible para obtener FECINIATE (fecha de inicio de atención) en folios de continuación.
			OUTER APPLY (
				SELECT TOP 1 hin_inner.FECINIATE
				FROM dbo.HCURGING1 hin_inner
				WHERE hin_inner.IPCODPACI = his.IPCODPACI
				  AND hin_inner.NUMINGRES = his.NUMINGRES
				ORDER BY TRY_CAST(RTRIM(hin_inner.NUMEFOLIO) AS INT) ASC
			) hin
			INNER JOIN dbo.INPACIENT p ON p.IPCODPACI = his.IPCODPACI
			INNER JOIN dbo.ADACTIVID act ON p.CODACTIVI = act.codactivi
			INNER JOIN dbo.ADINGRESO ing ON ing.NUMINGRES = his.NUMINGRES
			INNER JOIN dbo.ADCENATEN cen ON his.CODCENATE = cen.CODCENATE
			INNER JOIN dbo.INUNIFUNC Inu ON his.UFUCODIGO = inu.UFUCODIGO
			INNER JOIN Admissions.AdmissionModalities am ON ing.IdAdmissionModalities = am.Code
			INNER JOIN dbo.INPROFSAL prof ON his.CODPROSAL =  prof.CODPROSAL
			INNER JOIN dbo.EntryRoutesHealthServices ER ON ER.Id = ing.IdEntryRoutesHealthServices
			INNER JOIN dbo.Causesofattention AC ON ing.ICAUSAING = AC.Code
			INNER JOIN dbo.INDIAGNOS IND ON IND.CODDIAGNO = HIS.CODDIAGNO
			LEFT  JOIN dbo.INDIAGNOH INP ON IND.CODDIAGNO = INP.CODDIAGNO AND INP.NUMINGRES = ING.NUMINGRES AND INP.NUMEFOLIO = his.NUMEFOLIO AND INP.CODDIAPRI = 1
			LEFT  JOIN Contract.HealthAdministrator hea ON hea.Id = ing.GENCONENTITY
			LEFT  JOIN dbo.ADTIPOIDENTIFICA td ON td.CODIGO = p.IPTIPODOC
			LEFT  JOIN dbo.ADTIPOIDENTIFICA tdProf ON tdProf.ID = prof.IDADTIPOIDENTIFICA
			LEFT  JOIN Common.Country cNat ON cNat.Id = p.IDPAIS
			LEFT  JOIN Admissions.GenderTypes gt ON gt.Id = p.IdGenderIdentity
			LEFT  JOIN dbo.ADGRUETNI ge ON ge.CODGRUPOE = p.CODGRUPOE		
			LEFT  JOIN dbo.ADDISCAPACI dc ON dc.DISCCODIGO = p.DISCCODIGO

			OUTER APPLY
			(
				SELECT TOP (1)
					E.Code AS PaisCodigo,
					E.Name AS PaisNombre,
					C.DEPMUNCOD AS MunicipioCodigo,
					C.MUNNOMBRE AS MunicipioNombre,
					A.RuralArea AS ZonaCodigo,
					CASE WHEN A.RuralArea = '01' THEN 'Rural' ELSE 'Urbana' END AS ZonaNombre
				FROM Admissions.PatientAddress A					
					INNER JOIN INUBICACI B ON A.IdUbication = B.ID
					INNER JOIN INMUNICIP C ON B.DEPMUNCOD = C.DEPMUNCOD
					INNER JOIN INDEPARTA D ON C.DEPCODIGO = D.DEPCODIGO
					INNER JOIN Common.Country E ON D.IDPAIS = E.ID
				WHERE A.IPCODPACI = p.IPCODPACI AND A.IsMain = 1
				ORDER BY A.Id DESC
			) adr

			OUTER APPLY
			(
				SELECT
					(
						SELECT
							CASE WHEN m.IdAllergyType = 1 THEN COALESCE(NULLIF(LTRIM(RTRIM(m.CODPRODUC)), ''), 'NO_ESPECIFICADO') ELSE 'NO_APLICA' END AS Codigo,		
							COALESCE(NULLIF(LTRIM(RTRIM(m.Allergen)), ''), 'NO_ESPECIFICADO') AS Descripcion		
						FROM dbo.HCMEDRIES m						
						WHERE m.IPCODPACI = p.IPCODPACI AND m.NUMINGRES = his.NUMINGRES AND m.ALERGICO = 1 AND m.TIPOREGISTRO = 1 AND m.IDHCMOANULB IS NULL
						ORDER BY m.FECREGIST DESC
						FOR JSON PATH
					) AS AlergiasJson
			) al

			OUTER APPLY
			(
				SELECT
					(
						SELECT
							RTRIM(d.CODDIAGNO) + ' - ' + RTRIM(d.NOMDIAGNO) AS CodigoDescDiagnosticoAntFamiliar,	
							CASE FMH.Relationship WHEN 1 THEN 'Padres' WHEN 2 THEN 'Hermanos' WHEN 3 THEN 'Tios' WHEN 4 THEN 'Abuelos' ELSE 'NO APLICA' END AS ParentescoAntFamiliar	
						FROM MedicalHistory.FamilyMedicalHistory FMH
						INNER JOIN dbo.INDIAGNOS d ON FMH.DiagnosticCode = d.CODDIAGNO
						WHERE FMH.PatientCode = his.IPCODPACI AND FMH.AdmissionNumber = his.NUMINGRES AND FMH.IDHCMOANULB IS NULL 
						ORDER BY FMH.DateRegistration DESC
						FOR JSON PATH
					) AS FamiliaresJson
			) fam

			OUTER APPLY
			(
				SELECT
					(
					SELECT
						C.Code AS CodigoTipoFactor, RTRIM(C.Name) AS NombreTipoFactor,		
						B.Code AS CodigoFactorRiesgo, RTRIM(B.Description) AS NombreFactorRiesgo
					FROM dbo.ReportRiskFactors A
					INNER JOIN dbo.RiskFactor B ON A.IdRiskFactor = B.Id
					LEFT JOIN Admissions.RisksType C ON B.TypeOfRisk = C.Id
					WHERE A.IPCODPACI = p.IPCODPACI
					ORDER BY A.DateCreation DESC
					FOR JSON PATH
					) AS FactoresRiesgoJson
			) fr

		-- Otras tecnologías: transcurso (rango de folios de urgencias anteriores al actual)
		OUTER APPLY
		(
			SELECT
				(
					SELECT
						'Dispositivo médico' AS TipoOtrasTecnologias, RTRIM(D.CODPRODUC) AS CodigoOtrasTecnologias, RTRIM(B.DESPRODUC) AS NombreOtrasTecnologias,		
						C.FECHAORDE AS FechaPrescripcionOtrasTecnologias, G.FECHAUTIL AS FechaAdministracionOtrasTecnologias,
						[dbo].[TipoDocumento](PA.IDADTIPOIDENTIFICA) AS TipoDocProfesionalOtrasTecnologias, RTRIM(G.CODPROSAL) AS IdentificacionProfOtrasTecnologias, '2. Terapéutico' As FinalidadOtrasTecnologias
					FROM dbo.HCSOLINSC C
					INNER JOIN dbo.HCSOLINSD D ON C.ID = D.IDHCSOLINSC
					INNER JOIN DBO.IHLISTPRO B ON D.CODPRODUC = B.CODPRODUC
					LEFT JOIN dbo.HCHOGASIN G ON D.IPCODPACI = G.IPCODPACI AND D.NUMINGRES = G.NUMINGRES AND D.CODCENATE = G.CODCENATE AND D.UFUCODIGO = G.UFUCODIGO AND D.CODPRODUC = G.CODPRODUC
					LEFT JOIN dbo.INPROFSAL PA ON G.CODPROSAL = PA.CODPROSAL	
					LEFT JOIN dbo.INPROFSAL PO ON D.CODPROSAL = PO.CODPROSAL
					WHERE ISNULL(D.MANEJOEXTRA, 0) = 0
					  AND C.IPCODPACI = his.IPCODPACI
					  AND C.NUMINGRES = his.NUMINGRES
					  AND C.NUMEFOLIO >= @PrimerFolio
					  AND (@FolioAnterior IS NULL OR C.NUMEFOLIO <= @FolioAnterior)
					ORDER BY C.FECHAORDE DESC
					FOR JSON PATH
				) AS OtrasTecnologiasJson
		) oth

		-- Otras tecnologías: egreso (folio actual)
		OUTER APPLY
		(
			SELECT
				(
					SELECT
						'Dispositivo médico' AS TipoOtrasTecnologias, RTRIM(D.CODPRODUC) AS CodigoOtrasTecnologias, RTRIM(B.DESPRODUC) AS NombreOtrasTecnologias,		
						C.FECHAORDE AS FechaPrescripcionOtrasTecnologias, G.FECHAUTIL AS FechaAdministracionOtrasTecnologias,
						[dbo].[TipoDocumento](PA.IDADTIPOIDENTIFICA) AS TipoDocProfesionalOtrasTecnologias, RTRIM(G.CODPROSAL) AS IdentificacionProfOtrasTecnologias, '2. Terapéutico' As FinalidadOtrasTecnologias
					FROM dbo.HCSOLINSC C
					INNER JOIN dbo.HCSOLINSD D ON C.ID = D.IDHCSOLINSC
					INNER JOIN DBO.IHLISTPRO B ON D.CODPRODUC = B.CODPRODUC
					LEFT JOIN dbo.HCHOGASIN G ON D.IPCODPACI = G.IPCODPACI AND D.NUMINGRES = G.NUMINGRES AND D.CODCENATE = G.CODCENATE AND D.UFUCODIGO = G.UFUCODIGO AND D.CODPRODUC = G.CODPRODUC					
					LEFT JOIN dbo.INPROFSAL PA ON G.CODPROSAL = PA.CODPROSAL	
					LEFT JOIN dbo.INPROFSAL PO ON D.CODPROSAL = PO.CODPROSAL
					WHERE ISNULL(D.MANEJOEXTRA, 0) = 0
					  AND C.IPCODPACI = his.IPCODPACI
					  AND C.NUMINGRES = his.NUMINGRES
					  AND C.NUMEFOLIO = his.NUMEFOLIO
					ORDER BY C.FECHAORDE DESC
					FOR JSON PATH
				) AS OtrasTecnologiasJson
		) ote

		OUTER APPLY
		(
			SELECT
				(
					SELECT DISTINCT
						diag.CODDIAGNO AS CodigoDiagnosticoRelSalud, RTRIM(diag.NOMDIAGNO) AS NombreDiagnosticoRelSalud
					FROM dbo.INDIAGNOP INPN
					INNER JOIN dbo.INDIAGNOS diag ON diag.CODDIAGNO = INPN.CODDIAGNO
					WHERE INPN.IPCODPACI = his.IPCODPACI
					  AND INPN.NUMINGRES = his.NUMINGRES
					  AND INPN.NUMEFOLIO = his.NUMEFOLIO
					  AND INPN.CODDIAPRI = 0
					FOR JSON PATH
				) AS DiagnosticosRelacionadosJson
		) dr

		OUTER APPLY
			(
				SELECT
					(
						SELECT 
							CASE DisabilityClass WHEN 1 THEN '1. Licencia de maternidad y de paternidad (Certificado de licencia de maternidad y parentales)'
							WHEN 2 THEN '2. Incapacidad de origen común (Certificado de incapacidad)'
							WHEN 3 THEN '3. Certificado médico de la licencia para el cuidado de la niñez'
							WHEN 4 THEN '4. Certificado de otras licencias derivadas del proceso gestacional'
							END AS TipoIncapacidad, NUMDIAINC AS DiasIncapacidad
						FROM dbo.HCINCAPAC INC
						WHERE INC.IPCODPACI = his.IPCODPACI AND INC.NUMINGRES = his.NUMINGRES
						FOR JSON PATH
					) AS IncapacidadesJson
			) inc

		OUTER APPLY
		(
			SELECT TOP 1 TRIAGECLA, TRIAFECHA
			FROM dbo.ADTRIAGEU
			WHERE IPCODPACI = his.IPCODPACI
			  AND NUMINGRES = his.NUMINGRES
			ORDER BY TRIAFECHA ASC
		) tri
		  OUTER APPLY (
			SELECT TOP 1
				ps.CODIGONIT,
				ps.MEDPRINOM,
				ps.MEDSEGNOM,
				ps.MEDPRIAPEL,
				ps.MEDSEGAPEL
			FROM dbo.CHREGESTA cr
			INNER JOIN dbo.INPROFSAL ps ON ps.CODPROSAL = cr.CODPROSAL
			WHERE cr.NUMINGRES = his.NUMINGRES
			  AND cr.REGESTADO = 2
			ORDER BY cr.FECFINEST DESC
		) profEst

			WHERE 
				his.IPCODPACI = @IPCODPACI and
				his.NUMINGRES = @Ingreso and
				his.ID = @IdHispaca

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();
        RAISERROR('sp_rda_Urgencias falló. Detalle: %s', 16, 1, @ErrorMessage);
    END CATCH
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento almacenado que consolida y retorna el conjunto completo de datos clínicos, administrativos y de facturación requeridos para el reporte RDA (Registro de Atención) de urgencias. Extrae información del paciente, prestador, EAPB, triage, diagnósticos CIE-10, tecnologías en salud (procedimientos y medicamentos) diferenciadas por rango de folios durante la urgencia y al egreso, condición de salida, remisiones, incapacidades y antecedentes, filtrando únicamente unidades funcionales de tipo urgencias.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el dataset RDA (Registro de Datos de Atención) de un episodio de Urgencias para un paciente/ingreso/folio específico, consolidando identificación, diagnósticos, triage, tecnologías de salud durante el transcurso y al egreso, antecedentes y condiciones de egreso.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir filas en HCHISPACA para el paciente e ingreso indicados, vinculadas a una unidad funcional con UFUTIPUNI=1 (urgencias), para calcular el rango de folios.; Debe existir un registro en HCHISPACA cuyo ID coincida con @IdHispaca y corresponda al @IPCODPACI y @Ingreso.; Debe existir un HCURGING1 asociado al folio (his.NUMEFOLIO) para que el INNER JOIN devuelva resultados.; El ingreso debe estar registrado en ADINGRESO con modalidad de admisión, vía de ingreso y causa de atención válidas (INNER JOIN con AdmissionModalities, EntryRoutesHealthServices, Causesofattention).; El diagnóstico de la HC (HIS.CODDIAGNO) debe existir en INDIAGNOS.; Debe existir al menos una fila en INEMPRESU para obtener el NIT de la empresa.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de folios para tecnologías ''en transcurso'' siempre excluye el folio actual (usa @PrimerFolio … @FolioAnterior, donde @FolioAnterior < @NUMEFOLIO).; Las tecnologías ''al egreso'' siempre se calculan exclusivamente sobre el folio actual (@NUMEFOLIO).; Solo se contabilizan folios pertenecientes a unidades funcionales de urgencias (UFUTIPUNI=1) para delimitar el episodio.; La dirección de residencia usada es la marcada como principal (IsMain=1) y la más reciente por Id descendente.; Las alergias incluidas son únicamente las activas no anuladas (ALERGICO=1, TIPOREGISTRO=1, IDHCMOANULB IS NULL).; Los antecedentes familiares listados excluyen registros anulados (IDHCMOANULB IS NULL).; El triage reportado es el primero (mínima TRIAFECHA) del ingreso del paciente.; Los dispositivos médicos en ''Otras tecnologías'' siempre se reportan con finalidad fija ''2. Terapéutico''.; La salida está siempre filtrada por his.IPCODPACI=@IPCODPACI, his.NUMINGRES=@Ingreso e his.ID=@IdHispaca, garantizando un único folio.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto de resultados con todos los campos RDA del episodio de urgencias del folio indicado (@IdHispaca, @IPCODPACI, @Ingreso).; [RAISERROR] N/A: En caso de excepción captura ERROR_MESSAGE() y lanza RAISERROR severidad 16: ''sp_rda_Urgencias falló. Detalle: %s''.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HC.NUMEFOLIO < @NUMEFOLIO al calcular @FolioAnterior → Toma el MAX de folios anteriores como FolioAnterior; si no hay folios previos, FolioAnterior queda NULL y los rangos de transcurso usan solo (@FolioAnterior IS NULL OR ...) else Cuando @FolioAnterior es NULL, las consultas de ''Otras tecnologías en transcurso'' incluyen todos los folios desde @PrimerFolio sin tope superior.; si ESTPACEGR = 3 en HCREGEGRE → Marca RDA_PacienteFallecido = 1 y expone FECMUEPAC formateada como RDA_FechaPacienteFallecido; además permite obtener el diagnóstico de muerte (RDA_42) uniendo HCREGEGRE-HCHISPACA-INDIAGNOS. else RDA_PacienteFallecido = 0 y RDA_FechaPacienteFallecido = NULL.; si ESTPACEGR = 4 en HCREGEGRE para el folio actual → Expone AIPSREMIS como RDA_44_CodigoPrestadorRemision (paciente remitido). else RDA_44 queda NULL.; si INDICAPAC IN (''1'',''3'',''4'',''5'',''6'',''7'',''8'',''9'',''10'',''11'',''12'',''15'',''16'',''17'',''19'',''20'',''21'') → Selecciona la última FECHISPAC del folio actual como RDA_43_FechaHoraFinAtencion (traslado o egreso). else RDA_43 queda NULL si ningún registro cumple.; si UF.UFUTIPUNI = 1 → Solo se consideran folios de unidades funcionales tipificadas como urgencias para construir el rango [@PrimerFolio, @FolioAnterior].; si m.IdAllergyType = 1 en HCMEDRIES → Toma el código de producto (CODPRODUC) como código de alergia; en otro caso, fija ''NO_APLICA''.; si A.RuralArea = ''01'' → Clasifica la zona de residencia como ''Rural''. else Clasifica la zona como ''Urbana''.; si INP.CODDIAPRI = 1 → Considera el diagnóstico como principal (RDA_23 / RDA_37); con CODDIAPRI = 0 se incluyen como diagnósticos relacionados (RDA_38).; si ISNULL(D.MANEJOEXTRA,0) = 0 → Incluye el dispositivo médico en ''Otras tecnologías'' del transcurso o egreso; los marcados como manejo extra se excluyen.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'rda.Service_Group; rda.sf_rda_OrdenesMedicasProcedimientos; rda.sf_rda_OrdenesMedicasMedicamentos; dbo.DestinoPaciente; dbo.TipoDocumento', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Urgencias';
-- GO
