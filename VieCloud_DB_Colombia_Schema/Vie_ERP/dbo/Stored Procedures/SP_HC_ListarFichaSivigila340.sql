-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,27-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila340]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
	 
			Select
				CASE DONASANGRE  WHEN '1' THEN 'X' END AS 'DONASANGRE si',CASE DONASANGRE WHEN '0' THEN 'X' END AS 'DONASANGRE no',
				CASE VACUPREVHEPB  WHEN '1' THEN 'X' END AS 'VACUPREVHEPB si',CASE VACUPREVHEPB WHEN '0' THEN 'X' END AS 'VACUPREVHEPB no',
				CASE SIGYSINT  WHEN '1' THEN 'X' END AS 'SIGYSINT si',CASE SIGYSINT WHEN '0' THEN 'X' END AS 'SIGYSINT no',
				CASE COINFVIH  WHEN '1' THEN 'X' END AS 'COINFVIH si',CASE COINFVIH WHEN '0' THEN 'X' END AS 'COINFVIH no',
				convert(varchar(10),FECHAULTDOS,103) As 'FECHAULTDOS', convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE',
				convert(varchar(10),FECHARESUL,103) As 'FECHARESUL',
				Rtrim(SEMGEST) As 'SEMGEST', Rtrim(NUMDOSIS) As 'NUMDOSIS', Rtrim(NOMAPE) As 'NOMAPE', 
				CASE Rtrim(TIPOID)  WHEN '1' THEN 'RC: Registro Civil' WHEN '2' THEN 'TI: Tarjeta de identidad' WHEN '3' THEN 'CC: Cedula de ciudadanía' WHEN '4' THEN 'CE: Cedula de extranjería' WHEN '5' THEN 'PA: Pasaporte' WHEN '6' THEN 'MS: Menor Sin Identificación' WHEN '7' THEN 'AS: Adulto Sin Identificación' WHEN '8' THEN 'PE: Permiso Especial de Permanencia' WHEN 9 THEN 'PT: Permiso por protección temporal' END AS 'TIPOID', 				
				Rtrim(NUMIDENT) As 'NUMIDENT', Rtrim(VALOR) As 'VALOR',
				CASE CLASICOMO WHEN '1' THEN 'X' END AS 'Paciente Resu',CASE CLASICOMO WHEN '2' THEN 'X' END AS 'Hepatitis B Aguda', CASE CLASICOMO WHEN '3' THEN 'X' END AS 'HepaBCronica',
				CASE FUENTE WHEN '1' THEN 'X' END AS 'Carne',CASE FUENTE WHEN '2' THEN 'X' END AS 'Verbal', CASE FUENTE WHEN '3' THEN 'X' END AS 'Sin Dato',
				CASE CLASICOMO WHEN '4' THEN 'X' END AS 'HepaBtransPerin',CASE CLASICOMO WHEN '5' THEN 'X' END AS 'HepaCoinfB-D', CASE CLASICOMO WHEN '6' THEN 'X' END AS 'HepaC',
				CASE PRESEALGCOMPLI WHEN '1' THEN 'X' END AS 'FallaHepat',CASE PRESEALGCOMPLI WHEN '2' THEN 'X' END AS 'Cirrosis', CASE PRESEALGCOMPLI WHEN '3' THEN 'X' END AS 'Carcinoma',
				CASE PRESEALGCOMPLI WHEN '4' THEN 'X' END AS 'SindromeFebril',CASE PRESEALGCOMPLI WHEN '5' THEN 'X' END AS 'Ninguna',
				CASE APLIVACUHEPB WHEN '1' THEN 'X' END AS 'Prim12hrs1',CASE APLIVACUHEPB WHEN '2' THEN 'X' END AS '13a24hrs1', CASE APLIVACUHEPB WHEN '3' THEN 'X' END AS 'mas24hrs1',
				CASE APLIVACUHEPB WHEN '4' THEN 'X' END AS 'SinDato1',CASE APLIVACUHEPB WHEN '5' THEN 'X' END AS 'NoApli1',
				CASE APLIGAMAGLO WHEN '1' THEN 'X' END AS 'Prim12hrs2',CASE APLIGAMAGLO WHEN '2' THEN 'X' END AS '13a24hrs2', CASE APLIGAMAGLO WHEN '3' THEN 'X' END AS 'mas24hrs2',
				CASE APLIGAMAGLO WHEN '4' THEN 'X' END AS 'SinDato2',CASE APLIGAMAGLO WHEN '5' THEN 'X' END AS 'NoApli2',
				CASE MODOTRANS WHEN '1' THEN 'X' END AS 'MaternoInf',CASE MODOTRANS WHEN '2' THEN 'X' END AS 'Horizontal', CASE MODOTRANS WHEN '3' THEN 'X' END AS 'Parental',
				CASE MODOTRANS WHEN '4' THEN 'X' END AS 'Sexual',
				CASE MOMDIAGHB WHEN '1' THEN 'X' END AS 'PrevGest',CASE MOMDIAGHB WHEN '2' THEN 'X' END AS 'DuranGest', CASE MOMDIAGHB WHEN '3' THEN 'X' END AS 'MomentPart',
				CASE MOMDIAGHB WHEN '4' THEN 'X' END AS 'PostParto',
				CASE HIJOMAD WHEN '1' THEN 'X' END AS 'HIJOMAD',CASE MASCOMPSEX WHEN '1' THEN 'X' END AS 'MASCOMPSEX', CASE HSH WHEN '1' THEN 'X' END AS 'HSH',
				CASE BISEXUAL WHEN '1' THEN 'X' END AS 'BISEXUAL',CASE ANTETRANSHEMO WHEN '1' THEN 'X' END AS 'ANTETRANSHEMO', CASE USUHEMODIA WHEN '1' THEN 'X' END AS 'USUHEMODIA',
				CASE TRABSALUD WHEN '1' THEN 'X' END AS 'TRABSALUD',CASE ACCILABORAL WHEN '1' THEN 'X' END AS 'ACCILABORAL', CASE TRANSPORG WHEN '1' THEN 'X' END AS 'TRANSPORG',
				CASE PERSOINYDROG WHEN '1' THEN 'X' END AS 'PERSOINYDROG',CASE CONVPORTHBsAg WHEN '1' THEN 'X' END AS 'CONVPORTHBsAg',
				CASE CONTSEXHBsAg WHEN '1' THEN 'X' END AS 'CONTSEXHBsAg',CASE PROCENTESTE WHEN '1' THEN 'X' END AS 'PROCENTESTE', CASE RECACUPUNTU WHEN '1' THEN 'X' END AS 'RECACUPUNTU',
				CASE MUESTRA WHEN '1' THEN 'Sangre Total' WHEN '2' THEN 'Tejido' WHEN '3' THEN 'Suero' END AS 'MUESTRA',
				CASE PRUEBA  WHEN '1' THEN 'HBsAg' WHEN '2' THEN 'Patología' WHEN '3' THEN 'AntiVHD' WHEN '4' THEN'Anti-HBc IgM' WHEN '5' THEN 'Anti-HBc Totales' WHEN '6' THEN 'Anti VHC'
				WHEN '7' THEN 'Carga Viral' WHEN '8' THEN 'Pruebas Genotípicas' WHEN '9' THEN 'Inmunoensayo' END AS 'PRUEBA',
				CASE AGENTE  WHEN '1' THEN 'Hepatitis b' WHEN '2' THEN 'Hepatitis Delta' WHEN '3' THEN 'Hepatitis c' END AS 'AGENTE',
				CASE RESULTADO  WHEN '1' THEN 'Compatible' WHEN '2' THEN 'Reactivo' WHEN '3' THEN 'No Reactivo' END AS 'RESULTADO',
				VERSION AS 'VERSION', 
				JSON AS 'JSON',
				CASE JSON_VALUE(JSON, '$.ANTEPROCENESTE') WHEN 'true' THEN 'X' END As 'ANTEPROCENESTE',
				CASE JSON_VALUE(JSON, '$.ANTEPIERC') WHEN 'true' THEN 'X' END As 'ANTEPIERC'

			From 
				HCFICHA340 
			
			Where 
				IDFICHANOTIFICACION  = @IdFicha 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta el contenido completo de una ficha epidemiológica de notificación de Hepatitis B (evento SIVIGILA 340) a partir de su identificador o número de ficha. Accede a la tabla HCFICHA340 y transforma los códigos numéricos en etiquetas legibles para impresión o visualización del formulario oficial, incluyendo datos de identificación del paciente (cédula, tipo de documento), factores de riesgo y antecedentes de transmisión (donación de sangre, modo de transmisión, relaciones sexuales, uso de drogas intravenosas, accidente laboral), vacunación previa contra Hepatitis B y aplicación de gammaglobulina, resultados de laboratorio (tipo de muestra, prueba realizada, agente y resultado), signos y síntomas, coinfección con VIH, complicaciones presentes, clasificación del caso (aguda, crónica, perinatal, coinfección B-D, Hepatitis C), y datos específicos para gestantes como semanas de gestación y momento del diagnóstico. Se utiliza para generar el reporte o formulario impreso de la ficha SIVIGILA 340 dentro del módulo de historia clínica y vigilancia epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila340';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista y formatea la información de una ficha de notificación SIVIGILA (evento 340 - Hepatitis B/C/D) para reportes, traduciendo códigos a etiquetas legibles y marcando casillas tipo ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA340 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.; La columna JSON debe contener un objeto JSON válido para que JSON_VALUE pueda extraer ANTEPROCENESTE y ANTEPIERC.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven datos de una ficha por ejecución (filtrada por IDFICHANOTIFICACION).; Los campos booleanos (''1''/''0'' o ''1'') se presentan como ''X'' o nulo, nunca como valores numéricos.; Las fechas se entregan en formato dd/mm/yyyy (estilo 103).; El parámetro @NumFicha se recibe pero no participa en el filtrado ni en el resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación SIVIGILA; Hepatitis B; Hepatitis C; Hepatitis Delta; Coinfección VIH; Vacunación Hepatitis B; Gammaglobulina; Transmisión materno-infantil; Donación de sangre; Tipo de identificación del paciente; Semanas de gestación; Antecedentes de exposición (hemodiálisis, transfusión, acupuntura, drogas inyectables, trabajador de salud, accidente laboral, trasplante de órganos); HSH/Bisexual/Múltiples compañeros sexuales; Muestra y prueba de laboratorio; Resultado serológico; Complicaciones (falla hepática, cirrosis, carcinoma, síndrome febril)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA340: Cuando IDFICHANOTIFICACION = @IdFicha, retorna la fila con códigos traducidos a descripciones y marcadores ''X'' según el valor de cada campo categórico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOID ∈ {1..9} → Traduce a etiqueta de tipo de identificación (RC, TI, CC, CE, PA, MS, AS, PE, PT).; si CLASICOMO = 1..6 → Marca ''X'' en la clasificación clínica correspondiente: Paciente Resuelto, Hepatitis B Aguda, Hepatitis B Crónica, Hepatitis B transmisión perinatal, Coinfección B-D, o Hepatitis C.; si APLIVACUHEPB / APLIGAMAGLO = 1..5 → Marca ''X'' según ventana de aplicación (≤12h, 13-24h, >24h, Sin Dato, No Aplica) para vacuna HepB y gammaglobulina.; si MODOTRANS = 1..4 → Marca ''X'' según modo de transmisión (Materno-Infantil, Horizontal, Parenteral, Sexual).; si MOMDIAGHB = 1..4 → Marca ''X'' según momento del diagnóstico de Hepatitis B (Pre-gestación, Durante gestación, Momento del parto, Post-parto).; si MUESTRA = 1..3 → Traduce a tipo de muestra (Sangre Total, Tejido, Suero).; si PRUEBA = 1..9 → Traduce a nombre de prueba (HBsAg, Patología, AntiVHD, Anti-HBc IgM/Totales, Anti VHC, Carga Viral, Pruebas Genotípicas, Inmunoensayo).; si AGENTE = 1..3 → Traduce a agente etiológico (Hepatitis B, Hepatitis Delta, Hepatitis C).; si RESULTADO = 1..3 → Traduce a resultado (Compatible, Reactivo, No Reactivo).; si JSON_VALUE(JSON,''$.ANTEPROCENESTE'')=''true'' o JSON_VALUE(JSON,''$.ANTEPIERC'')=''true'' → Marca ''X'' en los antecedentes correspondientes extraídos del documento JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA340', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila340';
-- GO
