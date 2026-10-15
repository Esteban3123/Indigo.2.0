

CREATE PROCEDURE [dbo].[SPREP_AGE_CabeceraHojaGastoQX]
(
@IdHojaGastoQX varchar(20)
)
AS
BEGIN

	SET NOCOUNT ON;
	
SELECT A.ID as ID,A.CONSECUTIVO as 'ConsecutivoHoja' ,A.IDAGEPROGQX as 'IdprogramacionQXPrincipal', A.FECHAREGISTRO  as 'FechaHoja', A.IPCODPACI,A.NUMINGRES as 'Ingreso', S.CODSERIPS, 
RTRIM(IPNOMCOMP) as Nombre,IPFECNACI AS 'Fecha Nacimiento','' as Edad ,IPDIRECCI AS Direccion,IPTELEFON AS Telefono,
CASE IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Sexo, [dbo].[TypeGenderIdentity](B.IdGenderIdentity) AS IdentidadGenero,
CASE when IPTIPODOC = 1 then 'Cédula de Ciudadanía' 
	when IPTIPODOC = 2 then 'Cedula Extranjera' 
	when IPTIPODOC = 3 then 'Tarjeta Identidad' 
	when IPTIPODOC = 4 then 'Registro Civil' 
	when IPTIPODOC = 5 then 'Pasaporte' 
	when IPTIPODOC = 6 THEN 'Adulto sin Identificación (solo para el Régimen Subsidiado)' 
	When IPTIPODOC = 7 then 'Menor sin Identificación (solo para el Régimen Subsidiado)'
	when IPTIPODOC= 9 then 'Carnet Diplomático' END AS TipoIdentificacion,
CASE when IPTIPOPAC = 0 then 'Contributivo' 
	when IPTIPOPAC = 1 then 'Subsidiado'  
	when IPTIPOPAC = 2 then 'Vinculado'
	when IPTIPOPAC = 3 then 'Particular' 
	when IPTIPOPAC = 5 then 'Desplazado Reg. Contributivo'  
	when IPTIPOPAC = 6 then 'Desplazado Reg. Subsidiado' 
	when IPTIPOPAC = 7 then 'Desplazado no Asegurado' END AS TipoUsuario,
	rtrim(c.CODCENATE) + ' - ' + c.NOMCENATE as 'CentroAtencion',
	rtrim(U.UFUCODIGO) + ' - ' + U.UFUDESCRI as 'UnidadFuncional',
	rtrim(D.CODPROSAL) + ' - ' + D.NOMMEDICO as 'Profesional',
	rtrim(s.CODSERIPS) + ' - ' + s.DESSERIPS as 'Servicio',	
	rtrim(F.CODESPECI) +' - ' + F.DESESPECI as 'Especialidad',
	rtrim(sal.CODIGSALA) +' - ' + sal.DESCRIPSAL as 'Sala',
	CASE A.ESTADO WHEN 1 THEN 'Hoja Sin confirmar' WHEN 2 THEN 'En espera aceptacion devolutivo' WHEN 3 THEN 'Devolucion parcial aceptada' WHEN 4 THEN 'Hoja Confirmada'   END AS 'ESTADO'
FROM HCHOJAGASTOQX A with(nolock) INNER JOIN 
AGEPROGQX P with(nolock) ON P.CODAUTONU = A.IDAGEPROGQX INNER JOIN 
INPACIENT B with(nolock) ON B.IPCODPACI = P.IPCODPACI INNER JOIN 
INPROFSAL D with(nolock) ON D.CODPROSAL = P.CODPROSAL INNER JOIN 
INESPECIA F with(nolock) ON F.CODESPECI = P.CODESPECI INNER JOIN  
INCUPSIPS S with(nolock) ON S.CODSERIPS = P.CODSERIPS INNER JOIN
ADCENATEN c with(nolock) ON C.CODCENATE = P.CODCENATE INNER JOIN 
AGENSALAC Sal with(nolock) ON sal.CODCONCEC = P.AGENSALAC INNER JOIN
INUNIFUNC U with(nolock) ON U.UFUCODIGO = sal.UFUCODIGO  
WHERE A.ID = @IdHojaGastoQX
 
END