CREATE PROCEDURE [dbo].[ESE_SP_Admisiones_IngresosAbiertos]
AS
     SELECT DISTINCT 
            I.CODCENATE AS CodCentro, 
            RTRIM(cen.NOMCENATE) AS CentroAtencion, 
            RTRIM(i.NUMINGRES) AS Ingreso, 
            ga.Code AS Cód_Grupo_Atención, 
            ga.Name AS Grupo_Atención, 
            RTRIM(ea.Name) AS Entidad, 
            RTRIM(i.IPCODPACI) AS Identificación, 
            CAST(p.GENEXPEDITIONCITY AS VARCHAR(20)) + ' - ' + ISNULL(ci.Name, '') AS Lugar_Expedición, 
            SUBSTRING(RTRIM(p.IPNOMCOMP), 1, 40) AS Paciente, 
            i.IFECHAING AS Fecha_Ingreso,
            CASE i.IESTADOIN
                WHEN '  '
                THEN 'Sin Confirmar Hoja de Trabajo'
                WHEN 'F'
                THEN 'Confirmada Hoja de Trabajo'
                WHEN 'A'
                THEN 'Anulado'
                WHEN 'C'
                THEN 'Cerrado'
                WHEN 'P'
                THEN 'Facturado Parcial'
            END AS Estado, 
            RTRIM(uf.UFUDESCRI) AS Unidad_Funcional, 
            em.FECALTPAC AS Fecha_Alta_Médica, 
            HC.CODDIAGNO AS CIE_10, 
            CIE10.NOMDIAGNO AS Diagnóstico,
            CASE
                WHEN SEG2.CODUSUARI IS NULL
                THEN uu.CODUSUARI
                ELSE SEG2.CODUSUARI
            END AS CodUsuarioCrea,
            CASE
                WHEN SEG2.NOMUSUARI IS NULL
                THEN uu.NOMUSUARI
                ELSE SEG2.NOMUSUARI
            END AS Usuario_Crea, 
            i.FECREGCRE AS Fecha_Creación, 
            uu.NOMUSUARI AS UsuarioModifico, 
            i.FECREGMOD AS Fecha_Modificación, 
            RTRIM(D.UFUDESCRI) AS UnidadActual, 
            RTRIM(i.IOBSERVAC) AS Observaciones,
            CASE i.TIPOINGRE
                WHEN 1
                THEN 'Ambulatorio'
                WHEN 2
                THEN 'Hospitalario'
            END AS TipoIngreso, 
            HCU.ENFACTUAL AS Enfermedad_Actual, 
            RTRIM(UBINOMBRE) AS Ubicación, 
            '1' AS Cantidad
     FROM.ADINGRESO AS i WITH(NOLOCK)
         INNER JOIN.INUNIFUNC AS uf WITH(NOLOCK) ON uf.UFUCODIGO = i.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen ON Cen.CODCENATE = i.CODCENATE
         LEFT OUTER JOIN Contract.CareGroup AS ga WITH(NOLOCK) ON ga.Id = i.GENCAREGROUP
         LEFT OUTER JOIN Security.[User] AS u ON u.UserCode = i.CODUSUCRE
         LEFT OUTER JOIN Security.[User] AS um ON u.UserCode = i.codusumod
         LEFT OUTER JOIN Security.Person AS per ON per.Id = u.IdPerson
         LEFT OUTER JOIN Security.Person AS PERM ON PERM.Id = um.IdPerson
         LEFT OUTER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON HC.NUMINGRES = i.NUMINGRES
                                                         AND HC.IPCODPACI = HC.IPCODPACI
                                                         AND HC.TIPHISPAC = 'i'
         LEFT OUTER JOIN.INPACIENT AS p WITH(NOLOCK) ON p.IPCODPACI = i.IPCODPACI
         LEFT OUTER JOIN Contract.HealthAdministrator AS ea WITH(NOLOCK) ON ea.Id = i.GENCONENTITY
         LEFT OUTER JOIN Common.City AS ci WITH(NOLOCK) ON ci.Id = p.GENEXPEDITIONCITY
         LEFT OUTER JOIN.HCURGING1 AS HCU WITH(NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES
                                                          AND HCU.IPCODPACI = HC.IPCODPACI
                                                          AND HCU.NUMEFOLIO = HC.NUMEFOLIO
         LEFT OUTER JOIN
     (
         SELECT IPCODPACI, 
                NUMINGRES, 
                MAX(NUMEFOLIO) AS Folio
         FROM.INDIAGNOP
         WHERE(CODDIAPRI = 'True')
         GROUP BY NUMINGRES, 
                  IPCODPACI
     ) AS DX ON DX.IPCODPACI = HCU.IPCODPACI
                AND DX.NUMINGRES = HCU.NUMINGRES
                AND DX.Folio = HC.NUMEFOLIO
         LEFT OUTER JOIN.INDIAGNOS AS CIE10 WITH(NOLOCK) ON CIE10.CODDIAGNO = HC.CODDIAGNO
         LEFT OUTER JOIN.HCURGEVO1 AS HCU1 WITH(NOLOCK) ON HCU.NUMINGRES = HC.NUMINGRES
                                                           AND HCU1.IPCODPACI = HC.IPCODPACI
                                                           AND HCU1.NUMEFOLIO = HC.NUMEFOLIO
         LEFT OUTER JOIN.ADINGRESO AS I2 WITH(NOLOCK) ON I2.NUMINGRES = i.NUMINGRES
         LEFT OUTER JOIN.INUNIFUNC AS D WITH(NOLOCK) ON I2.UFUAACTHOS = D.UFUCODIGO
         LEFT OUTER JOIN.HCREGEGRE AS em WITH(NOLOCK) ON em.IPCODPACI = HC.IPCODPACI
                                                         AND em.NUMINGRES = HC.NUMINGRES
         LEFT OUTER JOIN.SEGusuaru AS uu WITH(NOLOCK) ON uu.CODUSUARI = i.CODUSUCRE
         LEFT OUTER JOIN.INUBICACI AS BB ON BB.AUUBICACI = P.AUUBICACI
         LEFT OUTER JOIN.INMUNICIP AS EE ON EE.DEPMUNCOD = BB.DEPMUNCOD
         LEFT OUTER JOIN.ADTRIAGEU AS AD ON AD.NUMINGRES = I.NUMINGRES
         LEFT OUTER JOIN.ADCONTURG AS ADCO ON AD.CODCONCEC = ADCO.CODCONCEC
         LEFT OUTER JOIN.SEGusuaru AS SEG2 ON SEG2.CODUSUARI = ADCO.CODUSUARI
     WHERE(i.IESTADOIN <> 'F')
          AND (i.IESTADOIN <> 'A')
          AND (i.IESTADOIN <> 'C');
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los ingresos o admisiones de pacientes que están actualmente abiertos (no confirmados o facturados parcialmente), excluyendo los cerrados, anulados y con hoja de trabajo confirmada. Consolida información del episodio de atención: datos del paciente (identificación, nombre, lugar de expedición del documento), centro de atención, unidad funcional actual y de ingreso, grupo de atención y entidad pagadora (EPS/aseguradora), fecha de ingreso, estado del ingreso, tipo de ingreso (ambulatorio u hospitalario), diagnóstico principal CIE-10, enfermedad actual registrada en la historia clínica de urgencias, fecha de alta médica, ubicación, observaciones, y los usuarios que crearon y modificaron el registro. Se usa para el seguimiento operativo y administrativo de la lista de espera o censo de pacientes activos en la institución, permitiendo identificar ingresos pendientes de cierre o facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos de pacientes que permanecen abiertos (no facturados totalmente, no anulados ni cerrados) con datos clínicos, administrativos y de ubicación asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingresos en ADINGRESO con estado distinto de ''F'', ''A'' y ''C''; Catálogos de unidades funcionales, centros de atención, ciudades, diagnósticos CIE-10 y usuarios disponibles para los joins', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan ingresos cuyo estado no sea Confirmada Hoja de Trabajo, Anulado ni Cerrado; Solo se utiliza historia clínica con TIPHISPAC=''i''; El diagnóstico considerado es el principal (CODDIAPRI=''True'') con el mayor NUMEFOLIO; El campo Cantidad siempre es ''1''; El nombre del paciente se trunca a 40 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso de paciente; Centro de atención; Grupo de atención; Entidad administradora de salud; Paciente; Lugar de expedición; Unidad funcional; Hoja de trabajo; Facturación parcial; Alta médica; Diagnóstico CIE-10; Triage de urgencias; Historia clínica de ingreso; Tipo de ingreso ambulatorio/hospitalario; Enfermedad actual; Ubicación del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ADINGRESO: Cuando IESTADOIN no es ''F'',''A'' ni ''C'' se retorna el ingreso como abierto (incluye estados ''  '' Sin Confirmar Hoja de Trabajo y ''P'' Facturado Parcial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = ''  '' → Estado = ''Sin Confirmar Hoja de Trabajo''; si IESTADOIN = ''F'' → Estado = ''Confirmada Hoja de Trabajo'' (excluido por WHERE); si IESTADOIN = ''A'' → Estado = ''Anulado'' (excluido por WHERE); si IESTADOIN = ''C'' → Estado = ''Cerrado'' (excluido por WHERE); si IESTADOIN = ''P'' → Estado = ''Facturado Parcial''; si TIPOINGRE = 1 → TipoIngreso = ''Ambulatorio'' else Si TIPOINGRE = 2 entonces ''Hospitalario''; si Existe usuario en ADCONTURG (SEG2.CODUSUARI no nulo) → Usuario_Crea se toma del control de urgencias (SEG2) else Se toma el usuario creador del ingreso (uu); si Diagnóstico principal: CODDIAPRI = ''True'' en INDIAGNOP → Se selecciona el folio máximo (MAX(NUMEFOLIO)) por paciente/ingreso como diagnóstico principal; si TIPHISPAC = ''i'' en HCHISPACA → Solo se considera la historia clínica de tipo ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; Contract.CareGroup; Security.User; Security.Person; dbo.HCHISPACA; dbo.INPACIENT; Contract.HealthAdministrator; Common.City; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE; dbo.SEGusuaru; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADTRIAGEU; dbo.ADCONTURG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos';
-- GO
