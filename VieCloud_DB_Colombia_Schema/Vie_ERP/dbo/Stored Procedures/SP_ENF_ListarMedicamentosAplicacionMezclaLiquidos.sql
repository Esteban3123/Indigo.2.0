
CREATE PROCEDURE [dbo].[SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos]
(
@CodigoPaciente varchar(25),
@CodigoIngreso AS CHAR(10),
@CodigoCentroAtencion AS CHAR(10),
@CodigoUnidadFuncional AS CHAR(10),
@ParametroRegistraMedicamentosSinCantidadDisponibles AS BIT,
@ListarInformacionCabecera AS INTEGER,
@IDCabeceraHojaMezclas INT,
@IDCabeceraOrdenMezcla INT

)
AS
BEGIN
	SET NOCOUNT ON;
  
 IF @ListarInformacionCabecera = 1 --listar informacion cabecera
 BEGIN

	 SELECT 
			C.CONSECUTI AS 'IDCabeceraHojaMezclas' , C.IDHCINFLIQC AS 'IDCabeceraOrdenMezcla',
			C.IPCODPACI AS 'Identificacion',C.NUMINGRES AS 'Ingreso', C.CODCENATE AS 'Codigo Centro', C.UFUCODIGO AS 'Codigo Unidad Funcional',
			RTRIM(C.NOMMEZCLA) AS 'Nombre Mezcla',RTRIM(C.INDAPLMED) AS 'Dosis', RTRIM(I.NOMMEDICO) AS 'Medico Prescrible',C.FECPROGRAMACIONAPL AS 'Fecha Programacion Para Aplicacion',
			CASE C.TIPMEZLIQ when '1' then 'Mezcla Continua' when '2' then 'Liquido' when '3' then 'Mezcla Frecuencia' when '4' then 'Mezcla Magistral' end as 'Tipo Mezcla',
			CASE c.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla' when '3' then 'Bolo Medicamento Mezcla' WHEN 4 THEN 'Infusion Liquidos' WHEN '5' THEN 'Bolo Medicamento Liquido' end as 'Solicitud',c.METAPLMED,
			CONCAT(c.INDAPLMED,' - ', c.INDAPLMEDADICIONALES) AS 'Instrucciones',
			C.DOSISUNICA, C.DOSISAPLICACION, C.CODUNIMEDIAPLICACION, RTRIM(x.CODUNIMED) +'-'+ RTRIM(x.DESUNIMED) AS 'DESUNIMED' , C.DURINFUSION, C.UNIDADINFUSION, C.DURFRECUENCIA, C.UNIDADFRECUENCIA,C.DURACIDOS, C.VALDURFIJ, C.UNIDURFIJ --campos de la mezcla magistral - Mezcla Frecuencia
		
		FROM HCHOJMEZC  C
			INNER JOIN dbo.INPROFSAL I WITH (NOLOCK) ON C.CODPROSAL = I.CODPROSAL
			LEFT JOIN dbo.INUNIMEDI x WITH (NOLOCK) ON c.CODUNIMEDIAPLICACION = x.CODUNIMED
		WHERE C.CONSECUTI = @IDCabeceraHojaMezclas
 end
 ELSE IF @ListarInformacionCabecera = 2 --Listar medicamentos detalle
 BEGIN
	
	IF @ParametroRegistraMedicamentosSinCantidadDisponibles = 1 BEGIN
    
			SELECT distinct RTRIM(A.CODPRODUC) AS Codigo, RTRIM(A.DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad', SUM(CAST(COALESCE(NULLIF(B.QUANTITY,0),0) AS numeric )) AS Fisico,CODDCIMED as 'DCI' 
			FROM dbo.IHLISTPRO A 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON a.CODPRODUC = v.CODPRODUC AND v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN Inventory.ATC AS C WITH (NOLOCK) ON A.CODPRODUC = C.Code 
			INNER JOIN Inventory.InventoryProduct AS P WITH (NOLOCK) ON C.Id = P.ATCId 
			LEFT OUTER JOIN Inventory.PhysicalInventory AS B WITH (NOLOCK) ON B.ProductId = P.id AND B.WarehouseId IN (select Id from Inventory.Warehouse
			WHERE CodeCenterAttention = @CodigoCentroAtencion AND v.CONSECUTI =  @IDCabeceraHojaMezclas
			UNION ALL 
			SELECT Id from Inventory.Warehouse where CodeCenterAttention IS NULL) 
			INNER JOIN Inventory.Warehouse AS E WITH (NOLOCK) ON B.WarehouseId = E.Id 
			WHERE A.TIPPRODUC IN ('1','3') AND A.PROESTADO = 1 GROUP BY A.CODPRODUC,A.DESPRODUC,v.CANTIUTIL,CODDCIMED 

	END
    ELSE  begin
			
			/*SELECT distinct RTRIM(A.CODPRODUC) AS Codigo, RTRIM(DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad',A.CANACTPRO AS Fisico,B.CODDCIMED as 'DCI'
			FROM dbo.HCFISIPRO A
			INNER JOIN dbo.IHLISTPRO B WITH (NOLOCK) ON A.CODPRODUC=B.CODPRODUC 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON a.CODPRODUC = v.CODPRODUC and v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN HCHOJMEZC c WITH (NOLOCK) ON v.CONSECUTI = c.CONSECUTI
			WHERE A.IPCODPACI= @CodigoPaciente AND A.NUMINGRES=@CodigoIngreso AND A.CODCENATE=@CodigoCentroAtencion AND A.UFUCODIGO=@CodigoUnidadFuncional 
				  AND B.TIPPRODUC IN ('1','3') AND PROESTADO='1' and v.CONSECUTI =  @IDCabeceraHojaMezclas*/

			SELECT distinct RTRIM(B.CODPRODUC) AS Codigo, RTRIM(B.DESPRODUC) AS Medicamento,v.CANTIUTIL AS 'Cantidad',A.CANACTPRO AS Fisico,B.CODDCIMED as 'DCI'
			FROM 
		     HCHOJMEZC c WITH (NOLOCK) 
			INNER JOIN HCHOJMEZD v WITH (NOLOCK) ON v.CONSECUTI = c.CONSECUTI  and v.CONSECUTI =  @IDCabeceraHojaMezclas
			INNER JOIN dbo.IHLISTPRO B WITH (NOLOCK) ON b.CODPRODUC = v.CODPRODUC
			LEFT JOIN HCFISIPRO  A on A.CODPRODUC = V.CODPRODUC AND a.NUMINGRES = c.NUMINGRES and a.IPCODPACI = c.IPCODPACI 
			WHERE A.IPCODPACI= @CodigoPaciente AND A.NUMINGRES=@CodigoIngreso AND A.CODCENATE=@CodigoCentroAtencion  AND A.UFUCODIGO=@CodigoUnidadFuncional
				  AND B.TIPPRODUC IN ('1','3') AND B.PROESTADO='1' and v.CONSECUTI =  @IDCabeceraHojaMezclas
	
	END
  END
 end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de enfermería que lista los medicamentos y mezclas intravenosas programadas para aplicación a un paciente durante su ingreso hospitalario. Opera en dos modos: cuando se solicita información de cabecera (modo 1), retorna los datos generales de la hoja de mezcla —nombre de la mezcla, tipo (continua, líquido, magistral o por frecuencia), vía de administración, dosis, instrucciones, duración de infusión, frecuencia y el médico prescriptor— cruzando la tabla de mezclas HCHOJMEZC con el maestro de profesionales INPROFSAL y las unidades de medida INUNIMEDI. Cuando se solicita el detalle de medicamentos componentes (modo 2), lista los productos farmacéuticos que conforman la mezcla con su cantidad requerida y existencia física disponible, con la opción de mostrar medicamentos aunque no tengan stock disponible en el centro de atención. Se utiliza en el módulo de enfermería para que el personal pueda visualizar y gestionar la preparación y aplicación de mezclas intravenosas y líquidos en el contexto de un ingreso (hospitalización) específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y retorna la información de cabecera y/o el detalle de medicamentos de una hoja de mezclas/líquidos para aplicación de enfermería, mostrando el stock disponible bien sea desde el inventario por bodega o desde el stock asignado al paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una hoja de mezclas (HCHOJMEZC) con el consecutivo indicado; Para el modo detalle con stock por paciente, deben existir registros en HCFISIPRO que coincidan con paciente, ingreso, centro de atención y unidad funcional; Para el modo detalle con stock por inventario, debe existir mapeo entre IHLISTPRO, Inventory.ATC e Inventory.InventoryProduct; El modo de listado debe ser 1 (cabecera) o 2 (detalle); otros valores no producen resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos cuyo tipo (TIPPRODUC) sea ''1'' o ''3'' y estén activos (PROESTADO=1); El detalle de medicamentos siempre se restringe a la hoja de mezclas indicada (CONSECUTI); Cuando se calcula stock por inventario, solo se consideran bodegas del centro de atención solicitado o bodegas sin centro asignado (globales); El stock físico nulo o cero se trata como 0 mediante COALESCE/NULLIF; El procedimiento es de solo lectura: no realiza modificaciones de datos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hoja de mezclas; Orden de mezcla; Medicamentos; Mezcla continua; Mezcla magistral; Liquidos endovenosos; Bolo; Infusión; Dosis; Unidad de medida; Médico prescriptor; Inventario físico de medicamentos; Bodega/Centro de atención; Clasificación ATC; DCI (Denominación Común Internacional); Paciente; Ingreso hospitalario; Unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCHOJMEZC: Cuando modo=1, retorna una fila con datos de la cabecera de la hoja de mezclas filtrada por su consecutivo, incluyendo descripciones traducidas de tipo de mezcla y método de aplicación; [RETURN_RESULT] Inventory.PhysicalInventory: Cuando modo=2 y flag de cantidades disponibles=1, retorna medicamentos del detalle con stock físico agregado desde Inventory.PhysicalInventory para bodegas del centro de atención o bodegas sin centro; [RETURN_RESULT] dbo.HCFISIPRO: Cuando modo=2 y flag=0, retorna medicamentos del detalle con stock físico tomado de HCFISIPRO filtrado por paciente, ingreso, centro y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Modo de listado = 1 (cabecera) → Retorna información de la cabecera de la hoja de mezclas, incluyendo tipo de mezcla, método de aplicación, médico prescriptor, dosis, instrucciones e información de infusión/frecuencia else Si modo = 2, retorna el detalle de medicamentos asociados a la hoja de mezcla; si Modo = 2 y se permite registrar medicamentos sin cantidades disponibles (flag = 1) → Calcula el stock físico sumando PhysicalInventory por bodegas del centro de atención (o bodegas sin centro asociado), considerando solo productos activos de tipo ''1'' o ''3'' else Calcula el físico desde HCFISIPRO filtrando por paciente, ingreso, centro de atención y unidad funcional; si Tipo de mezcla (TIPMEZLIQ) → Se traduce a etiqueta: 1=Mezcla Continua, 2=Liquido, 3=Mezcla Frecuencia, 4=Mezcla Magistral; si Método de aplicación (METAPLMED) → Se traduce a: 1=Bolo Mezcla, 2=Infusion Mezcla, 3=Bolo Medicamento Mezcla, 4=Infusion Liquidos, 5=Bolo Medicamento Liquido', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJMEZC; dbo.HCHOJMEZD; dbo.INPROFSAL; dbo.INUNIMEDI; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Warehouse; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMedicamentosAplicacionMezclaLiquidos';
-- GO
