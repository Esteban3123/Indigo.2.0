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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el esquema de quimioterapia más reciente y sus actividades de medicación para un paciente oncológico identificado por su cédula/documento. Integra las órdenes de quimioterapia (HCORDQUIMIO) con los ciclos (HCORDCICLOS), los días de cada ciclo (HCORDCICLOSD), los medicamentos ordenados (HCORDMEDICAM) y el detalle del protocolo por esquema (HCORMEDICAMESQUEMA y Schemes) para construir una vista consolidada del tratamiento. Por cada medicamento retorna el nombre del esquema, número de ciclo, día de intervalo, dosis con unidad de medida, vía de administración, fecha y estado de aplicación (aplicado, pendiente, sin aplicar, suspendido, domiciliario), profesional que ordenó y estado global del ciclo (solicitado, iniciado, cumplido, suspendido, anulado). Se utiliza en el módulo de oncología para mostrar el seguimiento clínico y el estado de cumplimiento del plan de quimioterapia de un paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los esquemas de quimioterapia y sus medicamentos por ciclo/día para un paciente, mostrando estado del esquema, estado de aplicación de cada medicamento y resumen de avance del ciclo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir e identificarse por IPCODPACI; Solo se consideran órdenes de quimioterapia con ESTADO en (1,2,3,4,5): solicitado, iniciado, cumplido, suspendido o anulado; Los días del ciclo deben tener ESTADODIA distinto de 3 para ser considerados; Debe existir relación entre la orden, el esquema, ciclos, días, medicamentos y vía de administración', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retorna la administración más reciente por combinación esquema/ciclo/medicamento/día (ROW_NUMBER ordenado por FECINITRA DESC, RN=1); Días con ESTADODIA = 3 quedan excluidos del conteo y resultado; Las aplicaciones contabilizadas suman los medicamentos con EstadoMed=2 más los pendientes con orden de farmacia en estado 2; Los medicamentos con EstadoMed 3 (suspendido) o 4 (esquema anulado) no cuentan como registros pendientes del ciclo; El conteo de días totales del ciclo es por intervalos distintos con RN=1; TipoMedicamentoGrafica siempre se asigna como 2; Resultado ordenado por Ciclo, Intervalo, Código Medicamento y Hora de frecuencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Esquema de tratamiento oncológico; Ciclo de quimioterapia; Día de ciclo / intervalo; Medicamento oncológico; Dosis y unidad de medida; Vía de administración; Profesional que ordena; Folio e ingreso; Hoja de medicación; Administración domiciliaria; Estado de aplicación (aplicado, suspendido, pendiente, sin aplicar); Estado del esquema (solicitado, iniciado, cumplido, suspendido, anulado); Orden de farmacia; Código DCI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas por medicamento/ciclo/día (RN=1, la administración más reciente por FECINITRA) con descripción del esquema, dosis formateada, estado del medicamento y resumen del ciclo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.ESTADO = 1 → Etiqueta esquema como ''SOLICITADO''; si C.ESTADO = 2 → Etiqueta esquema como ''INICIADO''; si C.ESTADO = 3 → Etiqueta esquema como ''CUMPLIDO''; si C.ESTADO = 4 → Etiqueta esquema como ''SUSPENDIDO/FINALIZACIÓN PREMATURA'' con FECHAFINALIZA y marca el medicamento como ''Suspendido''; si C.ESTADO = 5 → Etiqueta esquema como ''ANULADO/NO PROGRAMADO'' con FECHAANULACION y marca el medicamento como ''Esquema anulado''; si M.MEDICAMENTOENCASA = 1 → Marca administración como domiciliaria (ADMISTRADIACASA=1) y fija fecha/estado como ''Administración Domiciliaria'' else Toma fecha y estado desde HCHOJAMED; si HO.MEDESTADO = 2 → Estado medicamento = ''Aplicado'' (EstadoMed=2); si HO.MEDESTADO = 3 → Estado medicamento = ''Suspendido'' (EstadoMed=3); si HO.MEDESTADO = 1 y FECPROAPL > GETDATE → Estado = ''Pendiente aplicar'' (EstadoMed=5); si HO.MEDESTADO = 1 y FECPROAPL <= GETDATE → Estado = ''Sin aplicar'' (EstadoMed=6); si HO.MEDESTADO IS NULL → Estado = ''Pendiente aplicar''; si HORAFRECUDIA IS NULL → Dosis se muestra solo con valor y unidad else Dosis se muestra con valor, unidad y hora ''a las HH:mm''; si Aplicaciones = 0 en el ciclo → Etiqueta el ciclo como ''Ciclo Solicitado''; si Aplicaciones < Registros del ciclo → Etiqueta el ciclo como ''Ciclo Iniciado''; si Aplicaciones = Registros del ciclo → Etiqueta el ciclo como ''Ciclo cumplido''; si M.State IN (2,3) → MODIFICADO = 1; si M.State = 4 → MODIFICADO = 2 else MODIFICADO = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.Schemes; EHR.HCORDCICLOS; EHR.HCORDCICLOSD; EHR.HCORDMEDICAM; EHR.HCORMEDICAMESQUEMA; dbo.INUNIMEDI; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INPROFSAL; dbo.HCHOJAMED; dbo.HCFARMEPC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarEsquemaActividadesReciente';
-- GO
