-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesAtencionFarmaceutica] 
(
@CentroAtencion Char(10),
@UnidadFuncional Varchar(250)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @UnidadFuncionalAux as varchar(250) = RTRIM(@UnidadFuncional)

if @UnidadFuncionalAux IS NOT NULL AND LEN(@UnidadFuncionalAux) = 0 SET @UnidadFuncionalAux = NULL

SELECT 
--CASE
--		WHEN ((SELECT
--					COUNT(1)
--				FROM HCNUTPAREC AS HC
--				WHERE HC.IPCODPACI = C.IPCODPACI
--				AND HC.NUMINGRES = C.NUMINGRES
--				AND HC.STATUS IN (1))
--			) > 0 THEN 1
--		ELSE 0
--	END AS PerfilFarmacoterapeutico
	CASE WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS Egreso,
	cast(0 as bit) as PerfilFarmacoterapeutico
	,CASE
		WHEN ((SELECT
					COUNT(1)
				FROM HCPRESCRA AS HC
				INNER JOIN IHLISTPRO IH
					ON HC.CODPRODUC = IH.CODPRODUC
				WHERE HC.IPCODPACI = C.IPCODPACI
				AND HC.NUMINGRES = C.NUMINGRES
				AND IH.MEDTRAZA = 1
				AND HC.PREESTADO IN ('1', '6'))
			) > 0 THEN 1
		ELSE 0
	END AS MedicamentoTrazador
--   ,CASE
--		WHEN ((SELECT
--					COUNT(1)
--				FROM HCPRESCRA AS HC
--				INNER JOIN IHLISTPRO IH
--					ON HC.CODPRODUC = IH.CODPRODUC
--				INNER JOIN HCMEDRIES RE
--					ON HC.CODPRODUC = RE.CODPRODUC
--					AND HC.IPCODPACI = RE.IPCODPACI
--					AND HC.NUMINGRES = RE.NUMINGRES
--					AND HC.NUMEFOLIO = RE.NUMEFOLIO
--				WHERE HC.IPCODPACI = C.IPCODPACI
--				AND HC.NUMINGRES = C.NUMINGRES
--				AND IH.MEDTRAZA = 1
--				AND HC.PREESTADO IN ('1', '6'))
--			) > 0 THEN 1
--		ELSE 0
--	END AS ReaccionAdversa
   ,cast(0 as bit) as ReaccionAdversa
   ,CASE
		WHEN ((SELECT
					COUNT(1)
				FROM HCPRESCRA AS HC
				INNER JOIN IHLISTPRO IH	ON HC.CODPRODUC = IH.CODPRODUC 
				INNER JOIN HCINTEMED INM ON INM.CODPRODUA IN (IH.CODPRODUC, IH.CODDCIMED) OR INM.CODPRODUB IN (IH.CODPRODUC, IH.CODDCIMED)
				WHERE HC.IPCODPACI = C.IPCODPACI AND HC.NUMINGRES = C.NUMINGRES AND HC.PREESTADO IN ('1', '6'))
			) > 1 THEN 1
		ELSE 0
	END AS Interaccion
	--,cast(0 as bit) as MedicamentoTrazador
	--,cast(0 as bit) as Interaccion
   --,'1 - Pacientes en la Unidad' AS Egreso
   ,'Normal' AS Alerta
   ,A.CODICAMAS AS 'Codigo Cama'
   ,RTRIM(DESCCAMAS) AS Cama -- RTRIM(A.NUMCAMHOS) + ' - ' +
   ,RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) as Diagnostico
   ,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion
   ,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama'
   ,C.IPCODPACI AS Identificacion
   ,C.NUMINGRES AS Ingreso
   ,dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento
   ,RTRIM(DESTIPEST) AS 'Tipo Estancia'
   ,RTRIM(IPNOMCOMP) AS Paciente
   ,CAST(0 AS BIT) AS Resultado
   ,A.CAMTRACIR AS TrasladoCirugia
   ,A.CAMTRAMED AS TrasladoMedicamentos
   ,A.CODCONCEC AS Consecutivo
   ,CAST('' AS BIT) AS MuestraAlerta
   ,K.CODESPECI AS CodigoEspecialidad
   ,RTRIM(K.DESESPECI) AS DescripcionEspecialidad
   ,IFECHAING
   ,J.ESCADOWNT
   ,J.ESCARASS
   ,H.IPSEXOPAC AS Sexo
   ,Z.Color
   ,IPFECNACI AS 'Fecha Nacimiento'
   ,CAST('' AS CHAR(50)) AS Edad
   ,ZONAPARTADA
   ,L.RIESGOAGRE
   ,(SELECT
			COUNT(1)
		FROM dbo.ADPOBESPEPAC Z WITH (NOLOCK)
		INNER JOIN ADPOBESPE X WITH (NOLOCK)
			ON X.ID = Z.IDADPOBESPE
		WHERE IPCODPACI = h.IPCODPACI
		AND TIPOPOESPERIES = 1)
	AS POBESPECIAL
   ,VIVESOLO
   ,(SELECT
			COUNT(1)
		FROM ADACOMPAN WITH (NOLOCK)
		WHERE NUMINGRES = j.NUMINGRES)
	AS ACOMPANANTES
   ,CONVERT(BIT, 0) AS Riesgo
   ,Prof.CODPROSAL AS CodigoMedicoTratante
   ,RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante
   ,E.UFUCODIGO
   ,UFUDESCRI = RTRIM(E.UFUDESCRI)
   ,dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEDOWN
   ,dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) AS PUNTAJERASS
   ,dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON
   ,dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS
   ,dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE
   ,convert(bit,iif(ESCALA_CAIDA.RESULTADO is null,0,1)) AS 'ESCALACAIDA'
   ,convert(bit,iif(ESCALA_DOLOR.RESULTADO is null,0,1)) AS 'ESCALADOLOR'
   ,(Select CAST(CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END AS BIT) from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and NOT(HE.TIPOESCALA in (47,92,49,90,91,93))) AS 'ESCALASGENERAL'
   ,ESCALA_DOLOR.RESULTADO AS 'RESULTESDOLOR'
   ,ESCALA_DOLOR.TIPOESCALA AS 'TIPOESDOLOR'
   ,ESCALA_CAIDA.RESULTADO AS 'RESULTESCAIDA'
   ,ESCALA_CAIDA.TIPOESCALA AS 'TIPOESCAIDA'
   ,iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.CHCAMASHO A WITH (NOLOCK)
INNER JOIN dbo.INUNIFUNC E WITH (NOLOCK) ON A.UFUCODIGO = E.UFUCODIGO AND ESTADCAMA = 2
INNER JOIN dbo.CHREGESTA C WITH (NOLOCK) ON A.CODICAMAS = C.CODICAMAS AND C.REGESTADO = 1
INNER JOIN dbo.INPacient H WITH (NOLOCK) ON C.IPCODPACI = H.IPCODPACI
INNER JOIN dbo.ADACTIVID L WITH (NOLOCK) ON H.CODACTIVI = L.codactivi
INNER JOIN dbo.ADINGRESO J WITH (NOLOCK) ON C.NUMINGRES = J.NUMINGRES
INNER JOIN dbo.CHTIPESTA G WITH (NOLOCK) ON G.CODTIPEST = C.CODTIPEST
INNER JOIN dbo.INPROFSAL Prof WITH (NOLOCK) ON C.CODPROSAL = Prof.CODPROSAL
LEFT OUTER JOIN dbo.INESPECIA K WITH (NOLOCK) ON C.CODESPECI = K.CODESPECI
LEFT OUTER JOIN CHTIPOSAISLAMIENTOS Z WITH (NOLOCK) ON A.CODAISLAM = Z.Id
LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON C.NUMINGRES = I.NUMINGRES
OUTER APPLY
  (
  SELECT TOP 1 SS.CODDIAGNO, SS.NOMDIAGNO FROM INDIAGNOP DP inner join INDIAGNOS SS on DP.CODDIAGNO = SS.CODDIAGNO  WHERE IPCODPACI = H.IPCODPACI AND NUMINGRES = J.NUMINGRES AND CODDIAPRI = 1
  ) AS Q
OUTER APPLY
(
Select TOP 1 HE.RESULTADO, HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (49,90,91,93) ORDER BY HE.FECHAREGISTRO DESC
) as ESCALA_DOLOR
OUTER APPLY
(
Select TOP 1 HE.RESULTADO , HE.TIPOESCALA from HCESCALAS HE where HE.IPCODPACI = J.IPCODPACI AND HE.NUMINGRES = J.NUMINGRES and HE.TIPOESCALA in (47,92) ORDER BY HE.FECHAREGISTRO DESC
) as ESCALA_CAIDA
Outer apply 
(select TOP 1 INDICAPAC,IPCODPACI, NUMINGRES from HCHISPACA where IPCODPACI = J.IPCODPACI AND NUMINGRES = J.NUMINGRES order by FECHISPAC desc) as X 
WHERE A.CODCENATE = @CentroAtencion
AND (@UnidadFuncionalAux IS NULL OR (@UnidadFuncionalAux IS NOT NULL AND RTRIM(A.UFUCODIGO) in (select * from  [dbo].[SplitString](@UnidadFuncionalAux))))
AND NOT EXISTS(select 1 from dbo.HCREGEGRE R WITH (NOLOCK) where R.NUMINGRES = C.NUMINGRES )
ORDER BY Cama ASC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes actualmente hospitalizados (con cama ocupada) para el módulo de atención farmacéutica, filtrando por centro de atención y opcionalmente por unidad funcional (sala o servicio). Para cada paciente internado retorna datos de identificación, cama asignada, tipo de estancia, diagnóstico principal, médico tratante, especialidad, fechas de ingreso, estado de egreso (en unidad, pre-alta o con salida) y variables clínicas de riesgo como escalas de caídas (Down-Ton), dolor (VAS), sedación (RASS), Norton y Apache. Además señala alertas farmacológicas relevantes: presencia de medicamentos trazadores con prescripción activa, posibles interacciones medicamentosas registradas y si el paciente tiene reacciones adversas. Integra información de camas (CHCAMASHO), registro de estados del ingreso (CHREGESTA), datos del paciente (INPACIENT), admisión (ADINGRESO), unidad funcional (INUNIFUNC), profesional tratante (INPROFSAL), especialidad (INESPECIA), tipo de aislamiento (CHTIPOSAISLAMIENTOS) y actividad de admisión con indicador de riesgo agregado (ADACTIVID), siendo el insumo principal para que el farmacéutico hospitalario realice seguimiento y atención farmacéutica a los pacientes ingresados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes hospitalizados activos de un centro de atención (y opcionalmente de unidades funcionales específicas) con información clínica, escalas, alertas farmacéuticas y de seguimiento para apoyar la atención farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir y tener camas asociadas con ESTADCAMA = 2 (cama ocupada).; Debe existir un registro activo en CHREGESTA con REGESTADO = 1 para considerar al paciente.; El paciente no debe tener registro de egreso en HCREGEGRE para el ingreso considerado.; Si se proporciona la lista de unidades funcionales, debe poder dividirse mediante dbo.SplitString.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen camas con ESTADCAMA = 2 (ocupadas).; Solo se incluyen estancias con REGESTADO = 1 (estado activo).; Se excluyen pacientes que tengan cualquier registro de egreso en HCREGEGRE.; El diagnóstico mostrado es el principal (CODDIAPRI = 1) y solo uno (TOP 1).; Las escalas de dolor y caída devueltas son las más recientes por FECHAREGISTRO.; El indicador de pre-alta se basa en el último registro de HCHISPACA por FECHISPAC.; PerfilFarmacoterapeutico y ReaccionAdversa siempre se devuelven como 0 (lógica comentada).; El resultado se ordena por nombre de cama ascendente.; Una cadena de unidades funcionales con solo espacios se trata como nula.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención farmacéutica; Paciente hospitalizado; Cama hospitalaria; Unidad funcional; Egreso / Pre-alta hospitalaria; Medicamento trazador; Interacción medicamentosa; Reacción adversa; Diagnóstico principal; Escalas clínicas (Downton, RASS, Norton, VAS, Apache, dolor, caída); Aislamiento; Clase de habitación / cama; Población especial; Riesgo de agresión; Acompañantes; Médico tratante; Especialidad; Recomendación de interconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de filas con datos del paciente, cama, diagnóstico principal, escalas clínicas y banderas farmacéuticas para los pacientes activos en la(s) unidad(es) funcional(es) del centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si X.INDICAPAC = ''22'' (último registro en HCHISPACA) → Clasifica al paciente como ''2 - Pre-alta hospitalaria'' else Si no tiene registro en HCREGEGRE se clasifica como ''1 - Pacientes en la unidad''; en otro caso ''3 - Pacientes con salida''; si Existe al menos una prescripción (HCPRESCRA) con PREESTADO IN (''1'',''6'') sobre un producto con IH.MEDTRAZA = 1 → Marca MedicamentoTrazador = 1 else MedicamentoTrazador = 0; si Existen más de 1 prescripciones (PREESTADO 1 o 6) cuyo producto aparezca en HCINTEMED como CODPRODUA o CODPRODUB (incluyendo CODDCIMED) → Marca Interaccion = 1 else Interaccion = 0; si Existe registro en HCESCALAS con TIPOESCALA en (49,90,91,93) → ESCALADOLOR = 1 y se devuelve último RESULTADO/TIPOESCALA por FECHAREGISTRO desc else ESCALADOLOR = 0; si Existe registro en HCESCALAS con TIPOESCALA en (47,92) → ESCALACAIDA = 1 y se devuelve último RESULTADO/TIPOESCALA else ESCALACAIDA = 0; si Existe escala en HCESCALAS distinta a las de dolor/caída (no en 47,92,49,90,91,93) → ESCALASGENERAL = 1 else ESCALASGENERAL = 0; si Existe RecommendPatient con Status = 1 para el paciente/ingreso → Recomendacion = 1 else Recomendacion = 0; si Parámetro de unidades funcionales nulo o vacío (tras RTRIM) → No se filtra por unidad funcional else Se filtra A.UFUCODIGO contra el resultado de SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.INPacient; dbo.ADACTIVID; dbo.ADINGRESO; dbo.CHTIPESTA; dbo.INPROFSAL; dbo.INESPECIA; dbo.CHTIPOSAISLAMIENTOS; dbo.HCREGEGRE; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.HCESCALAS; dbo.HCHISPACA; dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.HCINTEMED; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesAtencionFarmaceutica';
-- GO
