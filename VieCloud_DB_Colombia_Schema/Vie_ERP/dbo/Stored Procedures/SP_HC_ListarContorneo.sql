CREATE PROCEDURE [dbo].[SP_HC_ListarContorneo]
(
  @Identificacion as varchar(25),
  @IdordenRadio as integer
)

AS
BEGIN
  SET NOCOUNT ON;
 
		select S.OBSERVACION AS 'OBSERVACIONCONTORNEO', P.NOMCENATE AS 'CENTATENCION', FECHACONTORNEO, O.USUARIOSIMULA AS 'DOCUSUARIO', Q.NOMMEDICO AS 'USUARIOCONTORNEO', Q.MEDIFIRMA AS 'FIRMAUSU'
		from HCRADCONTORNEO S 
		inner join HCRADORDEN O on S.IDHCRADORDEN = O.ID 
		INNER JOIN ADCENATEN P ON S.CODCENATECONTORNEO = P.CODCENATE
		--INNER JOIN INPROFSAL Q ON O.USUARIOCONTORNEO = Q.CODPROSAL
		LEFT JOIN INPROFSAL Q ON O.USUARIOCONTORNEO = Q.CODUSUARI
		where O.IPCODPACI  = @Identificacion AND S.IDHCRADORDEN = @IdordenRadio
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial de contra-órdenes (contorneos) asociadas a una orden de radiología o imagen diagnóstica específica de un paciente. Recibe como parámetros la cédula o identificación del paciente y el identificador de la orden de radiología, y retorna las observaciones del contorneo, el centro de atención donde se gestionó, la fecha del contorneo, y los datos del profesional de la salud que lo realizó (nombre y firma). Combina información de las tablas de contra-órdenes (HCRADCONTORNEO), órdenes de radiología (HCRADORDEN), centros de atención (ADCENATEN) y profesionales de salud (INPROFSAL) para ofrecer una vista consolidada del proceso de anulación o reversión de una orden médica de imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarContorneo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarContorneo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la información de contorneo (observación, centro de atención, fecha, usuario simulador, médico que realizó el contorneo y su firma) asociada a una orden de radioterapia de un paciente específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La orden de radioterapia debe existir en HCRADORDEN y estar relacionada al paciente indicado; Debe existir registro de contorneo en HCRADCONTORNEO ligado a la orden; El centro de atención del contorneo debe estar registrado en ADCENATEN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan contorneos pertenecientes al paciente y orden de radioterapia indicados; El centro de atención del contorneo siempre debe existir en el catálogo ADCENATEN (INNER JOIN obligatorio); El usuario de contorneo se resuelve contra el código de usuario (CODUSUARI), no contra el código de profesional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Contorneo de radioterapia; Orden de radioterapia; Centro de atención; Paciente; Profesional de salud / médico; Usuario simulador; Firma médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRADCONTORNEO: Cuando existe contorneo cuya orden pertenece al paciente identificado y coincide con la orden recibida, se retorna observación, centro, fecha, usuario simulador, médico de contorneo y firma', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si LEFT JOIN sobre INPROFSAL por USUARIOCONTORNEO = CODUSUARI → Si no existe profesional asociado al usuario de contorneo, igualmente se devuelve la fila con nombre de médico y firma en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADCONTORNEO; dbo.HCRADORDEN; dbo.ADCENATEN; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarContorneo';
-- GO
