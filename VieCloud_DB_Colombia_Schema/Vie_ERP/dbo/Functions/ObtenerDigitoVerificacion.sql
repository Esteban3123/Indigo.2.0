CREATE  FUNCTION [dbo].[ObtenerDigitoVerificacion]
(
	@Nit NVARCHAR(20)
 )
 RETURNS VARCHAR(1)
 
 AS
 BEGIN
 DECLARE @dv INT
 DECLARE @Lenght INT
 DECLARE @SEQ NVARCHAR(30)
 DECLARE @Contador INT
 DECLARE @Multiplo INT
 DECLARE @Acumulador INT
 --Variables
set @Lenght = Len(LTRIM (RTRIM ((@Nit))))
set @SEQ = substring('716759534743413729231917130703', (31 - (@Lenght * 2)),31)
set @Contador = 1
set @dv = 0
--Calculo sobre secuencia
while (@Contador <= @Lenght)
begin
    --Sumo y multiplico
    set @dv = @dv + (cast(substring(@SEQ,1,2) as INT)) * (cast(substring(@Nit, @Contador, 1) as INT))
    --Recorto secuencia y aumento indice
    set @SEQ = substring(@SEQ,3,31)
    set @Contador = @Contador + 1
end
--Obtengo digito de verificacion
set @dv = 11 - (@dv - (floor(@dv / 11) * 11))
 
RETURN @dv
 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el dígito de verificación de un NIT (Número de Identificación Tributaria) según el algoritmo oficial de la DIAN. Recibe el NIT sin dígito de verificación y devuelve el carácter numérico correspondiente aplicando la secuencia de multiplicadores ponderados y el módulo 11. Se utiliza para validar o completar el NIT de empresas, proveedores, aseguradoras o entidades relacionadas en procesos de facturación, contratos y RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ObtenerDigitoVerificacion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ObtenerDigitoVerificacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el dígito de verificación de un NIT aplicando la fórmula estándar de ponderación módulo 11 utilizada en Colombia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El NIT debe contener únicamente caracteres numéricos, ya que cada carácter se convierte a INT individualmente.; La longitud del NIT (sin espacios) no puede exceder 15 dígitos, debido a que la secuencia de ponderadores tiene 30 caracteres (15 pares).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La secuencia de factores ponderadores utilizada es ''71,67,59,53,47,43,41,37,29,23,19,17,13,07,03'', tomando los últimos N pares según la longitud del NIT.; El cálculo aplica la fórmula: DV = 11 - (suma_ponderada MOD 11).; Cada dígito del NIT es multiplicado por su factor ponderador correspondiente según la posición.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'NIT; Dígito de verificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (scalar return): Retorna el resultado de 11 - (suma_ponderada MOD 11) como dígito verificador del NIT recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ObtenerDigitoVerificacion';
GO
