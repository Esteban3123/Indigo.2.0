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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los profesionales de salud participantes en una junta médica específica, identificada por el código o cédula del paciente, el número de ingreso y el número de folio de la junta. Consulta la tabla de juntas médicas cruzando con el maestro de profesionales de la salud y el catálogo de especialidades para obtener el nombre del médico, la especialidad, el número de tarjeta profesional y la firma digital de cada participante. Para la presentación en el reporte, organiza los participantes en dos columnas distribuidas en filas pareadas, mostrando hasta dos profesionales por fila. Se utiliza principalmente en la historia clínica del paciente para generar el acta o documento formal de la junta médica realizada durante su hospitalización o ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarJuntaMedica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarJuntaMedica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los profesionales participantes de una junta médica de un paciente/ingreso/folio, distribuyéndolos en dos columnas paralelas para presentación en reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en JUNTAMEDICA para la combinación paciente, ingreso y folio.; Los códigos de profesional y especialidad deben existir en INPROFSAL e INESPECIA respectivamente para que se incluyan vía INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad total de profesionales se divide en dos columnas usando redondeo aritmético estándar de la mitad.; Los nombres de profesionales se devuelven sin espacios laterales (rtrim/ltrim).; El emparejamiento entre columna 1 y columna 2 se hace por orden ascendente del ID original de JUNTAMEDICA.; Si un profesional no tiene especialidad o registro en INPROFSAL/INESPECIA, no aparece en el resultado (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Junta médica; Paciente; Ingreso; Folio; Profesional de salud; Especialidad médica; Tarjeta profesional; Firma médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve la junta médica del paciente/ingreso/folio repartida en dos columnas: la primera mitad (Row <= cantidad/2 redondeado) en PRO1/ESPE1/TARJE1/FIRMA1 y el resto en PRO2/ESPE2/TARJE2/FIRMA2.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Row <= @ResultadoSegmentacion (mitad redondeada del total de profesionales) → El profesional se inserta en la columna 1 (PRO1/ESPE1/TARJE1/FIRMA1). else El profesional se asigna mediante UPDATE a la columna 2 (PRO2/ESPE2/TARJE2/FIRMA2) emparejado por número de fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.JUNTAMEDICA; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarJuntaMedica';
-- GO
