--Obtiene el dato de las ambulancias a las que el usuario esta autorizado
CREATE procedure [dbo].[SP_ConsultarAmbulanciasPorUsuario]
@userCode char(15)
as
SELECT		a.Id as idAmbulance,
			a.Codigo as codeAmbulance,
			a.Nombre as description,
			case when a.TipoAmbulancia = 2 then '1' else '0' end as isTAM,
			a.Placa as licensePlate
FROM		SEGusuaru AS U 
INNER JOIN	INPROFSAL AS P ON U.CODUSUARI = P.CODPROSAL
INNER JOIN	INAUTORIU AS AU ON AU.CODUSUARI = P.CODUSUARI
INNER JOIN	RCVEHICTRAS AS A ON A.Id = AU.PKVALORCO
WHERE		u.CODUSUARI = @userCode AND AU.PKTIPOCON = '11'
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las ambulancias y vehículos de traslado autorizados para un usuario específico del sistema. A partir del código de usuario, identifica el profesional de salud vinculado, verifica sus autorizaciones de tipo ''11'' (correspondiente a vehículos de transporte) en la tabla de autorizaciones, y retorna el listado de ambulancias habilitadas con su código, descripción, placa y si es de tipo TAM (Traslado Asistencial Medicalizado). Se usa para controlar qué ambulancias puede gestionar u operar cada usuario según sus permisos en el módulo de traslado de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las ambulancias a las que un usuario tiene autorización vigente, indicando si son de tipo TAM.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario debe existir en SEGusuaru y tener un profesional asociado en INPROFSAL (CODUSUARI = CODPROSAL).; El usuario debe tener registros de autorización en INAUTORIU con tipo de control ''11'' (autorización sobre vehículos/ambulancias).; El valor PKVALORCO de la autorización debe corresponder a un Id existente en RCVEHICTRAS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan vehículos cuya autorización tiene tipo de concepto ''11''.; El cruce usuario-profesional se realiza igualando CODUSUARI con CODPROSAL.; Únicamente se incluyen ambulancias explícitamente autorizadas al usuario consultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ambulancia; usuario; autorización; profesional de salud; TAM (Transporte Asistencial Medicalizado); vehículo de traslado; placa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RCVEHICTRAS: Cuando el usuario tiene autorizaciones con PKTIPOCON=''11'', devuelve los vehículos asociados marcando isTAM=''1'' si TipoAmbulancia=2, en caso contrario ''0''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si a.TipoAmbulancia = 2 → Marca la ambulancia como TAM (isTAM=''1'') else Marca la ambulancia como no TAM (isTAM=''0'')', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.SEGusuaru; dbo.INPROFSAL; dbo.INAUTORIU; dbo.RCVEHICTRAS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ConsultarAmbulanciasPorUsuario';
-- GO
