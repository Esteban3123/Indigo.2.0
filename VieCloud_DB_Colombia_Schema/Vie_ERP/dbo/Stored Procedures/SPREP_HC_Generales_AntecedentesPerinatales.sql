
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_AntecedentesPerinatales]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10),
@NumeroIngreso nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
 SELECT EDADMADRE AS 'EDAD MADRE', EDADGESTA AS 'EDAD GESTACIONAL', NUMPARIDA AS 'NUMERO PARIDAD', CASE TIPOPARTO WHEN 1 THEN 'Vaginal' WHEN 2 THEN 'Instrumentada' WHEN 3 THEN 'Cesarea' END AS 'TIPO PARTO', CASE CONTPRENA WHEN 1 THEN 'Si' ELSE 'No' END AS 'CONTROL PRENATAL', CANTPRENA AS 'CANTIDAD CONTROLES PRENATALES',CASE GESTACION WHEN 1 THEN 'Unico' WHEN 2 THEN 'Multiple' END AS GESTACION, CANTGESTA AS 'CANTIDAD GESTACIONES', CASE IQGTOXOPL WHEN 1 THEN 'Positivo' WHEN 0 THEN 'Negativo' ELSE 'No Tiene' END AS 'IQG TOXOPLASMA', CANTTOXO AS 'CANTIDAD IQG TOXOPLASMA', CASE IQMTOXOPL WHEN 1 THEN 'Positivo' WHEN 2 THEN 'Negativo' WHEN 3 THEN 'No Tiene' END AS 'IQM TOXOPLASMA', CASE PRESENTAC WHEN 1 THEN 'Cefalico' WHEN 2 THEN 'Pelvis' WHEN 3 THEN 'Transverso' END AS PRESENTACION, CASE RESULTHIV WHEN 1 THEN 'Positivo' WHEN 0 THEN 'Negativo' ELSE 'No Tiene' END AS 'RESULTADO HIV VIH',
                 CASE HEPATITIB WHEN 1 THEN 'Positivo' WHEN 2 THEN 'Negativo' WHEN 3 THEN 'No Tiene' END AS 'RESULTADO HEPATITIS B', CANTHEPAT AS 'CANTIDAD HEPATITIS B', CASE RESULVDRL WHEN 1 THEN 'Positivo' WHEN 0 THEN 'Negativo' ELSE 'No Tiene' END AS 'RESULTADO VDRL', DILUCVDRL AS DILUCIONES, RUPPREMEM AS 'RUPTURA PREMATURA MEMBRANAS', CASE UNIDTIEMP WHEN 1 THEN 'Horas' WHEN 2 THEN 'Dias' END AS 'UNIDAD TIEMPO RUPTURA PREMATURA MEMBRANA', OTROSPERI AS 'OTROS PERINATALES', IPGRUPSAP AS 'GRUPO SANGUINEO PATERNO', IPRHSANGP AS 'RH PATERNO', IPGRUPSAM AS 'GRUPO SANGUINEO MATERNO', IPRHSANGM AS 'RH MATERNO',
                 PERICEFAL AS 'PERIMETRO CEFALICO', PERITORAX AS 'PERIMETRO TORAXICO', PERIABDOM AS 'PERIMETRO ABDOMINAL', TALLAPACI AS 'TALLA PACIENTE', PESOPACIE AS 'PESO PACIENTE', BALLARDPA AS 'BALLARD PACIENTE', APGAR1PAC AS 'APGAR 1MINUTO', APGAR5PAC AS 'APGAR 5MINUTOS', APGAR10PA AS 'APGAR 10MINUTOS', CASE ADAPNEONT WHEN 1 THEN 'Espontanea' WHEN 2 THEN 'Conducida' WHEN 3 THEN 'Inducida' END AS 'ADAPTACION NEONATAL', CASE MECONIOPA WHEN 1 THEN 'Positivo' ELSE 'Negativo' END AS 'MECONIO PACIENTE',
                 REANIMACI AS REANIMACION, CASE TIPALIMEN WHEN 1 THEN 'Sin Definir' WHEN 2 THEN 'Leche Materna' WHEN 3 THEN 'Artificial' END AS 'TIPO ALIMENTACION', EXAMENESE AS 'EXAMENES EXTERNOS ADICIONALES', DESCRIPEX AS 'DESCRIPCION ANOMALIA'
                 
FROM HCANTPERI WITH(NOLOCK)

WHERE NUMEFOLIO = @NumeroFolio AND IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y presenta los antecedentes perinatales registrados en la historia clínica de un recién nacido durante su ingreso hospitalario, filtrando por cédula del paciente, número de folio y número de ingreso. Recupera datos clínicos del parto y del período neonatal como: edad de la madre, edad gestacional, tipo de parto (vaginal, instrumentado o cesárea), controles prenatales, resultados de laboratorio maternos (toxoplasma, VIH/HIV, hepatitis B, VDRL), presentación fetal, ruptura prematura de membranas, grupos sanguíneos y RH de padres, así como medidas antropométricas del recién nacido (peso, talla, perímetros cefálico, torácico y abdominal), puntajes Apgar a 1, 5 y 10 minutos, escala de Ballard, adaptación neonatal, meconio, tipo de alimentación y reanimación. Este procedimiento se usa para imprimir o visualizar el informe de antecedentes perinatales dentro de la historia clínica, consultando directamente la tabla HCANTPERI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los antecedentes perinatales de un paciente para un folio e ingreso específicos, traduciendo códigos internos a etiquetas legibles para presentación en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de antecedentes perinatales asociado a la combinación de folio, código de paciente e ingreso suministrados; de lo contrario el resultado será vacío.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta filtra exclusivamente por la combinación de folio, paciente e ingreso, garantizando que sólo se devuelven antecedentes perinatales del episodio clínico solicitado.; Las lecturas se hacen con NOLOCK, asumiendo que se aceptan lecturas sucias para reportes.; Los códigos numéricos almacenados se traducen a etiquetas legibles (Vaginal/Instrumentada/Cesárea, Positivo/Negativo/No Tiene, Cefálico/Pelvis/Transverso, etc.).; Para resultados serológicos binarios (toxoplasma IgG, VIH, VDRL): 1=Positivo, 0=Negativo, cualquier otro valor=No Tiene.; Para resultados serológicos ternarios (toxoplasma IgM, hepatitis B): 1=Positivo, 2=Negativo, 3=No Tiene.; Meconio sólo distingue Positivo (1) vs Negativo (cualquier otro valor).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Antecedentes perinatales; Edad gestacional; Paridad; Tipo de parto; Control prenatal; Gestación única/múltiple; Toxoplasmosis (IgG/IgM); VIH; Hepatitis B; VDRL; Ruptura prematura de membranas; Grupo sanguíneo y RH materno/paterno; Antropometría neonatal (perímetro cefálico, torácico, abdominal, talla, peso); Ballard; APGAR (1, 5 y 10 minutos); Adaptación neonatal; Meconio; Reanimación neonatal; Tipo de alimentación (leche materna/artificial); Presentación fetal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCANTPERI: Cuando NUMEFOLIO, IPCODPACI y NUMINGRES coinciden con los parámetros, se devuelve un resultset con los antecedentes perinatales decodificados (tipo de parto, serologías, antropometría neonatal, APGAR, etc.).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPERI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_AntecedentesPerinatales';
-- GO
