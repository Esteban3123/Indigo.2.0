
create VIEW [dbo].[HCVistaGestionHospitalaria]
AS
SELECT     ROW_NUMBER() OVER (ORDER BY A.CODICAMAS) AS NumeroFila, A.CODICAMAS AS CodigoCama, RTRIM(A.DESCCAMAS) AS Cama, 
dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion, dbo.ClaseCama(A.CODCLACAM) AS ClasedeCama, D .IPCODPACI AS Identificacion, D .NUMINGRES AS Ingreso, 
dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento, RTRIM(E.DESTIPEST) AS TipoEstancia, RTRIM(F.IPNOMCOMP) AS Paciente, CAST(0 AS BIT) AS Resultado, 
A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo, A.CODCENATE AS CentroAtencion, 
A.UFUCODIGO AS UnidadFuncional, A.ESTADCAMA AS EstadoCama
FROM         dbo.CHCAMASHO AS A INNER JOIN
                      dbo.ADCENATEN AS B ON A.CODCENATE = B.CODCENATE INNER JOIN
                      dbo.INUNIFUNC AS C ON A.UFUCODIGO = C.UFUCODIGO LEFT OUTER JOIN
                      dbo.CHREGESTA AS D ON A.CODICAMAS = D .CODICAMAS AND D .REGESTADO = 1 LEFT OUTER JOIN
                      dbo.CHTIPESTA AS E ON D .CODTIPEST = E.CODTIPEST LEFT OUTER JOIN
                      dbo.INPACIENT AS F ON D .IPCODPACI = F.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de gestión hospitalaria que muestra el estado actual de cada cama del hospital, combinando información del maestro de camas (CHCAMASHO) con el centro de atención, la unidad funcional y el registro activo de estancia del paciente internado en ese momento. Para cada cama expone su código, descripción, clase de habitación, clase de cama, tipo de aislamiento, tipo de estancia (urgencias, hospitalización, etc.) y el paciente (cédula, número de ingreso y nombre completo) que la ocupa actualmente, si la hubiere. Sirve como panel de control de camas u ocupación hospitalaria en tiempo real, permitiendo al personal administrativo y asistencial conocer qué camas están ocupadas, por quién y bajo qué modalidad de estancia, así como las camas libres disponibles por sede y servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaGestionHospitalaria';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'HCVistaGestionHospitalaria';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Provee una vista consolidada del estado actual de las camas hospitalarias junto con la información del paciente que las ocupa, su tipo de estancia y características de la cama, para gestión hospitalaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Toda cama debe tener un centro de atención válido en ADCENATEN; Toda cama debe tener una unidad funcional válida en INUNIFUNC; Solo se considera estancia vigente la que tiene REGESTADO = 1 en CHREGESTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se listan todas las camas existentes aunque no tengan paciente asignado (LEFT JOIN sobre CHREGESTA); Solo se muestra el estado de estancia vigente (REGESTADO=1), excluyendo históricos; Cada fila del resultado representa una cama única con numeración secuencial por código de cama; El campo Resultado siempre se devuelve como 0 (BIT)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Clase de habitación; Clase de cama; Tipo de aislamiento; Tipo de estancia; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Estado de cama; Traslado de cirugía; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve una fila por cama con datos del paciente solo si existe un registro de estancia activo (REGESTADO=1); de lo contrario, los campos de paciente/estancia quedan en NULL por el LEFT OUTER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CHREGESTA.REGESTADO = 1 → Se asocia el paciente y tipo de estancia a la cama else La cama se muestra sin paciente (campos en NULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'HCVistaGestionHospitalaria';
GO
