
create VIEW [dbo].[ViewNursingProceduresDetail]
AS

SELECT CAST(FECHAUTIL AS CHAR)+ C.CODACTENF  +CAST(CANACTENF as char) AS Llave
,RTRIM(DESPRODUC) AS Producto
,B.CODPRODUC AS CodigoProducto
,CANUTIPRO AS CantidadProducto
, NUMINGRES
, IPCODPACI
, CODCENATE
,FECHAUTIL
,C.CODACTENF
,CANACTENF
FROM dbo.HCHOGASIN A 
INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
INNER JOIN dbo.HCACTENFE C ON A.CODACTENF=C.CODACTENF 
--WHERE A.NUMINGRES='' AND A.CODCENATE='' AND A.IPCODPACI=''
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de procedimientos e insumos aplicados por enfermería durante la atención hospitalaria de un paciente. Combina el registro de consumo de actividades y productos de enfermería (HCHOGASIN) con el catálogo de productos farmacéuticos y dispositivos médicos (IHLISTPRO) y el catálogo de actividades de enfermería (HCACTENFE), para obtener por cada ingreso el nombre del producto, su código, las cantidades utilizadas, la fecha de utilización y la actividad de enfermería asociada. Sirve para reportería clínica y de costos de enfermería, permitiendo consultar qué insumos o procedimientos fueron aplicados a un paciente específico (identificado por cédula y número de ingreso) en un centro de atención determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewNursingProceduresDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewNursingProceduresDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de procedimientos e insumos de enfermería consumidos por paciente, enriqueciendo el consumo con la descripción del producto y la actividad asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los registros de consumo deben tener un código de producto válido presente en el catálogo de productos.; Los registros de consumo deben tener un código de actividad de enfermería válido presente en el catálogo de actividades de enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone consumos cuyo producto exista en el catálogo maestro de productos (IHLISTPRO) y cuya actividad de enfermería exista en el catálogo de actividades (HCACTENFE), por uso de INNER JOIN.; Construye una llave compuesta concatenando fecha de utilización, código de actividad de enfermería y cantidad de actividad para identificar cada detalle.; Devuelve la descripción del producto sin espacios a la derecha (RTRIM).; No aplica filtros por ingreso, centro de atención ni paciente; cualquier restricción debe hacerse por el consumidor de la vista.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'actividad de enfermería; producto/insumo médico; consumo hospitalario; ingreso del paciente; centro de atención; historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHOGASIN: Retorna una fila por cada registro de consumo de HCHOGASIN que tenga coincidencia tanto en IHLISTPRO (por CODPRODUC) como en HCACTENFE (por CODACTENF), incluyendo llave compuesta, producto, cantidades, ingreso, paciente, centro de atención y fecha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.IHLISTPRO; dbo.HCACTENFE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewNursingProceduresDetail';
GO
