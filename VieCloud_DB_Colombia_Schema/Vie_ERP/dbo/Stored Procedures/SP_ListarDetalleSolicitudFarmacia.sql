CREATE PROCEDURE [dbo].[SP_ListarDetalleSolicitudFarmacia]
(
  @Consecutivo as Int
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', C.INDAPLMED AS 'OBSERVACION ITEM', RTRIM(C.DESADMINI) AS 'INDICACIONES', '' AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', '1' AS 'COD TIPO PEDIDO', 'Medicamentos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(C.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(C.VALDURFIJ, 0) AS 'DURACION', C.UNIDURFIJ AS 'COD UNIDAD FRECUENCIA', 
				CASE C.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA',
				ISNULL(C.FRECUENCI, 0)  AS 'POSOLOGIA', RTRIM(C.UNIFRECUE) AS 'COD UNIDAD POSOLOGIA', CASE C.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.AbbreviationName) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', C.DOSISPROD AS 'CANTIDAD DOSIS', 
				C.CODVIAADM AS 'COD VIA ADMIN', RTRIM(G.DESVIAADM) AS 'VIA ADMIN', IIF(F.Multidose = 1, 1,0) AS 'MULTIDOSIS', 0 AS 'BOLO', 
				IIF(C.DOSISPROD IS NOT NULL, C.DOSISPROD,A.DOSISPROD) AS 'DOSIS', IIF(C.CODUNIMED IS NOT NULL, C.CODUNIMED,A.CODUNIMED) AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',(SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', C.NUMEFOLIO AS 'NUMEFOLIO'
			FROM HCFARMEPD A																											   
				INNER JOIN HCFARMEPC B ON A.CODCONCEC = B.CODCONCEC
				INNER JOIN HCPRESCRA C ON A.IdSourceTable= C.ID AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN HCVIAADMI G ON C.CODVIAADM = G.CODVIAADM
				INNER JOIN INUNIMEDI H ON C.CODUNIMED = H.CODUNIMED
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.SourceTable = 'HCPRESCRA' AND A.IDCITA is null AND A.CANPEDPRO > 0

UNION ALL

			SELECT 
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', B.INDAPLMED AS 'OBSERVACION ITEM', RTRIM(B.ADMMEZLIQ) AS 'INDICACIONES', CASE B.TIPMEZLIQ WHEN 1 THEN '1' WHEN 3 THEN '2' WHEN 4 THEN '3' END AS 'COD TIPO MEZCLA', CASE B.TIPMEZLIQ WHEN 1 THEN 'Mezcla Continua' WHEN 3 THEN 'Mezcla Frecuencia' WHEN 4 THEN 'Mezcla Magistral' END AS 'TIPO MEZCLA',
				A.TIPOREGIS AS 'COD TIPO PEDIDO', CASE A.TIPOREGIS WHEN 1 THEN 'Medicamentos' WHEN 2 THEN 'Materiales e Insumos' END AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(B.TIPODURACION = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', IIF(C.VADUBOMEZ IS NOT NULL,ISNULL(C.VADUBOMEZ, 0),IIF(B.DURACIONFIJA IS NOT NULL,ISNULL(B.DURACIONFIJA, 0),IIF(B.DURINFUSION IS NOT NULL,ISNULL(B.DURINFUSION, 0),ISNULL(B.DURFRECUENCIA, 0)))) AS 'DURACION',IIF(C.UNDUBOMEZ IS NOT NULL,CAST(C.UNDUBOMEZ AS CHAR),IIF(B.UNIDADDURFIJA IS NOT NULL,CAST(B.UNIDADDURFIJA AS CHAR), IIF(B.UNIDADFRECUENCIA IS NOT NULL, CAST(B.UNIDADFRECUENCIA AS CHAR), CAST(B.UNIDADINFUSION AS CHAR)))) AS 'COD UNIDAD FRECUENCIA', 
				IIF(C.UNDUBOMEZ IS NOT NULL,CASE C.UNDUBOMEZ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END,IIF(B.UNIDADDURFIJA IS NOT NULL,CASE B.UNIDADDURFIJA WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END,IIF(B.UNIDADFRECUENCIA IS NOT NULL,CASE B.UNIDADFRECUENCIA WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END,CASE B.UNIDADINFUSION WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END))) AS 'UNIDAD FRECUENCIA', 
				IIF(B.TIPMEZLIQ = 1,null,ISNULL(B.DURFRECUENCIA, 0))  AS 'POSOLOGIA',IIF(B.TIPMEZLIQ = 1,null, RTRIM(CAST(B.UNIDADFRECUENCIA AS CHAR))) AS 'COD UNIDAD POSOLOGIA', IIF(B.TIPMEZLIQ = 1,null,CASE B.UNIDADFRECUENCIA WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END) AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', A.DOSISPROD AS 'CANTIDAD DOSIS', ISNULL(C.CODVIABOM,C.VIAADMDIL) AS 'COD VIA ADMIN',
				RTRIM(G.DESVIAADM) AS 'VIA ADMIN', F.Multidose AS 'MULTIDOSIS', CASE WHEN C.ESBOLMEDL = 1 THEN 1 ELSE CASE WHEN C.ESBOLOMEZ = 1 THEN 1 ELSE CASE WHEN C.ESBOLMEDM = 1 THEN 1 ELSE 0 END END END AS 'BOLO', 
				A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA',(SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', B.NUMEFOLIO AS 'NUMEFOLIO'
			FROM HCFARMEPD A
				INNER JOIN HCINFLIQA B ON A.IdSourceTable = b.CONSECUTI
				LEFT JOIN HCINFLIQD C ON B.CODCONCEC = C.CODCONCEC --AND C.CODPRODUC = A.CODPRODUC
				LEFT JOIN HCINFCONC E ON B.CODCONCEC = E.CODCONCEC AND E.CODPRODUC = A.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				LEFT JOIN HCVIAADMI G ON ISNULL(C.CODVIABOM,C.VIAADMDIL) = G.CODVIAADM
				LEFT JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.SourceTable = 'HCINFLIQA' AND B.TIPMEZLIQ <> 2 AND A.IDCITA is null AND A.CANPEDPRO > 0

UNION ALL

			SELECT 
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', C.JUSTIINSU AS 'OBSERVACION ITEM', NULL AS 'INDICACIONES', '' AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', '3' AS 'COD TIPO PEDIDO', 'Insumos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', 
				ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', RTRIM(A.UNIDURFIJ) AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', A.CANPEDPRO AS 'CANTIDAD DOSIS', NULL AS 'COD VIA ADMIN', NULL AS 'VIA ADMIN', 
				0 AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', NULL AS 'UNIDAD MEDIDA', (SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', A.NUMEFOLIO AS 'NUMEFOLIO'
			FROM HCFARMEPD A
				INNER JOIN HCSOLINSC B ON A.IdSourceTable= B.ID 
				INNER JOIN HCSOLINSD C ON B.CODCONCEC = C.CODCONCEC AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL 
				INNER JOIN Inventory.InventoryProduct F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.SourceTable = 'HCSOLINSD' AND A.IDCITA is null AND A.CANPEDPRO > 0

UNION ALL

			SELECT 
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', B.INDAPLMED AS 'OBSERVACION ITEM', RTRIM(C.ADMMEZLIQ) AS 'INDICACIONES', '4' AS 'COD TIPO MEZCLA', 'Liquido' AS 'TIPO MEZCLA', 
				A.TIPOREGIS AS 'COD TIPO PEDIDO', CASE A.TIPOREGIS WHEN 1 THEN 'Medicamentos' WHEN 2 THEN 'Materiales e Insumos' END AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(B.TIPODURACION = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', IIF(C.VADUBOLIQUIDOS is null,ISNULL(C.VALDURINF, 0),ISNULL(C.VADUBOLIQUIDOS, 0)) AS 'DURACION', IIF(C.UNDUBOLIQUIDOS IS NULL,C.UNIDURINF, C.UNDUBOLIQUIDOS) AS 'COD UNIDAD FRECUENCIA', 
				IIF(C.UNDUBOLIQUIDOS IS NULL,CASE C.UNIDURINF WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END,CASE C.UNDUBOLIQUIDOS WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END) AS 'UNIDAD FRECUENCIA', 
				ISNULL(C.FRECUEINF, 0)  AS 'POSOLOGIA', RTRIM(C.UNIFREINF) AS 'COD UNIDAD POSOLOGIA', CASE C.UNIFREINF WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD',IIF(C.DOSISINFU IS NOT NULL, C.DOSISINFU,A.DOSISPROD) AS 'CANTIDAD DOSIS',ISNULL(C.CODVIABOM,C.VIAADMDIL) AS 'COD VIA ADMIN', 
				RTRIM(G.DESVIAADM) AS 'VIA ADMIN', F.Multidose AS 'MULTIDOSIS', C.ESBOLMEDL AS 'BOLO', IIF(C.DOSISINFU IS NOT NULL, C.DOSISINFU,A.DOSISPROD) AS 'DOSIS', IIF(C.UNIMEDINF IS NOT NULL, C.UNIMEDINF,A.CODUNIMED) AS 'COD UNIDAD MEDIDA',
				H.DESUNIMED AS 'UNIDAD MEDIDA', (SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', B.NUMEFOLIO AS 'NUMEFOLIO'
			FROM HCFARMEPD A
				INNER JOIN HCINFLIQA B ON A.IdSourceTable = b.CONSECUTI--A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES AND A.NUMEFOLIO = B.NUMEFOLIO 
				INNER JOIN HCINFLIQD C ON B.CODCONCEC_ORIGEN = C.CODCONCEC AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL
				INNER JOIN Inventory.ATC F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
				LEFT JOIN HCVIAADMI G ON ISNULL(C.CODVIABOM,C.VIAADMDIL) = G.CODVIAADM
				LEFT JOIN INUNIMEDI H ON IIF(C.UNIMEDINF IS NOT NULL, C.UNIMEDINF,A.CODUNIMED) = H.CODUNIMED
			WHERE A.CODCONCEC = @Consecutivo AND B.TIPMEZLIQ = 2 AND A.IDCITA is null AND A.CANPEDPRO > 0

UNION ALL
			SELECT 
				IIF(A.PROESTADO = 3, 1, 0) AS 'CANCELADO', '' AS 'OBSERVACION ITEM', NULL AS 'INDICACIONES', '' AS 'COD TIPO MEZCLA', '' AS 'TIPO MEZCLA', '3' AS 'COD TIPO PEDIDO', 'Insumos' AS 'TIPO PEDIDO', IIF(A.Stat=1, 1, 0) AS 'URGENTE',
				IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA', ISNULL(A.FRECUENCI, 0) AS 'DURACION', A.UNIFRECUE AS 'COD UNIDAD FRECUENCIA', 
				CASE A.UNIFRECUE WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD FRECUENCIA', 
				ISNULL(A.VALDURFIJ, 0)  AS 'POSOLOGIA', RTRIM(A.UNIDURFIJ) AS 'COD UNIDAD POSOLOGIA', CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Mes' END AS 'UNIDAD POSOLOGIA',
				RTRIM(A.CODPRODUC) AS 'COD PRODUCTO', RTRIM(F.Name) AS 'PRODUCTO', A.CANPEDPRO AS 'CANTIDAD', A.CANPEDPRO AS 'CANTIDAD DOSIS', NULL AS 'COD VIA ADMIN', NULL AS 'VIA ADMIN', 
				0 AS 'MULTIDOSIS', 0 AS 'BOLO', A.DOSISPROD AS 'DOSIS', A.CODUNIMED AS 'COD UNIDAD MEDIDA', NULL AS 'UNIDAD MEDIDA', (SELECT FECHAORDE FROM HCFARMEPC WHERE CODCONCEC = @Consecutivo) AS 'FECHA INICIO', NULL AS 'COD PRODUCTO COMPONENTE', NULL AS 'PRODUCTO COMPONENTE', 0 AS 'CANTIDAD COMPONENTE', NULL AS 'COD UNIDAD MEDIDA COMPONENTE', NULL AS 'UNIDAD MEDIDA COMPONENTE'
				, A.Id as 'Id', A.NUMEFOLIO AS 'NUMEFOLIO'
			FROM HCFARMEPD A
				--INNER JOIN HCSOLINSD C ON A.NUMINGRES = C.NUMINGRES AND A.CODPRODUC = C.CODPRODUC
				INNER JOIN INPROFSAL D ON A.CODPROSAL = D.CODPROSAL 
				INNER JOIN Inventory.InventoryProduct F ON A.CODPRODUC = F.Code
				INNER JOIN INUNIFUNC I ON I.UFUCODIGO = A.UFUCODIGO
			WHERE A.CODCONCEC = @Consecutivo AND A.SourceTable = 'HCPRESCRA' AND A.IdSourceTable IS NULL AND A.IDCITA is null AND A.CANPEDPRO > 0
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una solicitud de despacho farmacéutico dado un número de consecutivo de orden (encabezado de farmacia). Consolida mediante tres bloques UNION ALL los distintos tipos de ítems que puede contener una solicitud: medicamentos prescritos en historia clínica (HCPRESCRA), mezclas intravenosas o de infusión (HCINFLIQA) e insumos/materiales. Para cada ítem retorna información de negocio como: estado de cancelación, urgencia (Stat), indicaciones y observaciones, tipo de pedido (medicamento, insumo, mezcla continua/frecuencia/magistral), posología, frecuencia y duración del tratamiento, dosis única, vía de administración, nombre y código del producto (desde el catálogo ATC de inventario), cantidad pedida y de dosis, unidad de medida, unidad funcional (servicio o sala), profesional prescriptor y fecha de la orden. Este procedimiento es utilizado por el módulo de farmacia para presentar o transmitir el detalle de cada solicitud de despacho antes de su preparación y entrega.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle consolidado de los ítems (medicamentos, mezclas, líquidos e insumos) de una solicitud de farmacia identificada por su consecutivo, unificando distintas fuentes de prescripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un consecutivo válido en HCFARMEPC/HCFARMEPD que agrupe los ítems a listar.; Los ítems deben tener cantidad pedida mayor a 0 (CANPEDPRO > 0) y no estar asociados a una cita (IDCITA IS NULL).; Cada ítem debe tener producto registrado en Inventory.ATC o Inventory.InventoryProduct según corresponda.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ítems con CANPEDPRO > 0.; Solo se incluyen ítems sin cita asociada (IDCITA IS NULL).; Todos los ítems pertenecen al consecutivo solicitado (A.CODCONCEC = @Consecutivo).; La FECHA INICIO siempre proviene de HCFARMEPC.FECHAORDE del mismo consecutivo.; Los códigos de unidades de tiempo se estandarizan al dominio {Minutos, Horas, Dias, Semanas, Mes}.; Los componentes (COD/PRODUCTO COMPONENTE y unidad/cantidad componente) siempre se devuelven NULL/0 en este SP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de farmacia; Prescripción de medicamentos; Mezclas (continua, frecuencia, magistral, líquido); Insumos médicos; Vía de administración; Posología y frecuencia; Duración del tratamiento; Dosis única; Bolo; Multidosis; Urgencia del pedido; Cancelación de ítem; Unidad funcional; Profesional de la salud; Folio clínico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un único resultset con la unión de hasta 5 categorías de ítems: prescripciones (HCPRESCRA), mezclas no líquidas (HCINFLIQA con TIPMEZLIQ<>2), insumos (HCSOLINSD), líquidos (HCINFLIQA con TIPMEZLIQ=2) e insumos sin origen (SourceTable=''HCPRESCRA'' con IdSourceTable NULL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.SourceTable = ''HCPRESCRA'' y existe IdSourceTable → Se trata como pedido de Medicamentos (COD TIPO PEDIDO=''1''), join con HCPRESCRA para obtener indicaciones, vía y unidades.; si A.SourceTable = ''HCINFLIQA'' y B.TIPMEZLIQ <> 2 → Se trata como mezcla; el TIPO MEZCLA se mapea: 1=Mezcla Continua, 3=Mezcla Frecuencia, 4=Mezcla Magistral; POSOLOGIA es null si TIPMEZLIQ=1.; si B.TIPMEZLIQ = 2 → Se trata como Líquido (COD TIPO MEZCLA=''4''), usando datos de infusión (VALDURINF/UNIDURINF/FRECUEINF) y join con HCINFLIQD por CODCONCEC_ORIGEN.; si A.SourceTable = ''HCSOLINSD'' → Se trata como Insumos (COD TIPO PEDIDO=''3''), tomando justificación e información desde HCSOLINSD/HCSOLINSC.; si A.SourceTable = ''HCPRESCRA'' y A.IdSourceTable IS NULL → Se trata como Insumo sin origen de prescripción (COD TIPO PEDIDO=''3''), usando solo datos de HCFARMEPD e InventoryProduct.; si A.PROESTADO = 3 → El ítem se marca como CANCELADO=1; en caso contrario CANCELADO=0.; si A.Stat = 1 → El ítem se marca como URGENTE=1.; si DURACIDOS/TIPODURACION = ''Dosis Unica'' → El ítem se marca como DOSIS UNICA=1.; si C.ESBOLMEDL=1 OR C.ESBOLOMEZ=1 OR C.ESBOLMEDM=1 (en mezclas) → El ítem se marca como BOLO=1.; si Unidad de frecuencia/posología en {1,2,3,4,5} → Se traduce a Minutos, Horas, Días, Semanas o Mes respectivamente.; si TIPOREGIS en mezclas/líquidos → 1=Medicamentos, 2=Materiales e Insumos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.HCPRESCRA; dbo.INPROFSAL; Inventory.ATC; dbo.HCVIAADMI; dbo.INUNIMEDI; dbo.INUNIFUNC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.HCINFCONC; dbo.HCSOLINSC; dbo.HCSOLINSD; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDetalleSolicitudFarmacia';
-- GO
