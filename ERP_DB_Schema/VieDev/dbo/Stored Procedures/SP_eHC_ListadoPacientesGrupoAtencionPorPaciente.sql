
CREATE PROCEDURE [dbo].[SP_eHC_ListadoPacientesGrupoAtencionPorPaciente]
(
	@IdEntidades varchar(MAX),
	@Identificacion varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
	Select   CASE P.IPTIPODOC 
		 WHEN 1 THEN 'CC'
		 WHEN 2 THEN 'CE'
		 WHEN 3 THEN 'TI'
		 WHEN 4 THEN 'RC'
		 WHEN 5 THEN 'PA'
		 WHEN 6 THEN 'AS'
		 WHEN 7 THEN 'MS'
		 WHEN 8 THEN 'NU'
		 WHEN 9 THEN 'CN'
		 WHEN 10 THEN 'CD'
		 WHEN 11 THEN 'SC'
		 WHEN 12 THEN 'PE'
		 END AS TipoIdentificacion,  				
		 rtrim(A.IPCODPACI) as Identificacion,
		 rtrim(A.NUMINGRES) as Ingreso,
		 rtrim(P.IPNOMCOMP) as NombrePaciente,
		 E.Code + ' - ' + E.Name as Entidad,
		 [dbo].[Edad](convert(date,P.IPFECNACI),convert(date,getdate())) as Edad,
		 P.IPFECNACI as FechaNacimiento,
		 convert(int,0) as Generado
		FROM ADINGRESO A with(nolock)
		    INNER JOIN INPACIENT P with(nolock) ON A.IPCODPACI = P.IPCODPACI
			INNER JOIN Contract.HealthAdministrator E with(nolock) ON E.Id = A.GENCAREGROUP   
		WHERE A.IPCODPACI = @Identificacion AND A.GENCAREGROUP IN (SELECT Value FROM dbo.SplitString(@IdEntidades))  
END