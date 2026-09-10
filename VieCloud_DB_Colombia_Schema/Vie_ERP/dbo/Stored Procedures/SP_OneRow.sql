
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SP_OneRow]
(
@source char(10),
@nameSchema varchar(100),
@nameTable varchar(100),
@column varchar(100),
@conditions varchar(3000)
)

AS
BEGIN

declare @tmp nvarchar(1000)

set @tmp = 'SELECT STRING_AGG(RTRIM('+ @column +'), '' + '') FROM INDIGO' + RTRIM(LTRIM(@source)) + '.' + @nameSchema + '.' + @nameTable + ' WHERE ' + @conditions  execute sp_executesql @tmp

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento utilitario genérico que consulta una sola columna de cualquier tabla o vista de cualquier base de datos del ecosistema Indigo (según la fuente o tenant indicado) y devuelve todos los valores de esa columna concatenados en una única cadena separada por '' + ''. Recibe dinámicamente el nombre del esquema, la tabla, la columna a extraer y las condiciones de filtro, construyendo y ejecutando SQL en tiempo de ejecución. Se usa como auxiliar interno para obtener un resumen consolidado de valores de un campo específico bajo ciertos criterios, aplicable a cualquier entidad de negocio (pacientes, ingresos, facturas, diagnósticos, etc.). Por su naturaleza de SQL dinámico, el objeto real que termina siendo consultado depende de los parámetros recibidos en cada llamada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_OneRow';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_OneRow';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Ejecuta dinámicamente un SELECT que devuelve, en una sola fila, la concatenación (STRING_AGG con separador '' + '') de los valores de una columna sobre una tabla en una BD con prefijo ''INDIGO''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El servidor debe contener una base de datos cuyo nombre siga el patrón ''INDIGO''+sufijo, accesible con el esquema y tabla indicados.; La columna debe existir en la tabla y ser compatible con RTRIM/STRING_AGG (tipo carácter).; La cláusula de condiciones debe ser una expresión SQL válida para WHERE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La base de datos destino se construye concatenando el prefijo ''INDIGO'' con el sufijo recibido, apuntando siempre a una BD con ese patrón de nombre.; El resultado es una única fila con los valores de la columna concatenados mediante el separador '' + '' usando STRING_AGG.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INDIGO<source>.<schema>.<table>: Construye y ejecuta vía sp_executesql un SELECT STRING_AGG(RTRIM(<column>), '' + '') sobre la tabla indicada filtrando por las condiciones recibidas, devolviendo el resultado al cliente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_OneRow';
-- GO
