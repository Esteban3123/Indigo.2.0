-- =============================================
-- Author:		Yezid Garcia Medina
-- Create date: 10 Mayo 2021
-- Description:	Listar Interacciones - Nota Servicio Farmaceutico
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarInteracciones_NotaServicioFarmaceutico] 
(
@Paciente Varchar(25),
@Ingreso  Char(10),
@ProductoA  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------		
	
	declare @MaxFolio_OtrosMedicamentos nchar(10)

	SELECT @MaxFolio_OtrosMedicamentos = ISNULL(MAX(NUMEFOLIO),'0')  FROM HCNOSERFOTROMED AS A WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso  ;

	
	with Tabla
	as
	(	SELECT
				
				RTRIM(A.CODPRODUC) As 'Codigo', 
				RTRIM(E.DESPRODUC) As 'Medicamento',
                RTRIM(CODDCIMED) AS 'CODDCIMED'

			FROM
				dbo.HCPRESCRA As A with(nolock)
				INNER JOIN dbo.IHLISTPRO As E with(nolock) ON A.CODPRODUC=E.CODPRODUC 
			
			WHERE 
				A.IPCODPACI= @Paciente 
				AND A.NUMINGRES= @Ingreso
				AND A.PREESTADO IN (1,6) 

		UNION ALL

			SELECT 
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
                RTRIM(pro.CODDCIMED) AS 'CODDCIMED' 

			FROM 
				dbo.HCINFLIQC as cab with(nolock)
				INNER JOIN dbo.HCINFCONC as med with(nolock) ON cab.CODCONCEC = med.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO as pro with(nolock) ON med.CODPRODUC = pro.CODPRODUC  
				INNER JOIN dbo.HCINFLIQA as admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC 
				
			WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
		
		UNION ALL
			
			SELECT
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
                RTRIM(pro.CODDCIMED) AS 'CODDCIMED'

			FROM 
				dbo.HCINFLIQC As cab with(nolock) 
				INNER JOIN dbo.HCINFLIQD As dil with(nolock) ON cab.CODCONCEC = dil.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO As pro with(nolock) ON dil.CODPRODUC = pro.CODPRODUC 
				INNER JOIN dbo.HCINFLIQA As admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC
           
		   WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
				 				 
		UNION ALL
			
			SELECT
				
				RTRIM(B.CODPRODUC) As 'Codigo',
				RTRIM(B.DESPRODUC) As 'Medicamento',
				RTRIM(B.CODDCIMED) AS 'CODDCIMED' 
			FROM 
				dbo.HCNOSERFOTROMED As A with(nolock) 
				INNER JOIN dbo.IHLISTPRO As B with(nolock) ON A.CODPRODUC = B.CODPRODUC 				
				INNER JOIN dbo.INPROFSAL as C with(nolock) ON A.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INUNIFUNC as D with(nolock) ON A.UFUCODIGO = D.UFUCODIGO 
				INNER JOIN dbo.ADcenaten as E with(nolock) ON A.CODCENATE = E.CODCENATE
           
		   WHERE 
				 A.IPCODPACI = @Paciente
				 AND A.NUMINGRES = @Ingreso
				 AND A.PREESTADO = 1
				 AND A.NUMEFOLIO = @MaxFolio_OtrosMedicamentos
				 				 				
	 )	
	 SELECT	Distinct		 
			Z.* , 
			CASE WHEN inter.CODPRODUA IS NOT NULL THEN Cast(1 As Bit) ELSE Cast(0 As Bit) END AS 'Interaccion',
            RTRIM(inter.CODPRODUA) As 'Codigo_A' ,
            RTRIM(inter.CODPRODUB) AS 'Codigo_B' ,
			CASE inter.NIVRIESGO
				WHEN '1' THEN 'Grave'
				WHEN '2' THEN 'Moderada'
				WHEN '3' THEN 'Leve' 
			END	AS 'Riesgo',
            RTRIM(inter.OBSERVACI) AS 'Observaciones' ,
            RTRIM(Z.Medicamento) As 'Medicamento_A',
            RTRIM(pro.DESPRODUC) As 'Medicamento_B'
	 FROM Tabla As Z
	 LEFT JOIN dbo.HCINTEMED As inter	
		ON inter.CODPRODUA in (Z.Codigo, Z.CODDCIMED) and inter.CODPRODUB in (select Codigo from Tabla union all select CODDCIMED from Tabla)
     LEFT JOIN dbo.IHLISTPRO As pro  ON inter.CODPRODUB IN ( pro.CODPRODUC , pro.CODDCIMED)
    WHERE  Z.Codigo =@ProductoA 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que detecta interacciones medicamentosas para un paciente y un ingreso hospitalario específicos, evaluando un medicamento de referencia contra todos los fármacos activos del paciente. Consolida los medicamentos vigentes del paciente desde cuatro fuentes: prescripciones activas (HCPRESCRA), mezclas o preparaciones magistrales con sus concentraciones y diluyentes (HCINFLIQC/HCINFCONC/HCINFLIQD), y los medicamentos registrados en la nota de servicio farmacéutico (HCNOSERFOTROMED, tomando el folio más reciente). Una vez consolidada esa lista, cruza el medicamento indicado en el parámetro @ProductoA contra los demás medicamentos activos del paciente usando la tabla de interacciones (HCINTEMED), retornando el nivel de riesgo (Grave, Moderada, Leve) y las observaciones clínicas de cada interacción detectada. Se usa en la nota de servicio farmacéutico para alertar al profesional de la salud sobre combinaciones peligrosas de medicamentos en un ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Detecta y lista las interacciones medicamentosas entre un producto seleccionado y los demás medicamentos activos del paciente en su ingreso (prescripciones, mezclas, infusiones y otros medicamentos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir y tener medicamentos registrados en al menos una de las fuentes: prescripciones, mezclas, infusiones u otros medicamentos.; El producto a evaluar debe existir como código en alguna de las fuentes consultadas para el paciente/ingreso.; Para que aparezcan los ''otros medicamentos'' debe existir un folio vigente (se toma sólo el folio máximo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran medicamentos en estados activos definidos por fuente (prescripciones 1 o 6; mezclas/infusiones 1 o 5; otros medicamentos 1).; De HCNOSERFOTROMED se considera únicamente el folio más reciente (MAX(NUMEFOLIO)).; La detección de interacción se hace tanto por código de producto (CODPRODUC) como por código de principio activo/DCI (CODDCIMED), permitiendo equivalencias.; El resultado se filtra siempre al producto A indicado por parámetro.; Se utiliza WITH(NOLOCK) en todas las lecturas, permitiendo lecturas sucias.; El procedimiento es de sólo lectura: no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción de medicamentos; Mezclas e infusiones intravenosas; Diluciones; Otros medicamentos (fuera de servicio); Folio de orden; Interacción medicamentosa; Nivel de riesgo (Grave/Moderada/Leve); Principio activo (DCI); Nota de servicio farmacéutico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto único (DISTINCT) por cada medicamento del paciente vs producto evaluado, marcando Interaccion=1 cuando existe coincidencia en HCINTEMED y 0 en caso contrario.; [RETURN_RESULT] resultset: El nivel de riesgo se traduce: NIVRIESGO ''1''→''Grave'', ''2''→''Moderada'', ''3''→''Leve''; cualquier otro valor queda en NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCPRESCRA.PREESTADO IN (1,6) → Incluye el medicamento prescrito en la lista de medicamentos activos del paciente.; si HCINFLIQA.PREESTADO IN (1,5) sobre mezclas (HCINFLIQC + HCINFCONC) o diluciones (HCINFLIQD) → Incluye el componente o dilución de la mezcla/infusión como medicamento activo.; si HCNOSERFOTROMED.PREESTADO = 1 AND NUMEFOLIO = MAX(NUMEFOLIO) del paciente/ingreso → Incluye los ''otros medicamentos'' del último folio vigente.; si inter.CODPRODUA IS NOT NULL (existe registro en HCINTEMED cruzando códigos directos o por CODDCIMED) → Marca Interaccion=1 y devuelve riesgo, observaciones y medicamento B; en caso contrario Interaccion=0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNOSERFOTROMED; dbo.HCPRESCRA; dbo.IHLISTPRO; dbo.HCINFLIQC; dbo.HCINFCONC; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADcenaten; dbo.HCINTEMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarInteracciones_NotaServicioFarmaceutico';
-- GO
