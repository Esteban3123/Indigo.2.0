-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,14-12-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila355]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON; 

			Select
				CASE NAUSEAS WHEN '1' THEN 'X' END AS 'NAUSEAS',CASE VOMITO WHEN '1' THEN 'X' END AS 'VOMITO', CASE DIARREA WHEN '1' THEN 'X' END AS 'DIARREA',
				CASE FIEBRE WHEN '1' THEN 'X' END AS 'FIEBRE',CASE CALAMBRES WHEN '1' THEN 'X' END AS 'CALAMBRES', CASE CEFALEA WHEN '1' THEN 'X' END AS 'CEFALEA',
				CASE DESHIDRATA WHEN '1' THEN 'X' END AS 'DESHIDRATA',CASE CIANOSIS WHEN '1' THEN 'X' END AS 'CIANOSIS', CASE MIALGIAS WHEN '1' THEN 'X' END AS 'MIALGIAS',
				CASE ARTRALGIAS WHEN '1' THEN 'X' END AS 'ARTRALGIAS',CASE MAREO WHEN '1' THEN 'X' END AS 'MAREO',
				CASE LESIONES WHEN '1' THEN 'X' END AS 'LESIONES',CASE ESCALOFRIO WHEN '1' THEN 'X' END AS 'ESCALOFRIO', CASE PARESTESIAS WHEN '1' THEN 'X' END AS 'PARESTESIAS',
				CASE SIALORREA WHEN '1' THEN 'X' END AS 'SIALORREA',CASE ESPASMOS WHEN '1' THEN 'X' END AS 'ESPASMOS', CASE OTROS WHEN '1' THEN 'X' END AS 'OTROS',
				CASE HECES WHEN '1' THEN 'X' END AS 'HECES',CASE VOMITO2 WHEN '1' THEN 'X' END AS 'VOMITO2', CASE SANGRE WHEN '1' THEN 'X' END AS 'SANGRE',
				CASE OTRA WHEN '1' THEN 'X' END AS 'OTRA',
				Rtrim(MARCOTROS) As 'MARCOTROS', Rtrim(NOMALIME1) As 'NOMALIME1', Rtrim(NOMALIME2) As 'NOMALIME2', Rtrim(NOMALIME3) As 'NOMALIME3', Rtrim(NOMALIME4) As 'NOMALIME4', Rtrim(NOMALIME5) As 'NOMALIME5',
				Rtrim(NOMALIME6) As 'NOMALIME6', Rtrim(NOMALIME7) As 'NOMALIME7', Rtrim(NOMALIME8) As 'NOMALIME8', Rtrim(NOMALIME9) As 'NOMALIME9', Rtrim(LUGCONSU1) As 'LUGCONSU1', Rtrim(LUGCONSU2) As 'LUGCONSU2',
				Rtrim(LUGCONSU3) As 'LUGCONSU3', Rtrim(LUGCONSU4) As 'LUGCONSU4', Rtrim(LUGCONSU5) As 'LUGCONSU5', Rtrim(LUGCONSU6) As 'LUGCONSU6', Rtrim(LUGCONSU7) As 'LUGCONSU7', Rtrim(LUGCONSU8) As 'LUGCONSU8',
				Rtrim(LUGCONSU9) As 'LUGCONSU9', Rtrim(NOMLUG) As 'NOMLUG', Rtrim(DIRECCION) As 'DIRECCION', Rtrim(CUAL) As 'CUAL', Rtrim(CUALOTRO) As 'CUALOTRO',
				cast(HORAINICIO as time) As 'HORAINICIO', cast(HORACON1 as time) As 'HORACON1', cast(HORACON2 as time) As 'HORACON2', cast(HORACON3 as time) As 'HORACON3',
				cast(HORACON4 as time) As 'HORACON4', cast(HORACON5 as time) As 'HORACON5', cast(HORACON6 as time) As 'HORACON6', cast(HORACON7 as time) As 'HORACON7', 
				cast(HORACON8 as time) As 'HORACON8', cast(HORACON9 as time) As 'HORACON9',
				CASE CASOASOC  WHEN '1' THEN 'X' END AS 'CASOASOC si',CASE CASOASOC WHEN '0' THEN 'X' END AS 'CASOASOC no',
				CASE CASOCAPT  WHEN '1' THEN 'X' END AS 'CASOCAPT si',CASE CASOCAPT WHEN '0' THEN 'X' END AS 'CASOCAPT no',
				CASE RELAEXPO  WHEN '1' THEN 'X' END AS 'RELAEXPO si',CASE RELAEXPO WHEN '0' THEN 'X' END AS 'RELAEXPO no',
				CASE RECOMUES  WHEN '1' THEN 'X' END AS 'RECOMUES si',CASE RECOMUES WHEN '0' THEN 'X' END AS 'RECOMUES no',

				CASE AGENIDENT1 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3' WHEN '4' THEN '4' WHEN '5' THEN '5' WHEN '6' THEN '6'
				WHEN '7' THEN '7' WHEN '8' THEN '8' WHEN '9' THEN '9' WHEN '10' THEN '11' WHEN '11' THEN '12' WHEN '12' THEN '13'
				WHEN '13' THEN '14' WHEN '14' THEN '15' WHEN '15' THEN '16' WHEN '16' THEN '17' WHEN '17' THEN '18' WHEN '18' THEN '19'
				WHEN '19' THEN '20' WHEN '20' THEN '21' WHEN '21' THEN '22' WHEN '22' THEN '24' WHEN '23' THEN '25' WHEN '24' THEN '26'
				WHEN '25' THEN '27' WHEN '26' THEN '28' WHEN '27' THEN '30' WHEN '28' THEN '32' WHEN '29' THEN '33' WHEN '30' THEN '34'
				WHEN '31' THEN '35' WHEN '32' THEN '36' WHEN '33' THEN '37' WHEN '34' THEN '38' WHEN '35' THEN '39' WHEN '36' THEN '40'
				WHEN '37' THEN '41' WHEN '38' THEN '42' WHEN '39' THEN '43' WHEN '40' THEN '44' WHEN '41' THEN '45' WHEN '42' THEN '46'
				WHEN '43' THEN '47' WHEN '44' THEN '48' WHEN '45' THEN '49' WHEN '46' THEN '50' WHEN '47' THEN '51' WHEN '48' THEN '51'
				WHEN '49' THEN '53' WHEN '50' THEN '54' WHEN '51' THEN '54' WHEN '52' THEN '56' WHEN '53' THEN '57' WHEN '54' THEN '58'
				WHEN '55' THEN '59' WHEN '56' THEN '60' WHEN '57' THEN '61' WHEN '58' THEN '62' WHEN '59' THEN '63' WHEN '60' THEN '64'
				WHEN '61' THEN '65' WHEN '62' THEN '66' WHEN '63' THEN '67' WHEN '64' THEN '68' WHEN '65' THEN '69' WHEN '66' THEN '70'
				WHEN '67' THEN '71' WHEN '68' THEN '72' WHEN '69' THEN '73' WHEN '70' THEN '74' WHEN '71' THEN '75' WHEN '78' THEN '77'
				WHEN '79' THEN '78' WHEN '80' THEN '79' WHEN '81' THEN '29' WHEN '85' THEN '85' WHEN '86' THEN '86' END AS 'AGENIDENT1',

				CASE AGENIDENT2 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3' WHEN '4' THEN '4' WHEN '5' THEN '5' WHEN '6' THEN '6'
				WHEN '7' THEN '7' WHEN '8' THEN '8' WHEN '9' THEN '9' WHEN '10' THEN '11' WHEN '11' THEN '12' WHEN '12' THEN '13'
				WHEN '13' THEN '14' WHEN '14' THEN '15' WHEN '15' THEN '16' WHEN '16' THEN '17' WHEN '17' THEN '18' WHEN '18' THEN '19'
				WHEN '19' THEN '20' WHEN '20' THEN '21' WHEN '21' THEN '22' WHEN '22' THEN '24' WHEN '23' THEN '25' WHEN '24' THEN '26'
				WHEN '25' THEN '27' WHEN '26' THEN '28' WHEN '27' THEN '30' WHEN '28' THEN '32' WHEN '29' THEN '33' WHEN '30' THEN '34'
				WHEN '31' THEN '35' WHEN '32' THEN '36' WHEN '33' THEN '37' WHEN '34' THEN '38' WHEN '35' THEN '39' WHEN '36' THEN '40'
				WHEN '37' THEN '41' WHEN '38' THEN '42' WHEN '39' THEN '43' WHEN '40' THEN '44' WHEN '41' THEN '45' WHEN '42' THEN '46'
				WHEN '43' THEN '47' WHEN '44' THEN '48' WHEN '45' THEN '49' WHEN '46' THEN '50' WHEN '47' THEN '51' WHEN '48' THEN '51'
				WHEN '49' THEN '53' WHEN '50' THEN '54' WHEN '51' THEN '54' WHEN '52' THEN '56' WHEN '53' THEN '57' WHEN '54' THEN '58'
				WHEN '55' THEN '59' WHEN '56' THEN '60' WHEN '57' THEN '61' WHEN '58' THEN '62' WHEN '59' THEN '63' WHEN '60' THEN '64'
				WHEN '61' THEN '65' WHEN '62' THEN '66' WHEN '63' THEN '67' WHEN '64' THEN '68' WHEN '65' THEN '69' WHEN '66' THEN '70'
				WHEN '67' THEN '71' WHEN '68' THEN '72' WHEN '69' THEN '73' WHEN '70' THEN '74' WHEN '71' THEN '75' WHEN '78' THEN '77'
				WHEN '79' THEN '78' WHEN '80' THEN '79' WHEN '81' THEN '29' WHEN '85' THEN '85' WHEN '86' THEN '86' END AS 'AGENIDENT2',
	
				CASE AGENIDENT3 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3' WHEN '4' THEN '4' WHEN '5' THEN '5' WHEN '6' THEN '6'
				WHEN '7' THEN '7' WHEN '8' THEN '8' WHEN '9' THEN '9' WHEN '10' THEN '11' WHEN '11' THEN '12' WHEN '12' THEN '13'
				WHEN '13' THEN '14' WHEN '14' THEN '15' WHEN '15' THEN '16' WHEN '16' THEN '17' WHEN '17' THEN '18' WHEN '18' THEN '19'
				WHEN '19' THEN '20' WHEN '20' THEN '21' WHEN '21' THEN '22' WHEN '22' THEN '24' WHEN '23' THEN '25' WHEN '24' THEN '26'
				WHEN '25' THEN '27' WHEN '26' THEN '28' WHEN '27' THEN '30' WHEN '28' THEN '32' WHEN '29' THEN '33' WHEN '30' THEN '34'
				WHEN '31' THEN '35' WHEN '32' THEN '36' WHEN '33' THEN '37' WHEN '34' THEN '38' WHEN '35' THEN '39' WHEN '36' THEN '40'
				WHEN '37' THEN '41' WHEN '38' THEN '42' WHEN '39' THEN '43' WHEN '40' THEN '44' WHEN '41' THEN '45' WHEN '42' THEN '46'
				WHEN '43' THEN '47' WHEN '44' THEN '48' WHEN '45' THEN '49' WHEN '46' THEN '50' WHEN '47' THEN '51' WHEN '48' THEN '51'
				WHEN '49' THEN '53' WHEN '50' THEN '54' WHEN '51' THEN '54' WHEN '52' THEN '56' WHEN '53' THEN '57' WHEN '54' THEN '58'
				WHEN '55' THEN '59' WHEN '56' THEN '60' WHEN '57' THEN '61' WHEN '58' THEN '62' WHEN '59' THEN '63' WHEN '60' THEN '64'
				WHEN '61' THEN '65' WHEN '62' THEN '66' WHEN '63' THEN '67' WHEN '64' THEN '68' WHEN '65' THEN '69' WHEN '66' THEN '70'
				WHEN '67' THEN '71' WHEN '68' THEN '72' WHEN '69' THEN '73' WHEN '70' THEN '74' WHEN '71' THEN '75' WHEN '78' THEN '77'
				WHEN '79' THEN '78' WHEN '80' THEN '79' WHEN '81' THEN '29' WHEN '85' THEN '85' WHEN '86' THEN '86' END AS 'AGENIDENT3',

				CASE AGENIDENT4 WHEN '1' THEN '1' WHEN '2' THEN '2' WHEN '3' THEN '3' WHEN '4' THEN '4' WHEN '5' THEN '5' WHEN '6' THEN '6'
				WHEN '7' THEN '7' WHEN '8' THEN '8' WHEN '9' THEN '9' WHEN '10' THEN '11' WHEN '11' THEN '12' WHEN '12' THEN '13'
				WHEN '13' THEN '14' WHEN '14' THEN '15' WHEN '15' THEN '16' WHEN '16' THEN '17' WHEN '17' THEN '18' WHEN '18' THEN '19'
				WHEN '19' THEN '20' WHEN '20' THEN '21' WHEN '21' THEN '22' WHEN '22' THEN '24' WHEN '23' THEN '25' WHEN '24' THEN '26'
				WHEN '25' THEN '27' WHEN '26' THEN '28' WHEN '27' THEN '30' WHEN '28' THEN '32' WHEN '29' THEN '33' WHEN '30' THEN '34'
				WHEN '31' THEN '35' WHEN '32' THEN '36' WHEN '33' THEN '37' WHEN '34' THEN '38' WHEN '35' THEN '39' WHEN '36' THEN '40'
				WHEN '37' THEN '41' WHEN '38' THEN '42' WHEN '39' THEN '43' WHEN '40' THEN '44' WHEN '41' THEN '45' WHEN '42' THEN '46'
				WHEN '43' THEN '47' WHEN '44' THEN '48' WHEN '45' THEN '49' WHEN '46' THEN '50' WHEN '47' THEN '51' WHEN '48' THEN '51'
				WHEN '49' THEN '53' WHEN '50' THEN '54' WHEN '51' THEN '54' WHEN '52' THEN '56' WHEN '53' THEN '57' WHEN '54' THEN '58'
				WHEN '55' THEN '59' WHEN '56' THEN '60' WHEN '57' THEN '61' WHEN '58' THEN '62' WHEN '59' THEN '63' WHEN '60' THEN '64'
				WHEN '61' THEN '65' WHEN '62' THEN '66' WHEN '63' THEN '67' WHEN '64' THEN '68' WHEN '65' THEN '69' WHEN '66' THEN '70'
				WHEN '67' THEN '71' WHEN '68' THEN '72' WHEN '69' THEN '73' WHEN '70' THEN '74' WHEN '71' THEN '75' WHEN '78' THEN '77'
				WHEN '79' THEN '78' WHEN '80' THEN '79' WHEN '81' THEN '29' WHEN '85' THEN '85' WHEN '86' THEN '86' END AS 'AGENIDENT4',

				VERSION AS 'VERSION', 
				JSON AS 'JSON'

			From 
				HCFICHA355 
	
			Where 
				IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha SIVIGILA código 355 (Enfermedad Transmitida por Alimentos - ETA) a partir del identificador interno de ficha y su número de ficha. Consulta la tabla HCFICHA355 y transforma los campos de síntomas (náuseas, vómito, diarrea, fiebre, calambres, cefalea, deshidratación, cianosis, mialgias, artralgias, mareo, lesiones, escalofríos, parestesias, sialorrea, espasmos, entre otros) de valores numéricos a marcas visuales (''X'') para su impresión en el formulario oficial. También devuelve los nombres de los alimentos sospechosos (hasta 9 alimentos), los lugares de consumo con sus respectivas horas de ingesta, los agentes causales identificados con su codificación oficial SIVIGILA, e indicadores sobre casos asociados, captura, relación de exposición y recolección de muestras. Se usa para generar e imprimir la ficha de notificación obligatoria ETA ante el sistema de vigilancia epidemiológica SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila355';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y formatea para presentación los datos de una ficha de notificación Sivigila (síntomas, alimentos/lugares de consumo, horas, banderas y agentes identificados recodificados) a partir de su identificador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en HCFICHA355 con IDFICHANOTIFICACION igual al identificador suministrado para que se devuelvan filas; Los campos de síntomas y banderas deben almacenarse como ''0''/''1'' para que la conversión a ''X'' funcione; Los campos de hora deben ser convertibles al tipo TIME', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros cuya ficha de notificación coincida con el identificador recibido; Las marcas de síntoma se normalizan a ''X'' o NULL (presentación tipo formulario), nunca exponen el valor crudo ''0''/''1''; Los nombres y lugares de consumo se devuelven sin espacios a la derecha (RTRIM); Las horas se devuelven exclusivamente como TIME, descartando la parte de fecha; Los códigos de agente identificado se traducen a una codificación oficial diferente a la almacenada (recodificación de catálogo); El procedimiento es de solo lectura (no modifica datos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Síntomas clínicos (náuseas, vómito, diarrea, fiebre, cefalea, cianosis, mialgias, artralgias, parestesias, sialorrea, espasmos, escalofrío); Brote alimentario (alimentos consumidos, lugares de consumo, horas de consumo); Caso asociado / caso captado; Relación de exposición; Recolección de muestras; Agente identificado (catálogo Sivigila); Versión de ficha; Payload JSON de la ficha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: WHERE IDFICHANOTIFICACION = @IdFicha → devuelve un conjunto de resultados con los campos de la ficha transformados (síntomas como ''X'', textos con RTRIM, horas como TIME, banderas sí/no, agentes recodificados, VERSION y JSON)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campos de síntomas (NAUSEAS, VOMITO, DIARREA, FIEBRE, etc.) = ''1'' → Se devuelve ''X'' como marca de presencia del síntoma else Se devuelve NULL; si Campos booleanos (CASOASOC, CASOCAPT, RELAEXPO, RECOMUES) = ''1'' o ''0'' → Se generan dos columnas separadas (sí/no) marcadas con ''X'' según el valor else NULL en ambas; si AGENIDENT1..4 con valor entre ''1'' y ''86'' → Se mapea/recodifica al código equivalente del catálogo destino (p.ej. ''10''→''11'', ''22''→''24'', ''81''→''29'') else NULL si el valor no está en el mapeo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA355', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila355';
-- GO
