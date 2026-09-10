-- =============================================
-- Author:      <Giovanny Plazas>
-- Create Date: <2023-06-18>
-- Description: <Funcion para buscar si un producto es en custodia>
-- =============================================
CREATE FUNCTION [Inventory].[FlagCustodyProduct]
(
    @CODPRODUC VARCHAR(20),
	@AdmissionNumber varchar(10)
)
RETURNS BIT
AS
BEGIN
	DECLARE @Flag BIT  =0

	IF @CODPRODUC is null OR @CODPRODUC='' or @AdmissionNumber  is null or @AdmissionNumber=''
	begin
		return  0
	end

	SELECT top 1 @Flag = isnull(hcd.MEDICACUSTODIA,0)
	FROM HCFARMEPD hcd WITH(NOLOCK)
	where hcd.NUMINGRES=@AdmissionNumber and hcd.CODPRODUC=@CODPRODUC
	order by hcd.ID DESC

	return @Flag
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que determina si un medicamento o producto farmacéutico está bajo régimen de custodia para un ingreso hospitalario específico. Recibe el código del producto y el número de ingreso del paciente, y consulta el historial de dispensación farmacéutica (HCFARMEPD) para obtener el indicador de medicamento en custodia del registro más reciente. Retorna un valor booleano (1 = es producto en custodia, 0 = no lo es o no se encontró información). Se usa en el módulo de inventario y farmacia para controlar y señalizar los medicamentos que requieren manejo especial por custodia dentro de la hospitalización del paciente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'FlagCustodyProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'FlagCustodyProduct';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un producto asociado a una admisión corresponde a medicación en custodia, devolviendo una bandera booleana.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de producto no debe ser nulo ni vacío; El número de admisión no debe ser nulo ni vacío', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna un BIT (0 o 1), nunca nulo, gracias al ISNULL y al valor por defecto del flag; Cuando hay múltiples registros, prevalece el más reciente según ID descendente; Si no existe registro coincidente, el resultado es 0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Producto en custodia; Medicación en custodia; Admisión; Farmacia', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Si el código de producto o el número de admisión son nulos o vacíos, retorna 0 sin consultar; [RETURN_RESULT] HCFARMEPD: Retorna el valor de MEDICACUSTODIA (o 0 si es nulo) del registro más reciente (mayor ID) que coincida con NUMINGRES y CODPRODUC', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de producto o número de admisión nulos/vacíos → Retorna 0 inmediatamente sin consultar la tabla else Consulta el indicador de custodia en HCFARMEPD', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HCFARMEPD', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'FlagCustodyProduct';
GO
