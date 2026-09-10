

create VIEW [dbo].[SOLAceptadas]
AS
SELECT     A.UFUCODIGO, A.UFUDESCRI, B.COMFECHA, B.COMAUTON, B.COMESTADO, B.COMPRIORI, B.CODUSUARI, B.UFUCODIGO AS Expr1, C.COMAUTO, C.NOVEDADES, 
                      C.CODUSUARI AS CODUSER, C.FECHAUTORI, C.AUTO, D.NOMUSUARI
FROM         dbo.INUNIFUNC AS A INNER JOIN
                      dbo.SOLCOMPRA AS B ON A.UFUCODIGO = B.UFUCODIGO INNER JOIN
                      dbo.SOL_AUTORI AS C ON B.COMAUTON = C.COMAUTO INNER JOIN
                      dbo.SEGusuaru AS D ON B.CODUSUARI = D.CODUSUARI AND C.CODUSUARI = D.CODUSUARI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las solicitudes de compra que han sido aceptadas o autorizadas, cruzando la unidad funcional solicitante (área o servicio), los datos de la solicitud (fecha, número, estado y prioridad), la información de la autorización (quién aprobó, cuándo y si tiene novedades pendientes) y el nombre completo del usuario que realizó y autorizó la solicitud. Integra el catálogo de unidades funcionales, el registro de solicitudes de compra, las autorizaciones emitidas y el directorio de usuarios del sistema. Se utiliza para hacer seguimiento y reportería de las solicitudes de compra ya aprobadas, permitiendo identificar qué área solicitó, quién autorizó y en qué fecha se formalizó la aprobación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SOLAceptadas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'SOLAceptadas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida las solicitudes de compra que cuentan con autorización registrada, mostrando datos de la unidad funcional, estado, prioridad, autorización y usuario responsable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre la unidad funcional de la solicitud y el catálogo de unidades funcionales.; La solicitud de compra debe tener un número de autorización (COMAUTON) que exista en SOL_AUTORI.; El usuario solicitante de la compra y el usuario de la autorización deben coincidir y existir en SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen solicitudes de compra que tengan una autorización asociada (INNER JOIN con SOL_AUTORI por COMAUTON = COMAUTO).; El usuario solicitante de la compra y el usuario que registró la autorización deben ser el mismo (B.CODUSUARI = D.CODUSUARI AND C.CODUSUARI = D.CODUSUARI).; Solo se incluyen solicitudes cuya unidad funcional exista en el catálogo INUNIFUNC.; Solo se incluyen registros cuyo usuario exista en el catálogo de seguridad SEGusuaru.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de compra; autorización; unidad funcional; usuario; estado de solicitud; prioridad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.SOLAceptadas: Devuelve un conjunto de resultados con solicitudes de compra autorizadas cruzando INUNIFUNC, SOLCOMPRA, SOL_AUTORI y SEGusuaru bajo coincidencia de usuario solicitante y autorizador.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INUNIFUNC; dbo.SOLCOMPRA; dbo.SOL_AUTORI; dbo.SEGusuaru', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'SOLAceptadas';
GO
