

CREATE VIEW [Billing].[ViewScheduleActivityByEspeciality]
AS
	SELECT	A.CODACTMED AS ActivityCode,
			C.DESACTMED AS ActivityDescription,
			(CASE 
				WHEN ACTIVICON = 1 THEN 'QX' + ' - ' + A.CODACTMED + ' - ' +  DESACTMED 
				ELSE 'CE' + ' - ' + A.CODACTMED + ' - ' +  DESACTMED 
				END) AS Activities,
			C.ACTIVICON as ActivityType ,
			A.CODESPECI AS EspecialityCode
FROM dbo.AGESPACTI A 
  INNER JOIN dbo.INESPECIA B ON A.CODESPECI=B.CODESPECI
  INNER JOIN dbo.AGACTIMED C ON A.CODACTMED=C.CODACTMED
WHERE ESTADOACT=1