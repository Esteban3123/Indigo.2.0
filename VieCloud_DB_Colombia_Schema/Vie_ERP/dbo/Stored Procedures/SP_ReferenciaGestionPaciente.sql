CREATE PROCEDURE [dbo].[SP_ReferenciaGestionPaciente]
AS
     SELECT R.NUMINGRES AS Ingreso, 
            R.IPCODPACI AS Identificacion, 
            p.IPNOMCOMP AS Paciente, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE
                WHEN RD2.FECSEGUIMIENTO IS NULL
                THEN RD.FECSEGUIMIENTO
                ELSE RD2.FECSEGUIMIENTO
            END AS FechaGestion,
            CASE
                WHEN RD.TIPENTIDAD = 1
                THEN IPS.DSCRIPIPS
                WHEN RD.TIPENTIDAD = 2
                THEN ENT2.NOMENTIDA
                WHEN RD.TIPENTIDAD = 3
                THEN 'CRUE'
                WHEN RD.TIPENTIDAD = 4
                THEN RD.OTRAENTIDAD
            END AS EntidadGestion,
            CASE
                WHEN RD2.NOMCONTACT IS NULL
                THEN RD.NOMCONTACT
                ELSE RD2.NOMCONTACT
            END AS ContactoGestion,
            CASE
                WHEN RD.REGESTADO = '1'
                THEN 'SI acepta'
                WHEN RD.REGESTADO = '2'
                THEN 'No Acepta'
                WHEN RD.REGESTADO = '3'
                THEN 'Pendiente'
                WHEN RD.REGESTADO = '4'
                THEN 'No Aplica'
            END AS EstadoGestion, 
            RD.OBSERVACI AS Observaciones, 
            RD.CODUSUAREG + ' - ' + U.NOMUSUARI AS UsuarioGestion, 
            RD.FECSEGUIMIENTO AS FechaSeguimiento, 
            RD.NOMCONTACT AS ContactoSeguimiento
     FROM.HCREFCONP AS R WITH(NOLOCK)
         INNER JOIN.ADINGRESO AS ING WITH(NOLOCK) ON R.NUMINGRES = ING.NUMINGRES
         INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON R.IPCODPACI = P.IPCODPACI
         INNER JOIN.INUNIFUNC AS UNI WITH(NOLOCK) ON UNI.UFUCODIGO = R.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = R.CODCENATE
         INNER JOIN.INENTIDAD AS ENT ON ING.CODENTIDA = ENT.CODENTIDA
         INNER JOIN.INDIAGNOS AS D WITH(NOLOCK) ON ING.CODDIAING = D.CODDIAGNO
         INNER JOIN.HCREFCONTD AS RD WITH(NOLOCK) ON R.AUTO = RD.HCREFCONPID
         LEFT OUTER JOIN.HCREFCONTD AS RD2 WITH(NOLOCK) ON RD2.ID = RD.HCREFCONTDID
         LEFT OUTER JOIN.INENTIDAD ENT2 WITH(NOLOCK) ON RD.CODENTIDA = ENT2.CODENTIDA
         LEFT JOIN.ADCONTIPS AS IPS WITH(NOLOCK) ON IPS.CODIGOIPS = RD.IPSSELECCION
         LEFT OUTER JOIN.SEGusuaru AS U ON RD.CODUSUAREG = U.CODUSUARI

/*LEFT JOIN
.SEGusuaru AS U2 ON RD2.CODUSUAREG =U2.CODUSUARI*/

     WHERE R.ESTADO = 2 --and R.IPCODPACI in ('26555868')-- AND RD.ID=27062
     ORDER BY CASE
                  WHEN RD2.FECSEGUIMIENTO IS NULL
                  THEN RD.FECSEGUIMIENTO
                  ELSE RD2.FECSEGUIMIENTO
              END, 
              RD.FECHCREREG;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el estado de gestión de referencias y contrarreferencias activas (estado = 2) de pacientes, consolidando información del proceso de remisión entre instituciones. Integra datos del ingreso hospitalario, información demográfica del paciente (nombre, edad, cédula), unidad funcional y centro de atención de origen, diagnóstico de ingreso, entidad aseguradora y la IPS o entidad receptora de la remisión (puede ser una IPS contratada, una EPS/entidad externa o el CRUE). Muestra la fecha de gestión, el contacto realizado, el estado de aceptación de la referencia (acepta, no acepta, pendiente, no aplica), las observaciones y el usuario responsable del seguimiento, considerando tanto el registro de gestión inicial como su novedad o actualización más reciente. Sirve como panel de control o reporte operativo para el equipo de referencia y contrarreferencia, permitiendo hacer seguimiento a los traslados y remisiones pendientes o en curso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ReferenciaGestionPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ReferenciaGestionPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con referencia activa y su última gestión/seguimiento, mostrando entidad gestionada, estado de aceptación y datos del contacto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros de referencia con estado = 2 (activos/gestionables).; Cada referencia tiene su detalle en HCREFCONTD vinculado por HCREFCONPID.; El paciente, ingreso, unidad funcional, centro de atención, entidad y diagnóstico de ingreso deben existir para que la referencia aparezca (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan referencias activas (ESTADO = 2).; El detalle más reciente (RD2 encadenado vía HCREFCONTDID) prevalece sobre el detalle inicial para fecha y contacto de gestión.; Los tipos de entidad gestionada están restringidos a 4 categorías: IPS, EAPB/Entidad, CRUE u Otra.; El estado de gestión se restringe a 4 valores codificados (1-4).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Referencia y contrarreferencia; Gestión de referencia; Seguimiento; IPS; Entidad/EAPB; CRUE (Centro Regulador de Urgencias y Emergencias); Diagnóstico de ingreso; Unidad funcional; Centro de atención; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result-set: Devuelve solo referencias con R.ESTADO = 2, ordenadas por la fecha de seguimiento más reciente (RD2 si existe, si no RD) y luego por fecha de registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RD2.FECSEGUIMIENTO IS NULL → Usa RD.FECSEGUIMIENTO y RD.NOMCONTACT como fecha y contacto de gestión. else Usa RD2.FECSEGUIMIENTO y RD2.NOMCONTACT (seguimiento posterior) como datos de gestión vigente.; si RD.TIPENTIDAD = 1 → Entidad gestionada = nombre de IPS (ADCONTIPS.DSCRIPIPS).; si RD.TIPENTIDAD = 2 → Entidad gestionada = entidad/aseguradora (INENTIDAD.NOMENTIDA).; si RD.TIPENTIDAD = 3 → Entidad gestionada = ''CRUE'' (Centro Regulador de Urgencias).; si RD.TIPENTIDAD = 4 → Entidad gestionada = texto libre en RD.OTRAENTIDAD.; si RD.REGESTADO = ''1''/''2''/''3''/''4'' → Traduce estado de gestión a ''SI acepta'', ''No Acepta'', ''Pendiente'' o ''No Aplica'' respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREFCONTD; dbo.ADCONTIPS; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ReferenciaGestionPaciente';
-- GO
