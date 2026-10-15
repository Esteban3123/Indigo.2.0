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