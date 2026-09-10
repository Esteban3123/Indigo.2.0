CREATE PROCEDURE [dbo].[SP_ListarDetalleSolicitudFarmaciaNPTCabecera]
(
  @Consecutivo as Int
)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', B.INDICACIONADI AS 'OBSERVACION ITEM',RTRIM(B.INDICACIONADM) AS 'INDICACIONES','5' AS 'COD TIPO MEZCLA',
				'Nutricion' AS 'TIPO MEZCLA', '5' AS 'COD TIPO PEDIDO', 'Nutricion' AS 'TIPO PEDIDO',IIF(A.Stat=1, 1, 0) AS 'URGENTE',IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA',
				ISNULL(B.TEMPOADMIN, 0) AS 'DURACION', '2' AS 'COD UNIDAD FRECUENCIA','Horas' AS 'UNIDAD FRECUENCIA',0  AS 'POSOLOGIA',RTRIM(CAST('' AS CHAR)) AS 'COD UNIDAD POSOLOGIA', 
				'' AS 'UNIDAD POSOLOGIA', RTRIM(C.CODE) AS 'COD PRODUCTO', RTRIM(C.Name) AS 'PRODUCTO',1 AS 'CANTIDAD',B.VOLUTOTAL AS 'CANTIDAD DOSIS', B.VIADMIN AS 'COD VIA ADMIN', CASE B.VIADMIN WHEN 1 THEN 'Línea central' WHEN 2 THEN 'Línea periférica' END AS 'VIA ADMIN',
				0 AS 'MULTIDOSIS', 0 AS 'BOLO',B.VOLUTOTAL AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',(SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO',A.IdSourceTable AS 'IdSourceTable'
				, B.VELINFUSION AS 'VELOCIDAD INFUSION'
			FROM HCFARMEPD A
				INNER JOIN HCNUTPAREC B ON B.ID = A.IdSourceTable
				INNER JOIN HCPARNUTC C ON C.ID = B.IDHCPARNUTC
				INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED                
			WHERE A.CODCONCEC = @Consecutivo AND A.SourceTable = 'HCNUTPAREC' AND A.IDCITA is null
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle de los componentes nutricionales (nutrición parenteral total - NPT) asociados a una solicitud de farmacia, identificada por su número consecutivo. Combina la información del encabezado de la orden farmacéutica (HCFARMEPC), los ítems de la orden (HCFARMEPD), los parámetros de la mezcla nutricional prescrita (HCNUTPAREC), el catálogo de nutrientes (HCPARNUTC) y las unidades de medida (INUNIMEDI) para construir el detalle completo de cada componente de la mezcla. Devuelve datos como producto, dosis, vía de administración (línea central o periférica), velocidad de infusión, duración, urgencia, estado de cancelación y fecha de inicio de la orden, información necesaria para el despacho y preparación de mezclas NPT en farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la cabecera y detalle de solicitudes de farmacia tipo Nutrición Parenteral Total (NPT) asociadas a un consecutivo, devolviendo información de producto, dosis, vía y administración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFARMEPD con CODCONCEC igual al consecutivo recibido, SourceTable=''HCNUTPAREC'' e IDCITA nulo.; El IdSourceTable de HCFARMEPD debe corresponder a un registro válido en HCNUTPAREC.; El producto referenciado por HCNUTPAREC.IDHCPARNUTC debe existir en HCPARNUTC.; La unidad de medida (CODUNIMED) debe existir en INUNIMEDI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tipo de mezcla y tipo de pedido siempre se entrega fijo como ''Nutricion'' con código ''5''.; La unidad de frecuencia siempre se entrega como ''Horas'' con código ''2''.; La cantidad siempre es 1; MULTIDOSIS y BOLO siempre 0; POSOLOGIA siempre 0.; Solo se consideran solicitudes cuya tabla origen es ''HCNUTPAREC'' y que no están asociadas a una cita (IDCITA IS NULL).; La duración por defecto es 0 cuando TEMPOADMIN es nulo.; La fecha de inicio se toma de HCFARMEPC para el consecutivo recibido, independiente de la fila.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nutrición Parenteral Total (NPT); Solicitud de farmacia; Vía de administración (línea central/periférica); Dosis única; Urgencia de pedido; Velocidad de infusión; Unidad de medida; Cancelación de orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT de detalle NPT cuando A.CODCONCEC=@Consecutivo, A.SourceTable=''HCNUTPAREC'' y A.IDCITA IS NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PROESTADO = 3 → Marca el registro como CANCELADO=1 else CANCELADO=0; si Stat = 1 → Marca URGENTE=1 else URGENTE=0; si DURACIDOS = ''Dosis Unica'' → Marca DOSIS UNICA=1 else DOSIS UNICA=0; si VIADMIN = 1 → Vía de administración ''Línea central'' else Si VIADMIN=2 entonces ''Línea periférica''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCNUTPAREC; dbo.HCPARNUTC; dbo.INUNIMEDI; dbo.HCFARMEPC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmaciaNPTCabecera';
-- GO
