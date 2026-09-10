
CREATE PROCEDURE [dbo].[SPHC_Pathologies_EMR]
(
    @Paciente varchar(20),
    @NumeroIngreso CHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RTRIM(F.IPNOMCOMP) AS Nombre,
        F.IPPRINOMB AS PrimerNombre,
        F.IPSEGNOMB AS SegundoNombre,
        F.IPPRIAPEL AS PrimerApellido,
        F.IPSEGAPEL AS SegundoApellido,
        F.CODIGONIT AS NumeroDocumento,
        F.IPFECNACI AS FechaNacimiento,
        CASE F.IPSEXOPAC 
            WHEN '1' THEN 'male' 
            WHEN '2' THEN 'female' 
            ELSE 'other' 
        END AS Sexo,
        RTRIM(D.UFUDESCRI) AS Unidad,
        RTRIM(H.DESCCAMAS) AS Cama,
        RTRIM(E.DESSERIPS) AS Servicio,
        A.FECORDMED AS FechaSolicitud,   
        CASE A.ESTSERIPS
            WHEN '1' THEN 'requested'
            WHEN '2' THEN 'request_sent'
            WHEN '3' THEN 'interconsultation_completed'
            WHEN '4' THEN 'extramural'
            WHEN '5' THEN 'canceled'
            WHEN '6' THEN 'pending_specialist_verification'
        END AS Estado,
        CD.Name AS DescripcionRelacionada,
        A.OBSSERIPS AS Observacion,
        CASE A.PRISERIPS
            WHEN '1' THEN 'urgent'
            WHEN '2' THEN 'routine'
        END AS TipoExamen,
        I.NOMDIAGNO AS Diagnostico
    FROM dbo.HCORDPATO A
    INNER JOIN dbo.INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
    INNER JOIN dbo.INPACIENT F ON A.IPCODPACI = F.IPCODPACI
    INNER JOIN dbo.ADINGRESO G ON F.IPCODPACI = G.IPCODPACI
    INNER JOIN dbo.INDIAGNOS I ON A.CODDIAGNO = I.CODDIAGNO
    INNER JOIN dbo.CHCAMASHO H ON G.CODCAMACT = H.CODICAMAS
    INNER JOIN dbo.ADcenaten C ON A.CODCENATE = C.codcenate
    INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO = D.UFUCODIGO
    INNER JOIN dbo.INCUPSIPS E ON A.CODSERIPS = E.CODSERIPS
    INNER JOIN dbo.HCHISPACA J ON A.NUMEFOLIO = J.NUMEFOLIO AND A.IPCODPACI = J.IPCODPACI   
    LEFT JOIN contract.CUPSEntityContractDescriptions CDD 
        ON CDD.Id = A.IDDESCRIPCIONRELACIONADA
    LEFT JOIN contract.ContractDescriptions CD 
        ON CD.Id = CDD.ContractDescriptionId
    WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @NumeroIngreso;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que recupera las órdenes de patología e imágenes diagnósticas asociadas a un paciente y su número de ingreso específico. Consolida datos demográficos del paciente, ubicación hospitalaria (unidad funcional y cama), el servicio CUPS solicitado, el diagnóstico CIE-10, el estado de la orden (solicitada, enviada, completada, cancelada, etc.), la prioridad (urgente/rutina) y una descripción contractual relacionada opcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera la información clínica y administrativa de las órdenes de patología asociadas a un paciente y a un ingreso específico, incluyendo datos demográficos, ubicación hospitalaria, estado, prioridad y diagnóstico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir en HCORDPATO con órdenes de patología registradas.; El paciente debe tener un ingreso (ADINGRESO) con cama actual válida en CHCAMASHO.; La orden debe tener diagnóstico, profesional, unidad funcional, servicio CUPS y centro de atención válidos para satisfacer los INNER JOIN.; Debe existir historia clínica (HCHISPACA) ligada al folio y paciente de la orden.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan órdenes de patología que tengan diagnóstico, profesional, unidad funcional, servicio, centro de atención, cama e historia clínica asociados (por uso de INNER JOIN).; La descripción contractual relacionada es opcional (LEFT JOIN); puede venir nula.; El sexo siempre se normaliza a male/female/other.; Los estados y prioridades se exponen como códigos en inglés, no como valores numéricos crudos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Orden de patología; Diagnóstico clínico; Unidad funcional; Cama hospitalaria; Servicio CUPS; Historia clínica; Estado de solicitud; Prioridad de examen (urgente/rutinario); Descripción contractual', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve una fila por orden de patología filtrando por paciente e ingreso (A.IPCODPACI=@Paciente AND A.NUMINGRES=@NumeroIngreso), traduciendo sexo, estado y prioridad a códigos textuales.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPSEXOPAC = ''1'' → Sexo se mapea a ''male'' else Si ''2'' → ''female''; cualquier otro valor → ''other''; si ESTSERIPS in 1..6 → Estado se traduce a: 1=requested, 2=request_sent, 3=interconsultation_completed, 4=extramural, 5=canceled, 6=pending_specialist_verification else NULL si no coincide; si PRISERIPS = ''1'' → TipoExamen=''urgent'' else Si ''2'' → ''routine''; otro valor → NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDPATO; dbo.INPROFSAL; dbo.INPACIENT; dbo.ADINGRESO; dbo.INDIAGNOS; dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.INCUPSIPS; dbo.HCHISPACA; contract.CUPSEntityContractDescriptions; contract.ContractDescriptions', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_Pathologies_EMR';
-- GO
