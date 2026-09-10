CREATE PROCEDURE [dbo].[SPREP_CH_ListarRecienNacidos] (@CodigoPaciente Varchar(25), @NumeroFolio Char(10), @NumeroIngreso nChar(10)) AS BEGIN -- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

DECLARE @IdNino AS varchar(25) IF NOT EXISTS
  (SELECT IPCODPACI
   FROM dbo.HCRECINAC
   WHERE IPCODPACI = @CodigoPaciente
     AND NUMEFOLIO = @NumeroFolio) BEGIN
SET @IdNino =
  (SELECT IPCODPACIHIJO
   FROM dbo.HCRECINAC
   WHERE IPCODPACIHIJO = @CodigoPaciente) END ELSE BEGIN
SET @IdNino = NULL END -- Insert statements for procedure here

SELECT NUMCONSEC AS CONSECUTIVO,
       NUMHIJREG AS 'NUMERO RECIEN NACIDO',
       FECHANACIM AS 'FECHA DE NACIMIENTO',
       VITANACIM AS 'VITALIDAD AL NACER',
       RECPERCEF AS 'PERIMETRO CEFALICO',
       RECPERTOR AS 'PERIMETRO TORAXICO',
       RECPERABD AS 'PERIMETRO ABDOMINAL',
       CASE
           WHEN SEXRECNAC='1' THEN 'Masculino'
           WHEN SEXRECNAC='3' THEN 'Ambiguo'
           ELSE 'Femenino'
       END AS SEXO,
       PESORECNA AS PESO,
       TALLARECI AS TALLA,
       concat(trim(FRECARREC), '  latidos/min') AS 'FRECUENCIA CARDIACA',
       concat(trim(FRERESREC), '  respiraciones/min') AS 'FRECUENCIA RESPIRATORIA',
       TEMPERREC AS TEMPERATURA,
       ADAPRECNA AS ADAPTACION,
       APGAR1REC AS 'APAGAR 1MINUTO',
       APGAR5REC AS 'APAGAR 5MINUTOS',
       APGAR10RN AS 'APAGAR 10MINUTOS',
       IIF(NUMSEMCAP IS NOT NULL, CONCAT(NUMSEMCAP, ' Semanas'), 'No evaluado') AS 'SEMANAS CAPURRO',
       DESPESXAG AS 'PESO XA E.G',
       CASE ANOCABREC
           WHEN 1 THEN 0
           WHEN 0 THEN 1
       END 'CHECK CABEZA Y CUELLO NORMAL',
           ANOCABREC AS 'CHECK CABEZA Y CUELLO ANOMALIA',
           DESCABREC AS 'DESCRIPCION CABEZA Y CUELLO',
           CASE ANOTORREC
               WHEN 1 THEN 0
               WHEN 0 THEN 1
           END 'CHECK TORAX NORMAL',
               ANOTORREC AS 'CHECK TORAX ANOMALIA',
               DESTORREC AS 'DESCRIPCION TORAX',
               CASE ANOABDREC
                   WHEN 1 THEN 0
                   WHEN 0 THEN 1
               END 'CHECK ABDOMEN NORMAL',
                   ANOABDREC AS 'CHECK ABDOMEN ANOMALIA',
                   DESABDREC AS 'DESCRIPCION ABDOMEN',
                   CASE ANOGEUREC
                       WHEN 1 THEN 0
                       WHEN 0 THEN 1
                   END 'CHECK GENITOURINARIO NORMAL',
                       ANOGEUREC AS 'CHECK GENITOURINARIO ANOMALIA',
                       DESGEUREC AS 'DESCRIPCION GENITOURINARIO',
                       CASE ANOCADREC
                           WHEN 1 THEN 0
                           WHEN 0 THEN 1
                       END 'CHECK CADERA NORMAL',
                           ANOCADREC AS 'CHECK CADERA ANOMALIA',
                           DESCADREC AS 'DESCRIPCION CADERA',
                           CASE ANOEXTREC
                               WHEN 1 THEN 0
                               WHEN 0 THEN 1
                           END AS 'CHECK EXTREMIDADES NORMAL',
                           ANOEXTREC AS 'CHECK EXTREMIDADES ANOMALIA',
                           DESEXTREC AS 'DESCRIPCION EXTREMIDADES',
                           CASE ANONEUREC
                               WHEN 1 THEN 0
                               WHEN 0 THEN 1
                           END 'CHECK NEUROLOGICA NORMAL',
                               ANONEUREC AS 'CHECK NEUROLOGICA ANOMALIA',
                               DESNEUREC AS 'DESCRIPCION NEUROLOGICA',
                               DESRECNAC AS 'DESCRIPCION RECIEN NACIDO',
                               PATOLOGIA,
                               OBSERVACI AS OBSERVACIONES,
                               EDADGESNAC AS 'EDAD GESTACIONAL AL NACER',
                               IIF(BALLARD IS NOT NULL, Concat(BALLARD, ' Semanas'), 'No evaluado') AS BALLARD,
                               FECMUEPAC AS FechaMuerte,
                               A.CODCAUMUE AS CodCausaMuerte,
                               B.DESCAUMUE AS CausaMuerte,
                               NUMCERDEF AS NumCertificadoDefuncion,
                               CASE WHEN(PESORECNA <> 0
                                         AND TALLARECI <> 0) THEN (CASE
                                                                       WHEN DATEDIFF(YEAR, FECHANACIM, FECHISPAC) >= 18 THEN Sqrt(((CAST(PESORECNA AS DECIMAL) / 1000) * CAST(TALLARECI AS DECIMAL)) / 3600)
                                                                       WHEN DATEDIFF(YEAR, FECHANACIM, FECHISPAC) < 18 THEN (CASE
                                                                                                                                 WHEN PESORECNA < 10000 THEN ((((CAST(PESORECNA AS DECIMAL) / 1000) * 4) + 9) / 100)
                                                                                                                                 WHEN PESORECNA >= 10000 THEN ((((CAST(PESORECNA AS DECIMAL) / 1000) * 4) + 7) / ((CAST(PESORECNA AS DECIMAL) / 1000) + 90))
                                                                                                                             END)
                                                                   END)
                               END AS 'SUPERFICIE CORPORAL TOTAL',					    
							   BloodPressure AS 'TENSION ARTERIAL',
							   case A.BirthWeightClassification
								   WHEN 1 THEN 'Alto peso al nacer'
								   WHEN 2 THEN 'Peso normal al nacer'
								   WHEN 3 THEN 'Peso bajo al nacer'
								   WHEN 4 THEN 'Muy bajo peso al nacer'
								   WHEN 5 THEN 'Extremado bajo peso al nacer'
							   END AS 'CLASIFICACION PESO AL NACER',
							   CASE A.CordClamping 
								   WHEN 1 THEN 'Habitual'
								   WHEN 2 THEN 'Precoz'
								   WHEN 3 THEN 'Temprano'
								   WHEN 4 THEN 'Tardío'
							   END AS 'PINZAMIENTO CORDON',
							   A.CordClamping AS 'PINZAMIENTO',
							   A.CordClampingReason AS 'POR QUE PINZAMIENTO PRECOZ',
							   CASE A.SkinToSkinContact 
								   WHEN 1 THEN 'Si'
								   WHEN 2 THEN 'No'
								   WHEN 3 THEN 'No aplica'
							   END AS 'CONTACTO PIEL PIEL',
							   CASE A.SkinToSkinContactBreastfeeding 
								   WHEN 0 THEN 'No'
								   WHEN 1 THEN 'Si'
							   END AS 'LACTANCIA PIEL PIEL',
							   iif(A.SkinToSkinContactBreastfeeding = 1, 'No aplica descripción', A.WhyNotSkinToSkinBreastfeeding) AS 'NO LACTANCIA PIEL PIEL',
							   iif(A.NeonatalAdaptationObservations IS NULL, 'Sin registrar', A.NeonatalAdaptationObservations) AS 'ADAPTACION NEONATAL',
							   iif(A.NewbornCareSequence IS NULL, 'Sin registrar', A.NewbornCareSequence) AS 'DESCRIPCION SECUENCIA ATENCION',
							   A.NUMINGRESHIJO AS 'INGRESO HIJO',
							   A.IPCODPACIHIJO AS 'CODIGO HIJO',
							   IIF(A.Meconium = 1, 'Si', 'No') AS Meconio
							   
FROM HCRECINAC A WITH(NOLOCK)
LEFT OUTER JOIN INCAUMUER B WITH(NOLOCK) ON B.CODCAUMUE = A.CODCAUMUE
WHERE (@IdNino IS NULL
       AND A.NUMEFOLIO = @NumeroFolio
       AND A.IPCODPACI = @CodigoPaciente
       AND A.NUMINGRES = @NumeroIngreso)
  OR (@IdNino IS NOT NULL
      AND A.IPCODPACIHIJO = @IdNino) 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista el registro clínico completo de recién nacidos a partir del código de la madre (o del propio neonato) y un número de folio. Recupera todos los datos del nacimiento registrados en la historia clínica neonatal (tabla HCRECINAC): signos vitales al nacer (peso, talla, temperatura, frecuencia cardíaca y respiratoria), puntuaciones APGAR al minuto 1, 5 y 10, perímetros cefálico, torácico y abdominal, sexo, vitalidad, edad gestacional (Capurro y Ballard), clasificación del peso al nacer, exploración física por segmentos corporales (cabeza, tórax, abdomen, genitourinario, cadera, extremidades, neurológico), superficie corporal total, tensión arterial, pinzamiento de cordón umbilical, patología, observaciones y, en caso de fallecimiento, fecha de muerte, código y descripción de la causa de muerte cruzado con el catálogo INCAUMUER, y número de certificado de defunción. Se usa en reportería clínica y perinatal para consultar el estado del neonato al momento del parto y su evolución inmediata.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_ListarRecienNacidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la ficha clínica completa del recién nacido (datos antropométricos, signos vitales, APGAR, anomalías por sistema, adaptación neonatal, causa de muerte y superficie corporal calculada), localizándola por la madre (folio + ingreso) o directamente por el código del hijo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de paciente corresponde a la madre (con folio e ingreso válidos en HCRECINAC) o al hijo (existente en HCRECINAC.IPCODPACIHIJO); Para el cálculo de superficie corporal se requiere que peso y talla sean distintos de cero y que existan fecha de nacimiento y fecha histórica del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La codificación de sexo mapea ''1''=Masculino, ''3''=Ambiguo y cualquier otro valor=Femenino; Los flags de anomalía por sistema son binarios invertidos: si ANO*=1 entonces ''NORMAL''=0 y viceversa; Las semanas Capurro y Ballard nulas se reportan como ''No evaluado''; Las observaciones de adaptación neonatal y secuencia de atención nulas se reportan como ''Sin registrar''; Clasificación de peso al nacer codificada 1=Alto, 2=Normal, 3=Bajo, 4=Muy bajo, 5=Extremado bajo; Pinzamiento de cordón codificado 1=Habitual, 2=Precoz, 3=Temprano, 4=Tardío; Contacto piel a piel codificado 1=Si, 2=No, 3=No aplica; El procedimiento permite localizar el registro tanto por el código de la madre (folio+ingreso) como por el código del hijo; Las consultas usan WITH(NOLOCK), permitiendo lecturas sucias; La superficie corporal sólo se calcula si peso y talla son distintos de cero, aplicando fórmula adulta (>=18 años) o pediátrica (<18 años con dos rangos según peso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'recién nacido; paciente madre; paciente hijo; folio de atención; número de ingreso; vitalidad al nacer; perímetro cefálico/torácico/abdominal; sexo del recién nacido; peso y talla al nacer; frecuencia cardíaca y respiratoria; temperatura; adaptación neonatal; APGAR (1, 5 y 10 minutos); semanas Capurro; Ballard; edad gestacional; anomalías por sistema (cabeza/cuello, tórax, abdomen, genitourinario, cadera, extremidades, neurológica); patología; fecha y causa de muerte; certificado de defunción; superficie corporal; tensión arterial; clasificación del peso al nacer; pinzamiento de cordón; contacto piel a piel; lactancia piel a piel; secuencia de atención del recién nacido; meconio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando el código no corresponde a un registro madre con ese folio, se busca como hijo (IPCODPACIHIJO = código) y se devuelve su ficha; en caso contrario se devuelve filtrando por folio + código paciente + número de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en HCRECINAC con IPCODPACI = código recibido y NUMEFOLIO = folio recibido → Se considera que el código corresponde a la madre y se consulta usando folio + código paciente + ingreso (IdNino queda NULL) else Se asume que el código corresponde al hijo: IdNino se setea con IPCODPACIHIJO y la consulta se filtra por IPCODPACIHIJO = IdNino; si PESORECNA <> 0 y TALLARECI <> 0 → Se calcula la superficie corporal total; si la edad (años entre fecha nacimiento y fecha histórica) es >=18 usa fórmula Mosteller (sqrt(peso*talla/3600)); si <18 y peso<10000g usa ((peso*4)+9)/100; si <18 y peso>=10000g usa ((peso*4)+7)/(peso+90) else No se calcula superficie corporal (queda NULL); si SkinToSkinContactBreastfeeding = 1 → El campo ''NO LACTANCIA PIEL PIEL'' se reporta como ''No aplica descripción'' else Se muestra el texto registrado en WhyNotSkinToSkinBreastfeeding', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.INCAUMUER', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_ListarRecienNacidos';
-- GO
