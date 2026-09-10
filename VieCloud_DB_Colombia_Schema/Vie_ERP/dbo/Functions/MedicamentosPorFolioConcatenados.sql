
CREATE FUNCTION [dbo].[MedicamentosPorFolioConcatenados](@CodigoPaciente as varchar(25), @NumIngreso as char(10), @NumFolio as varchar(10))
RETURNS varchar(max)
AS
BEGIN
    DECLARE @Medicamentos as varchar(max)

SELECT 
	@Medicamentos = STRING_AGG(
						CONCAT(	RTRIM(B.DESPRODUC), ' - ',
						RTRIM(CASE WHEN A.FORMAPRESCRIBE IS NOT NULL THEN A.DESADMINI ELSE CASE WHEN A.DOSISPRFN IS NULL THEN A.DESADMINI WHEN A.DURACIDOS='Dosis Unica' THEN RTRIM(CAST(A.DOSISPRFN AS CHAR)) + ' '  
						+ RTRIM(CAST(C.ABRUNIMED AS CHAR)) + ' Dosis Única Via: ' + RTRIM(D.DESVIAADM) ELSE RTRIM(CAST(A.DOSISPRFN AS CHAR)) + ' ' + RTRIM(CAST(C.ABRUNIMED AS CHAR)) + ' Cada ' +  
						RTRIM(CAST(A.FRECUENCI AS CHAR)) + CASE A.UNIFRECUE WHEN '1' THEN ' min(s) ' WHEN '2' THEN ' Hora(s) ' WHEN '3' THEN ' Dia(s) ' END + 'Vía: ' + RTRIM(D.DESVIAADM) END END)), ', ')
        FROM 
            HCPRESCRD A With(Nolock) 
            INNER JOIN IHLISTPRO B With(Nolock) On A.CODPRODUC=B.CODPRODUC
            INNER JOIN HCVIAADMI D With(Nolock) On A.CODVIAADM=D.CODVIAADM 
            LEFT OUTER JOIN INUNIMEDI C With(Nolock) On A.CODUNIMFN=C.CODUNIMED 
        WHERE 
            A.IPCODPACI= @CodigoPaciente AND NUMINGRES = @NumIngreso AND A.MANEXTPRO IN('0','1') AND A.IDETIPHIS<>'CODIGOAZU' AND IDESQUEMAONC IS NULL AND A.NUMEFOLIO= @NumFolio  
    
    RETURN @Medicamentos
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que, dado un paciente (cédula/identificación), un número de ingreso y un número de folio de historia clínica, devuelve en una sola cadena de texto todos los medicamentos prescritos en ese folio, con su descripción completa: nombre del producto farmacéutico, dosis, unidad de medida, frecuencia de administración, vía de administración y duración del tratamiento (incluyendo el caso de dosis única). Combina las prescripciones de la historia clínica (HCPRESCRD) con el catálogo de productos (IHLISTPRO), las vías de administración (HCVIAADMI) y las unidades de medida (INUNIMEDI), excluyendo medicamentos de esquema oncológico y registros de tipo ''CODIGOAZU''. Se utiliza para mostrar o imprimir de forma legible y resumida la medicación de una atención específica, por ejemplo en documentos clínicos, informes de egreso o visualización en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicamentosPorFolioConcatenados';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicamentosPorFolioConcatenados';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en una sola cadena concatenada, la lista de medicamentos prescritos a un paciente en un ingreso y folio determinados, incluyendo descripción, dosis, frecuencia y vía de administración.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir prescripciones (HCPRESCRD) para el paciente, ingreso y folio dados; El producto prescrito debe existir en el catálogo de productos (IHLISTPRO); La vía de administración debe existir en HCVIAADMI; La unidad de medida puede no existir (LEFT JOIN con INUNIMEDI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera prescripciones con MANEXTPRO en (''0'',''1''); Excluye prescripciones cuyo IDETIPHIS sea ''CODIGOAZU''; Excluye prescripciones asociadas a esquema oncológico (IDESQUEMAONC IS NULL); Filtra siempre por la combinación paciente + ingreso + folio; Devuelve los medicamentos como una sola cadena separada por '', ''; Usa lecturas con NOLOCK (lecturas sucias permitidas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamento; prescripción; dosis; vía de administración; unidad de medida; frecuencia de administración; dosis única; paciente; ingreso hospitalario; folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna varchar(max) con los medicamentos concatenados con '', '' aplicando el formato de posología según FORMAPRESCRIBE/DOSISPRFN/DURACIDOS/UNIFRECUE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FORMAPRESCRIBE no es NULL → Usa la descripción de administración (DESADMINI) como texto de posología else Evalúa la dosis para construir la posología; si DOSISPRFN es NULL (con FORMAPRESCRIBE NULL) → Usa solo DESADMINI como descripción else Construye texto detallado con dosis y unidad; si DURACIDOS = ''Dosis Unica'' → Formatea como ''dosis unidad Dosis Única Via: <vía>'' else Formatea con frecuencia: ''dosis unidad Cada N <unidad de tiempo> Vía: <vía>''; si UNIFRECUE = ''1'' / ''2'' / ''3'' → Traduce a ''min(s)'', ''Hora(s)'' o ''Dia(s)'' respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRD; dbo.IHLISTPRO; dbo.HCVIAADMI; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicamentosPorFolioConcatenados';
GO
