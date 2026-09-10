

CREATE FUNCTION [dbo].[CantNoPos] (@Ingreso as nvarchar(10),@Pruducto as nvarchar(20) )
RETURNS nvarchar (20)
AS
BEGIN

declare @Cant nvarchar(20)

select distinct @Cant= a.CANPEDPRO 
from  dbo.HCJUNOPOM as a INNER JOIN
 (      SELECT IPCODPACI, NUMINGRES, CODPRODUC, MAX(NUMEFOLIO) AS MaxFolio
                                    FROM            dbo.HCJUNOPOM
                                    WHERE       CODCENATE = '001'     
                                    GROUP BY IPCODPACI, NUMINGRES, CODPRODUC) as i on i.IPCODPACI = a.IPCODPACI AND i.NUMINGRES = a.NUMINGRES AND 
                         i.CODPRODUC = a.CODPRODUC AND i.MaxFolio = a.numefolio
where @Ingreso= a.NUMINGRES and @Pruducto = a.CODPRODUC

RETURN @Cant

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que retorna la cantidad pedida (cantidad solicitada o prescrita) de un medicamento o producto NO POS para un ingreso hospitalario específico. Consulta la tabla de órdenes de medicamentos (HCJUNOPOM) filtrando por el número de ingreso y el código de producto recibidos como parámetros, tomando únicamente el folio más reciente del centro de atención ''001'' para evitar duplicados por correcciones o versiones anteriores de la orden. Se utiliza para obtener la cantidad solicitada del último registro vigente de un ítem no incluido en el plan de beneficios (No POS) dentro de la historia clínica de un paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CantNoPos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CantNoPos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la cantidad pedida del medicamento (no POS) prescrito en la última orden médica vigente de un ingreso y producto específicos dentro del centro de atención ''001''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una orden en HCJUNOPOM con CODCENATE=''001'' para el ingreso y producto consultados, de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera la prescripción del centro de atención ''001''.; Para cada combinación paciente/ingreso/producto se toma únicamente el folio más reciente (MAX NUMEFOLIO).; Si no existe orden que cumpla las condiciones, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica de medicamento; Prescripción; Ingreso del paciente; Producto/medicamento; Centro de atención; Folio de prescripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCJUNOPOM: Devuelve CANPEDPRO de la orden con MAX(NUMEFOLIO) filtrando por CODCENATE=''001'', NUMINGRES=@Ingreso y CODPRODUC=@Pruducto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCJUNOPOM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CantNoPos';
GO
