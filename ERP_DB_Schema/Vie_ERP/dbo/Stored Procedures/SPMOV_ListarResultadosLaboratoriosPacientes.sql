CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientes]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  rtrim(CLASIFICACION) 
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto
  group by rtrim(CLASIFICACION) 
end