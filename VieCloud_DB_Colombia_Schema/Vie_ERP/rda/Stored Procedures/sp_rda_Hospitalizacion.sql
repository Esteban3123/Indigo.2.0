CREATE   PROCEDURE [rda].[sp_rda_Hospitalizacion]
(
    @IPCODPACI varchar(50),
	@Ingreso varchar(10),
	@NUMEFOLIO varchar(10),
    @IdHispaca INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PrimerFolio   VARCHAR(10)
    DECLARE @FolioAnterior VARCHAR(10)

    SELECT
        @PrimerFolio   = CAST(MIN(TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT)) AS VARCHAR(10)),
        @FolioAnterior = CAST(MAX(TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT)) AS VARCHAR(10))
    FROM dbo.HCHISPACA HC
    INNER JOIN dbo.INUNIFUNC UF ON UF.UFUCODIGO = HC.UFUCODIGO
    WHERE HC.NUMINGRES = @Ingreso
      AND HC.IPCODPACI = @IPCODPACI
      AND UF.UFUTIPUNI IN (2,5,6,7,8,9,10,11,12,13,16,17,18,19,23)
	  AND TRY_CAST(RTRIM(HC.NUMEFOLIO) AS INT) < TRY_CAST(RTRIM(@NUMEFOLIO) AS INT)

    DECLARE @NitEmpresa VARCHAR(20)
    SELECT @NitEmpresa = INDNITEMP FROM INEMPRESU

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
            (SELECT TOP 1 FECHISPAC FROM HCHISPACA h WHERE h.IPCODPACI = his.IPCODPACI AND h.NUMINGRES = his.NUMINGRES AND h.TIPHISPAC = 'T' ORDER BY FECHISPAC ASC) AS RDA_17_FechaHoraInicioAtencion,
            (SELECT FECALTPAC FROM HCREGEGRE EG WHERE IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_43_FechaHoraFinAtencion,  

			am.Code as RDA_18_1_ModalidadAtencionCodigo,
            am.Name AS RDA_18_1_ModalidadAtencionNombre,

            rda.Service_Group(un.UFUTIPUNI) AS RDA_18_2_GrupoServiciosNombre,

			CASE ing.CareSettingCode WHEN 1 THEN '01: Hogar' WHEN 2 THEN '02: Comunitario' WHEN 3 THEN '03: Escolar' 
			WHEN 4 THEN '04: Laboral' WHEN 5 THEN '05: Institucional' ELSE 'No aplica' END AS RDA_19_EntornoAtencion,

			ER.Id AS RDA_20_ViaIngresoCode,
			ER.Name AS RDA_20_ViaIngresoNombre,

			AC.Code AS RDA_21_CausaAtencionCode,
			AC.Name AS RDA_21_CausaAtencionName,

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
            al.AlergiasJson RDA_47_1_2_TieneAlergia,
			fam.FamiliaresJson RDA_47_3_4_AntecedentesFamiliares,
			fr.FactoresRiesgoJson RDA_48_1_2_FactoresRiesgo,       

			/* =========================================================
               Tecnologias de salud en Hospitalizacion(Órdenes médicas)
               ========================================================= */
			@ProcTranscurso AS RDA_24_1_2_3_25_33_36_1_2_39_1_Procedimientos_Tecnologias_Salud_Hospitalizacion,
            
			/* =========================================================
               Tecnologias de salud en Hospitalizacion(Medicamentos)
               Rango: desde el primer folio de hospitalizacion hasta el folio anterior al actual
               ========================================================= */
			@MedTranscurso AS RDA_24_1_2_3_26_27_28_1_2_29_31_1_2_32_1_2_Medicamentos_Tecnologias_Salud_Hospitalizacion,

			/* =========================================================
               Otras tecnologias de salud en Hospitalizacion(Órdenes médicas->dispositivos médicos->servicio complementeario,etc)
               ========================================================= */
			oth.OtrasTecnologiasJson AS RDA_24_1_2_3_27_33_36_1_2_25_Otras_Tecnologias_Salud_Hospitalizacion,

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
			WHEN 5 THEN '5: Hospitalizacion en Casa' END + ' - ' + dbo.DestinoPaciente(his.INDICAPAC) from HCREGEGRE RE WHERE RE.IPCODPACI = his.IPCODPACI AND RE.NUMINGRES = his.NUMINGRES) AS RDA_41_CondicionDestinoEgreso,

			(SELECT C.CODDIAGNO + ' - ' + C.NOMDIAGNO from HCREGEGRE A INNER JOIN HCHISPACA B ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO
			LEFT OUTER JOIN INDIAGNOS C ON B.CODDIAGNO = C.CODDIAGNO WHERE A.ESTPACEGR = 3 AND A.IPCODPACI = his.IPCODPACI) AS RDA_42_1_2_CodigoNombreDiagnosticoMuerte,

			/* =========================================================
               Prestador remisión
               ========================================================= */
			(SELECT E.AIPSREMIS FROM HCREGEGRE E WHERE E.ESTPACEGR = 4 AND E.IPCODPACI = his.IPCODPACI AND E.NUMINGRES = his.NUMINGRES AND E.NUMEFOLIO = his.NUMEFOLIO) AS RDA_44_CodigoPrestadorRemision,

			/* =========================================================
               Tecnologias de salud al egreso(Órdenes médicas)
               ========================================================= */
			@ProcEgreso AS RDA_24_1_2_3_25_33_36_1_2_39_1_Procedimientos_Tecnologias_Salud_Egreso,
            
			/* =========================================================
               Tecnologias de salud en egreso(Medicamentos)
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

            CAST(prof.IDADTIPOIDENTIFICA AS varchar(20)) AS RDA_49_1_ProfTipoDocumentoCodigo,
			CAST(tdProf.NOMBRE AS varchar(100)) AS RDA_49_1_ProfTipoDocumentoNombre,
			CAST(prof.CODIGONIT AS varchar(25)) AS RDA_49_2_ProfNumeroDocumento,
			CAST(prof.MEDPRINOM AS varchar(100)) AS RDA_49_3_ProfPrimerNombre,
			CAST(prof.MEDSEGNOM AS varchar(100)) AS RDA_49_4_ProfSegundoNombre,
			CAST(prof.MEDPRIAPEL AS varchar(100)) AS RDA_49_5_ProfPrimerApellido,
			CAST(prof.MEDSEGAPEL AS varchar(100)) AS RDA_49_6_ProfSegundoApellido

        FROM HCHISPACA his
		INNER JOIN dbo.INPACIENT p ON p.IPCODPACI = his.IPCODPACI
		INNER JOIN dbo.ADINGRESO ing ON ing.NUMINGRES = his.NUMINGRES
		INNER JOIN dbo.ADACTIVID act ON p.CODACTIVI = act.codactivi
		INNER JOIN dbo.ADCENATEN cen ON his.CODCENATE = cen.CODCENATE
		INNER JOIN dbo.INUNIFUNC un ON his.UFUCODIGO = un.UFUCODIGO
		INNER JOIN Admissions.AdmissionModalities am ON ing.IdAdmissionModalities = am.Code
		INNER JOIN dbo.INPROFSAL prof ON his.CODPROSAL =  prof.CODPROSAL
		INNER JOIN dbo.EntryRoutesHealthServices ER ON ER.Id = ing.IdEntryRoutesHealthServices
		INNER JOIN dbo.Causesofattention AC ON ing.ICAUSAING = AC.Code
		INNER JOIN dbo.INDIAGNOS IND ON IND.CODDIAGNO = HIS.CODDIAGNO
		INNER JOIN dbo.INDIAGNOH INP ON IND.CODDIAGNO = INP.CODDIAGNO AND INP.NUMINGRES = ING.NUMINGRES AND INP.NUMEFOLIO = his.NUMEFOLIO
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

		-- Otras tecnologías: transcurso (rango de folios intrahospitalarios anteriores al actual)
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

        WHERE 
			his.IPCODPACI = @IPCODPACI
			AND his.NUMINGRES = @Ingreso
			AND his.NUMEFOLIO = @NUMEFOLIO
			AND un.UFUTIPUNI NOT IN (1, 15, 24)

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();
        RAISERROR('sp_rda_Hospitalizacion falló. Detalle: %s', 16, 1, @ErrorMessage);
    END CATCH
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento almacenado que consolida la información clínica y administrativa completa de un episodio de hospitalización para la generación del Registro de Atención en Salud (RDA). Reúne datos del paciente (identificación, demografía, etnia, discapacidad, residencia), del ingreso (modalidad, vía de entrada, causa de atención, entorno), diagnósticos de ingreso y egreso, condición de fallecimiento, tecnologías en salud (procedimientos, medicamentos y otros insumos) durante la hospitalización y al egreso, incapacidades, antecedentes familiares, factores de riesgo, alergias y datos del profesional tratante, calculando el rango de folios intrahospitalarios del ingreso para delimitar las tecnologías reportadas.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el conjunto de datos RDA (Registro de Datos de Atención) para una hospitalización específica, agregando identificación del prestador, paciente, EAPB, diagnósticos, antecedentes, tecnologías de salud (procedimientos, medicamentos, otras) durante el transcurso y al egreso, e incapacidades.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA con la combinación IPCODPACI, NUMINGRES y NUMEFOLIO indicada.; La unidad funcional del folio (HCHISPACA.UFUCODIGO → INUNIFUNC.UFUTIPUNI) NO debe ser de tipo 1, 15 ni 24 (se excluyen explícitamente).; Deben existir filas relacionadas en INPACIENT, ADINGRESO, ADACTIVID, ADCENATEN, INUNIFUNC, AdmissionModalities, INPROFSAL, EntryRoutesHealthServices, Causesofattention, INDIAGNOS e INDIAGNOP (joins INNER).; Debe existir al menos un folio del ingreso con UFUTIPUNI en (2,5,6,7,8,9,10,11,12,13,16,17,18,19,23) para calcular el rango intrahospitalario.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan folios cuya unidad funcional no sea de los tipos 1, 15 ni 24 (excluye unidades no hospitalarias).; El rango intrahospitalario para tecnologías del ''transcurso'' se construye exclusivamente con tipos de unidad funcional intrahospitalarios (2,5,6,7,8,9,10,11,12,13,16,17,18,19,23).; Las alergias incluidas siempre cumplen ALERGICO=1, TIPOREGISTRO=1 e IDHCMOANULB IS NULL (no anuladas).; Los antecedentes familiares incluidos no están anulados (FMH.IDHCMOANULB IS NULL).; La dirección de residencia usada es la marcada como principal (IsMain = 1), tomando la más reciente por Id descendente.; El diagnóstico de muerte se obtiene únicamente cuando el egreso tiene ESTPACEGR = 3.; El prestador de remisión se reporta solo cuando ESTPACEGR = 4 y para el folio actual.; Las tecnologías ''al egreso'' se calculan con el folio actual (@NUMEFOLIO) y FolioAnterior NULL; las del ''transcurso'' con @PrimerFolio..@FolioAnterior.; Toda excepción del bloque principal se transforma en RAISERROR severidad 16 con el mensaje original; no hay manejo silencioso.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve un único resultset con los campos del RDA de hospitalización del folio solicitado, incluyendo agregaciones JSON de alergias, antecedentes familiares, factores de riesgo, otras tecnologías (transcurso/egreso), diagnósticos relacionados e incapacidades.; [RAISERROR] (error): En el bloque CATCH se eleva ''sp_rda_Hospitalizacion falló. Detalle: %s'' con severidad 16 cuando ocurre cualquier excepción durante la consulta.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UF.UFUTIPUNI IN (2,5,6,7,8,9,10,11,12,13,16,17,18,19,23) sobre los folios del ingreso → Calcula @PrimerFolio (MIN) y @FolioAnterior (MAX de folios < @NUMEFOLIO) como rango intrahospitalario para tecnologías del transcurso; si un.UFUTIPUNI NOT IN (1, 15, 24) en el folio actual → Permite construir el resultado; en caso contrario el folio no se considera hospitalización válida y no devuelve filas else No retorna registros para ese folio; si HCREGEGRE.ESTPACEGR = 3 → Marca RDA_PacienteFallecido = 1 y entrega FECMUEPAC formateada; además permite traer diagnóstico de muerte (RDA_42); si HCREGEGRE.ESTPACEGR = 4 → Permite obtener AIPSREMIS como código del prestador de remisión (RDA_44); si CASE sobre HCREGEGRE.ESTPACEGR (1..5) → Mapea condición de destino al egreso a etiquetas ''1: Mejor'', ''2: Igual o Peor'', ''3: Fallecido'', ''4: Remitido'', ''5: Hospitalizacion en Casa''; si CASE INP.TIPDIAGNO (''I'',''C'',''R'') → Traduce tipo de diagnóstico a ''Impresion Diagnostica'', ''Confirmado Nuevo'' o ''Confirmado Repetido''; si CASE p.IPSEXOPAC (1,2) → Traduce sexo biológico a ''Hombre'' o ''Mujer''; otros valores → NULL; si CASE ing.CareSettingCode (1..5) → Mapea entorno de atención a ''01: Hogar'', ''02: Comunitario'', ''03: Escolar'', ''04: Laboral'', ''05: Institucional''; resto → ''No aplica''; si HCMEDRIES.IdAllergyType = 1 → Toma m.CODPRODUC como código de alérgeno; en otro caso devuelve ''NO_APLICA''; si A.RuralArea = ''01'' en PatientAddress → Clasifica la zona como ''Rural''; cualquier otro valor como ''Urbana''; si En subconsulta de otras tecnologías: NUMEFOLIO entre @PrimerFolio y @FolioAnterior (si no es NULL) → Considera dispositivos médicos del transcurso de hospitalización; para egreso usa NUMEFOLIO = folio actual; si ISNULL(D.MANEJOEXTRA,0) = 0 → Solo incluye dispositivos médicos no marcados como manejo extra en las JSON de otras tecnologías', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'rda.sf_rda_OrdenesMedicasProcedimientos; rda.sf_rda_OrdenesMedicasMedicamentos; dbo.DestinoPaciente; dbo.TipoDocumento', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Hospitalizacion';
-- GO
