-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 13/05/2022
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_ENF_ListarMedicamentosConExistencia]
(
@unidadfuncional as varchar(10),
@paciente as varchar(20),
@ingreso as varchar(20)
)
AS
BEGIN

	declare @horaMinimaxReposicion as int ;

	--Asignamos las horas parametrizadas para el calculo de medicamento desde enfermeria, si no es encuentra parametrizado se toma por default como 24 horas
	--este parametros es solo para sumar las cantidades tanto solicitada como despachadas desde una orden medica
	set @horaMinimaxReposicion = (SELECT TOP(1) ISNULL(MinimumNumberHoursReplenishment,24) FROM HCUNITHIS WHERE UFUCODIGO = @unidadfuncional AND CODTIPHIS = 'ENF');


WITH CTEEntregado
AS
(
SELECT A.CODPRODUC, SUM(A.CANPEDPRO) as 'CantidadEntregada'   
from HCFARMEPD AS A INNER JOIN 
HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC INNER JOIN
HCPRESCRA AS C ON A.IdSourceTable = C.ID AND A.SourceTable = 'HCPRESCRA'
WHERE A.IPCODPACI = @paciente AND  
(A.EXTRAMURAL =0 or A.EXTRAMURAL is null) AND A.PROESTADO = '2' AND (B.TIPOSOLICITUD = 1 or B.TIPOSOLICITUD is null) AND (C.DURACIDOS = 'Tratamiento Continuo' OR (C.DURACIDOS = 'Fija' and [dbo].[Validar24PrescripcionMedica] (C.UNIDURFIJ, C.VALDURFIJ)= 1))
AND (A.IDETIPHIS = 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE)) --para listar lo de enfermeria
									OR (A.IDETIPHIS <> 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE) AND DATEDIFF(HOUR,B.FECHAORDE,Common.GETDATE()) <= @horaMinimaxReposicion))) --para listar lo del medico del dia y que haya superado las horas parametrizadas
GROUP BY A.CODPRODUC
),
CTESolicitado
AS
(
SELECT A.CODPRODUC, SUM(A.CANPEDPRO) as 'CantidadPedida'   
from HCFARMEPD AS A INNER JOIN 
HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC INNER JOIN
HCPRESCRA AS C ON A.IdSourceTable = C.ID AND A.SourceTable = 'HCPRESCRA'
WHERE A.IPCODPACI = @paciente AND  
(A.EXTRAMURAL =0 or A.EXTRAMURAL is null) AND A.PROESTADO = '1' AND (B.TIPOSOLICITUD = 1 or B.TIPOSOLICITUD is null) AND (C.DURACIDOS = 'Tratamiento Continuo' OR (C.DURACIDOS = 'Fija' and [dbo].[Validar24PrescripcionMedica] (C.UNIDURFIJ, C.VALDURFIJ)= 1))
AND (A.IDETIPHIS = 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE)) --para listar lo de enfermeria
																						OR (A.IDETIPHIS <> 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE) AND DATEDIFF(HOUR,B.FECHAORDE,Common.GETDATE()) <= @horaMinimaxReposicion)))--para listar lo del medico del dia y que haya superado las horas parametrizadas
GROUP BY A.CODPRODUC
)

SELECT 
  NUMINGRES AS Ingreso, 
  IPCODPACI AS Paciente, 
  CODPROSAL AS Medico, 
  B.TIPPRODUC AS TipoProducto, 
  RTRIM(B.CODPRODUC) AS 'Codigo Producto', 
   RTRIM(B.CODPRODUC) +' - '+RTRIM(DESPRODUC) AS Producto, 
  isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) AS 'Cantidad Prescrita', 
  isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) AS 'Cantidad Facturada',
  case a.TIPFORMED 
			WHEN 4 then 0 --reposicion manual cantidad = 0
		else 
			iif(isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0) < 0 , 0 , isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0))		
		END AS 'Cantidad Pedida',
  ISNULL(Entregado.CantidadEntregada,0) AS 'CantidadDespachada', 
  A.CODCENATE AS 'Centro Atencion', 
  A.UFUCODIGO AS 'Unidad Funcional', 
  A.MEDICACUSTODIA AS 'MEDICAMENTO CUSTODIA', 
  isnull(Solicitado.CantidadPedida,0) AS 'P Despacho', 
  0 as 'Cantidad del Paciente', 
  1 as 'TipoPreescripcion', 
  A.ID as 'Identificador', 
  ISNULL(
    EXT.NUMEROAPLICACIONREPOSICION, 
    1
  ) AS 'Aplicaciones', 
  ISNULL(a.DOSISPRFN, A.DOSISPROD) as 'Dosis', 
  ISNULL(A.CODUNIMFN, A.CODUNIMED) as 'UnidadMedida', 
  isnull(EXT.CANTIDADREPOSICION,A.CANPEDPRO) as CANTIDADREPOSICION, 
  RTRIM(A.DESADMINI) AS 'Administracion', 
  0 as 'Diluyente' ,
  CASE a.TIPFORMED WHEN  4 THEN 'Medicamento de reposición manual' else 'Medicamentos de reposición automática' END as TipoReposicion,
  (select top 1 FECHISPAC from HCHISPACA 
	where NUMINGRES = A.NUMINGRES and IPCODPACI = a.IPCODPACI and Reformulation = 1 order by FECHISPAC desc) as FechaUltimaPrescripcionConfirmacion
FROM 
  dbo.HCPRESCRA A 
  INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC = B.CODPRODUC 
  LEFT JOIN dbo.HCPRESCRAEXT EXT ON A.ID = EXT.IDHCPRESCRA 
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadEntregada FROM CTEEntregado) as Entregado ON A.CODPRODUC = Entregado.CODPRODUC
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadPedida FROM CTESolicitado) AS Solicitado ON A.CODPRODUC = Solicitado.CODPRODUC
WHERE 
  A.PREESTADO IN ('1', '6') 
  AND IPCODPACI = @paciente 
  AND NUMINGRES = @ingreso 
  AND B.TIPPRODUC IN ('1', '3') 
  AND (
    A.IDESQUEMAONC IS NULL 
    OR A.IDESQUEMAONC = 0
  ) 
  AND (A.DURACIDOS = 'Tratamiento Continuo' OR (A.DURACIDOS = 'Fija' and [dbo].[Validar24PrescripcionMedica] (A.UNIDURFIJ, A.VALDURFIJ)= 1))

END