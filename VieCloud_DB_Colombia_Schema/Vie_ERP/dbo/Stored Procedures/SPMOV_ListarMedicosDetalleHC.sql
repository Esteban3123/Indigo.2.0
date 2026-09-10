
CREATE Procedure [dbo].[SPMOV_ListarMedicosDetalleHC]
(
@Paciente Varchar(25),
@Ingreso Char(10)
)
AS
	SELECT  DISTINCT
		RTRIM(A.CODPROSAL) as CodigoMedico,
		RTRIM(C.DESESPECI) as Especialidad,
		RTRIM(B.NOMMEDICO) as NombreCompleto,
		A.FECHISPAC as Fecha,
		dbo.ObtenerFechaFormateada(A.FECHISPAC) as FechaFormateada,
		RTRIM(A.DATOBJETI) as Analisis,
		RTRIM(A.NUMEFOLIO) as Folio,
		CASE 
			WHEN A.IDMODELOHC IS NULL THEN 
				(CASE A.TIPHISPAC
					WHEN 'I' THEN 'Historia Clinica Ingreso'
					WHEN 'N' THEN 'Nota Evolución'
					WHEN 'E' THEN 'Evolución'
					WHEN 'O' THEN 'Otros modelos de apoyo'
					WHEN 'PT' THEN 'Partograma'
					WHEN 'NF' THEN 'Nota Farmaceutica'
					WHEN 'V' THEN 'Valoración de Seguimiento'
					WHEN 'F' THEN 'Consulta Preanestesia'	
					WHEN 'T' THEN 'Historia clinica de Control'
					WHEN 'S' THEN 'Servicio de apoyo'
					WHEN 'P' THEN 'Atención partos'
					WHEN 'B' THEN 'Recien Nacido'
					WHEN 'JM' THEN 'Junta Médica - Nota Evolución'
					ELSE 'Otro'
				END)
			WHEN A.IDMODELOHC IS NOT NULL THEN M.DESCRIPCION
		End as Tipo
	FROM 
		HCHISPACA AS A with(nolock)
		Inner Join INPROFSAL B with(nolock) on B.CODPROSAL = A.CODPROSAL
		Inner Join INESPECIA C with(nolock) on C.CODESPECI = A.CODESPTRA
		LEFT JOIN PRMODELOHC M with(nolock) ON M.ID = A.IDMODELOHC
	where 
		A.IPCODPACI	= @Paciente AND A.NUMINGRES = @Ingreso
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de los médicos y sus notas clínicas registradas en la historia clínica de un paciente para un ingreso específico. Combina los folios de historia clínica (HCHISPACA) con los datos del profesional de la salud (nombre y especialidad), el tipo de nota o modelo de HC utilizado, y el análisis u observaciones registradas. Se usa para visualizar el historial de atenciones médicas documentadas durante una hospitalización o consulta, mostrando qué médico atendió, en qué especialidad, qué tipo de nota generó (ingreso, evolución, nota farmacéutica, junta médica, etc.) y en qué fecha, filtrando por cédula del paciente y número de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista de forma única los registros de historia clínica del paciente durante un ingreso, incluyendo médico tratante, especialidad, fecha, análisis y tipo de documento clínico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en HCHISPACA; El médico debe existir en INPROFSAL (INNER JOIN obligatorio); La especialidad de tratamiento debe existir en INESPECIA (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen registros del paciente e ingreso indicados; La descripción del tipo de historia clínica priorizar el modelo (PRMODELOHC) cuando existe; en caso contrario se traduce el código TIPHISPAC; Los registros duplicados se eliminan por DISTINCT; Las consultas se hacen con NOLOCK (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Historia clínica; Nota de evolución; Partograma; Nota farmacéutica; Valoración de seguimiento; Consulta preanestésica; Atención de partos; Recién nacido; Junta médica; Servicio de apoyo; Médico tratante; Especialidad médica; Modelo de historia clínica; Folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHISPACA: Devuelve registros DISTINCT filtrando por IPCODPACI y NUMINGRES, junto con datos del médico, especialidad y modelo de HC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.IDMODELOHC IS NULL → El tipo se deriva del código TIPHISPAC mapeando a etiquetas fijas (Historia Clínica Ingreso, Nota Evolución, Evolución, Partograma, Nota Farmacéutica, Valoración de Seguimiento, Consulta Preanestesia, Historia Clínica de Control, Servicio de Apoyo, Atención Partos, Recién Nacido, Junta Médica - Nota Evolución, Otros modelos de apoyo, Otro). else El tipo se toma de PRMODELOHC.DESCRIPCION asociado a IDMODELOHC.; si TIPHISPAC no coincide con ningún código conocido (I, N, E, O, PT, NF, V, F, T, S, P, B, JM) → Se etiqueta como ''Otro''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ObtenerFechaFormateada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.INPROFSAL; dbo.INESPECIA; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPMOV_ListarMedicosDetalleHC';
-- GO
