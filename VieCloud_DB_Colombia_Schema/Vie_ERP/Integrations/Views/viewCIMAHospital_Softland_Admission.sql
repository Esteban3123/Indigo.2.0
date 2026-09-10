

-- Ordenes Asignadas sin gestionar
CREATE VIEW [Integrations].[viewCIMAHospital_Softland_Admission]
AS

select
Sync.id,
Sync.Type,
case SYnc.Action when 1 then 'Insert' when 2 then 'update' when 3 then 'delete' end Action,
Sync.TransactionDate,
Sync.State,
I.NUMINGRES as NumeroIngreso,
I.IFECHAING as FechaIngreso,
'Admitido' as EstadoPaciente,
'Externo' as TipoVisita,
'Urgencias' as TipoPaciente,
'Urgencias' as NivelAtencion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = I.UFUCODIGO  and MasterType = 'UnidadFuncional' ) as Ubicacion,
'ND' as Profesional,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = P.IPTIPODOC and MasterType = 'TipoDocumento' ) as TipoIdentificacion,
(select top 1 LegacyCode from [Integrations].[CIMAHospital_HomologationMaster] where IndigoCode = I.GENCONENTITY and MasterType = 'Aseguradoras' ) as Aseguradora,
--P.IPCODPACI as IdentificacionPaciente,
[Integrations].[GetIdentificationSoftland](P.IPTIPODOC,P.IPCODPACI) as IdentificacionPaciente,
rtrim(ltrim(p.IPNOMCOMP)) as NombrePaciente,
P.IPTIPODOC,
P.IPCODPACI as IdentificacionReal
from ADINGRESO I inner join
INPACIENT p on I.IPCODPACI = p.IPCODPACI inner join 
Integrations.CIMAHospital_Softland_Synch sync on sync.DataId = I.NUMINGRES  
where sync.State = 0 AND sync.Type = 1 -- 1 : ingresos
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de integración que expone los ingresos o admisiones de pacientes pendientes de sincronización entre el sistema CIMA Hospital y el ERP Softland. Combina datos del episodio de ingreso (número de ingreso, fecha, unidad funcional) con la información del paciente (nombre, tipo y número de documento/cédula) y los traduce al vocabulario de CIMA Hospital usando la tabla de homologación de códigos maestros (unidad funcional, tipo de documento, aseguradoras). Solo muestra los registros de tipo ''ingresos'' cuyo estado de sincronización está pendiente (State=0, Type=1), permitiendo al proceso de integración identificar qué admisiones deben enviarse o actualizarse en Softland.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Admission';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'VIEW', @level1name = N'viewCIMAHospital_Softland_Admission';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos hospitalarios pendientes de sincronización hacia Softland, enriquecidos con datos del paciente y homologaciones de códigos entre Indigo y el sistema legado.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la cola de sincronización con State=0 (pendiente) y Type=1 (ingresos); El DataId de la cola de sincronización debe corresponder a un NUMINGRES válido en ADINGRESO; El ingreso debe tener un paciente asociado existente en INPACIENT; Deben existir homologaciones cargadas para UnidadFuncional, TipoDocumento y Aseguradoras en la tabla maestra', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Todos los ingresos expuestos se clasifican como EstadoPaciente=''Admitido'', TipoVisita=''Externo'', TipoPaciente=''Urgencias'' y NivelAtencion=''Urgencias''; El profesional siempre se reporta como ''ND'' (no disponible); Las homologaciones se resuelven tomando el primer LegacyCode encontrado por IndigoCode y MasterType; El nombre del paciente se entrega sin espacios iniciales ni finales; Solo se expone información de ingresos con sincronización pendiente (State=0) del tipo ingreso (Type=1)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Paciente; Admisión; Urgencias; Unidad Funcional; Tipo de Documento; Aseguradora; Homologación de códigos; Sincronización con ERP Softland; Identificación del paciente', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Solo retorna ingresos cuya transacción de sincronización tenga State=0 y Type=1 (ingresos pendientes)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Action = 1 → Se etiqueta como ''Insert''; si Action = 2 → Se etiqueta como ''update''; si Action = 3 → Se etiqueta como ''delete''', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Integrations.GetIdentificationSoftland', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADINGRESO; INPACIENT; Integrations.CIMAHospital_Softland_Synch; Integrations.CIMAHospital_HomologationMaster', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'VIEW', @level1name=N'viewCIMAHospital_Softland_Admission';
GO
