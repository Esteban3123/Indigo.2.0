
CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentosPerfilFarmacoterapeutico] 
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------

SELECT distinct  
A.ID AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado', 
(select top 1 FECAPLMED from HCHOJAMED where CODPRODUC = A.CODPRODUC AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll order by FECAPLMED asc) as 'FechaInicial', --A.FECINIDOS AS 'FechaInicial',
RTRIM(A.CODPRODUC) AS 'Codigo',RTRIM(E.DESPRODUC) AS 'Medicamentos', A.DESADMINI AS 'Administracion', 
CONVERT(varchar(10), A.CODVIAADM) AS 'Via', CONVERT(varchar(10), A.CODUNIMED) AS 'UnidadMedida',
CONVERT(varchar(10), A.CANPEDPRO) AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', 
'' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion', 
(SELECT TOP 1 CASE WHEN FOLIOINIC = NUMEFOLIO THEN '1 - Nuevo' WHEN TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA' FROM HCPRESCRD WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODPRODUC = A.CODPRODUC ORDER BY FECINIDOS DESC) AS 'LEYENDA',
(SELECT TOP 1 CASE WHEN TRATMODIF = '1' THEN 1 ELSE 0 END FROM HCPRESCRD WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES AND CODPRODUC = A.CODPRODUC ORDER BY FECINIDOS DESC) AS 'MODIFY',
RTRIM(A.CODPROSAL) AS 'CodigoProfesional', RTRIM(B.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad', '1' AS 'Origen',
CASE WHEN J.CONCILIACIONMED IS NULL THEN 'NO' WHEN J.CONCILIACIONMED = 1 THEN 'SI' ELSE 'NO' END as ConciliacionMedicamentosa, concat(DIA.CODDIAGNO,' - ',DIA.NOMDIAGNO) as Diagnostico, ISNULL(A.DOSISPROD,0) as Dosis,
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
	   dbo.fnDiasTratamiento(1,A.ID,NULL,A.CODPRODUC,A.NUMINGRES) AS DiasTranscurridos, via.DESVIAADM as DescripcionVia,
	  ( select count(CONSECUTI) from HCHOJAMED where CODPRODUC = A.CODPRODUC AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll ) as CantidadDosisAplicada,
	  Uni.ABRUNIMED as DescripcionUnidadMedida, E.CODDCIMED as CodigoDCI, 1 AS 'TipoMedicamentoGrafica'
FROM HCPRESCRA A 
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL=B.CODPROSAL 
INNER JOIN IHLISTPRO E WITH(NOLOCK) ON A.CODPRODUC=E.CODPRODUC 
INNER JOIN HCHISPACA AS J WITH(NOLOCK) ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI AND A.NUMINGRES = J.NUMINGRES
INNER JOIN HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.ID and SourceTable = 'HCPRESCRA'
INNER JOIN INDIAGNOS DIA with(nolock) ON DIA.CODDIAGNO = A.CODDIAGNO
LEFT  JOIN INESPECIA N WITH(NOLOCK) ON J.CODESPTRA=N.CODESPECI
LEFT JOIN HCVIAADMI Via WITH(NOLOCK) ON via.CODVIAADM = A.CODVIAADM 
LEFT JOIN INUNIMEDI Uni with(nolock) ON Uni.CODUNIMED = A.CODUNIMED 
where A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso AND A.PREESTADO IN (1,6) AND A.IDESQUEMAONC IS NULL --1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez - 6: Medicamentos Solicitados sin Existencia Actual en el Kardex.

UNION ALL
SELECT distinct  A.CONSECUTI AS 'ID', CAST(A.PREESTADO AS INT) AS 'Estado',
(select top 1 FECAPLMED from HCHOJMEZC where IDHCINFLIQC = B.CODCONCEC AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll order by FECAPLMED asc) as 'FechaInicial', --A.FECHAINIC AS 'FechaInicial',
RTRIM(ISNULL(CON.CODPRODUC,E.CODPRODUC)) AS 'Codigo', RTRIM(A.MEZLIQPAC) AS 'Medicamentos', A.ADMMEZLIQ AS 'Administracion',
'' AS 'Via','' AS 'UnidadMedida', '1' AS 'Cantidad', A.INDAPLMED AS 'Indicaciones', CONVERT(varchar(10), A.DURFRECUENCIA) AS 'DuracionFrecuencia', CONVERT(varchar(10), A.UNIDADFRECUENCIA) AS 'UnidadFrecuencia', 
CONVERT(varchar(10), A.TIPODURACION) AS 'TipoDuracion', '' AS 'PesoPaciente', '' AS 'VolumenTotal', '' AS 'VolumenAdministrado', '' AS 'TiempoAdministrado', '' AS 'VelocidadInfusion',
CASE WHEN E.FOLIOINIC = E.NUMEFOLIO THEN '1 - Nuevo' WHEN E.TRATMODIF= '1' THEN '2 - Modificado' ELSE '3 - En tratamiento' END AS 'LEYENDA', 
CASE WHEN TRATMODIF = '1' THEN 1 ELSE 0 END AS 'MODIFY',
RTRIM(A.CODPROSAL) AS 'CodigoProfesional',
RTRIM(C.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(N.DESESPECI)) AS 'Especialidad','3' AS 'Origen',
CASE WHEN D.CONCILIACIONMED IS NULL THEN 'NO' WHEN D.CONCILIACIONMED = 1 THEN 'SI' ELSE 'NO' END as ConciliacionMedicamentosa, concat(DIA.CODDIAGNO,' - ',DIA.NOMDIAGNO) as Diagnostico, 
--ISNULL(A.DOSISUNICA, A.DOSISAPLICACION) as Dosis,
/*DATEDIFF(day,A.FECHAINIC,Common.GETDATE()) AS DiasTranscurridos */
CASE B.METAPLMED WHEN 1  THEN CON.CONMEDMEZ WHEN 2 THEN CON.CONMEDMEZ WHEN 3 THEN E.DOSISBOLM WHEN 4 then E.DOSISINFU WHEN 5 THEN E.DOSISBOLO END as DOSIS,
dbo.fnDiasTratamiento(2,NULL,B.CODCONCEC,NULL,NULL) AS DiasTranscurridos,
via.DESVIAADM as DescripcionVia,
( select count(CONSECUTI) from HCHOJMEZC where IDHCINFLIQC = B.CODCONCEC AND NUMINGRES = @Ingreso  AND FECAPLMED  IS NOT NUll ) as CantidadDosisAplicada,
CASE B.METAPLMED 
WHEN 1  THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = CON.UNIMEDMED) 
WHEN 2 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = CON.UNIMEDMED) 
WHEN 3 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDBOL)  
WHEN 4 then (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDINF)  
WHEN 5 THEN (select ABRUNIMED from INUNIMEDI where CODUNIMED = E.UNIMEDBOL) END as DescripcionUnidadMedida,  P.CODDCIMED as CodigoDCI,
3 AS 'TipoMedicamentoGrafica'
from HCINFLIQA A
INNER JOIN HCINFLIQC B WITH(NOLOCK) ON A.CODCONCEC = B.CODCONCEC
INNER JOIN HCINFLIQD E WITH(NOLOCK) ON A.CODCONCEC = E.CODCONCEC
LEFT JOIN  HCINFCONC CON WITH(NOLOCK) ON CON.CODCONCEC = E.CODCONCEC
INNER JOIN INPROFSAL C WITH(NOLOCK) ON B.CODPROSAL = C.CODPROSAL 
INNER JOIN HCHISPACA AS D WITH(NOLOCK) ON A.NUMEFOLIO = D.NUMEFOLIO AND A.IPCODPACI = D.IPCODPACI AND A.NUMINGRES = D.NUMINGRES
INNER JOIN HCFARMEPD FD with(nolock) ON FD.IdSourceTable = A.CONSECUTI and SourceTable = 'HCINFLIQA' 
INNER JOIN INDIAGNOS DIA with(nolock) ON DIA.CODDIAGNO = A.CODDIAGNO
LEFT  JOIN INESPECIA N WITH(NOLOCK) ON D.CODESPTRA = N.CODESPECI 
LEFT JOIN HCVIAADMI Via WITH(NOLOCK) ON via.CODVIAADM = E.CODVIABOM  
LEFT JOIN IHLISTPRO P WITH(NOLOCK) ON E.CODPRODUC=P.CODPRODUC 
where A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.PREESTADO IN (1,3,5) -- 1: Iniciado: Cuando el Medicamento se Solicita por Primera Vez - 3: Tratamiento Modificado: Cuando existe una modificacion en la Dosificacion, Duracion o Frecuencia - 5: Alguno de los Medicamentos de la mezcla estan sin Existencia en el Kardex.

UNION ALL

SELECT A.ID AS 'ID', CAST(A.STATUS AS INT) AS 'Estado', A.FECHAORDEN AS 'FechaInicial', RTRIM(A.ID) AS 'Codigo', RTRIM(B.NAME) AS 'Medicamentos', 'Administrar Continuamente ' + CONVERT(varchar(10), A.VOLUTOTAL) + ' ml ' + 'en infusión continua a ' + CONVERT(varchar(10), A.VELINFUSION) + ' ml/hora por ' + CONVERT(varchar(10),A.TEMPOADMIN) + ' Horas'  AS 'Administracion', CONVERT(varchar(10), A.VIADMIN) AS 'Via', '' AS 'UnidadMedida', 
'1' AS 'Cantidad', '' AS 'Indicaciones', '' AS 'DuracionFrecuencia', '' AS 'UnidadFrecuencia', '' AS 'TipoDuracion', CONVERT(varchar(10), A.PESOPACIE) AS 'PesoPaciente',
CONVERT(varchar(10), A.VOLUTOTAL) AS 'VolumenTotal', CONVERT(varchar(10), A.VOLUADM) AS 'VolumenAdministrado', CONVERT(varchar(10), A.TEMPOADMIN) AS 'TiempoAdministrado',
CONVERT(varchar(10), A.VELINFUSION) AS 'VelocidadInfusion', '1 - Nuevo' AS 'LEYENDA', 0 AS 'MODIFY', RTRIM(PRO.CODPROSAL) AS 'CodigoProfesional', 
RTRIM(PRO.NOMMEDICO) AS 'NombreProfesional', Ltrim(RTRIM(ESP.DESESPECI)) AS 'Especialidad', '4' AS 'Origen',
'NO' as ConciliacionMedicamentosa, concat(DIA.CODDIAGNO,' - ',DIA.NOMDIAGNO) as Diagnostico, 0 as Dosis,0 AS DiasTranscurridos,'' as DescripcionVia, 0 as CantidadDosisAplicada,'' as DescripcionUnidadMedida, '' as  CodigoDCI, 4 AS 'TipoMedicamentoGrafica'
FROM HCNUTPAREC AS A 
INNER JOIN HCPARNUTC B ON A.IDHCPARNUTC = B.ID 
INNER JOIN INPROFSAL PRO ON A.CODPROSAL = PRO.CODPROSAL
INNER JOIN HCHISPACA HIS ON HIS.ID = A.IDHCHISPACA 
INNER JOIN INDIAGNOS DIA with(nolock) ON DIA.CODDIAGNO = HIS.CODDIAGNO
LEFT  JOIN INESPECIA ESP WITH(NOLOCK) ON HIS.CODESPTRA = ESP.CODESPECI 
WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.STATUS IN (1)

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos activos del perfil farmacoterapéutico de un paciente para un ingreso específico, consolidando en un único resultado tanto las prescripciones individuales (tabletas, ampollas, etc.) como las mezclas de infusión líquida (quimioterapia, nutrición parenteral, etc.). Consulta las prescripciones registradas en historia clínica, el catálogo de productos farmacéuticos, los datos del profesional prescriptor, la especialidad médica, las vías de administración, unidades de medida, diagnósticos CIE-10 asociados y el folio de historia clínica, para mostrar al farmacéutico o al equipo clínico el estado actual del tratamiento del paciente (medicamentos iniciados o sin existencia en kardex), incluyendo leyenda de nuevo/modificado/en tratamiento, días transcurridos de tratamiento, dosis aplicadas, conciliación medicamentosa y código DCI de cada medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y lista el perfil farmacoterapéutico de un paciente en un ingreso, unificando medicamentos prescritos individuales, mezclas/infusiones y nutrición parenteral activos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso vigente identificable; Las prescripciones deben tener diagnóstico, profesional y producto referenciados en catálogos (INDIAGNOS, INPROFSAL, IHLISTPRO); Las prescripciones individuales deben tener registro asociado en HCFARMEPD con SourceTable=''HCPRESCRA''; Las mezclas/infusiones deben tener registro asociado en HCFARMEPD con SourceTable=''HCINFLIQA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye prescripciones asociadas a esquemas oncológicos (IDESQUEMAONC IS NULL); Sólo se devuelven medicamentos en estados activos según cada origen (1,6 para prescripciones; 1,3,5 para mezclas; 1 para nutrición parenteral); La fecha inicial corresponde a la primera aplicación registrada en hoja de medicación (FECAPLMED no nula); La cantidad de dosis aplicadas se obtiene contando registros con FECAPLMED no nula del ingreso; Nutrición parenteral siempre se reporta como ''NO'' conciliada y leyenda ''1 - Nuevo''; Los días de tratamiento de prescripciones y mezclas se delegan a la función fnDiasTratamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Perfil farmacoterapéutico; Prescripción de medicamentos; Mezcla / infusión intravenosa; Nutrición parenteral; Conciliación medicamentosa; Vía de administración; Unidad de medida / DCI; Diagnóstico; Especialidad médica; Hoja de medicación / aplicación de dosis; Esquema oncológico (excluido); Estados de prescripción (Iniciado, Modificado, Sin existencia en kardex); Días de tratamiento; Dosis (bolo, infusión, mezcla)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve UNION ALL de tres orígenes: medicamentos individuales (Origen=1), mezclas/infusiones (Origen=3) y nutrición parenteral (Origen=4); [RETURN_RESULT] HCPRESCRA: Incluye prescripciones cuyo PREESTADO IN (1,6) y IDESQUEMAONC IS NULL (excluye esquemas oncológicos); [RETURN_RESULT] HCINFLIQA: Incluye mezclas/infusiones cuyo PREESTADO IN (1,3,5); [RETURN_RESULT] HCNUTPAREC: Incluye nutrición parenteral cuyo STATUS = 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FOLIOINIC = NUMEFOLIO en HCPRESCRD → Marca leyenda ''1 - Nuevo'' else Si TRATMODIF=''1'' marca ''2 - Modificado''; en otro caso ''3 - En tratamiento''; si TRATMODIF = ''1'' → MODIFY = 1 (medicamento modificado) else MODIFY = 0; si CONCILIACIONMED = 1 en HCHISPACA → ConciliacionMedicamentosa = ''SI'' else ConciliacionMedicamentosa = ''NO'' (incluye NULL); si METAPLMED de la mezcla (HCINFLIQC) → Selecciona la dosis y unidad según método: 1/2 → CONMEDMEZ con UNIMEDMED; 3 → DOSISBOLM con UNIMEDBOL; 4 → DOSISINFU con UNIMEDINF; 5 → DOSISBOLO con UNIMEDBOL; si Origen del registro (HCPRESCRA / HCINFLIQA / HCNUTPAREC) → Determina el TipoMedicamentoGrafica (1, 3 o 4) y el cálculo de DiasTranscurridos vía fnDiasTratamiento o valor fijo 0 para nutrición', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.fnDiasTratamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.HCPRESCRD; dbo.HCHOJAMED; dbo.INPROFSAL; dbo.IHLISTPRO; dbo.HCHISPACA; dbo.HCFARMEPD; dbo.INDIAGNOS; dbo.INESPECIA; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.HCINFLIQA; dbo.HCINFLIQC; dbo.HCINFLIQD; dbo.HCINFCONC; dbo.HCHOJMEZC; dbo.HCNUTPAREC; dbo.HCPARNUTC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosPerfilFarmacoterapeutico';
-- GO
