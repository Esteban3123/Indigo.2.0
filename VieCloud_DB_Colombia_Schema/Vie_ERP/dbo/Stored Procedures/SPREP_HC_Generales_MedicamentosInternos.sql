

CREATE PROCEDURE [dbo].[SPREP_HC_Generales_MedicamentosInternos]
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
CASE WHEN FOLIOINIC=NUMEFOLIO THEN 'N' WHEN TRATMODIF='1' THEN 'M' ELSE '' END AS LEYENDA,CASE DURACIDOS WHEN 'Fija' THEN CAST(VALDURFIJ AS CHAR) + ' ' + CASE UNIDURFIJ WHEN '1' THEN 'Minutos' WHEN '2' THEN 'Horas' WHEN '3' THEN 'Dias' WHEN '4' THEN 'Semana(s)' WHEN '5' THEN 'Meses' WHEN '6' THEN 'Año(s)' END ELSE DURACIDOS END AS 'DURACION DE LA DOSIS',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
                     
FROM HCPRESCDI A WITH(NOLOCK)
INNER JOIN IHLISTPRO B WITH(NOLOCK) ON A.CODPRODUC=B.CODPRODUC 
INNER JOIN HCVIAADMI D WITH(NOLOCK) ON A.CODVIAADM=D.CODVIAADM 
LEFT OUTER JOIN INUNIMEDI C WITH(NOLOCK) ON A.CODUNIMFN=C.CODUNIMED 
WHERE IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND MANEXTPRO=@ManejoExterno AND IDETIPHIS<>'CODIGOAZU'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el detalle de los medicamentos prescritos internamente para un paciente en un folio específico de un ingreso hospitalario. Compone la información de prescripción desde HCPRESCDI (ítems del folio) con el catálogo de productos de IHLISTPRO para obtener la descripción del medicamento, la vía de administración desde HCVIAADMI y las unidades de medida desde INUNIMEDI. Construye dinámicamente el texto de administración (dosis, frecuencia, unidad de tiempo, vía) según si el médico usó forma libre o estructurada, e indica si el medicamento es nuevo en el folio o fue modificado respecto a un folio anterior. Se utiliza para imprimir o visualizar la prescripción de medicamentos internos en la historia clínica del paciente durante la hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado de medicamentos prescritos (internos o externos) de un folio de historia clínica de un paciente, con datos de dosis, vía, frecuencia, duración e indicador de prescripción nueva o modificada, para reportes clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente, ingreso y folio de historia clínica en HCPRESCDI; Los productos prescritos deben estar registrados en el catálogo IHLISTPRO; La vía de administración debe existir en HCVIAADMI; Se debe indicar si las prescripciones son de manejo externo o no', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye prescripciones cuyo tipo de historia sea ''CODIGOAZU''; Filtra siempre por la combinación paciente + ingreso + folio + indicador de manejo externo; Solo retorna prescripciones que tengan producto válido en el catálogo y vía de administración registrada (INNER JOIN obligatorios); La unidad de medida es opcional (LEFT JOIN), no impide listar la prescripción; Identifica visualmente prescripciones nuevas (''N'') vs modificadas (''M'') según folio inicial y bandera de modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; folio de historia clínica; prescripción de medicamentos; vía de administración; dosis; frecuencia de administración; duración del tratamiento; manejo externo de medicamentos; tratamiento modificado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPRESCDI: Cuando IPCODPACI=@CodigoPaciente AND NUMINGRES=@NumeroIngreso AND NUMEFOLIO=@NumeroFolio AND MANEXTPRO=@ManejoExterno AND IDETIPHIS<>''CODIGOAZU'' → retorna las prescripciones de medicamentos con su descripción, cantidad, indicaciones, administración formateada, leyenda (N/M), duración y centro/UFU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FORMAPRESCRIBE IS NOT NULL o DOSISPRFN IS NULL → La administración se muestra solo con la descripción de la vía/forma de administración (DESADMINI) else Se arma una cadena de administración con dosis, unidad de medida y vía; si DURACIDOS = ''Dosis Unica'' → Se formatea la administración como ''X UNIDAD Dosis Unica Via: ...'' else Se formatea como ''X UNIDAD Cada N min/Hora/Día Via: ...'' según UNIFRECUE (1=min, 2=hora, 3=día); si FOLIOINIC = NUMEFOLIO → Marca la prescripción con leyenda ''N'' (Nueva) else Si TRATMODIF=''1'' marca con ''M'' (Modificada); en caso contrario sin leyenda; si DURACIDOS = ''Fija'' → La duración se formatea con VALDURFIJ y UNIDURFIJ traducido (1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años) else Se muestra el valor textual de DURACIDOS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCDI; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_MedicamentosInternos';
-- GO
