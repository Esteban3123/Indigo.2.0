
CREATE PROCEDURE [Scheduling].[ListRenalTherapyAppointment]
(
 @AUTO Char(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    WITH Numeros AS (
        SELECT TOP (100) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Sesion 
        FROM sys.objects
    ),
    CitasEnumeradas AS (
        SELECT 
            C.*, 
            ROW_NUMBER() OVER (PARTITION BY C.IdHCORDPRON ORDER BY C.FECHORAIN) AS NumSesion 
        FROM AGASICITA C
    ),
    Sesiones AS (
        SELECT 
            A.AUTO, 
            A.IPCODPACI, 
            A.CANSERIPS, 
            N.Sesion 
        FROM HCORDPRON A 
        CROSS APPLY (SELECT TOP (A.CANSERIPS) Sesion FROM Numeros) N
    )
    SELECT 
        CASE 
            WHEN CE.CODESTCIT = 3 THEN '0'
            ELSE CAST(CE.CODESTCIT AS varchar(25))
        END AS EstadoIcono,
        S.Sesion,
        ISNULL(E.DESCREQUI, '') AS DescripcionEquipo,
        CE.FECHORAIN AS FechaInicial,
        CE.FECHORAFI AS FechaFinal,
		DATEDIFF(MINUTE, CE.FECHORAIN, CE.FECHORAFI) AS Duracion ,
        CAST('' AS varchar(250)) AS Disponibilidad,
        CAST('' AS varchar(250)) AS Mensaje,
        IIF(CE.CODAUTONU IS NULL, 0, 1) AS CitaAsignada,
        CONVERT(BIT, 0) AS AsignarCita,
        CE.CODAUTONU AS IdCita,
        E.ID AS IdEquipo,
		CE.CODESTCIT as EstadoCita,
		S.AUTO,
		convert(int,null) as CodigoDisponibilidad,
		CE.FECHORAIN AS FechaInicialCopia,
		CE.FECHORAFI AS FechaFinalCopia,
		CAST(0 AS BIT) AS Actualizar_Y_CrearCitaNueva,
		CAST('' AS varchar(50)) AS CodigoUsuarioReprogramo,
		CAST('' AS varchar(25)) AS Motivo,
		CAST('' AS varchar(200)) AS Justificacion,
		CAST('' AS varchar(200)) AS Observacion,
		E.ID AS IdEquipoOrigen,
		ISNULL(E.DESCREQUI, '') AS DescripcionEquipoOrigen
    FROM Sesiones S
    LEFT JOIN CitasEnumeradas CE 
        ON CE.IdHCORDPRON = S.AUTO 
        AND CE.NumSesion = S.Sesion
    LEFT JOIN AGEQUIPTRA E 
        ON E.ID = CE.IDEQUIPOTRA
    WHERE S.AUTO = @AUTO
    ORDER BY S.Sesion;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las sesiones de terapia renal (diálisis u otros procedimientos renales) asociadas a una orden médica específica, identificada por su código de orden (@AUTO). Combina la orden médica de la historia clínica (HCORDPRON) con las citas agendadas (AGASICITA) y los equipos o recursos físicos asignados (AGEQUIPTRA) para mostrar, sesión por sesión, el estado de cada cita, el equipo utilizado (silla, camilla, máquina de diálisis, etc.), las fechas y duración, y si la sesión ya tiene cita asignada o está pendiente. Se utiliza en el módulo de agendamiento de terapia renal para visualizar el plan completo de sesiones del paciente y facilitar la programación, reprogramación o seguimiento de cada una.', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListRenalTherapyAppointment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Scheduling', @level1type = N'PROCEDURE', @level1name = N'ListRenalTherapyAppointment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar las sesiones programadas (asignadas o pendientes) de una orden de terapia renal, mostrando para cada sesión la cita correspondiente, su estado, equipo asignado y campos auxiliares para reprogramación.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una orden/prescripción en HCORDPRON identificada por el AUTO recibido.; La cantidad de sesiones (CANSERIPS) debe estar definida en la orden y no exceder 100.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El número de filas devueltas por orden equivale a la cantidad de sesiones autorizadas (CANSERIPS), aunque no existan citas asignadas para todas.; Las sesiones se enumeran de 1 a CANSERIPS por orden cronológico (FECHORAIN) dentro de cada orden.; Se soporta hasta un máximo de 100 sesiones por orden (TOP 100 sobre sys.objects).; Cuando no existe cita asignada para una sesión, los campos provenientes de la cita y el equipo quedan en NULL o cadena vacía y CitaAsignada=0.; La duración se calcula en minutos como diferencia entre fecha inicial y final de la cita.; Los campos Disponibilidad, Mensaje, AsignarCita, Actualizar_Y_CrearCitaNueva, CodigoUsuarioReprogramo, Motivo, Justificación, Observación y CodigoDisponibilidad se devuelven vacíos/cero como placeholders para la capa cliente.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita; Sesión de terapia renal; Orden/Prescripción (HCORDPRON); Equipo de terapia; Estado de cita; Cantidad de sesiones IPS (CANSERIPS); Autorización (CODAUTONU)', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Se retorna un conjunto de resultados con una fila por cada sesión esperada de la orden (según CANSERIPS), enlazando la N-ésima cita cronológica de AGASICITA con la N-ésima sesión.; [RETURN_RESULT] (resultset): Si CODAUTONU es NULL la sesión se marca como no asignada (CitaAsignada=0); en caso contrario CitaAsignada=1.; [RETURN_RESULT] (resultset): Si CODESTCIT = 3 se expone EstadoIcono=''0'', de lo contrario el código de estado de la cita en texto.', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Estado de la cita = 3 → Se devuelve ''0'' como icono de estado else Se devuelve el código de estado original convertido a texto', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'sys.objects; AGASICITA; HCORDPRON; AGEQUIPTRA', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Scheduling', @level1type=N'PROCEDURE', @level1name=N'ListRenalTherapyAppointment';
-- GO
