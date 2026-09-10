CREATE PROCEDURE [dbo].[SP_HC_ListarDetallePlanCuidadoEnf](
@CodPlan varchar(3)
)
AS

BEGIN	
	SET NOCOUNT ON;
SELECT
 A.CODPLANCENF
 ,C.CODVALORAPCE
 ,B.IDPLANVAL 	
,NOMVALORAPCE 'Valoracion'
,ISNULL(Objetivo.[Name],'') as 'Objetivo'
,D.IDHCCLASENF
,ISNULL(E.[Name],'') as 'Clase'
,ISNULL(F.CODDIAGENF,'') as CODDIAGENF
,ISNULL(G.NOMBDIAGENF,'') as 'Diagnostico'
,(Select STUFF((SELECT Char(10) + B.NOMCAUSAPCE FROM HCDIAGCAUPCE A INNER JOIN HCCAUSAPCE B ON A.CODCAUSAPCE = B.CODCAUSAPCE WHERE A.CODPLANCENF = @CodPlan AND A.CODVALORAPCE=C.CODVALORAPCE AND CODDIAGENF=F.CODDIAGENF ORDER BY B.NOMCAUSAPCE FOR XML PATH('')),1,1,'')) as 'PosibleCausa'
,ISNULL(K.CODINTPCE,'') AS CODINTPCE
,ISNULL(K.NOMINTPCE,'') as 'Intervencion'
,(Select STUFF((SELECT Char(10) + B.NOMACTPCE FROM HCINTACTPCE A INNER JOIN HCACTIVIPCE B ON A.CODACTPCE = B.CODACTPCE WHERE A.CODPLANCENF = @CodPlan AND A.CODVALORAPCE=C.CODVALORAPCE AND CODDIAGENF=F.CODDIAGENF AND CODINTPCE=K.CODINTPCE AND IDHCCLASENF = D.IDHCCLASENF ORDER BY B.NOMACTPCE FOR XML PATH('')),1,1,'')) as 'Actividad'
FROM HCPLANCUIENF A
	INNER JOIN HCPLANVALPCE B ON A.CODPLANCENF=B.CODPLANCENF
	INNER JOIN HCVALORAPCE C ON B.CODVALORAPCE=C.CODVALORAPCE
	LEfT JOIN HCCLASPCE D ON A.CODPLANCENF = D.CODPLANCENF AND B.CODVALORAPCE = D.CODVALORAPCE
	LEFT JOIN HCCLASENF E ON D.IDHCCLASENF = E.Id
	LEFT JOIN HCVALDIAGPCE F ON B.CODPLANCENF = F.CODPLANCENF AND B.CODVALORAPCE = F.CODVALORAPCE AND D.IDHCCLASENF = F.IDHCCLASENF
	LEFT JOIN HCDIAGENF G ON F.CODDIAGENF = G.CODDIAGENF
	LEFT JOIN HCDIAGINTPCE J ON F.CODPLANCENF = J.CODPLANCENF AND F.CODVALORAPCE = J.CODVALORAPCE AND D.IDHCCLASENF = J.IDHCCLASENF AND F.CODDIAGENF = J.CODDIAGENF
	LEFT JOIN HCINTERVENPCE K ON K.CODINTPCE = J.CODINTPCE
 OUTER APPLY(Select STUFF((SELECT Char(10) + rtrim(Y.NOMOBJPCE) from HCVALOBJPCE X INNER JOIN HCOBJPCE Y ON X.CODOBJPCE = Y.CODOBJPCE Where X.CODVALORAPCE = B.CODVALORAPCE AND X.IDHCCLASENF = E.Id AND X.CODDIAGENF = F.CODDIAGENF AND X.CODPLANCENF=A.CODPLANCENF FOR XML PATH('')),1,1,'') as [Name])Objetivo
WHERE A.CODPLANCENF = @CodPlan	
ORDER BY C.NOMVALORAPCE
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el detalle completo de un plan de cuidado de enfermería dado su código, integrando en una sola consulta todas las capas del proceso de enfermería: las valoraciones clínicas del plan, las clasificaciones o escalas aplicadas, los diagnósticos de enfermería (NANDA u otra taxonomía), las posibles causas de cada diagnóstico, los objetivos de cuidado, las intervenciones de enfermería y las actividades asociadas a cada intervención. Sirve para visualizar o imprimir la estructura completa de un plan de cuidados tal como está configurado en el catálogo institucional, siendo el insumo principal para el módulo de planes de cuidado de enfermería en la historia clínica. Compone información de los catálogos de planes (HCPLANCUIENF), valoraciones (HCVALORAPCE), clasificaciones (HCCLASPCE, HCCLASENF), diagnósticos de enfermería (HCDIAGENF, HCVALDIAGPCE), causas, objetivos, intervenciones y actividades de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el detalle jerárquico (valoración, clase, diagnóstico, posibles causas, objetivos, intervenciones y actividades) de un plan de cuidado de enfermería identificado por su código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un plan de cuidado de enfermería con el código suministrado para obtener filas (filtro WHERE A.CODPLANCENF = @CodPlan).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelve el detalle del plan de cuidado cuyo código coincide con el parámetro recibido.; Las causas, objetivos y actividades se concatenan en una sola cadena separada por salto de línea (Char(10)).; Los campos de clase, objetivo, diagnóstico, código y nombre de intervención se devuelven como cadena vacía cuando no existen relaciones (ISNULL a '''').; El resultado se ordena alfabéticamente por nombre de la valoración.; La agregación de causas se restringe al mismo plan, valoración y diagnóstico; la de actividades además al mismo intervención y clase; la de objetivos al mismo plan, valoración, clase y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de cuidado de enfermería; Valoración de enfermería; Diagnóstico de enfermería; Clase de enfermería; Objetivo de enfermería; Intervención de enfermería; Actividad de enfermería; Causa del diagnóstico (PCE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPLANCUIENF: Cuando A.CODPLANCENF = @CodPlan, retorna el resultset con valoraciones, clases, diagnósticos, intervenciones y agregados de causas/objetivos/actividades asociados al plan.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPLANCUIENF; dbo.HCPLANVALPCE; dbo.HCVALORAPCE; dbo.HCCLASPCE; dbo.HCCLASENF; dbo.HCVALDIAGPCE; dbo.HCDIAGENF; dbo.HCDIAGINTPCE; dbo.HCINTERVENPCE; dbo.HCDIAGCAUPCE; dbo.HCCAUSAPCE; dbo.HCINTACTPCE; dbo.HCACTIVIPCE; dbo.HCVALOBJPCE; dbo.HCOBJPCE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnf';
-- GO
