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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las mezclas o preparaciones magistrales (líquidas) activas de un paciente en un ingreso específico, junto con la existencia disponible de cada medicamento componente para el módulo de Solicitud de Medicamentos e Insumos de Enfermería. Calcula, por cada producto de la mezcla, las cantidades prescritas, ya despachadas y pendientes de despacho (en solicitud), descontando lo entregado y lo solicitado previamente según las órdenes farmacéuticas registradas en historia clínica. Para determinar el horizonte de tiempo válido de las órdenes, consulta la configuración de horas mínimas de reposición parametrizada por unidad funcional (piso/servicio) en HCUNITHIS; si no hay parámetro, asume 24 horas. Solo incluye mezclas de tratamiento continuo o con duración fija vigente (validada por la función Validar24PrescripcionMedica), excluyendo prescripciones canceladas o en planes de medicación no aplican reposición automática, y retorna la fecha de la última prescripción confirmada por reformulación para apoyar la gestión de reposición automática de medicamentos en enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las mezclas de medicamentos vigentes de un paciente/ingreso con sus cantidades prescritas, despachadas y pendientes para apoyar la reposición automática desde el módulo de enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe existir en HCUNITHIS con CODTIPHIS=''ENF'' para obtener las horas mínimas de reposición; en caso contrario se asume 24 horas.; El paciente y el ingreso deben tener mezclas registradas en HCINFLIQA con estado distinto a 2 y 4 (no canceladas/anuladas).; Las prescripciones consideradas deben tener tipo de duración ''Tratamiento Continuo'' o ''Fija'' vigente (validada por dbo.Validar24PrescripcionMedica).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen prescripciones con PREESTADO 2 o 4 (canceladas/anuladas).; Nunca se incluyen prescripciones con METAPLMED en (1,3,5) (planes que no aplican reposición automática).; Solo se procesan productos con TIPPRODUC en (''1'',''3'').; Solo se cuentan movimientos EXTRAMURAL = 0 o NULL (no extramurales) y con TIPOSOLICITUD = 1 o NULL.; La ''Cantidad Pedida'' nunca es negativa (se trunca a 0).; La cantidad de reposición prevalece sobre la cantidad calculada (EXT.CANTIDADREPOSICION sobre CANPROCAL) cuando existe.; El número de aplicaciones por defecto es 1 si no hay valor en la extensión.; La filtración temporal solo considera órdenes del día actual (CAST(FECHAORDE AS DATE) = hoy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Unidad funcional; Mezcla de medicamentos; Diluyente; Prescripción médica; Reposición automática de medicamentos; Despacho farmacéutico; Tratamiento continuo; Duración fija de prescripción; Reformulación; Plan de aplicación de medicamento; Enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultado de consulta: Devuelve por cada mezcla del paciente la cantidad prescrita, despachada y pendiente, calculando ''Cantidad Pedida'' = max(0, prescrita - entregada - solicitada).; [RETURN_RESULT] Resultado de consulta: Cuando TIPPRODUC del producto está en (''1'',''3'') y la prescripción no está en estado (2,4) ni con METAPLMED en (1,3,5), se incluye el registro en el resultado.; [RETURN_RESULT] Resultado de consulta: Para diluyentes (segunda parte UNION) se marca ''Diluyente''=1 y se toma dosis de CANPASDIL/UNIMEDDIL; para componentes principales se marca ''Diluyente''=0 con dosis CONMEDMEZ/UNIMEDMED.; [RETURN_RESULT] Resultado de consulta: Se incluye la fecha de la última prescripción de HCHISPACA con Reformulation=1 como FechaUltimaPrescripcionConfirmacion.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe parametrización de MinimumNumberHoursReplenishment en HCUNITHIS para la unidad funcional con CODTIPHIS=''ENF'' → Se asume 24 horas como ventana mínima de reposición else Se utiliza el valor parametrizado en horas; si IDETIPHIS = ''ENFERMER1'' → Se consideran movimientos cuya FECHAORDE sea de la fecha actual sin restricción de minutos else Se exige además que DATEDIFF(MINUTE, FECHAORDE, ahora) <= horaMinimaxReposicion*60; si TIPODURACION = ''Fija'' → Solo se incluye si dbo.Validar24PrescripcionMedica(UNIDADDURFIJA, DURACIONFIJA) = 1 (vigente dentro de 24h) else Si TIPODURACION = ''Tratamiento Continuo'' se incluye sin validación adicional; si isnull(EXT.CANTIDADREPOSICION,A.CANPROCAL) - entregada - solicitada < 0 → Cantidad Pedida se fija en 0 else Cantidad Pedida = prescrita - entregada - solicitada; si PROESTADO = ''2'' (entregado) → Se acumula en CTEEntregado como CantidadEntregada else Si PROESTADO = ''1'' se acumula en CTESolicitado como CantidadPedida; si Segundo SELECT del UNION: existe en HCINFCONC un registro con mismo CODCONCEC y CODPRODUC → Se excluye el registro (NOT EXISTS) para evitar duplicar componentes ya listados como concentrado else Se incluye el líquido/diluyente; si C.TIPMEZLIQ = 2 y D.TIPODURACION es NULL → Se incluye el líquido aunque no tenga tipo de duración (''líquidos solitos'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Validar24PrescripcionMedica; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCUNITHIS; dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.HCINFLIQA; dbo.HCINFCONC; dbo.HCINFLIQC; dbo.IHLISTPRO; dbo.HCINFCONCEXT; dbo.HCINFLIQD; dbo.HCINFLIQDEXT; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasConExistencia';
-- GO
