
CREATE PROCEDURE [dbo].[SPREP_HC_AtencionRecienNacido_CabeceraHistoria]
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
     SELECT TOP 1 A.NUMCONSEC , A.NUMINGRES AS INGRESO, A.NUMEFOLIO AS 'NUMERO FOLIO', RTRIM(B.nomcenate) + ' - ' + RTRIM(C.UFUDESCRI) AS UBICACION, 
        D.MEDIFIRMA AS 'FIRMA PROFESIONAL', D.NOMMEDICO AS 'NOMBRE MEDICO',D.TARJETAPR AS 'TARJETA PROFESIONAL',RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',
       A.FECHANACIM  AS 'FECHA DE NACIMIENTO', RTRIM(C.UFUDESCRI) AS 'DESCRIPCION UNIDAD FUNCIONAL',
       COALESCE(NULLIF(I.IAUTORIZA,''),'') AS 'NUMERO DE AUTORIZACION DE INGRESO',I.ITIPORIES AS 'TIPO DE RIESGO',
	   J.INDICAMED 'INDICACIONES MEDICAS', J.DATOBJETI 'ANALISIS',dbo.DestinoPaciente(J.INDICAPAC) AS 'DESTINO DEL PACIENTE', J.PLAINDMED AS 'PLANTILLA RECOMENDACIONES'
       FROM HCRECINAC  A WITH(NOLOCK)
       INNER JOIN ADcenaten B WITH(NOLOCK) ON A.CODCENATE=B.codcenate 
	   INNER JOIN INUNIFUNC C WITH(NOLOCK) ON A.UFUCODIGO=C.UFUCODIGO
	   INNER JOIN INPROFSAL D WITH(NOLOCK) ON A.CODPROSAL=D.CODPROSAL 
	   INNER JOIN INESPECIA E WITH(NOLOCK) ON A.CODESPECI=E.CODESPECI 
	   INNER JOIN ADINGRESO I WITH(NOLOCK) ON A.NUMINGRES=I.NUMINGRES 
	   INNER JOIN HCHISPACA J WITH(NOLOCK) ON A.IPCODPACI = J.IPCODPACI AND A.NUMEFOLIO = J.NUMEFOLIO 
	   	   
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO=@NumeroFolio

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el encabezado o cabecera de la historia clínica de atención al recién nacido para un paciente, ingreso y folio específicos. Consolida en un solo resultado los datos del episodio de atención (número de ingreso, folio y consecutivo), la ubicación del paciente (sede y unidad funcional), la información del profesional tratante (nombre, firma y tarjeta profesional), la especialidad médica, la fecha de nacimiento del recién nacido, el número de autorización y tipo de riesgo del ingreso, así como las indicaciones médicas, análisis, destino del paciente y recomendaciones registradas en la historia clínica. Se utiliza para encabezar e imprimir el documento clínico oficial de atención al recién nacido dentro del módulo de historia clínica del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la cabecera/encabezado de la historia clínica de atención del recién nacido, consolidando datos de ubicación, profesional, especialidad, ingreso e indicaciones médicas para un folio específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro de atención de recién nacido (HCRECINAC) que coincida con el paciente, número de ingreso y número de folio.; Deben existir registros relacionados en centro de atención, unidad funcional, profesional de salud, especialidad, ingreso administrativo e historia paciente para que el INNER JOIN devuelva resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna 1 registro por uso de TOP 1.; El número de autorización de ingreso nunca se devuelve NULL: si IAUTORIZA es vacío o NULL, se devuelve cadena vacía vía COALESCE(NULLIF(...),'''').; La ubicación se compone concatenando el nombre del centro de atención y la descripción de la unidad funcional separados por '' - ''.; El destino del paciente se obtiene siempre traducido vía la función dbo.DestinoPaciente sobre el código de indicación al paciente.; Usa NOLOCK en todas las tablas, por lo que admite lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Atención de recién nacido; Historia clínica; Folio de atención; Ingreso del paciente; Centro de atención; Unidad funcional; Profesional de salud; Tarjeta profesional; Firma profesional; Especialidad médica; Autorización de ingreso; Tipo de riesgo; Indicaciones médicas; Análisis (datos objetivos); Destino del paciente; Plantilla de recomendaciones', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRECINAC: Devuelve TOP 1 fila con datos consolidados cuando IPCODPACI, NUMINGRES y NUMEFOLIO coinciden con los parámetros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.DestinoPaciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRECINAC; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.ADINGRESO; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_AtencionRecienNacido_CabeceraHistoria';
-- GO
