

CREATE VIEW [dbo].[ViewAdmissionsToLiquidationConfirmOnlyLiquidation]
  
AS

-- Vista campos necesarios para la liquidación

SELECT DISTINCT
ING.NUMINGRES AS AdmissionCode,
PAT.IPCODPACI AS PatientCode,
PAT.IPNOMCOMP AS PatientName,
ING.TIPOINGRE AS AdmissionType,
ING.IFECHAING AS AdmissionDate,
ING.CODICAMHO AS BedStay,
ING.ILIQUIDAC AS LiquidationType,
ING.IPRNOMBRE AS ResponsibleName,
ING.IESTADOIN AS Status,
'- Ingresos' AS StatusName,
null TratamientoEspecial
, tpp.Id As ThirdPartyPatientId
FROM dbo.ADINGRESO AS ING with(nolock)
INNER JOIN dbo.INPACIENT AS PAT with(nolock) ON ING.IPCODPACI = PAT.IPCODPACI
--INNER JOIN dbo.ADNIVELES AS NIV with(nolock) ON PAT.NIVCODIGO = NIV.NIVCODIGO
--INNER JOIN dbo.ADCENATEN AS CEN with(nolock) ON ING.CODCENATE = CEN.CODCENATE
--INNER JOIN dbo.INUNIFUNC AS UFU with(nolock) ON ING.UFUCODIGO = UFU.UFUCODIGO
inner join Billing.Invoice bi with(nolock) on bi.AdmissionNumber = ING.NUMINGRES
left join Common.ThirdParty tpp with(nolock) on tpp.Nit = ltrim(rtrim(pat.IPCODPACI))
where ING.IESTADOIN = 'F'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los ingresos o admisiones de pacientes que están en estado ''Finalizado'' (IESTADOIN = ''F'') y que ya tienen al menos una factura de cobro emitida, mostrando únicamente los datos necesarios para el proceso de liquidación y confirmación de cuentas. Combina información del episodio de ingreso (número de ingreso, tipo, fecha, cama, tipo de liquidación y responsable) con los datos del paciente (cédula o código, nombre completo) y el identificador del tercero asociado al paciente si existe en el maestro de terceros. Es utilizada en el módulo de facturación y liquidación para identificar qué admisiones finalizadas ya tienen factura generada y están pendientes de confirmar su liquidación, facilitando el cierre financiero de cada episodio de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los ingresos en estado ''F'' (finalizados/egresados) que ya cuentan con factura, para el proceso de confirmación de liquidación, junto con datos básicos del paciente y su tercero asociado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe estar en estado ''F'' (IESTADOIN = ''F''); Debe existir al menos una factura en Billing.Invoice asociada al número de ingreso; El paciente del ingreso debe existir en INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos en estado ''F''; Solo se exponen ingresos que tengan factura asociada en Billing.Invoice (INNER JOIN); El campo StatusName se fija siempre como ''- Ingresos''; El campo TratamientoEspecial siempre es NULL; El emparejamiento del tercero usa el código del paciente como NIT, aplicando LTRIM/RTRIM; Se aplica DISTINCT para evitar duplicados cuando un ingreso tiene múltiples facturas; Todas las lecturas se realizan con NOLOCK (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/Admisión; Paciente; Liquidación; Tipo de ingreso; Cama/estancia; Responsable del paciente; Estado del ingreso; Factura; Tercero (NIT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ViewAdmissionsToLiquidationConfirmOnlyLiquidation: Devuelve filas DISTINCT de ingresos cuando IESTADOIN = ''F'' y existe Billing.Invoice con AdmissionNumber = NUMINGRES; el tercero se vincula por Nit = TRIM(IPCODPACI) vía LEFT JOIN (puede ser nulo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.IESTADOIN = ''F'' → Se incluye el ingreso en el resultado (candidato a liquidación) else Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; Billing.Invoice; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewAdmissionsToLiquidationConfirmOnlyLiquidation';
GO
