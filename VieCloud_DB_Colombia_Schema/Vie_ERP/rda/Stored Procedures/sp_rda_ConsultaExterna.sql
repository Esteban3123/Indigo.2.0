CREATE   PROCEDURE [rda].[sp_rda_ConsultaExterna]
(
    @IPCODPACI varchar(50),
	@Ingreso varchar(10),
	@NUMEFOLIO varchar(10),
    @IdHispaca INT
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        SELECT 
			/* =========================================================
               NIT Empresa
               ========================================================= */

			(SELECT INDNITEMP FROM INEMPRESU) AS RDA_NIT_Empresa,
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
				Datos atención (pendientes) - Hospitalizacion
				========================================================= */
            his.FECHISPAC AS RDA_17_FechaHoraInicioAtencion,
            (SELECT FECALTPAC FROM HCREGEGRE EG WHERE IPCODPACI = his.IPCODPACI AND EG.NUMINGRES = his.NUMINGRES) AS RDA_43_FechaHoraFinAtencion,  
			am.Code as RDA_18_1_ModalidadAtencionCodigo,
            am.Name AS RDA_18_1_ModalidadAtencionNombre,

            CAST(NULL AS varchar(50)) AS RDA_18_2_GrupoServiciosCodigo,  --//Preguntar a Claudia dado que no hay conexion aparente
            CAST(NULL AS varchar(100)) AS RDA_18_2_GrupoServiciosNombre, --//Preguntar a Claudia dado que no hay conexion aparente

			CASE ing.CareSettingCode WHEN 1 THEN '01: Hogar' WHEN 2 THEN '02: Comunitario' WHEN 3 THEN '03: Escolar' 
			WHEN 4 THEN '04: Laboral' WHEN 5 THEN '05: Institucional' ELSE 'No aplica' END AS RDA_19_EntornoAtencion,

			ER.Id AS RDA_20_ViaIngresoCode,
			ER.Name AS RDA_20_ViaIngresoNombre,

			AC.Code AS RDA_21_CausaAtencionCode,
			AC.Name AS RDA_21_CausaAtencionName,

			/* =========================================================
               Antecedentes / Medicamentos / Profesional
               ========================================================= */
            al.AlergiasJson RDA_47_1_2_TieneAlergia,
			fam.FamiliaresJson RDA_47_3_4_AntecedentesFamiliares,
			fr.FactoresRiesgoJson RDA_48_1_2_FactoresRiesgo,       

			/* =========================================================
               Diagnosticos al egreso
               ========================================================= */
			IND.CODDIAGNO AS RDA_37_1_DxPrincipalCIE10Codigo,
			IND.NOMDIAGNO AS RDA_37_2_DxPrincipalCIE10Nombre,
			INP.TIPDIAGNO AS RDA_37_3_TipoDxPrincipalCodigo,
			dr.DiagnosticosRelacionadosJson AS RDA_38_1_2_CodigoNombreDiagnosticoRelacionado,

			(SELECT CASE RE.ESTPACEGR WHEN 1 THEN '1: Mejor' WHEN 2 THEN '2: Igual o Peor' WHEN 3 THEN '3: Fallecido' WHEN 4 THEN '4: Remitido' 
			WHEN 5 THEN '5: Hospitalizacion en Casa' END + ' - ' + dbo.DestinoPaciente(his.INDICAPAC) from HCREGEGRE RE WHERE RE.IPCODPACI = his.IPCODPACI AND RE.NUMINGRES = his.NUMINGRES) AS RDA_41_CondicionDestinoEgreso,

			/* =========================================================
               Prestador remisión
               ========================================================= */
			(SELECT E.AIPSREMIS FROM HCREGEGRE E WHERE E.ESTPACEGR = 4 AND E.IPCODPACI = his.IPCODPACI AND E.NUMINGRES = his.NUMINGRES AND E.NUMEFOLIO = his.NUMEFOLIO) AS RDA_44_CodigoPrestadorRemision,

			/* =========================================================
               Tecnologias de salud en Consulta externa(Medicamentos)
               ========================================================= */
			rda.sf_rda_OrdenesMedicasMedicamentos(@Ingreso, @IPCODPACI, @NUMEFOLIO, NULL, 1)  AS RDA_24_1_2_3_26_27_28_1_2_29_31_1_2_32_1_2_Medicamentos_Tecnologias_Salud_Egreso,

			/* =========================================================
               Tecnologias de salud al Consulta externa(Órdenes médicas)
               ========================================================= */
			rda.sf_rda_OrdenesMedicasProcedimientos(@Ingreso, @IPCODPACI, @NUMEFOLIO, NULL, 1) AS RDA_24_1_2_3_25_33_36_1_2_39_1_Procedimientos_Tecnologias_Salud_Egreso,

			/* =========================================================
               Otras tecnologias de salud en Consulta externa(Órdenes médicas->dispositivos médicos->servicio complementeario,etc)
               ========================================================= */
			ot.OtrasTecnologiasJson AS RDA_24_1_2_3_27_33_36_1_2_25_Otras_Tecnologias_Salud_Hospitalizacion,

			/* =========================================================
               Incapacidades
               ========================================================= */
			inc.IncapacidadesJson AS RDA_45_1_2_46_Incapacidades,

            CAST(prof.IDADTIPOIDENTIFICA AS varchar(20))  AS RDA_49_1_ProfTipoDocumentoCodigo,
            CAST(prof.NOMMEDICO AS varchar(100)) AS RDA_49_1_ProfTipoDocumentoNombre,
            CAST(prof.CODIGONIT AS varchar(25))  AS RDA_49_2_ProfNumeroDocumento

			FROM HCHISPACA his WITH (NOLOCK)
			INNER JOIN dbo.INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = his.IPCODPACI
			INNER JOIN dbo.ADACTIVID act ON p.CODACTIVI = act.codactivi
			INNER JOIN dbo.ADINGRESO ing WITH (NOLOCK) ON ing.NUMINGRES = his.NUMINGRES
			INNER JOIN dbo.ADCENATEN cen WITH (NOLOCK) ON his.CODCENATE = cen.CODCENATE
			INNER JOIN Admissions.AdmissionModalities am ON ing.IdAdmissionModalities = am.Code
			INNER JOIN dbo.INPROFSAL prof WITH (NOLOCK) ON his.CODPROSAL =  prof.CODPROSAL
			INNER JOIN dbo.EntryRoutesHealthServices ER ON ER.Id = ing.IdEntryRoutesHealthServices
			INNER JOIN dbo.Causesofattention AC ON ing.ICAUSAING = AC.Code
			INNER JOIN dbo.INDIAGNOS IND ON IND.CODDIAGNO = HIS.CODDIAGNO
			LEFT JOIN dbo.INDIAGNOP INP ON IND.CODDIAGNO = INP.CODDIAGNO AND INP.NUMINGRES = ING.NUMINGRES AND INP.NUMEFOLIO = his.NUMEFOLIO AND INP.CODDIAPRI = 1
			LEFT  JOIN Contract.HealthAdministrator hea WITH (NOLOCK) ON hea.Id = ing.GENCONENTITY
			LEFT  JOIN dbo.ADTIPOIDENTIFICA td WITH (NOLOCK) ON td.CODIGO = p.IPTIPODOC
			LEFT  JOIN Common.Country cNat WITH (NOLOCK) ON cNat.Id = p.IDPAIS
			LEFT  JOIN Admissions.GenderTypes gt WITH (NOLOCK) ON gt.Id = p.IdGenderIdentity
			LEFT  JOIN dbo.ADGRUETNI ge WITH (NOLOCK) ON ge.CODGRUPOE = p.CODGRUPOE		
			LEFT  JOIN dbo.ADDISCAPACI dc WITH (NOLOCK) ON dc.DISCCODIGO = p.DISCCODIGO

			OUTER APPLY
			(
				SELECT TOP (1)
					E.Code AS PaisCodigo,
					E.Name AS PaisNombre,
					C.DEPMUNCOD AS MunicipioCodigo,
					C.MUNNOMBRE AS MunicipioNombre,
					A.RuralArea AS ZonaCodigo,
					CASE WHEN A.RuralArea = '01' THEN 'Rural' ELSE 'Urbana' END AS ZonaNombre
				FROM Admissions.PatientAddress A WITH (NOLOCK)
					INNER JOIN INUBICACI B WITH (NOLOCK) ON A.IdUbication = B.ID
					INNER JOIN INMUNICIP C WITH (NOLOCK) ON B.DEPMUNCOD = C.DEPMUNCOD
					INNER JOIN INDEPARTA D WITH (NOLOCK) ON C.DEPCODIGO = D.DEPCODIGO
					INNER JOIN Common.Country E WITH (NOLOCK) ON D.IDPAIS = E.ID
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
						FROM dbo.HCMEDRIES m WITH (NOLOCK)
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
						INNER JOIN dbo.HCHOGASIN G ON D.IPCODPACI = G.IPCODPACI AND D.NUMINGRES = G.NUMINGRES AND D.CODCENATE = G.CODCENATE AND D.UFUCODIGO = G.UFUCODIGO AND D.CODPRODUC = G.CODPRODUC
						INNER JOIN DBO.IHLISTPRO B ON D.CODPRODUC = B.CODPRODUC
						LEFT JOIN dbo.INPROFSAL PA ON G.CODPROSAL = PA.CODPROSAL	
						LEFT JOIN dbo.INPROFSAL PO ON D.CODPROSAL = PO.CODPROSAL
					WHERE D.MANEJOEXTRA = 1
					  AND C.IPCODPACI = his.IPCODPACI
					  AND C.NUMINGRES = his.NUMINGRES
					  AND C.NUMEFOLIO = his.NUMEFOLIO
					ORDER BY C.FECHAORDE DESC
						FOR JSON PATH
					) AS OtrasTecnologiasJson
			) ot

			OUTER APPLY
			(
				SELECT
					(
						SELECT DISTINCT
							IND.CODDIAGNO AS CodigoDiagnosticoRelSalud, RTRIM(IND.NOMDIAGNO) AS NombreDiagnosticoRelSalud
						FROM dbo.INDIAGNOS IND
						LEFT OUTER JOIN HCHISPACA his ON HIS.CODDIAGNO = IND.CODDIAGNO
						LEFT OUTER JOIN INDIAGNOP INPN ON IND.CODDIAGNO = INPN.CODDIAGNO AND INPN.NUMINGRES = his.NUMINGRES AND INPN.NUMEFOLIO = his.NUMEFOLIO AND INPN.CODDIAPRI = 0
						WHERE INPN.IPCODPACI = his.IPCODPACI AND INPN.NUMINGRES = his.NUMINGRES
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
				SELECT
					(
						SELECT 
							I.SIGLA AS TipoIdentificacion, P.CODPROSAL AS Identificacion
						FROM dbo.HCREGEGRE E
						INNER JOIN INPROFSAL P ON E.CODPROSAL = P.CODPROSAL
						INNER JOIN ADTIPOIDENTIFICA I ON P.IDADTIPOIDENTIFICA = I.ID 
						WHERE E.IPCODPACI = his.IPCODPACI AND E.NUMINGRES = his.NUMINGRES
						FOR JSON PATH
					) AS EgresoJson
			) egr

			WHERE 
				his.IPCODPACI = @IPCODPACI and
				his.NUMINGRES = @Ingreso and
				his.ID = @IdHispaca

    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();
        RAISERROR('sp_rda_ConsultaExterna falló. Detalle: %s', 16, 1, @ErrorMessage);
    END CATCH
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento almacenado que consolida los datos clínicos, demográficos y administrativos de una atención de consulta externa para generar el Registro de Atención en Salud (RDA), requerido por la normativa colombiana de RIPS. Recupera información del paciente, prestador, EAPB, diagnósticos CIE-10, modalidad de ingreso, tecnologías en salud (medicamentos, procedimientos y otros), antecedentes familiares, alergias, factores de riesgo, incapacidades y condición de egreso, incluyendo indicador de fallecimiento.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve a lo sumo una fila por la combinación (IPCODPACI, NUMINGRES, ID de HCHISPACA) recibida.; Solo considera la dirección principal del paciente (IsMain = 1) más reciente (ORDER BY A.Id DESC TOP 1) para residencia.; Solo incluye alergias activas no anuladas (ALERGICO = 1, TIPOREGISTRO = 1, IDHCMOANULB IS NULL).; Solo incluye antecedentes familiares no anulados (IDHCMOANULB IS NULL).; Para ''Otras tecnologías'' solo considera ítems con MANEJOEXTRA = 1 y atados al folio del ingreso (NUMEFOLIO).; El código de prestador de remisión solo se devuelve cuando el egreso registra estado ''Remitido'' (ESTPACEGR = 4).; Toda la consulta se ejecuta con NOLOCK en las tablas principales (lectura sucia tolerada).; Cualquier excepción es relanzada con RAISERROR severidad 16 sin alterar datos.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'RIPS / RDA (Registro de Atenciones); Consulta externa; Paciente fallecido; Prestador de servicios de salud (IPS); EAPB / Entidad responsable del plan de beneficios; Tipo y número de documento de identificación; Nacionalidad y país de residencia; Sexo biológico e identidad de género; Comunidad étnica; Discapacidad; Ocupación; Modalidad de atención; Vía de ingreso a servicios de salud; Causa de atención; Entorno de atención (hogar, comunitario, escolar, laboral, institucional); Diagnóstico principal CIE-10 y diagnósticos relacionados; Condición y destino al egreso; Prestador de remisión; Alergias; Antecedentes familiares; Factores de riesgo; Tecnologías en salud (medicamentos, procedimientos, dispositivos médicos); Incapacidades médicas y licencias; Profesional de la salud responsable', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTPACEGR = 3 en HCREGEGRE para el ingreso del paciente → Marca RDA_PacienteFallecido = 1 y formatea FECMUEPAC como fecha/hora de fallecimiento else RDA_PacienteFallecido = 0 y RDA_FechaPacienteFallecido = NULL; si p.IPSEXOPAC = 1 / 2 → Mapea sexo biológico a ''Hombre'' (1) o ''Mujer'' (2) else NULL para otros valores; si ing.CareSettingCode in (1..5) → Mapea entorno de atención a etiquetas: 01 Hogar, 02 Comunitario, 03 Escolar, 04 Laboral, 05 Institucional else ''No aplica''; si RE.ESTPACEGR (1..5) en HCREGEGRE → Construye condición/destino al egreso concatenando etiqueta (Mejor/Igual o Peor/Fallecido/Remitido/Hospitalización en Casa) con dbo.DestinoPaciente(his.INDICAPAC); si ESTPACEGR = 4 (paciente remitido) en HCREGEGRE filtrado por NUMEFOLIO → Devuelve AIPSREMIS como código del prestador de remisión (RDA_44) else NULL; si A.RuralArea = ''01'' en Admissions.PatientAddress → ZonaNombre = ''Rural'' else ZonaNombre = ''Urbana''; si m.IdAllergyType = 1 en HCMEDRIES → Toma m.CODPRODUC como código de alergia (o ''NO_ESPECIFICADO'' si vacío) else Código = ''NO_APLICA''; si FMH.Relationship in (1..4) → Mapea parentesco a Padres/Hermanos/Tíos/Abuelos else ''NO APLICA''; si DisabilityClass in (1..4) en HCINCAPAC → Etiqueta tipo de incapacidad (licencia maternidad/paternidad, incapacidad común, licencia cuidado niñez, otras licencias gestacionales); si INP.CODDIAPRI = 1 (diagnóstico principal) vs INPN.CODDIAPRI = 0 (relacionados) → Separa el dx principal (RDA_37) de los dx relacionados (RDA_38) por folio e ingreso', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'rda.sf_rda_OrdenesMedicasMedicamentos; rda.sf_rda_OrdenesMedicasProcedimientos; dbo.DestinoPaciente; dbo.TipoDocumento', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADACTIVID; dbo.ADINGRESO; dbo.ADCENATEN; Admissions.AdmissionModalities; dbo.INPROFSAL; dbo.EntryRoutesHealthServices; dbo.Causesofattention; dbo.INDIAGNOS; dbo.INDIAGNOP; Contract.HealthAdministrator; dbo.ADTIPOIDENTIFICA; Common.Country; Admissions.GenderTypes; dbo.ADGRUETNI; dbo.ADDISCAPACI; Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCMEDRIES; MedicalHistory.FamilyMedicalHistory; dbo.ReportRiskFactors; dbo.RiskFactor; Admissions.RisksType; dbo.HCSOLINSC; dbo.HCSOLINSD (+3 adicionales)', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_ConsultaExterna';
-- GO
