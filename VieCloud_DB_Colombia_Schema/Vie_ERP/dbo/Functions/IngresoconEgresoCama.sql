
CREATE FUNCTION [dbo].[IngresoconEgresoCama] (@Cedula as varchar(25), @Ingreso as varchar(10))
RETURNS VARCHAR(10)
AS
BEGIN
declare @Compara varchar(10);
declare @IngresoFinal varchar(10)

   select @Compara=(SELECT MAX(NUMINGRES) AS NUMINGRES FROM CHREGEGRE WHERE IPCODPACI=@CEDULA GROUP BY IPCODPACI);
    IF @Ingreso = @Compara 
	      begin
		  SELECT 	@IngresoFinal=D.NUMINGRES
	   	   FROM   (SELECT C.IPCODPACI AS IPCODPACI, MIN(A.FECINIEST) AS FECINIEST, C.NUMINGRES AS NUMINGRES
                     FROM  CHREGESTA  AS A  INNER JOIN 
		             (SELECT MAX(numingres) AS numingres, ipcodpaci AS ipcodpaci
		                      from CHREGESTA where IPCODPACI = @Cedula
					  GROUP BY IPCODPACI) AS C ON @Cedula= A.IPCODPACI AND C.NUMINGRES = A.NUMINGRES GROUP BY C.IPCODPACI,C.NUMINGRES) as D INNER JOIN
		     (Select row_number()over (PARTITION by ipcodpaci order by ipcodpaci) as Autonumerico,IPCODPACI,FECEGRESO, MAX(NUMINGRES) AS NUMINGRES
                    from CHREGEGRE  where IPCODPACI = @Cedula
                    GROUP BY IPCODPACI,FECEGRESO,numingres) AS E ON E.Autonumerico = dbo.AutonumericoEgresoCama(@Cedula) and @Cedula = D.IPCODPACI
	                WHERE convert(date,D.FECINIEST,103)  BETWEEN convert(date,E.FECEGRESO,103)  AND  convert(date,E.FECEGRESO +15,103)  
	   end
	   ELSE if @Ingreso <> @Compara 
	   begin
	      SELECT 	  @IngresoFinal=D.NUMINGRES
	   	   FROM   (SELECT C.IPCODPACI AS IPCODPACI, MIN(A.FECINIEST) AS FECINIEST, C.NUMINGRES AS NUMINGRES
                          FROM  CHREGESTA  AS A INNER JOIN 
		                      (SELECT MAX(numingres) AS numingres, ipcodpaci AS ipcodpaci
		                               from CHREGESTA where IPCODPACI = @Cedula
					            GROUP BY IPCODPACI) AS C ON @Cedula= A.IPCODPACI AND C.NUMINGRES = A.NUMINGRES where C.IPCODPACI = @Cedula GROUP BY C.IPCODPACI,C.NUMINGRES) as D INNER JOIN
				   (SELECT  MAX(FECEGRESO) AS FECEGRESO, MAX(NUMINGRES) AS NUMINGRES 
                              from CHREGEGRE  where IPCODPACI = @Cedula
                    GROUP BY IPCODPACI) AS E ON @Cedula = D.IPCODPACI  
                          AND convert(date,D.FECINIEST,103)  BETWEEN convert(date,E.FECEGRESO,103)  AND  convert(date,E.FECEGRESO +15,103)  	
						  end 
RETURN @IngresoFinal

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si un ingreso hospitalario (número de ingreso) está relacionado con un reingreso por cama, verificando si la fecha de inicio de la estancia del paciente cae dentro de los 15 días posteriores a su último egreso registrado. Recibe la cédula del paciente y el número de ingreso como parámetros, y retorna el número de ingreso que cumple la condición de reingreso por cama, o nulo si no aplica. Cruza los egresos hospitalarios (CHREGEGRE) con los estados o estancias (CHREGESTA) del paciente para establecer si el nuevo ingreso ocurrió dentro de la ventana de tiempo definida tras el alta. Aplica una lógica diferenciada según si el ingreso consultado es el último registrado del paciente o uno anterior, apoyándose en la función AutonumericoEgresoCama para identificar el egreso de referencia correcto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'IngresoconEgresoCama';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'IngresoconEgresoCama';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el número de ingreso hospitalario de un paciente cuya fecha de inicio de estancia ocurre dentro de los 15 días posteriores a un egreso previo, vinculando readmisiones a un egreso anterior.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en CHREGEGRE (egresos) y CHREGESTA (estancias).; Debe haber al menos un ingreso registrado para el paciente para obtener el MAX(NUMINGRES).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ventana de readmisión considerada es de 15 días desde la fecha de egreso.; Solo retorna un NUMINGRES si la fecha de inicio de estancia está dentro del rango [FECEGRESO, FECEGRESO+15].; Las comparaciones de fechas se hacen a nivel de fecha (sin hora) en formato 103.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Egreso; Estancia hospitalaria; Cama; Readmisión dentro de 15 días', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve el NUMINGRES del paciente cuando la fecha de inicio de estancia (FECINIEST) cae entre la fecha de egreso (FECEGRESO) y FECEGRESO+15 días; si no se cumple, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El ingreso recibido es igual al MAX(NUMINGRES) del paciente en CHREGEGRE → Cruza estancias con egresos usando la función AutonumericoEgresoCama para seleccionar un egreso específico y valida la ventana de 15 días contra ese FECEGRESO. else Cuando el ingreso es distinto al máximo, cruza la estancia con el egreso de FECEGRESO máxima del paciente y valida la misma ventana de 15 días.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.AutonumericoEgresoCama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGEGRE; dbo.CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'IngresoconEgresoCama';
GO
