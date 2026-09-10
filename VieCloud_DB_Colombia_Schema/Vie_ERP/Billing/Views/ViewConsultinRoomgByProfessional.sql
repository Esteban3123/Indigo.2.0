CREATE VIEW [Billing].[ViewConsultinRoomgByProfessional]
AS

	SELECT ROW_NUMBER() OVER (ORDER BY F.ID)  AS Id,
		RTRIM(F.CODIGOCON) AS ConsultingRoomCode, 
		RTRIM(F.DESCRICON) AS ConsultingRoomName,
		F.CODCENATE AS ConsultingRoomCenterAttentionCode,
		CAST(Common.GETDATE() AS DATE) AS AppointmentDate,
		F.CODCENATE AS AppointmentCenterAttentionCode
	FROM dbo.AGCONSULT F WITH(NOLOCK)
	WHERE F.ESTADOCON = 1 --Solo lista consultorios activos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los consultorios o tipos de consulta activos disponibles para agendamiento, obtenidos del catálogo AGCONSULT filtrando únicamente los que están habilitados (estado activo). Para cada consultorio expone su código, nombre descriptivo y el centro de atención al que pertenece, junto con la fecha actual del sistema como fecha de cita referencial. Esta vista es utilizada en el módulo de facturación y agendamiento para presentar al usuario los consultorios vigentes agrupables por profesional, centro de atención o fecha de turno.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewConsultinRoomgByProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewConsultinRoomgByProfessional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los consultorios activos disponibles para agendamiento por profesional, asociándolos a la fecha actual y a su centro de atención.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La tabla dbo.AGCONSULT debe contener registros con ESTADOCON poblado para distinguir activos (=1) de inactivos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone consultorios cuyo estado es activo (ESTADOCON = 1).; La fecha de cita expuesta siempre corresponde a la fecha actual del sistema (Common.GETDATE casteada a DATE).; El código y nombre del consultorio se entregan sin espacios a la derecha (RTRIM).; El centro de atención del consultorio se reutiliza como centro de atención de la cita.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'consultorio; centro de atención; agendamiento; fecha de cita', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.AGCONSULT: Devuelve únicamente filas de dbo.AGCONSULT donde ESTADOCON = 1 (consultorios activos), generando un Id secuencial con ROW_NUMBER ordenado por F.ID.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGCONSULT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional';
GO
