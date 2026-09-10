

CREATE VIEW [dbo].[VKardexMedicineSupplierBrenda]
AS
select Ingreso,PersonName as Paciente,UFUDESCRI as UnidadFuncional,Codigo, Nombre, sum(Cantidad) as Cantidad from (
--Consulta detalle Kardex
SELECT A.NUMCONSEC AS Row
,a.CODPRODUC AS Codigo
,pro.DESPRODUC as Nombre
, a.IPCODPACI as CodigoPaciente
, NUMINGRES as Ingreso
,FECREGKAR AS Fecha
, DESMOVPRO AS Movimiento
,CANPRODUCT AS Cantidad
,TIPREGIST AS TipoKardex
,TIPORIREG AS Origen
,CASE WHEN (TIPORIREG>0 AND TIPORIREG<6) OR (TIPORIREG = 20 AND TIPREGIST = 1) THEN '1' WHEN (TIPORIREG>10 AND TIPORIREG < 20) OR (TIPORIREG = 20 AND TIPREGIST = 2) THEN '2' WHEN TIPORIREG>5 AND TIPORIREG<9 THEN '3' WHEN TIPORIREG>8 AND TIPORIREG<11 THEN '4' END AS Tipo,
RTRIM(UFUDESCRI) AS UFUDESCRI,CONSECFAR AS ConsLog
,i.IPNOMCOMP  as PersonName
FROM dbo.HCKARDPAC A 
INNER JOIN dbo.INUNIFUNC B ON A.UFUCODIGO=B.UFUCODIGO 
inner join dbo.INPACIENT i on a.IPCODPACI = i.IPCODPACI
inner join dbo.IHLISTPRO pro on pro.CODPRODUC = a.CODPRODUC
LEFT OUTER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL

union all

SELECT  A.NUMCONSEC AS Row
,a.CODPRODUC AS Codigo
, pro.DESPRODUC as Nombre
, a.IPCODPACI as CodigoPaciente
, INGMH.NUMINGRES as Ingreso
,FECREGKAR AS Fecha
, DESMOVPRO AS Movimiento
,CANPRODUCT AS Cantidad
,TIPREGIST AS TipoKardex
,TIPORIREG AS Origen
,CASE WHEN (TIPORIREG>0 AND TIPORIREG<6) OR (TIPORIREG = 20 AND TIPREGIST = 1) THEN '1' WHEN (TIPORIREG>10 AND TIPORIREG < 20) OR (TIPORIREG = 20 AND TIPREGIST = 2) THEN '2' WHEN TIPORIREG>5 AND TIPORIREG<9 THEN '3' WHEN TIPORIREG>8 AND TIPORIREG<11 THEN '4' END AS Tipo,
RTRIM(UFUDESCRI) AS UFUDESCRI,CONSECFAR AS ConsLog
,'Hijo '+ cast(rn.NUMHIJREG as varchar(20)) as PersonName
FROM dbo.HCKARDPAC A 
INNER JOIN dbo.INUNIFUNC B ON A.UFUCODIGO=B.UFUCODIGO 
inner join dbo.HCINGRESORECNAC INGMH  on a.NUMINGRES = INGMH .NUMINGRESHIJO AND INGMH.TIPREGISTRO = 1
inner join dbo.HCRECINAC RN  on INGMH.NUMINGRESHIJO  = rn.NUMINGRESHIJO  
inner join  dbo.ADINGRESO AS ING  on ING.NUMINGRES = INGMH.NUMINGRESHIJO 
inner join dbo.IHLISTPRO pro on pro.CODPRODUC = a.CODPRODUC  
LEFT OUTER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL
) as data
where Movimiento like 'Aplicacion del Medicamento%' or Movimiento like 'Insumo Utilizado%' group by Ingreso,PersonName,UFUDESCRI,Codigo, Nombre
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de consumo de medicamentos e insumos por paciente (incluyendo recién nacidos vinculados a su madre), agrupada por ingreso, unidad funcional y producto. Filtra únicamente movimientos de kárdex de tipo "Aplicación del Medicamento" e "Insumo Utilizado", sumando cantidades dispensadas. Sirve para reporting de consumo farmacéutico por ingreso hospitalario, útil para análisis de proveedor o auditoría de suministros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el consumo total de medicamentos e insumos aplicados a pacientes (incluyendo recién nacidos asociados al ingreso de la madre) por unidad funcional, agrupando cantidades del kárdex.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El movimiento del kárdex debe describir ''Aplicacion del Medicamento%'' o ''Insumo Utilizado%'' para ser considerado; Cada registro del kárdex debe tener unidad funcional, paciente y producto válidos (joins INNER); Para incluir consumos de recién nacidos, el ingreso debe estar vinculado en HCINGRESORECNAC con TIPREGISTRO = 1 y existir su registro en HCRECINAC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contabilizan movimientos de aplicación de medicamentos o uso de insumos; otros tipos de kárdex (devoluciones, dispensaciones puras, etc.) no se reflejan; Los consumos asociados a recién nacidos se identifican como ''Hijo N'' y no con el nombre de la madre, manteniendo trazabilidad por número de hijo; La cantidad reportada es siempre una suma agregada por la combinación ingreso/paciente/unidad funcional/producto; Toda fila debe tener unidad funcional, paciente y producto resolubles (relaciones INNER obligatorias)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kárdex de medicamentos; Aplicación de medicamento; Insumo utilizado; Paciente; Unidad funcional; Ingreso/Admisión; Recién nacido; Profesional de salud; Producto farmacéutico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Retorna la suma de cantidades (Cantidad) agrupada por Ingreso, Paciente, UnidadFuncional, Código y Nombre del producto, filtrando solo movimientos cuya descripción inicia con ''Aplicacion del Medicamento'' o ''Insumo Utilizado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si (TIPORIREG entre 1 y 5) o (TIPORIREG=20 y TIPREGIST=1) → Clasifica el movimiento como Tipo ''1''; si (TIPORIREG entre 11 y 19) o (TIPORIREG=20 y TIPREGIST=2) → Clasifica el movimiento como Tipo ''2''; si TIPORIREG entre 6 y 8 → Clasifica el movimiento como Tipo ''3''; si TIPORIREG entre 9 y 10 → Clasifica el movimiento como Tipo ''4''; si El ingreso corresponde a un recién nacido (existe en HCINGRESORECNAC como NUMINGRESHIJO con TIPREGISTRO=1) → El nombre del paciente se reemplaza por ''Hijo '' concatenado con el número de hijo (NUMHIJREG) en lugar del nombre del paciente titular else Se usa el nombre completo del paciente (IPNOMCOMP) desde INPACIENT; si Movimiento NO empieza por ''Aplicacion del Medicamento'' ni por ''Insumo Utilizado'' → Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCKARDPAC; dbo.INUNIFUNC; dbo.INPACIENT; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.HCINGRESORECNAC; dbo.HCRECINAC; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VKardexMedicineSupplierBrenda';
GO
