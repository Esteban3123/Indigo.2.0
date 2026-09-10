
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las glucometrías (mediciones de glucosa en sangre) registradas para un paciente durante un ingreso hospitalario específico, combinando los registros provenientes del examen físico de la historia clínica (HCEXFISIC) y de los insumos y actividades de enfermería (HCHOGASIN), filtrando únicamente unidades funcionales de tipo UCI o internación (códigos UI1, IU1, IN1) según la configuración clínica por unidad (HCUNITHIS). El resultado se presenta en dos columnas paralelas ordenadas cronológicamente, optimizando la impresión en papel, donde cada fila muestra el profesional de salud responsable (nombre y código del médico o enfermero obtenido de INPROFSAL), la fecha y el valor de glucosa de dos mediciones a la vez. Se usa en la generación de reportes clínicos de control glicémico para pacientes hospitalizados, permitiendo visualizar el historial completo de glucometrías de forma compacta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarGlucometrias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarGlucometrias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las glucometrías registradas para un paciente e ingreso hospitalario, distribuyéndolas en dos columnas paralelas (mitad y mitad) con el fin de optimizar el uso de papel en la impresión.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir y tener registros asociados en historia clínica; Las unidades de historia deben corresponder a tipos de hospitalización ''UI1'', ''IU1'' o ''IN1'' (unidades de internación/UCI); Deben existir registros de glucometría con valor no nulo y distinto de cero en HCEXFISIC.UCIADUGLU o HCHOGASIN.VALACTENF', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se consideran glucometrías solo cuando el valor existe y es distinto de cero; Solo se listan registros de unidades cuyo tipo de historia sea UI1, IU1 o IN1; Las glucometrías se obtienen de dos fuentes: examen físico (HCEXFISIC.UCIADUGLU) y notas/hoja de enfermería (HCHOGASIN.VALACTENF); Los registros se ordenan cronológicamente ascendente por fecha antes de segmentarse; Cuando el total es impar, la primera columna recibe un registro más que la segunda (uso de CEILING); El profesional se presenta como ''CODPROSAL - NOMMEDICO'' con espacios recortados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glucometría; Paciente; Ingreso hospitalario; Historia clínica; Unidad de internación/UCI; Profesional de la salud; Examen físico; Hoja de gases/enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN_RESULT: Devuelve un único result set con seis columnas (profesional, fecha y valor de glucometría) pareadas en dos bloques: la primera mitad (CEILING(total/2)) en las columnas PRO1/FEC1/GLU1 y el resto en PRO2/FEC2/GLU2, unidos por LEFT JOIN sobre el correlativo de fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Filtro T.CODTIPHIS IN (''UI1'',''IU1'',''IN1'') y campo de glucometría IS NOT NULL y <> 0 → Se incluye el registro en el conteo y en ambas tablas temporales como glucometría válida else Se excluye del listado; si Rownum <= CEILING(@cantidad/2.0) → El registro se asigna a la primera columna (@TABLA1) else El registro se asigna a la segunda columna (@TABLA2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC; dbo.HCUNITHIS; dbo.INPROFSAL; dbo.HCHOGASIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGlucometrias';
-- GO
