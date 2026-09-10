
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el perfil farmacoterapéutico histórico completo de un paciente para uno o varios ingresos, consolidando en un único listado todos los medicamentos que le fueron prescritos o administrados. Combina dos fuentes: las prescripciones individuales de medicamentos (HCPRESCRD) con su producto, vía de administración, unidad de medida, cantidad, dosis y días de tratamiento, y las mezclas o infusiones de líquidos (HCINFLIQA/HCINFLIQC/HCINFLIQD) con sus componentes e indicaciones de aplicación. Para cada medicamento devuelve el estado del tratamiento (activo, suspendido, etc.), la fecha en que fue aplicado por primera vez, la leyenda de si es nuevo, modificado o en tratamiento, el diagnóstico principal del ingreso (CIE-10), los días transcurridos en tratamiento calculados por la función fnDiasTratamiento, y la descripción de la unidad de medida; este procedimiento es el motor del módulo de perfil farmacoterapéutico en la historia clínica, utilizado por médicos y farmacéuticos para visualizar la evolución completa de la medicación del paciente a lo largo de sus ingresos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y devuelve el histórico de medicamentos prescritos y mezclas/infusiones de un paciente para uno o varios ingresos, mostrando estado, dosis, días de tratamiento y diagnóstico principal, para el perfil farmacoterapéutico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de paciente debe existir en HCPRESCRD y/o HCINFLIQA; La cadena de ingresos debe ser parseable por dbo.splitstring y contener NUMINGRES válidos; Los productos referenciados deben existir en IHLISTPRO y los profesionales en INPROFSAL (INNER JOIN obligatorio); Las funciones dbo.fnDiasTratamiento y dbo.splitstring deben estar disponibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha inicial reportada corresponde a la primera FECAPLMED no nula registrada para el medicamento/mezcla en el ingreso; El diagnóstico mostrado es siempre el más reciente marcado como principal (CODDIAPRI=1) según FECDIAGNO desc; Los medicamentos individuales se etiquetan con TipoMedicamentoGrafica=1 y las mezclas con TipoMedicamentoGrafica=3; La dosis nunca es NULL para prescripciones simples (se reemplaza por 0 con ISNULL); Solo se incluyen registros cuyo NUMINGRES esté en la lista de ingresos provista', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Perfil farmacoterapéutico; Prescripción de medicamentos; Mezclas e infusiones; Vía de administración; Unidad de medida; Dosis; Días de tratamiento; Diagnóstico principal (CIE); Ingreso hospitalario; Estado de prescripción (Nuevo/Modificado/En tratamiento); Folio de prescripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único conjunto resultado con UNION ALL de medicamentos individuales (HCPRESCRD, TipoMedicamentoGrafica=1) y mezclas/infusiones (HCINFLIQA, TipoMedicamentoGrafica=3) filtrados por paciente e ingresos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si En HCPRESCRD: FOLIOINIC = NUMEFOLIO → La leyenda del medicamento es ''1 - Nuevo'' else Si TRATMODIF=''1'' → ''2 - Modificado''; en otro caso → ''3 - En tratamiento''; si En mezclas, METAPLMED = 1 o 2 → La dosis se toma de CON.CONMEDMEZ y la unidad de medida de CON.UNIMEDMED else Si METAPLMED=3 o 5 → dosis E.DOSISBOLM/DOSISBOLO con unidad E.UNIMEDBOL; si METAPLMED=4 → dosis E.DOSISINFU con unidad E.UNIMEDINF; si Cálculo de días de tratamiento para prescripción simple → Se invoca dbo.fnDiasTratamiento(1, A.ID, NULL, A.CODPRODUC, A.NUMINGRES) else Para mezclas/infusiones se invoca dbo.fnDiasTratamiento(2, NULL, B.CODCONCEC, NULL, NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fnDiasTratamiento; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.INPROFSAL; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.HCHOJAMED; dbo.INDIAGNOH; dbo.INDIAGNOS; dbo.HCINFLIQA; dbo.HCINFLIQC; dbo.HCINFLIQD; dbo.HCINFCONC; dbo.HCHOJMEZC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeuticoHistorico';
-- GO
