
CREATE VIEW [MixingStation].[ViewWarehouseMaquila] 
as 
	SELECT	
			rmsd.Id Id,
			cd.Id	CampaignDetailId,
			cec.ManagesMaquila,
			rmsd.Source,
			cec.WarehouseId,
			CONCAT(w.Code,' - ',w.Name) WarehouseMaquilaCodeName,
			cec.Status,
			ecc.Code ExternalCareCenterCode,
			cec.CustomerId,
			CONCAT(c.Nit,' - ',c.Name) CustomerNitName
	FROM MixingStation.CampaignDetail cd WITH(NOLOCK)
	JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON cd.Id=rmsd.CampaignDetailId
	JOIN MixingStation.ExternalCareCenter ecc WITH(NOLOCK) ON rmsd.CareCenterCode=ecc.Code
	JOIN MixingStation.ContractExternalClients cec WITH(NOLOCK) ON ecc.ContractExternalClientsId=cec.Id
	JOIN Inventory.Warehouse w WITH(NOLOCK) ON cec.WarehouseId = w.Id
	JOIN Common.Customer c WITH(NOLOCK) ON cec.CustomerId=c.Id
	WHERE cec.ManagesMaquila =1  and rmsd.Source in (2,3) and cec.Status=1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que identifica las bodegas de maquila asociadas a campañas de preparación farmacéutica en la estación de mezclas. Cruza el detalle de cada campaña activa con los centros de atención externos, los contratos de clientes externos que gestionan maquila (ManagesMaquila = 1) y la bodega de inventario asignada a ese contrato, filtrando únicamente solicitudes de origen externo (Source 2 o 3) y contratos vigentes (Status = 1). Devuelve, para cada solicitud de mezcla, el código y nombre de la bodega maquila, el código del centro de atención externo, y el NIT con nombre del cliente (EPS o aseguradora) responsable del contrato, permitiendo trazabilidad de qué bodega atiende cada lote de preparación maquilado para terceros.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewWarehouseMaquila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewWarehouseMaquila';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los detalles de solicitudes de la estación de mezclas asociadas a contratos externos que gestionan maquila, con datos de bodega y cliente, filtrando solo orígenes y contratos válidos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia íntegra entre CampaignDetail, RequestMixingStationDetail, ExternalCareCenter, ContractExternalClients, Warehouse y Customer para que el registro aparezca en la vista.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros cuyo contrato externo tiene ManagesMaquila = 1 (gestiona maquila).; Solo se incluyen detalles de solicitud cuyo Source está en (2,3).; Solo se incluyen contratos externos con Status = 1 (activos).; Cada fila enlaza obligatoriamente un detalle de campaña, detalle de solicitud, centro externo, contrato externo, bodega y cliente (todas las relaciones son INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'maquila; estación de mezclas; centro de atención externo; contrato con cliente externo; bodega; cliente; campaña de preparación', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewWarehouseMaquila: Devuelve filas concatenando Code-Name de la bodega y Nit-Name del cliente solo cuando cec.ManagesMaquila=1 AND rmsd.Source IN (2,3) AND cec.Status=1.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.CampaignDetail; MixingStation.RequestMixingStationDetail; MixingStation.ExternalCareCenter; MixingStation.ContractExternalClients; Inventory.Warehouse; Common.Customer', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewWarehouseMaquila';
GO
