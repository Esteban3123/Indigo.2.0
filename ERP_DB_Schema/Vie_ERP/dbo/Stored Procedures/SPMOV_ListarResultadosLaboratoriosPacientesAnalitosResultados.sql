CREATE PROCEDURE [dbo].[SPMOV_ListarResultadosLaboratoriosPacientesAnalitosResultados]
(
@CodigoAuto Char(10),
@Paciente Varchar(25),
@Ingreso Char(20),
@Clasificacion Char(60),
@Analito Char(70)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(ANALITO) as ANALITO  ,NUMMUESTRA, VALOR, UNIDAD, VALORMINIMO, VALORMAXIMO, a.FECORDMED
		  FROM HCORDLABO A INNER JOIN
  INTERLABD B ON A.AUTO=B.AUTOLABOR
  Where a.IPCODPACI=@Paciente and a.NUMINGRES=@Ingreso and a.CODSERIPS=@CodigoAuto and b.CLASIFICACION=@Clasificacion and b.ANALITO=@Analito

end