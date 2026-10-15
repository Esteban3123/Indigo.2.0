CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientesAnalitos]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20),
@Clasificacion Char(60)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(ANALITO) as ANALITO  ,NUMMUESTRA, VALOR, UNIDAD, VALORMINIMO, VALORMAXIMO
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto and b.CLASIFICACION=@Clasificacion

end