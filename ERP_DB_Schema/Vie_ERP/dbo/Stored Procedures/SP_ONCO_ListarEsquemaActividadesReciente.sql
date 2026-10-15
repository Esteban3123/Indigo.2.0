CREATE PROCEDURE [dbo].[SP_ONCO_ListarEsquemaActividadesReciente]
(
@Identificacion Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

WITH Datos as (
		select distinct  C.ID
			  , Rtrim(D.Description) +'   -   N° Total de Ciclos: '+ Rtrim(C.CICLOS) +'   -   Fecha Orden: '+ Format(C.FECHAREGISTRO,'dd-MM-yyyy HH:mm') + CASE C.ESTADO WHEN 1 THEN '   -   SOLICITADO' WHEN  2 THEN '   -   INICIADO' WHEN 3 THEN '   -   CUMPLIDO' WHEN 4 THEN '   -   SUSPENDIDO/FINALIZACIÓN PREMATURA' + '   Fecha: ' + Format(C.FECHAFINALIZA,'dd-MM-yyyy HH:mm') WHEN 5 THEN '   -   ANULADO/NO PROGRAMADO    ' + ' Fecha: ' + Format(C.FECHAANULACION,'dd-MM-yyyy HH:mm') ELSE + '' END AS 'Nombre Esquema'
			  , Rtrim(N.CODPRODUC) As 'Codigo Medicamento'
			  , Rtrim(N.DESPRODUC) as 'Nombre Medicamento'
			  , IIF(HORAFRECUDIA IS NULL,Rtrim(convert(numeric(18,2),m.DOSISPROD)) +' '+ Rtrim(x.ABRUNIMED),concat(Rtrim( convert(numeric(18,2),m.DOSISPROD)), ' ', Rtrim(x.ABRUNIMED), ' a las ', format(HORAFRECUDIA,'HH:mm'), ' horas'))  as 'Dosis Producto' 
			  , M.DOSISPROD AS 'Dosis' 
			  , M.State As 'EstadoMedicamentoOnco'
			  , Rtrim(o.DESVIAADM )as 'Via' 
			  , Rtrim(P.NOMMEDICO) AS 'Profesional Ordeno'
			  , Rtrim(b.CICLO) AS 'Ciclo' 
			  , CONCAT(Format(Z.FECHAREGISTRO,'dd-MM-yyyy HH:mm') , ' -  N° Folio: ',Rtrim(Z.NUMEFOLIO)) AS FechaFolio
			  , IIF(M.MEDICAMENTOENCASA = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS ADMISTRADIACASA
			  , CASE M.MEDICAMENTOENCASA when 1 then 'Administración Domiciliaria' else Rtrim(HO.FECAPLMED) end AS 'Fecha Administracion'
		      , CASE M.MEDICAMENTOENCASA when 1 then 'Administración Domiciliaria' else case when HO.MEDESTADO = 2 then 'Aplicado' when HO.MEDESTADO = 3 then 'Suspendido' WHEN C.ESTADO = 4 THEN 'Suspendido' WHEN C.ESTADO = 5 THEN 'Esquema anulado' when HO.MEDESTADO = '1' AND HO.FECPROAPL > Common.GETDATE() tHEN 'Pendiente aplicar' when HO.MEDESTADO = '1' AND HO.FECPROAPL <= Common.GETDATE() tHEN 'Sin aplicar' WHEN HO.MEDESTADO is null THEN 'Pendiente aplicar' END end as 'Estado Medicamento'
			  , b.DIA as 'Intervalo'
			  , C.ESTADO, D.TypeScheme,HORAFRECUDIA, N.CODDCIMED as CodigoDCI, RTRIM(x.ABRUNIMED) AS 'DescripcionUnidadMedida'
			  , 2 AS 'TipoMedicamentoGrafica'
			  , hO.NUMINGRES AS 'IngresoAplicacion'
			  , c.NUMINGRES AS 'Ingreso'
			  , c.NUMEFOLIO AS 'Folio'
			  , CASE M.State WHEN 2 THEN 1 WHEN 3 THEN 1 WHEN 4 THEN 2 ELSE 0 END AS MODIFICADO
			  , ROW_NUMBER() OVER (PARTITION BY  C.ID, b.CICLO, N.CODPRODUC, b.DIA ORDER BY HO.FECINITRA DESC) AS rn
			  , CASE WHEN M.MEDICAMENTOENCASA = 1 then 1 when HO.MEDESTADO = 2 then 2 when HO.MEDESTADO = 3 OR C.ESTADO = 4 then 3 WHEN C.ESTADO = 5 THEN 4 when HO.MEDESTADO = '1' AND HO.FECPROAPL > Common.GETDATE() OR HO.MEDESTADO is null tHEN 5 when HO.MEDESTADO = '1' AND HO.FECPROAPL <= Common.GETDATE() tHEN 6 END as 'EstadoMed'			   
			  , IIF(HF.ORDESTADO IS NULL, 0, HF.ORDESTADO) AS EstadoOrden, b.CICLO AS NumCiclo
		from [EHR].[HCORDQUIMIO] C with(nolock)
			  INNER JOIN EHR.Schemes D with(nolock) ON D.Id  = c.SchemesId 
			  INNER JOIN EHR.HCORDCICLOS z with(nolock) on z.IDHCORDQUIMIO  = C.ID 
			  INNER JOIN EHR.HCORDCICLOSD B with(nolock) on B.IDHCORDCICLOS  = Z.Id  and ESTADODIA <> 3 
			  INNER JOIN EHR.HCORDMEDICAM M with(nolock) ON C.ID = M.IDHCORDQUIMIO and M.CICLO = B.CICLO  and M.DIA = B.DIA 
			  INNER JOIN EHR.HCORMEDICAMESQUEMA ME ON ME.IDHCORDQUIMIO = C.ID and ME.SchemesId = C.SchemesId and ME.CICLO = B.CICLO and ME.CODPRODUC = M.CODPRODUC		
			  INNER JOIN INUNIMEDI X with(nolock) ON x.CODUNIMED = m.CODUNIMED  
			  INNER JOIN IHLISTPRO N with(nolock) ON N.CODPRODUC = M.CODPRODUC 
			  INNER JOIN HCVIAADMI o with(nolock) ON o.CODVIAADM = M.CODVIAADM  
			  INNER JOIN INPROFSAL P with(nolock) ON P.CODPROSAL  = C.CODPROSAL
			  LEFT OUTER JOIN dbo.HCHOJAMED HO  with(nolock) ON HO.IDHCORDCICLOSD  = B.ID and ho.CODPRODUC = M.CODPRODUC 
			  LEft JOIN HCFARMEPC HF ON ME.IDHCFARMEPC = HF.CODCONCEC
		WHERE c.IPCODPACI = @Identificacion AND C.ESTADO IN (1,2,3,4,5)
  ), 
 EstadosCiclos AS (
	Select *, 	
	CONCAT(Ciclo, ' - N° Total de Dias: ', (Select COUNT(DISTINCt B.Intervalo) From Datos B Where A.ID = B.ID AND A.NumCiclo = B.NumCiclo AND RN = 1), ' -  Fecha Orden: ', FechaFolio) AS Ciclos,
	(Select COUNT(*) From Datos B Where A.ID = B.ID AND A.NumCiclo = B.NumCiclo AND RN = 1 AND B.EstadoMed = 2) + (Select COUNT(*) From Datos B Where A.ID = B.ID AND A.NumCiclo = B.NumCiclo AND RN = 1 AND B.EstadoMed = 1 AND EstadoOrden = 2) AS Aplicaciones,
	(Select COUNT(*) From Datos B Where A.ID = B.ID AND A.NumCiclo = B.NumCiclo AND RN = 1 AND B.EstadoMed NoT IN(3,4)) AS Registros
	FROM Datos A WHERE Rn = 1 
 ),
 Ciclos as (
	 SELECT 
	 ID, [Nombre Esquema], [Codigo Medicamento], [Nombre Medicamento], [Dosis Producto], Dosis, EstadoMedicamentoOnco, Via, [Profesional Ordeno], 
	 Concat(Ciclos, CASE WHEN Aplicaciones = 0 THEN ' - Ciclo Solicitado' WHEN Aplicaciones < Registros THEN ' - Ciclo Inicado' WHEN Aplicaciones = Registros THEN ' - Ciclo cumplido' END) AS Ciclo, 
	 ADMISTRADIACASA, [Fecha Administracion], [Estado Medicamento], Intervalo, ESTADO, TypeScheme, HORAFRECUDIA, CodigoDCI, DescripcionUnidadMedida, TipoMedicamentoGrafica, 
	 IngresoAplicacion, Ingreso, Folio, MODIFICADO
	 FROM EstadosCiclos 
 )
 Select * from Ciclos ORDER BY CICLO,Intervalo,[Codigo Medicamento],HORAFRECUDIA
END