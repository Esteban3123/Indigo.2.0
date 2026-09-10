

CREATE PROCEDURE [dbo].[SPHC_ListarProfesionalesParaEnviarSMS]
AS
BEGIN
	SET NOCOUNT ON;
      
        SELECT A.IPCODPACI,A.NUMINGRES,A.NUMEFOLIO,A.CODSERIPS,D.UFUDESCRI,A.CODESPECI,F.DESESPECI,I.DESCCAMAS,C.IPSEXOPAC,
	 C.IPFECNACI, A.CODPROENV,A.CODPROENV AS MedicoPreferente,PRISERIPS,A.CODCENATE
     FROM      dbo.HCORDINTE A 
	 INNER JOIN dbo.INPACIENT C ON A.IPCODPACI=C.IPCODPACI 
	 INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO 
	 INNER JOIN dbo.INESPECIA F ON A.CODESPECI=F.CODESPECI
	 INNER JOIN dbo.ADINGRESO H ON A.NUMINGRES = H.NUMINGRES
	 LEFT OUTER JOIN dbo.CHCAMASHO I ON H.CODCAMACT = I.CODICAMAS
     WHERE MSGPENENV='True' AND ESTSERIPS='1'
     
     
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los profesionales de salud a quienes se debe enviar un SMS de notificación pendiente, basándose en órdenes médicas internas que tienen mensaje pendiente de envío y estado activo. Combina información de la orden médica (servicio CUPS, especialidad, folio, centro de atención) con datos del paciente (sexo, fecha de nacimiento, cédula), la unidad funcional donde se generó la orden, la especialidad médica correspondiente y la cama hospitalaria asignada al ingreso. Es el punto de partida del flujo de notificaciones SMS a profesionales de salud sobre órdenes médicas pendientes de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado de órdenes/servicios con mensaje SMS pendiente de envío y estado activo, junto con los datos del paciente, ingreso, especialidad, unidad funcional, cama y profesional asociado, para notificar a los profesionales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas maestras de pacientes, unidades funcionales, especialidades e ingresos deben tener los registros referenciados por la orden, de lo contrario la orden no aparece.; Las órdenes deben tener marcado el indicador de mensaje pendiente por enviar y estado de servicio igual a ''1''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con marca de mensaje pendiente de envío (MSGPENENV=''True'').; Solo se consideran órdenes con estado de servicio activo/vigente (ESTSERIPS=''1'').; Cada orden listada debe estar asociada a un paciente, una unidad funcional, una especialidad y un ingreso existentes (INNER JOIN obligatorio).; La cama del ingreso es opcional: si el ingreso no tiene cama asignada, la orden igualmente se incluye (LEFT JOIN sobre camas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; orden de servicio; especialidad médica; unidad funcional; cama hospitalaria; médico que envía / médico preferente; envío de SMS pendientes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDINTE: Cuando MSGPENENV=''True'' y ESTSERIPS=''1'', retorna un conjunto de resultados con datos de la orden, paciente, ingreso, especialidad, unidad funcional, cama y profesional para envío de SMS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDINTE; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INESPECIA; dbo.ADINGRESO; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarProfesionalesParaEnviarSMS';
-- GO
