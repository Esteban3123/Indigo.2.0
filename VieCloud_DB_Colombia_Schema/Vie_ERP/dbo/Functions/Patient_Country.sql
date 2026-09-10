-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[Patient_Country] (@IPCODPACI as varchar(25))
RETURNS varchar (300)
AS
BEGIN

declare @Country varchar(300)

 SET @Country  = (select Rtrim(E.Name)
					from INPACIENT A WITH(NOLOCK) 
					inner join Common.Country E  WITH(NOLOCK) on A.IDPAIS = E.ID
					where A.IPCODPACI = @IPCODPACI
				  )

    RETURN @Country  
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que devuelve el nombre del país de nacionalidad u origen de un paciente a partir de su cédula o código de paciente (IPCODPACI). Consulta el registro del paciente en la tabla de información de pacientes (INPACIENT) y cruza con el catálogo de países (Common.Country) para obtener el nombre legible del país. Se utiliza para mostrar la nacionalidad del paciente en reportes, historia clínica y formularios de admisión. Útil cuando se necesita saber de qué país es el paciente ingresado o registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Country';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Patient_Country';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el nombre del país asociado a un paciente específico, resolviendo la relación entre el paciente y el catálogo de países.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un paciente con el identificador suministrado en INPACIENT.; El país asignado al paciente debe existir en el catálogo Common.Country.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre del país se devuelve sin espacios en blanco a la derecha (RTRIM).; Solo se obtiene el país si existe correspondencia entre el país asignado al paciente y el catálogo de países.; Si el paciente no existe o no tiene país asociado válido, el resultado es NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; país', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.Country: Retorna el nombre del país (RTRIM) cuando el paciente coincide por IPCODPACI y su IDPAIS empata con Common.Country.ID; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; Common.Country', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Patient_Country';
GO
