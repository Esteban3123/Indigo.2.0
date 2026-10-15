-- =============================================
-- Author:      Duvan Felipe Chavarro Gutiérrez
-- Create Date: 26 Marzo 2024
-- Description: Unidad funcional de ingreso y egreso según numero de folio
-- =============================================
CREATE FUNCTION [dbo].[GetInunifuncByFolio](@CodigoPaciente VARCHAR(15), @NumeroIngreso VARCHAR(15), @NumeroFolio VARCHAR(5), @Identificador INT)
RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @UFUCodigo NVARCHAR(15);
    DECLARE @UFUDescripcion NVARCHAR(100);
 
    IF @Identificador = 1
    BEGIN
        SELECT TOP 1
			@UFUCodigo = UFUCODIGO
        FROM HCHISPACA
        WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso
		ORDER BY FECHISPAC ASC
    END;
    ELSE IF @Identificador = 2	
    BEGIN
	    -- Verificar si el ingreso es del ámbito ambulatorio u hospitalario, de allí a tomar decisiones
	    DECLARE @TIPOINGRESO INT
		SELECT 
		 @TIPOINGRESO = TIPOINGRE
		FROM ADINGRESO WHERE NUMINGRES = @NumeroIngreso
		IF @TIPOINGRESO = 1 -- Si es ambito ambulatorio, llama la misma UF, de la cual fue ingresado 
		BEGIN
		   RETURN [dbo].[GetInunifuncByFolio](@CodigoPaciente, @NumeroIngreso, @NumeroFolio, 1)
		END;

		DECLARE @Contador As INT;
		WITH TmpResultadoEgreso AS (
         SELECT *
          FROM HCREGEGRE
         WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso AND NUMEFOLIO = @NumeroFolio
        )
		SELECT 
		 @Contador = COUNT(*)
		FROM TmpResultadoEgreso

		IF @Contador = 0
		BEGIN
		 RETURN 'No aplica'
		END

		SELECT 
		   @UFUCodigo = UFUCODIGO
        FROM HCREGEGRE
        WHERE IPCODPACI = @CodigoPaciente AND NUMINGRES = @NumeroIngreso AND NUMEFOLIO = @NumeroFolio 

    END

	SELECT @UFUDescripcion = B.UFUDESCRI
    FROM INUNIFUNC AS B
    WHERE B.UFUCODIGO = @UFUCodigo
 
    RETURN @UFUDescripcion
END