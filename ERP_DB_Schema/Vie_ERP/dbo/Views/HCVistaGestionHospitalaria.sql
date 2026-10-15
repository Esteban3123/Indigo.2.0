
create VIEW [dbo].[HCVistaGestionHospitalaria]
AS
SELECT     ROW_NUMBER() OVER (ORDER BY A.CODICAMAS) AS NumeroFila, A.CODICAMAS AS CodigoCama, RTRIM(A.DESCCAMAS) AS Cama, 
dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion, dbo.ClaseCama(A.CODCLACAM) AS ClasedeCama, D .IPCODPACI AS Identificacion, D .NUMINGRES AS Ingreso, 
dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento, RTRIM(E.DESTIPEST) AS TipoEstancia, RTRIM(F.IPNOMCOMP) AS Paciente, CAST(0 AS BIT) AS Resultado, 
A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo, A.CODCENATE AS CentroAtencion, 
A.UFUCODIGO AS UnidadFuncional, A.ESTADCAMA AS EstadoCama
FROM         dbo.CHCAMASHO AS A INNER JOIN
                      dbo.ADCENATEN AS B ON A.CODCENATE = B.CODCENATE INNER JOIN
                      dbo.INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                      dbo.CHREGESTA AS D ON A.CODICAMAS = D .CODICAMAS AND D .REGESTADO = 1 LEFT OUTER JOIN
                      dbo.CHTIPESTA AS E ON D .CODTIPEST = E.CODTIPEST LEFT OUTER JOIN
                      dbo.INPACIENT AS F ON D .IPCODPACI = F.IPCODPACI