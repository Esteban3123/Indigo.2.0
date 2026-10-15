-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 15/05/2022
-- Description: SP para listar los medicamentos actuales asociados a una mezcla del paciente en Solicitud de medicamento e insumos (Enfermeria)
-- =============================================
CREATE PROCEDURE [dbo].[SP_ENF_ListarMezclasConExistencia]
(
@unidadfuncional as varchar(10),
@paciente as varchar(20),
@ingreso as varchar(20)
)
AS
BEGIN

	declare @horaMinimaxReposicion as int

	--Asignamos las horas parametrizadas para el calculo de medicamento desde enfermeria, si no es encuentra parametrizado se toma por default como 24 horas
	set @horaMinimaxReposicion = (SELECT TOP(1) ISNULL(MinimumNumberHoursReplenishment,24) FROM HCUNITHIS WHERE UFUCODIGO = @unidadfuncional AND CODTIPHIS = 'ENF');


WITH CTEEntregado
AS
(
SELECT A.CODPRODUC, SUM(A.CANPEDPRO) as 'CantidadEntregada'   
from HCFARMEPD AS A INNER JOIN 
HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC INNER JOIN
HCINFLIQA AS C ON A.IdSourceTable = C.CONSECUTI AND A.SourceTable ='HCINFLIQA'
WHERE A.IPCODPACI = @paciente AND  (A.EXTRAMURAL =0 or A.EXTRAMURAL is null) AND A.PROESTADO = '2' AND (B.TIPOSOLICITUD = 1 or B.TIPOSOLICITUD is null) AND (C.TIPODURACION = 'Tratamiento Continuo' OR (C.TIPODURACION= 'Fija' and [dbo].[Validar24PrescripcionMedica] (C.UNIDADDURFIJA, C.DURACIONFIJA)= 1))
AND (A.IDETIPHIS = 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE)) OR (A.IDETIPHIS <> 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE) AND DATEDIFF(MINUTE,B.FECHAORDE,Common.GETDATE()) <= (@horaMinimaxReposicion * 60))))
AND C.METAPLMED NOT IN(1, 3, 5) 
GROUP BY A.CODPRODUC
),
CTESolicitado
AS
(
SELECT A.CODPRODUC, SUM(A.CANPEDPRO) as 'CantidadPedida'   
from HCFARMEPD AS A INNER JOIN 
HCFARMEPC AS B ON A.CODCONCEC = B.CODCONCEC INNER JOIN
HCINFLIQA AS C ON A.IdSourceTable = C.CONSECUTI AND A.SourceTable ='HCINFLIQA'
WHERE A.IPCODPACI = @paciente AND  (A.EXTRAMURAL =0 or A.EXTRAMURAL is null) AND A.PROESTADO = '1' AND (B.TIPOSOLICITUD = 1 or B.TIPOSOLICITUD is null) AND (C.TIPODURACION = 'Tratamiento Continuo' OR (C.TIPODURACION= 'Fija' and [dbo].[Validar24PrescripcionMedica] (C.UNIDADDURFIJA, C.DURACIONFIJA)= 1))
AND (A.IDETIPHIS = 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE)) OR (A.IDETIPHIS <> 'ENFERMER1' AND (CAST(B.FECHAORDE AS DATE) = CAST(Common.GETDATE() AS DATE) AND DATEDIFF(MINUTE,B.FECHAORDE,Common.GETDATE()) <= (@horaMinimaxReposicion * 60)))) 
AND C.METAPLMED NOT IN(1, 3, 5) 
GROUP BY A.CODPRODUC
)

SELECT 
  A.NUMINGRES AS Ingreso, 
  A.IPCODPACI AS Paciente, 
  A.CODPROSAL AS Medico, 
  B.TIPPRODUC AS TipoProducto, 
  RTRIM(B.CODPRODUC) AS 'Codigo Producto', 
  d.MEZLIQPAC, 
  a.CODPRODUC, 
  0 as 'Cantidad del Paciente', 
   RTRIM(B.CODPRODUC) +' - '+RTRIM(B.DESPRODUC) AS Producto, 
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) AS 'Cantidad Prescrita', 
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) AS 'Cantidad Facturada', 
  iif(isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0) < 0 , 0 , isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0) ) as 'Cantidad Pedida',
  ISNULL(Entregado.CantidadEntregada,0) AS 'CantidadDespachada', 
  A.CODCENATE AS 'Centro Atencion', 
  A.UFUCODIGO AS 'Unidad Funcional', 
  ISNULL(Solicitado.CantidadPedida,0) AS 'P Despacho', 
  0 as 'Cantidad del Paciente', 
  2 as 'TipoPreescripcion', 
  D.CONSECUTI as 'Identificador', 
  ISNULL(
    EXT.NUMEROAPLICACIONREPOSICION, 
    1
  ) AS 'Aplicaciones', 
  RTRIM(d.ADMMEZLIQ) AS 'Administracion', 
  0 as 'Diluyente', 
  A.CONMEDMEZ as 'Dosis', 
  A.UNIMEDMED as 'UnidadMedida',
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) as CANTIDADREPOSICION,
  'Medicamentos de reposición automática'  as TipoReposicion,
  (select top 1 FECHISPAC from HCHISPACA 
	where NUMINGRES = A.NUMINGRES and IPCODPACI = a.IPCODPACI and Reformulation = 1 order by FECHISPAC desc) as FechaUltimaPrescripcionConfirmacion
FROM 
  dbo.HCINFLIQA d 
  INNER JOIN dbo.HCINFCONC A ON D.CODCONCEC = A.CODCONCEC 
  INNER JOIN dbo.HCINFLIQC C ON D.CODCONCEC = C.CODCONCEC 
  INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC = B.CODPRODUC 
  LEFT JOIN dbo.HCINFCONCEXT EXT on A.CODCONCEC = EXT.IDHCINFCONC 
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadEntregada FROM CTEEntregado) as Entregado ON A.CODPRODUC = Entregado.CODPRODUC
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadPedida FROM CTESolicitado) AS Solicitado ON A.CODPRODUC = Solicitado.CODPRODUC

WHERE 
  D.IPCODPACI = @paciente
  AND D.NUMINGRES = @ingreso
  AND B.TIPPRODUC IN ('1', '3') 
  AND d.PREESTADO NOT IN (2, 4) 
  AND D.METAPLMED NOT IN(1, 3, 5) 
  AND D.TIPODURACION IN('Tratamiento Continuo', 'Fija') 
  AND (D.TIPODURACION = 'Tratamiento Continuo' OR (D.TIPODURACION= 'Fija' and [dbo].[Validar24PrescripcionMedica] (D.UNIDADDURFIJA, D.DURACIONFIJA)= 1))

  UNION 

SELECT 
  A.NUMINGRES AS Ingreso, 
  A.IPCODPACI AS Paciente, 
  A.CODPROSAL AS Medico, 
  B.TIPPRODUC AS TipoProducto, 
  RTRIM(B.CODPRODUC) AS 'Codigo Producto', 
  d.MEZLIQPAC, 
  a.CODPRODUC, 
  0 as 'Cantidad del Paciente', 
   RTRIM(B.CODPRODUC) +' - '+RTRIM(B.DESPRODUC) AS Producto, 
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) AS 'Cantidad Prescrita', 
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) AS 'Cantidad Facturada', 
  iif(isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0) < 0 , 0 , isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) -  ISNULL(Entregado.CantidadEntregada,0) - ISNULL(Solicitado.CantidadPedida,0) ) as 'Cantidad Pedida',
  ISNULL(Entregado.CantidadEntregada,0) AS 'CantidadDespachada', 
  A.CODCENATE AS 'Centro Atencion', 
  A.UFUCODIGO AS 'Unidad Funcional', 
  ISNULL(Solicitado.CantidadPedida,0) AS 'P Despacho', 
  0 as 'Cantidad del Paciente', 
  2 as 'TipoPreescripcion', 
  D.CONSECUTI as 'Identificador', 
  ISNULL(
    EXT.NUMEROAPLICACIONREPOSICION, 
    1
  ) AS 'Aplicaciones', 
  RTRIM(d.ADMMEZLIQ) AS 'Administracion', 
  1 as 'Diluyente', 
  A.CANPASDIL as 'Dosis', 
  A.UNIMEDDIL as 'UnidadMedida' ,
  isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) as CANTIDADREPOSICION,
  'Medicamentos de reposición automática'  as TipoReposicion,
  (select top 1 FECHISPAC from HCHISPACA 
	where NUMINGRES = A.NUMINGRES and IPCODPACI = a.IPCODPACI and Reformulation = 1 order by FECHISPAC desc) as FechaUltimaPrescripcionConfirmacion
FROM 
  dbo.HCINFLIQA d 
  INNER JOIN dbo.HCINFLIQD A ON D.CODCONCEC = A.CODCONCEC 
  INNER JOIN dbo.HCINFLIQC C ON D.CODCONCEC = C.CODCONCEC
  LEFT JOIN dbo.HCINFLIQDEXT EXT on D.CODCONCEC = EXT.IDHCINFLIQD 
  INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC = B.CODPRODUC 
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadEntregada FROM CTEEntregado) as Entregado ON A.CODPRODUC = Entregado.CODPRODUC
  LEFT OUTER JOIN (SELECT CODPRODUC, CantidadPedida FROM CTESolicitado) AS Solicitado ON A.CODPRODUC = Solicitado.CODPRODUC

WHERE 
  D.IPCODPACI = @paciente
  AND D.NUMINGRES = @ingreso
  AND B.TIPPRODUC IN ('1', '3') 
  AND d.PREESTADO NOT IN (2, 4) 
  AND D.METAPLMED NOT IN(1, 3, 5) 
  AND (
			(D.TIPODURACION = 'Tratamiento Continuo' OR (D.TIPODURACION= 'Fija' and [dbo].[Validar24PrescripcionMedica] (D.UNIDADDURFIJA, D.DURACIONFIJA)= 1)) 
			OR (C.TIPMEZLIQ = 2 and D.TIPODURACION is null ) --para mostrar los liquidos solitos
	  )
  AND NOT EXISTS (SELECT CODCONCEC FROM HCINFCONC H WHERE D.CODCONCEC = H.CODCONCEC AND A.CODPRODUC = H.CODPRODUC)


END