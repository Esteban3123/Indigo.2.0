

CREATE PROCEDURE [dbo].[SPREP_HC_RecienNacido_CabeceraHistoria]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10),
@NumeroFolio nchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
     SELECT A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION,
       INDICAMED AS 'INDICACIONES MEDICAS', D.MEDIFIRMA AS 'FIRMA PROFESIONAL', D.NOMMEDICO AS 'NOMBRE MEDICO',D.TARJETAPR AS 'TARJETA PROFESIONAL',RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',
       J.FECHISPAC  AS 'FECHA DE ATENCION INICIAL', RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL',
       COALESCE(NULLIF(I.IAUTORIZA,''),'') AS 'NUMERO DE AUTORIZACION DE INGRESO',I.ITIPORIES AS 'TIPO DE RIESGO', dbo.DestinoPaciente(J.INDICAPAC) AS 'DESTINODELPACIENTE' 
       FROM HCRECINAC  A WITH(NOLOCK)
       INNER JOIN ADcenaten B WITH(NOLOCK) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C WITH(NOLOCK) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPROFSAL D WITH(NOLOCK) ON A.CODPROSAL=D.CODPROSAL 
	   INNER JOIN INESPECIA E WITH(NOLOCK) ON D.CODESPEC1=E.CODESPECI 
	   INNER JOIN ADINGRESO I WITH(NOLOCK) ON A.NUMINGRES=I.NUMINGRES 
	   INNER JOIN HCHISPACA J WITH(NOLOCK) ON A.NUMINGRES = J.NUMINGRES AND A.IPCODPACI= J.IPCODPACI  AND A.NUMEFOLIO = J.NUMEFOLIO
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el encabezado o cabecera de la historia clínica de un recién nacido para un paciente, ingreso y folio específicos. Consolida información del episodio de atención neonatal cruzando el registro clínico del recién nacido (HCRECINAC) con el centro de atención (sede), la unidad funcional (sala o servicio), el profesional de la salud responsable junto con su firma, nombre y tarjeta profesional, la especialidad médica, los datos del ingreso (número de autorización y tipo de riesgo) y el folio de historia clínica (fecha de atención inicial, indicaciones médicas y destino del paciente). Se utiliza para imprimir o visualizar el encabezado oficial de la historia clínica neonatal en reportes médicos y documentos de egreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la cabecera de la historia clínica de un recién nacido, integrando datos de ubicación, profesional tratante, especialidad, ingreso y atención inicial para un paciente, ingreso y folio específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros relacionados en HCRECINAC, ADcenaten, INUNIFUNC, INPROFSAL, INESPECIA, ADINGRESO y HCHISPACA para la combinación paciente/ingreso/folio (los INNER JOIN obligan presencia en todas).; El profesional asociado al recién nacido debe tener una especialidad principal (CODESPEC1) registrada en INESPECIA.; La función escalar dbo.DestinoPaciente debe estar disponible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se filtra siempre por la triada paciente + ingreso + folio garantizando unicidad del contexto clínico.; El número de autorización nunca se devuelve como NULL: se normaliza a cadena vacía mediante COALESCE/NULLIF.; Se usa NOLOCK en todas las tablas, asumiendo lectura no bloqueante (puede leer datos no confirmados).; El destino del paciente se resuelve siempre vía la función dbo.DestinoPaciente sobre INDICAPAC de la atención inicial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recién nacido; Historia clínica; Ingreso hospitalario; Folio de atención; Centro de atención; Unidad funcional; Profesional de la salud; Tarjeta profesional; Especialidad médica; Indicaciones médicas; Autorización de ingreso; Tipo de riesgo; Destino del paciente; Atención inicial', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRECINAC: Cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros, retorna una fila con datos consolidados de cabecera del recién nacido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.IAUTORIZA es NULL o cadena vacía → Se retorna cadena vacía como número de autorización de ingreso else Se retorna el valor de I.IAUTORIZA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_RecienNacido_CabeceraHistoria';
-- GO
