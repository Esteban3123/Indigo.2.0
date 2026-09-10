
CREATE PROCEDURE [dbo].[SP_HV_ListarMedicamentosPacienteFolio]
(
@Paciente varchar(25),
@Folio varchar(6)
)
AS
BEGIN
select DCI.DESDCIMED AS Dci,PRO.DESPRODUC AS Atc,PRESC.FRECUENCI AS Frec, PRESC.UNIFRECUE AS UFrec,
PRESC.VALDURFIJ AS Durac, PRESC.UNIDURFIJ AS UDurac,
 RTRIM(DESVIAADM) AS [Route],PRESC.DESADMINI as FormulaManual, PRO.CODUNIPES AS CodUnidadPeso, UNID.CODUNIMED AS CodUnidadPesoFormula, FORMA.DESFORMED AS FormaMedicamento,PRESC.CANPEDPRO AS Cantidad,PRESC.DOSISPRFN AS DosisTotal, PRESC.INDAPLMED AS Indication,PRESC.FECINIDOS AS FechaInicio, 
 MED.MEDPRINOM as MedPriNom,MED.MEDSEGNOM AS MedSegNom, MED.MEDPRIAPEL AS MedPriApe, MED.MEDSEGAPEL AS MedSegApe, MED.CODIGONIT as codProfesional,
 ESP.DESESPECI as Especialidad, MED.IMDIRECCI as MedDireccion, MED.IMTELMOVI as Telefono, UNID.CODHOMHV AS CodHVUnidad, VIA.CODHOMHV AS CodHVRoute
FROM HCPRESCRD PRESC 
INNER JOIN INPROFSAL MED ON PRESC.CODPROSAL = MED.CODPROSAL
INNER JOIN INESPECIA ESP ON ESP.CODESPECI = MED.CODESPEC1
INNER JOIN IHLISTPRO PRO ON PRESC.CODPRODUC=PRO.CODPRODUC 
INNER JOIN IHFORMEDI FORMA ON PRESC.CODFORMED = FORMA.CODFORMED
INNER JOIN IHDCIMEDI DCI ON PRO.CODDCIMED = DCI.CODDCIMED
INNER JOIN HCVIAADMI VIA ON PRESC.CODVIAADM=VIA.CODVIAADM 
LEFT OUTER JOIN INUNIMEDI UNID ON PRESC.CODUNIMFN=UNID.CODUNIMED
where PRESC.IPCODPACI=@Paciente AND PRESC.NUMEFOLIO=@folio AND MANEXTPRO = '1'
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los medicamentos prescritos a un paciente para un folio (número de prescripción) específico, combinando la información de la prescripción con el catálogo de productos farmacéuticos, la clasificación DCI (Denominación Común Internacional), la forma de administración, la vía de administración, la unidad de medida, y los datos del profesional de salud que realizó la prescripción junto con su especialidad. Devuelve detalle clínico completo de cada línea de medicamento: nombre del producto, DCI, dosis, frecuencia, duración, vía de administración, indicación, fecha de inicio y datos del médico prescriptor. Se usa para visualizar o imprimir la hoja de medicamentos activos de un paciente según su folio de historia clínica, filtrando únicamente las prescripciones con aplicación médica activa (MANEXTPRO = 1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el listado de medicamentos prescritos a un paciente en un folio específico, incluyendo datos del fármaco, posología, vía de administración y profesional prescriptor, restringido a prescripciones marcadas como manejo externo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y folio deben existir en HCPRESCRD; Las prescripciones deben tener MANEXTPRO = ''1'' (manejo externo / receta externa); El profesional, especialidad, producto, forma medicamentosa, DCI y vía de administración deben estar referenciados en sus catálogos para que la fila aparezca (joins INNER)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan prescripciones con MANEXTPRO=''1'' (medicamentos de manejo externo); Únicamente se incluyen prescripciones con producto, forma, DCI, vía de administración, profesional y especialidad válidos en catálogos (INNER JOIN); La unidad de medida de la fórmula puede ser nula sin excluir la prescripción (LEFT JOIN a INUNIMEDI); Filtrado por la combinación paciente + folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Folio de atención; Prescripción de medicamentos; DCI (Denominación Común Internacional); ATC (producto); Forma farmacéutica; Vía de administración; Frecuencia y duración de dosis; Dosis total; Indicación médica; Profesional de salud prescriptor; Especialidad médica; Medicamento de manejo externo (receta ambulatoria); Homologación HV (código de interoperabilidad)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCPRESCRD: Cuando PRESC.IPCODPACI=@Paciente y PRESC.NUMEFOLIO=@Folio y MANEXTPRO=''1'', se retorna el conjunto de medicamentos prescritos con sus datos de posología y prescriptor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.INPROFSAL; dbo.INESPECIA; dbo.IHLISTPRO; dbo.IHFORMEDI; dbo.IHDCIMEDI; dbo.HCVIAADMI; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HV_ListarMedicamentosPacienteFolio';
-- GO
