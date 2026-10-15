-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila115]
(
  @IdFicha as Int
  )

AS
BEGIN
  SET NOCOUNT ON;

  Select CASE TIPOTUMOR  WHEN '1' THEN 'X' END AS 'Leucemia linfoide aguda',CASE TIPOTUMOR  WHEN '2' THEN 'X' END AS 'Leucemia mieloide aguda',CASE TIPOTUMOR  WHEN '3' THEN 'X' END AS 'Otras leucemias',CASE TIPOTUMOR  WHEN '4' THEN 'X' END AS 'Linfomas y neoplasias',CASE TIPOTUMOR  WHEN '5' THEN 'X' END AS 'Tumores del sistema nervioso central',CASE TIPOTUMOR  WHEN '6' THEN 'X' END AS 'Neuroblastoma',CASE TIPOTUMOR  WHEN '7' THEN 'X' END AS 'Retinoblastoma',CASE TIPOTUMOR  WHEN '8' THEN 'X' END AS 'Tumores renales',CASE TIPOTUMOR  WHEN '9' THEN 'X' END AS 'Tumores hepáticos',CASE TIPOTUMOR  WHEN '10' THEN 'X' END AS 'Tumores óseos malignos',CASE TIPOTUMOR  WHEN '11' THEN 'X' END AS 'Sarcomas',CASE TIPOTUMOR  WHEN '12' THEN 'X' END AS 'Tumores germinales',CASE TIPOTUMOR  WHEN '13' THEN 'X' END AS 'Tumores epiteliales',CASE TIPOTUMOR  WHEN '14' THEN 'X' END AS 'Otras neoplasias',
         CASE CONSULTANEO WHEN '1' THEN 'X' END AS 'Consulta Neo Si',CASE CONSULTANEO WHEN '0' THEN 'X' END AS 'Consulta Neo No',
		 CASE CONSULTARECA  WHEN '1' THEN 'X' END AS 'Consulta Reca Si',CASE CONSULTARECA WHEN '0' THEN 'X' END AS 'Consulta Reca No',
		 convert(varchar(10),FECHADIAGINI,103) As 'Fecha Dianostico Inicial',
		 CASE CRITERIODIAG  WHEN '1' THEN 'X' END AS 'Hemograma',CASE CRITERIODIAG  WHEN '2' THEN 'X' END AS 'Radiología',CASE CRITERIODIAG  WHEN '3' THEN 'X' END AS 'Gammagrafía',CASE CRITERIODIAG  WHEN '4' THEN 'X' END AS 'Marcadores',CASE CRITERIODIAG  WHEN '5' THEN 'X' END AS 'Clínica',
		 convert(varchar(10),FECHATOMA1,103) As 'Fecha Toma 1',convert(varchar(10),FECHARESUL1,103) As 'Fecha Resultado 1',
		 CASE CRITERIOCONF  WHEN '1' THEN 'X' END AS 'Mielograma',CASE CRITERIOCONF  WHEN '2' THEN 'X' END AS 'Histopatología',CASE CRITERIOCONF  WHEN '3' THEN 'X' END AS 'Inmunotipificación',CASE CRITERIOCONF  WHEN '4' THEN 'X' END AS 'Criterio médico',CASE CRITERIOCONF  WHEN '5' THEN 'X' END AS 'Certificado de defunción',CASE CRITERIOCONF  WHEN '6' THEN 'X' END AS 'Citogenética',CASE CRITERIOCONF  WHEN '7' THEN 'X' END AS 'Radiología diagnóstica',
		 convert(varchar(10),FECHATOMA2,103) As 'Fecha Toma 2',convert(varchar(10),FECHARESUL2,103) As 'Fecha Resultado 2',
		 convert(varchar(10),FECHAINICIOTRATAMIENTO ,103) As 'Fecha Inicio tratamiento',
		 VERSION AS 'VERSION', JSON AS 'JSON'
   from HCFICHA115 where IDFICHANOTIFICACION  = @IdFicha  

END

--Select * from HCFICHANOTIFICACION WHERE ID = '9'
--select * from HCFICHA115 