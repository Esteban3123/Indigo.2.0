CREATE PROCEDURE [dbo].[SPMOV_ListarProcedimientosPacientes]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	-- Procedimientos NoQx
	SELECT	RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
			CASE A.ESTSERIPS
				WHEN '1' Then 'Ordenado'
				WHEN '2' Then 'Completado'
				WHEN '3' Then 'Interpretado'
				WHEN '4' Then 'Sin Interfaz'
			END As 'Estado',
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
			'NoQx' as Tipo
	FROM HCORDPRON A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso

	UNION ALL

	-- Procedimientos Qx
	SELECT  RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
				CASE A.ESTSERIPS
				WHEN '1' Then 'Solicitado'
				WHEN '2' Then 'Muestra Recolectada'
				WHEN '3' Then 'Muestra Procesada o Resultado Enviado'
				WHEN '4' Then ' Resultado Revisado'
				END AS 'Estado',
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
				'Qx' as Tipo
	FROM HCORDPROQ A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso

	UNION ALL
	
	-- Procedimientos Patologias
	SELECT  RTRIM(E.DESSERIPS) AS 'Servicio',
			RTRIM(A.CODSERIPS) AS Codigo,
			A.FECORDMED AS 'Fecha Solicitud',
			dbo.ObtenerFechaFormateada(A.FECORDMED) as 'FechaFormateada',
			RTRIM(A.ESTSERIPS) AS IdEstado,
			CASE A.ESTSERIPS
				WHEN '1' THEN 'Solicitado'
				WHEN '2' THEN 'Muestra Recolectada'
				WHEN '3' THEN 'Resultado Entregado'
				WHEN '4' THEN 'Examen Interpretado'
				WHEN '5' THEN 'Remitido'
				WHEN '6' THEN 'Anulado'
				WHEN '7' THEN 'Extramural'
			END AS Estado,
			RTRIM(B.NOMMEDICO) AS 'Medico',
			CASE A.PRISERIPS 
				WHEN '1' THEN 'Urgente' 
				WHEN '2' THEN 'Rutina' 
			END AS 'Tipo Examen',
			RTRIM(D.UFUDESCRI) + ' - '  + RTRIM(C.NOMCENATE) AS Unidad,
			RTRIM(A.CANSERIPS) AS Cantidad, 
			RTRIM(A.OBSSERIPS) AS Observacion,
			RTRIM(MANEXTPRO) AS Externo,
			RTRIM(N.DESESPECI) AS Especialidad,
			'Patologia' as Tipo
	FROM HCORDPATO A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL=B.CODPROSAL 
		INNER JOIN ADcenaten C ON A.CODCENATE=C.codcenate 
		INNER JOIN INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
		INNER JOIN INCUPSIPS E ON A.CODSERIPS=E.CODSERIPS 
		INNER JOIN HCHISPACA AS J ON A.NUMEFOLIO=J.NUMEFOLIO AND A.IPCODPACI=J.IPCODPACI 
		LEFT OUTER JOIN INESPECIA N ON J.CODESPTRA=N.CODESPECI
	WHERE A.IPCODPACI= @Paciente AND A.NUMINGRES = @Ingreso
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los procedimientos médicos ordenados para un paciente en un ingreso específico, combinando tres tipos: procedimientos no quirúrgicos (NoQx), quirúrgicos (Qx) y de patología. Para cada procedimiento retorna el nombre del servicio CUPS/IPS, código, fecha de solicitud formateada, estado del trámite (ordenado, completado, interpretado, etc.), médico solicitante, prioridad (urgente o rutina), unidad funcional y sede donde se ordenó, cantidad, observaciones, si es externo, y la especialidad médica asociada. Consulta las órdenes médicas de historia clínica (HCORDPRON, HCORDPROQ, HCORDPATO), cruzando con el catálogo de servicios CUPS (INCUPSIPS), profesionales de salud (INPROFSAL), centros de atención (ADCENATEN), unidades funcionales (INUNIFUNC), folios de historia clínica (HCHISPACA) y especialidades (INESPECIA). Se usa para mostrar el historial completo de procedimientos solicitados al paciente durante su atención o ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único listado los procedimientos solicitados a un paciente durante un ingreso, diferenciando entre No quirúrgicos, Quirúrgicos y de Patología, con su estado, prioridad y datos clínicos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir y estar relacionados en HCHISPACA para resolver la especialidad tratante.; Los códigos de profesional, centro de atención, unidad funcional y CUPS referenciados deben existir en sus catálogos (INPROFSAL, ADcenaten, INUNIFUNC, INCUPSIPS) ya que se unen con INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los tres conjuntos de procedimientos siempre se filtran por el mismo paciente e ingreso recibidos.; La especialidad se toma de la hoja de historia (HCHISPACA.CODESPTRA) y es opcional (LEFT JOIN), por lo que un procedimiento puede listarse sin especialidad.; Solo se incluyen procedimientos cuyo profesional, centro de atención, unidad funcional y servicio CUPS existan en los catálogos maestros.; El significado del código de estado depende del tipo de procedimiento (mismo valor numérico tiene descripción distinta según NoQx/Qx/Patología).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Folio; Procedimiento No Quirúrgico; Procedimiento Quirúrgico; Procedimiento de Patología; Orden médica; Estado de la orden; Prioridad (Urgente/Rutina); CUPS; Unidad funcional; Centro de atención; Profesional de la salud; Especialidad tratante; Procedimiento extramural', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un único conjunto resultado con la unión (UNION ALL) de procedimientos NoQx, Qx y Patología filtrados por paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen del procedimiento es HCORDPRON → Se etiqueta Tipo=''NoQx'' y los estados se mapean a Ordenado/Completado/Interpretado/Sin Interfaz.; si Origen del procedimiento es HCORDPROQ → Se etiqueta Tipo=''Qx'' y los estados se mapean a Solicitado/Muestra Recolectada/Muestra Procesada o Resultado Enviado/Resultado Revisado.; si Origen del procedimiento es HCORDPATO → Se etiqueta Tipo=''Patologia'' y los estados se mapean a Solicitado/Muestra Recolectada/Resultado Entregado/Examen Interpretado/Remitido/Anulado/Extramural.; si PRISERIPS = ''1'' → Tipo Examen se reporta como ''Urgente''. else Si PRISERIPS = ''2'' se reporta como ''Rutina''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPRON; dbo.HCORDPROQ; dbo.HCORDPATO; dbo.INPROFSAL; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarProcedimientosPacientes';
-- GO
