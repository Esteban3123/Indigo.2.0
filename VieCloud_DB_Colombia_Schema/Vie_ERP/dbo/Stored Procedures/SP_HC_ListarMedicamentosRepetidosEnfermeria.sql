
CREATE  PROCEDURE [dbo].[SP_HC_ListarMedicamentosRepetidosEnfermeria]
(
@Paciente varchar(25),
@CodigoMedicamento char(15),
@OpcionConsulta as int, -- 1:Programacion Medicamentos ; 2: Aplicación Medicamentos
@IDHCORDQUIMIO AS INT,
@CONSECPRESCRA AS INT
)
AS
BEGIN
	SET NOCOUNT ON;

if @OpcionConsulta = 1 begin

		select DISTINCT  A.FECHAREGISTRO AS 'Fecha Orden',Rtrim(C.Description) as 'Nombre Esquema',Rtrim(d.NOMMEDICO) as 'Nombre medico',Rtrim(b.INSTRUADMINIS) as 'Instrucciones',
		case B.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (1,@Paciente,@CodigoMedicamento, B.IDHCORDQUIMIO,1)  END AS 'Ultima Fecha Aplicacion',
		case B.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (2,@Paciente,@CodigoMedicamento,B.IDHCORDQUIMIO,1)  END AS 'Proxima Fecha Aplicacion'
		from [EHR].[HCORDQUIMIO] A
			INNER JOIN [EHR].Schemes c ON A.SchemesId = c.Id
			INNER JOIN [EHR].[HCORDMEDICAM] B ON A.ID = B.IDHCORDQUIMIO
			INNER JOIN dbo.INPROFSAL d ON d.CODPROSAL = A.CODPROSAL
		WHERE A.IPCODPACI = @Paciente AND A.ESTADO IN (1,2) AND B.CODPRODUC = @CodigoMedicamento and EXISTS  (select IDHCORDQUIMIO from HCHOJAMED  WHERE  MEDESTADO = 1 AND IDHCORDQUIMIO = A.ID )

end
else if @OpcionConsulta = 2 begin

		select DISTINCT B.ID, B.FECHAREGISTRO AS 'Fecha Orden',Rtrim(d.NOMMEDICO) as 'Nombre medico',Rtrim(A.INSTRUADMINIS) as 'Instrucciones' ,
			case A.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (1,@Paciente,@CodigoMedicamento, A.IDHCORDQUIMIO,2)  END AS 'Ultima Fecha Aplicacion',
			case A.MEDICAMENTOENCASA WHEN 1 THEN 'Ordenado para Administración domiciliaria' ELSE dbo.[FechasAplicacionMedicamentoOncologico] (2,@Paciente,@CodigoMedicamento,A.IDHCORDQUIMIO,2)  END AS 'Proxima Fecha Aplicacion'
		from [EHR].[HCORDMEDICAM] A 
		Inner Join [EHR].[HCORDQUIMIO] B ON A.IDHCORDQUIMIO = B.ID
		INNER JOIN dbo.INPROFSAL d ON d.CODPROSAL = B.CODPROSAL
		WHERE  B.IPCODPACI = @Paciente AND B.ESTADO IN (1,2) AND A.CODPRODUC = @CodigoMedicamento AND B.ID <> @IDHCORDQUIMIO and EXISTS  (select IDHCORDQUIMIO from HCHOJAMED  WHERE  MEDESTADO = 1 AND IDHCORDQUIMIO = B.ID )
		
end

end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de enfermería oncológica que busca medicamentos repetidos (el mismo producto) en órdenes de quimioterapia activas de un paciente. Tiene dos modos de consulta: el primero (opción 1) muestra la programación de aplicaciones del medicamento, devolviendo la fecha de la orden, el esquema de tratamiento, el médico tratante, las instrucciones de administración y las fechas de última y próxima aplicación; el segundo (opción 2) muestra las órdenes de quimioterapia donde ese medicamento ya fue aplicado o está en curso, excluyendo la orden actual para identificar duplicidades. Consulta las órdenes de quimioterapia (HCORDQUIMIO), el detalle de medicamentos del ciclo (HCORDMEDICAM), el catálogo de esquemas terapéuticos (Schemes), el maestro de profesionales (INPROFSAL) y la hoja de enfermería (HCHOJAMED) para validar que exista al menos una administración activa, apoyándose en la función FechasAplicacionMedicamentoOncologico para calcular las fechas clave. Su propósito es alertar a enfermería sobre medicamentos oncológicos que ya están siendo administrados al paciente en otra orden activa, previniendo duplicaciones en el tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Detecta y lista órdenes de quimioterapia activas donde un medicamento oncológico ya fue prescrito o está en aplicación al mismo paciente, alertando a enfermería sobre posibles duplicidades en el tratamiento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener órdenes de quimioterapia registradas; El medicamento consultado debe corresponder a un código válido en el detalle de la orden; Debe existir al menos un registro en la hoja de enfermería con estado activo (MEDESTADO=1) asociado a la orden; La opción de consulta debe ser 1 (programación) o 2 (aplicación)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes de quimioterapia con ESTADO IN (1,2) (activas o en curso); Solo se consideran órdenes con al menos un registro en la hoja de enfermería con MEDESTADO=1; Los medicamentos administrados en domicilio nunca muestran cálculo de fechas, sino un literal descriptivo; En modo aplicación (opción 2) siempre se excluye la orden actual del resultado para mostrar únicamente duplicados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Medicamento oncológico; Esquema de tratamiento; Orden médica; Hoja de enfermería; Administración domiciliaria; Duplicidad de medicamentos; Profesional de la salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @OpcionConsulta=1, retorna fecha de orden, esquema, médico, instrucciones y fechas de última/próxima aplicación de las órdenes activas (ESTADO IN (1,2)) del paciente con el medicamento indicado, sin excluir orden actual; [RETURN_RESULT] resultset: Cuando @OpcionConsulta=2, retorna las mismas órdenes activas pero excluyendo la orden actual (B.ID <> @IDHCORDQUIMIO) para identificar duplicidades en aplicación; [RETURN_RESULT] resultset: Cuando MEDICAMENTOENCASA=1, las columnas de última y próxima fecha de aplicación se sustituyen por el literal ''Ordenado para Administración domiciliaria'' en lugar de calcularse mediante la función de fechas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @OpcionConsulta = 1 → Ejecuta consulta de programación de medicamentos incluyendo el nombre del esquema terapéutico y todas las órdenes activas del paciente con el medicamento else Si @OpcionConsulta = 2, ejecuta consulta de aplicación excluyendo la orden actual (@IDHCORDQUIMIO) para detectar duplicidades; si MEDICAMENTOENCASA = 1 → Marca las fechas de aplicación con literal ''Ordenado para Administración domiciliaria'' else Calcula última y próxima fecha mediante dbo.FechasAplicacionMedicamentoOncologico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.FechasAplicacionMedicamentoOncologico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; EHR.HCORDMEDICAM; EHR.Schemes; dbo.INPROFSAL; dbo.HCHOJAMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosRepetidosEnfermeria';
-- GO
