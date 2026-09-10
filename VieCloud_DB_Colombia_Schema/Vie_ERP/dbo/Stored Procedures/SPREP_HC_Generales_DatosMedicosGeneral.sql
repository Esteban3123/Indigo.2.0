
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_DatosMedicosGeneral]
(
@CodigoPaciente Varchar(25),
@NumeroFolio Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
SELECT RTRIM(NOMMEDICO) AS 'NOMBRE MEDICO',RTRIM(DESESPECI) AS 'DESCRIPCION DE LA ESPECIALIDAD',TARJETAPR AS 'TARJETA PROFESIONAL',MEDIFIRMA AS 'FIRMA PROFESIONAL'
FROM HCFIRMFOL A With(Nolock) 
INNER JOIN INPROFSAL AS B With(Nolock)  ON A.CODPROCRE=B.CODPROSAL 
INNER JOIN INESPECIA AS C With(Nolock)  ON B.CODESPEC1=C.CODESPECI

WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMEFOLIO=@NumeroFolio
                  
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene los datos del médico responsable de un folio específico de la historia clínica de un paciente. Dado el código del paciente y el número de folio, consulta quién firmó ese folio, cruzando el registro de firmas (HCFIRMFOL) con el maestro de profesionales de la salud (INPROFSAL) y el catálogo de especialidades (INESPECIA). Devuelve el nombre del médico, su especialidad, número de tarjeta profesional y firma digital, información utilizada para la impresión y validación legal de documentos clínicos como epicrisis, notas de evolución u órdenes médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera los datos del médico responsable (nombre, especialidad, tarjeta profesional y firma) asociado a un folio de historia clínica de un paciente específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el folio deben existir en la tabla de firmas de folio (HCFIRMFOL); El profesional creador del folio debe existir en el catálogo de profesionales de la salud (INPROFSAL); La especialidad principal del profesional debe existir en el catálogo de especialidades (INESPECIA)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna información del profesional creador del folio (CODPROCRE), no de otros firmantes; Filtra estrictamente por la combinación paciente + folio; Usa lecturas sin bloqueo (NOLOCK) en todas las tablas consultadas; Solo retorna registros cuando hay correspondencia válida entre folio, profesional y especialidad principal (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Folio de historia clínica; Firma de folio; Médico/Profesional de la salud; Especialidad médica; Tarjeta profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve nombre del médico, descripción de especialidad, tarjeta profesional y firma para el paciente y folio recibidos, uniendo HCFIRMFOL con INPROFSAL por CODPROCRE=CODPROSAL e INESPECIA por CODESPEC1=CODESPECI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFIRMFOL; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_DatosMedicosGeneral';
-- GO
