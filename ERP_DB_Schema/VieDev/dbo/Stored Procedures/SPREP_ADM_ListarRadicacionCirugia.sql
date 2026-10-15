CREATE PROCEDURE [dbo].[SPREP_ADM_ListarRadicacionCirugia]
(
@NumeroRadicacion varchar(20)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


SELECT A.ID as ID,A.NUMRADICACION as 'NumeroRadicacion' ,A.FECHAREGISTRO  as 'FechaRadicado', A.IPCODPACI, S.CODSERIPS, 
B.IPCODPACI as Codigo,RTRIM(IPNOMCOMP) as Nombre,IPFECNACI AS 'Fecha Nacimiento','' as Edad ,IPDIRECCI AS Direccion,IPTELEFON AS Telefono,
CASE IPSEXOPAC WHEN 1 THEN 'Masculino' WHEN 2 THEN 'Femenino' END AS Genero,

dbo.TipoDocumentoNombreCompleto(B.IPTIPODOC) AS TipoIdentificacion,
dbo.TipoPaciente(B.IPTIPOPAC) AS TipoUsuario,
dbo.TipoAfiliado(B.IPTIPOAFI) AS 'TIPO AFILIADO',
dbo.RangoAfiliacion(B.NIVCODIGO) AS 'NIVEL AFILIACION',

--CASE when b.IPTIPODOC = 1 then 'Cédula de Ciudadanía' 
--	when IPTIPODOC = 2 then 'Cedula Extranjera' 
--	when IPTIPODOC = 3 then 'Tarjeta Identidad' 
--	when IPTIPODOC = 4 then 'Registro Civil' 
--	when IPTIPODOC = 5 then 'Pasaporte' 
--	when IPTIPODOC = 6 THEN 'Adulto sin Identificación (solo para el Régimen Subsidiado)' 
--	When IPTIPODOC = 7 then 'Menor sin Identificación (solo para el Régimen Subsidiado)'
--	when IPTIPODOC= 9 then 'Carnet Diplomático' END AS TipoIdentificacion,

--CASE when IPTIPOPAC = 0 then 'Contributivo' 
--	when IPTIPOPAC = 1 then 'Subsidiado'  
--	when IPTIPOPAC = 2 then 'Vinculado'
--	when IPTIPOPAC = 3 then 'Particular' 
--	when IPTIPOPAC = 5 then 'Desplazado Reg. Contributivo'  
--	when IPTIPOPAC = 6 then 'Desplazado Reg. Subsidiado' 
--	when IPTIPOPAC = 7 then 'Desplazado no Asegurado' END AS TipoUsuario,


	rtrim(c.CODCENATE) + ' - ' + c.NOMCENATE as 'CentroAtencion',
	rtrim(D.CODPROSAL) + ' - ' + D.NOMMEDICO as 'Profesional',
	rtrim(s.CODSERIPS) + ' - ' + s.DESSERIPS as 'Servicio',
	 ENT.Code + ' - ' + ENT.Name as 'Entidad',
	 G.Code + ' - ' + G.Name as 'GrupoAtencion',
CASE A.PRIORIDAD WHEN '1' THEN 'Emergencia' WHEN '2' THEN 'Urgencia' WHEN '3' THEN 'Normal'   END AS 'PRIORIDAD',
	F.CODESPECI +' - ' + F.DESESPECI as 'Especialidad',
	CASE A.ESTADO WHEN '1' THEN 'Radicado' WHEN '2' THEN 'Confirmado' WHEN '3' THEN 'Anulado'   END AS 'ESTADO'
FROM ADRADICACIONQX A with(nolock) INNER JOIN 
INPACIENT B with(nolock) ON B.IPCODPACI = A.IPCODPACI INNER JOIN 
INPROFSAL D with(nolock) ON D.CODPROSAL = A.CODPROSAL INNER JOIN 
INESPECIA F with(nolock) ON F.CODESPECI = A.CODESPECI INNER JOIN  
INCUPSIPS S with(nolock) ON S.CODSERIPS = A.QXPRINCIPAL INNER JOIN
ADCENATEN c with(nolock) ON C.CODCENATE = A.CODCENATE INNER JOIN
[Contract].[HealthAdministrator] ENT with(nolock) on ENT.ID = A.GENCONENTITY INNER JOIN
[Contract].[CareGroup] G with(nolock) on G.ID = A.GENCAREGROUP 
WHERE A.NUMRADICACION = @NumeroRadicacion 
 
END