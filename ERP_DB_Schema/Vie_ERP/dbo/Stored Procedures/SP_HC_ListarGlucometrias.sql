
-- =============================================
-- Author:		<Johan Sebastian Carranza Ramos, Developer Junior>
-- Create date: <14 Mayo de 2019>
-- Description:	<Procedimiento almacenado que me lista las Glucometrias en 2 columnas para ahorro de papel>
--==============================================
-- Edition date:<29 Enero de 2024>
-- Edited by:   <Andres David Losada Valderrama, Technical Junior Developer>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarGlucometrias]( 
	@Paciente varchar(25),
	@Ingreso  varchar(20)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Se declaran las tablas que van a almacenar los datos de las diferentes consultas a realizar

	DECLARE @TABLA1 AS TABLE(ID INT IDENTITY(1,1),PRO1 VARCHAR(200), FEC1 DATETIME, GLU1 INT)
	DECLARE @TABLA2 AS TABLE(ID INT IDENTITY(1,1),PRO2 VARCHAR(200), FEC2 DATETIME, GLU2 INT)
	DECLARE @TABLA AS TABLE(PRO1 VARCHAR(200), FEC1 DATETIME, GLU1 INT, PRO2 VARCHAR(200), FEC2 DATETIME, GLU2 INT)

    -- Se encuentra la cantidad de registros totales de glucometrias
	declare @cantidad int, @ResultadoSegmentacion as int
	select @cantidad  = count(*) from(
									SELECT NUMCONSEC from HCEXFISIC F 
									inner join HCUNITHIS T  ON F.CODCENATE = T.CODCENATE and F.UFUCODIGO =  T.UFUCODIGO 
									inner join INPROFSAL P ON P.CODPROSAL  = F.CODPROSAL 
									where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND F.UCIADUGLU IS NOT NULL AND F.UCIADUGLU <> 0
				
									union all

									SELECT CONSECUTI from HCHOGASIN H 
									inner join HCUNITHIS T  ON H.CODCENATE = T.CODCENATE and H.UFUCODIGO =  T.UFUCODIGO 
									inner join INPROFSAL P ON P.CODPROSAL  = H.CODPROSAL 
									where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND H.VALACTENF IS NOT NULL AND H.VALACTENF <> 0
								) as Result

	-- Se divide el resultado entre dos
	set @ResultadoSegmentacion = CEILING(@cantidad / 2.0);

	-- Se insertan los datos en la primera tabla
	INSERT INTO @TABLA1 (PRO1,FEC1,GLU1)
	select  PRO1,FEC1,GLU1 from (		
				SELECT  ROW_NUMBER() OVER(ORDER BY FEC1 ASC) AS Rownum, PRO1, FEC1, GLU1
				from (
				SELECT   rtrim(ltrim(P.CODPROSAL)) + ' - ' + rtrim(ltrim(P.NOMMEDICO)) as PRO1  ,F.FECREGITE AS FEC1,F.UCIADUGLU  AS GLU1
					from HCEXFISIC F 
						inner join HCUNITHIS T  ON F.CODCENATE = T.CODCENATE and F.UFUCODIGO =  T.UFUCODIGO 
						inner join INPROFSAL P ON P.CODPROSAL  = F.CODPROSAL 
					where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND F.UCIADUGLU IS NOT NULL AND F.UCIADUGLU <> 0

				union all

					SELECT rtrim(ltrim(P.CODPROSAL)) + ' - ' + rtrim(ltrim(P.NOMMEDICO)) as PRO1  ,H.FECHAUTIL AS FEC1,H.VALACTENF AS GLU1 
					from HCHOGASIN H 
						inner join HCUNITHIS T  ON H.CODCENATE = T.CODCENATE and H.UFUCODIGO =  T.UFUCODIGO 
						inner join INPROFSAL P ON P.CODPROSAL  = H.CODPROSAL 
					where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND H.VALACTENF IS NOT NULL AND H.VALACTENF <> 0
				) As resultado
		) as TMP where Rownum <= @ResultadoSegmentacion

  -- Se insertan los datos en la segunda tabla
  INSERT INTO @TABLA2 (PRO2,FEC2,GLU2)	
  select  PRO2,FEC2,GLU2 from (		
				SELECT  ROW_NUMBER() OVER(ORDER BY FEC2 ASC) AS Rownum, PRO2, FEC2, GLU2
				from (
				SELECT   rtrim(ltrim(P.CODPROSAL)) + ' - ' + rtrim(ltrim(P.NOMMEDICO)) as PRO2  ,F.FECREGITE AS FEC2,F.UCIADUGLU  AS GLU2
					from HCEXFISIC F 
						inner join HCUNITHIS T  ON F.CODCENATE = T.CODCENATE and F.UFUCODIGO =  T.UFUCODIGO 
						inner join INPROFSAL P ON P.CODPROSAL  = F.CODPROSAL 
					where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND F.UCIADUGLU IS NOT NULL AND F.UCIADUGLU <> 0
				
				union all

					SELECT rtrim(ltrim(P.CODPROSAL)) + ' - ' + rtrim(ltrim(P.NOMMEDICO)) as PRO2  ,H.FECHAUTIL AS FEC2, H.VALACTENF AS GLU2
					from HCHOGASIN H 
					inner join HCUNITHIS T  ON H.CODCENATE = T.CODCENATE and H.UFUCODIGO =  T.UFUCODIGO 
					inner join INPROFSAL P ON P.CODPROSAL  = H.CODPROSAL 
					where IPCODPACI = @Paciente and NUMINGRES = @Ingreso AND T.CODTIPHIS IN ('UI1','IU1','IN1') AND H.VALACTENF IS NOT NULL AND H.VALACTENF <> 0
				) As resultado
		) as TMP2 where Rownum > @ResultadoSegmentacion

  -- Se insertan los datos en la Tabla para devolver un solo resultado
  INSERT INTO @TABLA (PRO1,FEC1,GLU1, PRO2,FEC2,GLU2)
  select PRO1,FEC1,GLU1, PRO2,FEC2,GLU2 from @TABLA1 T1 
  left join @TABLA2 T2 on T1.ID = T2.ID

  SELECT PRO1,FEC1,GLU1,PRO2,FEC2,GLU2 FROM @TABLA
	

END