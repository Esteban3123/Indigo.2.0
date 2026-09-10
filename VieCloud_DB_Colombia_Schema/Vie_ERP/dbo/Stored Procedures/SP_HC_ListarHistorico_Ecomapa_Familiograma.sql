
CREATE  PROCEDURE [dbo].[SP_HC_ListarHistorico_Ecomapa_Familiograma]
(
@TipoDiagrama Integer,
@Paciente varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

Select A.ID,A.NUMINGRES as 'Ingreso', A.IPCODPACI AS 'Paciente',Rtrim(B.IPNOMCOMP) As 'Nombre Paciente',A.FECHACREACION as 'Fecha Creacion', A.FECHAMODIFICO As 'Fecha Modificacion',Rtrim(A.USUARIOCREACION) + '-' + Rtrim(C.NOMUSUARI) AS 'Usuario Creacion',
	   Rtrim(A.USUARIOMODIFICO) + '-' + Rtrim(D.NOMUSUARI) AS 'Usuario Modificacion',DIAGRAMA
From HCDIAGRAMAS A 
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
		INNER JOIN SEGusuaru C ON A.USUARIOCREACION = C.CODUSUARI
		LEFT JOIN SEGusuaru D ON A.USUARIOMODIFICO = D.CODUSUARI
Where A. IPCODPACI = @Paciente AND A.TIPODIAGRAMA = @TipoDiagrama

End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de ecomapas y familiogramas registrados en la historia clínica de un paciente específico, filtrando por cédula del paciente y tipo de diagrama. Combina la información del diagrama clínico (HCDIAGRAMAS) con los datos del paciente (nombre completo desde INPACIENT) y los usuarios que crearon o modificaron cada registro (desde SEGusuaru). Devuelve el número de ingreso, fechas de creación y modificación, identificación del usuario responsable y el contenido gráfico del diagrama, permitiendo auditar y consultar la evolución familiar y social documentada en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar el histórico de diagramas clínicos (ecomapa o familiograma) registrados para un paciente, mostrando datos de creación/modificación y los usuarios responsables.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente en INPACIENT para que aparezcan registros.; El usuario de creación del diagrama debe existir en SEGusuaru.; Se debe indicar el tipo de diagrama y el código de paciente para acotar la búsqueda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan diagramas asociados a un paciente existente en INPACIENT (INNER JOIN obliga existencia).; Solo se retornan diagramas cuyo usuario creador exista en SEGusuaru (INNER JOIN); el usuario modificador puede no existir o estar vacío (LEFT JOIN).; El resultado se filtra simultáneamente por paciente y por tipo de diagrama, garantizando que cada consulta es específica al tipo (ecomapa o familiograma) solicitado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia Clínica; Ecomapa; Familiograma; Diagrama clínico; Ingreso; Usuario de creación/modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCDIAGRAMAS: Cuando IPCODPACI coincide con el paciente y TIPODIAGRAMA coincide con el tipo solicitado, retorna las filas de HCDIAGRAMAS enriquecidas con nombre del paciente y nombres de usuarios de creación y modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDIAGRAMAS; dbo.INPACIENT; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistorico_Ecomapa_Familiograma';
-- GO
