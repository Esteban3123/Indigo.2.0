
CREATE PROCEDURE [dbo].[SP_HC_ListarDetallePlanCuidadoEnfPac](
@idPceControl int
,@idPlanVal int
)
AS
BEGIN	
	SET NOCOUNT ON;
SELECT DISTINCT
  Isnull(Objetivo.[Name], '') as 'Objetivos',
  A.CODPLANCENF
  ,C.CODVALORAPCE
  ,B.IDPLANVAL 	
  ,NOMVALORAPCE 'Valoracion' 
  ,ISNULL(g.CODDIAGENF,'') as CODDIAGENF
  ,ISNULL(G.NOMBDIAGENF,'') as 'Diagnostico'
  ,ISNULL(CAST('' AS VARCHAR(MAX)),'') as 'PosibleCausa'
  ,ISNULL(K.CODINTPCE,'') AS CODINTPCE
  ,ISNULL(K.NOMINTPCE,'') as 'Intervencion'
  ,ISNULL( CAST('' AS VARCHAR(MAX)),'') as 'Actividad'
  ,ISNULL(F.IDVALDIAG,'') AS IDVALDIAG
  ,ISNULL(J.IDDIAGINT,'') AS IDDIAGINT
  ,A.OBSERVACION 
  ,E.Id as IDHCCLASENF
  ,E.[Name] as 'NameClase'
FROM HCPCECONTROL A
INNER JOIN HCPCEPLANVAL B ON A.ID = B.IDPCECONTROL
INNER JOIN HCVALORAPCE C ON B.CODVALORAPCE=C.CODVALORAPCE
INNER JOIN HCPCECLAS D ON D.IDPLANVAL = B.IDPLANVAL
INNER JOIN HCCLASENF E ON E.Id = D.IDHCCLASENF
INNER JOIN HCPCEVALDIAG F ON F.IDPLANVAL = B.IDPLANVAL AND F.IDHCCLASENF = D.IDHCCLASENF
INNER JOIN HCDIAGENF G ON G.CODDIAGENF = F.CODDIAGENF
INNER JOIN HCPCEDIAGINT J ON F.IDVALDIAG = J.IDVALDIAG AND A.ID = J.IDHCPCECONTROL
INNER JOIN HCINTERVENPCE K ON K.CODINTPCE =J.CODINTPCE
LEFT JOIN HCESCMEDPCE L ON L.CODESCMEDPCE=F.CODESCMEDPCE
LEFT JOIN HCVALDIAGPCE X ON X.CODDIAGENF = f.CODDIAGENF
LEFT JOIN HCPCEVALOBJ Y ON Y.IDPLANVAL = B.IDPLANVAL
LEFT JOIN HCOBJPCE M ON Y.CODOBJPCE=M.CODOBJPCE
OUTER APPLY(Select STUFF((SELECT Char(10) + rtrim(Y.NOMOBJPCE) 
			from HCVALOBJPCE X 
			INNER JOIN HCOBJPCE Y ON X.CODOBJPCE = Y.CODOBJPCE 
			LEFT JOIN HCPCEVALOBJ OB ON OB.IDPLANVAL = B.IDPLANVAL AND OB.CODOBJPCE = Y.CODOBJPCE
			Where X.CODVALORAPCE = B.CODVALORAPCE AND X.IDHCCLASENF = E.Id AND X.CODDIAGENF = F.CODDIAGENF AND X.CODPLANCENF=A.CODPLANCENF 
			FOR XML PATH('')),1,1,'') as [Name])Objetivo
WHERE A.ID=@idPceControl AND B.IDPLANVAL =@idPlanVal
--ORDER BY C.NOMVALORAPCE,G.NOMBDIAGENF,K.NOMINTPCE
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo del plan de cuidado de enfermería para un control y un plan de valoración específicos del paciente. Integra la valoración clínica (HCPCEPLANVAL, HCVALORAPCE), las clasificaciones de enfermería (HCPCECLAS, HCCLASENF), los diagnósticos de enfermería (HCPCEVALDIAG, HCDIAGENF), las intervenciones asociadas a cada diagnóstico (HCPCEDIAGINT, HCINTERVENPCE) y los objetivos del plan (HCOBJPCE). Se usa para mostrar en historia clínica el resumen estructurado del plan de cuidado de enfermería de un paciente: qué se valoró, qué diagnósticos de enfermería se identificaron, qué intervenciones se planificaron y cuáles son los objetivos de cuidado definidos para ese control de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el detalle del plan de cuidado de enfermería de un paciente (valoraciones, diagnósticos, intervenciones, clases y objetivos consolidados) para un control y plan de valoración específicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un control de plan de cuidado (HCPCECONTROL) cuyo ID coincida con el parámetro de control.; Debe existir un plan de valoración (HCPCEPLANVAL) asociado al control y que coincida con el parámetro de plan.; Deben existir relaciones consistentes entre valoración, clase, diagnóstico e intervención del PCE para retornar resultados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven filas que tengan correspondencia en valoración, clase, diagnóstico e intervención (joins INNER obligatorios).; Los objetivos asociados al diagnóstico se concatenan en una sola cadena separada por salto de línea.; Se aplica DISTINCT para evitar duplicados de combinaciones valoración-diagnóstico-intervención.; Los campos textuales nulos se devuelven como cadena vacía mediante ISNULL.; Los campos ''PosibleCausa'' y ''Actividad'' siempre se retornan como cadena vacía (no se obtienen del modelo).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de Cuidado de Enfermería (PCE); Valoración de enfermería; Diagnóstico de enfermería; Intervención de enfermería; Clase de enfermería; Objetivo del plan de cuidado; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCPCECONTROL: Devuelve un conjunto de resultados con el detalle del plan de cuidado filtrado por HCPCECONTROL.ID = @idPceControl y HCPCEPLANVAL.IDPLANVAL = @idPlanVal, agregando objetivos concatenados vía OUTER APPLY con FOR XML PATH.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPCECONTROL; dbo.HCPCEPLANVAL; dbo.HCVALORAPCE; dbo.HCPCECLAS; dbo.HCCLASENF; dbo.HCPCEVALDIAG; dbo.HCDIAGENF; dbo.HCPCEDIAGINT; dbo.HCINTERVENPCE; dbo.HCESCMEDPCE; dbo.HCVALDIAGPCE; dbo.HCPCEVALOBJ; dbo.HCOBJPCE; dbo.HCVALOBJPCE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarDetallePlanCuidadoEnfPac';
-- GO
