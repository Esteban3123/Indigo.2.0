

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesTratamientoNuevo]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;


Select TOP 1  A.ID,C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (B.IPFECNACI,getdate()) As 'Edad',
		 A.ESTADO as 'Estado Registro', CASE A.ESTADO  WHEN  1 then 'Pacientes con diagnósticos nuevos' WHEN 2 then 'Pacientes con datos modificados' WHEN 3 then 'Pacientes con diagnósticos visados' end as 'Nombre Estado',
		 Rtrim(D.NOMCENATE) AS 'Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion'
FROM  HCGRUPOCANCERPACIC  A 
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
		INNER JOIN ADINGRESO C on A.NUMINGRES = C.NUMINGRES
		INNER JOIN INENTIDAD ENT on ENT.CODENTIDA = C.CODENTIDA 
		INNER JOIN ADCENATEN D on A.CODCENATE = D.CODCENATE 
	where A.ESTADO IN (1,2) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   

end