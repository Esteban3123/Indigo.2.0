

CREATE FUNCTION [dbo].[AutonumericoEgresoCama] (@Cedula as varchar(25))
RETURNS int 
AS
BEGIN

declare @Autonumerico int

select  distinct @Autonumerico= MAX(B.Autonumerico)-1 
	      FROM CHREGEGRE AS A INNER JOIN
	           (Select row_number()over (PARTITION by ipcodpaci order by ipcodpaci) as Autonumerico,IPCODPACI,FECEGRESO, MAX(NUMINGRES) AS NUMINGRES
                    from CHREGEGRE  
                    GROUP BY IPCODPACI,FECEGRESO,numingres) as B ON B.IPCODPACI = A.IPCODPACI
		WHERE B.IPCODPACI=@Cedula
RETURN @Autonumerico

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula el número de egresos anteriores al último registrado para un paciente, recibiendo como parámetro su cédula o documento de identidad. Consulta el historial de egresos hospitalarios (CHREGEGRE) del paciente y, usando una numeración secuencial de salidas ordenadas por fecha de egreso y número de ingreso, retorna el total de egresos previos al más reciente (MAX menos 1). Se utiliza para determinar cuántas veces ha egresado anteriormente un paciente, apoyando procesos de seguimiento de altas, reingresos y análisis de historial de hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AutonumericoEgresoCama';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AutonumericoEgresoCama';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula un consecutivo (penúltimo) de egresos del paciente, usado para identificar la cama del egreso previo o equivalente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro de egreso para el paciente identificado; de lo contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado equivale al máximo número de secuencia (row_number) de egresos del paciente menos 1, es decir, el penúltimo orden de egreso registrado.; La numeración se particiona por paciente y agrupa por paciente, fecha de egreso y número de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'egreso; paciente; ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve MAX(Autonumerico)-1 sobre la numeración de filas (row_number) de egresos agrupados por paciente, fecha de egreso y número de ingreso, filtrando por la cédula del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGEGRE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AutonumericoEgresoCama';
GO
