  CREATE PROCEDURE [rda].[sp_rda_Paciente] (
    @Identificacion varchar(25),
    @Ingreso varchar(10),
	@NUMEFOLIO varchar(10),
    @IdHispaca INT
  ) AS BEGIN
SET
  NOCOUNT ON;

  DECLARE @MedEgreso       NVARCHAR(MAX)
  SELECT @MedEgreso      = rda.sf_rda_OrdenesMedicasMedicamentos(@Ingreso, @Identificacion, @NUMEFOLIO, NULL, 0)

BEGIN TRY
SELECT
  /* ========================================================== 
   NIT Empresa / Estado paciente / Grupo Snguineo / Identidad 
   ========================================================== */
  (
    SELECT
      INDNITEMP
    FROM
      INEMPRESU
  ) AS RDA_NIT_Empresa,
  (
    SELECT
      IIF(
        ESTPACEGR = 3,
        CAST(1 AS BIT),
        CAST(0 AS BIT)
      )
    FROM
      HCREGEGRE EG
    WHERE
      IPCODPACI = his.IPCODPACI
      AND EG.NUMINGRES = his.NUMINGRES
  ) AS RDA_PacienteFallecido,
  (
    SELECT
      IIF(
        ESTPACEGR = 3,
        FORMAT(
          EG.FECMUEPAC,
          'dd/MM/yyyy HH:mm'
        ),
        NULL
      )
    FROM
      HCREGEGRE EG
    WHERE
      IPCODPACI = his.IPCODPACI
      AND EG.NUMINGRES = his.NUMINGRES
  ) AS RDA_FechaPacienteFallecido,
  (
    SELECT
      rda.clinical_statu(ESTPACEGR)
    FROM
      HCREGEGRE EG
    WHERE
      IPCODPACI = his.IPCODPACI
      AND EG.NUMINGRES = his.NUMINGRES
  ) AS RDA_EstadoEgreso,
  CONCAT(IPGRUPSAN, IPRHSANGR) AS RDA_GrupoSanguineo,
  p.IPESTADOC AS RDA_EstadoCivilPacienteCodigo,
  CASE
    p.IPESTADOC
    WHEN 1 THEN 'Soltero'
    WHEN 2 THEN 'Casado'
    WHEN 3 THEN 'Viudo'
    WHEN 4 THEN 'Union libre'
    WHEN 5 THEN 'Separado/Divorciado'
    ELSE 'No especificado'
  END AS RDA_EstadoCivilPacienteNombre,
  GTY.Code AS RDA_IdentidadGeneroCodigo,
  GTY.Name AS RDA_IdentidadGeneroNombre,
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
  CASE
    p.IPSEXOPAC
    WHEN 1 THEN 'Hombre'
    WHEN 2 THEN 'Mujer'
    ELSE NULL
  END AS RDA_5_SexoBiologicoNombre,
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
  (
  SELECT
      TOP 1 FECHISPAC
    FROM
      HCHISPACA HC
    WHERE
      IPCODPACI = @Identificacion
      AND HC.NUMINGRES = @Ingreso 
	  order by id asc
  ) AS RDA_17_FechaHoraInicioAtencion,
  (
    SELECT
      TOP 1 FECALTPAC
    FROM
      HCREGEGRE EG
    WHERE
      IPCODPACI = his.IPCODPACI
      AND EG.NUMINGRES = his.NUMINGRES
  ) AS RDA_43_FechaHoraFinAtencion,
  am.Code as RDA_18_1_ModalidadAtencionCodigo,
  am.Name AS RDA_18_1_ModalidadAtencionNombre,
  CASE
    WHEN Inu.UFUTIPUNI = 1 THEN '05'
    WHEN Inu.UFUTIPUNI IN (15, 24, 31) THEN '01'
    WHEN Inu.UFUTIPUNI IN (2, 5, 6, 7, 8, 9, 10, 11, 16, 17, 23) THEN '03'
    WHEN Inu.UFUTIPUNI IN (19, 35) THEN '04'
    WHEN Inu.UFUTIPUNI IN (
      3,
      4,
      12,
      13,
      14,
      18,
      20,
      21,
      22,
      30,
      32,
      33,
      34,
      36,
      37
    ) THEN '02'
    ELSE NULL
  END AS RDA_18_2_GrupoServiciosCodigo,
  rda.Service_Group(Inu.UFUTIPUNI) AS RDA_18_2_GrupoServiciosNombre,
  /* ========================================================= 
   Antecedentes / Medicamentos / Diagnósticos / Profesional 
   ========================================================= */
  al.AlergiasJson AS RDA_47_1_2_TieneAlergia,
  fm.FamiliaresJson AS RDA_47_3_4_AntecedentesFamiliaresJson,
  mh.MedicamentosJson AS RDA_26_MedicamentosAntecedentesJson,

  /* =========================================================
     Tecnologias de salud en egreso(Medicamentos)
     ========================================================= */
	@MedEgreso AS RDA_24_1_2_3_26_27_28_1_2_29_31_1_2_32_1_2_Medicamentos_Tecnologias_Salud_Egreso,

  IND.CODDIAGNO AS RDA_37_1_DxPrincipalCIE10Codigo,
  IND.NOMDIAGNO AS RDA_37_2_DxPrincipalCIE10Nombre,
  INP.TIPDIAGNO AS RDA_37_3_TipoDxPrincipalCodigo,
  CASE
    INP.TIPDIAGNO
    WHEN 'I' THEN 'Impresion Diagnostica'
    WHEN 'C' THEN 'Confirmado Nuevo'
    WHEN 'R' THEN 'Confirmado Repetido'
  END AS RDA_37_3_TipoDxPrincipalNombre,
  CAST(prof.IDADTIPOIDENTIFICA AS varchar(20)) AS RDA_49_1_ProfTipoDocumentoCodigo,
  CAST(tdProf.NOMBRE AS varchar(100)) AS RDA_49_1_ProfTipoDocumentoNombre,
CAST(COALESCE(profEst.CODIGONIT,  prof.CODIGONIT)  AS varchar(25))  AS RDA_49_2_ProfNumeroDocumento,
CAST(COALESCE(profEst.MEDPRINOM,  prof.MEDPRINOM)  AS varchar(100)) AS RDA_49_3_ProfPrimerNombre,
CAST(COALESCE(profEst.MEDSEGNOM,  prof.MEDSEGNOM)  AS varchar(100)) AS RDA_49_4_ProfSegundoNombre,
CAST(COALESCE(profEst.MEDPRIAPEL, prof.MEDPRIAPEL) AS varchar(100)) AS RDA_49_5_ProfPrimerApellido,
CAST(COALESCE(profEst.MEDSEGAPEL, prof.MEDSEGAPEL) AS varchar(100)) AS RDA_49_6_ProfSegundoApellido
FROM
  HCHISPACA his
  INNER JOIN dbo.INPACIENT p ON p.IPCODPACI = his.IPCODPACI
  INNER JOIN dbo.ADINGRESO ing ON ing.NUMINGRES = his.NUMINGRES
  INNER JOIN dbo.ADCENATEN cen ON his.CODCENATE = cen.CODCENATE
  INNER JOIN dbo.INUNIFUNC Inu ON his.UFUCODIGO = inu.UFUCODIGO
  INNER JOIN Admissions.AdmissionModalities am ON ing.IdAdmissionModalities = am.Code
  INNER JOIN dbo.INPROFSAL prof ON his.CODPROSAL = prof.CODPROSAL
  INNER JOIN dbo.INDIAGNOS IND ON IND.CODDIAGNO = HIS.CODDIAGNO
  LEFT JOIN dbo.INDIAGNOH INP ON IND.CODDIAGNO = INP.CODDIAGNO  AND INP.NUMINGRES = ING.NUMINGRES AND INP.NUMEFOLIO = his.NUMEFOLIO
  AND INP.NUMINGRES = ING.NUMINGRES
  AND INP.CODDIAPRI = 1
  LEFT JOIN Contract.HealthAdministrator hea ON hea.Id = ing.GENCONENTITY
  LEFT JOIN dbo.ADTIPOIDENTIFICA td ON td.CODIGO = p.IPTIPODOC
  LEFT JOIN dbo.ADTIPOIDENTIFICA tdProf ON tdProf.ID = prof.IDADTIPOIDENTIFICA
  LEFT JOIN Common.Country cNat ON cNat.Id = p.IDPAIS
  LEFT JOIN Admissions.GenderTypes gt ON gt.Id = p.IdGenderIdentity
  LEFT JOIN dbo.ADGRUETNI ge ON ge.CODGRUPOE = p.CODGRUPOE
  LEFT JOIN dbo.ADDISCAPACI dc ON dc.DISCCODIGO = p.DISCCODIGO
  LEFT JOIN Admissions.GenderTypes GTY ON P.IdGenderIdentity = GTY.Id
  OUTER APPLY (
    SELECT
      TOP (1) E.Code AS PaisCodigo,
      E.Name AS PaisNombre,
      C.DEPMUNCOD AS MunicipioCodigo,
      C.MUNNOMBRE AS MunicipioNombre,
      A.RuralArea AS ZonaCodigo,
      CASE
        WHEN A.RuralArea = '01' THEN 'Rural'
        ELSE 'Urbana'
      END AS ZonaNombre
    FROM
      Admissions.PatientAddress A
      INNER JOIN INUBICACI B ON A.IdUbication = B.ID
      INNER JOIN INMUNICIP C ON B.DEPMUNCOD = C.DEPMUNCOD
      INNER JOIN INDEPARTA D ON C.DEPCODIGO = D.DEPCODIGO
      INNER JOIN Common.Country E ON D.IDPAIS = E.ID
    WHERE
      A.IPCODPACI = p.IPCODPACI
      AND A.IsMain = 1
    ORDER BY
      A.Id DESC
  ) adr
  OUTER APPLY (
    SELECT
      (
        SELECT
          CASE
            WHEN m.IdAllergyType = 1 THEN COALESCE(
              NULLIF(
                LTRIM(
                  RTRIM(m.CODPRODUC)
                ),
                ''
              ),
              'NO_ESPECIFICADO'
            )
            ELSE 'NO_APLICA'
          END AS Codigo,
          COALESCE(
            NULLIF(
              LTRIM(
                RTRIM(m.Allergen)
              ),
              ''
            ),
            'NO_ESPECIFICADO'
          ) AS Descripcion,
          at.Code AS AllergyTypeCode,
          at.Name AS AllergyTypeName
        FROM
          dbo.HCMEDRIES m 
          LEFT JOIN Admissions.AllergyType at ON at.Id = m.IdAllergyType
        WHERE
          m.IPCODPACI = p.IPCODPACI
          AND m.NUMINGRES = his.NUMINGRES
          AND m.ALERGICO = 1
          AND m.TIPOREGISTRO = 1
          AND m.IDHCMOANULB IS NULL
        ORDER BY
          m.FECREGIST DESC FOR JSON PATH
      ) AS AlergiasJson
  ) al
  OUTER APPLY (
    SELECT
      (
        SELECT
          mh.DiagnosticCode AS DiagnosticoCodigo,
          diag.NOMDIAGNO AS DiagnosticoNombre,
          CAST(
            mh.Relationship AS varchar(10)
          ) AS ParentescoCodigo,
          CASE
            mh.Relationship
            WHEN 1 THEN 'Padres'
            WHEN 2 THEN 'Hermanos'
            WHEN 3 THEN 'Tíos'
            WHEN 4 THEN 'Abuelos'
            ELSE 'No especificado'
          END AS ParentescoNombre
        FROM
          MedicalHistory.FamilyMedicalHistory mh
          LEFT JOIN dbo.INDIAGNOS diag ON diag.CODDIAGNO = mh.DiagnosticCode
        WHERE
          mh.PatientCode = p.IPCODPACI
          AND mh.AdmissionNumber = his.NUMINGRES
          AND mh.TypeHistory = 2
          AND mh.Status = 1
          AND mh.IDHCMOANULB IS NULL
        ORDER BY
          mh.DateRegistration DESC FOR JSON PATH
      ) AS FamiliaresJson
  ) fm
  OUTER APPLY (
    SELECT
      (
        SELECT
          COALESCE(
            NULLIF(LTRIM(RTRIM(d.Code)), ''),
            'NO_ESPECIFICADO'
          ) AS Codigo,
          COALESCE(
            NULLIF(LTRIM(RTRIM(m.CODPRODUC)), ''),
            'NO_ESPECIFICADO'
          ) AS Descripcion,
          NULLIF(
            LTRIM(RTRIM(m.MOTSUSMED)),
            ''
          ) AS Observaciones,
          m.FECREGIST AS FechaRegistro
        FROM
          dbo.HCMEDRIES m
          LEFT JOIN Inventory.ATC atc ON atc.Code = m.CODPRODUC
          LEFT JOIN Inventory.DCI d ON d.Id = atc.DCIId
        WHERE
          m.IPCODPACI = p.IPCODPACI
          AND m.NUMINGRES = his.NUMINGRES
          AND m.TIPOREGISTRO = 2
          AND m.ALERGICO = 1
          AND m.IDHCMOANULB IS NULL
        ORDER BY
          m.FECREGIST DESC FOR JSON PATH
      ) AS MedicamentosJson
  ) mh
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
  his.IPCODPACI = @Identificacion
  and his.NUMINGRES = @Ingreso
  and his.ID = @IdHispaca
END TRY BEGIN CATCH DECLARE @ErrorMessage nvarchar(4000) = ERROR_MESSAGE();

RAISERROR(
  'sp_rda_Paciente falló. Detalle: %s',
  16,
  1,
  @ErrorMessage
);

END CATCH
END;
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que consolida la ficha completa de un paciente para el reporte RDA (Registro de Atención), dado un número de identificación, ingreso e ID de historia clínica. Retorna en una sola fila datos demográficos (documento, nombre, sexo, fecha nacimiento, nacionalidad, etnia, discapacidad, residencia), estado clínico del egreso (incluido fallecimiento), EAPB, prestador, modalidad y grupo de servicios, diagnóstico principal CIE-10, alergias, antecedentes familiares y medicamentos previos en formato JSON, más el profesional tratante.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el conjunto de datos RDA de un paciente para un ingreso específico, consolidando identificación, residencia, antecedentes, alergias, medicamentos, diagnóstico principal, modalidad y grupo de servicios para reporte regulatorio.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCHISPACA cuyo IPCODPACI, NUMINGRES e ID coincidan con los parámetros recibidos; El paciente debe existir en INPACIENT y el ingreso en ADINGRESO (INNER JOIN obligatorio); El centro de atención (ADCENATEN), unidad funcional (INUNIFUNC), modalidad de admisión (AdmissionModalities), profesional (INPROFSAL) y diagnóstico (INDIAGNOS) referenciados deben existir, ya que los JOIN son INNER; Debe existir al menos un registro en INEMPRESU para obtener el NIT de la empresa', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Egreso clínico; Estado del paciente (fallecido); Grupo sanguíneo; Estado civil; Identidad de género; Sexo biológico; Prestador de servicios de salud (IPS); EAPB (Entidad responsable del plan de beneficios); Tipo y número de documento; Nacionalidad/País; Comunidad étnica; Discapacidad; Residencia (país, municipio, zona rural/urbana); Modalidad de atención; Grupo de servicios (RIPS); Alergias; Antecedentes familiares; Medicamentos antecedentes (DCI/ATC); Diagnóstico principal CIE-10; Tipo de diagnóstico (Impresión/Confirmado nuevo/repetido); Profesional de la salud; Reporte RDA', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve una única fila con datos demográficos, clínicos y de atención del paciente para el ingreso identificado por @Identificacion, @Ingreso e @IdHispaca.; [RAISERROR] ERROR: Si ocurre cualquier excepción, lanza error con severidad 16 con el mensaje ''sp_rda_Paciente falló. Detalle: %s'' incluyendo el ERROR_MESSAGE() original.', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCREGEGRE.ESTPACEGR = 3 para el paciente e ingreso → RDA_PacienteFallecido = 1 y RDA_FechaPacienteFallecido = FECMUEPAC formateada ''dd/MM/yyyy HH:mm'' else RDA_PacienteFallecido = 0 y RDA_FechaPacienteFallecido = NULL; si INPACIENT.IPESTADOC en {1..5} → Mapea a ''Soltero'', ''Casado'', ''Viudo'', ''Union libre'' o ''Separado/Divorciado'' respectivamente else ''No especificado''; si INPACIENT.IPSEXOPAC = 1 o 2 → Sexo biológico = ''Hombre'' o ''Mujer'' else NULL; si INUNIFUNC.UFUTIPUNI según rangos definidos → Asigna RDA_18_2_GrupoServiciosCodigo: ''05'' si UFUTIPUNI=1; ''01'' si UFUTIPUNI∈(15,24,31); ''03'' si ∈(2,5,6,7,8,9,10,11,16,17,23); ''04'' si ∈(19,35); ''02'' si ∈(3,4,12,13,14,18,20,21,22,30,32,33,34,36,37) else NULL para tipos de unidad no contemplados; si INDIAGNOP.TIPDIAGNO = ''I'' / ''C'' / ''R'' → Mapea a ''Impresion Diagnostica'' / ''Confirmado Nuevo'' / ''Confirmado Repetido''; si Admissions.PatientAddress.RuralArea = ''01'' → RDA_14_ZonaResidenciaNombre = ''Rural'' else ''Urbana''; si HCMEDRIES.IdAllergyType = 1 (alergia a medicamento) → Codigo de alergia = CODPRODUC (o ''NO_ESPECIFICADO'' si vacío) else Codigo = ''NO_APLICA''; si MedicalHistory.FamilyMedicalHistory.Relationship en {1..4} → ParentescoNombre = ''Padres''/''Hermanos''/''Tíos''/''Abuelos'' else ''No especificado''', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'rda.clinical_statu; rda.Service_Group', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INEMPRESU; dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INPACIENT; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; Admissions.AdmissionModalities; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INDIAGNOP; Contract.HealthAdministrator; dbo.ADTIPOIDENTIFICA; Common.Country; Admissions.GenderTypes; dbo.ADGRUETNI; dbo.ADDISCAPACI; Admissions.PatientAddress; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.HCMEDRIES; Admissions.AllergyType; MedicalHistory.FamilyMedicalHistory; Inventory.InventoryProduct; Inventory.ATC; Inventory.DCI', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'rda', @level1type=N'PROCEDURE', @level1name=N'sp_rda_Paciente';
-- GO
