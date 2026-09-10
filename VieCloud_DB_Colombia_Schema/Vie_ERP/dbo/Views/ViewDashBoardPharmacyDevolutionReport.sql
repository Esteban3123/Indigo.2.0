
CREATE VIEW [dbo].[ViewDashBoardPharmacyDevolutionReport]
AS
select d.CODCONCEC Row,
RTRIM(LTRIM(d.IPCODPACI)) PatientCode, RTRIM(LTRIM(pa.IPNOMCOMP)) PatientName,
RTRIM(LTRIM(c.NUMINGRES)) AdmissionNumber,
RTRIM(LTRIM(c.UFUCODIGO)) + ' - ' + RTRIM(LTRIM(fu.UFUDESCRI)) FunctionalUnitCodeName,
cama.NUMCAMHOS BedNumber,
c.FECHDEVOL DevolutionDate,
RTRIM(LTRIM(d.CODPRODUC)) + ' - ' + RTRIM(LTRIM(pro.DESPRODUC)) ProductCodeName,
d.CANDEVOLV DevolutionQuantity, d.CANPENDIE PendingQuantity,
RTRIM(LTRIM(c.CODPROSAL)) + ' - ' + RTRIM(LTRIM(prof.NOMMEDICO)) ProfessionalCodeName, RTRIM(LTRIM(prof.TARJETAPR)) ProfessionalCard
from dbo.HCDEVMEDD d
inner join dbo.HCDEVMEDC c on c.CODCONCEC = d.CODCONCEC
inner join dbo.IHLISTPRO pro on pro.CODPRODUC = d.CODPRODUC
inner join dbo.IHBODEGAS as bo on bo.CODBODEGA = c.CODBODEGA
inner join dbo.INPACIENT AS pa on pa.IPCODPACI  = c.IPCODPACI
inner join dbo.INPROFSAL AS prof on prof.CODPROSAL = c.CODPROSAL 
inner join dbo.INUNIFUNC AS fu on fu.UFUCODIGO  = c.UFUCODIGO	
inner join dbo.ADCENATEN cent on cent.CODCENATE = c.CODCENATE 
inner join dbo.ADINGRESO as ing on ing.NUMINGRES = c.NUMINGRES 
left join dbo.CHCAMASHO cama on cama.CODICAMAS = ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '')
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del dashboard de devoluciones de medicamentos y dispositivos médicos en farmacia. Consolida información de las cabeceras y detalles de devoluciones (HCDEVMEDC y HCDEVMEDD) cruzando datos del paciente (cédula, nombre), el ingreso hospitalario (número de admisión), la unidad funcional o servicio donde se encontraba el paciente, la cama asignada, el producto devuelto con su descripción, las cantidades devueltas y pendientes, y el profesional de salud responsable. Sirve para el seguimiento y reporte operativo del proceso de devolución de medicamentos e insumos a bodega, permitiendo al área de farmacia monitorear en tiempo real qué productos fueron retornados, por qué paciente y en qué ingreso, facilitando auditorías de inventario y control de glosas o inconsistencias en la dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyDevolutionReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ViewDashBoardPharmacyDevolutionReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida información de devoluciones de medicamentos hospitalarios con datos del paciente, ingreso, profesional, unidad funcional, cama y producto, para reportería de dashboard farmacéutico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de devolución debe tener un encabezado correspondiente (HCDEVMEDC).; El producto, bodega, paciente, profesional, unidad funcional, centro de atención e ingreso referenciados deben existir en sus catálogos maestros para que la fila aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los códigos de paciente, ingreso, unidad funcional y profesional se devuelven sin espacios en blanco (RTRIM/LTRIM).; Los campos descriptivos combinados se presentan con formato ''CODIGO - DESCRIPCION''.; Una devolución sin cama asociada (CODCAMACT=0) no impide que la fila aparezca en el reporte.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de medicamentos; Paciente; Ingreso hospitalario; Unidad funcional; Cama hospitalaria; Producto farmacéutico; Bodega; Profesional de salud; Centro de atención; Cantidad devuelta; Cantidad pendiente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un registro por cada detalle de devolución (HCDEVMEDD) cuyo encabezado y entidades relacionadas (producto, bodega, paciente, profesional, unidad funcional, centro de atención, ingreso) existan; la cama es opcional vía LEFT JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ing.CODCAMACT = 0 → Se trata como cama vacía ('''') y por tanto el LEFT JOIN no enlaza ninguna cama. else Se convierte el código de cama actual del ingreso a varchar(15) para enlazar con el maestro de camas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCDEVMEDD; dbo.HCDEVMEDC; dbo.IHLISTPRO; dbo.IHBODEGAS; dbo.INPACIENT; dbo.INPROFSAL; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.ADINGRESO; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'ViewDashBoardPharmacyDevolutionReport';
GO
