

CREATE VIEW [Billing].[ViewListAppointments]
AS

select ROW_NUMBER() OVER (ORDER BY ag.NUMINGRES) Id,
ag.IPCODPACI PatientCode, ad.NUMINGRES AdmissionCode,
ad.CODCENATE CareCenterCode, RTRIM(LTRIM(ag.CODCENATE)) + ' - ' + RTRIM(LTRIM(adce.NOMCENATE)) CareCenterDescription, 
ag.FECINICIT InitialDate, ag.FECHORAFI EndDate,
ag.CODESPECI SpecialtyCode, RTRIM(LTRIM(ag.CODESPECI)) + ' - ' + RTRIM(LTRIM(ine.DESESPECI)) SpecialtyDescription,
ag.CODACTMED ActivityCode, RTRIM(LTRIM(ag.CODACTMED)) + ' - ' + RTRIM(LTRIM(aga.DESACTMED)) ActivityDescription,
ag.CODPROSAL ProfessionalCode, RTRIM(LTRIM(ag.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalDescription,
ag.CODESTCIT StatusCode, case ag.CODESTCIT when '0' then 'Asignada' when '1' then 'Cumplida' when '2' then 'Incumplida' when '3' then 'Preasignada' when '4' then 'Cancelada' end StatusDescription
from .ADCONCOEX ad WITH(NOLOCK)
inner join .AGASICITA ag WITH(NOLOCK) on ag.CODAUTONU = ad.NUMCONCIT
inner join .ADCENATEN adce WITH(NOLOCK) on adce.CODCENATE = ad.CODCENATE
inner join .INESPECIA ine WITH(NOLOCK) on ine.CODESPECI = ag.CODESPECI
inner join .AGACTIMED aga WITH(NOLOCK) on aga.CODACTMED = ag.CODACTMED
inner join .INPROFSAL prof WITH(NOLOCK) on prof.CODPROSAL = ag.CODPROSAL
--where ad.IPCODPACI = '1079183992' --and ad.NUMINGRES = '62224'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de citas médicas asociadas a ingresos de pacientes, utilizada por el módulo de facturación para consultar y reportar la agenda de atenciones. Combina el registro de citas (AGASICITA) con el ingreso o admisión correspondiente (ADCONCOEX), enriqueciendo cada fila con el nombre del centro de atención, la descripción de la especialidad médica, el tipo de actividad agendada y el nombre del profesional de salud. Incluye el estado actual de cada cita (Asignada, Cumplida, Incumplida, Preasignada o Cancelada), las fechas de inicio y fin de la cita, el código del paciente y el número de ingreso, permitiendo trazabilidad completa entre la agenda médica y los procesos de facturación y cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListAppointments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewListAppointments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de citas médicas asociadas a admisiones de consulta externa, enriquecido con descripciones de centro de atención, especialidad, actividad médica, profesional y estado de la cita.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir relación entre la admisión de consulta externa (ADCONCOEX.NUMCONCIT) y la cita asignada (AGASICITA.CODAUTONU).; Los catálogos de centro de atención, especialidad, actividad médica y profesional deben tener los códigos referenciados por la cita/admisión.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan citas cuyo CODAUTONU coincide con un NUMCONCIT existente en admisiones de consulta externa (INNER JOIN obligatorio).; Solo se incluyen citas con centro de atención, especialidad, actividad médica y profesional vigentes en sus catálogos maestros (todos los joins son INNER).; El Id es un consecutivo volátil generado por ROW_NUMBER ordenado por NUMINGRES; no es una clave de negocio estable.; Los códigos descriptivos se entregan concatenados como ''CODIGO - DESCRIPCION'' con espacios recortados.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica (AGASICITA); Admisión / consulta externa (ADCONCOEX); Paciente; Centro de atención; Especialidad; Actividad médica; Profesional de la salud; Estados de cita (Asignada, Cumplida, Incumplida, Preasignada, Cancelada)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewListAppointments: Devuelve una fila por cada cita (AGASICITA) vinculada a una admisión de consulta externa (ADCONCOEX) que además tenga catálogos válidos de centro, especialidad, actividad y profesional.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ag.CODESTCIT = ''0'' → Estado de cita = ''Asignada''; si ag.CODESTCIT = ''1'' → Estado de cita = ''Cumplida''; si ag.CODESTCIT = ''2'' → Estado de cita = ''Incumplida''; si ag.CODESTCIT = ''3'' → Estado de cita = ''Preasignada''; si ag.CODESTCIT = ''4'' → Estado de cita = ''Cancelada'' else Sin descripción asociada (NULL)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADCONCOEX; AGASICITA; ADCENATEN; INESPECIA; AGACTIMED; INPROFSAL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewListAppointments';
GO
