
CREATE PROCEDURE [dbo].[SP_ONCO_ListarBraquiterapiasActividadesReciente]
(
@Identificacion Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

select
	A.ID,A.CODCENATE, 
			rtrim(F.CODPROSAL) as CODPROSAL,
			rtrim(A.CODSERIPS) as CODSERIPS,
			Rtrim(A.IPCODPACI) As Identificacion, A.FECHAORDEN,
			Rtrim(IPNOMCOMP) As NombrePaciente,
			Rtrim(E.CODSERIPS) +' - '+ Rtrim(DESSERIPS) As Procedimiento,
			Rtrim(CE.NOMCENATE ) as CentroAtencion,
			Rtrim(F.NOMMEDICO) as Profesional,
			Rtrim(D.CODDIAGNO) + ' - ' + Rtrim(D.NOMDIAGNO ) as Diagnostico,
			Rtrim(D.CODDIAGNO) as CodigoDiagnostico,
			Rtrim(ESP.DESESPECI) as Especialidad,
			CASE ORD.MANEXTPRO when 1 then 'Ambulatoria' else 'Hospitalario' END as OrigenOrden,
			E.SERIPSDASH as 'TipoServicio',
			Case 
			WHEN  A.ESTADO = 1 Then 'Solicitado' 
			when (A.ESTADO = 5 )  Then 'Finalizado' 
			when (A.ESTADO = 6 )  Then 'Anulado' 
			when (A.ESTADO = 7 )  Then 'Completado' 
			when (A.ESTADO = 8 )  Then 'Braquiterapia Iniciada' 
			End as 'Estado Esquema', 
			ORD.NUMINGRES
		from HCRADORDEN A 
			INNER JOIN HCORDPRON ORD with(nolock) ON A.IDHCORDPRON = ORD.AUTO
			INNER JOIN INCUPSIPS  E with(nolock) ON A.CODSERIPS = E.CODSERIPS AND E.SERIPSDASH  = 13 ---Braquiterapias
			INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
			INNER JOIN ADCENATEN CE with(nolock) ON CE.CODCENATE = A.CODCENATE
			INNER JOIN INPROFSAL   F with(nolock) ON A.CODPROSAL  = F.CODPROSAL
			INNER JOIN INDIAGNOS  D with(nolock) ON A.CODDIAGNO = D.CODDIAGNO
			INNER JOIN INESPECIA ESP with(nolock) ON ESP.CODESPECI  = A.CODESPECI
		WHERE A.IPCODPACI = @Identificacion

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las órdenes de braquiterapia registradas en historia clínica para un paciente específico, identificado por su cédula o documento. Consulta las órdenes de radiología (HCRADORDEN) filtradas exclusivamente al tipo de servicio ''Braquiterapia'' (código de panel 13), cruzando información del paciente, el profesional de la salud tratante, el centro de atención, el diagnóstico CIE-10, la especialidad médica y el origen de la orden (ambulatoria u hospitalaria). Devuelve el estado actual de cada esquema de braquiterapia (Solicitado, Braquiterapia Iniciada, Finalizado, Completado o Anulado), el procedimiento con su código CUPS, el número de ingreso y los datos de identificación del paciente, siendo útil para el seguimiento oncológico y la gestión clínica de tratamientos de braquiterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes/actividades recientes de braquiterapia de un paciente, con datos del procedimiento, centro, profesional, diagnóstico, especialidad, origen y estado del esquema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT con la identificación recibida.; El servicio asociado a la orden debe estar parametrizado en INCUPSIPS con SERIPSDASH = 13 (Braquiterapias).; Las órdenes deben tener referencia válida a HCORDPRON, centro de atención, profesional, diagnóstico y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen servicios clasificados como Braquiterapia (SERIPSDASH = 13).; Los estados manejados para el esquema son únicamente 1, 5, 6, 7 y 8; otros estados se devuelven como NULL en la etiqueta.; El filtro siempre se hace por la identificación del paciente recibida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Braquiterapia; Orden médica; Paciente; Procedimiento; Centro de atención; Profesional de salud; Diagnóstico; Especialidad médica; Estado de esquema oncológico; Origen ambulatorio/hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRADORDEN: Devuelve órdenes del paciente (A.IPCODPACI = @Identificacion) cuyo servicio sea de tipo Braquiterapia (E.SERIPSDASH = 13).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORD.MANEXTPRO = 1 → Origen de orden se reporta como ''Ambulatoria'' else Se reporta como ''Hospitalario''; si A.ESTADO = 1 → Estado del esquema = ''Solicitado''; si A.ESTADO = 5 → Estado del esquema = ''Finalizado''; si A.ESTADO = 6 → Estado del esquema = ''Anulado''; si A.ESTADO = 7 → Estado del esquema = ''Completado''; si A.ESTADO = 8 → Estado del esquema = ''Braquiterapia Iniciada''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADORDEN; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.ADCENATEN; dbo.INPROFSAL; dbo.INDIAGNOS; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarBraquiterapiasActividadesReciente';
-- GO
