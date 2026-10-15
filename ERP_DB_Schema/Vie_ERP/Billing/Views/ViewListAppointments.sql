


























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