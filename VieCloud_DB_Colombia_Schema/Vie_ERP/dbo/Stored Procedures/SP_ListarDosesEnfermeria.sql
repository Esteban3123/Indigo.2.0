/**** 
SP para listar las dosis que acaba de programar la enfermera para la construcción de los objetos Doses de UNIHEALTH
****/
CREATE PROCEDURE [dbo].[SP_ListarDosesEnfermeria]
(
  @Paciente as varchar(25),
  @Ingreso as char(10),
  @Producto as char(20),
  @Folio as nchar(20)
)
WITH RECOMPILE
AS
BEGIN
	SET NOCOUNT ON;
		
			SELECT 
				DOSISPROD as 'CANTIDAD DOSIS', A.FECPROAPL AS 'FECHA INICIO',
				RTRIM(A.CODUNIMED) AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA', A.DOSISPROD AS 'DOSIS', 'Medicamento' as 'Tipo', CODPRODUC as 'CODPRODUC'
				,IIF(A.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA'
			FROM HCHOJAMED A
			INNER JOIN INUNIMEDI H ON A.CODUNIMED = H.CODUNIMED
			WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND CODPRODUC = @Producto AND MEDESTADO = 1

UNION ALL
			SELECT 
				C.DOSISPROD as 'CANTIDAD DOSIS', B.FECPROGRAMACIONAPL AS 'FECHA INICIO',
				D.CODUNIMED AS 'COD UNIDAD MEDIDA', H.DESUNIMED AS 'UNIDAD MEDIDA', D.DOSISPROD AS 'DOSIS', 'Mezcla' as 'Tipo', D.CODPRODUC AS 'CODPRODUC'
				,IIF(D.DURACIDOS = 'Dosis Unica', 1, 0) AS 'DOSIS UNICA'
			FROM HCINFLIQC A
			INNER JOIN HCHOJMEZC B ON B.IDHCINFLIQC = A.CODCONCEC AND B.CABESTADO = 4
			INNER JOIN HCHOJMEZD C ON B.CONSECUTI = C.CONSECUTI AND C.CODPRODUC = @Producto
			INNER JOIN HCFARMEPD D ON A.NUMINGRES = D.NUMINGRES AND A.NUMEFOLIO = D.NUMEFOLIO AND D.CODPRODUC = @Producto
			INNER JOIN INUNIMEDI H ON D.CODUNIMED = H.CODUNIMED
			WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso AND D.CODPRODUC = @Producto AND A.NUMEFOLIO = @Folio
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las dosis programadas por enfermería para un paciente, ingreso y producto (medicamento) específicos, con el fin de construir los objetos de tipo Doses que consume el sistema UNIHEALTH. Combina dos fuentes: las dosis registradas directamente en la hoja de administración de medicamentos (HCHOJAMED) y las dosis provenientes de mezclas intravenosas o soluciones (HCINFLIQC, HCHOJMEZC, HCHOJMEZD, HCFARMEPD), unificando ambos conjuntos en un único resultado. Para cada dosis retorna la cantidad, la fecha de inicio programada, la unidad de medida (obtenida del catálogo INUNIMEDI), el código del producto y un indicador de si es dosis única. Recibe como parámetros la cédula del paciente, el número de ingreso, el código del producto farmacéutico y el número de folio de la hoja de líquidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDosesEnfermeria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarDosesEnfermeria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las dosis de medicamentos y mezclas recién programadas a un paciente en un ingreso específico, para alimentar la construcción de objetos de dosificación de enfermería.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y número de ingreso deben existir en HCHOJAMED o en HCINFLIQC; Las unidades de medida referenciadas deben existir en INUNIMEDI (INNER JOIN); Para mezclas, debe existir el folio de fórmula en HCFARMEPD asociado al ingreso; Para mezclas, la cabecera HCHOJMEZC debe estar en estado 4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran medicamentos activos (MEDESTADO = 1); Solo se consideran mezclas con cabecera en estado 4; Cada fila siempre incluye una clasificación de Tipo (''Medicamento'' o ''Mezcla''); La unidad de medida siempre se resuelve contra el catálogo INUNIMEDI; El filtro por folio (NUMEFOLIO) aplica únicamente a las mezclas, no a los medicamentos individuales', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Hoja de medicamentos; Mezclas (preparaciones farmacéuticas); Dosis; Dosis única; Unidad de medida; Programación de aplicación por enfermería; Folio de fórmula', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCHOJAMED: Devuelve dosis de medicamentos individuales solo cuando MEDESTADO = 1 para el paciente e ingreso indicados; [RETURN_RESULT] HCHOJMEZC: Devuelve dosis de mezclas solo cuando la cabecera de mezcla tiene CABESTADO = 4; [RETURN_RESULT] HCFARMEPD: Para mezclas, restringe el resultado al folio de fórmula (NUMEFOLIO) y producto indicados, unido por ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DURACIDOS = ''Dosis Unica'' → Marca el registro con bandera DOSIS UNICA = 1 else Marca DOSIS UNICA = 0; si Origen del registro: HCHOJAMED vs HCHOJMEZC/HCFARMEPD → Etiqueta el Tipo como ''Medicamento'' o ''Mezcla'' respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAMED; dbo.INUNIMEDI; dbo.HCINFLIQC; dbo.HCHOJMEZC; dbo.HCHOJMEZD; dbo.HCFARMEPD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarDosesEnfermeria';
-- GO
