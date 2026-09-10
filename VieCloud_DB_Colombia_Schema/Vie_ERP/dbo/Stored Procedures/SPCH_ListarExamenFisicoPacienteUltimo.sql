

CREATE PROCEDURE [dbo].[SPCH_ListarExamenFisicoPacienteUltimo]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT TOP 1 
(select top 1 RTRIM(TENARTSIS) from HCEXFISIC where IPCODPACI = H.IPCODPACI and TENARTSIS is not null and LEN(TENARTSIS) > 0 order by FECREGITE desc  ) as TENARTSIS,
(select top 1 RTRIM(TENARTDIA) from HCEXFISIC where IPCODPACI = H.IPCODPACI and TENARTDIA is not null and LEN(TENARTDIA) > 0 order by FECREGITE desc  ) as TENARTDIA,
(select top 1 RTRIM(TEMPERPAC) from HCEXFISIC where IPCODPACI = H.IPCODPACI and TEMPERPAC is not null and LEN(TEMPERPAC) > 0 order by FECREGITE desc  ) as TEMPERPAC,
(select top 1 RTRIM(FRECARPAC) from HCEXFISIC where IPCODPACI = H.IPCODPACI and FRECARPAC is not null and LEN(FRECARPAC) > 0 order by FECREGITE desc  ) as FRECARPAC,
(select top 1 RTRIM(FRERESPAC) from HCEXFISIC where IPCODPACI = H.IPCODPACI and FRERESPAC is not null and LEN(FRERESPAC) > 0 order by FECREGITE desc  ) as FRERESPAC,
(select top 1 RTRIM(REGSO2PAC) from HCEXFISIC where IPCODPACI = H.IPCODPACI and REGSO2PAC is not null and LEN(REGSO2PAC) > 0 order by FECREGITE desc  ) as REGSO2PAC,
(select top 1 TALLAPACI from HCEXFISIC where IPCODPACI = H.IPCODPACI and TALLAPACI is not null and LEN(TALLAPACI) > 0 order by FECREGITE desc  ) as TALLAPACI,
(select top 1  PESOPACIE/1000 from HCEXFISIC where IPCODPACI = H.IPCODPACI and PESOPACIE is not null and LEN(PESOPACIE) > 0 order by FECREGITE desc  ) as PESOPACIE,
(select top 1 FECREGITE from HCEXFISIC where IPCODPACI = H.IPCODPACI and TENARTSIS is not null and LEN(TENARTSIS) > 0 order by FECREGITE desc) as FechaRegistro,
P.IPFECNACI as IPFECNACI,
			 Resultado = ISNULL((select TOP 1  CASE 
			    WHEN CAST(E.RESULTADO as int) = 0 THEN CONCAT(E.RESULTADO,' puntos - Sin dolor')
				WHEN CAST(E.RESULTADO as int)  BETWEEN 1 and 4 THEN CONCAT(E.RESULTADO,' puntos - Dolor suave')
			    WHEN CAST(E.RESULTADO as int)  BETWEEN 4 and 6 THEN CONCAT(E.RESULTADO,' puntos - Dolor moderado')
				WHEN CAST(E.RESULTADO as int)  BETWEEN 7 and 10 THEN CONCAT(E.RESULTADO,' puntos - Dolor intenso') 			
				ELSE 'sin escala ' 
			    END
    		    AS DOLOR FROM HCESCALAS E WHERE E.IPCODPACI = H.IPCODPACI AND E.NUMINGRES = H.NUMINGRES AND E.TIPOESCALA = '94' ORDER BY FECHAREGISTRO DESC ), 'Sin escala'), 
P.IPSEXOPAC	as IPSEXOPAC
FROM HCEXFISIC H
	LEFT JOIN INPACIENT P ON P.IPCODPACI = H.IPCODPACI
WHERE H.IPCODPACI = @Paciente 
ORDER BY FECREGITE DESC  

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que retorna el último registro de signos vitales y datos antropométricos de un paciente específico, identificado por su cédula o código. Consolida los valores más recientes de tensión arterial sistólica y diastólica, temperatura, frecuencia cardíaca, frecuencia respiratoria, saturación de oxígeno (SpO2), talla y peso corporal (convertido de gramos a kilogramos), consultados desde el historial de examen físico del paciente. Además incorpora la fecha de nacimiento y el sexo del paciente desde el maestro de pacientes, y el resultado de la escala de dolor (EVA u otra escala tipo 94) obtenido desde las escalas clínicas del ingreso, interpretando el puntaje en categorías: sin dolor, dolor suave, moderado o intenso. Se usa principalmente en la visualización rápida del estado físico actual del paciente en la historia clínica, urgencias u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtener los últimos signos vitales y medidas antropométricas registrados del paciente junto con la última valoración de escala de dolor categorizada e información demográfica básica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código de paciente.; Debe existir al menos un registro de examen físico para el paciente, de lo contrario no se retorna información demográfica ni de signos vitales.; Para evaluar la escala de dolor debe coincidir el ingreso (NUMINGRES) entre el examen físico y la escala registrada con tipo ''94''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para cada signo vital se obtiene el último registro NO nulo y NO vacío en HCEXFISIC del paciente, evaluado independientemente por columna según FECREGITE descendente.; El peso se devuelve convertido de gramos a kilogramos (división entre 1000).; La escala de dolor evaluada corresponde exclusivamente al tipo ''94''.; Si el paciente no tiene escala de dolor registrada para el ingreso, se entrega ''Sin escala'' en lugar de NULL.; Los valores de texto se entregan sin espacios a la derecha (RTRIM).; Solo se retorna una fila (TOP 1) correspondiente al examen físico más reciente del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Examen físico; Signos vitales (tensión arterial sistólica/diastólica, temperatura, frecuencia cardíaca, frecuencia respiratoria, saturación O2); Talla y peso; Escala de dolor; Ingreso hospitalario; Fecha de nacimiento; Sexo del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un único registro con los últimos valores no nulos por cada signo vital del paciente, fecha de nacimiento, sexo y categorización textual del dolor según el resultado numérico de la escala tipo ''94''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Resultado de escala de dolor = 0 → Etiqueta ''Sin dolor''; si Resultado de escala de dolor entre 1 y 4 → Etiqueta ''Dolor suave''; si Resultado de escala de dolor entre 4 y 6 → Etiqueta ''Dolor moderado''; si Resultado de escala de dolor entre 7 y 10 → Etiqueta ''Dolor intenso''; si Resultado de escala de dolor fuera de los rangos definidos → Etiqueta ''sin escala'' else Si no existe registro en HCESCALAS para el paciente con TIPOESCALA=''94'', se devuelve ''Sin escala'' por ISNULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCEXFISIC; dbo.INPACIENT; dbo.HCESCALAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarExamenFisicoPacienteUltimo';
-- GO
