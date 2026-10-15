
CREATE PROCEDURE [dbo].[SPHC_ListarTableroHistorias]
(
@Paciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
SELECT TIPHISPAC,UFUCODIGO 
FROM dbo.HCHISPACA with(nolock)
WHERE IPCODPACI=@paciente AND NUMINGRES=@ingreso and TIPHISPAC in ('I','T')
end