CREATE PROCEDURE [dbo].[SPREP_HC_Generales_Medicamentos]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10),
@ManejoExterno Bit
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT A.PREESTADO,A.FECINIDOS,A.FECFINDOS,A.CODPRODUC as 'CODIGO DEL PRODUCTO',RTRIM(B.DESPRODUC) AS 'DESCRIPCION DEL PRODUCTO',CANPEDPRO AS 'CANTIDAD PEDIDA',INDAPLMED AS 'INDICACIONES DE APLICACION',
RTRIM(CASE WHEN FORMAPRESCRIBE IS NOT NULL THEN DESADMINI ELSE CASE WHEN DOSISPRFN IS NULL THEN DESADMINI WHEN DURACIDOS='Dosis Unica' THEN RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Dosis Unica Via: ' + RTRIM(DESVIAADM) ELSE RTRIM(CAST(DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(ABRUNIMED AS CHAR)) + ' Cada ' + RTRIM(CAST(FRECUENCI AS CHAR)) + CASE UNIFRECUE WHEN '1' THEN ' min(s) ' WHEN '2' THEN ' Hora(s) ' WHEN '3' THEN ' Dia(s) ' END + 'Via: ' + RTRIM(DESVIAADM) END END) AS ADMINISTRACION,
CASE WHEN FOLIOINIC=NUMEFOLIO THEN 'N' WHEN TRATMODIF='1' THEN 'M' ELSE '' END AS LEYENDA,CASE DURACIDOS WHEN 'Fija' THEN CAST(VALDURFIJ AS CHAR) + ' ' + CASE UNIDURFIJ WHEN '1' THEN 'Minutos' WHEN '2' THEN 'Horas' WHEN '3' THEN 'Dias' WHEN '4' THEN 'Semana(s)' WHEN '5' THEN 'Meses' WHEN '6' THEN 'Año(s)' END ELSE DURACIDOS END AS 'DURACION DE LA DOSIS',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI,E.DESDCIMED AS 'DCI'
FROM HCPRESCRD A  with(noLock)
INNER JOIN IHLISTPRO B with(noLock) ON A.CODPRODUC=B.CODPRODUC 
INNER JOIN IHDCIMEDI E with(noLock) ON B.CODDCIMED =E.CODDCIMED
INNER JOIN HCVIAADMI D with(noLock) ON A.CODVIAADM=D.CODVIAADM 
LEFT OUTER JOIN INUNIMEDI C with(noLock) ON A.CODUNIMFN=C.CODUNIMED 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND MANEXTPRO=@ManejoExterno AND IDETIPHIS<>'CODIGOAZU' and IDESQUEMAONC IS NULL

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el detalle completo de los medicamentos prescritos para un paciente en un ingreso y folio específicos, combinando la prescripción médica (HCPRESCRD) con el catálogo de productos farmacéuticos (IHLISTPRO), la denominación común internacional o DCI (IHDCIMEDI), la vía de administración (HCVIAADMI) y la unidad de medida de dosificación (INUNIMEDI). Construye la descripción legible de administración del medicamento indicando dosis, frecuencia, unidad de tiempo y vía (oral, intravenosa, intramuscular, etc.), diferenciando si es dosis única, dosis fija o esquema libre, y marcando si el medicamento es nuevo o fue modificado en el tratamiento. Se usa para imprimir o visualizar la hoja de medicamentos del paciente dentro de la historia clínica, filtrando por cédula del paciente, número de ingreso y número de folio, excluyendo productos de manejo externo y esquemas oncológicos especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_Medicamentos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve las prescripciones de medicamentos de un paciente para un folio/ingreso específico, formateando la posología, vía de administración y duración para visualización en historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente, ingreso y folio deben existir en HCPRESCRD; Los productos prescritos deben tener correspondencia en IHLISTPRO, IHDCIMEDI y HCVIAADMI; Se filtra por la bandera de manejo externo recibida', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye prescripciones de tipo ''CODIGOAZU''; Excluye prescripciones asociadas a esquemas oncológicos (IDESQUEMAONC IS NULL); Marca como ''N'' las prescripciones cuyo folio inicial coincide con el folio actual y como ''M'' las modificadas; No realiza modificaciones de datos, es de solo lectura (consulta con NOLOCK)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Prescripción médica; Medicamento; Dosis; Vía de administración; Frecuencia; Duración del tratamiento; DCI (Denominación Común Internacional); Folio de historia clínica; Ingreso hospitalario; Manejo externo de medicamentos; Esquema oncológico; Tratamiento modificado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRD: Retorna prescripciones del paciente/ingreso/folio donde MANEXTPRO coincide con el parámetro, IDETIPHIS<>''CODIGOAZU'' e IDESQUEMAONC IS NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FORMAPRESCRIBE IS NOT NULL → La administración se muestra como la descripción de la vía (DESADMINI) else Se construye la posología según DOSISPRFN, DURACIDOS y UNIFRECUE; si DOSISPRFN IS NULL (y FORMAPRESCRIBE NULL) → Administración = DESADMINI else Se arma cadena dosis+unidad+frecuencia+vía; si DURACIDOS = ''Dosis Unica'' → Se formatea como ''X UNIDAD Dosis Unica Via: ...'' else Se formatea como ''X UNIDAD Cada N min/Hora/Dia Via: ...''; si UNIFRECUE in (''1'',''2'',''3'') → Se traduce a min(s), Hora(s) o Dia(s) respectivamente; si FOLIOINIC = NUMEFOLIO → LEYENDA = ''N'' (nuevo) else Si TRATMODIF=''1'' LEYENDA=''M'' (modificado), sino vacío; si DURACIDOS = ''Fija'' → Duración = VALDURFIJ + unidad traducida desde UNIDURFIJ (Minutos/Horas/Dias/Semana(s)/Meses/Año(s)) else Duración = valor literal de DURACIDOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.IHLISTPRO; dbo.IHDCIMEDI; dbo.HCVIAADMI; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_Medicamentos';
-- GO
