
CREATE VIEW [Contract].[ViewListIPSServiceWithHomologations]
AS

select CONCAT(ips.Id, ' - ', ISNULL(ch.Id, 0)) Row, 
	ips.Id, ips.Code, ips.Name, ips.ServiceManual, 
	case ips.ServiceManual 
		when 1 then 'ISS 2001' 
		when 2 then 'ISS 2004' 
		when 3 then 'SOAT' 
		when 4 then 'Institucional'
	else '' end ServiceManualName,
	ips.ServiceClass,
	case ips.ServiceClass 
		when 1 then 'Ninguno' 
		when 2 then 'Cirujano' 
		when 3 then 'Anestesiólogo' 
		when 4 then 'Ayudante' 
		when 5 then 'Derecho Sala' 
		when 6 then 'Materiales Sutura' 
		when 7 then 'Instrumentación Quirúrgica' 
	else '' end ServiceClassName,
	ips.Presentation, 
	case ips.Presentation 
		when 1 then 'No Quirúrgico' 
		when 2 then 'Quirúrgico' 
		when 3 then 'Paquete' 		
	else '' end PresentationName,
	0 SelectOption, ips.ServiceType, ips.Status, IIF(ips.Status = 1, 'Activo', 'Inactivo') StatusName,
	CONCAT(ips.Code, ' - ', ips.Name) CodeName, 
	ch.CupsEntityId, 
	ce.Code CupsEntityCode, 
	ce.Description CupsEntityName, 
	CONCAT(ce.Code, ' - ', ce.Description) CupsEntityCodeName,
	isnull(gli.Percentage, 0) AS IVAValue
from Contract.IPSService ips
left join Contract.CupsHomologation ch on ch.IPSServiceId = ips.Id
left join Contract.CUPSEntity ce on ce.Id = ch.CupsEntityId
LEFT JOIN GeneralLedger.GeneralLedgerIVA gli (nolock) ON ips.IVAId = gli.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de servicios de salud propios de la IPS junto con sus homologaciones a códigos CUPS de entidades contratantes. Combina el catálogo interno de servicios (con su manual tarifario —ISS 2001, ISS 2004, SOAT o Institucional—, clase quirúrgica, presentación, estado y porcentaje de IVA) con el código CUPS equivalente que utiliza cada entidad contratante para ese mismo servicio. Sirve para configuración y consulta en procesos de contratación, facturación y RIPS, permitiendo identificar a qué código CUPS externo corresponde cada procedimiento o servicio interno de la institución.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListIPSServiceWithHomologations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListIPSServiceWithHomologations';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios propios de la IPS con sus homologaciones a códigos CUPS de entidades y el porcentaje de IVA asociado, decodificando manuales tarifarios, clases y presentación para visualización.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La columna Row se construye como concatenación del Id del servicio IPS con el Id de la homologación CUPS (o 0 si no existe), garantizando una clave de fila aún sin homologación.; Si el servicio no tiene IVA asociado o el IVA no existe en GeneralLedgerIVA, el porcentaje IVAValue se devuelve como 0.; Un servicio IPS con múltiples homologaciones CUPS produce múltiples filas (una por homologación).', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios IPS; Homologación CUPS; Manual tarifario (ISS 2001, ISS 2004, SOAT, Institucional); Clase de servicio quirúrgico (Cirujano, Anestesiólogo, Ayudante, Derecho de Sala, Materiales de Sutura, Instrumentación Quirúrgica); Presentación del servicio (Quirúrgico, No Quirúrgico, Paquete); IVA', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Contract.IPSService: Devuelve todos los servicios IPS con LEFT JOIN a homologaciones CUPS, entidad CUPS e IVA, por lo que un servicio sin homologación aparece igualmente con CupsEntityId nulo.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ips.ServiceManual = 1/2/3/4 → Etiqueta como ''ISS 2001'', ''ISS 2004'', ''SOAT'' o ''Institucional'' respectivamente else Cadena vacía; si ips.ServiceClass entre 1 y 7 → Etiqueta como Ninguno/Cirujano/Anestesiólogo/Ayudante/Derecho Sala/Materiales Sutura/Instrumentación Quirúrgica else Cadena vacía; si ips.Presentation = 1/2/3 → Etiqueta como ''No Quirúrgico'', ''Quirúrgico'' o ''Paquete'' else Cadena vacía; si ips.Status = 1 → StatusName = ''Activo'' else StatusName = ''Inactivo''', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService; Contract.CupsHomologation; Contract.CUPSEntity; GeneralLedger.GeneralLedgerIVA', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListIPSServiceWithHomologations';
GO
