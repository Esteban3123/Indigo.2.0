CREATE VIEW [Inventory].[ViewListATCEntityRelatedDCI]
AS

select ROW_NUMBER() OVER (ORDER BY dci.Id) Id, atc.Id ATCId, atc.Code, atc.Name, atc.Code + ' - ' + atc.Name CodeName, dci.Id DCIId
from Inventory.DCI dci
inner join Inventory.DCIATCEntity dciatc on dciatc.IdDCI = dci.Id
inner join Inventory.ATCEntity atc on atc.Id = dciatc.IdATCEntity
where dci.Status = 1 and atc.State = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las categorías ATC (Anatómica, Terapéutica, Química) asociadas a los principios activos (DCI) vigentes en el inventario de farmacia. Combina el catálogo de principios activos (DCI), la relación DCI-ATC y el catálogo de entidades ATC para mostrar, por cada DCI activo, el código y nombre del grupo farmacológico ATC al que pertenece. Solo incluye registros donde tanto el principio activo como la entidad ATC estén en estado activo. Se usa para consultas de clasificación terapéutica de medicamentos, reportería de inventario y validación de formulario farmacológico según estándar internacional ATC.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListATCEntityRelatedDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewListATCEntityRelatedDCI';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las relaciones activas entre principios activos (DCI) y entidades de clasificación ATC, exponiendo código y nombre ATC para cada DCI vigente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Inventory.DCI con Status=1 (DCI activos); Existen registros en Inventory.ATCEntity con State=1 (ATC activas); Existen vínculos en Inventory.DCIATCEntity entre ambas entidades', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen DCIs en estado activo (Status=1); Solo se exponen entidades ATC en estado activo (State=1); El campo CodeName concatena código y nombre ATC separados por '' - ''; Una DCI puede aparecer múltiples veces si está vinculada a varias entidades ATC (relación N:M vía DCIATCEntity); El Id es un número secuencial volátil generado por ROW_NUMBER, no un identificador persistente', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'DCI (Denominación Común Internacional); Clasificación ATC (Anatomical Therapeutic Chemical); Principio activo; Medicamento', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna filas con ATCId, Code, Name, CodeName (Code+'' - ''+Name) y DCIId solo cuando dci.Status=1 y atc.State=1, numeradas con ROW_NUMBER ordenado por DCI.Id', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.DCI; Inventory.DCIATCEntity; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewListATCEntityRelatedDCI';
GO
