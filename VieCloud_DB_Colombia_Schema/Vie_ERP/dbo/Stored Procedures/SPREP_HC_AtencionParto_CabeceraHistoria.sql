CREATE PROCEDURE [dbo].[SPREP_HC_AtencionParto_CabeceraHistoria]
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
     SELECT A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION, ANALISISP AS ANALISIS,
       INDICAMED AS 'INDICACIONES MEDICAS', D.MEDIFIRMA AS 'FIRMA PROFESIONAL', D.NOMMEDICO AS 'NOMBRE MEDICO',D.TARJETAPR AS 'TARJETA PROFESIONAL',RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',
       FECINIATE AS 'FECHA DE ATENCION INICIAL', RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL',
       COALESCE(NULLIF(I.IAUTORIZA,''),'') AS 'NUMERO DE AUTORIZACION DE INGRESO',I.ITIPORIES AS 'TIPO DE RIESGO', dbo.DestinoPaciente(A.INDICAPAC) AS 'DESTINO DEL PACIENTE' ,PLAINDMED AS 'PLANTILLA RECOMENDACIONES'
       FROM HCATINPAR A with(nolock)
       INNER JOIN ADcenaten B with(nolock) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C with(nolock) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPROFSAL D with(nolock) ON A.CODPROSAL=D.CODPROSAL 
	   INNER JOIN INESPECIA E with(nolock) ON A.CODESPECI=E.CODESPECI 
	   INNER JOIN ADINGRESO I with(nolock) ON A.NUMINGRES=I.NUMINGRES
	   
	   	   
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el encabezado o carátula de la historia clínica de atención del parto para un paciente, ingreso y folio específicos. Consolida en un único resultado los datos del registro de parto (HCATINPAR) con la sede o centro de atención, la unidad funcional donde ocurrió el parto, el profesional de salud que lo atendió (nombre, firma y tarjeta profesional), la especialidad médica, y los datos del ingreso del paciente como número de autorización y tipo de riesgo. Se utiliza para imprimir o visualizar el encabezado del documento clínico del parto, incluyendo análisis, indicaciones médicas, destino del paciente y plantilla de recomendaciones, dentro de la historia clínica perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cabecera de la historia clínica de una atención de parto, integrando datos del ingreso, ubicación asistencial, profesional tratante, especialidad y autorización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de atención de parto que coincida con paciente, ingreso y folio indicados.; El ingreso, centro de atención, unidad funcional, profesional de salud y especialidad referenciados deben existir en sus respectivos catálogos (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan atenciones que tengan correspondencia completa en centro de atención, unidad funcional, profesional de salud, especialidad e ingreso (INNER JOIN).; El número de autorización siempre se devuelve como cadena no nula (vacía si no existe).; Las consultas se realizan con NOLOCK, por lo que no se aplican bloqueos sobre las tablas leídas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención de parto; Historia clínica; Ingreso hospitalario; Folio de atención; Unidad funcional; Centro de atención; Profesional de salud; Especialidad médica; Autorización de ingreso; Tipo de riesgo; Destino del paciente; Indicaciones médicas; Plantilla de recomendaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCATINPAR: Cuando coinciden paciente, número de ingreso y número de folio, se retorna una fila con la cabecera de la atención de parto enriquecida con ubicación, profesional, especialidad, autorización y destino del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Número de autorización de ingreso es NULL o cadena vacía → Se retorna cadena vacía como número de autorización else Se retorna el valor existente de la autorización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCATINPAR; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionParto_CabeceraHistoria';
-- GO
