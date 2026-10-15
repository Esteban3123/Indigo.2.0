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