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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera el detalle clínico de una Ficha 115 de notificación obligatoria SIVIGILA para casos de cáncer (neoplasias) en pediatría y adultos. Dado el identificador de la ficha de notificación, consulta la tabla HCFICHA115 y devuelve la información formateada para impresión o visualización: tipo de tumor clasificado (leucemia linfoide, mieloide, linfomas, neuroblastoma, retinoblastoma, sarcomas, entre otros), indicadores de consulta por neoplasia nueva o recaída, criterios de sospecha diagnóstica (hemograma, radiología, gammagrafía, marcadores, clínica), criterios de confirmación (mielograma, histopatología, inmunotipificación, citogenética, entre otros), fechas clave de diagnóstico inicial, toma de muestras, resultados y fecha de inicio del tratamiento. Se utiliza en el módulo de historia clínica para consultar, imprimir o exportar la ficha epidemiológica de vigilancia en salud pública de eventos oncológicos de notificación obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila115';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea los datos de una ficha de notificación Sivigila 115 (cáncer en menores) traduciendo códigos numéricos a marcas ''X'' por categoría diagnóstica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA115 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo una de las columnas mutuamente excluyentes (tipo de tumor, consulta neo, consulta reca, criterio diagnóstico, criterio confirmación) puede contener ''X'' por fila, según el código almacenado.; Las fechas se devuelven siempre en formato británico dd/MM/yyyy (estilo 103) o NULL si la fecha original es NULL.; Códigos fuera del rango definido producen NULL en todas las columnas marcadas con ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Cáncer en menores (Sivigila 115); Tipos de tumor pediátrico; Criterio de diagnóstico; Criterio de confirmación; Consulta de neoplasia; Consulta de recaída; Fecha de inicio de tratamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA115: Cuando IDFICHANOTIFICACION coincide con el parámetro, retorna una fila con las marcas ''X'' por tipo de tumor, criterios de diagnóstico/confirmación, fechas formateadas dd/MM/yyyy y campos VERSION y JSON.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOTUMOR entre ''1'' y ''14'' → Marca con ''X'' la columna correspondiente al tipo de neoplasia (Leucemia linfoide aguda, mieloide aguda, linfomas, tumores SNC, neuroblastoma, retinoblastoma, renales, hepáticos, óseos, sarcomas, germinales, epiteliales, otras).; si CONSULTANEO = ''1'' o ''0'' → Marca ''X'' en ''Consulta Neo Si'' o ''Consulta Neo No'' respectivamente.; si CONSULTARECA = ''1'' o ''0'' → Marca ''X'' en ''Consulta Reca Si'' o ''Consulta Reca No'' respectivamente.; si CRITERIODIAG entre ''1'' y ''5'' → Marca el criterio de diagnóstico: Hemograma, Radiología, Gammagrafía, Marcadores o Clínica.; si CRITERIOCONF entre ''1'' y ''7'' → Marca el criterio de confirmación: Mielograma, Histopatología, Inmunotipificación, Criterio médico, Certificado de defunción, Citogenética o Radiología diagnóstica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA115', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila115';
-- GO
