CREATE PROCEDURE [dbo].[ESE_SP_Admisiones_IngresosAbiertosPorUsuario] @UserName VARCHAR(250)
AS
     SELECT DISTINCT 
            I.CODCENATE AS CodCentro, 
            RTRIM(cen.NOMCENATE) AS CentroAtencion, 
            RTRIM(i.NUMINGRES) AS Ingreso, 
            ga.Code AS CodGrupoAtencion, 
            ga.Name AS GrupoAtención, 
            RTRIM(ea.Name) AS Entidad, 
            RTRIM(i.IPCODPACI) AS Identificacion, 
            SUBSTRING(RTRIM(p.IPNOMCOMP), 1, 40) AS Paciente, 
            i.IFECHAING AS Fecha_Ingreso,
            CASE i.IESTADOIN
                WHEN '  '
                THEN 'Sin Confirmar'
                WHEN 'F'
                THEN 'Confirmado'
                WHEN 'A'
                THEN 'Anulado'
                WHEN 'C'
                THEN 'Cerrado'
                WHEN 'P'
                THEN 'Facturado Parcial'
            END AS Estado, 
            RTRIM(uf.UFUDESCRI) AS UnidadFuncional,
            CASE
                WHEN SEG2.CODUSUARI IS NULL
                THEN uu.CODUSUARI
                ELSE SEG2.CODUSUARI
            END AS CodUsuarioCrea,
            CASE
                WHEN SEG2.NOMUSUARI IS NULL
                THEN uu.NOMUSUARI
                ELSE SEG2.NOMUSUARI
            END AS UsuarioCrea,
            CASE i.TIPOINGRE
                WHEN 1
                THEN 'Ambulatorio'
                WHEN 2
                THEN 'Hospitalario'
            END AS TipoIngreso, 
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
          AND (i.IESTADOIN <> 'C')
          AND ((SEG2.NOMUSUARI IS NULL
                AND uu.CODUSUARI = @UserName)
               OR ((uu.CODUSUARI IS NULL
                    AND SEG2.CODUSUARI = @UserName)));
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta todos los ingresos o admisiones de pacientes que están aún abiertos (no confirmados, en proceso o facturados parcialmente), filtrados por el usuario que los creó o registró. Integra información de admisiones, pacientes, unidades funcionales, centros de atención, grupos de atención del contrato, entidad pagadora (EPS/aseguradora) y datos de triage o urgencias, para presentar un listado operativo de ingresos pendientes de cierre asignados a un usuario específico. Se usa en el módulo de admisiones para que cada usuario gestione y haga seguimiento de sus propios ingresos abiertos, ya sean ambulatorios u hospitalarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los ingresos (admisiones) abiertos —no confirmados, anulados ni cerrados— asociados a un usuario específico, ya sea como creador del ingreso o como usuario de la consulta de triage.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario consultado debe existir como creador del ingreso (CODUSUCRE) o como responsable en la consulta de urgencias/triage (ADCONTURG.CODUSUARI).; Los ingresos deben tener estado distinto de ''F'' (Confirmado), ''A'' (Anulado) y ''C'' (Cerrado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se retornan ingresos con estado Confirmado (''F''), Anulado (''A'') o Cerrado (''C'').; Solo se retornan ingresos cuyo usuario creador o usuario de triage coincide con el parámetro recibido.; El nombre del paciente se trunca a 40 caracteres.; Cada fila representa un ingreso único (DISTINCT) con cantidad fija = 1.; El diagnóstico principal se obtiene tomando el folio máximo donde CODDIAPRI=''True''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Centro de atención; Unidad funcional; Grupo de atención; Administradora de salud (entidad); Paciente; Diagnóstico principal (CIE10); Triage; Historia clínica de urgencias; Egreso; Estado de ingreso (Sin Confirmar, Confirmado, Anulado, Cerrado, Facturado Parcial); Tipo de ingreso (Ambulatorio/Hospitalario); Usuario creador', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve ingresos abiertos con datos de centro, paciente, entidad, unidad funcional, estado, tipo de ingreso y usuario creador, filtrados cuando IESTADOIN no es ''F'',''A'' ni ''C'' y el usuario coincide con el creador o con el usuario de triage.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = ''  '' → Estado se reporta como ''Sin Confirmar''; si IESTADOIN = ''F'' → Estado ''Confirmado'' (excluido por WHERE); si IESTADOIN = ''A'' → Estado ''Anulado'' (excluido por WHERE); si IESTADOIN = ''C'' → Estado ''Cerrado'' (excluido por WHERE); si IESTADOIN = ''P'' → Estado ''Facturado Parcial''; si TIPOINGRE = 1 → Tipo de ingreso ''Ambulatorio'' else Si TIPOINGRE = 2 entonces ''Hospitalario''; si SEG2.CODUSUARI IS NULL (no hay usuario de triage) → Se reporta como usuario creador el de SEGusuaru por CODUSUCRE else Se reporta el usuario asociado a la consulta de triage (ADCONTURG); si SEG2.NOMUSUARI IS NULL AND uu.CODUSUARI = @UserName → Incluye el ingreso por coincidencia con el usuario creador else Si uu.CODUSUARI IS NULL AND SEG2.CODUSUARI = @UserName, incluye por coincidencia con el usuario de triage', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INUNIFUNC; dbo.ADCENATEN; Contract.CareGroup; Security.User; Security.Person; dbo.HCHISPACA; dbo.INPACIENT; Contract.HealthAdministrator; Common.City; dbo.HCURGING1; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCURGEVO1; dbo.HCREGEGRE; dbo.SEGusuaru; dbo.INUBICACI; dbo.INMUNICIP; dbo.ADTRIAGEU; dbo.ADCONTURG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertosPorUsuario';
-- GO
