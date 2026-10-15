
CREATE PROCEDURE [dbo].[SPMOV_ListarImagenesDxPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  A.IPCODPACI
	,A.NUMINGRES,
				RTRIM(E.DESSERIPS) AS 'Servicio',
				RTRIM(A.CODSERIPS) AS Codigo,
				RTRIM(A.FECORDMED) AS 'Fecha Solicitud',
				dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
				A.ESTSERIPS AS IdEstado,
				CASE A.ESTSERIPS
				WHEN '1' THEN 'Solicitado'
				WHEN '2' THEN 'Estudio Realizado'
				WHEN '3' THEN 'Imagen Procesada'
				WHEN '4' THEN 'Estudio Interpretado'
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
				/*RTRIM(K.NOMSERPAC) as 'Url',
				RTRIM(HKE.url_imagen) as 'UrlImagen'*/
				RTRIM(K.NOMSERPACEXT) as 'Url',
				LTRIM(RTRIM(K.NOMSERPACEXT)) + '' +Ltrim(RTRIM(HKE.url_imagen)) as 'UrlImagen'
	FROM HCORDIMAG A 
			INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
			INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
			INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
			INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS
			INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
			LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
			Inner join HCPARPACS K on K.CODCENATE = A.CODCENATE
			left outer join HKLECTURA HKE ON HKE.auto_imagen_his = A.AUTO
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso
end