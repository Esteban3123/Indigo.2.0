-- =============================================
-- Author:		<Duvan Mejia>
-- Create date: <13 Mayo de 2021>
-- Description:	<Listar Historico Entrega de Turno>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricoEntregaTurno]
(   
	@CentroAtencion varchar(10),
	@UnidadFuncional varchar(max),
	@Dashboard integer,
	@FechaInicio datetime,
	@FechaFin datetime,
	@IDEntrega integer,
	@TipoSolicitud integer --1:Consulta Turno; 2:Consulta Profesionales;  3:Consulta Pacientes
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-- Insert statements for procedure here	CAST(0 AS BIT)

	IF @TipoSolicitud = 1 --Consulta Turno
		BEGIN

			SELECT
			       cast(0 as bit) as Seleccionados,
					A.ID as 'ID',
					A.FECHAREGISTRO AS 'FechaRegistro',
				CASE
					WHEN A.DASHBOARD = 0 THEN 'Médico'
					WHEN A.DASHBOARD = 1 THEN 'Enfermería'
					WHEN A.DASHBOARD = 2 THEN 'Academico'
					WHEN A.DASHBOARD = 2 THEN 'Terapia'
				END AS 'Dashboard',
					RTRIM(B.CODCENATE) AS 'CodigoCentroAtencion' ,
					RTRIM(B.NOMCENATE) AS 'CentroAtencion' ,
					RTRIM(C.UFUCODIGO) AS 'CodigoUnidad Funcional',
					RTRIM(C.UFUDESCRI) AS 'UnidadFuncional' ,
				CASE
					WHEN A.JORNADA = 0 THEN 'Mañana'
					WHEN A.JORNADA = 1 THEN 'Tarde'
					WHEN A.JORNADA = 2 THEN 'Noche'
				END AS 'Jornada'
				FROM
				dbo.HCENTREGATURNOC AS A WITH (NOLOCK)
				INNER JOIN ADCENATEN AS B WITH (NOLOCK)
				ON A.CODCENATE = B.CODCENATE
				INNER JOIN INUNIFUNC AS C WITH (NOLOCK)
				ON A.UFUCODIGO = C.UFUCODIGO
				WHERE
				A.CODCENATE = @CentroAtencion
				AND C.UFUCODIGO IN (SELECT Value FROM dbo.SplitString(@UnidadFuncional))
				AND A.FECHAREGISTRO  BETWEEN  @FechaInicio AND @FechaFin
				AND A.DASHBOARD = @Dashboard
	END 

	ELSE IF @TipoSolicitud = 2 --Consulta Profesionales
			BEGIN

				SELECT
						A.ID,
					CASE
						WHEN B.ROLPROFESIONAL = 0 THEN 'Entrega Turno'
						WHEN B.ROLPROFESIONAL = 1 THEN 'Recibe Turno'
					END AS 'Rol',
						C.CODPROSAL AS 'Codigo',
						C.NOMMEDICO AS 'Nombre'
						FROM
						dbo.HCENTREGATURNOC AS A WITH (NOLOCK)
						INNER JOIN HCENTREGATURNOPROFSAL AS B WITH (NOLOCK)
						ON A.ID = B.IDHCENTREGATURNOC
						INNER JOIN INPROFSAL AS C WITH (NOLOCK)
						ON B.CODPROSAL = C.CODPROSAL
						WHERE
						A.ID = @IDEntrega			
	END

	ELSE IF @TipoSolicitud = 3 --Consulta Pacientes
			BEGIN

				SELECT
						A.ID,
						C.IPCODPACI AS 'Identificacion',
						C.IPNOMCOMP AS 'Nombre'
						FROM
						dbo.HCENTREGATURNOC AS A WITH (NOLOCK)
						INNER JOIN HCENTREGATURNOCPACIEN AS B WITH (NOLOCK)
						ON A.ID = B.IDHCENTREGATURNOC
						INNER JOIN INPACIENT AS C WITH (NOLOCK)
						ON B.IPCODPACI = C.IPCODPACI
						WHERE
						A.ID = @IDEntrega			
	END
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el historial de entregas de turno registradas en historia clínica, permitiendo tres tipos de búsqueda según el parámetro de tipo de solicitud: (1) lista los turnos realizados filtrando por centro de atención, unidades funcionales (múltiples), rango de fechas y dashboard (Médico, Enfermería, Académico o Terapia), mostrando la jornada (Mañana, Tarde o Noche); (2) consulta los profesionales de la salud que participaron en una entrega de turno específica, indicando si cada uno entregó o recibió el turno; (3) consulta los pacientes incluidos en una entrega de turno específica, mostrando su identificación y nombre. Integra las tablas de entregas de turno nocturno (HCENTREGATURNOC), centros de atención (ADCENATEN), unidades funcionales (INUNIFUNC), profesionales de la salud (INPROFSAL e HCENTREGATURNOPROFSAL) y pacientes, y utiliza la función SplitString para admitir múltiples unidades funcionales como parámetro. Es el procedimiento central para la consulta y auditoría del proceso de entrega de turno clínico entre profesionales de la salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el histórico de entregas de turno clínico, permitiendo obtener cabecera de turnos, profesionales que entregan/reciben o pacientes asociados según el tipo de solicitud.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El tipo de solicitud debe ser 1, 2 o 3; cualquier otro valor no produce resultados; Para tipo 1 se requieren centro de atención, unidades funcionales (lista delimitada), dashboard y rango de fechas válidos; Para tipo 2 y 3 se requiere el ID de la entrega de turno; La lista de unidades funcionales debe ser parseable por dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consultan entregas del centro de atención indicado y de las unidades funcionales contenidas en la lista; Todas las consultas usan WITH (NOLOCK), aceptando lecturas sucias; El campo Seleccionados siempre se devuelve en falso (bit 0) en la consulta de cabecera; El valor DASHBOARD=2 siempre se interpreta como ''Academico'' (la rama ''Terapia'' nunca se alcanza por duplicación); Las consultas de profesionales y pacientes se restringen a una única entrega de turno por su ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Entrega de turno clínico; Centro de atención; Unidad funcional; Jornada (Mañana/Tarde/Noche); Dashboard clínico (Médico/Enfermería/Académico/Terapia); Profesional de la salud; Rol de entrega/recibe turno; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCENTREGATURNOC: Cuando TipoSolicitud=1, retorna cabeceras de entrega filtradas por CODCENATE, UFUCODIGO en lista, FECHAREGISTRO entre rango y DASHBOARD igual al parámetro; [RETURN_RESULT] dbo.HCENTREGATURNOPROFSAL: Cuando TipoSolicitud=2, retorna profesionales (entrega/recibe) asociados a la entrega de turno con A.ID = @IDEntrega; [RETURN_RESULT] dbo.HCENTREGATURNOCPACIEN: Cuando TipoSolicitud=3, retorna pacientes asociados a la entrega de turno con A.ID = @IDEntrega', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TipoSolicitud = 1 → Lista cabeceras de entrega de turno con datos de centro de atención, unidad funcional y jornada decodificada else Evalúa siguiente rama; si TipoSolicitud = 2 → Lista profesionales asociados a la entrega indicando rol (Entrega Turno / Recibe Turno) else Evalúa siguiente rama; si TipoSolicitud = 3 → Lista pacientes asociados a la entrega de turno con identificación y nombre else No retorna nada; si DASHBOARD del registro = 0/1/2 → Decodifica como Médico / Enfermería / Academico (valor 2 también mapeado a Terapia, inalcanzable por duplicado); si JORNADA = 0/1/2 → Decodifica como Mañana / Tarde / Noche; si ROLPROFESIONAL = 0/1 → Decodifica como Entrega Turno / Recibe Turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCENTREGATURNOC; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.HCENTREGATURNOPROFSAL; dbo.INPROFSAL; dbo.HCENTREGATURNOCPACIEN; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoEntregaTurno';
-- GO
