CREATE PROCEDURE [dbo].[SPMOV_ListarProcedimientosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Procedimientos NoQx
	SELECT	RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
			CASE A.ESTSERIPS
				WHEN '1' Then 'Ordenado'
				WHEN '2' Then 'Completado'
				WHEN '3' Then 'Interpretado'
				WHEN '4' Then 'Sin Interfaz'
			END As 'Estado',
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
			'NoQx' as Tipo
	FROM HCORDPRON A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso

	UNION ALL

	-- Procedimientos Qx
	SELECT  RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
				CASE A.ESTSERIPS
				WHEN '1' Then 'Solicitado'
				WHEN '2' Then 'Muestra Recolectada'
				WHEN '3' Then 'Muestra Procesada o Resultado Enviado'
				WHEN '4' Then ' Resultado Revisado'
				END AS 'Estado',
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
				'Qx' as Tipo
	FROM HCORDPROQ A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso

	UNION ALL
	
	-- Procedimientos Patologias
	SELECT  RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
			CASE A.ESTSERIPS
				WHEN '1' THEN 'Solicitado'
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '3' THEN 'Resultado Entregado'
				WHEN '4' THEN 'Examen Interpretado'
				WHEN '5' THEN 'Remitido'
				WHEN '6' THEN 'Anulado'
				WHEN '7' THEN 'Extramural'
			END AS Estado,
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
			'Patologia' as Tipo
	FROM HCORDPATO A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso
end