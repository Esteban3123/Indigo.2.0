
CREATE PROCEDURE [MedicalLaboratory].[OrderData] (
    @IdOrden INT,
    @Hospitalario BIT
) AS BEGIN
SET NOCOUNT ON;

DECLARE @CodeBD AS VARCHAR(3) = SUBSTRING(DB_NAME(), 7, 3);

IF @Hospitalario = 1
BEGIN
    IF NOT EXISTS (SELECT 1 FROM HCORDLABO WHERE AUTO = @IdOrden)
        RETURN;

    SELECT (
        SELECT
            @CodeBD AS 'Evento.CodeBD',
            RTRIM(A.AUTO) AS 'Orden.IdOrden',
            'Hospitalario' AS 'Orden.Origen',
            CASE A.PRISERIPS WHEN 1 THEN 'Urgente' WHEN 2 THEN 'Rutinario' ELSE '' END AS 'Orden.Prioridad',
            RTRIM(A.NUMINGRES) AS 'Orden.Ingreso',
            RTRIM(COA.Name) AS 'Orden.CausaIngreso',
            RTRIM(A.NUMEFOLIO) AS 'Orden.Folio',
            RTRIM(FORMAT(A.FECORDMED, 'dd/MM/yyyy HH:mm')) AS 'Orden.FechaOrden',
            RTRIM(D.CODSERIPS) AS 'Orden.Servicio.CodigoServicio',
            RTRIM(D.DESSERIPS) AS 'Orden.Servicio.DescripcionServicio',
            ISNULL(CONVERT(VARCHAR(50), A.IDDESCRIPCIONRELACIONADA), '') AS 'Orden.Servicio.IdDescripcionRelacionada',
            RTRIM(ISNULL(CD.name,'')) AS 'Orden.Servicio.DescripcionRelacionada',
            RTRIM(A.OBSSERIPS) AS 'Orden.Observacion',
            RTRIM(E.CODCENATE) AS 'CentroAtencion.CodigoCentro',
            RTRIM(E.NOMCENATE) AS 'CentroAtencion.NombreCentro',
            RTRIM(F.UFUCODIGO) AS 'UnidadFuncional.CodigoUnidad',
            RTRIM(F.UFUDESCRI) AS 'UnidadFuncional.DescripcionUnidad',
            RTRIM(K.CODDIAGNO) AS 'Diagnostico.CodigoDiagnostico',
            RTRIM(K.NOMDIAGNO) AS 'Diagnostico.DescripcionDiagnostico',
            CASE B.IPTIPOPAC 
                WHEN 1 THEN 'Contributivo'
                WHEN 2 THEN 'Subsidiado'
                WHEN 3 THEN 'No afiliado'
                WHEN 4 THEN 'Particular'
                WHEN 5 THEN 'Otro'
                WHEN 6 THEN 'Desplazado Reg. Contributivo'
                WHEN 7 THEN 'Desplazado Reg. Subsidiado'
                WHEN 8 THEN 'Desplazado No Asegurado'
                WHEN 9 THEN 'Especial o excepción'
                WHEN 10 THEN 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud'
                WHEN 11 THEN 'Tomador / amparado ARL'
                WHEN 12 THEN 'Tomador / amparado SOAT'
                WHEN 13 THEN 'Tomador / amparado planes voluntarios de salud'
                ELSE ''
            END AS 'Paciente.TipoAfiliacion',
            ISNULL(RTRIM(L.DESCCAMAS), 'SIN_CAMA') AS 'Paciente.Cama',
            RTRIM(G.SIGLA) AS 'Paciente.TipoDocumentoSiglas',
            RTRIM(G.NOMBRE) AS 'Paciente.TipoDocumento',
            RTRIM(B.IPCODPACI) AS 'Paciente.CodigoPaciente',
            RTRIM(B.IPNOMCOMP) AS 'Paciente.NombreCompleto',
            RTRIM(B.IPPRINOMB) AS 'Paciente.PrimerNombre',
            RTRIM(B.IPSEGNOMB) AS 'Paciente.SegundoNombre',
            RTRIM(B.IPPRIAPEL) AS 'Paciente.PrimerApellido',
            RTRIM(B.IPSEGAPEL) AS 'Paciente.SegundoApellido',
            RTRIM(ISNULL(B.IPGRUPSAN, '')) AS 'Paciente.GrupoSanguineo',
            RTRIM(ISNULL(B.IPRHSANGR, '')) AS 'Paciente.RH',
            CASE B.IPSEXOPAC 
                WHEN 1 THEN 'Masculino' 
                WHEN 2 THEN 'Femenino' 
                ELSE NULL 
            END AS 'Paciente.Sexo',
            RTRIM(GT.Name) AS 'Paciente.IdentidadGenero',
            CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS 'Paciente.Gestacion',
            RTRIM(FORMAT(B.IPFECNACI, 'dd/MM/yyyy')) AS 'Paciente.FechaNacimiento',
            IIF(GE.DESGRUPET IS NOT NULL, RTRIM(GE.DESGRUPET),'') AS 'Paciente.GrupoEtnico',
            IIF(B.EthnicCommunity IS NOT NULL, RTRIM(B.EthnicCommunity), '') AS 'Paciente.ComunidadEtnica',
            RTRIM(ENT.Code) AS 'Paciente.EntidadAdministradora.CodigoEntidad',
            RTRIM(ENT.Name) AS 'Paciente.EntidadAdministradora.NombreEntidad',
            dbo.Patient_Country(A.IPCODPACI) AS 'Paciente.InformacionContacto.Pais',
            dbo.patient_department(A.IPCODPACI) AS 'Paciente.InformacionContacto.Departamento',
            dbo.patient_municipality(A.IPCODPACI) AS 'Paciente.InformacionContacto.Municipio',
            dbo.Only_Patient_Address(A.IPCODPACI) AS 'Paciente.InformacionContacto.Direccion',
            RTRIM(ISNULL(B.IPTELEFON, '')) AS 'Paciente.InformacionContacto.Telefono',
            RTRIM(ISNULL(B.IPTELMOVI, '')) AS 'Paciente.InformacionContacto.Movil',
            RTRIM(ISNULL(B.CORELEPAC, '')) AS 'Paciente.InformacionContacto.Correo',
            RTRIM(H.SIGLA) AS 'ProfesionalSalud.TipoDocumentoSiglas',
            RTRIM(H.NOMBRE) AS 'ProfesionalSalud.TipoDocumento',
            RTRIM(C.CODPROSAL) AS 'ProfesionalSalud.CodigoProfesional',
            RTRIM(C.NOMMEDICO) AS 'ProfesionalSalud.NombreProfesional',
            RTRIM(I.CODESPTRA) AS 'ProfesionalSalud.Especialidad.CodigoEspecialidad',
            RTRIM(dbo.Especialidades(I.CODESPTRA)) AS 'ProfesionalSalud.Especialidad.DescripcionEspecialidad'
        FROM HCORDLABO A
            INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
            INNER JOIN INPROFSAL C ON A.CODPROSAL = C.CODPROSAL
            INNER JOIN INCUPSIPS D ON A.CODSERIPS = D.CODSERIPS
            INNER JOIN ADCENATEN E ON A.CODCENATE = E.CODCENATE
            INNER JOIN INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
            INNER JOIN ADTIPOIDENTIFICA G ON G.CODIGO = B.IPTIPODOC
            INNER JOIN ADTIPOIDENTIFICA H ON H.ID = C.IDADTIPOIDENTIFICA
            INNER JOIN HCHISPACA I ON A.IPCODPACI = I.IPCODPACI AND A.NUMINGRES = I.NUMINGRES AND A.NUMEFOLIO = I.NUMEFOLIO
            INNER JOIN ADINGRESO J ON A.NUMINGRES = J.NUMINGRES
            INNER JOIN INDIAGNOS K ON A.CODDIAGNO = K.CODDIAGNO
            INNER JOIN Contract.HealthAdministrator ENT ON J.GENCONENTITY = ENT.Id
            INNER JOIN dbo.CausesOfAttention AS COA ON COA.Code = J.ICAUSAING
            LEFT JOIN CHCAMASHO L ON J.CODCAMACT = L.CODICAMAS
            LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
            LEFT JOIN CONTRACT.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
            LEFT JOIN Admissions.GenderTypes GT ON GT.Id = B.IdGenderIdentity
            LEFT JOIN dbo.ADGRUETNI GE ON GE.CODGRUPOE = B.CODGRUPOE
            LEFT JOIN dbo.HCRIESGOSP AS RP ON RP.NUMINGRCES = A.NUMINGRES
        WHERE A.AUTO = @IdOrden
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    ) AS Payload
END
ELSE -- Ambulatorio
BEGIN
    IF NOT EXISTS (SELECT 1 FROM AMBORDLAB WHERE AUTO = @IdOrden)
        RETURN;

    SELECT (
        SELECT
            @CodeBD AS 'Evento.CodeBD',
            RTRIM(A.AUTO) AS 'Orden.IdOrden',
            'Ambulatorio' AS 'Orden.Origen',
            'Rutinario' AS 'Orden.Prioridad',
            RTRIM(A.NUMINGRES) AS 'Orden.Ingreso',
            RTRIM(COA.Name) AS 'Orden.CausaIngreso',
            '' AS 'Orden.Folio',
            RTRIM(FORMAT(A.FECORDMED, 'dd/MM/yyyy HH:mm')) AS 'Orden.FechaOrden',
            RTRIM(D.CODSERIPS) AS 'Orden.Servicio.CodigoServicio',
            RTRIM(D.DESSERIPS) AS 'Orden.Servicio.DescripcionServicio',
            ISNULL(CONVERT(VARCHAR(50), A.IDDESCRIPCIONRELACIONADA), '') AS 'Orden.Servicio.IdDescripcionRelacionada',
            RTRIM(ISNULL(CD.name,'')) AS 'Orden.Servicio.DescripcionRelacionada',
            RTRIM(A.OBSERVACI) AS 'Orden.Observacion',
            RTRIM(E.CODCENATE) AS 'CentroAtencion.CodigoCentro',
            RTRIM(E.NOMCENATE) AS 'CentroAtencion.NombreCentro',
            RTRIM(F.UFUCODIGO) AS 'UnidadFuncional.CodigoUnidad',
            RTRIM(F.UFUDESCRI) AS 'UnidadFuncional.DescripcionUnidad',
            '' AS 'Diagnostico.CodigoDiagnostico',
            '' AS 'Diagnostico.DescripcionDiagnostico',
            CASE B.IPTIPOPAC 
                WHEN 1 THEN 'Contributivo'
                WHEN 2 THEN 'Subsidiado'
                WHEN 3 THEN 'No afiliado'
                WHEN 4 THEN 'Particular'
                WHEN 5 THEN 'Otro'
                WHEN 6 THEN 'Desplazado Reg. Contributivo'
                WHEN 7 THEN 'Desplazado Reg. Subsidiado'
                WHEN 8 THEN 'Desplazado No Asegurado'
                WHEN 9 THEN 'Especial o excepción'
                WHEN 10 THEN 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud'
                WHEN 11 THEN 'Tomador / amparado ARL'
                WHEN 12 THEN 'Tomador / amparado SOAT'
                WHEN 13 THEN 'Tomador / amparado planes voluntarios de salud'
                ELSE ''
            END AS 'Paciente.TipoAfiliacion',
            '' AS 'Paciente.Cama',
            RTRIM(G.SIGLA) AS 'Paciente.TipoDocumentoSiglas',
            RTRIM(G.NOMBRE) AS 'Paciente.TipoDocumento',
            RTRIM(B.IPCODPACI) AS 'Paciente.CodigoPaciente',
            RTRIM(B.IPNOMCOMP) AS 'Paciente.NombreCompleto',
            RTRIM(B.IPPRINOMB) AS 'Paciente.PrimerNombre',
            RTRIM(B.IPSEGNOMB) AS 'Paciente.SegundoNombre',
            RTRIM(B.IPPRIAPEL) AS 'Paciente.PrimerApellido',
            RTRIM(B.IPSEGAPEL) AS 'Paciente.SegundoApellido',
            RTRIM(B.IPGRUPSAN) AS 'Paciente.GrupoSanguineo',
            RTRIM(ISNULL(B.IPRHSANGR, '')) AS 'Paciente.RH',
            CASE B.IPSEXOPAC 
                WHEN 1 THEN 'Masculino' 
                WHEN 2 THEN 'Femenino' 
                ELSE NULL 
            END AS 'Paciente.Sexo',
            RTRIM(GT.Name) AS 'Paciente.IdentidadGenero',
            CASE B.IPSEXOPAC WHEN 1 THEN 'No Aplica' ELSE CASE RP.GESTACION WHEN 1 THEN 'Si' ELSE 'No' END END AS 'Paciente.Gestacion',
            RTRIM(FORMAT(B.IPFECNACI, 'dd/MM/yyyy')) AS 'Paciente.FechaNacimiento',
            IIF(GE.DESGRUPET IS NOT NULL, RTRIM(GE.DESGRUPET),'') AS 'Paciente.GrupoEtnico',
            IIF(B.EthnicCommunity IS NOT NULL, RTRIM(B.EthnicCommunity), '') AS 'Paciente.ComunidadEtnica',
            RTRIM(ENT.Code) AS 'Paciente.EntidadAdministradora.CodigoEntidad',
            RTRIM(ENT.Name) AS 'Paciente.EntidadAdministradora.NombreEntidad',
            dbo.Patient_Country(A.IPCODPACI) AS 'Paciente.InformacionContacto.Pais',
            dbo.patient_department(A.IPCODPACI) AS 'Paciente.InformacionContacto.Departamento',
            dbo.patient_municipality(A.IPCODPACI) AS 'Paciente.InformacionContacto.Municipio',
            dbo.Only_Patient_Address(A.IPCODPACI) AS 'Paciente.InformacionContacto.Direccion',
            RTRIM(ISNULL(B.IPTELEFON, '')) AS 'Paciente.InformacionContacto.Telefono',
            RTRIM(ISNULL(B.IPTELMOVI, '')) AS 'Paciente.InformacionContacto.Movil',
            RTRIM(ISNULL(B.CORELEPAC, '')) AS 'Paciente.InformacionContacto.Correo',
            RTRIM(H.SIGLA) AS 'ProfesionalSalud.TipoDocumentoSiglas',
            RTRIM(H.NOMBRE) AS 'ProfesionalSalud.TipoDocumento',
            RTRIM(C.CODPROSAL) AS 'ProfesionalSalud.CodigoProfesional',
            RTRIM(C.NOMMEDICO) AS 'ProfesionalSalud.NombreProfesional',
            RTRIM(C.CODESPEC1) AS 'ProfesionalSalud.Especialidad.CodigoEspecialidad',
            RTRIM(dbo.Especialidades(C.CODESPEC1)) AS 'ProfesionalSalud.Especialidad.DescripcionEspecialidad'
        FROM AMBORDLAB A
            INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
            INNER JOIN INPROFSAL C ON A.CODPROSAL = C.CODPROSAL
            INNER JOIN INCUPSIPS D ON A.CODSERIPS = D.CODSERIPS
            INNER JOIN ADCENATEN E ON A.CODCENATE = E.CODCENATE
            INNER JOIN INUNIFUNC F ON A.UFUCODIGO = F.UFUCODIGO
            INNER JOIN ADTIPOIDENTIFICA G ON G.CODIGO = B.IPTIPODOC
            INNER JOIN ADTIPOIDENTIFICA H ON H.ID = C.IDADTIPOIDENTIFICA
            INNER JOIN ADINGRESO J ON A.NUMINGRES = J.NUMINGRES
            INNER JOIN Contract.HealthAdministrator ENT ON J.GENCONENTITY = ENT.Id
            INNER JOIN dbo.CausesOfAttention AS COA ON COA.Code = J.ICAUSAING
            LEFT JOIN CONTRACT.CUPSEntityContractDescriptions CDD WITH(NOLOCK) ON CDD.Id = A.IDDESCRIPCIONRELACIONADA 
            LEFT JOIN CONTRACT.ContractDescriptions CD WITH(NOLOCK) ON CD.Id = CDD.ContractDescriptionId 
            LEFT JOIN Admissions.GenderTypes GT ON GT.Id = B.IdGenderIdentity
            LEFT JOIN dbo.ADGRUETNI GE ON GE.CODGRUPOE = B.CODGRUPOE
            LEFT JOIN dbo.HCRIESGOSP AS RP ON RP.NUMINGRCES = A.NUMINGRES
        WHERE A.AUTO = @IdOrden
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
    ) AS Payload
END
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que genera un payload JSON con el evento "Muestra recolectada" para una orden de laboratorio, diferenciando entre órdenes hospitalarias (HCORDLABO) y ambulatorias (AMBORDLAB). Consolida datos de la orden, paciente, profesional de salud, centro de atención, unidad funcional, diagnóstico y entidad administradora, incluyendo tipo de afiliación, identidad de género y condición de gestación. El resultado es consumido por un sistema de eventos/mensajería para notificar la recolección de muestra en el módulo de laboratorio clínico.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el payload JSON del evento ''Muestra recolectada'' de una orden de laboratorio, diferenciando la construcción según el canal hospitalario o ambulatorio.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse si la orden es hospitalaria o ambulatoria mediante el flag de canal.; La orden debe existir en HCORDLABO (hospitalario) o AMBORDLAB (ambulatorio) con el AUTO indicado; en caso contrario el procedimiento termina sin resultados.; Deben existir los registros relacionados obligatorios (paciente INPACIENT, profesional INPROFSAL, servicio INCUPSIPS, centro ADCENATEN, unidad funcional INUNIFUNC, tipos de identificación, ingreso ADINGRESO, entidad administradora, causa de atención y, en hospitalario, historia HCHISPACA y diagnóstico INDIAGNOS) por los INNER JOIN.; El nombre de la base de datos debe tener al menos 9 caracteres para extraer el CodeBD con SUBSTRING(DB_NAME(),7,3).', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de evento (IdEvento) se genera como NEWID() único por ejecución.; El TipoEvento siempre es ''Muestra recolectada'' y el Origen siempre ''Laboratorio''.; La fecha del evento se formatea como ''dd/MM/yyyy HH:mm:ss'' usando common.GETDATE().; El CodeBD se deriva siempre de los caracteres 7 a 9 del nombre de la base de datos (DB_NAME()).; Si la orden no existe en la tabla correspondiente al canal, no se retorna ningún resultset.; El payload se serializa como JSON sin envoltorio de arreglo (FOR JSON PATH, WITHOUT_ARRAY_WRAPPER).; Para órdenes ambulatorias la prioridad es siempre ''Rutinario'', el folio es vacío y no se incluye diagnóstico ni cama.; Para pacientes masculinos la gestación siempre se reporta como ''No Aplica''.; Si el paciente no tiene cama asignada en órdenes hospitalarias se devuelve ''SIN_CAMA''.; El procedimiento es de solo lectura: no realiza INSERT/UPDATE/DELETE.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio; Evento ''Muestra recolectada''; Atención hospitalaria vs ambulatoria; Prioridad de la orden (urgente/rutinario); Causa de ingreso; Centro de atención; Unidad funcional; Diagnóstico; Tipo de afiliación / régimen de salud; Cama del paciente; Tipo de documento; Identidad de género; Gestación; Grupo étnico y comunidad étnica; Entidad administradora de salud (EPS); Profesional de salud y especialidad; Información de contacto del paciente (país, departamento, municipio, dirección); Descripción contractual del CUPS', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Si @Hospitalario=1 y existe la orden en HCORDLABO con AUTO=@IdOrden, retorna fila con Id, EventType, AggregateId, FechaEvento y Payload JSON construido desde fuentes hospitalarias.; [RETURN_RESULT] resultset: Si @Hospitalario=0 y existe la orden en AMBORDLAB con AUTO=@IdOrden, retorna fila con Id, EventType, AggregateId, FechaEvento y Payload JSON construido desde fuentes ambulatorias.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Hospitalario = 1 → Construye payload desde HCORDLABO con prioridad derivada de PRISERIPS (1=Urgente, 2=Rutinario), folio, observación clínica, diagnóstico (INDIAGNOS) y cama (CHCAMASHO). else Construye payload desde AMBORDLAB con prioridad fija ''Rutinario'', folio vacío, sin diagnóstico y sin cama.; si Cuando la orden hospitalaria no existe en HCORDLABO con AUTO = id solicitado → Sale del procedimiento sin retornar resultset (RETURN).; si Cuando la orden ambulatoria no existe en AMBORDLAB con AUTO = id solicitado → Sale del procedimiento sin retornar resultset (RETURN).; si Sexo del paciente IPSEXOPAC = 1 (masculino) → Marca ''Paciente.Gestacion'' como ''No Aplica'' independientemente de HCRIESGOSP. else Si HCRIESGOSP.GESTACION = 1 entonces ''Si'', en otro caso ''No''.; si IPTIPOPAC del paciente entre 1 y 13 → Mapea a etiqueta de régimen/tipo de afiliación (Contributivo, Subsidiado, No afiliado, Particular, Otro, Desplazados, Especial, PPL, ARL, SOAT, planes voluntarios). else Devuelve cadena vacía.; si PRISERIPS de la orden hospitalaria → 1 → ''Urgente''; 2 → ''Rutinario''; cualquier otro → cadena vacía.', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INPACIENT; dbo.INPROFSAL; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADTIPOIDENTIFICA; dbo.HCHISPACA; dbo.ADINGRESO; dbo.INDIAGNOS; Contract.HealthAdministrator; dbo.CausesOfAttention; dbo.CHCAMASHO; CONTRACT.CUPSEntityContractDescriptions; CONTRACT.ContractDescriptions; Admissions.GenderTypes; dbo.ADGRUETNI; dbo.HCRIESGOSP', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalLaboratory', @level1type=N'PROCEDURE', @level1name=N'OrderData';
-- GO
