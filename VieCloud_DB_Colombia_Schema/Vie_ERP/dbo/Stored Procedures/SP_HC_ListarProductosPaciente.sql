
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosPaciente]
(
@Almacen char(4),
@VersionERP int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	
IF @VersionERP=1
	

	
	   
       SELECT b.PRESENMED,RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(E.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCPRESCRA A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN  dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	  INNER JOIN dbo.INPRODUC AS D ON C.IPRCODIGO=D.IPRCODIGO
       INNER JOIN dbo.INFISICO E ON D.IPRCODIGO=E.IPRCODIGO 
       WHERE E.IALCODIGO=@Almacen AND A.PREESTADO IN ('1','6')  AND B.TIPPRODUC IN ('1','3') 
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC,b.PRESENMED
       UNION 
       SELECT b.PRESENMED ,RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(E.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	  INNER JOIN dbo.INPRODUC AS D ON C.IPRCODIGO=D.IPRCODIGO
       INNER JOIN dbo.INFISICO E ON D.IPRCODIGO=E.IPRCODIGO 
       INNER JOIN dbo.HCINFLIQA F ON A.CODCONCEC=F.CODCONCEC 
       WHERE E.IALCODIGO=@Almacen   AND F.PREESTADO IN ('1','5') 
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC,b.PRESENMED
     
     
       
ELSE
		
		/*aca la consulta*/
           SELECT RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(A.CODPRODUC)+' - '+RTRIM(DESPRODUC)AS CodigoDescripcion,SUM(CAST(D.IFICANTID AS INT)) AS Disponibles,A.TIPPRODUC 
       FROM dbo.IHLISTPRO A 
       INNER JOIN dbo.IHRINDDGH AS B ON A.CODPRODUC=B.CODPRODUC
	   INNER JOIN dbo.INNPRODUC AS C ON b.IPRCODIGO=C.IPRCODIGO
       INNER JOIN dbo.INNFISICO D ON C.OID=D.INNPRODUC  
       WHERE D.INNALMACE= (SELECT OID FROM DBO.INNALMACE WHERE IALCODIGO=@Almacen)  AND A.TIPPRODUC IN ('2','3') 
       GROUP BY A.CODPRODUC,DESPRODUC,A.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(E.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCPRESCRA A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN  dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	   INNER JOIN dbo.INNPRODUC AS D ON c.IPRCODIGO=D.IPRCODIGO
       INNER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC 
       WHERE E.INNALMACE= (SELECT OID FROM DBO.INNALMACE WHERE IALCODIGO=@Almacen)  AND A.PREESTADO IN ('1','6','7')  AND B.TIPPRODUC IN ('1','3') 
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(C.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(C.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(E.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFCONC A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	   INNER JOIN dbo.INNPRODUC AS D ON c.IPRCODIGO=D.IPRCODIGO
       INNER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC 
       INNER JOIN dbo.HCINFLIQA F ON A.CODCONCEC=F.CODCONCEC 
       WHERE E.INNALMACE= (SELECT OID FROM DBO.INNALMACE WHERE IALCODIGO=@Almacen)   AND F.PREESTADO IN ('1','5') 
       GROUP BY C.CODPRODUC,DESPRODUC,B.TIPPRODUC
       UNION 
       SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,RTRIM(B.CODPRODUC)+' - '+RTRIM(DESPRODUC) AS CodigoDescripcion,SUM(CAST(E.IFICANTID AS INT)) AS Disponibles,B.TIPPRODUC 
       FROM dbo.HCINFLIQD A 
       INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
       INNER JOIN dbo.IHRINDDGH AS C ON B.CODPRODUC=C.CODPRODUC
	   INNER JOIN dbo.INNPRODUC AS D ON c.IPRCODIGO=D.IPRCODIGO
       INNER JOIN dbo.INNFISICO E ON D.OID=E.INNPRODUC 
       INNER JOIN dbo.HCINFLIQA F ON A.CODCONCEC=F.CODCONCEC 
       WHERE E.INNALMACE= (SELECT OID FROM DBO.INNALMACE WHERE IALCODIGO=@Almacen)   AND F.PREESTADO IN ('1','5')
       GROUP BY B.CODPRODUC,DESPRODUC,B.TIPPRODUC

       
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos, insumos y dispositivos médicos disponibles en un almacén específico que han sido prescritos o están relacionados con preparaciones magistrales (mezclas) de pacientes en historia clínica. Consulta las prescripciones activas (HCPRESCRA), las concentraciones de mezclas (HCINFCONC) y los líquidos asociados (HCINFLIQA), cruzándolos con el catálogo maestro de productos (IHLISTPRO) y el inventario físico disponible en el almacén indicado. Soporta dos versiones del motor de inventario del ERP (VersionERP 1 o superior), adaptando las tablas de stock físico según la versión activa. Se usa típicamente para dispensación en farmacia: muestra qué productos prescritos tienen unidades disponibles en bodega para ser entregados al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos (medicamentos e insumos) prescritos o asociados a infusiones/mezclas de un paciente con su stock disponible en un almacén, soportando dos versiones del módulo ERP de inventario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El almacén indicado debe existir en el catálogo correspondiente (IALCODIGO en INFISICO para versión 1, o existir en INNALMACE para la versión alterna).; Los productos deben estar enlazados entre el catálogo clínico (IHLISTPRO) y el maestro de inventario vía IHRINDDGH e IPRCODIGO.; Deben existir prescripciones, conceptos de infusión o detalles de liquidación con estados activos para que aparezcan resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran prescripciones en estados PREESTADO 1 y 6 (y 7 en versión nueva); las canceladas/otros estados se excluyen.; Las mezclas/infusiones solo se incluyen si la cabecera HCINFLIQA está en estado PREESTADO 1 o 5.; El stock disponible se calcula sumando IFICANTID convertido a entero, agrupado por producto.; El filtrado de stock siempre se restringe al almacén recibido como parámetro.; En versión 1 los productos del flujo de prescripción se limitan a TIPPRODUC 1 y 3 (medicamentos/insumos clínicos); en versión nueva se incluye además el catálogo con TIPPRODUC 2 y 3.; La descripción mostrada concatena código y descripción separados por '' - '' y aplica RTRIM.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prescripción médica; Productos farmacéuticos / insumos; Mezclas e infusiones intravenosas; Liquidación de infusiones; Stock/disponibilidad por almacén; Presentación del medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando VersionERP=1: devuelve la unión de productos prescritos en HCPRESCRA con PREESTADO IN (''1'',''6'') y TIPPRODUC IN (''1'',''3''), más productos de mezclas HCINFCONC ligadas a HCINFLIQA con PREESTADO IN (''1'',''5''), filtrando stock por IALCODIGO=@Almacen sobre INFISICO.; [RETURN_RESULT] resultset: Cuando VersionERP<>1: devuelve la unión de (a) todo el catálogo IHLISTPRO con TIPPRODUC IN (''2'',''3''); (b) prescripciones HCPRESCRA con PREESTADO IN (''1'',''6'',''7'') y TIPPRODUC IN (''1'',''3''); (c) mezclas HCINFCONC con HCINFLIQA.PREESTADO IN (''1'',''5''); (d) detalles HCINFLIQD con HCINFLIQA.PREESTADO IN (''1'',''5''), todo filtrando stock por INNALMACE correspondiente al @Almacen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @VersionERP = 1 → Consulta inventario clásico (INPRODUC/INFISICO) e incluye solo prescripciones (HCPRESCRA estados 1,6) y mezclas (HCINFCONC con HCINFLIQA estados 1,5); incluye columna PRESENMED. else Consulta inventario nuevo (INNPRODUC/INNFISICO/INNALMACE), añade catálogo general TIPPRODUC IN (''2'',''3''), amplía prescripciones a estado 7 y agrega detalles de liquidación HCINFLIQD; no incluye PRESENMED.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.IHRINDDGH; dbo.INPRODUC; dbo.INFISICO; dbo.HCINFCONC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.INNPRODUC; dbo.INNFISICO; dbo.INNALMACE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosPaciente';
-- GO
