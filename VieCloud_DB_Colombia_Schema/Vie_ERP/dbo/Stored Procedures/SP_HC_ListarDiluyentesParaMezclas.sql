
CREATE PROCEDURE [dbo].[SP_HC_ListarDiluyentesParaMezclas]
(
@Almacen char(4),
@VersionERP int
)
AS
BEGIN
SET NOCOUNT ON;

IF @VersionERP=1

	 SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,RTRIM(CODUNIVOL) AS CODUNIVOL,RTRIM(ABRPROMEZ) AS AbreviaturaProducto, PESTOTMED, CODUNIPES, VOLTOTMED, TIEESTMED ,RTRIM(A.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,A.NOPOSPROD AS NOPOS,CAST(0 AS BIT) AS FormatoNOPOS, SUM(CAST(C.IFICANTID AS INT)) AS Disponibles
	 ,CAST(0 AS BIT) TODASPATO, Conditioned = CAST(0 AS BIT), UNIRS = 'No', PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
	 FROM dbo.IHLISTPRO A 
	 INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	 INNER JOIN dbo.INFISICO C ON B.IPRCODIGO=C.IPRCODIGO 
	 WHERE C.IALCODIGO=@Almacen AND TIPPRODUC IN ('1','3') AND ESPDILPRO=1 AND PROESTADO = 1
	 GROUP BY A.CODPRODUC, DESPRODUC,CODUNIVOL,ABRPROMEZ, PESTOTMED, CODUNIPES, VOLTOTMED, TIEESTMED,NOPOSPROD
	 
ELSE IF @VersionERP=2
	
	SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,RTRIM(CODUNIVOL) AS CODUNIVOL,RTRIM(ABRPROMEZ) AS AbreviaturaProducto, PESTOTMED, CODUNIPES, VOLTOTMED, TIEESTMED ,RTRIM(A.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,A.NOPOSPROD AS NOPOS,CAST(0 AS BIT) AS FormatoNOPOS,A.CODDCIMED AS 'DCI', SUM(CAST(D.IFICANTID AS INT)) AS Disponibles
	,CAST(0 AS BIT) TODASPATO, Conditioned = CAST(0 AS BIT), UNIRS = 'No', PBS = CASE WHEN NOPOSPROD = 1 THEN 'No' ELSE 'Si' END
	 FROM dbo.IHLISTPRO A 
	 INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	 INNER JOIN dbo.INNPRODUC C ON B.IPRCODIGO=C.IPRCODIGO 
	 INNER JOIN dbo.INNFISICO D ON D.INNPRODUC=C.OID 
	 INNER JOIN dbo.INNALMACE E ON D.INNALMACE=E.OID 
	 WHERE E.IALCODIGO=@Almacen AND TIPPRODUC IN ('1','3') AND ESPDILPRO=1 AND PROESTADO = 1
	 GROUP BY A.CODPRODUC, DESPRODUC,CODUNIVOL,ABRPROMEZ, PESTOTMED, CODUNIPES, VOLTOTMED, TIEESTMED,NOPOSPROD,A.CODDCIMED 
	
ELSE
	/*SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,RTRIM(D.CODUNIVOL) AS CODUNIVOL,RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto, 
	D.PESTOTMED, D.CODUNIPES, D.VOLTOTMED, D.TIEESTMED ,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,D.NOPOSPROD AS NOPOS,
	CAST(0 AS BIT) AS FormatoNOPOS
	FROM Inventory.PhysicalInventory AS A 
	INNER JOIN Inventory.InventoryProduct AS B ON B.Id =  A.ProductId
	INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
	INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
	INNER JOIN Inventory.Warehouse AS E ON A.WarehouseId = E.Id
	where D.TIPPRODUC IN ('1','3') AND D.PROESTADO = 1 AND D.ESPDILPRO='1' AND E.Code = @Almacen
	GROUP BY D.CODPRODUC, D.DESPRODUC,D.CODUNIVOL,D.ABRPROMEZ, D.PESTOTMED, D.CODUNIPES, D.VOLTOTMED, D.TIEESTMED,D.NOPOSPROD*/

	select  Codigo,Medicamento,CODUNIVOL,AbreviaturaProducto,PESTOTMED,CODUNIPES,VOLTOTMED,TIEESTMED,CodigoDescripcion,[NOPOS],FormatoNOPOS,DCI, isnull(Sum(isnull(Quantity,0)),0) as Disponibles, PRO.TODASPATO
	, PRO.Conditioned, PRO.UNIRS, PRO.PBS,PRO.RequireStability
	from (
		SELECT  RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.DESPRODUC) AS Medicamento,RTRIM(D.CODUNIVOL) AS CODUNIVOL,RTRIM(D.ABRPROMEZ) AS AbreviaturaProducto, 
			D.PESTOTMED, D.CODUNIPES, D.VOLTOTMED, D.TIEESTMED ,RTRIM(D.CODPRODUC)+' - '+RTRIM(D.DESPRODUC) AS CodigoDescripcion,D.NOPOSPROD AS NOPOS,CAST(0 AS BIT) AS FormatoNOPOS, D.CODDCIMED AS 'DCI',
			D.TODASPATO, C.Conditioned, UNIRS = CASE WHEN C.UNIRS = 1 THEN 'Si' ELSE 'No' END
			, PBS = CASE WHEN C.Conditioned = 1 THEN 'Condicionado'
				WHEN D.NOPOSPROD = 1 THEN 'No'
				ELSE 'Si'
				END, F.RequireStability
		FROM Inventory.InventoryProduct AS B 
				INNER JOIN Inventory.ATC AS C ON C.Id = B.ATCId
				INNER JOIN dbo.IHLISTPRO AS D ON D.CODPRODUC = C.Code
				LEFT JOIN dbo.IHFORMEDI F on F.CODFORMED = D.CODFORMED 
		where D.TIPPRODUC IN ('1','3') AND D.PROESTADO = 1 AND D.ESPDILPRO='1' 
	GROUP BY D.CODPRODUC, D.DESPRODUC,D.CODUNIVOL,D.ABRPROMEZ, D.PESTOTMED, D.CODUNIPES, D.VOLTOTMED, D.TIEESTMED,D.NOPOSPROD, D.CODDCIMED, D.TODASPATO,C.Conditioned,C.UNIRS, F.RequireStability
	) as PRO
		left join (
		select ATC.Code,Quantity from Inventory.PhysicalInventory phy
		INNER JOIN Inventory.Warehouse AS E ON phy.WarehouseId = E.Id
		inner join Inventory.InventoryProduct invpro on invpro.Id = phy.ProductId
		inner join Inventory.ATC atc on atc.Id = invpro.ATCId
		where  E.Code = @Almacen and 
		 invpro.Status = 1 and atc.DiluentProduct = 1 ) as INV on PRO.Codigo = INV.Code
	GROUP BY Codigo,Medicamento,CODUNIVOL,AbreviaturaProducto,PESTOTMED,CODUNIPES,VOLTOTMED,TIEESTMED,CodigoDescripcion,[NOPOS],FormatoNOPOS,DCI, PRO.TODASPATO, PRO.Conditioned, PRO.UNIRS, PRO.PBS,PRO.RequireStability
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos diluyentes disponibles en un almacén específico, para ser usados en la preparación de mezclas intravenosas o farmacéuticas. Consulta el catálogo maestro de productos (IHLISTPRO) filtrando únicamente los productos de tipo medicamento o solución (tipos 1 y 3) marcados explícitamente como diluyentes (ESPDILPRO=1) y en estado activo, cruzando con el inventario físico para calcular las unidades disponibles en el almacén indicado. Devuelve para cada diluyente su código, nombre, abreviatura, unidades de volumen y peso, tiempo de estabilidad, condición PBS (Plan de Beneficios en Salud / No PBS / Condicionado), indicador de unidosis (UNIRS), si requiere estabilidad, y la cantidad disponible en stock. Soporta tres versiones del motor de inventario del ERP (versión 1, versión 2 y versión 3/Inventory schema) para mantener compatibilidad con distintas instalaciones del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos marcados como diluyentes aptos para preparación de mezclas en un almacén dado, con su disponibilidad en inventario, según la versión del ERP en uso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe indicarse el código de almacén/bodega a consultar.; Debe indicarse la versión de ERP (1, 2 u otra) para seleccionar el origen de inventario.; Los productos deben existir en el catálogo IHLISTPRO con TIPPRODUC en (''1'',''3''), ESPDILPRO=1 y PROESTADO=1 para ser considerados diluyentes activos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuyo tipo es ''1'' o ''3'', estén activos (PROESTADO=1) y marcados explícitamente como diluyentes (ESPDILPRO=1).; El filtro por almacén siempre se aplica al inventario, nunca al catálogo de productos.; La disponibilidad nunca es nula: se aplica ISNULL/SUM para garantizar 0 cuando no hay existencias (rama por defecto).; Los flags TODASPATO, FormatoNOPOS y Conditioned se devuelven como BIT 0 en versiones 1 y 2 (no se calculan).; PBS se deriva determinísticamente de NOPOSPROD y, en la rama moderna, también de Conditioned.; En la rama por defecto, solo cuentan como disponibles los productos cuyo ATC está marcado como DiluentProduct=1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Diluyente; Mezcla / preparación magistral; Medicamento POS / NO POS; PBS (Plan de Beneficios en Salud); DCI (Denominación Común Internacional); Clasificación ATC; Inventario físico por almacén/bodega; Estabilidad de medicamento (RequireStability); Producto condicionado (Conditioned); UNIRS (Unidosis)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @VersionERP=1, retorna diluyentes desde IHLISTPRO+IHRINDDGH+INFISICO filtrando por almacén, TIPPRODUC IN (''1'',''3''), ESPDILPRO=1 y PROESTADO=1, sumando IFICANTID como Disponibles.; [RETURN_RESULT] resultset: Cuando @VersionERP=2, retorna diluyentes desde IHLISTPRO+IHRINDDGH+INNPRODUC+INNFISICO+INNALMACE con los mismos filtros e incluye además el código DCI (CODDCIMED).; [RETURN_RESULT] resultset: Cuando @VersionERP no es 1 ni 2, retorna diluyentes desde el modelo Inventory.* (InventoryProduct/ATC/IHLISTPRO/IHFORMEDI) cruzando con PhysicalInventory por bodega y considerando solo ATC con DiluentProduct=1 e InventoryProduct.Status=1.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta el inventario clásico en INFISICO para construir la lista de diluyentes y disponibles. else Evalúa siguiente rama; si @VersionERP = 2 → Consulta el inventario en INNPRODUC/INNFISICO/INNALMACE e incluye la columna DCI (CODDCIMED). else Cae a la rama del modelo Inventory.* moderno; si NOPOSPROD = 1 (versiones 1 y 2) → Marca PBS = ''No'' else PBS = ''Si''; si C.Conditioned = 1 (versión por defecto) → Marca PBS = ''Condicionado'' else Si NOPOSPROD=1 entonces PBS=''No'', en caso contrario PBS=''Si''; si C.UNIRS = 1 (versión por defecto) → UNIRS = ''Si'' else UNIRS = ''No''; si atc.DiluentProduct = 1 e invpro.Status = 1 (versión por defecto) → Solo se suman al Disponible los productos ATC marcados como diluyentes y activos en el almacén indicado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INFISICO; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE; dbo.IHFORMEDI; Inventory.PhysicalInventory; Inventory.InventoryProduct; Inventory.ATC; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDiluyentesParaMezclas';
-- GO
