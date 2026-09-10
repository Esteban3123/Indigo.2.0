
CREATE PROCEDURE [dbo].[SP_HC_ListarProductosMezclasPacienteEnfermeria]
(
@Paciente varchar(25),
@Ingreso char(20),
@CentroAtencion char(10),
@UnidadFuncional char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
        SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,CAST(C.CANACTPRO AS INT) AS Disponibles,B.TIPPRODUC 
		FROM dbo.HCPRESCRA A 
		INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
		INNER JOIN dbo.HCFISIPRO C ON B.CODPRODUC=C.CODPRODUC 
		WHERE A.PREESTADO IN ('1','6') AND A.IPCODPACI=@Paciente AND A.NUMINGRES=@Ingreso  AND B.TIPPRODUC IN ('1','3')
		AND C.CODCENATE=@CentroAtencion AND C.UFUCODIGO=@UnidadFuncional AND C.CANACTPRO>0
		UNION 
		SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,CAST(C.CANACTPRO AS INT) AS Disponibles,B.TIPPRODUC 
		FROM dbo.HCINFCONC A 
		INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
		INNER JOIN dbo.HCFISIPRO C ON B.CODPRODUC=C.CODPRODUC 
		INNER JOIN dbo.HCINFLIQA D ON A.CODCONCEC=D.CODCONCEC 
		WHERE D.IPCODPACI=@Paciente AND D.NUMINGRES=@Ingreso AND D.PREESTADO IN ('1','5') 
		AND C.CODCENATE=@CentroAtencion AND C.UFUCODIGO=@UnidadFuncional AND C.CANACTPRO>0
		UNION 
		SELECT RTRIM(B.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Descripcion,CAST(C.CANACTPRO AS INT) AS Disponibles,B.TIPPRODUC 
		FROM dbo.HCINFLIQD A 
		INNER JOIN dbo.IHLISTPRO B ON A.CODPRODUC=B.CODPRODUC 
		INNER JOIN dbo.HCFISIPRO C ON B.CODPRODUC=C.CODPRODUC 
		INNER JOIN dbo.HCINFLIQA D ON A.CODCONCEC=D.CODCONCEC 
		WHERE D.IPCODPACI=@Paciente AND D.NUMINGRES=@Ingreso AND D.PREESTADO IN ('1','5')
		AND C.CODCENATE=@CentroAtencion AND C.UFUCODIGO=@UnidadFuncional AND C.CANACTPRO>0
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los productos farmacéuticos y mezclas magistrales disponibles en enfermería para un paciente y su ingreso hospitalario específico. Combina tres fuentes: los medicamentos prescritos directamente al paciente (órdenes activas o vigentes), los componentes de concentraciones de mezclas, y los líquidos de dilución asociados a esas mezclas. Para cada producto devuelve el código, descripción, cantidad disponible en físico y tipo de producto, filtrando únicamente los que tienen stock activo en el centro de atención y unidad funcional indicados. Se usa desde los módulos de enfermería para que el personal identifique qué medicamentos, insumos o preparaciones magistrales están disponibles al momento de administrar tratamientos al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos y mezclas (medicamentos/insumos) disponibles en stock para enfermería, asociados a prescripciones activas de un paciente en un ingreso, centro de atención y unidad funcional específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener prescripciones registradas en HCPRESCRA, mezclas en HCINFCONC o líquidos en HCINFLIQD vinculados al ingreso indicado.; Los productos deben existir en el catálogo IHLISTPRO y tener registro físico en HCFISIPRO para el centro de atención y unidad funcional indicados.; Debe existir cantidad disponible (CANACTPRO > 0) en la unidad funcional consultada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan productos con disponibilidad real (CANACTPRO > 0) en el centro y unidad funcional consultados.; Las prescripciones directas se filtran a tipos de producto ''1'' y ''3'' únicamente.; Los estados válidos para prescripciones son ''1'' y ''6''; para líquidos/mezclas son ''1'' y ''5''.; El UNION elimina duplicados cuando el mismo producto aparece por varias fuentes.; El resultado siempre se restringe al paciente, ingreso, centro de atención y unidad funcional indicados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; prescripción médica; mezclas/preparaciones magistrales; líquidos de infusión; centro de atención; unidad funcional; stock/disponibilidad de productos; enfermería', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna un conjunto unificado (UNION) de productos tipo ''1'' o ''3'' desde prescripciones activas, más productos de mezclas/concentrados y líquidos de infusión, con su disponibilidad en stock.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen HCPRESCRA: PREESTADO IN (''1'',''6'') y TIPPRODUC IN (''1'',''3'') → Incluye productos prescritos directamente al paciente en estado activo/vigente, restringidos a tipos de producto 1 y 3.; si Origen HCINFCONC unido a HCINFLIQA con D.PREESTADO IN (''1'',''5'') → Incluye los componentes/concentraciones de las mezclas de infusión cuya cabecera de liquido está en estado válido.; si Origen HCINFLIQD unido a HCINFLIQA con D.PREESTADO IN (''1'',''5'') → Incluye los productos del detalle de líquidos de infusión cuya cabecera está en estado válido.; si C.CANACTPRO > 0 → Solo se devuelven productos con cantidad activa disponible en el centro/unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.HCFISIPRO; dbo.HCINFCONC; dbo.HCINFLIQA; dbo.HCINFLIQD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosMezclasPacienteEnfermeria';
-- GO
