
CREATE PROCEDURE [dbo].[SP_AD_ListarPacientesExtramuralesAutoServicio]
(
@CentroAtencion as varchar(200),
@UnidadFuncional as varchar(200),
@FechaInicial as varchar(200),
@FechaFinal as varchar(200),
@Entidad as varchar(200),
@EmpresaVie as varchar(20)
)

AS
BEGIN
	
	SET NOCOUNT ON;

declare @Sql as nvarchar(max)

set @Sql = 'SELECT distinct  '''' as Alerta, RTRIM(A.IPCODPACI) AS Identificacion,Estado, RTRIM(IPNOMCOMP) as NombrePaciente, IPFECNACI AS ''Fecha Nacimiento'', CAST('''' AS CHAR(50)) AS Edad, A.NUMINGRES AS Ingreso, RTRIM(D.DESESPECI) AS Especialidad, RTRIM(E.Name) AS Entidad, rtrim(a.UFUCODIGO) as ''Unidad Funcional'' 
From dbo.ADAUTOSER A 
INNER JOIN dbo.ADINGRESO B ON B.NUMINGRES = A.NUMINGRES 
INNER JOIN dbo.INPACIENT C ON C.IPCODPACI = A.IPCODPACI 
LEFT OUTER JOIN ' + @EmpresaVie + '.Contract.healthadministrator E on E.Id = B.GENCONENTITY 
inner join dbo.INESPECIA D ON D.CODESPECI=B.CODESPTRA 
WHERE A.CODCENATE = ' + @CentroAtencion + ' AND A.UFUCODIGO IN ( '+ @UnidadFuncional +' ) AND SOLEXTRAM = 1 AND A.SERSUSCEP = 1 AND PROESTADO IN (1,2,3,5) AND FECREGIST BETWEEN ' + @FechaInicial + ' AND ' + @FechaFinal + ' AND E.Code IN (' + @Entidad + ') '

exec sp_executesql @Sql 

END