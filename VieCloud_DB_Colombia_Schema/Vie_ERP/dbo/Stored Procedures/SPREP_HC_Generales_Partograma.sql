
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_Partograma]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

SELECT FECPARTOG AS HORA,  CONVERT(VARCHAR(30), FECREGIST) AS 'FECHA DEL REGISTRO',CONVERT(TIME, FECREGIST) AS 'HORA DEL REGISTRO', TENSSARTE AS 'TENSION ARTERIAL', 
		FRECUCARD AS 'SIGNO MATERNO FRECUENCIA CARDIACA',SMFRERESP AS 'FRECUENCIA RESPIRATORIA',
       TEMPERATU AS TEMPERATURA, AUFRECUEN AS 'ACT UTERINA FRECUENCIA CARDIACA', CASE WHEN AUINTESID = 1 THEN '+' WHEN AUINTESID = 2 THEN '++' WHEN AUINTESID = 3 THEN '+++' END AS INTENSIDAD, AUDURACIO AS DURACION, BFFETOCAR AS FETOCARDIA, 
       CASE WHEN BFDESACEL = 0 THEN 'No' WHEN BFDESACEL = 1 THEN 'Sí' END AS DESACELERACION, CASE WHEN BFMOVFETA = 0 THEN 'No' WHEN BFMOVFETA = 1 THEN 'Sí' END AS 'MOVIMIENTO FETAL', TVDILATAC AS DILATACION, CONVERT(VARCHAR(3),TVBORRAMI) + '%'  AS BORRAMIENTO, TVESTACIO AS ESTACION, CASE WHEN TVMEMBRAN = 1 THEN 'Integras' WHEN TVMEMBRAN = 2 THEN 'Rotas' END AS MEMBRANAS, 
       CASE WHEN TVLIQANMI = 1 THEN 'Claro' WHEN TVLIQANMI = 2 THEN 'Meconio Claro' WHEN TVLIQANMI = 3 THEN 'Meconio Espeso' END AS 'LIQUIDO AMNIOTICO', 
	   CASE WHEN TVVARPOSI = 1 THEN 'O.T.I' WHEN TVVARPOSI = 2 THEN 'O.I.A' WHEN TVVARPOSI = 3 THEN 'O.A' WHEN TVVARPOSI = 4 THEN 'O.D.A' WHEN TVVARPOSI = 5 THEN 'O.T.D' WHEN TVVARPOSI = 6 THEN 'O.P.D' WHEN TVVARPOSI = 7 THEN 'O.P' WHEN TVVARPOSI = 8 THEN 'O.P.I' END AS 'VARIEDAD DE POSICION', 
	   CONVERT(varbinary,'') AS VARPOSIMG,TVVARPOSI AS TVVARPOSI,OBSERVACI AS OBSERVACION, FECINIREG AS 'FECHA DE INICIO DE PARTOGRAMA', 
       CASE PARPELVIS WHEN '1' THEN 'ADECUADA' WHEN '2' THEN 'DUDOSA' WHEN '3' THEN 'NO ADECUADA' END AS PELVIS ,A.CODPROSAL AS 'CODIGO DEL MEDICO', 
	   B.NOMMEDICO AS 'NOMBRE MEDICO',B.TARJETAPR AS 'TARJETA PROFESIONAL',B.MEDIFIRMA AS 'FIRMA PROFESIONAL', RTRIM(C.DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',A.PARIDAD,A.POSMATERNA
FROM HCPARTGRA A WITH(NOLOCK)
INNER JOIN INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL = B.CODPROSAL
INNER JOIN INESPECIA C WITH(NOLOCK) ON B.CODESPEC1 = C.CODESPECI 
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte clínico del partograma obstétrico para una paciente y un ingreso específicos, consolidando el registro detallado del trabajo de parto (signos vitales maternos, actividad uterina, fetocardia, dilatación cervical, borramiento, estación, estado de membranas, líquido amniótico y variedad de posición fetal) almacenado en HCPARTGRA. Complementa la información con el nombre, tarjeta profesional, firma y especialidad del médico responsable, obtenidos del maestro de profesionales INPROFSAL y el catálogo de especialidades INESPECIA. Se utiliza para imprimir o visualizar el partograma completo en la historia clínica de la paciente en trabajo de parto, incluyendo paridad, posición materna y valoración pélvica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_Partograma';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_Partograma';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los registros del partograma de un paciente durante un ingreso específico, decodificando catálogos clínicos (intensidad, membranas, líquido amniótico, variedad de posición, pelvis) y enriqueciendo con datos del profesional tratante.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro de partograma asociado al paciente y número de ingreso suministrados; El profesional registrado en el partograma debe existir en el maestro de profesionales de salud; El profesional debe tener una especialidad principal válida en el catálogo de especialidades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan partogramas que tengan profesional de salud y especialidad asociados (INNER JOIN); El borramiento se expresa siempre como porcentaje concatenando ''%''; La consulta no modifica datos; usa NOLOCK en todas las tablas; Los códigos numéricos clínicos se entregan siempre traducidos a etiquetas legibles', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Partograma; Paciente; Ingreso hospitalario; Signos maternos (tensión arterial, frecuencia cardíaca, frecuencia respiratoria, temperatura); Actividad uterina (frecuencia, intensidad, duración); Bienestar fetal (fetocardia, desaceleración, movimiento fetal); Tacto vaginal (dilatación, borramiento, estación, membranas, líquido amniótico, variedad de posición); Evaluación de pelvis; Paridad; Posición materna; Profesional de salud; Especialidad médica; Tarjeta profesional; Firma profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPARTGRA: Cuando IPCODPACI = paciente y NUMINGRES = ingreso, retorna el conjunto de datos del partograma con catálogos decodificados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AUINTESID en (1,2,3) → Traduce intensidad de actividad uterina a ''+'', ''++'' o ''+++''; si BFDESACEL = 0 o 1 → Traduce desaceleración a ''No'' o ''Sí''; si BFMOVFETA = 0 o 1 → Traduce movimiento fetal a ''No'' o ''Sí''; si TVMEMBRAN = 1 o 2 → Traduce estado de membranas a ''Integras'' o ''Rotas''; si TVLIQANMI en (1,2,3) → Traduce líquido amniótico a ''Claro'', ''Meconio Claro'' o ''Meconio Espeso''; si TVVARPOSI entre 1 y 8 → Traduce variedad de posición fetal (O.T.I, O.I.A, O.A, O.D.A, O.T.D, O.P.D, O.P, O.P.I); si PARPELVIS en (''1'',''2'',''3'') → Traduce evaluación de pelvis a ''ADECUADA'', ''DUDOSA'' o ''NO ADECUADA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPARTGRA; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Partograma';
-- GO
