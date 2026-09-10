
CREATE PROCEDURE [dbo].[SPMOV_ListarMedicamentosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  A.FECINIDOS AS 'Fecha Inicio',
		dbo.ObtenerFechaFormateada(FECINIDOS) as FechaFormateada,
        CASE 
			WHEN A.PREESTADO IN ('1','6') THEN DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]()) 
			WHEN A.PREESTADO IN (2,3,4,7) AND A.FECFINDOS IS NULL THEN DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]()) 
			WHEN A.PREESTADO IN (2,3,4,7) THEN DATEDIFF(day,A.FECINIDOS,A.FECFINDOS) ELSE DATEDIFF(day,A.FECINIDOS,[Common].[GETDATE]()) 
		END AS DiasTranscurridos, 
		RTRIM(E.DESPRODUC) AS 'Medicamento', 
		RTRIM(F.NOMDIAGNO) AS 'Dx / Motivo', 
		RTRIM(CASE WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI ELSE
				CASE
					WHEN DOSISPRFN IS NULL THEN DESADMINI 
					WHEN DURACIDOS='Dosis Unica' THEN RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Unica ' + RTRIM(H.DESVIAADM) ELSE RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(FRECUENCI AS CHAR)) + CASE UNIFRECUE WHEN '1' THEN 'M ' WHEN '2' THEN 'H ' WHEN '3' THEN 'D ' END + RTRIM(H.DESVIAADM) 
				END
			END) AS Administracion,
		CAST(A.PREESTADO AS INT) AS Estado,
		RTRIM(A.CODPRODUC) as 'CODPRODUC',
		RTRIM(B.NOMMEDICO) AS 'Medico',
		RTRIM(A.NUMEFOLIO) AS Folio,
		'Medicamento' as 'Tipo'
	FROM HCPRESCRA A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO
		INNER JOIN IHLISTPRO E ON A.CODPRODUC=E.CODPRODUC 
		INNER JOIN INDIAGNOS AS F ON A.CODDIAGNO = F.CODDIAGNO 
		LEFT OUTER JOIN INUNIMEDI AS G ON A.CODUNIMFN = G.CODUNIMED 
		INNER JOIN HCVIAADMI AS H ON A.CODVIAADM = H.CODVIAADM 
		INNER JOIN IHFORMEDI AS I ON A.CODFORMED = I.CODFORMED 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	where A.IPCODPACI = @Paciente and A.NUMINGRES = @Ingreso

UNION

	SELECT  FECHAINIC AS 'Fecha Inicio',
	    dbo.ObtenerFechaFormateada(FECHAINIC) as FechaFormateada,
		DATEDIFF(day,FECHAINIC,[Common].[GETDATE]()) AS DiasTranscurridos,
		RTRIM(MEZLIQPAC) AS 'Medicamento', 
		RTRIM(B.NOMDIAGNO) AS 'Dx / Motivo',
		RTRIM(ADMMEZLIQ) AS Administracion,
		PREESTADO AS Estado,
		NULL AS 'CODPRODUC',
		RTRIM(C.NOMMEDICO) AS Medico,
		RTRIM(A.NUMEFOLIO) AS Folio,
		'Mezcla' as 'Tipo'
	FROM HCINFLIQA A 
		INNER JOIN INDIAGNOS B ON A.CODDIAGNO=B.CODDIAGNO 
		INNER JOIN INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO  
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	where A.IPCODPACI = @Paciente and A.NUMINGRES = @Ingreso

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos y mezclas líquidas prescritas a un paciente durante un ingreso específico. Combina dos fuentes: las prescripciones individuales de medicamentos (con nombre del fármaco, diagnóstico, vía y forma de administración, dosis, frecuencia, médico prescriptor y folio de historia clínica) y las mezclas o soluciones líquidas indicadas al mismo paciente. Para cada ítem calcula los días transcurridos desde el inicio de la dosificación y devuelve el estado actual de la prescripción. Se usa en la visualización del perfil farmacológico del paciente durante una atención u hospitalización, integrando datos de prescripciones, productos farmacéuticos, diagnósticos CIE-10, unidades funcionales, profesionales de la salud y la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos y mezclas de líquidos prescritos a un paciente durante un ingreso específico, con su estado, días transcurridos, dosificación e información del prescriptor.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir y tener prescripciones asociadas en HCPRESCRA o HCINFLIQA.; Las prescripciones deben tener relaciones válidas con producto, diagnóstico, profesional de la salud, vía de administración, forma médica y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan registros del paciente e ingreso indicados.; Los medicamentos provienen de HCPRESCRA y las mezclas de HCINFLIQA, identificados con la columna ''Tipo''.; Para mezclas, el campo CODPRODUC siempre se devuelve como NULL.; La fecha actual se obtiene siempre vía Common.GETDATE() (no GETDATE() nativo).; Por uso de UNION (no UNION ALL) se eliminan filas duplicadas exactas entre ambos orígenes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción de medicamentos; Mezcla de líquidos; Dosis; Frecuencia de administración; Vía de administración; Diagnóstico; Médico prescriptor; Estado de prescripción; Folio de historia clínica; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto unificado (UNION) de medicamentos (tipo ''Medicamento'') desde HCPRESCRA y mezclas (tipo ''Mezcla'') desde HCINFLIQA filtrados por paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PREESTADO IN (''1'',''6'') → Días transcurridos = diferencia entre FECINIDOS y fecha actual (prescripción activa/vigente).; si PREESTADO IN (2,3,4,7) AND FECFINDOS IS NULL → Días transcurridos = diferencia entre FECINIDOS y fecha actual (no se ha registrado fin).; si PREESTADO IN (2,3,4,7) AND FECFINDOS NOT NULL → Días transcurridos = diferencia entre FECINIDOS y FECFINDOS (prescripción cerrada).; si FORMAPRESCRIBE IS NOT NULL → La administración se muestra como la descripción de administración (DESADMINI). else Si DOSISPRFN es NULL muestra DESADMINI; si DURACIDOS=''Dosis Unica'' arma cadena con dosis, unidad, ''Dosis Unica'' y vía; en otro caso arma cadena con dosis, unidad, frecuencia y vía.; si UNIFRECUE = ''1'' / ''2'' / ''3'' → Sufijo de frecuencia: ''M'' (minutos), ''H'' (horas), ''D'' (días) respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.IHLISTPRO; dbo.INDIAGNOS; dbo.INUNIMEDI; dbo.HCVIAADMI; dbo.IHFORMEDI; dbo.HCHISPACA; dbo.INESPECIA; dbo.HCINFLIQA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicamentosPacientes';
-- GO
