


CREATE VIEW [dbo].[vwActividadesAgendamientoParamCUPS]
AS
(
SELECT AM.CODACTMED AS CodigoActividad, AM.DESACTMED AS DescripciónActividad, AM.ESTADOACT AS EstadoActividad, 
	AM.CODSERIPS AS CodigoCUPS,	I.DESSERIPS AS DescripcionCUPS, 
	CASE I.SERIPSDASH WHEN 1 THEN 'Laboratorio' WHEN 2 THEN 'Patología' WHEN 3 THEN 'Imágenes Diagnósticas' 
	WHEN 4 THEN 'Consulta Externa' WHEN 5 THEN 'Quimioterapia' WHEN 6 THEN 'Radioterapia' 
	WHEN 7 THEN 'Diálisis' WHEN 8 THEN 'Ninguno' WHEN 9 THEN 'Procedimiento No Qx' WHEN 10 THEN 'Procedimiento Qx' 
	WHEN 11 THEN 'Interconsultas' WHEN 7 THEN 'Otros Procedimientos' END AS Dashboard,
	CASE WHEN I.SIPSESTADO=1 THEN 'Activo' WHEN I.SIPSESTADO=2 THEN 'Inactivo' END AS EstadoCups
FROM dbo.AGACTIMED AS AM LEFT OUTER JOIN
	 dbo.INCUPSIPS AS I ON AM.CODSERIPS = I.CODSERIPS
	);