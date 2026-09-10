-- =============================================
-- Author:		<Johan Sebastian Carranza Ramos, Developer Junior>
-- Create date: <20 Agosto de 2019>
-- Description:	<Procedimiento almacenado que me lista el hisotrico de las camas para mostrarlo en GridView>
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarHistoricoCamasLimpieza]( 
	@Centro varchar(20),
	@Unidad varchar(1000), --Vienen varias unidades funcionales!!
	@Origen varchar(20),
	@FechaInicial As Datetime,
	@FechaFinal As Datetime
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @Origen = 'Inicial' --Cargue inicial. -------------------------------------------------------------------------------------------------------------

	SELECT 	B.CODICAMAS,
	RTRIM(B.DESCCAMAS) AS CAMA,
	--Case clase de la cama. 
	CASE B.CODCLACAM
    WHEN 1 THEN 'Observacion urgencias' 
	WHEN 2 THEN 'Recuperacion post-quirurgico'
    WHEN 3 THEN 'Hospitalaria'
	WHEN 4 THEN 'Cuna de observacion nota'
    ELSE 'Otros'
	--Case clase de habitacion.
	END AS CLASECAMA,
	CASE B.CODCLAHAB
	WHEN 1 THEN 'Sala de observacion'
	WHEN 2 THEN 'Sala de procedimientos'
	WHEN 3 THEN 'Sala de recuperacion'
	WHEN 4 THEN 'Habitacion de 1 cama'
	WHEN 5 THEN 'Habitacion de 2 camas'
	WHEN 6 THEN 'Habitacion de 3 camas'
	WHEN 7 THEN 'Habitacion de 4 camas'
	WHEN 8 THEN 'Suite'
	WHEN 9 THEN 'Habitacion especial'
	WHEN 10 THEN 'UCI'
	ELSE 'Otros'
	END AS CLASEHABITA,
	RTRIM(C.UFUDESCRI) AS UNIDADFUN,
	--Case codigo de aislamiento.
	CASE B.CODAISLAM
	WHEN 1 THEN 'Aerosol'
	WHEN 2 THEN 'Contacto'
	WHEN 3 THEN 'Estandar'
	WHEN 4 THEN 'Gota'
	WHEN 5 THEN 'Protector'
	ELSE 'Otro'
	END AS AISLAMIENTO,
	A.FECHAINI AS FECHAINICIAL, A.FECHAFIN AS FECHAFINAL,  
	--Case del estado de la cama.
	CASE B.ESTADCAMA WHEN 1 THEN 'Camas libres' WHEN 6 THEN 'Camas libres' 	ELSE 'Camas ocupadas'END AS ESTADOCAMA,
	CASE A.ESTADO WHEN 1 THEN 'Inicio Jornada' WHEN 2 THEN 'Inicio Limpieza' WHEN 3 THEN 'Finalizo Limpieza' END ESTADOLIMPIEZADES,
	CASE A.ESTADO WHEN 1 THEN 'Inicio Jornada' WHEN 2 THEN 'Inicio Limpieza' WHEN 3 THEN 'Finalizo Limpieza' END ESTADOLIMPIEZADES,
	CASE A.ESTADO WHEN 1 THEN 'Red' WHEN 2 THEN 'Yellow' WHEN 3 THEN 'Green' END ESTADOLIMPIEZACOLOR, CASE A.ESTADO WHEN 1 THEN 'Por limpiar' WHEN 2 THEN 'En limpieza' WHEN 3 THEN 'Limpia' END ESTADOLIMPIEZA,
	A.ULTIMAJORNADA AS ULTIMAJORNADA,
	A.ULTIMALIMPIEZA AS ULTIMALIMPIEZA,
	A.ID AS ID,
	CASE A.TIPOLIMPIEZA WHEN 1 THEN 'Rutinaria' WHEN 2 THEN 'Extra' WHEN 3 THEN 'Terminal' ELSE 'Otro' END AS TIPOLIMPIEZA

	FROM CHCAMASHO B with (nolock) INNER JOIN 
	INUNIFUNC C with (nolock) ON B.UFUCODIGO= C.UFUCODIGO LEFT JOIN

	(SELECT * FROM(SELECT ROW_NUMBER() OVER (PARTITION by CODICAMAS ORDER BY ULTIMAJORNADA DESC) id_particion, codicamas, ULTIMAJORNADA,ESTADO,ULTIMALIMPIEZA,FECHAINI,FECHAFIN ,ID,TIPOLIMPIEZA from CHCAMAASEO) ASEO
	where ASEO.id_particion = 1) A ON B.CODICAMAS = A.CODICAMAS

	WHERE B.CODCENATE = @Centro AND B.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad))

	ELSE IF @Origen = 'LimpiezaNormal' --Reporte limpieza normal-------------------------------------------------------------------------------------------------------------

	SELECT 	B.CODICAMAS,
	RTRIM(B.DESCCAMAS) AS CAMA,
	RTRIM(D.NOMCENATE) AS CENTROATENCION,
	RTRIM(C.UFUDESCRI) AS UNIDADFUN,
	--Case codigo de aislamiento.
	CASE B.CODAISLAM
	WHEN 1 THEN 'Aerosol'
	WHEN 2 THEN 'Contacto'
	WHEN 3 THEN 'Estandar'
	WHEN 4 THEN 'Gota'
	WHEN 5 THEN 'Protector'
	ELSE 'Otro'
	END AS AISLAMIENTO,
	A.FECHAINI AS FECHAINICIAL, A.FECHAFIN AS FECHAFINAL, 
	--Case del estado de la cama.
	CASE B.ESTADCAMA WHEN 1 THEN 'Camas libres' WHEN 6 THEN 'Camas libres' 	ELSE 'Camas ocupadas'END AS ESTADOCAMADES,
	CAST(B.ESTADCAMA AS VARCHAR(20)) AS ESTADOCAMA,
	A.ULTIMAJORNADA AS ULTIMAJORNADA,
	A.ULTIMALIMPIEZA AS ULTIMALIMPIEZA,
	A.USUARIOINI AS USUARIOINICIA,
	RTRIM(F.NOMUSUARI) AS USUARIOINICIANOM,
	A.USUARIOFIN AS USUARIOFNAL,
	RTRIM(G.NOMUSUARI) AS USUARIOFINALNOM,
	A.USUARIOREG AS USUARIOREGISTRA,
	RTRIM(E.NOMUSUARI) AS USUARIONOMBREREG,
	CASE A.TIPOLIMPIEZA WHEN 1 THEN 'Rutinaria' WHEN 2 THEN 'Extra' WHEN 3 THEN 'Terminal' ELSE 'Otro' END AS TIPOLIMPIEZA,
	A.ULTIMALIMPIEZA AS ULTIMALIMPIEZA

	FROM CHCAMASHO B with (nolock) INNER JOIN 
	ADCENATEN D with(nolock) ON B.CODCENATE = D.CODCENATE INNER JOIN
	INUNIFUNC C with (nolock) ON B.UFUCODIGO= C.UFUCODIGO LEFT JOIN
	CHCAMAASEO A with (nolock) ON A.CODICAMAS = B.CODICAMAS INNER JOIN
	SEGusuaru E with (nolock) ON E.CODUSUARI=A.USUARIOREG LEFT JOIN 
	SEGusuaru F with (nolock) ON F.CODUSUARI=A.USUARIOINI LEFT JOIN
	SEGusuaru G with (nolock) ON G.CODUSUARI=A.USUARIOFIN 

	WHERE B.CODCENATE = @Centro AND B.UFUCODIGO IN (SELECT Value FROM dbo.splitstring(@Unidad)) --AND A.LIMPIEZAEXTRA = 0
	AND (((A.FECHAINI IS NOT NULL AND (A.FECHAINI >= @FechaInicial AND A.FECHAINI <= @FechaFinal )) 
	OR (A.FECHAFIN IS NOT NULL AND (A.FECHAFIN >= @FechaInicial AND A.FECHAFIN <= @FechaFinal )) OR (A.FECHAINI IS NULL AND A.FECHAFIN IS NULL)))
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta el historial de limpieza de camas hospitalarias según el centro de atención, las unidades funcionales y un rango de fechas. Combina el maestro de camas (CHCAMASHO) con el catálogo de unidades funcionales (INUNIFUNC) y los registros de aseo (CHCAMAASEO) para mostrar, por cada cama, su clase, tipo de habitación, estado de ocupación, tipo de aislamiento y el estado del proceso de limpieza (por limpiar, en limpieza o limpia). Opera en dos modos según el parámetro @Origen: ''Inicial'' devuelve el último registro de limpieza por cama para la pantalla principal del módulo de gestión de camas; ''LimpiezaNormal'' genera un reporte histórico detallado que incluye los usuarios que iniciaron, finalizaron y registraron cada jornada de limpieza, útil para auditoría e informes de higiene hospitalaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'"Lista el histórico de camas y sus jornadas de limpieza filtrado por centro, unidades funcionales y rango de fechas, en dos modos: estado actual por cama (''Inicial'') o reporte detallado de limpiezas con usuarios responsables (''LimpiezaNormal'')."', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Origen debe ser ''Inicial'' o ''LimpiezaNormal''; otros valores no retornan datos.; @Unidad debe ser una cadena con códigos de unidad funcional separados, parseable por dbo.splitstring.; @Centro debe corresponder a un CODCENATE existente en CHCAMASHO/ADCENATEN.; Para ''LimpiezaNormal'' se requieren @FechaInicial y @FechaFinal válidos para filtrar por rango.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran camas pertenecientes al centro de atención indicado y a las unidades funcionales recibidas en lista delimitada.; En el modo ''Inicial'', cada cama aparece a lo sumo una vez, mostrando únicamente el registro de aseo más reciente según ULTIMAJORNADA.; Estado de cama 1 o 6 se interpreta como ''Camas libres''; cualquier otro valor como ''Camas ocupadas''.; Estado de limpieza 1=Por limpiar (Red), 2=En limpieza (Yellow), 3=Limpia (Green).; Tipo de limpieza 1=Rutinaria, 2=Extra, 3=Terminal; otros valores se reportan como ''Otro''.; Códigos de aislamiento 1..5 mapean a Aerosol/Contacto/Estándar/Gota/Protector; valores fuera de rango se reportan como ''Otro''.; Las consultas se ejecutan con NOLOCK, asumiendo lecturas sucias tolerables para reporting.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Clase de cama (observación urgencias, recuperación post-quirúrgico, hospitalaria, cuna observación); Clase de habitación (sala observación, procedimientos, recuperación, suite, UCI, etc.); Aislamiento (aerosol, contacto, estándar, gota, protector); Estado de cama (libres/ocupadas); Limpieza/aseo de camas (jornada, inicio, fin); Tipo de limpieza (rutinaria, extra, terminal); Estado de limpieza (Por limpiar/En limpieza/Limpia); Unidad funcional; Centro de atención; Usuarios que registran/inician/finalizan limpieza', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CHCAMASHO: Cuando @Origen=''Inicial'', retorna una fila por cama del centro/unidades con su último aseo (id_particion=1 sobre CHCAMAASEO ordenado por ULTIMAJORNADA DESC) y descripciones traducidas de clase, aislamiento, estado y limpieza.; [RETURN_RESULT] dbo.CHCAMAASEO: Cuando @Origen=''LimpiezaNormal'', retorna registros de CHCAMAASEO unidos con cama, centro, unidad y usuarios (registra/inicia/finaliza) cuyo FECHAINI o FECHAFIN cae en [@FechaInicial,@FechaFinal] o ambas fechas son NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Origen = ''Inicial'' → Devuelve por cada cama del centro/unidades su último registro de aseo (ROW_NUMBER PARTITION BY CODICAMAS ORDER BY ULTIMAJORNADA DESC = 1) con descripciones traducidas de clase de cama, clase de habitación, aislamiento, estado de cama y estado/color de limpieza. else Si @Origen = ''LimpiezaNormal'' devuelve histórico completo con usuarios que iniciaron/finalizaron/registraron y filtros por rango de fechas; cualquier otro valor no produce resultado.; si @Origen = ''LimpiezaNormal'' → Devuelve histórico de limpiezas filtrando registros cuya FECHAINI o FECHAFIN esté en [@FechaInicial, @FechaFinal], o ambos sean NULL, incluyendo nombres de centro de atención y de los usuarios que iniciaron, finalizaron y registraron la jornada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.CHCAMAASEO; dbo.ADCENATEN; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarHistoricoCamasLimpieza';
-- GO
