CREATE PROCEDURE [dbo].[ESE_SP_ReferenciaGestionPaciente]
AS
     SELECT RTRIM(R.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS Centro, 
            RTRIM(R.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadRemision, 
            RTRIM(ING.CODENTIDA) + ' - ' + RTRIM(ENT.NOMENTIDA) AS Entidad, 
            R.NUMINGRES AS Ingreso, 
            R.IPCODPACI AS Identificacion, 
            p.IPNOMCOMP AS Paciente, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad, 
            ING.CODDIAING + ' - ' + RTRIM(D.NOMDIAGNO) AS Diagnostico,
            CASE
                WHEN RD2.FECSEGUIMIENTO IS NULL
                THEN RD.FECSEGUIMIENTO
                ELSE RD2.FECSEGUIMIENTO
            END AS FechaGestion,
            CASE
                WHEN RD.TIPENTIDAD = 1
                THEN IPS.DSCRIPIPS
                WHEN RD.TIPENTIDAD = 2
                THEN RTRIM(ENT2.NOMENTIDA)
                WHEN RD.TIPENTIDAD = 3
                THEN 'CRUE'
                WHEN RD.TIPENTIDAD = 4
                THEN RTRIM(RD.OTRAENTIDAD)
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el listado de gestión y seguimiento de referencias (remisiones) activas de pacientes, mostrando por cada remisión: el centro de atención y unidad de origen, la identificación y nombre del paciente, su edad, el diagnóstico de ingreso (CIE-10), la entidad aseguradora (EPS/ARS), y el detalle de cada gestión de seguimiento realizada (fecha, contacto, estado de aceptación, entidad gestora —IPS contratada, entidad externa, CRUE u otra— y el usuario responsable). Combina los datos de la remisión (HCREFCONP) con el ingreso hospitalario (ADINGRESO), la ficha del paciente (INPACIENT), la unidad funcional (INUNIFUNC), el centro de atención (ADCENATEN), el diagnóstico CIE-10 (INDIAGNOS), las entidades aseguradoras (INENTIDAD), las IPS de la red (ADCONTIPS) y los usuarios del sistema (SEGusuaru). Filtra únicamente las remisiones en estado activo (estado=2) y ordena los resultados por fecha de seguimiento, siendo útil para el equipo de regulación y referencia que monitorea el estado de las remisiones pendientes de resolución.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la gestión de referencias de pacientes activas, mostrando datos del ingreso, entidad gestionada, estado de aceptación y seguimientos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen referencias con estado activo (ESTADO=2) en la tabla de referencias y contrarreferencias de pacientes.; Cada referencia tiene al menos un detalle de gestión asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan referencias con ESTADO = 2.; La entidad gestora se determina por el tipo de entidad (1=IPS, 2=Entidad/EPS, 3=CRUE, 4=Otra).; El estado de gestión se traduce a una de cuatro etiquetas fijas (SI acepta, No Acepta, Pendiente, No Aplica).; Cuando existe un detalle posterior vinculado (RD2), su seguimiento prevalece sobre el original para la fecha y contacto de gestión.; La edad del paciente se calcula respecto a la fecha actual del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Paciente; Ingreso hospitalario; Diagnóstico; Entidad/EPS; IPS; CRUE (Centro Regulador de Urgencias y Emergencias); Centro de atención; Unidad funcional; Seguimiento de gestión; Estado de aceptación de referencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo referencias con ESTADO = 2 (activas/vigentes), ordenadas por la fecha de seguimiento más reciente (la del detalle hijo si existe, sino la del padre) y luego por fecha de registro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RD2.FECSEGUIMIENTO IS NULL → Usa la fecha de seguimiento del detalle principal (RD.FECSEGUIMIENTO) como FechaGestion y ContactoGestion. else Usa la fecha y contacto del detalle hijo/posterior (RD2).; si RD.TIPENTIDAD = 1 → EntidadGestion se toma de la descripción de IPS (ADCONTIPS).; si RD.TIPENTIDAD = 2 → EntidadGestion se toma del nombre de entidad (INENTIDAD).; si RD.TIPENTIDAD = 3 → EntidadGestion se rotula fija como ''CRUE''.; si RD.TIPENTIDAD = 4 → EntidadGestion toma el texto libre OTRAENTIDAD.; si RD.REGESTADO = ''1'' → EstadoGestion = ''SI acepta''.; si RD.REGESTADO = ''2'' → EstadoGestion = ''No Acepta''.; si RD.REGESTADO = ''3'' → EstadoGestion = ''Pendiente''.; si RD.REGESTADO = ''4'' → EstadoGestion = ''No Aplica''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREFCONTD; dbo.ADCONTIPS; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaGestionPaciente';
-- GO
