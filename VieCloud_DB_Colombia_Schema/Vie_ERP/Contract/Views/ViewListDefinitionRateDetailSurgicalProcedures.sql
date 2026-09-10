

CREATE VIEW [Contract].[ViewListDefinitionRateDetailSurgicalProcedures]
AS

select drdsp.Id, drdsp.DefinitionRateDetailId,
drdsp.IPSServiceId, drdsp.SurgicalProcedureServiceId,
ips.Code IPSServiceCode, ips.Name IPSServiceName,
case ips.ServiceClass 
	when 1 then 'Ninguno' 
	when 2 then 'Cirujano' 
	when 3 then 'Anesteciologo' 
	when 4 then 'Ayudante' 
	when 5 then 'Derecho Sala' 
	when 6 then 'Materiales Sutura' 
	when 7 then 'Instrumentacion Quirurgica' 
	else ''
end ServiceClassName
from Contract.DefinitionRateDetailSurgicalProcedures drdsp
inner join Contract.IPSService ips on ips.Id = drdsp.IPSServiceId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los procedimientos quirúrgicos asociados a cada detalle de tarifa contractual, combinando la información del servicio IPS (código, nombre) con la clasificación del rol quirúrgico al que aplica: Cirujano, Anestesiólogo, Ayudante, Derecho de Sala, Materiales de Sutura o Instrumentación Quirúrgica. Sirve para consultar y reportar qué servicios quirúrgicos están incluidos en cada definición de tarifa de un contrato, con la descripción legible del tipo de participante quirúrgico en lugar del código numérico interno.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'VIEW', @level1name = N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el listado de detalles de tarifa de procedimientos quirúrgicos enriquecido con el código, nombre y clase de servicio (cirujano, anestesiólogo, ayudante, etc.) del servicio IPS asociado.', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de detalle de tarifa quirúrgica debe referenciar un servicio IPS existente para ser visible', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen detalles de tarifa de procedimientos quirúrgicos que tengan un servicio IPS válido y existente (INNER JOIN obliga la correspondencia); Toda fila de salida tiene una etiqueta legible de clase de servicio, aunque sea vacía cuando el código no está catalogado', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tarifa contractual; Detalle de tarifa; Procedimiento quirúrgico; Servicio IPS; Clase de servicio quirúrgico (Cirujano, Anestesiólogo, Ayudante, Derecho de Sala, Materiales de Sutura, Instrumentación Quirúrgica)', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ServiceClass del servicio IPS → Se traduce el código numérico a su etiqueta de clase de servicio quirúrgico: 1=Ninguno, 2=Cirujano, 3=Anesteciologo, 4=Ayudante, 5=Derecho Sala, 6=Materiales Sutura, 7=Instrumentacion Quirurgica else Cadena vacía cuando el código no corresponde a ninguno de los valores definidos', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.DefinitionRateDetailSurgicalProcedures; Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Contract', @level1type=N'VIEW', @level1name=N'ViewListDefinitionRateDetailSurgicalProcedures';
GO
