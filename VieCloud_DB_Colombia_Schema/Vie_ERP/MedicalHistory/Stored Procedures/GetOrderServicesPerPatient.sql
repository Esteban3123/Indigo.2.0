CREATE PROCEDURE [MedicalHistory].[GetOrderServicesPerPatient]
@PatientCode Varchar(25),
@AdmissionNumber Varchar(25)
AS
BEGIN

	SET NOCOUNT ON;
-- Procedimientos No Qx

	SELECT
		0 AS Nuevo
		,A.AUTO
		,'Procedimientos No Quirúrgicos' as 'Tipo de Servicio'
		,A.FECORDMED
		,A.FECORDMED AS 'FECHASUGE'
		,RTRIM(E.DESSERIPS) AS 'Servicio'
		,A.CODSERIPS
		,A.CODSERIPS AS 'Codigo'
		,B.NOMMEDICO AS 'Medico'
		,A.PRISERIPS
		,CASE A.PRISERIPS
			WHEN '1'
				THEN 'Urgente'
			WHEN '2'
				THEN 'Rutinario'
			END AS 'Tipo'
		,A.ESTSERIPS
		,RTRIM(D.UFUDESCRI) + ' - ' + RTRIM(C.NOMCENATE) AS Unidad
		,A.MANEXTPRO
		,A.MANEXTPRO As 'Externo'
		,A.IDETIPHIS
		,A.NUMEFOLIO
		,A.NUMEFOLIO AS 'Folio'
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODDIAGNO
		,A.INDAUDFOR
		,E.TIPSERIPS
		,E.SERIPSDASH
		,Null AS ESTALEIMG
		,Cast(0 AS bit) REALINOTIF
		,Null AS 'Modalidad'
		,Cast(0 AS bit) ServicioRealizaSitio
		,Cast(0 AS bit) AS EXMREASIT
		,Cast(0 As bit) AS ESTALELAB
		,Cast(0 As bit) AS ESTALEPAT
		,Cast(0 As bit) AS Terapia
		,Cast(0 As int) AS SOLICITAMATOST
		,Null As OTROSMATERIALES
		,CASE ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Completado' WHEN '3' THEN 'Interpretado' WHEN '4' THEN 'Sin Interfaz' WHEN '5' THEN 'Anulado' END AS 'EstadoName'
        ,A.ESTSERIPS As 'Estado'
		,'' AS AlertaJustificacion
		,Null AS Ayuda
		,Null AS Justificacion
		,Null AS Incidencia
		,A.CANSERIPS
		,A.OBSSERIPS
		,RTRIM(N.DESESPECI) AS Especialidad
		,Null AS AnularServicio
        ,A.CODPROSAL
		,Null AS CodigoMinisterio
		,A.LATERALIDAD
		,CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDADDESP'
		,A.IDDESCRIPCIONRELACIONADA
		,CD.NAME AS 'DescripcionRelacionada'
		,Null AS PrescripcionJson
		,Null AS CodigoCupsMipres
        ,A.SOLICITASALA
		,A.EXREASITI
		,Null AS Archivo
		,CAST(0 AS BIT) AS SERREAINT
		,A.UFUCODIGO
		,A.CODCENATE
		,CAST(0 AS BIT) AS Resultado
		,'HCORDPRON' AS 'Tabla'
		,pm.DESHALLAZ as PROHallazgos
		,pm.DESCOMPLI as PROComplicaciones
		,pm.FECREAPRO as PROFecha
	FROM dbo.HCORDPRON A
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
	INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
	INNER JOIN dbo.HCHISPACA AS J ON A.NUMEFOLIO = J.NUMEFOLIO AND A.IPCODPACI = J.IPCODPACI
	LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
	LEFT JOIN contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId
	LEFT JOIN dbo.HCINFPROM pm on pm.IPCODPACI = a.IPCODPACI and pm.CODSERIPS = a.CODSERIPS and pm.NUMINGRES = a.NUMINGRES and pm.NUMEFOLIO = a.NUMEFOLIO 
	WHERE A.IPCODPACI = @PatientCode AND A.NUMINGRES = @AdmissionNumber

UNION ALL

--Procedimientos Qx

	SELECT
		0 AS Nuevo
		,A.AUTO 
		,'Procedimientos Quirúrgicos' as 'Tipo de Servicio'
		
		,A.FECORDMED
		,A.FECORDMED AS 'FECHASUGE'
		,RTRIM(E.DESSERIPS) AS 'Servicio'
		,A.CODSERIPS
		,A.CODSERIPS AS 'Codigo'
		,B.NOMMEDICO AS 'Medico'
		,A.PRISERIPS
		,CASE A.PRISERIPS
			WHEN '1'
				THEN 'Emergencia'
			WHEN '2'
				THEN 'Urgencia'
			WHEN '3'
				THEN 'Normal'
			WHEN '4'
				THEN 'Definir Conducta'
			END AS 'Tipo'
		,A.ESTSERIPS
		,RTRIM(D.UFUDESCRI) + ' - ' + RTRIM(C.NOMCENATE) AS Unidad
		,A.MANEXTPRO
		,A.MANEXTPRO As 'Externo'
		,A.IDETIPHIS
		,A.NUMEFOLIO
		,A.NUMEFOLIO AS 'Folio'
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODDIAGNO
		,A.INDAUDFOR
		,E.TIPSERIPS
        ,E.SERIPSDASH
		,Null AS ESTALEIMG
		,CAST(0 AS bit) AS REALINOTIF
		,Null AS 'Modalidad'
		,Null AS ServicioRealizaSitio
		,Null AS EXMREASIT
		,Null AS ESTALELAB
		,Null AS ESTALEPAT
		,Null AS Terapia
		,A.SOLICITAMATOST
		,A.OTROSMATERIALES
		,CASE ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Sala Programada' WHEN '3' THEN 'Cancelado' WHEN '4' THEN 'Resultado Revisado' WHEN '5' THEN 'Anulado' WHEN '6' THEN 'Programado no realizado' END AS 'EstadoName'
		,A.ESTSERIPS As 'Estado'
		,'' AS AlertaJustificacion
		,Null AS Ayuda
		,Null AS Justificacion
		,Null AS Incidencia
		,A.CANSERIPS
		,A.OBSSERIPS
		,RTRIM(N.DESESPECI) AS Especialidad
		,Null AS AnularServicio
		,A.CODPROSAL
        ,Null AS CodigoMinisterio
        ,A.LATERALIDAD
        ,CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS 'LATERALIDADDESP'
		,A.IDDESCRIPCIONRELACIONADA
		,CD.NAME AS 'DescripcionRelacionada'
		,Null AS PrescripcionJson
		,Null AS CodigoCupsMipres
    	,A.SOLICITASALA
		,NUll AS EXREASITI
		,'' AS Archivo
		,Null AS SERREAINT
		,A.UFUCODIGO
		,A.CODCENATE
		,Null AS Resultado
		,'HCORDPROQ' AS 'Tabla'
		,pm.DESHALLAZ as PROHallazgos
		,pm.DESCOMPLI as PROComplicaciones
		,pm.FECREAPRO as PROFecha
	FROM dbo.HCORDPROQ A	
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
	INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
	INNER JOIN DBO.HCHISPACA AS J ON A.NUMEFOLIO = J.NUMEFOLIO AND A.IPCODPACI = J.IPCODPACI
	LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
	LEFT JOIN contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId
	LEFT JOIN dbo.HCINFPROM pm on pm.IPCODPACI = a.IPCODPACI and pm.CODSERIPS = a.CODSERIPS and pm.NUMINGRES = a.NUMINGRES and pm.NUMEFOLIO = a.NUMEFOLIO 
	WHERE A.IPCODPACI = @PatientCode AND A.NUMINGRES = @AdmissionNumber

UNION ALL

-- Patologias

	SELECT
		0 AS Nuevo
		,A.AUTO
		,'Patologías' as 'Tipo de Servicio'
		
		,A.FECORDMED
		,A.FECORDMED as 'FECHASUGE'
		,RTRIM(E.DESSERIPS) AS 'Servicio'
		,A.CODSERIPS
		,A.CODSERIPS AS 'Codigo'
		,B.NOMMEDICO AS 'Medico'
		,A.PRISERIPS
		,CASE A.PRISERIPS
			WHEN '1'
				THEN 'Urgente'
			WHEN '2'
				THEN 'Rutinario'
			END AS 'Tipo'
		,A.ESTSERIPS
		,RTRIM(D.UFUDESCRI) + ' - ' + RTRIM(C.NOMCENATE) AS Unidad
		,A.MANEXTPRO
		,A.MANEXTPRO As 'Externo'
		,A.IDETIPHIS
		,A.NUMEFOLIO
		,A.NUMEFOLIO AS 'Folio'
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODDIAGNO
		,A.INDAUDFOR
		,E.TIPSERIPS
		,E.SERIPSDASH
		,Null AS ESTALEIMG
		,CAST(0 AS bit) AS REALINOTIF
		,Cast(0 AS bit) AS ServicioRealizaSitio
		,Cast(0 AS bit) AS EXMREASIT
		,Null AS ESTALELAB
		,Cast(0 As bit) AS Terapia
		,A.ESTALEPAT
		,Null AS 'Modalidad'
		,Cast(0 As int) As SOLICITAMATOST
		,Null As OTROSMATERIALES
		,CASE A.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Muestra Recolectada' WHEN '3' THEN 'Resultado Entregado' WHEN '4' THEN 'Examen Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Ambulatorio' END As 'EstadoName'
		,A.ESTSERIPS As 'Estado'
		,'' AS AlertaJustificacion
		,JC.DESAYUDIA AS Ayuda
		,JC.DESJUSTEC AS Justificacion
		,JC.DESINCAYU AS Incidencia
		,A.CANSERIPS
		,A.OBSSERIPS
		,RTRIM(N.DESESPECI) AS Especialidad
		,Null AS AnularServicio
		,A.CODPROSAL
		,JC.CODMINSALUD AS CodigoMinisterio
		,Cast(0 AS Int) AS LATERALIDAD
		,'No Aplica' AS LATERALIDADDESP
		,A.IDDESCRIPCIONRELACIONADA
		,CD.NAME AS 'DescripcionRelacionada'
		,JC.PRESCRIPCIONJSON AS PrescripcionJson
		,JC.CodCUPSMipres AS CodigoCupsMipres
		,CAST(0 as bit) AS SOLICITASALA
		,CAST(0 as bit) AS EXREASITI
		,A.NOMARCPAT AS Archivo
		,A.SERREAINT
		,A.UFUCODIGO
		,A.CODCENATE
		,CAST(0 AS BIT) AS Resultado
		,'HCORDPATO' AS 'Tabla'
		,'' as PROHallazgos
		,'' as PROComplicaciones
		,'' as PROFecha
	FROM dbo.HCORDPATO A
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
	INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
	INNER JOIN DBO.HCHISPACA AS J ON A.NUMEFOLIO = J.NUMEFOLIO
		AND A.IPCODPACI = J.IPCODPACI
	LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
	LEFT JOIN contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId
	LEFT JOIN dbo.HCJUSTECA JC ON A.IPCODPACI = JC.IPCODPACI AND A.NUMINGRES = JC.NUMINGRES AND A.NUMEFOLIO = JC.NUMEFOLIO 
	WHERE A.IPCODPACI = @PatientCode AND A.NUMINGRES = @AdmissionNumber

UNION ALL

-- Imagenes Dx

	SELECT 
		0 AS Nuevo
		,A.AUTO
		, 'Imagenes Diagnósticas' as 'Tipo de Servicio'
		
		,A.FECORDMED
		,A.FECHASUGE
		,RTRIM(E.DESSERIPS) AS 'Servicio'
		,A.CODSERIPS
		,A.CODSERIPS AS 'Codigo'
		,B.NOMMEDICO AS 'Medico'
		,A.PRISERIPS
		,CASE A.PRISERIPS
			WHEN '1'
				THEN 'Urgente'
			WHEN '2'
				THEN 'Rutinario'
			END AS 'Tipo'
		,A.ESTSERIPS
		,RTRIM(D.UFUDESCRI) + ' - ' + RTRIM(C.NOMCENATE) AS Unidad
		,A.MANEXTPRO
		,A.MANEXTPRO As 'Externo'
		,A.IDETIPHIS
		,A.NUMEFOLIO
		,A.NUMEFOLIO AS 'Folio'
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODDIAGNO
		,A.INDAUDFOR
		,E.TIPSERIPS
		,E.SERIPSDASH
		,A.ESTALEIMG
		,A.REALINOTIF
		,Null AS 'Modalidad'
		,Cast(0 AS bit) AS ServicioRealizaSitio
		,CAST(0 AS bit) AS EXMREASIT
		,Null AS ESTALELAB
		,Null AS ESTALEPAT
		,Cast(0 As bit) AS Terapia
		,Cast(0 As int) As SOLICITAMATOST
		,Null As OTROSMATERIALES
		,CASE A.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Estudio Realizado' WHEN '3' THEN 'Imagen Procesada' WHEN '4' THEN 'Estudio Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Ambulatorio' END As 'EstadoName'
		,A.ESTSERIPS As 'Estado'
		,'' AS AlertaJustificacion
		,Null AS Ayuda
		,Null AS Justificacion
		,Null AS Incidencia
		,A.CANSERIPS
		,A.OBSSERIPS
		,RTRIM(N.DESESPECI) AS Especialidad
		,Null AS AnularServicio
		,A.CODPROSAL
		,Null AS CodigoMinisterio
		,A.LATERALIDAD
		,CASE A.LATERALIDAD WHEN 0 THEN 'No Aplica' WHEN 1 THEN 'Izquierda' WHEN 2 THEN 'Derecha' WHEN 3 THEN 'Ambos' END AS LATERALIDADDESP
		,A.IDDESCRIPCIONRELACIONADA
		,CD.NAME AS 'DescripcionRelacionada'
		,Null AS PrescripcionJson
		,Null AS CodigoCupsMipres
		,CAST(0 as bit) AS SOLICITASALA
		,CAST(0 as bit) AS EXREASITI
		,A.NOMARCIMG AS Archivo
		,A.SERREAINT
		,A.UFUCODIGO
		,A.CODCENATE
		,CAST(0 AS BIT) AS Resultado
		,'HCORDIMAG' AS 'Tabla'
		,'' as PROHallazgos
		,'' as PROComplicaciones
		,'' as PROFecha
	FROM dbo.HCORDIMAG A
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
	INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
	INNER JOIN DBO.HCHISPACA AS J ON A.NUMEFOLIO = J.NUMEFOLIO
		AND A.IPCODPACI = J.IPCODPACI
	LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
	LEFT JOIN contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId
	WHERE A.IPCODPACI = @PatientCode AND A.NUMINGRES = @AdmissionNumber

UNION ALL 

-- Laboratorios

	SELECT 
		0 AS Nuevo
		,A.AUTO
		,'Laboratorios' as 'Tipo de Servicio'
		
		,A.FECORDMED
		,A.FECHASUGE
		,RTRIM(E.DESSERIPS) AS 'Servicio'
		,A.CODSERIPS
		,A.CODSERIPS AS 'Codigo'
		,B.NOMMEDICO AS 'Medico'
		,A.PRISERIPS
		,CASE A.PRISERIPS
			WHEN '1'
				THEN 'Urgente'
			WHEN '2'
				THEN 'Rutinario'
			END AS 'Tipo'
		,A.ESTSERIPS
		,RTRIM(D.UFUDESCRI) + ' - ' + RTRIM(C.NOMCENATE) AS Unidad
		,A.MANEXTPRO
		,A.MANEXTPRO As 'Externo'
		,A.IDETIPHIS
		,A.NUMEFOLIO
		,A.NUMEFOLIO AS 'Folio'
		,A.IPCODPACI
		,A.NUMINGRES
		,A.CODDIAGNO
		,A.INDAUDFOR
		,E.TIPSERIPS
		,E.SERIPSDASH
		,Null AS ESTALEIMG
		,CAST(0 AS bit) AS REALINOTIF
		,Null AS 'Modalidad'
		,Cast(0 AS bit) ServicioRealizaSitio
		,A.EXMREASIT
		,A.ESTALELAB
		,Null AS ESTALEPAT
		,Cast(0 As bit) AS Terapia
		,Cast(0 As int) As SOLICITAMATOST
		,Null As OTROSMATERIALES
		,CASE A.ESTSERIPS WHEN '1' THEN 'Solicitado' WHEN '2' THEN 'Muestra Recolectada' WHEN '3' THEN 'Resultado Procesada' WHEN '4' THEN 'Examen Interpretado' WHEN '5' THEN 'Remitido' WHEN '6' THEN 'Anulado' WHEN '7' THEN 'Ambulatorio' WHEN '8' THEN 'Muestra Recolectada Parcialmente' END As 'EstadoName'
		,A.ESTSERIPS AS 'Estado'
		,'' AS AlertaJustificacion
		,Null AS Ayuda
		,Null AS Justificacion
		,Null AS Incidencia
		,A.CANSERIPS
		,A.OBSSERIPS
		,RTRIM(N.DESESPECI) AS Especialidad
		,Null AS AnularServicio
		,A.CODPROSAL
		,Null AS CodigoMinisterio
		,Cast(0 AS Int) AS LATERALIDAD
		,'No Aplica' AS LATERALIDADDESP
		,A.IDDESCRIPCIONRELACIONADA
		,CD.NAME AS 'DescripcionRelacionada'
		,Null AS PrescripcionJson
		,Null AS CodigoCupsMipres
		,CAST(0 as bit) AS SOLICITASALA
		,CAST(0 as bit) AS EXREASITI
		,A.NOMARCLAB AS Archivo
		,A.SERREAINT
		,A.UFUCODIGO
		,A.CODCENATE
		,CAST(0 AS BIT) AS Resultado
		,'HCORDLABO' AS 'Tabla'
		,'' as PROHallazgos
		,'' as PROComplicaciones
		,'' as PROFecha
	FROM dbo.HCORDLABO A
	INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
	INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
	INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
	INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
	INNER JOIN DBO.HCHISPACA AS J ON A.NUMEFOLIO = J.NUMEFOLIO
		AND A.IPCODPACI = J.IPCODPACI
	LEFT JOIN dbo.INESPECIA N ON J.CODESPTRA = N.CODESPECI
	LEFT JOIN contract.CUPSEntityContractDescriptions CDD ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
	LEFT JOIN contract.ContractDescriptions CD ON CD.Id = CDD.ContractDescriptionId
	WHERE A.IPCODPACI = @PatientCode AND A.NUMINGRES = @AdmissionNumber

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y consolida todas las órdenes médicas de servicios de salud asociadas a un paciente y un ingreso específico, devolviendo en un único resultado los procedimientos no quirúrgicos, quirúrgicos, patologías y demás tipos de servicio ordenados durante la atención. Para cada orden combina información del profesional que la generó, el centro de atención, la unidad funcional, el servicio CUPS/IPS, el folio de historia clínica, la especialidad, la descripción contractual de facturación y los hallazgos o complicaciones documentados en la evolución clínica. Es el procedimiento principal que alimenta la vista de órdenes médicas en la historia clínica del paciente, permitiendo conocer el estado, prioridad, lateralidad, cantidad y observaciones de cada servicio solicitado durante un ingreso u hospitalización.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'GetOrderServicesPerPatient';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'PROCEDURE', @level1name = N'GetOrderServicesPerPatient';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único resultado todas las órdenes de servicios clínicos (procedimientos no quirúrgicos, quirúrgicos, patologías, imágenes diagnósticas y laboratorios) asociadas a un ingreso de un paciente, normalizando estados, prioridades y lateralidad.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir y estar relacionados con folios en HCHISPACA.; Cada orden debe tener un profesional de salud, centro de atención, unidad funcional y código CUPS válidos para que sea retornada (joins INNER).; Para procedimientos no quirúrgicos y quirúrgicos, los hallazgos/complicaciones se obtienen de HCINFPROM solo si coinciden paciente, servicio, ingreso y folio.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los registros retornados pertenecen al mismo paciente e ingreso indicados.; Cada fila se etiqueta con el tipo de servicio y la tabla origen (''HCORDPRON'',''HCORDPROQ'',''HCORDPATO'',''HCORDIMAG'',''HCORDLABO'').; Las patologías y laboratorios siempre tienen LATERALIDAD=0 (''No Aplica'').; Los datos de hallazgos, complicaciones y fecha de procedimiento (PROHallazgos/PROComplicaciones/PROFecha) solo se pueblan para procedimientos quirúrgicos y no quirúrgicos; en los demás se devuelven vacíos.; Solo las patologías exponen ayuda, justificación, incidencia, código de ministerio, prescripción JSON y CUPS Mipres provenientes de HCJUSTECA.; La especialidad mostrada corresponde a la especialidad tratante registrada en el folio clínico (HCHISPACA.CODESPTRA).', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Folio de historia clínica; Orden médica; Procedimientos no quirúrgicos; Procedimientos quirúrgicos; Patologías; Imágenes diagnósticas; Laboratorios; Profesional de salud; Centro de atención; Unidad funcional; CUPS; Especialidad médica; Prioridad de la orden (Urgente/Rutinario/Emergencia); Estado de la orden; Lateralidad; Hallazgos y complicaciones del procedimiento; Justificación / Ayuda diagnóstica; Código Ministerio de Salud; CUPS Mipres; Contrato/Descripción contractual', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un conjunto unificado (UNION ALL) con cinco bloques de servicios filtrados por IPCODPACI=@PatientCode y NUMINGRES=@AdmissionNumber, etiquetando el origen en la columna ''Tabla'' (HCORDPRON, HCORDPROQ, HCORDPATO, HCORDIMAG, HCORDLABO).', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de servicio = Procedimientos No Quirúrgicos / Patologías / Imágenes / Laboratorios y PRISERIPS in (''1'',''2'') → Mapea prioridad a ''Urgente'' o ''Rutinario''.; si Tipo de servicio = Procedimientos Quirúrgicos y PRISERIPS in (''1'',''2'',''3'',''4'') → Mapea prioridad a ''Emergencia'', ''Urgencia'', ''Normal'' o ''Definir Conducta''.; si ESTSERIPS en HCORDPRON → Mapea estado a Solicitado/Completado/Interpretado/Sin Interfaz/Anulado.; si ESTSERIPS en HCORDPROQ → Mapea estado a Solicitado/Sala Programada/Cancelado/Resultado Revisado/Anulado/Programado no realizado.; si ESTSERIPS en HCORDPATO → Mapea estado a Solicitado/Muestra Recolectada/Resultado Entregado/Examen Interpretado/Remitido/Anulado/Ambulatorio.; si ESTSERIPS en HCORDIMAG → Mapea estado a Solicitado/Estudio Realizado/Imagen Procesada/Estudio Interpretado/Remitido/Anulado/Ambulatorio.; si ESTSERIPS en HCORDLABO → Mapea estado a Solicitado/Muestra Recolectada/Resultado Procesada/Examen Interpretado/Remitido/Anulado/Ambulatorio/Muestra Recolectada Parcialmente.; si LATERALIDAD in (0,1,2,3) → Traduce a ''No Aplica'', ''Izquierda'', ''Derecha'' o ''Ambos'' (en patologías y laboratorios se fuerza a ''No Aplica'').', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDLABO; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions; dbo.HCINFPROM; dbo.HCJUSTECA', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'PROCEDURE', @level1name=N'GetOrderServicesPerPatient';
-- GO
