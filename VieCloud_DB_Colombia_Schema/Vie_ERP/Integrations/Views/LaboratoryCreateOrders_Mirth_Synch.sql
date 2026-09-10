CREATE VIEW [Integrations].[LaboratoryCreateOrders_Mirth_Synch] as

SELECT A.[AUTO] AS ID,
	   INTER.CODCONCEC AS NumeroOrdenIndigo,
	   A.FECORDMED AS FechaSolicitud,
	   RTRIM(A.IPCODPACI) AS Identificacion,
	   A.NUMINGRES AS Ingreso,
	   A.NUMEFOLIO AS Folio,
	   'INT' AS TipoOrden,
	   CASE WHEN C.IPSSERIAD = 1 THEN 'SI' ELSE 'NO' END Seriado,
	   CASE WHEN A.SYNCMIRTH = 1 THEN 'SI' ELSE 'NO' END Sincronizacado
FROM dbo.HCORDLABO AS A WITH (nolock)
	 LEFT JOIN dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'INT'
	 INNER JOIN dbo.INCUPSIPS C ON C.CODSERIPS = A.CODSERIPS
WHERE A.ESTSERIPS = 1
	  AND INTER.CODCONCEC IS NULL
	  AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0)
	  AND A.FECORDMED >= '2023-11-28 09:00:00.000'
	  AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')

UNION

SELECT A.[AUTO] AS ID,
	   INTER.CODCONCEC AS NumeroOrdenIndigo,
	   A.FECORDMED AS FechaSolicitud,
	   RTRIM(A.IPCODPACI) AS Identificacion,
	   A.NUMINGRES AS Ingreso,
	   '' AS Folio,
	   'AMB' AS TipoOrden,
	   CASE WHEN C.IPSSERIAD = 1 THEN 'SI' ELSE 'NO' END Seriado,
	   CASE WHEN A.SYNCMIRTH = 1 THEN 'SI' ELSE 'NO' END Sincronizacado
FROM dbo.AMBORDLAB AS A WITH (nolock)
	 LEFT JOIN dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'AMB'
	 INNER JOIN dbo.INCUPSIPS C ON C.CODSERIPS = A.CODSERIPS
WHERE A.ESTSERIPS = 1
	  AND INTER.CODCONCEC IS NULL
	  AND (SYNCMIRTH IS NULL OR SYNCMIRTH = 0)
	  AND A.FECORDMED >= '2023-11-28 09:00:00.000'
	  AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de laboratorio clínico (tanto intrahospitalarias como ambulatorias) que aún no han sido sincronizadas con el sistema de integración Mirth Connect y que tampoco tienen número de orden generado en la interfaz externa. Combina órdenes de historia clínica (HCORDLABO) y órdenes ambulatorias (AMBORDLAB) que están activas, filtrando códigos de servicio excluidos y órdenes anteriores a noviembre 2023, enriqueciendo cada registro con información del servicio CUPS (si es seriado) y el concepto de interfaz (INTERDETA). Su propósito es alimentar el proceso de integración para la creación de órdenes en el laboratorio externo, permitiendo identificar qué solicitudes de exámenes de laboratorio — con su cédula del paciente, número de ingreso, folio y tipo de orden (intrahospitalaria o ambulatoria) — están pendientes de ser enviadas a Mirth.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryCreateOrders_Mirth_Synch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la lista unificada de órdenes de laboratorio (hospitalarias y ambulatorias) pendientes de ser sincronizadas hacia el motor de integración Mirth, indicando si son seriadas y su estado de sincronización.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas HCORDLABO/AMBORDLAB deben tener registros con ESTSERIPS=1 y FECORDMED posterior al corte 2023-11-28 09:00:00.; Cada orden debe tener un CODSERIPS válido existente en INCUPSIPS (INNER JOIN).; Para considerarse pendiente de envío, la orden no debe tener consecutivo previo en INTERDETA y no debe estar marcada como sincronizada (SYNCMIRTH NULL o 0).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con estado de servicio activo (ESTSERIPS = 1).; Solo se exponen órdenes que aún no tienen consecutivo asignado en INTERDETA (CODCONCEC IS NULL).; Solo se exponen órdenes no sincronizadas previamente con Mirth (SYNCMIRTH NULL o 0).; Se aplica un corte temporal fijo: solo órdenes con FECORDMED desde 2023-11-28 09:00:00 en adelante.; Se excluyen siempre los servicios CUPS de la lista negra: 911003, 911005, 911009, 911011_J, 911013, 911015, 911016, 911017, 911018, 911019, 911020, 911021, 911033.; El cruce con INTERDETA se restringe al tipo de orden correspondiente (ORDTIP=''INT'' para hospitalarias, ''AMB'' para ambulatorias).; El resultado unifica órdenes hospitalarias y ambulatorias en un mismo conjunto mediante UNION (elimina duplicados exactos).; La identificación del paciente se entrega sin espacios a la derecha (RTRIM).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de laboratorio hospitalaria (internación); Orden de laboratorio ambulatoria; Servicio CUPS; Examen seriado; Sincronización con Mirth (motor de integración); Folio de ingreso; Identificación del paciente', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCORDLABO: Cuando ESTSERIPS=1, sin consecutivo en INTERDETA (ORDTIP=''INT''), SYNCMIRTH NULL/0, FECORDMED >= 2023-11-28 09:00:00 y CODSERIPS no está en la lista de exclusión, se retorna la orden como TipoOrden=''INT''.; [RETURN_RESULT] dbo.AMBORDLAB: Cuando ESTSERIPS=1, sin consecutivo en INTERDETA (ORDTIP=''AMB''), SYNCMIRTH NULL/0, FECORDMED >= 2023-11-28 09:00:00 y CODSERIPS no está en la lista de exclusión, se retorna la orden como TipoOrden=''AMB'' con Folio vacío.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSSERIAD = 1 en INCUPSIPS → Marca la orden como Seriado=''SI'' else Seriado=''NO''; si SYNCMIRTH = 1 en la orden → Sincronizacado=''SI'' else Sincronizacado=''NO''; si Origen de la fila → Si proviene de HCORDLABO se etiqueta TipoOrden=''INT'' e incluye Folio (NUMEFOLIO) else Si proviene de AMBORDLAB se etiqueta TipoOrden=''AMB'' y Folio se devuelve vacío', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.INTERDETA; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryCreateOrders_Mirth_Synch';
GO
