

CREATE FUNCTION [dbo].[TipDocR256] (@Tipdoc as int)
RETURNS varchar (20)
AS
BEGIN

declare @grupo varchar(20)
SET @grupo=
     CASE WHEN @Tipdoc=1 THEN 'CC' 
          WHEN @Tipdoc=2 THEN 'CE' 
		  WHEN @Tipdoc=3 THEN 'TI'
		  WHEN @Tipdoc=4 THEN 'RC'
		  WHEN @Tipdoc=5 THEN 'PA'
		  WHEN @Tipdoc=8 THEN 'NUIP'
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte un código numérico interno de tipo de documento de identidad al código abreviado estándar utilizado en reportes y normativa colombiana (por ejemplo: 1=CC cédula de ciudadanía, 2=CE cédula de extranjería, 3=TI tarjeta de identidad, 4=RC registro civil, 5=PA pasaporte, 8=NUIP número único de identificación personal). Se usa para traducir el identificador numérico del tipo de documento del paciente al formato de texto requerido en la generación de RIPS, facturas y otros reportes oficiales. Recibe como parámetro el código numérico del tipo de documento y retorna su sigla correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipDocR256';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipDocR256';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Convierte un código interno numérico de tipo de documento de identidad a su abreviatura estándar (CC, CE, TI, RC, PA, NUIP) usada en reportes regulatorios (Resolución 256).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de entrada debe ser un entero correspondiente a un código interno de tipo de documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo mapea los códigos internos 1, 2, 3, 4, 5 y 8 a abreviaturas estándar; cualquier otro valor produce NULL; Las abreviaturas devueltas corresponden a la nomenclatura oficial de tipos de documento de identidad colombianos usados en reportes regulatorios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento de identidad; Cédula de Ciudadanía (CC); Cédula de Extranjería (CE); Tarjeta de Identidad (TI); Registro Civil (RC); Pasaporte (PA); NUIP; Resolución 256', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de documento = 1 → Retorna ''CC'' (Cédula de Ciudadanía); si Tipo de documento = 2 → Retorna ''CE'' (Cédula de Extranjería); si Tipo de documento = 3 → Retorna ''TI'' (Tarjeta de Identidad); si Tipo de documento = 4 → Retorna ''RC'' (Registro Civil); si Tipo de documento = 5 → Retorna ''PA'' (Pasaporte); si Tipo de documento = 8 → Retorna ''NUIP'' else Para cualquier otro valor retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipDocR256';
GO
