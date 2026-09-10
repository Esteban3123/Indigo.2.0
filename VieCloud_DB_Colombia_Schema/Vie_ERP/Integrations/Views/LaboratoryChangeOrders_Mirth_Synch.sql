CREATE VIEW [Integrations].[LaboratoryChangeOrders_Mirth_Synch] as

SELECT 
A.[AUTO] AS ID, 
INTER.CODCONCEC AS NumeroOrdenIndigo, 
A.FECORDMED AS FechaSolicitud, 
RTRIM(A.IPCODPACI) AS Identificacion, 
A.CODSERIPS AS CodigoServicio, 
A.NUMINGRES AS Ingreso, 
A.NUMEFOLIO AS Folio, 
'INT' AS TipoOrden,
A.SYNCMIRTH
FROM            
dbo.HCORDLABO AS A WITH (nolock) INNER JOIN
dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'INT'
WHERE A.ESTSERIPS = 1 
AND A.FECORDMED >= '2023-11-28 09:00:00.000'
AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')

UNION

SELECT 
A.[AUTO] AS ID, 
INTER.CODCONCEC AS NumeroOrdenIndigo, 
A.FECORDMED AS FechaSolicitud, 
RTRIM(A.IPCODPACI) AS Identificacion, 
A.CODSERIPS AS CodigoServicio, 
A.NUMINGRES AS Ingreso, 
'' AS Folio, 
'AMB' AS TipoOrden,
A.SYNCMIRTH
FROM            
dbo.AMBORDLAB AS A WITH (nolock) INNER JOIN
dbo.INTERDETA AS INTER ON INTER.AUTOLABOR = A.AUTO AND INTER.ORDTIP = 'AMB'
WHERE A.ESTSERIPS = 1 
AND A.FECORDMED >= '2023-11-28 09:00:00.000'
AND A.CODSERIPS NOT IN ('911003','911005','911009','911011_J','911013','911015','911016','911017','911018','911019','911020','911021','911033')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que consolida las órdenes de laboratorio pendientes de sincronización con el motor de integración Mirth Connect, tanto de pacientes hospitalizados (órdenes intrahospitalarias de HCORDLABO) como ambulatorios (AMBORDLAB). Combina mediante UNION ambos tipos de orden, filtrando únicamente las órdenes activas (estado 1) generadas desde noviembre de 2023 y excluyendo un conjunto de servicios que no requieren integración. A través de la tabla INTERDETA obtiene el número de orden interno de Indigo (concepto de interfaz), y expone para cada orden: el identificador interno, la cédula del paciente, el código de servicio CUPS, el número de ingreso, el folio, el tipo de orden (INT=intrahospitalario / AMB=ambulatorio) y el indicador de sincronización con Mirth. Sirve como fuente de datos para que el motor Mirth detecte cambios en órdenes de laboratorio que deben enviarse a sistemas externos como el laboratorio clínico integrado.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'LaboratoryChangeOrders_Mirth_Synch';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone órdenes de laboratorio (hospitalarias y ambulatorias) activas con su consecutivo de interfaz, para sincronización con el motor de integración Mirth.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las órdenes deben estar relacionadas en INTERDETA con su tipo correspondiente (''INT'' para hospitalarias, ''AMB'' para ambulatorias); El estado del servicio (ESTSERIPS) debe ser 1 (activo); La fecha de la orden médica debe ser igual o posterior a 2023-11-28 09:00:00', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen órdenes con servicio activo (ESTSERIPS=1); Se excluyen explícitamente los códigos de servicio: 911003, 911005, 911009, 911011_J, 911013, 911015, 911016, 911017, 911018, 911019, 911020, 911021 y 911033 (servicios no sincronizables con Mirth); Solo se sincronizan órdenes generadas a partir del 28/11/2023 09:00:00; Cada orden retornada debe tener un consecutivo de interfaz (CODCONCEC) en INTERDETA; La identificación del paciente se entrega sin espacios finales (RTRIM); Las órdenes ambulatorias no manejan folio en este flujo de integración', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio hospitalarias; Órdenes de laboratorio ambulatorias; Identificación del paciente; Ingreso; Folio; Código de servicio (CUPS); Consecutivo de interfaz Indigo; Sincronización con Mirth; Tipo de orden (INT/AMB)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Cuando ESTSERIPS=1, FECORDMED>=''2023-11-28 09:00:00'' y existe vínculo en INTERDETA con ORDTIP=''INT'', se retorna la orden hospitalaria con TipoOrden=''INT'' y su Folio; [RETURN_RESULT] Resultset: Cuando ESTSERIPS=1, FECORDMED>=''2023-11-28 09:00:00'' y existe vínculo en INTERDETA con ORDTIP=''AMB'', se retorna la orden ambulatoria con TipoOrden=''AMB'' y Folio vacío', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen de la orden: hospitalaria (HCORDLABO con INTERDETA.ORDTIP=''INT'') → Se marca TipoOrden=''INT'' y se incluye el Folio (NUMEFOLIO) else Si proviene de AMBORDLAB con ORDTIP=''AMB'', se marca TipoOrden=''AMB'' y Folio se devuelve vacío', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INTERDETA; dbo.AMBORDLAB', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'LaboratoryChangeOrders_Mirth_Synch';
GO
