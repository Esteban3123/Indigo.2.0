
CREATE VIEW  [MixingStation].[ViewRequestMixingStationDetailPatient]
AS

    SELECT 
        p.Id,
        p.RequestMixingStationDetailId,
        p.PatientCode,
        p.FunctionalUnitCode,
        p.Bed,
        p.Quantity,
        ps.IPNOMCOMP AS PatientName
    FROM MixingStation.RequestMixingStationDetailPatients p
    JOIN ..INPACIENT ps on p.PatientCode = ps.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que enriquece el detalle de pacientes asignados a solicitudes de mezcla en la estación de farmacia, incorporando el nombre completo del paciente obtenido del maestro de pacientes (INPACIENT). Combina la tabla de pacientes por ítem de solicitud de mezcla con el catálogo general de pacientes, cruzando por el código o cédula del paciente, para exponer en un único resultado el identificador del registro, el ítem de solicitud de mezcla al que pertenece, el código del paciente, la unidad funcional, la cama asignada, la cantidad del preparado y el nombre del paciente. Sirve de base para reportes y pantallas de farmacia que necesitan mostrar, por cada solicitud de mezcla magistral, qué paciente recibirá el preparado, en qué ubicación hospitalaria se encuentra y en qué cantidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewRequestMixingStationDetailPatient';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewRequestMixingStationDetailPatient';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los pacientes asignados al detalle de una solicitud de mezcla enriquecidos con el nombre completo del paciente desde el maestro hospitalario.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de paciente (PatientCode) debe coincidir con un IPCODPACI registrado en el maestro INPACIENT para que la fila sea visible.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros de pacientes de mezcla cuyo PatientCode existe en INPACIENT (JOIN interno: excluye huérfanos sin maestro de paciente).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; estación de mezclas; unidad funcional; cama; solicitud de mezcla', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestMixingStationDetailPatients; INPACIENT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewRequestMixingStationDetailPatient';
GO
