CREATE PROCEDURE [dbo].[SP_HC_ListarProductosxMezcla]
(
@Paciente varchar(25),
@Ingreso char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

			SELECT 
				A.CODCONCEC,
				RTRIM(A.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				A.CANPROCAL As 'Cantidad'	
			FROM 
				dbo.HCINFCONC AS A with(nolock)
				INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON A.CODPRODUC = PRO.CODPRODUC  
			WHERE 
				 A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso
		
			UNION ALL
			SELECT 
				D.IDHCNUTPAREC AS 'CODCONCEC',
				RTRIM(D.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				D.CANTMED As 'Cantidad'	
			FROM HCNUTPAREND d
			INNER JOIN HCNUTPAREC AS c ON d.IDHCNUTPAREC = c.ID
			INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON D.CODPRODUC = PRO.CODPRODUC  
			WHERE c.IPCODPACI = @Paciente AND C.NUMINGRES =  @Ingreso
			UNION ALL 
			
			SELECT 
				A.CODCONCEC,
				RTRIM(A.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				A.CANPROCAL As 'Cantidad'	
			FROM 
				dbo.HCINFLIQD AS A with(nolock)
				INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON A.CODPRODUC = PRO.CODPRODUC  
			WHERE 
				 A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los productos o medicamentos que componen una mezcla, preparación magistral o fórmula especial prescrita a un paciente en un ingreso específico. Consolida en un único resultado tres fuentes distintas: los componentes de mezclas magistrales (HCINFCONC), los nutrientes de nutrición parenteral (HCNUTPAREND/HCNUTPAREC) y los líquidos endovenosos (HCINFLIQD), enriqueciendo cada ítem con el nombre del medicamento desde el catálogo de productos (IHLISTPRO). Se usa para visualizar la composición completa de una mezcla farmacéutica o pauta nutricional parenteral de un paciente hospitalizado, mostrando código, nombre y cantidad de cada componente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarProductosxMezcla';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolidar en un único listado los productos/medicamentos asociados a las mezclas, líquidos/infusiones y nutrición parenteral registrados en la historia clínica de un paciente para un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un paciente y un número de ingreso válidos.; Debe existir correspondencia del código de producto en el catálogo maestro IHLISTPRO para que el ítem aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos cuyo código exista en el catálogo maestro de productos (IHLISTPRO), por el INNER JOIN.; El listado se restringe siempre al paciente y número de ingreso indicados.; Los detalles de nutrición parenteral solo se incluyen si su cabecera está vinculada (INNER JOIN HCNUTPAREC).; Se permiten duplicados entre fuentes (UNION ALL no elimina repetidos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Mezclas magistrales; Medicamentos/Productos farmacéuticos; Nutrición parenteral; Líquidos e infusiones; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFCONC: Cuando IPCODPACI=@Paciente y NUMINGRES=@Ingreso → retorna los productos de concentraciones/mezclas con su cantidad (CANPROCAL).; [RETURN_RESULT] dbo.HCNUTPAREND: Cuando la cabecera HCNUTPAREC asociada cumple IPCODPACI=@Paciente y NUMINGRES=@Ingreso → retorna los productos del detalle de nutrición parenteral con su cantidad (CANTMED), usando IDHCNUTPAREC como código de concepto.; [RETURN_RESULT] dbo.HCINFLIQD: Cuando IPCODPACI=@Paciente y NUMINGRES=@Ingreso → retorna los productos de líquidos/infusiones con su cantidad (CANPROCAL).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFCONC; dbo.IHLISTPRO; dbo.HCNUTPAREND; dbo.HCNUTPAREC; dbo.HCINFLIQD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarProductosxMezcla';
-- GO
