/**** 
SP para listar el detalle para la creación del JSON para las solicitudes de farmacia de quimioterapia para UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarDetalleSolicitudFarmaciaQuimioterapia]
(
  @Consecutivo as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;

	-- AGASICITA es una tabla Scheduling migrada (ver EHR_ServicesCore\docs\migration\
	-- scheduling-cross-module-cutover-matrix.md): ya no puede leerse por SQL directo ni join
	-- cross-database desde este SP. FECHA CITA, INDICACIONES, COD VIA ADMIN, VIA ADMIN,
	-- COD UNIDAD MEDIDA y UNIDAD MEDIDA dependian del join AGASICITA -> ehr.HCORDCICLOSD ->
	-- ehr.HCORDQUIMIO -> ehr.HCORDMEDICAM y se devuelven aqui como placeholder NULL; el
	-- consumidor (Indigo-Events/Serializers/MethodsRequests.cs, EnrichChemotherapyRequestData)
	-- los completa via ISchedulingPlatformClient (cita) + lookup local a la cadena
	-- ehr.HCORDCICLOSD/HCORDQUIMIO/HCORDMEDICAM (que sigue siendo owner, no migrada).
	SELECT DISTINCT
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', '' AS 'OBSERVACION ITEM', CAST(NULL AS VARCHAR(MAX)) AS 'INDICACIONES', '' AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA',A.TIPOREGIS AS 'COD TIPO PEDIDO', CASE A.TIPOREGIS WHEN 1 THEN 'Medicamentos' WHEN 2 THEN 'Materiales e Insumos' WHEN 3 THEN 'Insumos' END AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA',
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA',
				ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', RTRIM(A.UNIDURFIJ) AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', iif(A.DOSISPROD = 0 OR A.DOSISPROD IS NULL ,A.CANPEDPRO ,A.DOSISPROD)AS 'CANTIDAD DOSIS', CAST(NULL AS VARCHAR(10)) AS 'COD VIA ADMIN', CAST(NULL AS VARCHAR(100)) AS 'VIA ADMIN',
				0 AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', CAST(NULL AS VARCHAR(10)) AS 'COD UNIDAD MEDIDA', CAST(NULL AS VARCHAR(100)) AS 'UNIDAD MEDIDA', A.FECINIDOS AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', A.NUMEFOLIO AS 'NUMEFOLIO', A.IDCITA, CAST(NULL AS DATETIME) AS 'FECHA CITA'
			FROM HCFARMEPD A
				INNER JOIN HCFARMEPC B ON A.CODCONCEC = B.CODCONCEC AND ORDENQUIMIO = 1
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.InventoryProduct F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.IDCITA is not null AND A.CANPEDPRO > 0
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene el detalle de los ítems de una solicitud de farmacia de quimioterapia, dado un número de consecutivo de orden. Combina la línea de despacho farmacéutico (medicamentos pedidos) con el encabezado de la orden médica y el nombre del producto del inventario. La cita agendada del paciente ya no se resuelve aquí: AGASICITA se migró a la base Scheduling desacoplada, por lo que este SP ya no la joinea (ni a la cadena ehr.HCORDCICLOSD/HCORDQUIMIO/HCORDMEDICAM que dependía de ella) y entrega FECHA CITA, INDICACIONES, COD VIA ADMIN, VIA ADMIN, COD UNIDAD MEDIDA y UNIDAD MEDIDA como placeholder NULL. El consumidor (Indigo-Events/Serializers/MethodsRequests.cs, EnrichChemotherapyRequestData) las completa vía ISchedulingPlatformClient (cita) y un lookup local a la cadena clínica (que sigue siendo owner, no migrada). Genera la estructura de datos necesaria para construir el JSON de integración con UNIHEALTH, incluyendo información de posología, dosis, frecuencia, urgencia y cancelación, exclusivamente para órdenes marcadas como quimioterapia y con cita asociada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'agasicita_scheduling_cutover_2026-08-14', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle de ítems (medicamentos/insumos) de una solicitud de farmacia de quimioterapia para construir el JSON enviado a UNIHEALTH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un consecutivo válido en HCFARMEPD/HCFARMEPC.; Solo se consideran encabezados marcados como orden de quimioterapia (ORDENQUIMIO = 1).; Los ítems deben estar asociados a una cita (IDCITA no nulo) y con cantidad pedida mayor a 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ítems pertenecientes a una orden marcada como quimioterapia (ORDENQUIMIO = 1).; Se excluyen ítems sin cita asociada o con cantidad pedida ≤ 0.; Los campos MULTIDOSIS y BOLO se entregan siempre en 0 (no se calculan).; Los campos de producto componente se devuelven siempre nulos/0 (no se manejan componentes).; La unidad de frecuencia y posología se mapean a un dominio cerrado de 5 valores (Minutos, Horas, Días, Semanas, Mes).; FECHA CITA, INDICACIONES, COD VIA ADMIN, VIA ADMIN, COD UNIDAD MEDIDA y UNIDAD MEDIDA se devuelven siempre NULL desde este SP (dependían del join AGASICITA -> ehr.HCORDCICLOSD/HCORDQUIMIO/HCORDMEDICAM, ya no permitido); el consumidor los completa fuera de SQL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de farmacia; Quimioterapia; Vía de administración; Unidad de medida; Posología; Frecuencia; Dosis única; Pedido urgente; Cita médica (resuelta fuera de SQL); Medicamentos; Materiales e insumos; Cancelación de ítem; Integración UNIHEALTH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna el detalle de productos de la solicitud filtrando por CODCONCEC = @Consecutivo, IDCITA NOT NULL y CANPEDPRO > 0, sólo cuando el encabezado tiene ORDENQUIMIO = 1. FECHA CITA/INDICACIONES/COD VIA ADMIN/VIA ADMIN/COD UNIDAD MEDIDA/UNIDAD MEDIDA viajan NULL; el consumidor las completa vía ISchedulingPlatformClient + lookup clínico local.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PROESTADO = 3 → Marca el ítem como CANCELADO = 1 else CANCELADO = 0; si TIPOREGIS (1/2/3) → Clasifica el pedido como Medicamentos, Materiales e Insumos o Insumos respectivamente; si Stat = 1 → Marca el ítem como URGENTE = 1 else URGENTE = 0; si DURACIDOS = ''Dosis Unica'' → Marca DOSIS UNICA = 1 else DOSIS UNICA = 0; si UNIFRECUE / UNIDURFIJ (1..5) → Traduce la unidad a Minutos, Horas, Días, Semanas o Mes; si DOSISPROD = 0 o NULL → Usa CANPEDPRO como CANTIDAD DOSIS else Usa DOSISPROD como CANTIDAD DOSIS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.INPROFSAL; Inventory.InventoryProduct; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-sonnet-5_2026-08-14_agasicita-scheduling-cutover', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaQuimioterapia';
GO
