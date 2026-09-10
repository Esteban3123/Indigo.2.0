

CREATE PROCEDURE [dbo].[SP_HC_ListarHistoricoOtrasHistorias]
(
@INDPaciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

Select c.ID, C.IPCODPACI AS 'Identificacion',C.FECHAREGISTRO AS 'Fecha Cargue',C.NOMBREARCHIVO AS 'Nombre Archivo',RTRIM(C.OBSERVACION) AS 'Observacion',B.DESCATEGO as 'Tipo Documento',
	   C.USUARIOSUBE AS 'Usuario',C.CODCENATE AS 'Centro Atencion'
 From HCDOCHISTORIASCLINICAS C
		LEFT JOIN HCCATDOCU B ON C.TIPODOCUMENTO = B.CODCATEGO 
 Where IPCODPACI = @INDPaciente

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de documentos clínicos adjuntos a la historia clínica de un paciente específico, identificado por su cédula o código de paciente. Combina los archivos cargados en la historia clínica (HCDOCHISTORIASCLINICAS) con el catálogo de categorías de documentos (HCCATDOCU) para mostrar el tipo o categoría legible de cada documento. Devuelve información como la fecha de cargue, el nombre del archivo, observaciones, el usuario que subió el documento y el centro de atención correspondiente. Se usa para consultar en la historia clínica del paciente todos los documentos externos o de otras historias que han sido adjuntados, como imágenes, PDFs o formularios clínicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los documentos de historia clínica cargados para un paciente, incluyendo metadatos del archivo y la descripción del tipo de documento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador del paciente debe corresponder al campo IPCODPACI usado en HCDOCHISTORIASCLINICAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Aunque no exista coincidencia en HCCATDOCU, el documento se devuelve igual (LEFT JOIN garantiza no perder filas por tipo de documento inexistente).; La observación se devuelve sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Historia clínica; Documento clínico; Categoría/Tipo de documento; Centro de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCDOCHISTORIASCLINICAS: Devuelve los documentos clínicos del paciente filtrando por IPCODPACI = parámetro recibido, enriquecidos con la descripción de la categoría documental.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDOCHISTORIASCLINICAS; dbo.HCCATDOCU', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHistoricoOtrasHistorias';
-- GO
