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