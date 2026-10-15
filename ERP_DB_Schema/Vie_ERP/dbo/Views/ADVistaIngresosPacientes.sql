


/*                      dbo.INENTIDAD C ON dbo.ADINGRESO.CODENTIDA = dbo.INENTIDAD.CODENTIDA AND dbo.INPACIENT.CODENTIDA = dbo.INENTIDAD.CODENTIDA*/
CREATE VIEW [dbo].[ADVistaIngresosPacientes]
AS
SELECT        A.NUMINGRES AS Ingreso, A.IPCODPACI AS Identificacion, B.IPPRINOMB AS PrimerNombre, B.IPSEGNOMB AS SegundoNombre, 
                         B.IPPRIAPEL AS PrimerApellido, B.IPSEGAPEL AS SegundoApellido, RTRIM(C.NOMENTIDA) AS Entidad, 
                         CASE WHEN A.IESTADOIN = ' ' THEN 'Sin Confirmar Hoja de Trabajo' WHEN A.IESTADOIN = 'F' THEN 'Confirmada Hoja de Trabajo' WHEN A.IESTADOIN = 'A' THEN
                          'Anulado' WHEN A.IESTADOIN = 'C' THEN 'Cerrado' END AS EstadoIngreso,
			   concat(Rtrim(IPPRINOMB), ' ', Rtrim(IPSEGNOMB), ' ', Rtrim(IPPRIAPEL), ' ',Rtrim(IPSEGAPEL)) as NombrePaciente
FROM            dbo.ADINGRESO AS A INNER JOIN
                         dbo.INPACIENT AS B ON B.IPCODPACI = A.IPCODPACI INNER JOIN
                         dbo.INENTIDAD AS C ON C.CODENTIDA = B.CODENTIDA