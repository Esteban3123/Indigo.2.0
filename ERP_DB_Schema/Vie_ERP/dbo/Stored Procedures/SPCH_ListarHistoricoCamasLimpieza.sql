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