CREATE PROCEDURE [dbo].[SPREP_HC_Generales_EsquemasModificados]
(
@CodigoPaciente Varchar(25),
@NumeroFolio nChar(10),
@NumeroIngreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
Rtrim(Schemes.Description) +'  -  Ciclo: ' + RTRIM(Ciclos.CICLO) + '/' + Rtrim(ORD.CICLOS) AS 'NOMBRE ESQUEMA',
RTRIM(MO.DESMOTANU) AS 'MOTIVO DE MODIFICACION', 
RTRIM(Ciclos.OBSERVACIONMOD) AS 'OBSERVACION DE LA MODIFICACION'
FROM [EHR].[HCORDCICLOS] Ciclos With(Nolock)																								 
INNER JOIN [EHR].Schemes Schemes With(Nolock) ON Schemes.Id = Ciclos.SchemesId
INNER JOIN [EHR].HCORDQUIMIO ORD With(Nolock) ON ORD.Id = Ciclos.IDHCORDQUIMIO
INNER JOIN [dbo].HCMOANULB MO With(Nolock) ON MO.CODMOTANU = Ciclos.IDHCMOANULB
WHERE Ciclos.IPCODPACI = @CodigoPaciente AND Ciclos.NUMINGRES= @NumeroIngreso AND Ciclos.NUMEFOLIO = @NumeroFolio AND Ciclos.IDHCMOANULB IS NOT NULL	
      
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de esquemas de quimioterapia modificados para un paciente específico. Dado el código del paciente, el número de ingreso y el número de folio, devuelve los ciclos de tratamiento oncológico que fueron modificados o anulados, mostrando el nombre del esquema terapéutico con el número de ciclo correspondiente, el motivo de la modificación y las observaciones registradas. Integra los ciclos de quimioterapia (HCORDCICLOS), el catálogo de esquemas de tratamiento (Schemes), las órdenes de quimioterapia (HCORDQUIMIO) y los motivos de anulación (HCMOANULB), filtrando únicamente los ciclos que tienen un motivo de modificación asociado. Se utiliza en la historia clínica oncológica para auditar o visualizar cambios realizados sobre los esquemas de tratamiento prescritos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los ciclos de esquemas de quimioterapia que han sido modificados para un paciente, ingreso y folio determinados, mostrando esquema, motivo y observación de la modificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir ciclos en EHR.HCORDCICLOS asociados al paciente, ingreso y folio indicados; Los ciclos deben tener un motivo de anulación/modificación registrado (IDHCMOANULB no nulo); Debe existir relación válida con esquema (Schemes), orden de quimioterapia (HCORDQUIMIO) y motivo (HCMOANULB)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan ciclos que tengan motivo de anulación/modificación asociado (IDHCMOANULB IS NOT NULL); El nombre del esquema se compone como ''Descripción - Ciclo: X/Y'' donde X es el ciclo actual e Y el total de ciclos de la orden; Solo se incluyen ciclos con esquema, orden de quimioterapia y motivo válidos (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Folio; Esquema de quimioterapia; Ciclo de tratamiento oncológico; Orden de quimioterapia; Motivo de anulación/modificación; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando Ciclos.IPCODPACI=@paciente AND NUMINGRES=@ingreso AND NUMEFOLIO=@folio AND IDHCMOANULB IS NOT NULL, retorna nombre del esquema con ciclo, motivo y observación de la modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDCICLOS; EHR.Schemes; EHR.HCORDQUIMIO; dbo.HCMOANULB', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_EsquemasModificados';
-- GO
