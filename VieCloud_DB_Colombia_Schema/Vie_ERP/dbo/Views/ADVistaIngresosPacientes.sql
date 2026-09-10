

/*                      dbo.INENTIDAD C ON dbo.ADINGRESO.CODENTIDA = dbo.INENTIDAD.CODENTIDA AND dbo.INPACIENT.CODENTIDA = dbo.INENTIDAD.CODENTIDA*/
CREATE VIEW [dbo].[ADVistaIngresosPacientes]
AS
SELECT      A.NUMINGRES AS Ingreso, A.IPCODPACI AS Identificacion, B.IPPRINOMB AS PrimerNombre, B.IPSEGNOMB AS SegundoNombre, 
                         B.IPPRIAPEL AS PrimerApellido, B.IPSEGAPEL AS SegundoApellido, RTRIM(C.NOMENTIDA) AS Entidad, A.IESTADOIN AS Estado,
                         CASE WHEN A.IESTADOIN = ' ' THEN 'Sin Confirmar Hoja de Trabajo' WHEN A.IESTADOIN = 'F' THEN 'Confirmada Hoja de Trabajo' WHEN A.IESTADOIN = 'A' THEN
                          'Anulado' WHEN A.IESTADOIN = 'C' THEN 'Cerrado' END AS EstadoIngreso,
			   concat(Rtrim(IPPRINOMB), ' ', Rtrim(IPSEGNOMB), ' ', Rtrim(IPPRIAPEL), ' ',Rtrim(IPSEGAPEL)) as NombrePaciente, B.IPTIPODOC AS TipoIdentificacion,
               B.IPFECNACI AS FechaNacimiento, A.TIPOINGRE AS TipoIngreso, A.IFECHAING AS FechaIngreso, A.CODICAMHO AS EstanciaCama, A.ILIQUIDAC AS TipoLiquidacion,
               A.IPRNOMBRE AS NombreResponsable, G.Name AS GrupoAtencion, A.IPTELEFON AS TelefonoResponsable,
               RTRIM(A.NUMINGRES) + ' - ' + RTRIM(B.IPCODPACI) + ' - ' + RTRIM(B.IPNOMCOMP) AS FullNombre, A.IINGREPOR AS OrigenIngreso, A.IAUTORIZA AS NumeroAutorizacion,
               A.GENCAREGROUP AS CodigoGrupoAten, A.CODENTIDA AS CodigoEntidad, A.UFUCODIGO As CodigoUnidadFuncional, F.UFUDESCRI As UnidadFuncional

FROM            dbo.ADINGRESO AS A 
                INNER JOIN dbo.INPACIENT AS B ON B.IPCODPACI = A.IPCODPACI 
                INNER JOIN dbo.INENTIDAD AS C ON C.CODENTIDA = B.CODENTIDA 
                INNER JOIN dbo.ADCENATEN AS D ON D.CODCENATE = A.CODCENATE 
                INNER JOIN dbo.INUNIFUNC AS F ON A.UFUCODIGO = F.UFUCODIGO
                INNER JOIN [Contract].CareGroup AS G ON G.Id = A.GENCAREGROUP
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los ingresos o admisiones de pacientes combinando datos de tres fuentes: el registro de ingreso (número de ingreso, identificación y estado), la ficha del paciente (nombres y apellidos completos) y la entidad aseguradora o pagadora a la que pertenece el paciente (EPS, ARS u otra). Permite consultar de forma unificada quién ingresó, bajo qué entidad y en qué estado se encuentra su admisión (sin confirmar, confirmada, anulada o cerrada). Es útil para reportes operativos de admisiones, búsqueda de pacientes por cédula o nombre, y seguimiento del ciclo de ingreso en urgencias, hospitalización y consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ADVistaIngresosPacientes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ADVistaIngresosPacientes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado de ingresos/admisiones con datos de identificación del paciente, entidad responsable de pago y estado del ingreso traducido a texto legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso debe tener un paciente existente en el maestro de pacientes (INNER JOIN por IPCODPACI); Cada paciente debe tener una entidad asociada existente en el directorio de entidades (INNER JOIN por CODENTIDA)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre completo del paciente se construye concatenando primer nombre, segundo nombre, primer apellido y segundo apellido separados por espacio; El nombre de la entidad se entrega sin espacios sobrantes a la derecha (RTRIM); La entidad mostrada corresponde a la entidad del paciente (INPACIENT.CODENTIDA), no necesariamente a la del ingreso (ADINGRESO.CODENTIDA); Solo se contemplan cuatro estados de ingreso reconocidos; cualquier otro valor de IESTADOIN produce NULL en EstadoIngreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso/admisión del paciente; Paciente; Entidad responsable de pago; Hoja de trabajo; Estado de ingreso (sin confirmar, confirmada, anulado, cerrado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADVistaIngresosPacientes: Devuelve una fila por ingreso con paciente y entidad; ingresos sin paciente o sin entidad asociada quedan excluidos por los INNER JOIN', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IESTADOIN = '' '' (espacio en blanco) → Estado del ingreso se muestra como ''Sin Confirmar Hoja de Trabajo''; si IESTADOIN = ''F'' → Estado del ingreso se muestra como ''Confirmada Hoja de Trabajo''; si IESTADOIN = ''A'' → Estado del ingreso se muestra como ''Anulado''; si IESTADOIN = ''C'' → Estado del ingreso se muestra como ''Cerrado''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ADVistaIngresosPacientes';
GO
