CREATE PROCEDURE [dbo].[SP_HC_ListarJuntaMedica]( 
	@Paciente varchar(25),
	@Ingreso  varchar(20),
	@Folio  varchar(20)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Declaramos los campos de la tabla que se va cargar en el reporte

	declare @Profesional1 as varchar(200)
	declare @Profesional2 as varchar(200)
	

	-- Se declara la tabla con los campos declarados anteriormente y agregamos un ID para hacer la segmentacion a 2 columnas y la FILA para hacer la relacion entre las dos tablas segmentadas (UPDATE)

	DECLARE @TABLA AS TABLE(ID INT IDENTITY(1,1),FILA INTEGER, PRO1 VARCHAR(200), ESPE1 VARCHAR(200),TARJE1 VARCHAR(200), FIRMA1 VARBINARY(MAX), PRO2 VARCHAR(200),ESPE2 VARCHAR(200),TARJE2  VARCHAR(200),FIRMA2 VARBINARY(MAX))

    -- Se encuentra la cantidad de registros totales de glucometrias
	declare @cantidad int = (
								SELECT COUNT(ID) from JUNTAMEDICA F  with(nolock)
								inner join INPROFSAL P with(nolock) ON P.CODPROSAL  = F.CODPROSAL 
								where F.IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND F.NUMEFOLIO =@Folio
							)
	-- Logica de Segmentacion
	declare @ResultadoSegmentacion as decimal(18,2) = 0
	set @ResultadoSegmentacion =convert(decimal,@cantidad) / 2
	set @ResultadoSegmentacion = CONVERT(int,ROUND(@ResultadoSegmentacion,0))

	-- Se inserta la primera tabla, es decir la de la columna 1.
	INSERT INTO @TABLA
	select  row,CodigoNombreProfesional,DESESPECI,TARJETAPR,MEDIFIRMA,NULL,NULL, NULL, NULL from (		
				SELECT  ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row, F.IPCODPACI as ID,  rtrim(ltrim(P.NOMMEDICO)) as CodigoNombreProfesional,I.DESESPECI,P.TARJETAPR,P.MEDIFIRMA  
				from JUNTAMEDICA F with(nolock) inner join
				INPROFSAL P with(nolock) ON P.CODPROSAL  = F.CODPROSAL Inner Join
				INESPECIA I with(nolock) ON I.CODESPECI = F.CODESPECIA
				where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND F.NUMEFOLIO =@Folio
		) as TMP where Row <= @ResultadoSegmentacion

   -- Se actualiza la segunda tabla, es decir la de la columna 2.
   UPDATE @TABLA SET PRO2 = CodigoNombreProfesional,ESPE2=DESESPECI,TARJE2=TARJETAPR,FIRMA2= MEDIFIRMA
   from (		
		 select  ROW_NUMBER() OVER(ORDER BY ID ASC) AS Fila, TMP.*  from (
				SELECT  ROW_NUMBER() OVER(ORDER BY ID ASC) AS Row, F.IPCODPACI as ID,  rtrim(ltrim(P.NOMMEDICO)) as CodigoNombreProfesional,I.DESESPECI,P.TARJETAPR,P.MEDIFIRMA  
				from JUNTAMEDICA F with(nolock) inner join 
				INPROFSAL P with(nolock) ON P.CODPROSAL  = F.CODPROSAL Inner Join
				INESPECIA I with(nolock) ON I.CODESPECI = F.CODESPECIA
				where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND F.NUMEFOLIO =@Folio
				) as TMP where Row > @ResultadoSegmentacion
		) as TMPY inner join @TABLA  as t on t.Fila = TMPY.Fila   
  
--  update @TABLA set PRO1='PRINCIPAL-' + PRO1 WHERE ID=1

  SELECT ID,FILA,PRO1,ESPE1,TARJE1,FIRMA1,PRO2,ESPE2,TARJE2,FIRMA2 FROM @TABLA
	

END