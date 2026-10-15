CREATE PROCEDURE [dbo].[SPCH_ListarHistoricoEscalasPaciente]
(
@Paciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
		SELECT A.FECHAREGISTRO AS FECHA, A.TIPOESCALA,  
			CASE A.TIPOESCALA
			WHEN 1 THEN 'CAGE' 
			WHEN 2 THEN 'APGAR Familiar'
			WHEN 3 THEN 'EDPS'
			WHEN 4 THEN 'Biopsicosocial'
			WHEN 5 THEN 'Tamizaje Violencia Domestica'
			WHEN 6 THEN 'Riesgo Framingham'
			WHEN 7 THEN 'Morisky'
			WHEN 8 THEN 'Test FINDRISC'
			WHEN 9 THEN 'Test MiniMental'
			WHEN 10 THEN 'Test Dependencia Nicotina'
			WHEN 11 THEN 'Tanner Desarrollo Mamario Mujer'
			WHEN 12 THEN 'Tanner Desarrollo Vello Pubiano Mujer'
			WHEN 13 THEN 'Tanner Desarrollo Genital Hombre'
			WHEN 14 THEN 'Tanner Desarrollo Vello Pubiano Hombre'
			WHEN 15 THEN 'Wagner'
			WHEN 16 THEN 'Modificada Disnea'
			WHEN 17 THEN 'CAT COPD Assessment Test'
			WHEN 18 THEN 'Exacerbaciones'
			WHEN 19 THEN 'Clasificacion EPOC'
			WHEN 20 THEN 'Test Goodenough'
			WHEN 21 THEN 'GOLD EPOC'
			WHEN 22 THEN 'Abreviada Desarrollo'
			WHEN 23 THEN 'TISS 28'
			WHEN 24 THEN 'Braden'
			WHEN 25 THEN 'ApacheII'
			WHEN 26 THEN 'Karnosfky'
			WHEN 27 THEN 'Ecog'
			WHEN 28 THEN 'Nems'
			WHEN 29 THEN 'Glasgow Mayor 5 Anos'
			WHEN 30 THEN 'Glasgow de 1 a 5 Anos'
			WHEN 31 THEN 'Glasgow Menor 1 Ano'
			WHEN 32 THEN 'SOFA'
			WHEN 33 THEN 'Charlson'
			WHEN 34 THEN 'SAPS3'
			WHEN 35 THEN 'Barthel'
			WHEN 36 THEN 'Morse'
			WHEN 37 THEN 'Macdems'
			WHEN 38 THEN 'NSRAS'
			WHEN 39 THEN 'MSTS'
			WHEN 40 THEN 'Person'
			WHEN 41 THEN 'beck'
			WHEN 42 THEN 'Zarit'
			WHEN 43 THEN 'RQC'
			WHEN 44 THEN 'Bacteriana Silness'
			WHEN 45 THEN 'VALE'
			WHEN 46 THEN 'RASS'
			WHEN 47 THEN 'Escala Downton - Adaptada'
			WHEN 50 THEN 'Nutricion'
			WHEN 51 THEN 'SQR'
			WHEN 52 THEN 'M CHAT'
			WHEN 53 THEN 'WHOOLEY' 
			WHEN 54 THEN 'AUDIT'
			WHEN 55 THEN 'LindaFried'
			WHEN 56 THEN 'Lawton Brody'
			WHEN 57 THEN 'GAD'
			WHEN 58 THEN 'MNA'
			WHEN 59 THEN 'MNA Simplificada'
			WHEN 60 THEN 'Assist'
			WHEN 61 THEN 'News'
			WHEN 62 THEN 'CHA2DS2 VASc'
			WHEN 63 THEN 'CRUSADE'
			WHEN 64 THEN 'HAS BLED'
			WHEN 65 THEN 'HEMORR2HAGES'
			WHEN 66 THEN 'EUROS CORE II'
			WHEN 67 THEN 'NYHA'
			WHEN 68 THEN 'KILLIP'
			WHEN 69 THEN 'PADUA'
			WHEN 70 THEN 'CAPRINI'
			WHEN 71 THEN 'MUST'
			WHEN 72 THEN 'STRONG KIDS'
			WHEN 73 THEN 'VGSDEN' 
			WHEN 74 THEN 'TIMI CEST'
			WHEN 75 THEN 'WELLS TVP'
			WHEN 76 THEN 'WELLS TEP'
			WHEN 77 THEN 'NPC'
			WHEN 78 THEN 'GRACE'
			WHEN 79 THEN 'TIMI SEST'
			WHEN 80 THEN 'ANTHONISEN'
			WHEN 81 THEN 'DAS 28'
			WHEN 82 THEN 'MRS'
			WHEN 83 THEN 'HAQ'
			WHEN 84 THEN 'ASPECT'
			WHEN 85 THEN 'Abreviada Desarrollo V3'
			WHEN 86 THEN 'Indice OLeary'
			WHEN 87 THEN 'NIHSS'
			WHEN 88 THEN 'Humpty Dumpty' 
			WHEN 89 THEN 'Riesgo Enfermedades Potencial Transmisibles'
			WHEN 90 THEN 'PIPP R'
			WHEN 91 THEN 'FLACC'
			WHEN 92 THEN 'OFRAS'
			WHEN 93 THEN 'FPSR'
			WHEN 94 THEN 'Escala (NRS) numérica del dolor'
			WHEN 95 THEN 'Escala de valoración sociofamiliar de Gijón modificada'
			WHEN 96 THEN 'Escala Cam (Confusion Assessment Method)' 
			WHEN 97 THEN 'Escala de valoración del riesgo de infección'
			WHEN 98 THEN 'Escala de valoración del riesgo farmacológico' 
			WHEN 99 THEN 'Escala de valoración riesgo psicosocial (conducta suicida)'
			WHEN 100 THEN 'Escala de valoración sociofamiliar de Gijón original' 
			WHEN 101 THEN 'Escala obstétrica de alerta temprana' 
			WHEN 102 THEN 'Escala MPEWS - Sistema de alerta temprana pediátrica modificado' 
			WHEN 103 THEN 'Escala BPEWS - sistema de slerta temprana pediátrica al lado de la cama' 
			WHEN 104 THEN 'Escala eventos tromboembólicos venosos durante la gestación, parto y puerperio' 
			WHEN 105 THEN 'Escala de Bishop'
			WHEN 106 THEN 'Escala Finnegan'
			WHEN 107 THEN 'Escala de clasificación de choque y evaluación de la respuesta'
			WHEN 108 THEN 'Cuestionario para búsqueda de casos sospechosos de EPOC'
			WHEN 109 THEN 'Escala GAD-2 (Generalized Anxiety Disorder Scale-2)'
			WHEN 110 THEN 'Escala de estratificación de riesgo cardiovascular de la organización mundial de la salud (OMS)'
			WHEN 111 THEN 'Escala medición grado de tabaquismo (índice paquete año)'
			WHEN 112 THEN 'Escala de Silverman - Anderson (Dificultad respiratoria neonatal)'
			WHEN 113 THEN 'Escala Downton'
			WHEN 118 THEN 'Escala de evaluación del riesgo de preeclampsia'
			WHEN 119 THEN 'Escala APGAR Familiar para uso en niños'
			END AS ESCALA, 
			CONVERT(BIT, 0) As Seleccion, 
			dbo.InterpretacionEscala(A.ID,A.TIPOESCALA,A.RESULTADO,A.RESULTADODECIMAL,B.IPSEXOPAC) AS RESULTADO, 
			A.CODPROSAL AS PROFESIONAL
		FROM HCESCALAS AS A
		INNER JOIN INPACIENT B ON B.IPCODPACI = @Paciente
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND A.TIPOESCALA NOT IN (48,49)
UNION
		--Se consulta escala DOWTOWN
		SELECT 
			A.FECREGSIS AS FECHA, 
			A.TIPOESCALA,  			
			'Escala Downton - Adaptada' as ESCALA,
			 Convert(BIT, 0) As Seleccion, 
			 dbo.InterpretacionEscala(A.[AUTO],A.TIPOESCALA,A.RESULTADO,A.RESULTADO,B.IPSEXOPAC) AS RESULTADO,
			 A.CODPROSAL AS PROFESIONAL
		FROM HCESCDOWN AS A
		INNER JOIN INPACIENT B ON B.IPCODPACI = @Paciente
		WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso 
	    AND NOT EXISTS(SELECT * FROM HCESCALAS A INNER JOIN HCESCDOWN B ON A.TIPOESCALA = B.TIPOESCALA WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso)

UNION
--Se consulta escala NORTON
		
		SELECT 
		b.FECREGSIS AS FECHA, 
		48 AS TIPOESCALA,
		'Escala Norton' AS ESCALA,
		 CONVERT(BIT, 0) As Seleccion, 
		 CASE 
                      WHEN ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD <= 9 THEN CONCAT((ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD), ' - ', 'Riesgo muy alto')
                      WHEN ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD >= 10 AND ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD <= 12 THEN CONCAT((ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD), ' - ', 'Riesgo alto')
                      WHEN ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD >= 13 AND ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD <= 14 THEN CONCAT((ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD), ' - ','Riesgo medio')
                      WHEN ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD > 14 THEN CONCAT((ESTMENTAL + MOVILIDAD  + ESTFISGEN + INCTINENCI + ACTIVIDAD), ' - ', 'Riesgo bajo')
                   END AS 'RESULTADO',
		 b.CODPROSAL AS PROFESIONAL
		FROM 
		  HCESCNTON B 
		  INNER JOIN INUNIFUNC C ON b.UFUCODIGO = C.UFUCODIGO 
		  INNER JOIN INPROFSAL D ON b.CODPROSAL = D.CODPROSAL 
		  INNER JOIN INESPECIA E ON D.CODESPEC1 = E.CODESPECI 
		WHERE 
		  b.NUMINGRES = @Ingreso
		  and b.IPCODPACI = @Paciente
	

UNION 
		--Se consulta escala VAS(
		SELECT 
		A.FECREGSIS AS FECHA, 
		49 AS TIPOESCALA,
		'VAS' AS ESCALA,
		 CONVERT(BIT, 0) As Seleccion, 
		 CONCAT(
			'Dolor - ', CASE CODTIPDOL WHEN 0 THEN 'Sordo' WHEN 1 THEN 'Punzante / Cortante' WHEN 2 THEN 'Palpitante' WHEN 3 THEN 'Quemante' WHEN 4 THEN 'Hormigueo, Burbujeo' WHEN 5 THEN 'Opresivo' WHEN 6 THEN 'Adormecido' WHEN 7 THEN 'Calambres' WHEN 8 THEN 'Dolorido' END, 
			' - ', CASE CODPARCUE WHEN 0 THEN 'Cabeza Anterior' WHEN 1 THEN 'Cuello Anterior' WHEN 2 THEN 'Miembro Superior Anterior Derecho' WHEN 3 THEN 'Torax' WHEN 4 THEN 'Miembro Superior Anterior Izquierdo' WHEN 5 THEN 'Abdomen' WHEN 6 THEN 'Pelvis' WHEN 7 THEN 'Miembro Inferior Anterior Derecho' WHEN 8 THEN 'Miembro Inferior Anterior Izquierdo' WHEN 9 THEN 'Cabeza Posterior' WHEN 10 THEN 'Cuello Posterior' WHEN 11 THEN 'Miembro Superior Posterior Izquierdo' WHEN 12 THEN 'Region Dorsal' WHEN 13 THEN 'Miembro Superior Posterior Derecho' WHEN 14 THEN 'Region Lumbar' WHEN 15 THEN 'Region Glutea' WHEN 16 THEN 'Miembro Inferior Posterior Izquierdo' WHEN 17 THEN 'Miembro Inferior Posterior Derecho' END, 
			+ ' - Reposo ', PTSESTREP, + ' - Movimiento ', 
			PTSESTMOV
		  ) AS RESULTADO,
		  A.CODPROSAL AS PROFESIONAL
		FROM 
		  HCESCVASD B 
		  INNER JOIN HCESCVASC A ON B.CODCONSEC = A.CODCONSEC 
		  INNER JOIN INUNIFUNC C ON A.UFUCODIGO = C.UFUCODIGO 
		  INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL 
		  INNER JOIN INESPECIA E ON D.CODESPEC1 = E.CODESPECI 
		WHERE 
		  NUMINGRES = @Ingreso
		  and A.IPCODPACI = @Paciente 
		ORDER BY FECHA DESC
END