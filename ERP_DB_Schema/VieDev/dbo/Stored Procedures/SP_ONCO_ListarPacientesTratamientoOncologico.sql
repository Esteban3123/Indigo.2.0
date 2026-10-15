

CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesTratamientoOncologico]
(
@CentroAtencion varchar(500)
)
AS
BEGIN
	SET NOCOUNT ON;


select  cast(0 as bit) as 'Seleccion', A.ID, C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',C.NUMINGRES AS 'Ingreso',[dbo].[EDAD] (B.IPFECNACI,getdate()) As 'Edad',
		 A.ESTADO as 'Estado Registro', CASE A.ESTADO  WHEN  1 then 'Pacientes con diagnósticos nuevos' WHEN 2 then 'Pacientes con datos modificados' WHEN 3 then 'Pacientes con diagnósticos visados' end as 'Nombre Estado',
		 Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',A.NUMEFOLIO AS 'Folio'
 from (
	    select  IPCODPACI,max(ID) as IDUltimo  from HCGRUPOCANCERPACIC A with (nolock)
		where A.ESTADO IN (1,2) AND  A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 
		group by IPCODPACI    
	) as TMP 
	inner join HCGRUPOCANCERPACIC  A with (nolock) on A.ID = TMP.IDUltimo
	INNER JOIN INPACIENT B with (nolock) ON A.IPCODPACI = B.IPCODPACI 
	INNER JOIN ADINGRESO C with (nolock) on A.NUMINGRES = C.NUMINGRES
	INNER JOIN INENTIDAD ENT with (nolock) on ENT.CODENTIDA = C.CODENTIDA 
	INNER JOIN ADCENATEN D with (nolock) on A.CODCENATE = D.CODCENATE 
	where A.ESTADO IN (1,2) AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   

end