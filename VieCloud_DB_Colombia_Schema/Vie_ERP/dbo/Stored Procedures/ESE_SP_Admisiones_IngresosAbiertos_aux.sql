CREATE PROCEDURE [dbo].[ESE_SP_Admisiones_IngresosAbiertos_aux]

--@filter varchar(100)

AS
     SELECT DISTINCT 
            IFECHAING AS 'FECHA-ING', 
            I.NUMINGRES AS INGRESO, 
            I.IPCODPACI AS PACIENTE, 
            P.IPNOMCOMP AS NOMBRE, 
            I.IESTADOIN AS ESTADO, 
            CG.Code AS 'CODIGO-GRUPO', 
            CG.Name AS 'NOM-GRUPO', 
            E.NOMENTIDA AS ENTIDAD, 
            AC.CODCENATE AS 'COD-CENTRO', 
            AC.NOMCENATE AS 'CENTRO-ATEN', 
            UNI.UFUDESCRI AS UNIDAD,
            CASE
                WHEN SEG2.CODUSUARI IS NULL
                THEN SEG.CODUSUARI
                ELSE SEG2.CODUSUARI
            END AS 'COD-USUARIO',
            CASE
                WHEN SEG2.NOMUSUARI IS NULL
                THEN SEG.NOMUSUARI
                ELSE SEG2.NOMUSUARI
            END AS 'NOM-USUARIO', 
            ADCO.CODCONCEC AS 'CONTRO-URG'
     FROM.ADINGRESO AS I
         INNER JOIN.INPACIENT AS P ON P.IPCODPACI = I.IPCODPACI
         INNER JOIN CONTRACT.CareGroup AS CG ON CG.Id = I.GENCAREGROUP
         INNER JOIN.INENTIDAD AS E ON E.CODENTIDA = I.CODENTIDA
         INNER JOIN.ADCENATEN AS AC ON AC.CODCENATE = I.CODCENATE
         INNER JOIN.INUNIFUNC AS UNI ON UNI.UFUCODIGO = I.UFUCODIGO
         INNER JOIN.SEGusuaru AS SEG ON SEG.CODUSUARI = I.CODUSUCRE
         LEFT OUTER JOIN.ADTRIAGEU AS AD ON AD.NUMINGRES = I.NUMINGRES
         LEFT OUTER JOIN.ADCONTURG AS ADCO ON AD.CODCONCEC = ADCO.CODCONCEC
         LEFT OUTER JOIN.SEGusuaru AS SEG2 ON SEG2.CODUSUARI = ADCO.CODUSUARI
     WHERE IESTADOIN <> 'F'
           AND IESTADOIN <> 'A'; --and SEG2.NOMUSUARI = @filter
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los ingresos (admisiones) actualmente abiertos o activos en el sistema, es decir, aquellos cuyo estado no es Finalizado ni Anulado. Para cada ingreso retorna: fecha de ingreso, número de ingreso, cédula y nombre completo del paciente, estado del ingreso, grupo de atención y entidad pagadora (EPS/aseguradora), centro de atención y unidad funcional donde se encuentra el paciente, y el usuario responsable (ya sea el que creó el ingreso o, si existe, el profesional que realizó el llamado en urgencias). También incluye el código de control de urgencias cuando el paciente pasó por triage. Se utiliza como consulta auxiliar para monitores de censo, tableros de control de camas y seguimiento operativo de pacientes en curso en todas las modalidades de atención (urgencias, hospitalización, consulta externa, entre otras).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los ingresos hospitalarios actualmente abiertos (no finalizados ni anulados) con sus datos demográficos, administrativos y el usuario responsable, priorizando el del control de urgencias sobre el creador del ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de catálogos relacionados: pacientes, grupos asistenciales (CareGroup), entidades, centros de atención, unidades funcionales y usuarios; Los ingresos deben tener CODUSUCRE válido referenciado en SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan ingresos cuyo estado no es ''F'' (finalizado/facturado) ni ''A'' (anulado), es decir, ingresos abiertos/activos; Cada fila corresponde a un ingreso con paciente, grupo asistencial, entidad, centro de atención y unidad funcional obligatoriamente asociados (INNER JOIN); El usuario reportado prioriza al responsable del control de urgencias sobre el creador del ingreso cuando ambos existen; Los datos de triage/control de urgencias son opcionales (LEFT JOIN); su ausencia no excluye el ingreso del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Paciente; Estado del ingreso (abierto/finalizado/anulado); Grupo asistencial (CareGroup); Entidad responsable de pago; Centro de atención; Unidad funcional; Triage de urgencias; Control de urgencias; Usuario creador / usuario responsable', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando IESTADOIN <> ''F'' y IESTADOIN <> ''A'', devuelve fila con datos del ingreso, paciente, grupo asistencial, entidad, centro de atención, unidad, usuario (de urgencias si existe, si no el creador) y control de urgencias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El ingreso tiene asociado un control de urgencias (ADCONTURG vía ADTRIAGEU) con usuario en SEGusuaru → Reporta el código y nombre de ese usuario de urgencias como ''COD-USUARIO''/''NOM-USUARIO'' else Reporta el usuario creador del ingreso (CODUSUCRE) como ''COD-USUARIO''/''NOM-USUARIO''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; CONTRACT.CareGroup; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.SEGusuaru; dbo.ADTRIAGEU; dbo.ADCONTURG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Admisiones_IngresosAbiertos_aux';
-- GO
