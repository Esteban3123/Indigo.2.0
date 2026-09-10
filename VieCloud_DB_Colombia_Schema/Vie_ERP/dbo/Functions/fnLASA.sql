-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 01/12/2016
-- Description:	Funcion para determinar si el medicamento es LASA 
-- =============================================
CREATE FUNCTION [dbo].[fnLASA] 
(
	@CodigoProducto as varchar(20)
)
RETURNS bit 
AS
BEGIN
	
	
	declare @Disponible as bit

	declare @DCI as varchar(20)= (	select top 1 CODDCIMED  from dbo.IHLISTPRO where CODPRODUC = @CodigoProducto)

	declare @Count as integer =(select count(*) from  (select  ID
														from dbo.IHPARAMDCI  A inner join 
														dbo.IHDCIMEDI M on A.CODDCIMED = M.CODDCIMED 
														WHERE A.CODDCIMEDPADRE = @DCI  
														union
														select  ID
														from dbo.IHPARAMDCI  A inner join 
														dbo.IHDCIMEDI M on A.CODDCIMEDPADRE = M.CODDCIMED 
														WHERE A.CODDCIMED = @DCI ) as tmp )

    if @count > 0 begin
			set @Disponible =  1
	end else begin
			set @Disponible =  0
	end
	
	 
	return @Disponible
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si un medicamento es LASA (Look-Alike, Sound-Alike), es decir, si tiene nombre o apariencia similar a otros medicamentos, lo cual representa un riesgo de error en la dispensación y administración. Recibe el código interno del producto farmacéutico y consulta su DCI (Denominación Común Internacional) en el catálogo de productos (IHLISTPRO), luego verifica en la tabla de parámetros LASA (IHPARAMDCI) si ese DCI aparece como padre o hijo de alguna relación de similitud registrada. Retorna 1 (verdadero) si el medicamento está involucrado en al menos una relación LASA, o 0 (falso) si no existe ninguna. Se usa en los procesos de prescripción, validación y dispensación de medicamentos para activar alertas de seguridad farmacológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnLASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnLASA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un medicamento es LASA (Look-Alike Sound-Alike) verificando si su DCI tiene relaciones de similitud registradas en la parametrización de DCI.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe existir en el catálogo de productos para poder obtener su DCI asociado; Deben existir registros de parametrización de relaciones entre DCI padre/hijo para detectar similitudes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es un bit (0 o 1), nunca NULL; La búsqueda de similitud es bidireccional: contempla al DCI tanto como padre como hijo en la relación; Solo se considera el primer DCI encontrado para el producto (TOP 1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento LASA (Look-Alike Sound-Alike); DCI (Denominación Común Internacional); Relación padre-hijo entre medicamentos; Catálogo de productos farmacéuticos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno escalar): Si existe al menos un registro en IHPARAMDCI donde el DCI del producto aparezca como padre o como hijo, retorna 1 (es LASA); en caso contrario retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cantidad de relaciones DCI encontradas (como padre o hijo) > 0 → Marca el medicamento como LASA (1) else Marca el medicamento como no LASA (0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; dbo.IHPARAMDCI; dbo.IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnLASA';
GO
