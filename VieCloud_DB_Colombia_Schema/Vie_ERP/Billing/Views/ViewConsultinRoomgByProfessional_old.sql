

CREATE VIEW [Billing].[ViewConsultinRoomgByProfessional_old]
AS
	SELECT ROW_NUMBER() OVER (ORDER BY A.CODIGOCON)  AS Id,
			RTRIM(A.CODIGOCON) AS ConsultingRoomCode, 
			RTRIM(F.DESCRICON) AS ConsultingRoomName,
			F.CODCENATE AS ConsultingRoomCenterAttentionCode,
			CAST(A.FECHORAIN AS DATE) AS AppointmentDate,
			A.CODCENATE AS AppointmentCenterAttentionCode,
			A.CODPROSAL as ProfessionalCode
	FROM dbo.AGASICITA A  
  	  INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL  
  	  INNER JOIN dbo.AGCONSULT F ON A.CODIGOCON=F.CODIGOCON  
	GROUP BY A.CODIGOCON, F.DESCRICON,F.CODCENATE,A.CODCENATE,
				A.CODPROSAL,CAST(A.FECHORAIN AS DATE)
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los consultorios (tipos de consulta) utilizados por cada profesional de la salud en sus citas agendadas, agrupados por fecha de atención y centro de atención. Cruza las citas registradas en AGASICITA con el maestro de profesionales INPROFSAL y el catálogo de tipos de consulta AGCONSULT, para obtener el código y nombre del consultorio, el centro de atención al que pertenece, la fecha de la cita y el código del profesional. Sirve como base de reportería de facturación para identificar qué consultorios o modalidades de consulta ha utilizado cada médico o profesional en un período dado, útil para auditoría de agenda, control de capacidad y liquidación de servicios ambulatorios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewConsultinRoomgByProfessional_old';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewConsultinRoomgByProfessional_old';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista (versión antigua) que lista consultorios distintos utilizados por profesionales en una fecha de cita, combinando información del consultorio y del centro de atención.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de citas en dbo.AGASICITA con profesional asociado en dbo.INPROFSAL y consultorio en dbo.AGCONSULT (los INNER JOIN excluyen registros sin coincidencia).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen citas cuyo profesional existe en INPROFSAL y cuyo consultorio existe en AGCONSULT (INNER JOIN).; La fecha/hora de la cita (FECHORAIN) se trunca a DATE, por lo que múltiples citas en el mismo día se consolidan en una sola fila por consultorio/profesional.; Los códigos de consultorio y nombre se entregan sin espacios a la derecha (RTRIM).; El Id se asigna secuencialmente por ROW_NUMBER ordenado por código de consultorio, no es estable entre ejecuciones si cambian los datos.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cita médica; profesional de la salud; consultorio; centro de atención; fecha de cita', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewConsultinRoomgByProfessional_old: Devuelve filas únicas (GROUP BY) por combinación de consultorio, centro de atención del consultorio, centro de atención de la cita, profesional y fecha (truncada a DATE) de la cita.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.INPROFSAL; dbo.AGCONSULT', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewConsultinRoomgByProfessional_old';
GO
