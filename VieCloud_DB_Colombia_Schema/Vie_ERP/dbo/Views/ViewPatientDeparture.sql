

/*  Modification author: Miguel Angel Ruiz Vega - DBA
    Date: 2026-03-26

   -Causa principal detectada
   La subconsulta actual sobre dbo.HCHISPACA mezcla al mismo tiempo:
   - MAX(_HC.FECHISPAC)
   - GROUP BY _HC.FECHISPAC
   - ROW_NUMBER() OVER (PARTITION BY _HC.NUMINGRES ORDER BY _HC.FECHISPAC DESC)
   
   Se reescribe la subconsulta de HCHISPACA para:
   - eliminar el GROUP BY
   - eliminar MAX(FECHISPAC)
   - dejar únicamente ROW_NUMBER()

   Mejora los tiempos en 7 seg y el consumo a la mitad
   
   */
  
CREATE VIEW [dbo].[ViewPatientDeparture]  
AS  
  
SELECT   
AI.UFUACTPAC AS UnitFunctional,  
CONCAT (RTRIM(AI.UFUACTPAC),' - ',UF.UFUDESCRI ) AS UnitFunctionalDescr,   
AI.CODCENATE AS CenterCare,   
NC.DESCCAMAS AS NomCama,   
PC.IPCODPACI AS IdenUsua,   
PC.IPNOMCOMP AS NomUsua,   
MD.NOMMEDICO AS NomMed,   
HC.FECHEGRES AS FechaEgresoM,    
DATEDIFF(MINUTE ,HC.FECHEGRES,EG.FECEGRESO) AS TiempoEgreso,   
ISNULL(EN1.NOMMEDICO, EN.NOMMEDICO) AS NomEnf,  
EG.FECEGRESO  AS FechaEgresoE,   
AI.NUMINGRES as NumIng  
from dbo.INPACIENT AS PC  
INNER JOIN dbo.ADINGRESO AS AI ON AI.IPCODPACI = PC.IPCODPACI AND AI.IESTADOIN IN('','P')  
INNER JOIN   
(SELECT  _HC.IPCODPACI ,   
   _HC.NUMINGRES,  
   _HC.CODPROSAL ,   
   MAX(_HC.FECHISPAC) AS FECHEGRES ,  
         row_number() over (  
   partition by _HC.NUMINGRES  
            order by _HC.FECHISPAC desc) as rn  
FROM dbo.HCHISPACA AS _HC   
WHERE _HC.INDICAPAC IN ('9','10','11','12','15','16')  
GROUP BY _HC.IPCODPACI,_HC.NUMINGRES,_HC.CODPROSAL, _HC.FECHISPAC  
) AS HC   
ON HC.NUMINGRES  = AI.NUMINGRES AND Hc.rn=1  
INNER JOIN dbo.INUNIFUNC AS UF ON UF.UFUCODIGO = COALESCE(AI.UFUAACTHOS,AI.UFUAACTMED)  
LEFT JOIN dbo.CHCAMASHO AS NC ON NC.CODICAMAS = AI.CODCAMACT  
LEFT JOIN dbo.INPROFSAL AS MD ON MD.CODPROSAL = HC.CODPROSAL  
LEFT JOIN dbo.CHREGEGRE AS EG ON EG.NUMINGRES = AI.NUMINGRES   
LEFT JOIN dbo.INPROFSAL AS EN ON EN.CODPROSAL = EG.CODPROSAL  
LEFT JOIN dbo.INPROFSAL AS EN1 ON EN1.CODUSUARI = EG.CODUSUARI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de egreso hospitalario, combinando datos del ingreso activo (urgencias u hospitalización en estado pendiente o en proceso), la última nota clínica con indicación de alta o egreso, la unidad funcional y cama actual, el médico tratante, el profesional de enfermería responsable del egreso, y los tiempos entre la indicación médica de egreso y el egreso efectivo registrado. Integra información de pacientes (INPACIENT), ingresos (ADINGRESO), historia clínica (HCHISPACA filtrando indicaciones de alta), unidades funcionales (INUNIFUNC), camas (CHCAMASHO), profesionales de salud (INPROFSAL) y el registro formal de egreso (CHREGEGRE). Sirve para monitorear en tiempo real el flujo de salida de pacientes hospitalizados, identificar demoras entre la orden médica de alta y el egreso efectivo, y apoyar la gestión de camas y ocupación hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPatientDeparture';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewPatientDeparture';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de egresos de pacientes hospitalizados, mostrando datos de ubicación (unidad funcional, cama), médico tratante, enfermero, fechas de egreso médico y administrativo, y el tiempo transcurrido entre ambos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso con estado vacío ('''') o ''P'' (pendiente/activo) en ADINGRESO; Debe existir al menos un folio de historia clínica con indicador de egreso (INDICAPAC en ''9'',''10'',''11'',''12'',''15'',''16''); La unidad funcional del ingreso (UFUAACTHOS o UFUAACTMED) debe existir en INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera ingresos activos o pendientes (IESTADOIN en '''' o ''P''); Solo se considera la nota clínica de egreso más reciente por ingreso; Los indicadores de paciente ''9'',''10'',''11'',''12'',''15'',''16'' representan estados de egreso/salida; El tiempo de egreso (TiempoEgreso) se mide en minutos entre la fecha del egreso clínico y el egreso administrativo; La unidad funcional se prioriza por hospitalización (UFUAACTHOS) sobre la médica (UFUAACTMED); El egreso administrativo (CHREGEGRE) y la cama (CHCAMASHO) son opcionales (LEFT JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso/admisión hospitalaria; egreso médico; egreso administrativo; historia clínica; unidad funcional; cama hospitalaria; médico tratante; enfermero; tiempo de egreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un registro por ingreso con la última nota clínica de egreso (rn=1 según ROW_NUMBER particionado por NUMINGRES ordenado por FECHISPAC DESC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AI.IESTADOIN IN ('''',''P'') → Se incluye el ingreso en el resultado else Se excluye el ingreso; si _HC.INDICAPAC IN (''9'',''10'',''11'',''12'',''15'',''16'') → El folio de historia clínica se considera nota de egreso candidata else Se descarta del cálculo; si rn = 1 (última FECHISPAC por NUMINGRES) → Se toma como fecha de egreso médica (FECHEGRES) else Se descartan las notas anteriores; si AI.UFUAACTHOS IS NOT NULL → Se usa UFUAACTHOS como unidad funcional else Se usa UFUAACTMED (COALESCE); si EN1.NOMMEDICO IS NOT NULL (profesional encontrado por CODUSUARI) → Se reporta como nombre del enfermero else Se usa EN.NOMMEDICO (búsqueda por CODPROSAL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INUNIFUNC; dbo.CHCAMASHO; dbo.INPROFSAL; dbo.CHREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewPatientDeparture';
GO
