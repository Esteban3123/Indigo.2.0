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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que retorna el nombre de la unidad funcional (servicio o sala de atención) asociada a un folio clínico específico de un paciente. Según el identificador recibido: si es 1, busca la unidad funcional del primer folio registrado en la historia clínica del ingreso (HCHISPACA); si es 2, determina si el ingreso es ambulatorio u hospitalario (ADINGRESO) y, para hospitalizaciones, consulta la unidad funcional del registro de egreso (HCREGEGRE), devolviendo ''No aplica'' cuando no existe egreso registrado. En ambos casos, traduce el código de unidad funcional a su descripción legible consultando el catálogo maestro de unidades (INUNIFUNC). Se usa para identificar en qué servicio, sala o área fue atendido o dado de alta el paciente durante un episodio de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetInunifuncByFolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetInunifuncByFolio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la descripción de la unidad funcional asociada a un episodio de atención, según se trate del ingreso o del egreso del paciente, traduciendo el código a su nombre legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el número de ingreso deben existir en las tablas de historia clínica para retornar una unidad funcional válida.; Para identificador=2 con egreso, debe existir un registro en HCREGEGRE con el folio indicado; si no existe, retorna ''No aplica''.; El código de unidad funcional resultante debe existir en INUNIFUNC para obtener descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Para ingresos ambulatorios la unidad funcional siempre proviene del primer registro de historia clínica (HCHISPACA), no del egreso.; Para ingresos hospitalarios sin egreso registrado, nunca se devuelve descripción de unidad sino el literal ''No aplica''.; La descripción retornada siempre se obtiene del catálogo maestro INUNIFUNC.; En la rama de identificador=1 se usa siempre el registro más antiguo (FECHISPAC ASC) del episodio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad funcional; Folio de atención; Ingreso del paciente; Egreso hospitalario; Ámbito ambulatorio; Ámbito hospitalario; Historia clínica; Episodio de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INUNIFUNC: Devuelve la descripción (UFUDESCRI) de la unidad funcional cuyo código fue resuelto desde HCHISPACA o HCREGEGRE.; [RETURN_RESULT] HCREGEGRE: Cuando identificador=2 y el ingreso es hospitalario y no existe registro de egreso para el folio, retorna literalmente ''No aplica''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Identificador = 1 → Toma la unidad funcional del primer registro de historia clínica del paciente/ingreso ordenado por fecha ascendente (HCHISPACA). else Evalúa rama de identificador=2.; si Identificador = 2 y TIPOINGRE = 1 (ambulatorio) en ADINGRESO → Invoca recursivamente la función con identificador=1 para reutilizar la unidad funcional del ingreso. else Procesa como hospitalario consultando HCREGEGRE.; si Identificador = 2, hospitalario, y no hay registros en HCREGEGRE para el paciente/ingreso/folio → Retorna ''No aplica'' sin consultar el catálogo de unidades. else Toma UFUCODIGO desde HCREGEGRE y devuelve su descripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.GetInunifuncByFolio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.ADINGRESO; dbo.HCREGEGRE; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetInunifuncByFolio';
GO
