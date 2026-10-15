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