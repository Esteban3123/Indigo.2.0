
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