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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos con existencia pendiente de reposición para un paciente, ingreso y unidad funcional (piso/servicio de enfermería) determinados. Calcula, por cada medicamento prescrito de forma continua o fija vigente, cuántas unidades ya fueron despachadas por farmacia y cuántas están en tránsito (solicitadas pero no despachadas), y determina la cantidad neta que aún debe pedirse al almacén. Combina las prescripciones activas (HCPRESCRA), el catálogo de productos farmacéuticos (IHLISTPRO) y las órdenes de despacho farmacéutico (HCFARMEPD/HCFARMEPC), respetando el parámetro de horas mínimas de reposición configurado por unidad funcional (HCUNITHIS). Se usa desde el módulo de enfermería para generar automáticamente los pedidos de medicamentos que necesita el paciente hospitalizado, distinguiendo entre reposición automática y manual, e incluyendo datos de custodia, dosis, vía de administración y fecha de última confirmación de prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos prescritos vigentes de un paciente/ingreso con su existencia disponible, calculando cantidades pedidas, despachadas y pendientes para reposición desde enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir con prescripciones en estado ''1'' o ''6''; Las prescripciones deben ser de productos tipo ''1'' o ''3'' (medicamentos/insumos); Las prescripciones no deben pertenecer a esquema oncológico (IDESQUEMAONC nulo o 0); La prescripción debe ser de duración ''Tratamiento Continuo'' o ''Fija'' validada dentro de las 24 horas por dbo.Validar24PrescripcionMedica; La unidad funcional puede tener parametrizadas horas mínimas/máximas de reposición en HCUNITHIS para tipo ''ENF''; si no, se asume 24 horas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ''Cantidad Pedida'' calculada nunca es negativa (se trunca a 0); Para medicamentos de reposición manual (TIPFORMED=4) la cantidad pedida siempre es 0; Solo se consideran prescripciones intramurales (EXTRAMURAL = 0 o nulo); Solo se incluyen prescripciones activas (PREESTADO 1 o 6) y no oncológicas; Si no hay parametrización de horas de reposición, se usa 24 horas por defecto; La cantidad de reposición prevalece sobre la cantidad prescrita original cuando existe registro en HCPRESCRAEXT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción médica; Medicamento; Reposición de medicamentos (manual y automática); Despacho farmacéutico; Unidad funcional; Enfermería; Tratamiento continuo; Dosis fija; Esquema oncológico; Medicamento en custodia; Reformulación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna las prescripciones del paciente/ingreso con PREESTADO IN (''1'',''6''), tipo de producto 1 o 3, no oncológicas y de duración continua o fija dentro de 24h, junto con cantidades calculadas de despacho y pendientes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPFORMED = 4 (reposición manual) → La ''Cantidad Pedida'' calculada se fuerza a 0 y se etiqueta como ''Medicamento de reposición manual'' else Se calcula Cantidad Pedida = CANTIDADREPOSICION (o CANPEDPRO) - CantidadEntregada - CantidadPedida; si es negativo se devuelve 0; etiqueta ''Medicamentos de reposición automática''; si IDETIPHIS = ''ENFERMER1'' y la orden es del día actual → Se incluye en los acumulados de entregado/solicitado; si IDETIPHIS <> ''ENFERMER1'', orden del día actual y dentro de las horas mínimas de reposición parametrizadas → Se incluye en los acumulados de entregado/solicitado else Se excluye del cálculo de cantidades acumuladas; si EXTRAMURAL = 0 o nulo y TIPOSOLICITUD = 1 o nulo → El movimiento de farmacia se considera para los acumulados else Se excluye; si PROESTADO = ''2'' → El movimiento se acumula como CantidadEntregada; si PROESTADO = ''1'' → El movimiento se acumula como CantidadPedida (pendiente de despacho)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Validar24PrescripcionMedica; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCUNITHIS; dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.HCPRESCRA; dbo.HCPRESCRAEXT; dbo.IHLISTPRO; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosConExistencia';
-- GO
