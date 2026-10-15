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