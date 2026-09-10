

CREATE PROCEDURE [dbo].[SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos]
(
@Paciente VARCHAR(25),
@Ingreso CHAR(10), 
@Almacen CHAR(4),
@VersionERP INT
)
AS
BEGIN
	SET NOCOUNT ON;
IF @VersionERP=1
		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Ordenados' AS Tipo 
		FROM dbo.HCPRESCRA A INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
		WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 
		AND ESPDILPRO='0' AND MANEXTPRO='0' AND PREESTADO IN ('1','6') 
		UNION 
		SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Todos' AS Tipo 
		FROM dbo.IHLISTPRO A 
		INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
		INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
		WHERE TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0' AND C.IALCODIGO=@Almacen
		GROUP BY A.CODPRODUC, DESPRODUC
ELSE IF @VersionERP=2
	
	
	SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Ordenados' AS Tipo 
	FROM dbo.HCPRESCRA A INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
	WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 
	AND ESPDILPRO='0' AND MANEXTPRO='0' AND PREESTADO IN ('1','6') 
	UNION 
	SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Todos' AS Tipo 
	FROM dbo.IHLISTPRO A 
	INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
	INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID 
	WHERE TIPPRODUC IN ('1','3') AND PROESTADO = 1 AND ESPDILPRO='0' AND E.IALCODIGO=@Almacen
	GROUP BY A.CODPRODUC, DESPRODUC

ELSE

	SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,'Ordenados' AS Tipo 
		FROM dbo.HCPRESCRA A INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
		WHERE IPCODPACI=@Paciente AND NUMINGRES=@Ingreso AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 
		AND ESPDILPRO='0' AND MANEXTPRO='0' AND PREESTADO IN ('1','6') 
		UNION 
		SELECT  Codigo, Medicamento, 'Todos' AS Tipo
		FROM (
		SELECT  MAX(B.CodeCUM)AS CodeCUM,RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento, D.NOPOSPROD AS 'NO POS',D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES AS JustificacionMedicamentosEspeciales
				FROM Inventory.InventoryProduct AS B
				INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
				INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
				WHERE  D.TIPPRODUC IN ('1','3') AND D.PROESTADO = 1 AND D.ESPDILPRO='0' 
				GROUP BY D.CODPRODUC,D.DESPRODUC,D.NOPOSPROD,D.TIPPRODUC,D.TIPFORMED,D.CODGRUFAR,D.CODJUMEES
		) AS PRO
				LEFT JOIN (
				SELECT ATC.Code,Quantity FROM Inventory.PhysicalInventory phy
				INNER JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
				INNER JOIN Inventory.InventoryProduct invpro ON invpro.Id = phy.ProductId
				INNER JOIN Inventory.ATC atc ON atc.Id = invpro.ATCId
				WHERE E.Code = @Almacen  AND invpro.Status = 1 AND atc.DiluentProduct = 0) AS INV ON PRO.Codigo = INV.Code
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos disponibles para la administración de enfermería en el calendario de solicitudes de un paciente hospitalizado. Combina dos conjuntos de información: los medicamentos ya ordenados/prescritos para el paciente en su ingreso actual (obtenidos de las prescripciones activas en HCPRESCRA), y el catálogo completo de medicamentos e insumos disponibles en el almacén o bodega indicada (obtenidos del maestro de productos IHLISTPRO cruzado con el inventario físico). Soporta tres versiones del ERP mediante el parámetro @VersionERP, adaptando la consulta de inventario físico según la estructura de tablas de cada versión. Se usa en el módulo de enfermería para que el profesional pueda seleccionar medicamentos al programar o registrar la administración en el calendario de dosis de un paciente internado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos prescritos a un paciente en un ingreso hospitalario y, adicionalmente, todos los medicamentos disponibles en un almacén, adaptando la consulta al modelo de datos según la versión del ERP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir en HCPRESCRA para obtener prescripciones (de lo contrario solo se obtienen ''Todos'').; El almacén debe existir en el modelo de inventario correspondiente a la versión del ERP (INFISICO, INNALMACE o Inventory.Warehouse).; Los productos deben estar activos (PROESTADO=1) y no ser diluyentes (ESPDILPRO=''0'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuyo TIPPRODUC esté en (''1'',''3'').; Solo se incluyen productos activos (PROESTADO=1).; Se excluyen productos marcados como diluyentes (ESPDILPRO=''0'' o DiluentProduct=0).; Las prescripciones consideradas son únicamente las de manejo intrahospitalario (MANEXTPRO=''0'') y con estado de prescripción en (''1'',''6'').; Cada fila del resultado se etiqueta como ''Ordenados'' (prescritas al paciente) o ''Todos'' (disponibles en almacén).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción de medicamentos; Medicamento; Almacén/Bodega; Inventario físico; Clasificación ATC; Diluyente; Medicamento POS/NO POS; Justificación de medicamentos especiales; Forma farmacéutica; Grupo farmacéutico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve dos conjuntos unidos: medicamentos prescritos al paciente marcados como ''Ordenados'' y medicamentos del almacén marcados como ''Todos''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta inventario clásico usando IHRINDDGH e INFISICO filtrando por IALCODIGO=@Almacen.; si @VersionERP = 2 → Consulta inventario nuevo usando INNPRODUC, INNFISICO e INNALMACE filtrando por IALCODIGO=@Almacen.; si @VersionERP distinto de 1 y 2 → Consulta el modelo Inventory (InventoryProduct, ATC, PhysicalInventory, Warehouse) filtrando por Warehouse.Code=@Almacen, Status=1 y excluyendo diluyentes (DiluentProduct=0); incluye atributos POS, forma médica, grupo farmacéutico y justificación de medicamentos especiales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; Inventory.InventoryProduct; Inventory.ATC; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPENF_ListarMedicamentosPacienteCalendarioSolicitudTodos';
-- GO
