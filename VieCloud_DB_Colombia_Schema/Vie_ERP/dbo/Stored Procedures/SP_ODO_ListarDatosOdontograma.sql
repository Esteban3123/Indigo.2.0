-- Stored Procedure
-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 29/11/2018
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_ODO_ListarDatosOdontograma]
(
  @IdControlOdontograma as Int,
  @TipoOdontograma as varchar(1)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	if @TipoOdontograma = 'V' 
		begin 
			 select * from (
							SELECT 
								D.DIENTE, 
								case OD.TIPO  
									WHEN 1 THEN 'Nivel Diente'
									WHEN 2 THEN 'Vestibular'
									WHEN 3 THEN 'Oclusal'
									WHEN 4 THEN 'Palatina'
									WHEN 5 THEN 'Distal Izquierda'
									WHEN 6 THEN 'Distal Derecha'
									WHEN 7 THEN 'Mesial Derecha'
									WHEN 8 THEN 'Mesial Izquierda'
									WHEN 9 THEN 'Vestibular'
									WHEN 10 THEN 'Lingual'
								end UBICACION,	
								rtrim(N.CODDIAGNO) + ' - ' + RTRIM(N.NOMDIAGNO) as NOMBRE,
								RTRIM(DIAG.OBSERVDIA) AS OBSERVACION
							FROM 
								dbo.ODONTODIENTE D
								INNER JOIN dbo.ODONTODIENTEDIAG OD on D.ID = OD.IDODONTODIENTE
								INNER JOIN dbo.ODOPARDIA DIAG ON OD.CONSECDIA = DIAG.CONSECDIA
								INNER JOIN dbo.INDIAGNOS N on N.CODDIAGNO = DIAG.CODDIAGNO  
							WHERE 
								D.IDODONTOCONTROL=@IdControlOdontograma 
								AND D.TIPOODONTOGRAMA = 'V'
							union all
							SELECT 
								D.DIENTE, 
								case OD.TIPO 
									WHEN 1 THEN 'Nivel Diente'
									WHEN 2 THEN 'Vestibular'
									WHEN 3 THEN 'Oclusal'
									WHEN 4 THEN 'Palatina'
									WHEN 5 THEN 'Distal Izquierda'
									WHEN 6 THEN 'Distal Derecha'
									WHEN 7 THEN 'Mesial Derecha'
									WHEN 8 THEN 'Mesial Izquierda'
									WHEN 9 THEN 'Vestibular'
									WHEN 10 THEN 'Lingual'
								end UBICACION,	
							rtrim(TRATA.CODIGOTRA) + ' - '+ RTRIM(TRATA.DESCRITRA)  as NOMBRE,
							RTRIM(TRATA.DESCRITRA) AS OBSERVACION
							FROM 
								dbo.ODONTODIENTE D
								INNER JOIN dbo.ODONTODIENTETRATA OD on D.ID = OD.IDODONTODIENTE
								INNER JOIN dbo.ODOPARTRA TRATA ON OD.CONSECTRA = TRATA.CONSECTRA 							
							WHERE 
								D.IDODONTOCONTROL=@IdControlOdontograma 
								AND D.TIPOODONTOGRAMA = 'V' 
							) as Tmp order by tmp.DIENTE  asc

		end
	else
		begin

			SELECT
				D.DIENTE,
				case OD.TIPO 
					WHEN 1 THEN 'Nivel Diente' 
					WHEN 2 THEN 'Vestibular' 
					WHEN 3 THEN 'Oclusal' 
					WHEN 4 THEN 'Palatina' 
					WHEN 5 THEN 'Distal Izquierda' 
					WHEN 6 THEN 'Distal Derecha' 
					WHEN 7 THEN 'Mesial Derecha' 
					WHEN 8 THEN 'Mesial Izquierda' 
					WHEN 9 THEN 'Vestibular' 
					WHEN 10 THEN 'Lingual' 
				end UBICACION,	
				rtrim(TRATA.CODIGOTRA) + ' - '+  RTRIM(TRATA.DESCRITRA)  as NOMBRE,
				rtrim(s.CODSERIPS) + ' - ' + rtrim(S.DESSERIPS) as CUPS
			FROM 
				dbo.ODONTODIENTE D
				INNER JOIN dbo.ODONTODIENTETRATA OD on D.ID = OD.IDODONTODIENTE
				INNER JOIN dbo.ODOPARTRA TRATA ON OD.CONSECTRA = TRATA.CONSECTRA
				inner join DBO.INCUPSIPS S on S.CODSERIPS = OD.CODSERIPS  
			WHERE 
				D.IDODONTOCONTROL=@IdControlOdontograma 
				AND D.TIPOODONTOGRAMA = 'T' 
			order by D.DIENTE  

		end

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista todos los datos clínicos registrados en un odontograma para un control odontológico específico, diferenciando entre dos modos de consulta según el tipo de odontograma solicitado. Cuando el tipo es ''V'' (valoración), retorna tanto los diagnósticos bucales por pieza dental (con su código CIE-10, nombre y ubicación en la cara del diente) como los tratamientos odontológicos asociados, combinando información de ODONTODIENTE, ODONTODIENTEDIAG, ODOPARDIA, INDIAGNOS, ODONTODIENTETRATA y ODOPARTRA. Cuando el tipo es ''T'' (tratamiento), retorna únicamente los tratamientos planificados por diente junto con el código CUPS del procedimiento, permitiendo visualizar el plan de tratamiento dental. Se usa para renderizar gráficamente el odontograma en la historia clínica odontológica del paciente, mostrando diagnósticos y tratamientos por pieza dental y por cara (vestibular, oclusal, palatina, mesial, distal, lingual).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ODO_ListarDatosOdontograma';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los hallazgos (diagnósticos y tratamientos) registrados sobre cada diente de un control odontológico, diferenciando entre odontograma de valoración (V) y de tratamiento (T).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el control odontológico identificado y dientes asociados en ODONTODIENTE.; El tipo de odontograma debe ser ''V'' (valoración) o cualquier otro valor que se interpreta como ''T'' (tratamiento).; Para tipo ''T'' los tratamientos deben tener CUPS válido en INCUPSIPS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ubicación del hallazgo se traduce desde un código numérico (1-10) a etiquetas anatómicas (Nivel Diente, Vestibular, Oclusal, Palatina, Distal/Mesial Izq/Der, Lingual).; Los códigos 2 y 9 se mapean ambos a ''Vestibular''.; El odontograma de valoración (''V'') incluye diagnósticos; el de tratamiento (''T'') no.; Solo el odontograma de tratamiento expone el código CUPS del servicio.; Los nombres se construyen como ''codigo - descripción'' con RTRIM.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Odontograma; Diente; Diagnóstico odontológico; Tratamiento odontológico; Superficies dentales (vestibular, oclusal, palatina, lingual, mesial, distal); CUPS; Control odontológico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Cuando @TipoOdontograma=''V'' devuelve la unión de diagnósticos (ODOPARDIA/INDIAGNOS) y tratamientos (ODOPARTRA) de los dientes con TIPOODONTOGRAMA=''V'', ordenado por DIENTE.; [RETURN_RESULT] RESULTSET: Cuando @TipoOdontograma<>''V'' devuelve los tratamientos (ODOPARTRA) con su CUPS (INCUPSIPS) de los dientes con TIPOODONTOGRAMA=''T'', ordenado por DIENTE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoOdontograma = ''V'' → Consulta el odontograma de valoración: une diagnósticos y tratamientos de dientes marcados con TIPOODONTOGRAMA=''V''. else Consulta el odontograma de tratamiento: solo tratamientos con su código CUPS de dientes con TIPOODONTOGRAMA=''T''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ODONTODIENTE; dbo.ODONTODIENTEDIAG; dbo.ODOPARDIA; dbo.INDIAGNOS; dbo.ODONTODIENTETRATA; dbo.ODOPARTRA; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ODO_ListarDatosOdontograma';
-- GO
