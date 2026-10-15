
CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico] 
(
@Paciente Varchar(25),
@Ingresos  varchar(500)
)
AS
BEGIN

SET NOCOUNT ON;


SELECT distinct  CAST(A.PREESTADO AS INT) AS 'Estado',
(select top 1 FECAPLMED from HCHOJAMED where CODPRODUC = A.CODPRODUC AND NUMINGRES = A.NUMINGRES  AND FECAPLMED  IS NOT NUll order by FECAPLMED asc) as 'FechaInicial',
RTRIM(A.CODPRODUC) AS 'Codigo', concat(RTRIM(E.CODPRODUC),' - ',RTRIM(E.DESPRODUC)) AS 'Medicamentos', A.DESADMINI AS 'Administracion', 
CONVERT(varchar(10), A.CODVIAADM) AS 'Via', CONVERT(varchar(10), A.CODUNIMED) AS 'UnidadMedida',
CONVERT(varchar(10), A.CANPEDPRO) AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', 
'' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion', 
(SELECT TOP 1 CASE WHEN FOLIOINIC = NUMEFOLIO THEN '1 - Nuevo' WHEN TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA' FROM HCPRESCRD WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODPRODUC = A.CODPRODUC ORDER BY FECINIDOS DESC) AS 'LEYENDA',
--RTRIM(A.CODPROSAL) AS 'CodigoProfesional', RTRIM(B.NOMMEDICO) AS 'NombreProfesional', 
(select top 1 concat(rtrim(D.CODDIAGNO),' - ', rtrim(D.NOMDIAGNO)) from dbo.INDIAGNOH x inner join INDIAGNOS D on D.CODDIAGNO = x.CODDIAGNO where x.NUMINGRES =A.NUMINGRES and x.CODDIAPRI =1 order by x.FECDIAGNO desc) as Diagnostico,
 /*CASE
         WHEN A.preestado IN (1, 6) THEN
			 Datediff(day, A.fecinidos, common.Getdate())
         WHEN A.preestado IN ( 2, 3, 4, 7 ) AND A.fecfindos IS NULL THEN
			 Datediff(day, A.fecinidos, common.Getdate())
         WHEN A.preestado IN ( 2, 3, 4, 7 ) THEN
			 Datediff(day, A.fecinidos, A.fecfindos)
         ELSE
			 Datediff(day, A.fecinidos, common.Getdate())
       END AS DiasTranscurridos*/
dbo.fnDiasTratamiento(1,A.ID,NULL,A.CODPRODUC,A.NUMINGRES) AS DiasTranscurridos,
ISNULL(A.DOSISPROD,0) as Dosis,
Uni.ABRUNIMED as DescripcionUnidadMedida,
1 AS 'TipoMedicamentoGrafica'
FROM HCPRESCRD A 
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
INNER JOIN IHLISTPRO E WITH(NOLOCK) ON A.CODPRODUC=E.CODPRODUC  
LEFT JOIN HCVIAADMI Via WITH(NOLOCK) ON via.CODVIAADM = A.CODVIAADM 
LEFT JOIN INUNIMEDI Uni with(nolock) ON Uni.CODUNIMED = A.CODUNIMED
where A.IPCODPACI= @Paciente AND A.NUMINGRES IN (SELECT Value FROM dbo.splitstring(@Ingresos))   

UNION ALL

SELECT distinct   CAST(A.PREESTADO AS INT) AS 'Estado', 
(select top 1 FECAPLMED from HCHOJMEZC where IDHCINFLIQC = B.CODCONCEC AND NUMINGRES = A.NUMINGRES  AND FECAPLMED  IS NOT NUll order by FECAPLMED asc) as 'FechaInicial',
RTRIM(ISNULL(CON.CODPRODUC,E.CODPRODUC)) AS 'Codigo', RTRIM(A.MEZLIQPAC) AS 'Medicamentos', A.ADMMEZLIQ AS 'Administracion',
'' AS 'Via','' AS 'UnidadMedida', '1' AS 'Cantidad', A.INDAPLMED AS 'Indicaciones', CONVERT(varchar(10), A.DURFRECUENCIA) AS 'DuracionFrecuencia', CONVERT(varchar(10), A.UNIDADFRECUENCIA) AS 'UnidadFrecuencia', 
CONVERT(varchar(10), A.TIPODURACION) AS 'TipoDuracion', '' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion',
CASE WHEN E.FOLIOINIC = E.NUMEFOLIO THEN '1 - Nuevo' WHEN E.TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA', 
--RTRIM(A.CODPROSAL) AS 'CodigoProfesional', RTRIM(C.NOMMEDICO) AS 'NombreProfesional', 
(select top 1 concat(rtrim(D.CODDIAGNO),' - ', rtrim(D.NOMDIAGNO)) from dbo.INDIAGNOH x inner join INDIAGNOS D on D.CODDIAGNO = x.CODDIAGNO where x.NUMINGRES =A.NUMINGRES and x.CODDIAPRI =1 order by x.FECDIAGNO desc) as Diagnostico,
/*DATEDIFF(day,A.FECHAINIC,Common.GETDATE()) AS DiasTranscurridos */
dbo.fnDiasTratamiento(2,NULL,B.CODCONCEC,NULL,NULL) AS DiasTranscurridos,
CASE B.METAPLMED WHEN 1  THEN CON.CONMEDMEZ WHEN 2 THEN CON.CONMEDMEZ WHEN 3 THEN E.DOSISBOLM WHEN 4 then E.DOSISINFU WHEN 5 THEN E.DOSISBOLO END as DOSIS,
CASE B.METAPLMED 
WHEN 1  THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = CON.UNIMEDMED) 
WHEN 2 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = CON.UNIMEDMED) 
WHEN 3 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDBOL)  
WHEN 4 then (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDINF)  
WHEN 5 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDBOL) END as DescripcionUnidadMedida,
3 AS 'TipoMedicamentoGrafica'
from HCINFLIQA A
INNER JOIN HCINFLIQC B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC
INNER JOIN HCINFLIQD E WITH(NOLOCK) ON A.CODCONCEC = E.CODCONCEC
LEFT JOIN  HCINFCONC CON WITH(NOLOCK) ON CON.CODCONCEC = E.CODCONCEC
INNER JOIN INPROFSAL C WITH(NOLOCK) ON B.CODPROSAL = C.CODPROSAL 
where A.IPCODPACI = @Paciente AND A.NUMINGRES IN (SELECT Value FROM dbo.splitstring(@Ingresos)) 

END