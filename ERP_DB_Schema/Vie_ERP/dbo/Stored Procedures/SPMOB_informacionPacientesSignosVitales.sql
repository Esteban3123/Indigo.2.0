
CREATE PROCEDURE [dbo].[SPMOB_informacionPacientesSignosVitales]
(
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  a.TENARTSIS,a.TENARTDIA,a.TEMPERPAC,a.FRECARPAC, a.FECREGSIS,DATEDIFF(DAY,b.IPFECNACI,[Common].[GETDATE]()) as Edad , dbo.ObtenerFechaFormateada(FECREGSIS) as FechaRegistro
FROM HCEXFISIC a inner JOIN
inpacient b on a.IPCODPACI=b.IPCODPACI
WHERE a.IPCODPACI=@Paciente	and a.NUMINGRES=@Ingreso

end