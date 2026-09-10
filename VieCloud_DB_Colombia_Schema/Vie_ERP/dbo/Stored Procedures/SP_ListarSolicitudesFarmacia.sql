
CREATE PROCEDURE [dbo].[SP_ListarSolicitudesFarmacia]
(
  @Consecutivo as Int,
  @Producto as varchar(20),
  @Origen as Int
)

AS
BEGIN
	SET NOCOUNT ON;

	 IF @Origen = 1 --Medicamentos
		
			SELECT RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', CASE I.UFUTIPUNI WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Hospitalizacion' WHEN 3 THEN 'Apoyo Dx' WHEN 4 THEN 'Apoyo Terapeutico' WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' WHEN 12 THEN 'Unidad Renal' WHEN 13 THEN 'Unidad Oncologica' WHEN 14 THEN 'Unidad Medicina Nuclear' WHEN 15 THEN 'Consulta Externa' WHEN 16 THEN 'Unidad Mental' WHEN 17 THEN 'Unidad de Quemados' WHEN 18 THEN 'Unidad de Cuidado Paliativo' WHEN 19 THEN 'Cirugia' WHEN 20 THEN 'Laboratorio' WHEN 21 THEN 'Cardiologia No Invasiva' WHEN 22 THEN 'Cardiologia Invasiva' WHEN 23 THEN 'Gineco-Obstetricia' WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia' WHEN 30 THEN 'Otras' WHEN 31 THEN 'Consulta Prioritaria' WHEN 32 THEN 'Atención domiciliaria' WHEN 33 THEN 'Unidad Radioterapia' WHEN 34 THEN 'Unidad Braquiterapia' WHEN 35 THEN 'Hemodinamia' END AS 'TIPO UNIDAD',				
				A.CODCONCEC AS 'ID', B.FECHAORDE AS 'FECHA ORDEN', DATEADD(HOUR, 24, B.FECHAORDE) AS 'FECHA FIN', NULL AS 'OBSERVACION', A.IPCODPACI AS 'PACIENTE', RTRIM(A.CODPROSAL) AS 'PROFESIONAL', 0 AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente}' END AS 'TIPO PROFESIONAL', 1 AS 'COD TIPO PEDIDO', 'Medicamentos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', NULL AS 'OBSERVACION ITEM', C.INDAPLMED AS 'INDICACIONES', IIF(C.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(C.FRECUENCI, 0) AS 'DURACION', C.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE C.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', ISNULL(C.VALDURFIJ, 0)  AS 'POSOLOGIA', C.UNIDURFIJ AS 'COD UNIDAD POSOLOGIA', CASE C.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.AbbreviationName) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', C.CODVIAADM AS 'COD VIA ADMIN', RTRIM(G.DESVIAADM) AS 'VIA ADMIN', IIF(F.Multidose = 1, 1,0) AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO', A.FECINIDOS AS 'FECHA INICIO'
			FROM HCFARMEPD A
				INNER JOIN HCFARMEPC B ON A.CODCONCEC = B.CODCONCEC
				INNER JOIN HCPRESCRA C ON A.IdSourceTable= C.ID AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN HCVIAADMI G ON C.CODVIAADM = G.CODVIAADM
				INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.CODPRODUC = @Producto AND A.SourceTable = 'HCPRESCRA'

	ELSE IF @Origen = 2 --Mezclas

			SELECT RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', CASE I.UFUTIPUNI WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Hospitalizacion' WHEN 3 THEN 'Apoyo Dx' WHEN 4 THEN 'Apoyo Terapeutico' WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' WHEN 12 THEN 'Unidad Renal' WHEN 13 THEN 'Unidad Oncologica' WHEN 14 THEN 'Unidad Medicina Nuclear' WHEN 15 THEN 'Consulta Externa' WHEN 16 THEN 'Unidad Mental' WHEN 17 THEN 'Unidad de Quemados' WHEN 18 THEN 'Unidad de Cuidado Paliativo' WHEN 19 THEN 'Cirugia' WHEN 20 THEN 'Laboratorio' WHEN 21 THEN 'Cardiologia No Invasiva' WHEN 22 THEN 'Cardiologia Invasiva' WHEN 23 THEN 'Gineco-Obstetricia' WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia' WHEN 30 THEN 'Otras' WHEN 31 THEN 'Consulta Prioritaria' WHEN 32 THEN 'Atención domiciliaria' WHEN 33 THEN 'Unidad Radioterapia' WHEN 34 THEN 'Unidad Braquiterapia' WHEN 35 THEN 'Hemodinamia' END AS 'TIPO UNIDAD',				
				A.CODCONCEC AS 'ID', B.FECHAINIC AS 'FECHA ORDEN', DATEADD(HOUR, 24, B.FECHAINIC) AS 'FECHA FIN', NULL AS 'OBSERVACION', A.IPCODPACI AS 'PACIENTE', RTRIM(A.CODPROSAL) AS 'PROFESIONAL', B.TIPMEZLIQ AS 'COD TIPO MEZCLA', CASE B.TIPMEZLIQ WHEN 1 THEN 'Mezcla Continua' WHEN 3 THEN 'Mezcla Frecuencia' WHEN 4 THEN 'Mezcla Magistral' END AS 'TIPO MEZCLA', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES 	WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente}' END AS 'TIPO PROFESIONAL', A.TIPOREGIS AS 'COD TIPO PEDIDO', CASE A.TIPOREGIS WHEN 1 THEN 'Medicamento' WHEN 2 THEN 'Materiales e Insumos' END AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', NULL AS 'OBSERVACION ITEM', C.INDAPLMED AS 'INDICACIONES', IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', A.UNIDURFIJ AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', C.CODVIABOM AS 'COD VIA ADMIN', RTRIM(G.DESVIAADM) AS 'VIA ADMIN', F.Multidose AS 'MULTIDOSIS', CASE WHEN C.ESBOLMEDL = 1 THEN 1 ELSE CASE WHEN C.ESBOLOMEZ = 1 THEN 1 ELSE CASE WHEN C.ESBOLMEDM = 1 THEN 1 ELSE 0 END END END AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO', A.FECINIDOS AS 'FECHA INICIO'
			FROM HCFARMEPD A
				INNER JOIN HCINFLIQA B ON A.IdSourceTable = b.CONSECUTI
				LEFT JOIN HCINFLIQD C ON B.CODCONCEC = C.CODCONCEC AND C.CODPRODUC = A.CODPRODUC
				LEFT JOIN HCINFCONC E ON B.CODCONCEC = E.CODCONCEC AND E.CODPRODUC = A.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				LEFT JOIN HCVIAADMI G ON C.CODVIABOM = G.CODVIAADM
				LEFT JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.CODPRODUC = @Producto AND A.SourceTable = 'HCINFLIQA' AND B.TIPMEZLIQ <> 2

	ELSE IF @Origen = 3 -- Insumos

			SELECT RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', CASE I.UFUTIPUNI WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Hospitalizacion' WHEN 3 THEN 'Apoyo Dx' WHEN 4 THEN 'Apoyo Terapeutico' WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' WHEN 12 THEN 'Unidad Renal' WHEN 13 THEN 'Unidad Oncologica' WHEN 14 THEN 'Unidad Medicina Nuclear' WHEN 15 THEN 'Consulta Externa' WHEN 16 THEN 'Unidad Mental' WHEN 17 THEN 'Unidad de Quemados' WHEN 18 THEN 'Unidad de Cuidado Paliativo' WHEN 19 THEN 'Cirugia' WHEN 20 THEN 'Laboratorio' WHEN 21 THEN 'Cardiologia No Invasiva' WHEN 22 THEN 'Cardiologia Invasiva' WHEN 23 THEN 'Gineco-Obstetricia' WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia' WHEN 30 THEN 'Otras' WHEN 31 THEN 'Consulta Prioritaria' WHEN 32 THEN 'Atención domiciliaria' WHEN 33 THEN 'Unidad Radioterapia' WHEN 34 THEN 'Unidad Braquiterapia' WHEN 35 THEN 'Hemodinamia' END AS 'TIPO UNIDAD',				
				A.CODCONCEC AS 'ID', B.FECHAORDE AS 'FECHA ORDEN', DATEADD(HOUR, 24, B.FECHAORDE) AS 'FECHA FIN', A.JUSTIINSU AS 'OBSERVACION', A.IPCODPACI AS 'PACIENTE', RTRIM(A.CODPROSAL) AS 'PROFESIONAL', 0 AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente}' END AS 'TIPO PROFESIONAL', 3 AS 'COD TIPO PEDIDO', 'Insumos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', C.JUSTIINSU AS 'OBSERVACION ITEM', NULL AS 'INDICACIONES', IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', A.UNIDURFIJ AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', NULL AS 'COD VIA ADMIN', NULL AS 'VIA ADMIN', 0 AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', NULL AS 'UNIDAD MEDIDA',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO', A.FECINIDOS AS 'FECHA INICIO'
			FROM HCFARMEPD A
				INNER JOIN HCSOLINSC B ON A.IdSourceTable= B.ID 
				INNER JOIN HCSOLINSD C ON B.CODCONCEC = C.CODCONCEC AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL 
				INNER JOIN Inventory.InventoryProduct F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.CODPRODUC = @Producto AND A.SourceTable = 'HCSOLINSD'

	ELSE IF @Origen = 4 --Liquidos 

			SELECT RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', CASE I.UFUTIPUNI WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Hospitalizacion' WHEN 3 THEN 'Apoyo Dx' WHEN 4 THEN 'Apoyo Terapeutico' WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' WHEN 12 THEN 'Unidad Renal' WHEN 13 THEN 'Unidad Oncologica' WHEN 14 THEN 'Unidad Medicina Nuclear' WHEN 15 THEN 'Consulta Externa' WHEN 16 THEN 'Unidad Mental' WHEN 17 THEN 'Unidad de Quemados' WHEN 18 THEN 'Unidad de Cuidado Paliativo' WHEN 19 THEN 'Cirugia' WHEN 20 THEN 'Laboratorio' WHEN 21 THEN 'Cardiologia No Invasiva' WHEN 22 THEN 'Cardiologia Invasiva' WHEN 23 THEN 'Gineco-Obstetricia' WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia' WHEN 30 THEN 'Otras' WHEN 31 THEN 'Consulta Prioritaria' WHEN 32 THEN 'Atención domiciliaria' WHEN 33 THEN 'Unidad Radioterapia' WHEN 34 THEN 'Unidad Braquiterapia' WHEN 35 THEN 'Hemodinamia' END AS 'TIPO UNIDAD',				
				A.CODCONCEC AS 'ID', B.FECHAINIC AS 'FECHA ORDEN', DATEADD(HOUR, 24, B.FECHAINIC) AS 'FECHA FIN', NULL AS 'OBSERVACION', A.IPCODPACI AS 'PACIENTE', RTRIM(A.CODPROSAL) AS 'PROFESIONAL', B.TIPMEZLIQ AS 'COD TIPO MEZCLA', 'Liquido' AS 'TIPO MEZCLA', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES 	WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente}' END AS 'TIPO PROFESIONAL', A.TIPOREGIS AS 'COD TIPO PEDIDO', CASE A.TIPOREGIS WHEN 1 THEN 'Medicamento' WHEN 2 THEN 'Materiales e Insumos' END AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', NULL AS 'OBSERVACION ITEM', C.INDAPLMED AS 'INDICACIONES', IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', A.UNIDURFIJ AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', C.CODVIABOM AS 'COD VIA ADMIN', RTRIM(G.DESVIAADM) AS 'VIA ADMIN', F.Multidose AS 'MULTIDOSIS', C.ESBOLMEDL AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO', A.FECINIDOS AS 'FECHA INICIO'
			FROM HCFARMEPD A
				INNER JOIN HCINFLIQA B ON A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO 
				INNER JOIN HCINFLIQD C ON B.CODCONCEC_ORIGEN = C.CODCONCEC AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
				LEFT JOIN HCVIAADMI G ON C.CODVIABOM = G.CODVIAADM
				LEFT JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
			WHERE A.CODCONCEC = @Consecutivo AND A.CODPRODUC = @Producto AND B.TIPMEZLIQ = 2

	ELSE IF @Origen = 5 -- Nutriones Parenterales

			SELECT RTRIM(A.UFUCODIGO) AS 'COD UNIDAD FUNCIONAL', CASE I.UFUTIPUNI WHEN 1 THEN 'Urgencias' WHEN 2 THEN 'Hospitalizacion' WHEN 3 THEN 'Apoyo Dx' WHEN 4 THEN 'Apoyo Terapeutico' WHEN 5 THEN 'Unidades de Cuidado Intensivo Adulto' WHEN 6 THEN 'Unidades de Cuidado Intermedio Adulto' WHEN 7 THEN 'Unidades de Cuidado Intensivo Pediatrica' WHEN 8 THEN 'Unidades de Cuidado Intermedio Pediatrica' WHEN 9 THEN 'Unidades de Cuidado Intensivo Neonatal' WHEN 10 THEN 'Unidades de Cuidado Intermedio Neonatal' WHEN 11 THEN 'Unidades de Cuidado Basico Neonatal' WHEN 12 THEN 'Unidad Renal' WHEN 13 THEN 'Unidad Oncologica' WHEN 14 THEN 'Unidad Medicina Nuclear' WHEN 15 THEN 'Consulta Externa' WHEN 16 THEN 'Unidad Mental' WHEN 17 THEN 'Unidad de Quemados' WHEN 18 THEN 'Unidad de Cuidado Paliativo' WHEN 19 THEN 'Cirugia' WHEN 20 THEN 'Laboratorio' WHEN 21 THEN 'Cardiologia No Invasiva' WHEN 22 THEN 'Cardiologia Invasiva' WHEN 23 THEN 'Gineco-Obstetricia' WHEN 24 THEN 'Consulta Externa - Gineco-Obstetricia' WHEN 30 THEN 'Otras' WHEN 31 THEN 'Consulta Prioritaria' WHEN 32 THEN 'Atención domiciliaria' WHEN 33 THEN 'Unidad Radioterapia' WHEN 34 THEN 'Unidad Braquiterapia' WHEN 35 THEN 'Hemodinamia' END AS 'TIPO UNIDAD',				
				A.CODCONCEC AS 'ID', B.FECHAORDEN AS 'FECHA ORDEN', DATEADD(HOUR, 24, B.FECHAORDEN) AS 'FECHA FIN', NULL AS 'OBSERVACION', A.IPCODPACI AS 'PACIENTE', RTRIM(A.CODPROSAL) AS 'PROFESIONAL', 0 AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', D.TIPPROFES AS 'COD TIPO PROFESIONAL', 
				CASE D.TIPPROFES WHEN 1 THEN 'Medico general' WHEN 2 THEN 'Medico Especialista' WHEN 3 THEN 'Enfermera' WHEN 4 THEN 'Auxiliar Enfermeria' WHEN 5 THEN 'Odontologo General' WHEN 6 THEN 'Odontologo Especialista' WHEN 7 THEN 'Nutricionista'
				WHEN 8 THEN 'Higienista' WHEN 9 THEN 'Psicologo' WHEN 10 THEN 'Trabajadora Social' WHEN 11 THEN 'Promotor de Saneamiento' WHEN 12 THEN 'Ingeniero Sanitario' WHEN 13 THEN 'Medico Veterinario' WHEN 14 THEN 'Ingeniero Alimento' WHEN 15 THEN 'Auxiliar Bacteriologo'
				WHEN 16 THEN 'Terapeuta' WHEN 17 THEN 'Optometra' WHEN 18 THEN 'Quimico Farmaceutico' WHEN 19 THEN 'Radiologo' WHEN 20 THEN 'Tecnologo Radiologo' WHEN 21 THEN 'Instrumentador Qx' WHEN 22 THEN 'Auxiliar Patologia' WHEN 23 THEN 'Otros' WHEN 24 THEN 'Medico Interno'
				WHEN 25 THEN 'Bacteriologo(a)' WHEN 26 THEN 'Patólogo(a)' WHEN 27 THEN 'Médico residente}' END AS 'TIPO PROFESIONAL', 1 AS 'COD TIPO PEDIDO', 'Medicamentos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', NULL AS 'OBSERVACION ITEM', B.INDICACIONADI AS 'INDICACIONES', IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(B.TEMPOADMIN, 0) AS 'DURACION', 2 AS 'COD UNIDAD FRECUENCIA', 
				'Horas' AS 'UNIDAD FRECUENCIA', 0  AS 'POSOLOGIA', NULL AS 'COD UNIDAD POSOLOGIA', '' AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.AbbreviationName) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', B.VIADMIN AS 'COD VIA ADMIN', CASE B.VIADMIN WHEN 1 THEN 'Cateter Central' WHEN 2 THEN 'Cateter lateral' END AS 'VIA ADMIN', IIF(F.Multidose = 1, 1,0) AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',
				(SELECT TOP 1 TALLAPACI FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'TALLA', (SELECT TOP 1 PESOPACIE FROM HCEXFISIC WHERE IPCODPACI = A.IPCODPACI ORDER BY FECREGSIS DESC) AS 'PESO', A.FECINIDOS AS 'FECHA INICIO'
			FROM HCFARMEPD A
				INNER JOIN HCNUTPAREC B ON A.IdSourceTable = B.ID
				INNER JOIN HCNUTPAREND C ON B.ID= C.IDHCNUTPAREC AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.CODPRODUC = @Producto AND A.SourceTable = 'HCNUTPAREC'

	END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las solicitudes de despacho farmacéutico pendientes o activas para un producto y consecutivo de orden específicos, diferenciando entre dos tipos de pedido según el parámetro de origen: medicamentos individuales (origen 1) o mezclas intravenosas/magistrales (origen 2). Para cada solicitud retorna el detalle completo de la orden: unidad funcional y tipo de servicio (urgencias, hospitalización, UCI, consulta externa, etc.), paciente, profesional prescriptor y su tipo, producto farmacéutico con nombre, dosis, cantidad, vía de administración, unidad de medida, indicaciones, frecuencia, posología, talla y peso del paciente, y si el pedido es urgente o cancelado. Compone la información cruzando el encabezado de la orden farmacéutica (HCFARMEPC), el detalle del ítem (HCFARMEPD), la prescripción de historia clínica (HCPRESCRA), el catálogo de medicamentos ATC, el maestro de profesionales de salud (INPROFSAL), las vías de administración (HCVIAADMI), las unidades de medida (INUNIMEDI) y las unidades funcionales (INUNIFUNC). Es utilizado por el módulo de farmacia para alimentar la cola de preparación y despacho de medicamentos y mezclas ordenadas en historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarSolicitudesFarmacia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de una solicitud de farmacia (medicamento, mezcla, insumo, líquido o nutrición parenteral) para un consecutivo y producto dados, normalizando catálogos y calculando indicadores como urgencia, cancelación, dosis única y vigencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Origen debe ser 1, 2, 3, 4 o 5; cualquier otro valor no produce resultados.; Debe existir un registro en HCFARMEPD con CODCONCEC = @Consecutivo y CODPRODUC = @Producto.; El SourceTable del registro en HCFARMEPD debe coincidir con el origen indicado (HCPRESCRA, HCINFLIQA, HCSOLINSD o HCNUTPAREC).; El producto debe existir en el catálogo correspondiente: Inventory.ATC para medicamentos/mezclas/líquidos/nutriciones, Inventory.InventoryProduct para insumos.; El profesional (CODPROSAL) y la unidad funcional (UFUCODIGO) deben existir en INPROFSAL e INUNIFUNC respectivamente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre filtra por la combinación CODCONCEC = @Consecutivo y CODPRODUC = @Producto (un único ítem de pedido por ejecución).; El campo URGENTE solo es 1 cuando Stat = 1; CANCELADO solo es 1 cuando PROESTADO = 3.; DOSIS UNICA es 1 únicamente cuando DURACIDOS = ''Dosis Unica''.; FECHA FIN se calcula siempre como FECHA ORDEN + 24 horas.; TALLA y PESO se obtienen del último registro de HCEXFISIC del paciente (TOP 1 ORDER BY FECREGSIS DESC).; En Mezclas se excluyen siempre los registros con TIPMEZLIQ = 2 (líquidos), y en Líquidos solo se incluyen TIPMEZLIQ = 2.; Cada origen está asociado a un valor específico de A.SourceTable: HCPRESCRA, HCINFLIQA, HCSOLINSD o HCNUTPAREC.; Para Insumos no se devuelve vía de administración, unidad de medida ni indicaciones.; Para Nutrición Parenteral la duración se toma de TEMPOADMIN expresada siempre en horas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de farmacia; Medicamentos; Mezclas (continua, frecuencia, magistral); Líquidos endovenosos; Insumos médicos; Nutrición parenteral; Vía de administración; Posología y frecuencia; Dosis única; Bolo; Multidosis; Unidad funcional hospitalaria; Tipo de profesional de salud; Talla y peso del paciente; Orden urgente; Orden cancelada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen = 1 (Medicamentos) → Consulta detalle de prescripción de medicamentos uniendo HCFARMEPD con HCFARMEPC y HCPRESCRA, filtrando SourceTable=''HCPRESCRA''; tipo de pedido fijo ''Medicamentos''.; si Origen = 2 (Mezclas) → Consulta mezclas desde HCINFLIQA/HCINFLIQD con SourceTable=''HCINFLIQA'' y TIPMEZLIQ <> 2 (excluye líquidos); tipo de mezcla derivado de TIPMEZLIQ (1 Continua, 3 Frecuencia, 4 Magistral).; si Origen = 3 (Insumos) → Consulta solicitudes de insumos desde HCSOLINSC/HCSOLINSD con SourceTable=''HCSOLINSD''; tipo de pedido fijo ''Insumos'', sin vía de administración ni unidad de medida.; si Origen = 4 (Líquidos) → Consulta líquidos desde HCINFLIQA/HCINFLIQD filtrando TIPMEZLIQ = 2; el join con HCINFLIQA se hace por paciente, ingreso y folio en lugar de IdSourceTable.; si Origen = 5 (Nutriciones Parenterales) → Consulta nutriciones parenterales desde HCNUTPAREC/HCNUTPAREND con SourceTable=''HCNUTPAREC''; unidad de frecuencia fija ''Horas'' (código 2) y vía de administración limitada a Cateter Central/Lateral.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.HCPRESCRA; dbo.INPROFSAL; Inventory.ATC; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.INUNIFUNC; dbo.HCEXFISIC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.HCINFCONC; dbo.HCSOLINSC; dbo.HCSOLINSD; Inventory.InventoryProduct; dbo.HCNUTPAREC; dbo.HCNUTPAREND', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarSolicitudesFarmacia';
-- GO
