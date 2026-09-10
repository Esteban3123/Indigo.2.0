
create VIEW [dbo].[VDashBoardPharmacy]
as
select ROW_NUMBER() OVER(ORDER BY ConsecutivoFarmacia ASC) as Row ,* from (
SELECT CODCONCEC AS ConsecutivoFarmacia,FECHAORDE AS FechaOrden,A.IPCODPACI AS CodigoPaciente,RTRIM(IPNOMCOMP) AS NombrePaciente,
RTRIM(UFUDESCRI) AS UnidadFuncional,ORDESTADO AS Estado,RTRIM(NOMMEDICO) AS NombreMedico,CODBODEGA AS Bodega,A.NUMINGRES AS Ingreso,
A.UFUCODIGO,COALESCE(NULLIF(A.CODCONCEP,''),'') AS ConsecutivoPrescripcion,COALESCE(NULLIF(A.CODCONCES,''),'') AS ConsecutivoInsumos,
RTRIM(CODCENCOS) AS CentroCostos,ORDTRANUE AS Tipo,A.NUMEFOLIO AS Folio,IDETIPHIS,CAST('' AS bit) AS Impresion 
FROM dbo.HCFARMEPC A 
INNER JOIN dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI 
INNER JOIN dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL 
INNER JOIN dbo.INUNIFUNC D ON A.UFUCODIGO=D.UFUCODIGO ) as Data
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Panel de control (dashboard) de órdenes farmacéuticas generadas en historia clínica. Consolida en una sola consulta la información de cada orden de despacho de medicamentos e insumos, cruzando el encabezado de la orden (HCFARMEPC) con los datos del paciente (nombre, cédula), el profesional de salud que la prescribió (médico tratante), la unidad funcional o servicio donde se originó y la bodega destino. Incluye el estado de la orden, tipo de transacción, folio, consecutivos de prescripción e insumos, centro de costos y número de ingreso, numerando cada registro con un consecutivo de fila para facilitar la paginación y visualización en tableros de control y reportes operativos de farmacia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VDashBoardPharmacy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VDashBoardPharmacy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone un listado consolidado y numerado de órdenes de farmacia con datos básicos del paciente, médico prescriptor y unidad funcional para alimentar un tablero/dashboard de farmacia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas maestras de pacientes, profesionales de la salud y unidades funcionales deben tener correspondencia con las claves registradas en el encabezado de órdenes de farmacia para que la orden sea visible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de farmacia cuyo paciente, profesional de la salud y unidad funcional existen en sus respectivos maestros (INNER JOIN).; A cada fila se le asigna un número de fila secuencial ordenado ascendentemente por el consecutivo de farmacia.; Los consecutivos de prescripción e insumos nunca se exponen como NULL: si están vacíos o nulos se devuelven como cadena vacía.; Los nombres de paciente, médico, unidad funcional y centro de costos se entregan sin espacios finales (RTRIM).; El indicador de impresión siempre se expone como bit en falso (0) por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de farmacia; Prescripción; Paciente; Profesional de la salud (médico); Unidad funcional; Bodega; Centro de costos; Ingreso; Folio; Insumos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFARMEPC: Devuelve solo órdenes de farmacia que tienen paciente, profesional y unidad funcional válidos en los maestros relacionados, enriquecidas con datos descriptivos y un número de fila secuencial.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPC; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VDashBoardPharmacy';
GO
