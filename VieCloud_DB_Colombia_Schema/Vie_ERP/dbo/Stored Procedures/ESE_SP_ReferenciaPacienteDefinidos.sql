CREATE PROCEDURE [dbo].[ESE_SP_ReferenciaPacienteDefinidos]
AS
     SELECT RTRIM(R.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS Centro, 
            RTRIM(R.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadRemision, 
            RTRIM(ING.CODENTIDA) + ' - ' + RTRIM(ENT.NOMENTIDA) AS Entidad, 
            R.NUMINGRES AS Ingreso, 
            R.IPCODPACI AS Identificacion, 
            RTRIM(p.IPNOMCOMP) AS Paciente, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad, 
            ING.CODDIAING + ' - ' + RTRIM(D.NOMDIAGNO) AS Diagnostico,
            CASE
                WHEN RD2.FECSEGUIMIENTO IS NULL
                THEN RD.FECSEGUIMIENTO
                ELSE RD2.FECSEGUIMIENTO
            END AS FechaGestion,
            CASE
                WHEN RD.TIPENTIDAD = 1
                THEN RTRIM(IPS.DSCRIPIPS)
                WHEN RD.TIPENTIDAD = 2
                THEN RTRIM(ENT2.NOMENTIDA)
                WHEN RD.TIPENTIDAD = 3
                THEN 'CRUE'
                WHEN RD.TIPENTIDAD = 4
                THEN RTRIM(RD.OTRAENTIDAD)
            END AS EntidadGestion,
            CASE
                WHEN RD2.NOMCONTACT IS NULL
                THEN RTRIM(RD.NOMCONTACT)
                ELSE RTRIM(RD2.NOMCONTACT)
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
            RTRIM(RD.OBSERVACI) AS Observaciones, 
            RTRIM(RD.CODUSUAREG) + ' - ' + RTRIM(U.NOMUSUARI) AS UsuarioGestion, 
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

     WHERE R.ESTADO NOT IN(1, 2) --and R.IPCODPACI in ('26555868')-- AND RD.ID=27062
     ORDER BY CASE
                  WHEN RD2.FECSEGUIMIENTO IS NULL
                  THEN RD.FECSEGUIMIENTO
                  ELSE RD2.FECSEGUIMIENTO
              END, 
              RD.FECHCREREG;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y consolida el listado de referencias de pacientes que tienen un proceso de gestión definido (con seguimiento en curso), excluyendo las remisiones ya finalizadas o canceladas. Combina datos de la solicitud de referencia (HCREFCONP) con la información del ingreso, datos del paciente (cédula, nombre, edad), diagnóstico de ingreso (CIE-10), entidad aseguradora, unidad funcional y centro de atención de origen. Adicionalmente incorpora el detalle del seguimiento de gestión (HCREFCONTD), mostrando la fecha de gestión, entidad gestora (IPS contratada, EPS, CRUE u otra), contacto, estado de aceptación (acepta, no acepta, pendiente, no aplica), observaciones y el usuario que registró la gestión. Se usa en el módulo de referencia y contrarreferencia para el seguimiento operativo y gerencial de las remisiones activas de pacientes hacia otras instituciones o especialidades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las referencias de pacientes con su última gestión de seguimiento, mostrando centro, unidad, entidad, diagnóstico, estado de aceptación y entidad gestora (IPS, EAPB, CRUE u otra).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de ingreso, paciente, unidad funcional, centro de atención, entidad y diagnóstico relacionados a la referencia.; Cada cabecera de referencia debe tener al menos un detalle de gestión asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado de gestión solo admite cuatro valores: 1=SI acepta, 2=No Acepta, 3=Pendiente, 4=No Aplica.; El tipo de entidad gestora se restringe a 4 dominios: IPS, Entidad (EAPB), CRUE y Otra.; Las referencias con ESTADO 1 o 2 nunca aparecen en el resultado.; La fecha y contacto de gestión efectiva siempre corresponden al detalle hijo si existe; en caso contrario al detalle padre.; Los resultados se ordenan cronológicamente por la fecha de gestión efectiva y luego por la fecha de registro del detalle.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia de paciente; Ingreso hospitalario; Diagnóstico; Centro de atención; Unidad funcional; Entidad / EAPB; IPS; CRUE (Centro Regulador de Urgencias y Emergencias); Seguimiento/gestión de referencia; Estado de aceptación de referencia; Edad del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente referencias cuyo ESTADO no esté en (1,2), excluyendo así referencias canceladas/cerradas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RD2.FECSEGUIMIENTO IS NULL (no existe gestión hija) → Toma fecha y contacto del detalle padre RD else Toma fecha y contacto del detalle hijo RD2 (gestión más reciente); si RD.TIPENTIDAD = 1 → La entidad gestora se resuelve contra catálogo de IPS (ADCONTIPS); si RD.TIPENTIDAD = 2 → La entidad gestora se resuelve contra catálogo de entidades (INENTIDAD); si RD.TIPENTIDAD = 3 → La entidad gestora se rotula como ''CRUE''; si RD.TIPENTIDAD = 4 → La entidad gestora se toma del campo libre OTRAENTIDAD; si RD.REGESTADO = ''1''/''2''/''3''/''4'' → Traduce a ''SI acepta'', ''No Acepta'', ''Pendiente'' o ''No Aplica'' respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.ADINGRESO; dbo.INPACIENT; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.HCREFCONTD; dbo.ADCONTIPS; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaPacienteDefinidos';
-- GO
