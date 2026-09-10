-- =============================================
-- Author:		HECTOR RODRIGUEZ RUBIANO
-- Create date: 17-04-2020
-- Description:	Lista medicamentos de tipo antibiotico o alto costo
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAtencionFarmaceuticaTipoMed]
(
@CentroAtencion Char(10),
@UnidadFuncional Varchar(250),
@TipoMed tinyint
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UnidadFuncionalAux as varchar(250) = RTRIM(@UnidadFuncional)

	if @UnidadFuncionalAux IS NOT NULL AND LEN(@UnidadFuncionalAux) = 0 SET @UnidadFuncionalAux = NULL

	
SELECT  
  TIPOPOBLACION = G.CODTIPPAC, 
  UNIDADFUNCIONAL = RTRIM(C.UFUDESCRI), 
  C.UFUCODIGO, 
  A.CODICAMAS, 
  CAMA = RTRIM(A.DESCCAMAS) 
  , 
  IDENTIFICACION = E.IPCODPACI, 
  PACIENTE = RTRIM(E.IPNOMCOMP), 
  E.IPFECNACI, 
  EDAD = CAST(
    '' AS CHAR(50)
  ), 
  DIAGNOSTICO = RTRIM(K.CODDIAGNO) + '-' + RTRIM(K.NOMDIAGNO), 
  FECHAORDEN = CONVERT(VARCHAR(20), N.FECHAORDE, 120), 
  FECHADMIN = CONVERT(VARCHAR(20), Q.FECAPLMED, 120), 
  ESTADOMEDICAMENTO = CASE H.PREESTADO WHEN '1' THEN 'Iniciado ' WHEN '2' THEN 'Completo ' WHEN '3' THEN 'Descontinuado ' WHEN '4' THEN 'Suspendido ' WHEN '5' THEN 'Manejo Externo ' WHEN '6' THEN 'Solicitados sin Existencia Actual ' WHEN '7' THEN 'Terminado ' END, 
  Color = Z.Color, 
  MEDICAMENTO = RTRIM(J.DESPRODUC) + ' - ' + RTRIM(J.CODPRODUC), 
  ADMINISTRACION = RTRIM(
    CASE WHEN H.FORMAPRESCRIBE IS NOT NULL THEN H.DESADMINI ELSE CASE WHEN H.DOSISPRFN IS NULL THEN H.DESADMINI WHEN H.DURACIDOS = 'Dosis Unica' THEN RTRIM(
      CAST(H.DOSISPRFN AS CHAR)
    ) + ' ' + RTRIM(
      CAST(O.ABRUNIMED AS CHAR)
    ) + ' Dosis Unica ' + RTRIM(L.DESVIAADM) ELSE RTRIM(
      CAST(H.DOSISPRFN AS CHAR)
    ) + ' ' + RTRIM(
      CAST(O.ABRUNIMED AS CHAR)
    ) + ' Cada ' + RTRIM(
      CAST(H.FRECUENCI AS CHAR)
    ) + CASE H.UNIFRECUE WHEN '1' THEN 'M ' WHEN '2' THEN 'H ' WHEN '3' THEN 'D ' END + RTRIM(L.DESVIAADM) END END
  ), 
  DURACIONPRESCRITA = CASE WHEN H.DURACIDOS = 'Fija' THEN CAST(
    H.VALDURFIJ AS VARCHAR(10)
  ) + ' ' + CASE H.UNIDURFIJ WHEN 1 THEN 'Minuto(s)' WHEN 2 THEN 'Hora(s)' WHEN 3 THEN 'Dia(s)' WHEN 4 THEN 'Semana(s)' WHEN 5 THEN 'Mes(es)' WHEN 6 THEN 'Año(s)' ELSE '' END ELSE H.DURACIDOS END, 
  DIASTRATAMIENTO = CASE WHEN H.PREESTADO IN ('1', '6') THEN DATEDIFF(
    DAY, 
    H.FECINIDOS, 
    [Common].[GETDATE]()
  ) WHEN H.PREESTADO IN (2, 3, 4, 7) 
  AND H.FECFINDOS IS NULL THEN DATEDIFF(
    DAY, 
    H.FECINIDOS, 
    [Common].[GETDATE]()
  ) WHEN H.PREESTADO IN (2, 3, 4, 7) THEN DATEDIFF(DAY, H.FECINIDOS, H.FECFINDOS) ELSE DATEDIFF(
    DAY, 
    H.FECINIDOS, 
    [Common].[GETDATE]()
  ) END, 
  DIASADMINISTRA = CASE WHEN Q.FECAPLMED IS NULL THEN 0 ELSE CASE WHEN H.FECFINDOS IS NULL THEN DATEDIFF(
    DAY, 
    Q.FECAPLMED, 
    [Common].[GETDATE]()
  ) ELSE DATEDIFF(DAY, Q.FECAPLMED, H.FECFINDOS) END END, 
  AISLAMIENTO = ISNULL(
    dbo.TipoAislamiento(A.CODAISLAM), 
    ''
  ), 
  G.NUMINGRES, 
  PROFESIONAL = RTRIM(I.NOMMEDICO), 
  ESPECIALIDAD = RTRIM(P.DESESPECI) 
FROM 
  dbo.CHREGESTA D WITH (NOLOCK)  
  INNER JOIN dbo.CHCAMASHO A WITH (NOLOCK) ON A.CODICAMAS = D.CODICAMAS AND D.REGESTADO = 1 AND A.ESTADCAMA = 2
  INNER JOIN dbo.INUNIFUNC C WITH (NOLOCK) ON A.UFUCODIGO = C.UFUCODIGO 
  INNER JOIN dbo.INPacient E WITH (NOLOCK) ON D.IPCODPACI = E.IPCODPACI 
  INNER JOIN dbo.ADINGRESO G WITH (NOLOCK) ON D.NUMINGRES = G.NUMINGRES 
  INNER JOIN dbo.HCPRESCRA H WITH (NOLOCK) ON E.IPCODPACI = H.IPCODPACI AND G.NUMINGRES = H.NUMINGRES 
  INNER JOIN dbo.HCPRESCRC N WITH (NOLOCK) ON H.CODCONCEC = N.CODCONCEC
  INNER JOIN dbo.INDIAGNOS AS K WITH (NOLOCK) ON H.CODDIAGNO = K.CODDIAGNO 
  INNER JOIN dbo.HCVIAADMI AS L WITH (NOLOCK) ON H.CODVIAADM = L.CODVIAADM 
  INNER JOIN dbo.INPROFSAL I WITH (NOLOCK) ON H.CODPROSAL = I.CODPROSAL 
  INNER JOIN dbo.IHLISTPRO J WITH (NOLOCK) ON H.CODPRODUC = J.CODPRODUC 
  INNER JOIN Inventory.ATC ATC WITH (NOLOCK) ON ATC.Code = J.CODPRODUC AND ((@TipoMed = 1 AND ATC.Antibiotic = 1) OR (@TipoMed = 2 AND ATC.HighCost = 1)) 
  LEFT OUTER JOIN CHTIPOSAISLAMIENTOS Z WITH (NOLOCK) ON A.CODAISLAM = Z.Id 
  LEFT OUTER JOIN dbo.INUNIMEDI AS O WITH (NOLOCK) ON H.CODUNIMFN = O.CODUNIMED 
  LEFT OUTER JOIN dbo.INESPECIA P WITH (NOLOCK) ON d.CODESPECI  = P.CODESPECI 
   OUTER APPLY
  (
    SELECT top 1 HM.IPCODPACI, HM.NUMINGRES, HM.CODPRODUC, HM.FECAPLMED 
    FROM dbo.HCHOJAMED HM WITH (NOLOCK) 
    WHERE HM.MEDESTADO = 2 AND HM.FECAPLMED IS NOT NULL AND HM.NUMINGRES = D.NUMINGRES AND HM.IPCODPACI = D.IPCODPACI AND HM.CODPRODUC = H.CODPRODUC 
	ORDER BY HM.FECAPLMED DESC
  ) as Q
WHERE 
  A.CODCENATE = @CentroAtencion 
  AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(A.UFUCODIGO) in (select * from  [dbo].[SplitString](@UnidadFuncionalAux)))) 
  AND NOT EXISTS(select 1 from dbo.HCREGEGRE R WITH (NOLOCK) where R.NUMINGRES = D.NUMINGRES )

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes actualmente hospitalizados (con cama ocupada y sin egreso registrado) que tienen prescripciones activas de medicamentos de un tipo específico: antibióticos o de alto costo, según el parámetro recibido. Filtra por centro de atención y opcionalmente por una o varias unidades funcionales. Para cada paciente devuelve información de la cama y unidad funcional (CHCAMASHO, INUNIFUNC), datos demográficos del paciente (INPACIENT), número de ingreso (ADINGRESO, CHREGESTA), la prescripción médica con su diagnóstico CIE-10 (HCPRESCRA, HCPRESCRC, INDIAGNOS), vía de administración (HCVIAADMI), nombre del medicamento clasificado como antibiótico o alto costo (IHLISTPRO, tabla ATC de inventario), profesional prescriptor, días de tratamiento transcurridos y la última fecha de administración registrada en hoja de medicamentos (HCHOJAMED). Se usa en la atención farmacéutica y seguimiento clínico para controlar el uso de antibióticos y medicamentos de alto costo en pacientes hospitalizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes hospitalizados activos junto con sus prescripciones de medicamentos clasificados como antibióticos o de alto costo, para seguimiento de atención farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en CHCAMASHO.CODCENATE.; La unidad funcional puede venir vacía o con múltiples valores separados (procesados por dbo.SplitString).; El medicamento debe estar clasificado en Inventory.ATC como Antibiotic=1 (TipoMed=1) o HighCost=1 (TipoMed=2).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo incluye registros de estancia activos (CHREGESTA.REGESTADO = 1) y camas en estado 2 (ocupada).; Excluye pacientes que ya tengan registro de egreso en HCREGEGRE para el ingreso.; Solo considera la última aplicación de medicamento (HCHOJAMED) con MEDESTADO=2 y FECAPLMED no nulo (TOP 1 ORDER BY FECAPLMED DESC).; El medicamento siempre debe estar catalogado en Inventory.ATC con la clasificación correspondiente al tipo solicitado.; Estado del medicamento se traduce a etiqueta legible (Iniciado, Completo, Descontinuado, Suspendido, Manejo Externo, Solicitados sin Existencia Actual, Terminado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención farmacéutica; Antibióticos; Medicamentos de alto costo; Prescripción médica; Hospitalización; Cama hospitalaria; Unidad funcional; Vía de administración; Aislamiento; Diagnóstico (CIE); Egreso hospitalario; Hoja de medicación; Especialidad médica; Tipo de población', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con datos del paciente, cama, prescripción y medicamento filtrado por tipo (antibiótico o alto costo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoMed = 1 → Filtra medicamentos donde ATC.Antibiotic = 1 (antibióticos) else Si @TipoMed = 2 filtra ATC.HighCost = 1 (alto costo); si @UnidadFuncionalAux IS NULL o vacío tras RTRIM → No aplica filtro por unidad funcional else Filtra A.UFUCODIGO contra los valores devueltos por dbo.SplitString(@UnidadFuncionalAux); si H.PREESTADO IN (''1'',''6'') → DIASTRATAMIENTO = días desde FECINIDOS hasta hoy else Si PREESTADO IN (2,3,4,7) y FECFINDOS NULL usa hoy; si FECFINDOS no nulo usa FECFINDOS; si Q.FECAPLMED IS NULL → DIASADMINISTRA = 0 else Calcula días entre FECAPLMED y FECFINDOS o fecha actual; si H.DURACIDOS = ''Dosis Unica'' → Construye texto de administración como ''dosis unidad Dosis Unica víaadmin'' else Construye con frecuencia y unidad de frecuencia (M/H/D); si H.FORMAPRESCRIBE IS NOT NULL → Usa H.DESADMINI como administración else Construye administración a partir de dosis, unidad, frecuencia y vía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.SplitString; dbo.TipoAislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.INPacient; dbo.ADINGRESO; dbo.HCPRESCRA; dbo.HCPRESCRC; dbo.INDIAGNOS; dbo.HCVIAADMI; dbo.INPROFSAL; dbo.IHLISTPRO; Inventory.ATC; dbo.CHTIPOSAISLAMIENTOS; dbo.INUNIMEDI; dbo.INESPECIA; dbo.HCHOJAMED; dbo.HCREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceuticaTipoMed';
-- GO
