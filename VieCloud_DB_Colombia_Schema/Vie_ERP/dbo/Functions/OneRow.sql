-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[OneRow]
(
@source char(10),
@nameSchema varchar(100),
@nameTable varchar(100),
@column varchar(100),
@conditions varchar(3000)
)
RETURNS varchar(4000)
AS
BEGIN
declare @temporal varchar(1000)

 exec [dbo].[sp_OneRow] @source, @nameSchema, @nameTable ,@column, @conditions, @temporal

 return @temporal

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recupera el valor de una columna específica de una fila única en cualquier tabla o vista de la base de datos, pudiendo apuntar a distintas fuentes de datos según el parámetro de origen. Recibe como parámetros el esquema, la tabla, la columna deseada y una condición de filtro (equivalente a un WHERE), y devuelve el resultado como texto. Internamente delega la consulta dinámica al procedimiento almacenado SP_OneRow, actuando como envoltorio funcional para poder ser utilizada directamente en sentencias SELECT. Es un utilitario genérico de consulta dinámica que puede tocar cualquier entidad de negocio del sistema (pacientes, ingresos, órdenes médicas, facturación, etc.) dependiendo de los parámetros que reciba.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'OneRow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'OneRow';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Función envoltorio que delega en un procedimiento la obtención de un valor escalar dinámico desde una tabla/columna y condiciones dadas, devolviéndolo como varchar.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'OneRow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir y ser ejecutable el procedimiento dbo.sp_OneRow que resuelve la consulta dinámica sobre el esquema/tabla/columna/condiciones recibidos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'OneRow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Siempre retorna el valor de la variable local @temporal tras invocar al procedimiento; al no asignarse explícitamente desde un OUTPUT, el resultado retornado será NULL salvo que el procedimiento la modifique por referencia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'OneRow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.sp_OneRow', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'OneRow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'OneRow';
GO
