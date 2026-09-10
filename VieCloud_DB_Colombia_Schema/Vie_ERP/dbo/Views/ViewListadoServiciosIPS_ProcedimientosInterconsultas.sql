
CREATE VIEW [dbo].[ViewListadoServiciosIPS_ProcedimientosInterconsultas]
AS
SELECT        RTRIM(CODSERIPS) AS Codigo, RTRIM(DESSERIPS) AS Servicio, TIPSERTER AS Terapia, RTRIM(CODSERIPS) + ' - ' + RTRIM(DESSERIPS) AS CodigoDescripcion, SERREASIT AS ServicioRealizaSitio, 
                         IPSSERIAD AS ServicioSeriado, SERIPSDASH, TIPSERIPS
FROM            dbo.INCUPSIPS
WHERE        (TIPSERIPS = '6') AND (SIPSESTADO = '1')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los servicios de procedimientos e interconsultas activos registrados en el catálogo de servicios de la IPS (tabla INCUPSIPS), filtrando únicamente los de tipo 6 (procedimientos/interconsultas) con estado activo. Expone el código CUPS, la descripción del servicio, el tipo de terapia, una combinación código-descripción para selección en formularios, si el servicio se realiza en sitio, si es seriado y otros atributos de configuración. Se usa como fuente de datos para desplegables y validaciones en módulos clínicos donde se ordenan interconsultas o procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Listar los servicios IPS activos clasificados como procedimientos/interconsultas, exponiendo su código, descripción y atributos asociados (terapia, sitio de realización, seriado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen servicios cuyo tipo corresponde a interconsultas/procedimientos (TIPSERIPS=''6'').; Solo se exponen servicios activos (SIPSESTADO=''1'').; El código y la descripción se entregan sin espacios sobrantes a la derecha (RTRIM) y se ofrece una representación combinada ''Código - Descripción''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Servicios IPS; Procedimientos; Interconsultas; Terapia; Servicio seriado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.INCUPSIPS: Cuando TIPSERIPS=''6'' y SIPSESTADO=''1'' se retorna el servicio en el listado; en caso contrario se excluye.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewListadoServiciosIPS_ProcedimientosInterconsultas';
GO
